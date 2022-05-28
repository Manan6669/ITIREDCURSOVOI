using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Kursovoi
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        private Regex passwordRegex = new Regex(@"[a-zA-Z][a-zA-Z0-9]{3,20}$");
        private Regex userNameRegex = new Regex(@"^[a-zA-Z][a-zA-Z0-9-_\-\D\s.][а-яА-ЯЁёa-zA-Z0-9]{1,20}$");
        public LogIn()
        {
             InitializeComponent();
           
           
        }

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            string loqin = User.Text.Trim();
            string password = Password.Password.Trim();           
            Users authus = null;
            if (loqin != "" || password != "")
            {
                 using (CURSOVOIContext db = new CURSOVOIContext())
                 {
                    authus = db.Users.Where(b => b.UsersLoqin == loqin && b.UsersPassword == password).FirstOrDefault();
                    Application.Current.Resources["AdminEntUser"] = loqin;
                    Application.Current.Resources["AdminPassw"] = password;

                    try
                    {
                        if (authus!=null && authus.UsersLoqin == "Admin" && authus.UsersPassword == "admin")
                        {
                            Application.Current.Resources["EntAdmin"] = loqin;
                            Application.Current.Resources["EntPasswAdmin"] = password;
                            WindowMainAdmin winm = new WindowMainAdmin();
                            winm.Show();
                            var window = Application.Current.Windows[0];
                            if (window != null)
                                window.Close();
                        }
                        else
                        {
                            if(authus!=null && authus.UsersLoqin == loqin && authus.UsersPassword == password)
                            { 
                            var authcode = authus.UnicCodeUsers;
                            Application.Current.Resources["EntUser"] = loqin;
                            Application.Current.Resources["EntPassw"] = password;
                            Application.Current.Resources["CodeUser"] = authcode;
                            WindowMain winm = new WindowMain();
                            winm.Show();
                            var window = Application.Current.Windows[0];
                            if (window != null)
                                window.Close();
                            }
                            else
                            {
                                MessageBox.Show("Такого пользователя не существует!", $"Ошибка");
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Неверный логин или пароль!", $"Ошибка");
                    }
                }
               
            }
            else
            { 
                 if(loqin =="")
                {
                    User.BorderThickness = new Thickness(1, 1, 1, 1);
                    User.BorderBrush = Brushes.Red;
                }
            
                 if(password == "")
                {
                    Password.BorderThickness = new Thickness(1, 1, 1, 1);
                    Password.BorderBrush = Brushes.Red;
                }
                    MessageBox.Show("Вы не ввели логин или пароль!");
            }
        }
        private void TextBox_Error(object sender, ValidationErrorEventArgs e)
        {
            MessageBox.Show(e.Error.ErrorContent.ToString());
        }

        private void PassReg(object sender, RoutedEventArgs args)
        {
            string password = Password.Password.Trim();
            bool PasswMatch = passwordRegex.IsMatch(password);
            if(!PasswMatch)
                {
                    Password.BorderThickness = new Thickness(1, 1, 1, 1);
                    Password.BorderBrush = Brushes.Red;
                    
                    
                }
                else
                {
                    Password.BorderThickness = new Thickness(0, 0, 0, 0);
                }
        }

        private void UsReg(object sender, TextChangedEventArgs args)
        {
            string loqin = User.Text.Trim();
            bool LoqMatch = userNameRegex.IsMatch(loqin);
            if (!LoqMatch)
            {
                User.BorderThickness = new Thickness(1, 1, 1, 1);
                User.BorderBrush = Brushes.Red;
            }
            else
            {
                User.BorderThickness = new Thickness(0, 0, 0, 0);
            }
        }

        private void EnterToProgrammWithRegistr(object sender, RoutedEventArgs e)
        {
            using (CURSOVOIContext db = new CURSOVOIContext())
            {
                int k = 0;
                string loqin = User.Text.Trim();
                string password = Password.Password.Trim();
                int id = db.Users.Max(m => m.UnicCodeUsers);               
                var authus = db.Users.Where(b => b.UsersLoqin == loqin && b.UsersPassword == password).ToList();

                bool PasswMatch = passwordRegex.IsMatch(password);
                bool LoqMatch = userNameRegex.IsMatch(loqin);
               

                    if (loqin == "" || password == "" || !PasswMatch || !LoqMatch)
                    {
                        MessageBox.Show("Вы не заполнили все поля или заполнили их некорректно!");
                    }
                    else
                    {
                        foreach (var i in authus)
                        {
                            if (i.UsersLoqin == loqin && i.UsersPassword == password)
                            {
                                k++;
                            }
                        }
                        if (k == 0)
                        {
                            try
                            {
                                Users newus = new Users
                                {
                                    UsersLoqin = loqin,
                                    UsersPassword = password,
                                    UnicCodeUsers = id + 1
                                };
                                db.Users.Add(newus);
                                db.SaveChanges();
                                Users user = db.Users.FirstOrDefault((u) => u.UsersLoqin == loqin);
                                MessageBox.Show("Войдите, пожалуйста, снова!", $"Добро пожаловать, {user.UsersLoqin}!");
                                LogIn log = new LogIn();
                                log.Show();
                                var window = Application.Current.Windows[0];
                                if (window != null)
                                    window.Close();
                            }
                            catch
                            {
                                MessageBox.Show("Ошибка!", $"Неверный логин или пароль!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Такой пользователь уже существует!");
                            User.Text = "";
                            Password.Password = "";
                        }
                    }
                
            }
        }
    }
}
