using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace testbd
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email AND Password = @Password";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);

                        int userExists = (int)command.ExecuteScalar();

                        if (userExists > 0)
                        {
                            MessageBox.Show("Вы успешно вошли!");
                        }
                        else
                        {
                            MessageBox.Show("Неверный логин или пароль.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }

        private void OpenRoleSpecificWindow(int roleId)
        {
            // Открываем окно в зависимости от роли
            if (roleId == 1)
            {
                MessageBox.Show("Открыто окно администратора.");
            }
            else if (roleId == 2)
            {
                MessageBox.Show("Открыто окно пользователя.");
            }
            else if (roleId == 3)
            {
                MessageBox.Show("Открыто окно оператора.");
            }
        }
    }
}
