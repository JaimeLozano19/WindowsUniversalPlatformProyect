using AppSistemaVentas.Library;
using AppSistemaVentas.Models;
using Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AppSistemaVentas.ViewModels
{
    class LoginViewModel : UserModel
    {
        private ICommand _command;
        private TextBox _textBoxEmail;
        private PasswordBox _textBoxPass;
        private ProgressRing _ProgressRing;
        private String date = DateTime.Now.ToString("dd/MM/yyy");
        private Frame rootFrame = Window.Current.Content as Frame;
        private Connections _conn;
        private SQLiteConnections _sqlite;
        public LoginViewModel(Object[] campos)
        {

           _textBoxEmail = (TextBox)campos[0];
           _textBoxPass = (PasswordBox)campos[1];
            _ProgressRing = (ProgressRing)campos[2];
            _conn = new Connections();
           _sqlite = new SQLiteConnections();

     //      var pass =  Encrypt.EncryptData("123","monica12por@gmail.com");
        }
        public ICommand IniciarCommand
        {

            get
            {
                return _command ?? (_command = new CommandHandler(async () =>
                {
                    await iniciarAsync();
                }));
            }
        }
        private async Task iniciarAsync()
        {
            if (Email == null || Email.Equals(""))
            {
                EmailMessage = "Ingrese el email";
                _textBoxEmail.Focus(FocusState.Programmatic);
            }
            else
            {
                if (TextBoxEvent.IsValidEmail(Email))
                {
                    if (Password == null || Password.Equals(""))
                    {
                        PasswordMessage = "Ingrese el password";
                        _textBoxPass.Focus(FocusState.Programmatic);
                    }
                    else
                    {
                        try
                        {
                            _ProgressRing.IsActive = true;
                            var user = _conn.TUsers.Where(p => p.Email.Equals(Email) && p.Password.Equals
                            (Password)).ToList();
                            if (0 < user.Count)
                            {
                                var dataUser = user.ElementAt(0);
                                dataUser.Date = DateTime.Now.ToString("dd/MMM/yyy");
                                _sqlite.Connection.Insert(dataUser);
                                rootFrame.Navigate(typeof(MainPage));
                            }
                            else
                            {
                                _ProgressRing.IsActive = false;
                                Message = "Contraseña o email incorrectos";
                            }
                        }
                        catch (Exception ex)
                        {
                            _ProgressRing.IsActive = false;
                            Message = ex.Message;
                        }

                    }
                }
                else
                {
                    EmailMessage = "El email no es válido";
                    _textBoxEmail.Focus(FocusState.Programmatic);
                }
            }
        }
    }
}
