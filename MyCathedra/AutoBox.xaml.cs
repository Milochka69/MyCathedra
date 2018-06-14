using System;
using System.Windows;
using MyCathedra.DataManager;

namespace MyCathedra
{
    /// <summary>
    /// Логика взаимодействия для AutoBox.xaml
    /// </summary>
    public partial class AutoBox 
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
                var login = LoginTb.Text;
                var password = PasswordTb.Text;
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