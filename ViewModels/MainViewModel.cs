using AppSistemaVentas.Views;
using Connection;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AppSistemaVentas.ViewModels
{
    public class MainViewModel
    {
        private SQLiteConnections _sqlite;
        public MainViewModel()
        {
            _sqlite = new SQLiteConnections();
        }
        public void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs
            args, Frame contentFrame)
        {
            NavigationViewItem item = args.SelectedItem as NavigationViewItem;
            switch (item.Tag)
            {
                case "Close":
                    _sqlite.Connection.DeleteAll<TUsers>();
                    Frame rootFrame = Window.Current.Content as Frame;
                    rootFrame.Navigate(typeof(Login));
                    break;

                case "User":
                    contentFrame.Navigate(typeof(Usuarios));
                    break;

            }
        }
    }
}
