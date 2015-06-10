using System.Threading.Tasks;

namespace Atmega.Flasher.Views.Operations {
    /// <summary>
    /// Interaction logic for WriteLocksWindow.xaml
    /// </summary>
    public partial class WriteLocksWindow : BaseDeviceOperationWindow {

        public WriteLocksWindow() {
            InitializeComponent();
        }

        protected override Task<bool> Execute(DeviceOperation op) {
            return Model.WriteLocksAsync(op, _cts.Token);
        }
    }
}
