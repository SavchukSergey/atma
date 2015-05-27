using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Atmega.Flasher.Views {
    /// <summary>
    /// Interaction logic for ReadDeviceWindow.xaml
    /// </summary>
    public partial class ReadDeviceWindow : Window {

        private Task _task;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public ReadDeviceWindow() {
            InitializeComponent();
        }

        private async void ReadDeviceWindow_OnLoaded(object sender, RoutedEventArgs e) {
            var m = Model;
            var op = new ObservableDeviceOperation();
            operationView.DataContext = op;
            try {
                _task = m.ReadDeviceAsync(op, _cts.Token);
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
