namespace Atmega.Flasher.Models {
    public class FlasherOperationModel {

        private readonly FlasherModel _flasher;
        private readonly ObservableDeviceOperation _deviceOperation = new ObservableDeviceOperation();

        public FlasherOperationModel(FlasherModel flasher) {
            _flasher = flasher;
            _deviceOperation = new ObservableDeviceOperation();
        }

        public FlasherModel Flasher {
            get { return _flasher; }
        }

        public DeviceOperation DeviceOperation {
            get { return _deviceOperation; }
        }
    }
}
