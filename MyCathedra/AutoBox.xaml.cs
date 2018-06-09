using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MyCathedra.DataManager;

namespace MyCathedra
{
    /// <summary>
    /// Логика взаимодействия для AutoBox.xaml
    /// </summary>
    public partial class AutoBox : Window
    {
        private readonly DbManager _dbManager;
        private readonly PasswordService _passwordService;

        public Guid UserId { get; private set; }

        public AutoBox(DbManager dbManager, PasswordService passwordService)
        {
            InitializeComponent();
            _dbManager = dbManager;
            _passwordService = passwordService;
        }


        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var login = LoginTB.Text;
                var password = PasswordTB.Text;
                var user = _dbManager.GetUserByLogin(login);
                if (user == null) throw new ArgumentException("Пользователь с таким логином и паролем не существует.");
                var hash = _passwordService.MakeHash(user.Id, password);
                if (hash != user.Password)
                    throw new ArgumentException("Пользователь с таким логином и паролем не существует.");
                UserId = user.Id;
                DialogResult = true;
                Close();
            }
            catch (Exception exception)
            {
                ExceptionMessege.Content = exception.Message;
            }
        }
    }
}