using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace AppSistemaVentas.Models
{
    public class UserModel : PropertyChangedNotification
    {
        public int ID { get; set; }
        public string Email
        {
            get { return GetValue(() => Email); }
            set
            {
                SetValue(() => Email, value);
                EmailMessage = "";
                Message = "";
                UserTitle = "Registrar usuarios";
            }

        }
        public string Password
        {
            get { return GetValue(() => Password); }
            set
            {
                SetValue(() => Password, value);
                PasswordMessage = "";
                Message = "";
                UserTitle = "Registrar usuarios";
            }
        }

        public string Nid
        {
            get { return GetValue(() => Nid); }
            set
            {
                SetValue(() => Nid, value);
              
                Message = "";
                UserTitle = "Registrar usuarios";
            }
        }
        public string Name
        {
            get { return GetValue(() => Name); }
            set
            {
                SetValue(() => Name, value);

                Message = "";
                UserTitle = "Registrar usuarios";
            }
        }
        public string LastName
        {
            get { return GetValue(() => LastName); }
            set
            {
                SetValue(() => LastName, value);

                Message = "";
                UserTitle = "Registrar usuarios";
            }
        }
        public string User
        {
            get { return GetValue(() => User); }
            set
            {
                SetValue(() => User, value);

                Message = "";
                UserTitle = "Registrar usuarios";
            }
        }
        

        public string Telephone
        {
            get { return GetValue(() => Telephone); }
            set
            {
                SetValue(() => Telephone, value);

                Message = "";
                UserTitle = "Registrar usuarios";
            }
        }
        public List<String> ListRoles
        {
            get
            {
                return new List<String> {
            "Admin",
            "User"
              };
            }
        }
        public string SelectedRole
        {
            get{ return GetValue(() => SelectedRole); }
            set { SetValue(() => SelectedRole, value); UserTitle = "Registrar usuarios"; }
        }

        public string UserTitle
        {

            get { return GetValue(() => UserTitle); }
            set
            {
                if (UserTitle == null || UserTitle.Equals(""))
                {
                    SetValue(() => UserTitle, "Registar usuarios");

                }
                else 
                {
                    SetValue(() => UserTitle, value);
                }
               
            }
        }
        public BitmapImage Image
        {
            get { return GetValue(() => Image);  }
            set { SetValue(() => Image, value); }
        }
     

        public string Message
        {
            get { return GetValue(() => Message); }
            set => SetValue(() => Message, value);

        }
        public string EmailMessage
        {
            get { return GetValue(() => EmailMessage); }
            set => SetValue(() => EmailMessage, value);

        }
        public string PasswordMessage
        {
            get { return GetValue(() => PasswordMessage); }
            set => SetValue(() => PasswordMessage, value);
        }
        public List<UserModel> ListUsers 
        {
            get { return GetValue(() => ListUsers); }
            set { SetValue(() => ListUsers, value);}
        }
        public byte[] ImageByte 
        {
            get { return GetValue(() => ImageByte); }
            set { SetValue(() => ImageByte, value); }
        }
        private static UserModel selectedUser;
        public UserModel SelectedUser
        {
            get { return selectedUser; }
            set { selectedUser = value; }
        }
        public int Reg_por_pagina
        {
            get { return GetValue(() => Reg_por_pagina); }
            set { SetValue(() => Reg_por_pagina, value); }
        }
        public bool IsCheck
        {
            get { return GetValue(() => IsCheck); }
            set { SetValue(() => IsCheck, value); }
        }
        public bool IsCheckedAll
        {
            get { return GetValue(() => IsCheckedAll); }
            set { SetValue(() => IsCheckedAll, value); }
        }
    }

}