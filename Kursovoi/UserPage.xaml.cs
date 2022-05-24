using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using Microsoft.Win32;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.IO;

namespace Kursovoi
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        public int k;
        public UserPage()
        {
            InitializeComponent();
            
            using (CURSOVOIContext db = new CURSOVOIContext())
            {
                var LoqUs = Application.Current.Resources["EntUser"];
                var PasUs = Application.Current.Resources["EntPassw"];
                var CodUs = Application.Current.Resources["CodeUser"];

                var sourc = db.Users.FirstOrDefault(s => s.UsersLoqin == LoqUs.ToString() && s.UsersPassword == PasUs.ToString());
                var Cod = sourc.UnicCodeUsers;
                UserLoqin.Text = LoqUs.ToString();

                var sourcBook = db.Bookmarks.Where(b => b.UnicCodeUsers == Cod).ToList();
                k = sourcBook.Count;
                Button[] btns = new Button[k];
                // var imgtitcode = sourcTitle.Photo;
                foreach (Bookmarks book in sourcBook)
                {
                    var titcode = book.CodeTitle;
                   
                    var sourcTitle = db.Title.FirstOrDefault(t => t.CodeTitle == titcode);
                    var st = sourcTitle.CodeTitle;
                    var picst = sourcTitle.Photo;

                    string imgtitcodepath = Environment.CurrentDirectory + "/PHOTOTITLE/" + $"{picst}";
                    // var bok = db.Bookmarks.ToList();
                    var stakk = new StackPanel { 
                    Name = "Stack" + st,
                    Orientation = Orientation.Vertical,
                      Height = 200,
                        Width = 120,
                        Margin = new Thickness(10, 5, 0, 0)};

                    var btndel = new Button
                    {
                        Name = "DelTit" + st,
                        Background = new ImageBrush { ImageSource = new BitmapImage(new Uri("D:/ЛЕНННННА/4 семестр/Kursovoi/Kursovoi/bin/Debug/net5.0-windows/PHOTOTITLE/Del.png")) },
                        Height = 10,
                        Width = 10,
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Right,
                        Margin = new Thickness(0, 0, 0, 0),
                        BorderThickness = new Thickness(0, 0, 0, 0)
                    };
                    var btnbook = new Button
                    {
                        Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(imgtitcodepath)) },
                        Name = "Title" + st //== st
                        ,
                        VerticalAlignment= VerticalAlignment.Bottom,
                        Height = 164,
                        Width = 120,
                        Margin = new Thickness(0, 5, 0, 0)

                    };
                    btnbook.Click += GoBookmark; 
                  
                    stakk.Children.Add(btndel);
                    stakk.Children.Add(btnbook);
                    btndel.Click += DeleteBookmark;
                    BookmarkCatalog.Children.Add(stakk);

                }

               // string path = Environment.CurrentDirectory + " / PHOTOTITLE/" + $"{sourc.PhotoUsers}";
                if (sourc.PhotoUsers == null)
                {
                    UserImg.Source = new BitmapImage(new Uri("D:/ЛЕНННННА/4 семестр/КУРСОВОЙ ООП/BD/Kursovoi/Kursovoi/bin/Debug/net5.0-windows/PHOTOTITLE/All.jpg"));
                }
                else
                {
                    
                    Binding binding = new Binding();
                    binding.Source = sourc.PhotoUsers;
                    UserImg.SetBinding(Image.SourceProperty, binding);
                    //UserImg.Source = new BitmapImage(new Uri(path));
                }

            }
        }
    /*    public void AddToCat (object sender, RoutedEventArgs e)
        {
            using (CURSOVOIContext db = new CURSOVOIContext())
            {
                var LoqUs = Application.Current.Resources["EntUser"];
                var PasUs = Application.Current.Resources["EntPassw"];
                var CodUs = Application.Current.Resources["CodeUser"];

                var sourc = db.Users.FirstOrDefault(s => s.UsersLoqin == LoqUs.ToString() && s.UsersPassword == PasUs.ToString());
                var Cod = sourc.UnicCodeUsers;
                UserLoqin.Text = LoqUs.ToString();

                var sourcBook = db.Bookmarks.Where(b => b.UnicCodeUsers == Cod).ToList();
                k = sourcBook.Count;
                Button[] btns = new Button[k];
                // var imgtitcode = sourcTitle.Photo;
                foreach (Bookmarks book in sourcBook)
                {
                    var titcode = book.CodeTitle;

                    var sourcTitle = db.Title.FirstOrDefault(t => t.CodeTitle == titcode);
                    var st = sourcTitle.CodeTitle;
                    var picst = sourcTitle.Photo;

                    string imgtitcodepath = Environment.CurrentDirectory + "/PHOTOTITLE/" + $"{picst}";
                    // var bok = db.Bookmarks.ToList();
                    var stakk = new StackPanel
                    {
                        Name = "Stack" + st,
                        Orientation = Orientation.Vertical,
                        Height = 200,
                        Width = 120,
                        Margin = new Thickness(10, 5, 0, 0)
                    };

                    var btndel = new Button
                    {
                        Name = "DelTit" + st,
                        Background = new ImageBrush { ImageSource = new BitmapImage(new Uri("D:/ЛЕНННННА/4 семестр/Kursovoi/Kursovoi/bin/Debug/net5.0-windows/PHOTOTITLE/Del.png")) },
                        Height = 10,
                        Width = 10,
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Right,
                        Margin = new Thickness(0, 0, 0, 0),
                        BorderThickness = new Thickness(0, 0, 0, 0)
                    };
                    var btnbook = new Button
                    {
                        Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(imgtitcodepath)) },
                        Name = "Title" + st //== st
                        ,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        Height = 164,
                        Width = 120,
                        Margin = new Thickness(0, 5, 0, 0)

                    };
                    btnbook.Click += GoBookmark;

                    stakk.Children.Add(btndel);
                    stakk.Children.Add(btnbook);
                    btndel.Click += DeleteBookmark;
                    BookmarkCatalog.Children.Add(stakk);

                }

                // string path = Environment.CurrentDirectory + " / PHOTOTITLE/" + $"{sourc.PhotoUsers}";
                if (sourc.PhotoUsers == null)
                {
                    UserImg.Source = new BitmapImage(new Uri("D:/ЛЕНННННА/4 семестр/КУРСОВОЙ ООП/BD/Kursovoi/Kursovoi/bin/Debug/net5.0-windows/PHOTOTITLE/All.jpg"));
                }
                else
                {

                    Binding binding = new Binding();
                    binding.Source = sourc.PhotoUsers;
                    UserImg.SetBinding(Image.SourceProperty, binding);
                    //UserImg.Source = new BitmapImage(new Uri(path));
                }

            }
        }*/
        public void DeleteBookmark(object sender, RoutedEventArgs e)
        {
            var LoqUs = Application.Current.Resources["EntUser"];
            var PasUs = Application.Current.Resources["EntPassw"];
            var CodUs = Application.Current.Resources["CodeUser"];
            var buttonNameBookmarkDel = (sender as Button).Name;
            string shortcodeBook = buttonNameBookmarkDel.ToString();
            shortcodeBook = shortcodeBook.Remove(0, 6);

            //if (e.Button == MouseButtons.Right)
            {
                using (CURSOVOIContext db = new CURSOVOIContext())
                {
                   try
                   {
                        var sourcbook = db.Bookmarks.FirstOrDefault(s => s.CodeTitle == int.Parse(shortcodeBook));
                        var sb = sourcbook.CodeTitle;
                        db.Bookmarks.Remove(sourcbook);
                        db.SaveChanges();
                        
                        MessageBox.Show("Закладка удалена!");
                        BookmarkCatalog.Children.Clear();
                        var sourc = db.Users.FirstOrDefault(s => s.UsersLoqin == LoqUs.ToString() && s.UsersPassword == PasUs.ToString());
                        var Cod = sourc.UnicCodeUsers;
                        UserLoqin.Text = LoqUs.ToString();

                        var sourcBook = db.Bookmarks.Where(b => b.UnicCodeUsers == Cod).ToList();
                        k = sourcBook.Count;
                        Button[] btns = new Button[k];
                        // var imgtitcode = sourcTitle.Photo;
                        foreach (Bookmarks book in sourcBook)
                        {
                            var titcode = book.CodeTitle;

                            var sourcTitle = db.Title.FirstOrDefault(t => t.CodeTitle == titcode);
                            var st = sourcTitle.CodeTitle;
                            var picst = sourcTitle.Photo;

                            string imgtitcodepath = Environment.CurrentDirectory + "/PHOTOTITLE/" + $"{picst}";
                            // var bok = db.Bookmarks.ToList();
                            var stakk = new StackPanel
                            {
                                Name = "Stack" + st,
                                Orientation = Orientation.Vertical,
                                Height = 200,
                                Width = 120,
                                Margin = new Thickness(10, 5, 0, 0)
                            };

                            var btndel = new Button
                            {
                                Name = "DelTit" + st,
                                Background = new ImageBrush { ImageSource = new BitmapImage(new Uri("D:/ЛЕНННННА/4 семестр/Kursovoi/Kursovoi/bin/Debug/net5.0-windows/PHOTOTITLE/Del.png")) },
                                Height = 10,
                                Width = 10,
                                VerticalAlignment = VerticalAlignment.Top,
                                HorizontalAlignment = HorizontalAlignment.Right,
                                Margin = new Thickness(0, 0, 0, 0),
                                BorderThickness = new Thickness(0, 0, 0, 0)
                            };
                            var btnbook = new Button
                            {
                                Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(imgtitcodepath)) },
                                Name = "Title" + st //== st
                                ,
                                VerticalAlignment = VerticalAlignment.Bottom,
                                Height = 164,
                                Width = 120,
                                Margin = new Thickness(0, 5, 0, 0)

                            };
                            btnbook.Click += GoBookmark;

                            stakk.Children.Add(btndel);
                            stakk.Children.Add(btnbook);
                            btndel.Click += DeleteBookmark;
                            BookmarkCatalog.Children.Add(stakk);

                        }

                        // string path = Environment.CurrentDirectory + " / PHOTOTITLE/" + $"{sourc.PhotoUsers}";
                        if (sourc.PhotoUsers == null)
                        {
                            UserImg.Source = new BitmapImage(new Uri("D:/ЛЕНННННА/4 семестр/КУРСОВОЙ ООП/BD/Kursovoi/Kursovoi/bin/Debug/net5.0-windows/PHOTOTITLE/All.jpg"));
                        }
                        else
                        {

                            Binding binding = new Binding();
                            binding.Source = sourc.PhotoUsers;
                            UserImg.SetBinding(Image.SourceProperty, binding);
                            //UserImg.Source = new BitmapImage(new Uri(path));
                        }

                        //AddToCat();
                    }
                    catch (Exception ex)
                   {
                       MessageBox.Show("Закладку не удалось удалить!");
                   }
                }
            }
        }


        public void GoBookmark(object sender, RoutedEventArgs e)
        {

            this.NavigationService.Navigate(new Uri("ShabTitle.xaml", UriKind.Relative));
            var buttonNameBookmark = (sender as Button).Name;
            Application.Current.Resources["TT"] = buttonNameBookmark;
        }


        private string _filepathUser;
        string filedb;

        public object MouseButtons { get; private set; }

        private void AddPhotoUser_Click(object sender, RoutedEventArgs e)
        {
            var LoqUs = Application.Current.Resources["EntUser"];
            var PasUs = Application.Current.Resources["EntPassw"];
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    using (FileStream fileStream = new FileStream(dlg.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        _filepathUser = dlg.FileName; fileStream.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Вы выбрали картинку которая уже существует!");
                };
                var file = System.IO.Path.GetFileName(_filepathUser);
                
                using (CURSOVOIContext db = new CURSOVOIContext())
                {
                    var sourc = db.Users.Where(s => s.UsersLoqin == LoqUs.ToString() && s.UsersPassword == PasUs.ToString()).ToList();
                    foreach (Users ugn in sourc)
                    { 
                        var uss = ugn.PhotoUsers;
                       // var titred = db.Users.FirstOrDefault(u => u.PhotoUsers == uss);
                        var usphoto = db.Users.FirstOrDefault(u => u.PhotoUsers == file);
                        if (usphoto != null && _filepathUser == usphoto.PhotoUsers)
                        {
                            MessageBox.Show("Данное фото уже существует!");
                        }
                        else
                        {
                            filedb = Environment.CurrentDirectory + "/PHOTOTITLE/" + $"{file}";
                            UserImg.Source = new BitmapImage(new Uri(_filepathUser));

                            //db.Users.Remove(titred);
                            ugn.PhotoUsers = UserImg.Source.ToString();
                            db.SaveChanges();

                        }
                    }
                }
            }
        }
    }
}
