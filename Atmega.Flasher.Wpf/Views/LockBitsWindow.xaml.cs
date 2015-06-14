using System.Windows;
using System.Windows.Input;
using Atmega.Flasher.Commands;
using Atmega.Flasher.Devices;
using Atmega.Flasher.Models;
using Atmega.Flasher.Views.Operations;

namespace Atmega.Flasher.Views {
    /// <summary>
    /// Interaction logic for LockBitsWindow.xaml
    /// </summary>
    public partial class LockBitsWindow : Window {
        public LockBitsWindow() {
            InitializeComponent();
        }

        private void CloseCommand(object sender, ExecutedRoutedEventArgs e) {
            Close();
        }

        private void SaveCommand(object sender, ExecutedRoutedEventArgs e) {
            var deviceBits = (DeviceBits)DeviceBitsView.DataContext;
            deviceBits.ApplyTo(Model.LocksHexBoard);

            FlasherCommands.WriteLockBits.Execute(null, Owner);
            Close(); 
        }

        private void LockBitsWindow_OnLoaded(object sender, RoutedEventArgs e) {
            var dlg = new ReadLocksWindow {
                DataContext = new FlasherOperationModel(Model),
                Owner = this
            };
            dlg.ShowDialog();

            var settings = Model.Config;
            var lockBits = settings.Device.LockBits;

            var locksData = Model.LocksHexBoard;
            lockBits.ApplyFrom(locksData);

            DeviceBitsView.DataContext = lockBits;
        }

        protected FlasherModel Model {
            get { return ((FlasherModel)DataContext); }
        }
    }
}
