using System.Windows.Input;

namespace Atmega.Flasher.Commands {
    public class FlasherCommands {

        private readonly static RoutedCommand _readDevice = new RoutedCommand();
        private readonly static RoutedCommand _writeDevice = new RoutedCommand();
        private readonly static RoutedCommand _eraseDevice = new RoutedCommand();
        private readonly static RoutedCommand _verifyDevice = new RoutedCommand();
        private readonly static RoutedCommand _lockBitsDevice = new RoutedCommand();
        private readonly static RoutedCommand _settings = new RoutedCommand();

        public static RoutedCommand ReadDevice {
            get { return _readDevice; }
        }

        public static RoutedCommand WriteDevice {
            get { return _writeDevice; }
        }

        public static RoutedCommand EraseDevice {
            get { return _eraseDevice; }
        }

        public static RoutedCommand VerifyDevice {
            get { return _verifyDevice; }
        }

        public static RoutedCommand LockBits {
            get { return _lockBitsDevice; }
        }

        public static RoutedCommand Settings {
            get { return _settings; }
        }
    }
}
