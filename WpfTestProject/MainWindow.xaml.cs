using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using HW.JD.CableReport.Model;
using Microsoft.Practices.Prism.Commands;
using wpfone;
using WpfTestProject.CableReport.Window;

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

        public ObservableCollection<string> Items { get; set; } = new ObservableCollection<string>() { "123", "456", "789" };

        private void dowork()
        {
            MessageBox.Show("是不是这个IDE运行速度更快了");
        }
        
        private async void DiyListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int curIndex = 0;
            if (Items.Count > 0)
            {
                try
                {
                    DiyListViewItem item = sender as DiyListViewItem;
                    await Task.Delay(500);
                    int index = Items.ToList().FindIndex(x => x.Equals(item.button.Content));
                    curIndex = index;
                    if(index != -1) Items.RemoveAt(index);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
        }

        private void AddRowBtn_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var item in ListViewTest.Items)
            {
                
            }
            
            Items.Add($"{DateTime.Now.Hour}-{DateTime.Now.Minute}--{DateTime.Now.Second}--{DateTime.Now.Millisecond}");
        }

        private void DelRowBtn_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}