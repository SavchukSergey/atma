using System.Windows;
using System.Windows.Input;
using Atmega.Flasher.Models;

namespace Atmega.Flasher.Views {
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window {
        public SettingsWindow() {
            InitializeComponent();
        }

        private void CloseCommand(object sender, ExecutedRoutedEventArgs e) {
            Close();
        }

        private void SaveCommand(object sender, ExecutedRoutedEventArgs e) {
            Model.Save();
            Close();
        }

        protected FlasherConfig Model {
            get { return DataContext as FlasherConfig; }
        }

    }
}
