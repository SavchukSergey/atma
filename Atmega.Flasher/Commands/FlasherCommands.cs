using System.Windows.Input;

namespace Atmega.Flasher.Commands {
    public class FlasherCommands {

        private readonly static RoutedCommand _readDevice = new RoutedCommand();
        private readonly static RoutedCommand _settings = new RoutedCommand();

        public static RoutedCommand ReadDevice {
            get { return _readDevice; }
        }

        public static RoutedCommand Settings {
            get { return _settings; }
        }
    }
}
