using System;
using System.Windows;
using System.Windows.Input;
using Atmega.Flasher.Views;
using Microsoft.Win32;

namespace Atmega.Flasher {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private readonly FlasherModel _model = new FlasherModel();

        public FlasherModel Model { get { return _model; } }

        public MainWindow() {
            InitializeComponent();
            DataContext = _model;
        }

        private void OpenCommand(object sender, ExecutedRoutedEventArgs e) {
            var dlg = new OpenFileDialog {
                Filter = "Intel Hex File (.hex)|*.hex"
            };

            var result = dlg.ShowDialog();

            if (result == true) {
                _model.OpenFile(dlg.FileName);
            }
        }

        private void SaveCommand(object sender, ExecutedRoutedEventArgs e) {
            var dlg = new SaveFileDialog {
                Filter = "Intel Hex File (.hex)|*.hex"
            };

            var result = dlg.ShowDialog();

            if (result == true) {
                _model.SaveFile(dlg.FileName);
            }
        }

        private void ReadDeviceCommand(object sender, ExecutedRoutedEventArgs e) {
            var dlg = new ReadDeviceWindow {
                DataContext = _model,
                Owner = this
            };
            dlg.ShowDialog();
        }

        private void SettingsCommand(object sender, ExecutedRoutedEventArgs e) {
            _model.Settings.Reload();
            var dlg = new SettingsWindow {
                DataContext = _model.Settings,
                Owner = this
            };
            dlg.ShowDialog();
        }

    }
}
