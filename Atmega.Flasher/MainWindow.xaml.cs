using System;
using System.Windows;
using System.Windows.Input;
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

        private void ReadDeviceCommand(object sender, ExecutedRoutedEventArgs e) {
            _model.ReadDevice();
        }
    }
}
