using System.Windows;

namespace Atmega.Flasher.Views {
    /// <summary>
    /// Interaction logic for ReadDeviceWindow.xaml
    /// </summary>
    public partial class ReadDeviceWindow : Window {
        public ReadDeviceWindow() {
            InitializeComponent();
        }

        private void ReadDeviceWindow_OnLoaded(object sender, RoutedEventArgs e) {
            var m = Model;
            m.ReadDevice();
            Close();
        }

        protected FlasherModel Model {
            get { return DataContext as FlasherModel; }
        }
    }
}
