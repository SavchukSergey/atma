﻿using System.Threading.Tasks;

namespace Atmega.Flasher.Views.Operations {
    /// <summary>
    /// Interaction logic for EraseDeviceWindow.xaml
    /// </summary>
    public partial class EraseDeviceWindow : BaseDeviceOperationWindow {

        public EraseDeviceWindow() {
            InitializeComponent();
        }

        protected override Task<bool> Execute(DeviceOperation op) {
            return Model.EraseDeviceAsync(op);
        }
    }
}