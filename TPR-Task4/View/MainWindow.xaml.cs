using System;
using System.Windows;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            MainViewModel mainViewModel = new MainViewModel(false);
            this.DataContext = mainViewModel;
            InitializeComponent();
        }
    }
}