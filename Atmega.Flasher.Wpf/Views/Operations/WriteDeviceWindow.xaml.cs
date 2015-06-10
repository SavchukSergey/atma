﻿using System.Threading.Tasks;

namespace Atmega.Flasher.Views.Operations {
    /// <summary>
    /// Interaction logic for WriteDeviceWindow.xaml
    /// </summary>
    public partial class WriteDeviceWindow : BaseDeviceOperationWindow {

        public WriteDeviceWindow() {
            InitializeComponent();
        }

        protected override Task<bool> Execute(DeviceOperation op) {
            return Model.WriteDeviceAsync(op);
        }
    }
}
