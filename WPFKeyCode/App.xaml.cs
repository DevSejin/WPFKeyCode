using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPFKeyCode
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Hook.UnHook();
            System.IO.File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VolumeWheelEx.log", "[" + DateTime.Now.ToString() + "]\n" + e.ToString());
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Dispatcher.UnhandledException += Dispatcher_UnhandledException;
            try
            {
                
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VolumeWheelEx.log", "[" + DateTime.Now.ToString() + "]\n" + "catch : "+ex.ToString());
                throw;
            }
        }
        private void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Hook.UnHook();
            System.IO.File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VolumeWheelEx.log", "[" + DateTime.Now.ToString() + "]\n" + e.ToString());
        }
    }
}
