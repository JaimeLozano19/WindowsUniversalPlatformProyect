using AppSistemaVentas.Library;
using AppSistemaVentas.Models;
using AppSistemaVentas.Views;
using Connection;
using LinqToDB;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace AppSistemaVentas.ViewModels
{
    public class UserViewModel : UserModel
    {

        private Connections _conn;
        private BitmapImage _bitmapImage;
        private UploadImage _uploadimage;
        private TextBox _textBoxNid, _textBoxName, _textBoxLastName;
        private TextBox _textBoxTelephone, _textBoxUser, _textBoxEmail;
        private TextBox _textBoxPass;
        private Paginador<TUsers> _paginador;
        private string fileName, filePath;
        private byte[] avatar;
        private int _num_pagina = 1;
        private TextBlock _paginas;
        private RefreshContainer _refreshContainer;
        public UserViewModel(TextBlock paginas, RefreshContainer refreshContainer)
        {
            _paginas = paginas;
            _bitmapImage = new BitmapImage();
            _uploadimage = new UploadImage();
            _conn = new Connections();
            Reg_por_pagina = 2;
            _refreshContainer = refreshContainer;
            _paginador = new Paginador<TUsers>(_conn.TUsers.ToList(), Reg_por_pagina, paginas);
            ResetUsers();

        }
        public UserViewModel(object[] campos) {
            UserTitle = "Registar usuarios";
            _textBoxNid = (TextBox)campos[0];
            _textBoxName = (TextBox)campos[1];
            _textBoxLastName = (TextBox)campos[2];
            _textBoxTelephone = (TextBox)campos[3];
            _textBoxEmail = (TextBox)campos[4];
            _textBoxPass = (TextBox)campos[5];
            _textBoxUser = (TextBox)campos[6];
            _conn = new Connections();
            _uploadimage = new UploadImage();
            _bitmapImage = new BitmapImage();
            if (SelectedUser == null)
            {
                ResetUsers();
            }
            else
            {
                editUser();
            }


        }
        public ICommand AddCommand
        {
            get {
                return new CommandHandler(() => App.mContentFrame.Navigate(typeof(AddUser)));

            }
        }
        public ICommand AddUser
        {
            get { return new CommandHandler(async () => await RegisterUserAsync()); }

        }
        public ICommand cargarImage
        {
            get
            {
                return new CommandHandler(async () =>
                {
                    object[] objects = await _uploadimage.cargarImageAsync();
                    avatar = (byte[])objects[0];
                    if (avatar != null)
                    {
                        _bitmapImage = (BitmapImage)objects[1];
                        Image = _bitmapImage;
                    }
                    else
                    {
                        if (0 == _bitmapImage.PixelHeight)
                        {
                            _bitmapImage.UriSource = new Uri("ms-appx:///Assets/StoreLogo.scale-400.png");
                            avatar = await _uploadimage.ImagebyteAsync(_bitmapImage);
                        }
                    }

                });
            }
        }
        public ICommand Back
        {
            get
            {
                return new CommandHandler(() => App.mContentFrame.Navigate(typeof(Usuarios)));
            }
        }
        public ICommand CancelUser
        {
            get
            {
                return new CommandHandler(() => ResetUsers());
            }
        }
        private async Task RegisterUserAsync()
        {
            if (Nid == null || Nid.Equals(""))
            {
                UserTitle = "Ingresar el nid";
                _textBoxNid.Focus(FocusState.Programmatic);

            }
            else
            {
                if (Name == null || Name.Equals(""))
                {
                    UserTitle = " Ingrese el nombre";
                    _textBoxName.Focus(FocusState.Programmatic);
                }
                else
                {
                    if (LastName == null || LastName.Equals(""))
                    {
                        UserTitle = " Ingrese el apellido";
                        _textBoxLastName.Focus(FocusState.Programmatic);
                    }
                    else
                    {
                        if (Telephone == null || Telephone.Equals(""))
                        {
                            UserTitle = " Ingrese el apellido";
                            _textBoxTelephone.Focus(FocusState.Programmatic);
                        }
                        else
                        {
                            if (Email == null || Email.Equals(""))
                            {
                                UserTitle = " Ingrese el apellido";
                                _textBoxEmail.Focus(FocusState.Programmatic);
                            }
                            else
                            {
                                if (TextBoxEvent.IsValidEmail(Email))
                                {
                                    if (Password == null || Password.Equals(""))
                                    {
                                        UserTitle = "Ingrese el password";
                                        _textBoxPass.Focus(FocusState.Programmatic);
                                    }
                                    else
                                    {
                                        if (User == null || User.Equals(""))
                                        {
                                            UserTitle = "Ingrese el usuario";
                                            _textBoxUser.Focus(FocusState.Programmatic);
                                        }
                                        else
                                        {
                                            if (SelectedRole == null || SelectedRole.Equals(""))
                                            {
                                                UserTitle = "Seleccione un rol";

                                            }
                                            else
                                            {
                                                SaveDataAsync();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    UserTitle = "El email no es válido";
                                    _textBoxEmail.Focus(FocusState.Programmatic);
                                }
                            }
                        }
                    }
                }
            }

        }
        private async Task SaveDataAsync()
        {
            if (avatar == null && ImageByte == null)
            {
                _bitmapImage.UriSource = new Uri("ms-appx:///Assets/StoreLogo.scale-400.png");
                avatar = await _uploadimage.ImagebyteAsync(_bitmapImage);
            }
            var user = _conn.TUsers.Where(u => u.Email.Equals(Email)).ToList();
            if (0 < user.Count)
            {
                if (SelectedUser == null)
                {
                    UserTitle = "El email ya está registrado";
                    _textBoxEmail.Focus(FocusState.Programmatic);
                }
                else
                {
                    if (user[0].ID.Equals(SelectedUser.ID))
                    {
                        update();
                    }
                    else
                    {
                        UserTitle = "El email ya está registrado";
                        _textBoxEmail.Focus(FocusState.Programmatic);
                    }
                }


            }
            else
            {
                if (SelectedUser == null)
                {
                    insert();

                }
                else
                {
                    update();
                }
            }
            void insert()
            {
                _conn.TUsers.Value(u => u.NID, Nid)
                             .Value(u => u.Name, Name)
                             .Value(u => u.LastName, LastName)
                             .Value(u => u.Telephone, Telephone)
                             .Value(u => u.Email, Email)
                             .Value(u => u.Password, Password)
                             .Value(u => u.Users, User)
                             .Value(u => u.Role, SelectedRole)
                             .Value(u => u.Date, DateTime.Now.ToString("dd//MMM/yyy"))
                             .Value(u => u.Images, avatar)
                             .Insert();
                App.mContentFrame.Navigate(typeof(Usuarios));
            }
            void update()
            {
                if (avatar == null)
                {
                    avatar = ImageByte;
                }
                _conn.TUsers.Where(u => u.ID.Equals(SelectedUser.ID))
                    .Set(u => u.NID, Nid)
                    .Set(u => u.Name, Name)
                    .Set(u => u.LastName, LastName)
                    .Set(u => u.Telephone, Telephone)
                    .Set(u => u.Email, Email)
                    .Set(u => u.Users, User)
                    .Set(u => u.Role, SelectedRole)
                    .Set(u => u.Images, avatar)
                    .Update();
                App.mContentFrame.Navigate(typeof(Usuarios));
            }
        }
        public string Search
        {
            get { return GetValue(() => Search); }
            set
            {
                SetValue(() => Search, value);
                _ = GetUserAsync();

            }
        }
        public ICommand Primero
        {
            get
            {
                return new CommandHandler(() =>
                {
                    _num_pagina = _paginador.primero();
                    _ = GetUserAsync();


                });
            }
        }
        public ICommand Anterior
        {
            get
            {
                return new CommandHandler(() =>
                { _num_pagina = _paginador.anterior();
                    _ = GetUserAsync();

                });
            }
        }

        public ICommand Siguiente
        {
            get {
                return new CommandHandler(() =>
                {
                    _num_pagina = _paginador.siguiente();
                    _ = GetUserAsync();
                });
            }
        }
        public ICommand Ultimo
        {
            get {
                return new CommandHandler(() =>
                {
                    _num_pagina = _paginador.ultimo();
                    _ = GetUserAsync();
                });
            }
        }
        public async Task GetUserAsync()
        {
            List<TUsers> list;
            var ListUser = new List<UserModel>();
            int inicio = (_num_pagina - 1) * Reg_por_pagina;
            if (Search == null || Search.Equals(""))
            {
                list = _conn.TUsers.Skip(inicio).Take(Reg_por_pagina).ToList();
            }
            else
            {
                list = _conn.TUsers.Where(u => u.LastName.StartsWith(Search) || u.Name.StartsWith
                (Search)).Skip(inicio).Take(Reg_por_pagina).ToList();
            }


            if (0 < list.Count)
            {
                foreach (var item in list)
                {
                    var image = await _uploadimage.ImageFromBufferAsync(item.Images);
                    ListUser.Add(new UserModel
                    {
                        ID = item.ID,
                        Nid = item.NID,
                        LastName = item.LastName,
                        Name = item.Name,
                        Telephone = item.Telephone,
                        Email = item.Email,
                        User = item.Users,
                        SelectedRole = item.Role,
                        Image = image,
                        ImageByte = item.Images,
                        IsCheck = false

                    });
                }
                ListUsers = ListUser;

            }
        }
        private void editUser()
        {
            Nid = SelectedUser.Nid;
            Name = SelectedUser.Name;
            LastName = SelectedUser.LastName;
            Telephone = SelectedUser.Telephone;
            Email = SelectedUser.Email;
            User = SelectedUser.User;
            Image = SelectedUser.Image;
            Password = "********************";
            ImageByte = SelectedUser.ImageByte;

            _textBoxPass.IsReadOnly = true;

        }

        public void AlignmentMenuFlyoutItem(object sender)
        {
            var option = ((MenuFlyoutItem)sender).Tag.ToString();

            Reg_por_pagina = Convert.ToInt32(option);
            _ = GetUserAsync();
            _paginador = new Paginador<TUsers>(_conn.TUsers.ToList(), Reg_por_pagina, _paginas);

        }
        public ICommand CheckBox
        {
            get
            {
                return new CommandHandler(() => {
                    if (IsCheckedAll)
                    {
                        foreach (var item in ListUsers)
                        {
                            item.IsCheck = true;
                        }
                    }
                    else
                    {
                        foreach (var item in ListUsers)
                        {
                            item.IsCheck = false;
                        }
                    }
                });
            }
        }
        public ICommand Delete
        {
            get 
            {
                return new CommandHandler(() => deleteAsync());
            }
        }
        private async Task deleteAsync()
        {
            int count = 0;
            foreach (var item in ListUsers)
            {
                if (item.IsCheck)
                {
                    count++;
                }
            }
            if (IsCheckedAll || 0 < count )
            {
                ContentDialog deleteFileDialog = new ContentDialog
                {
                    Title = "Eliminar Registro",
                    Content = "Esta seguro de eliminar los " + count + " registros ?",
                    PrimaryButtonText = "Delete",
                    CloseButtonText = "Cancel"
                };
                ContentDialogResult result = await deleteFileDialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    foreach (var item in ListUsers)
                    {
                        _conn.TUsers.Where(u => u.ID.Equals(item.ID)).Delete();
                        //   _conn.TUsers.Where(u => u.ID.Equals(item.ID)).Delete();
                        
                    }
                    _ = GetUserAsync();
                }
               
            }
        }

        public ICommand Refresh
        {
            get
            {
                return new CommandHandler(() => refreshContainer());
            }
        }
        private void refreshContainer()
        {
            _refreshContainer.RequestRefresh();
        }
        private void ResetUsers()
        {
            _ = GetUserAsync();
            _bitmapImage.UriSource = new Uri("ms-appx:///Assets/StoreLogo.scale-400.png");
            Image = _bitmapImage;
            _bitmapImage = new BitmapImage();
            SelectedUser = null;
            Nid = "";
            Name = "";
            LastName = "";
            Telephone = "";
            Email = "";
            User = "";

            if (SelectedUser == null)
                Password = "";
            ImageByte = null;
        }
    }
}
