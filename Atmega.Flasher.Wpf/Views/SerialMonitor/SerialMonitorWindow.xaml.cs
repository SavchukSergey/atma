using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using Atmega.Flasher.IO;
using Atmega.Flasher.Models;

namespace Atmega.Flasher.Views.SerialMonitor {
    /// <summary>
    /// Interaction logic for SerialMonitorWindow.xaml
    /// </summary>
    public partial class SerialMonitorWindow : Window {

        private IAvrChannel _channel;
        private readonly CancellationTokenSource _source = new CancellationTokenSource();

        private Run _receivedRun;

        private DateTime _receivedDateTime;
        private readonly TimeSpan _threshold = TimeSpan.FromMilliseconds(500);
        private Style _myStyle;
        private Style _otherStyle;

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
            var para = new Paragraph(new Run(content)) { Style = _myStyle };
            MessageLog.Document.Blocks.Add(para);
            foreach (var ch in content) {
                _channel.SendByte((byte)ch);
            }
            FlushReceived();
        }

        private void FlushReceived() {
            if (_receivedRun != null && _receivedRun.Text.Length > 0) {
                MessageLog.ScrollToEnd();
                _receivedRun = null;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            SendMessage(MessageToSend.Text);
            MessageToSend.Text = "";
        }

        private async void SerialMonitorWindow_OnLoaded(object sender, RoutedEventArgs e) {
            _channel = FlasherConfig.Read().GetProgrammerConfig().CreateChannel();
            _channel.Open();
            _myStyle = (Style)Resources["MyMessage"];
            _otherStyle = (Style)Resources["OtherMessage"];
            var disp = Dispatcher;
            await Task.Run(() => Listen(disp));
        }

        private void Listen(Dispatcher disp) {
            while (!_source.IsCancellationRequested) {
                byte? bt;
                try {
                    bt = _channel.ReceiveByte();
                } catch (Exception) {
                    bt = null;
                }

                var ts = DateTime.UtcNow;
                var delta = ts - _receivedDateTime;
                _receivedDateTime = ts;

                disp.Invoke(() => {
                    if (delta > _threshold) {
                        FlushReceived();
                    }

                    if (bt.HasValue) {
                        if (_receivedRun == null) {
                            _receivedRun = new Run();
                            var para = new Paragraph(_receivedRun) { Style = _otherStyle };
                            MessageLog.Document.Blocks.Add(para);
                        }
                        _receivedRun.Text += (char)bt;
                    }
                });
            }
        }

        protected override void OnClosed(EventArgs e) {
            _channel.Dispose();
            _source.Cancel();
            base.OnClosed(e);
        }

        private void ClearLog(object sender, RoutedEventArgs e) {
            MessageLog.Document.Blocks.Clear();
        }

    }
}
