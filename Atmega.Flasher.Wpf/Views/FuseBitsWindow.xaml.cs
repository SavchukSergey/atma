using System.Linq;
using System.Windows;
using System.Windows.Input;
using Atmega.Flasher.Devices;
using Atmega.Flasher.Models;
using Atmega.Flasher.Views.Operations;

namespace Atmega.Flasher.Views {
    /// <summary>
    /// Interaction logic for FuseBitsWindow.xaml
    /// </summary>
    public partial class FuseBitsWindow : Window {
        public FuseBitsWindow() {
            InitializeComponent();
        }

        private void CloseCommand(object sender, ExecutedRoutedEventArgs e) {
            Close();
        }

        private void SaveCommand(object sender, ExecutedRoutedEventArgs e) {
            var deviceBits = (DeviceBits)DeviceBitsView.DataContext;
            var data = deviceBits.ToBytes();

            Model.FusesHexBoard.Clear();
            foreach (var item in data) {
                Model.FusesHexBoard[item.Address] = item.Value;
            }

            var dlg = new WriteFusesWindow {
                DataContext = new FlasherOperationModel(Model),
                Owner = this
            };
            dlg.ShowDialog();

            Close();
        }

        private void FuseBitsWindow_OnLoaded(object sender, RoutedEventArgs e) {
            var dlg = new ReadFusesWindow {
                DataContext = new FlasherOperationModel(Model),
                Owner = this
            };
            dlg.ShowDialog();

            var settings = FlasherConfig.Read();
            var fuseBits = settings.Device.FuseBits;

            var fusesData = Model.FusesHexBoard.ToArray();
            fuseBits.ApplyFrom(fusesData);

            DeviceBitsView.DataContext = fuseBits;
        }

        protected FlasherModel Model {
            get { return ((FlasherModel)DataContext); }
        }
    }
}
