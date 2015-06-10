using System.Threading.Tasks;

namespace Atmega.Flasher.Views.Operations {
    /// <summary>
    /// Interaction logic for ReadFusesWindow.xaml
    /// </summary>
    public partial class ReadFusesWindow : BaseDeviceOperationWindow {

        public ReadFusesWindow() {
            InitializeComponent();
        }

        protected override Task<bool> Execute(DeviceOperation op) {
            return Model.ReadFusesAsync(op);
        }
    }
}
