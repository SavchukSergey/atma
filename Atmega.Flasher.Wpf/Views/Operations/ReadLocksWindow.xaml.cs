using System.Threading.Tasks;

namespace Atmega.Flasher.Views.Operations {
    /// <summary>
    /// Interaction logic for ReadLocksWindow.xaml
    /// </summary>
    public partial class ReadLocksWindow : BaseDeviceOperationWindow {

        public ReadLocksWindow() {
            InitializeComponent();
        }

        protected override Task<bool> Execute(DeviceOperation op) {
            return Model.ReadLocksAsync(op, _cts.Token);
        }
    }
}
