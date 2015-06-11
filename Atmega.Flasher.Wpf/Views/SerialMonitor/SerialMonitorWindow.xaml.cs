using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace Atmega.Flasher.Views.SerialMonitor {
    /// <summary>
    /// Interaction logic for SerialMonitorWindow.xaml
    /// </summary>
    public partial class SerialMonitorWindow : Window {
        public SerialMonitorWindow() {
            InitializeComponent();
        }

        private void MessageToSend_OnKeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                SendMessage(MessageToSend.Text);
                MessageToSend.Text = "";
            }
        }

        private void SendMessage(string content) {
            var para = new Paragraph(new Run(content)) { Style = (Style)Resources["MyMessage"] };
            MessageLog.Document.Blocks.Add(para);
            MessageLog.ScrollToEnd();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            SendMessage(MessageToSend.Text);
            MessageToSend.Text = "";
        }

    }
}
