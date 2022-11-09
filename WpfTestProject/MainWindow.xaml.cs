using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace WpfTestProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public DelegateCommand DoWork { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();

            DoWork = new DelegateCommand(dowork);
            
            this.DataContext = this;
        }

        private void dowork()
        {
            MessageBox.Show("是不是这个IDE运行速度更快了");
        }
    }
}