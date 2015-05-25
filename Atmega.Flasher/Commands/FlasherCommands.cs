using System.Windows.Input;

namespace Atmega.Flasher.Commands {
    public class FlasherCommands {

        private readonly static RoutedCommand _readDevice = new RoutedCommand();

        public static RoutedCommand ReadDevice {
            get { return _readDevice; }
        }
    }
}
