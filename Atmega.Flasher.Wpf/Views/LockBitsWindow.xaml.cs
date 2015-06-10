using System.Windows;
using System.Windows.Input;
using Atmega.Flasher.Devices;
using Atmega.Flasher.Models;
using Atmega.Flasher.Views.Operations;

namespace Atmega.Flasher.Views {
    /// <summary>
    /// Interaction logic for LockBitsWindow.xaml
    /// </summary>
    public partial class LockBitsWindow : Window {
        public LockBitsWindow() {
            InitializeComponent();
        }

        private void CloseCommand(object sender, ExecutedRoutedEventArgs e) {
            Close();
        }

        private void SaveCommand(object sender, ExecutedRoutedEventArgs e) {
            var deviceBits = (DeviceBits)DeviceBitsView.DataContext;
            var data = deviceBits.ToBytes();

            Model.LocksHexBoard.Clear();
            foreach (var item in data) {
                Model.LocksHexBoard[item.Address] = item.Value;
            }

            var dlg = new WriteLocksWindow {
                DataContext = new FlasherOperationModel(Model),
                Owner = this
            };
            dlg.ShowDialog();

            Close(); 
        }

        private void LockBitsWindow_OnLoaded(object sender, RoutedEventArgs e) {
            var dlg = new ReadLocksWindow {
                DataContext = new FlasherOperationModel(Model),
                Owner = this
            };
            dlg.ShowDialog();

            var settings = FlasherConfig.Read();
            var lockBits = settings.Device.LockBits;

            var locksData = Model.LocksHexBoard;
            lockBits.ApplyFrom(locksData);

            DeviceBitsView.DataContext = lockBits;
        }

        protected FlasherModel Model {
            get { return ((FlasherModel)DataContext); }
        }
    }
}
