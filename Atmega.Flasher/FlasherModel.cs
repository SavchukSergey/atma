﻿using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Atmega.Flasher.AvrIsp;
using Atmega.Flasher.Hex;
using Atmega.Flasher.Models;
using Atmega.Hex;

namespace Atmega.Flasher {
    public class FlasherModel : INotifyPropertyChanged {

        private HexBoard _eepromHexBoard = new HexBoard();
        private HexBoard _flashHexBoard = new HexBoard();
        private Dispatcher _dispatcher;

        public FlasherModel() {
            _eepromHexBoard[0] = null;
            _flashHexBoard[0] = null;
            _dispatcher = Dispatcher.CurrentDispatcher;
        }

        public void OpenFile(string filePath) {
            var hexFile = HexFile.Load(filePath);
            FlashHexBoard = HexBoard.From(hexFile);
            //todo: split eeprom & flash
        }

        public HexBoard EepromHexBoard {
            get {
                return _eepromHexBoard;
            }
            set {
                _eepromHexBoard = value;
                OnPropertyChanged();
            }
        }

        public HexBoard FlashHexBoard {
            get {
                return _flashHexBoard;
            }
            set {
                _flashHexBoard = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null) {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public DeviceOperation ReadDevice(DeviceOperation op, CancellationToken cancellationToken) {
            op.FlashSize += FlashSize;
            op.EepromSize += EepromSize;

            byte[] eepData;
            byte[] flashData;
            using (var programmer = CreateProgrammer(op, cancellationToken)) {
                programmer.Start();

                eepData = programmer.ReadPage(0, EepromSize, AvrMemoryType.Eeprom);
                flashData = programmer.ReadPage(0, FlashSize, AvrMemoryType.Flash);
                programmer.Stop();
            }

            EepromHexBoard = HexBoard.From(eepData);
            FlashHexBoard = HexBoard.From(flashData);

            return op;
        }

        public async Task<DeviceOperation> ReadDeviceAsync(DeviceOperation op, CancellationToken cancellationToken) {
            return await Task.Run(() => ReadDevice(op, cancellationToken), cancellationToken);
        }

        public int EepromSize {
            get { return 1024; }
        }

        public int FlashSize {
            get { return 32768; }
        }

        public void SaveFile(string fileName) {
            var hfb = new HexFileBuilder();
            foreach (var sourceLine in FlashHexBoard.Lines) {
                hfb.SetAddress(sourceLine.Address);
                foreach (var bt in sourceLine.Bytes) {
                    hfb.WriteByte(bt.Value);
                }
            }
            var hf = hfb.Build();
            hf.Save(fileName);
        }

        private static IProgrammer CreateProgrammer(DeviceOperation progress, CancellationToken cancellationToken) {
            var settings = FlasherConfig.Read();
            var inner = CreateProgrammerFromConfig(settings);
            var programmer = new ProgressTrackerProgrammer(inner, progress, cancellationToken);
            return programmer;
        }

        private static IProgrammer CreateProgrammerFromConfig(FlasherConfig settings) {
            //return new StubProgrammer();
            var set = settings.AvrIsp;
            var port = set.ComPortSettings.CreateSerialPort();
            return new AvrIspProgrammer(new AvrIspClient(port));
        }
    }
}
