using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Atmega.Flasher.Models;

namespace Atmega.Flasher.Views {
    public abstract class BaseDeviceOperationWindow : Window {

        protected readonly CancellationTokenSource _cts = new CancellationTokenSource();

        protected BaseDeviceOperationWindow() {
            Loaded += BaseDeviceOperationWindow_Loaded;
        }

        private async void BaseDeviceOperationWindow_Loaded(object sender, RoutedEventArgs e) {
            var op = new ObservableDeviceOperation();
            var opView = FindName("OperationView") as FrameworkElement;
            if (opView != null) opView.DataContext = op;
            try {
                var res = await Execute(op);
                if (res) {
                    Close();
                }
            } catch (OperationCanceledException) {
            } catch (Exception) {
                op.CurrentState = "Device is not ready";
            }
        }

        protected FlasherModel Model {
            get { return DataContext as FlasherModel; }
        }

        protected override void OnClosed(EventArgs e) {
            _cts.Cancel();
            base.OnClosed(e);
        }

        protected abstract Task<bool> Execute(DeviceOperation op);

    }
}
