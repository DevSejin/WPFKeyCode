using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using WinForms = System.Windows.Forms;

namespace WPFKeyCode
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LB1.Content = "empty";
            
        }

        public System.Windows.Forms.NotifyIcon notify;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.ContextMenu menu = new System.Windows.Forms.ContextMenu();

                System.Windows.Forms.MenuItem item1 = new System.Windows.Forms.MenuItem();
                menu.MenuItems.Add(item1);
                item1.Index = 0;
                item1.Text = "종료";
                item1.Click += delegate (object click, EventArgs eClick)
                {
                    App.Current.Shutdown();
                };

                notify = new System.Windows.Forms.NotifyIcon();

                notify.Visible = true;
                notify.DoubleClick += delegate (object click, EventArgs eClick)
                {
                    this.Show();
                };
                notify.ContextMenu = menu;
                notify.Text = "test";
                notify.Icon = Properties.Resources.Icon1;

            }
            catch (Exception ex)
            {

                throw;
            }

            //hooking
            try
            {
                Hook.SetHook();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            //base.OnClosing(e);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
           
        }

        private void BT1_Click(object sender, RoutedEventArgs e)
        {
        }


    }
}
