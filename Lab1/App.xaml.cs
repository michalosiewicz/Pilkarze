using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Lab1
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
       
        public static void ChangeLanguage(string version)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(version);
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(version);

            var oldWindow = Current.MainWindow;

            Current.MainWindow = new MainWindow();
            Current.MainWindow.Top = oldWindow.Top;
            Current.MainWindow.Left = oldWindow.Left;
            Current.MainWindow.Show();
            oldWindow.Close();
        }
    }
}
