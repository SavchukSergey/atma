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
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            ResizeMode = ResizeMode.NoResize;
        }

        private async void BaseDeviceOperationWindow_Loaded(object sender, RoutedEventArgs e) {
            var op = DeviceOperation;
            try {
                var res = await Execute(op);
                if (res) {
                    Close();
                }
            } catch (OperationCanceledException) {
                op.CurrentState = "Operation is cancelled";
                op.Status = DeviceOperationStatus.Error;
            } catch (Exception) {
                op.CurrentState = "Device is not ready";
                op.Status = DeviceOperationStatus.Error;
            }
            if (op.Status == DeviceOperationStatus.Error) {
                op.FlashSize = Math.Max(1, op.FlashSize);
                op.Complete();
            }
        }

        protected DeviceOperation DeviceOperation {
            get { return ((FlasherOperationModel)DataContext).DeviceOperation; }
        }


        protected FlasherModel Model {
            get { return ((FlasherOperationModel)DataContext).Flasher; }
        }

        protected override void OnClosed(EventArgs e) {
            _cts.Cancel();
            base.OnClosed(e);
        }

        protected abstract Task<bool> Execute(DeviceOperation op);

    }
}
