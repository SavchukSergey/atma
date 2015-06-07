﻿using System.Windows;
using System.Windows.Input;
using Atmega.Flasher.Models;

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
            var settings = FlasherConfig.Read();
            var lockBits = settings.Device.LockBits;
            DeviceBitsView.DataContext = lockBits;
        }

        protected FlasherModel Model {
            get { return ((FlasherModel)DataContext); }
        }
    }
}
