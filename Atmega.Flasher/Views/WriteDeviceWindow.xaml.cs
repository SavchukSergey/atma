using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Atmega.Flasher.Models;

namespace Atmega.Flasher.Views {
    /// <summary>
    /// Interaction logic for WriteDeviceWindow.xaml
    /// </summary>
    public partial class WriteDeviceWindow : Window {

        private Task _task;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public WriteDeviceWindow() {
            InitializeComponent();
        }

        private async void WriteDeviceWindow_OnLoaded(object sender, RoutedEventArgs e) {
            var m = Model;
            var op = new ObservableDeviceOperation();
            OperationView.DataContext = op;
            try {
                _task = m.WriteDeviceAsync(op, _cts.Token);
                await _task;
                Close();
            } catch (OperationCanceledException) {
            }
        }

        protected FlasherModel Model {
            get { return DataContext as FlasherModel; }
        }

        protected override void OnClosed(EventArgs e) {
            _cts.Cancel();
            base.OnClosed(e);
        }
    }
}
