using System.Linq;
using System.Windows;
using System.Windows.Input;
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

            var locksData = Model.LocksHexBoard.SplitBlocks(int.MaxValue).Blocks.First().Data;
            lockBits.ApplyFrom(locksData);

            DeviceBitsView.DataContext = lockBits;
        }

        protected FlasherModel Model {
            get { return ((FlasherModel)DataContext); }
        }
    }
}
