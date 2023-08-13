using AppSistemaVentas.Models;
using AppSistemaVentas.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AppSistemaVentas.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Usuarios : Page
    {
        private UserViewModel users;
        public Usuarios()
        {
            InitializeComponent();
            users = new UserViewModel(Paginas, RefreshContainer);
            DataContext = users;
        }


        // si no sirve lo de darle click a los usuarios y que se muestren para modificarlos es simplemente volver a
        // poner el siguiente código
        private void ListViewUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserModel selected = (UserModel)ListViewUsers.SelectedItem;
            users.SelectedUser = selected;
            App.mContentFrame.Navigate(typeof(AddUser));
        }
        private void AlignmentMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            users.AlignmentMenuFlyoutItem(sender);
        }

        private void RefreshContainer_RefreshRequested(RefreshContainer sender, RefreshRequestedEventArgs args)
        {
            _ = users.GetUserAsync();
        }
    }
}
