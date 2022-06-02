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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Win32;

namespace Kursovoi
{
    /// <summary>
    /// Логика взаимодействия для Catalog.xaml
    /// </summary>
    public partial class CatalogAdmin : Page
    {
        public int n;
        
        public CatalogAdmin()
        {
            InitializeComponent();
            using (CURSOVOIContext db = new CURSOVOIContext())
            {
                var pic = db.Title.ToList();
                n = pic.Count;
                Button[] btns = new Button[n];
                foreach (Title u in pic)
                {
                    string path = Environment.CurrentDirectory + "/PHOTOTITLE/" + $"{u.Photo}";
                    var btn = new Button
                    {
                        Background = new ImageBrush
                        { ImageSource = new BitmapImage(new Uri(path)) },
                        Name = "Title" + u.CodeTitle,
                        Height = 164,
                        Width = 120,
                        Margin = new Thickness(30, 20, 0, 0),
                        Cursor = Cursors.Hand
                    };
                    btn.Click += Image_MouseLeftButtonDown;
                    TitleCatalog.Children.Add(btn);
                }

            };

        }

      
        private void Image_MouseLeftButtonDown(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("ShabTitle.xaml", UriKind.Relative));
            var buttonName = (sender as Button).Name;
            Application.Current.Resources["TT"] = buttonName;
        }

        

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {

            RadioButton selectedRadio = (RadioButton)e.Source;
            string value = selectedRadio.Content.ToString();
            if (selectedRadio.IsChecked == true)
            {

                Application.Current.Resources["RAD"] = value;

            }
        }
        private void RadioButton_ClickType(object sender, RoutedEventArgs e)
        {

            RadioButton selectedRadio = (RadioButton)e.Source;
            string value = selectedRadio.Content.ToString();
            if (selectedRadio.IsChecked == true)
            {

                Application.Current.Resources["TypeTit"] = value;

            }
        }

        private void RadioButton_ClickStat(object sender, RoutedEventArgs e)
        {
            RadioButton selectedRadio = (RadioButton)e.Source;
            string value = selectedRadio.Content.ToString();
            if (selectedRadio.IsChecked == true)
            {

                Application.Current.Resources["StatTit"] = value;

            }
        }

        public void SearchCom(object sender, RoutedEventArgs e)
        {
            string NameToSearch = nameFiltr.Text.Trim();

            try
            {
                if (NameToSearch == "")
                {
                    MessageBox.Show("Вы не ввели текст");
                }
                else
                {
                    TitleCatalog.Children.Clear();
                    using (CURSOVOIContext db = new CURSOVOIContext())
                    {

                        var cldf = db.Title.Where(p => EF.Functions.Like(p.NameOfTitle, $"%{NameToSearch}%")).ToList();

                        var c = cldf.Count;

                        Button[] btns = new Button[c];
                        foreach (Title u in cldf)
                        {
                            string path = Environment.CurrentDirectory + "/PHOTOTITLE/" + $"{u.Photo}";
                            var btnsearch = new Button
                            {
                                Background = new ImageBrush
                                { ImageSource = new BitmapImage(new Uri(path)) },
                                Name = "Title" + u.CodeTitle,
                                Height = 164,
                                Width = 120,
                                Margin = new Thickness(30, 20, 0, 0),
                                Cursor = Cursors.Hand
                            };

                            TitleCatalog.Children.Add(btnsearch);
                            btnsearch.Click += Image_MouseLeftButtonDown;
                        }
                    }
                }

            }
            catch
            {
                MessageBox.Show("Что-то пошло не так");
            }

        }


        private void GoFilterGENRE(object sender, RoutedEventArgs e)
        {
            MCSGen.AddHandler(RadioButton.CheckedEvent, new RoutedEventHandler(RadioButton_Click));

            TitleCatalog.Children.Clear();
            try
            {
                using (CURSOVOIContext db = new CURSOVOIContext())
                {

                    var cucoldfiltr = db.Title.Where(p => EF.Functions.Like(p.Genre, $"%{Application.Current.Resources["RAD"]}%")).ToList();
                    n = cucoldfiltr.Count;
                    Button[] btns = new Button[n];
                    foreach (Title u in cucoldfiltr)

                    {
                        string path = Environment.CurrentDirectory + "/PHOTOTITLE/" + $"{u.Photo}";
                        var btn = new Button
                        {
                            Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(path)) },
                            Name = "Title" + u.CodeTitle,
                            Height = 164,
                            Width = 120,
                            Margin = new Thickness(30, 20, 0, 0),
                            Cursor = Cursors.Hand
                        };
                        TitleCatalog.Children.Add(btn);
                        btn.Click += Image_MouseLeftButtonDown;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так");
            }
        }

        private void GOFilterSKIP(object sender, RoutedEventArgs e)
        {
            TitleCatalog.Children.Clear();
            try
            {
                using (CURSOVOIContext db = new CURSOVOIContext())
                {
                    var pic = db.Title.ToList();
                    n = pic.Count;
                    Button[] btns = new Button[n];
                    foreach (Title u in pic)
                    {
                        string path = Environment.CurrentDirectory + "/PHOTOTITLE/" + $"{u.Photo}";
                        var btn = new Button
                        {
                            Background = new ImageBrush
                            { ImageSource = new BitmapImage(new Uri(path)) },
                            Name = "Title" + u.CodeTitle,
                            Height = 164,
                            Width = 120,
                            Margin = new Thickness(30, 20, 0, 0),
                            Cursor = Cursors.Hand
                        };
                        btn.Click += Image_MouseLeftButtonDown;
                        TitleCatalog.Children.Add(btn);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так");
            }
        }


        public T SelectedRadioValue<T>(T defaultValue, params RadioButton[] buttons)
        {
            foreach (RadioButton button in buttons)
            {
                if (button.IsChecked == true)
                {
                    if (button.Tag is string && typeof(T) != typeof(string))
                    {
                        string value = (string)button.Tag;
                        return (T)Convert.ChangeType(value, typeof(T));
                    }

                    return (T)button.Tag;
                }
            }

            return defaultValue;
        }
        private Regex lr = new Regex(@"^(?:(?:https?|ftp):\/\/)?(?:(?!(?:10|127)(?:\.\d{1,3}){3})(?!(?:169\.254|192\.168)( ?:\.\d{1,3}){2})(?!172\.(?:1[6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)(?:\.(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)*(?:\.(?:[a-z\u00a1-\uffff]{2,})))(?::\d{2,5})?(?:\/\S*)?$");

      
        private void AddTitle(object sender, RoutedEventArgs e)
        {
            string LinkChepTitReg = LinkChep.Text;
            string LinkReg = LinkSite.Text;
            bool LCMatch = lr.IsMatch(LinkReg);
            bool LCTR = lr.IsMatch(LinkChepTitReg);


            using (CURSOVOIContext db = new CURSOVOIContext())
            {
                int k = 0;
                try
                {

                    int IdTit = db.Title.Max(m => m.CodeTitle);
                    int CodeType = db.TypeOfComics.Max(c => c.CodeTypeOfComics);
                    int CodeAuth = db.Author.Max(a => a.CodeAuthor);
                    int CodeTrans = db.Translator.Max(t => t.CodeTranslator);
                    int CodeStat = db.Translation.Max(t => t.CodeTranslation);
                    int CodeDes = db.Description.Max(d => d.CodeDescription);
                    int CodeChep = db.Photochepter.Max(c => c.CodePhChepter);
                    string NameTit = NameT.Text;
                    var DateTit = DateT.SelectedDate;

                    int priority = SelectedRadioValue<int>(1, Manga, Manhva, Rumanga, Manhua);

                    string PublicTit = PubT.Text;
                    string AuthTit = AuT.Text;
                    string GenreTit = GenT.Text;
                    string TransTit = TranT.Text;
                    string LinkTit = LinkSite.Text;
                    string LinkChepTit = LinkChep.Text;



                    int prioritystat = SelectedRadioValue<int>(1, Prod, Zam, Zav, Zab);

                    string DescrTit = DesT.Text;


                    var file = System.IO.Path.GetFileName(_filepath);

                    file = PathImT.Text;

                    var nn = db.Title.Where(n => n.NameOfTitle == NameTit).ToList();
                    if ((NameT.Text != null && _filepath != null && DateT.SelectedDate != null && PubT.Text != null && AuT.Text != null && GenT != null && TranT.Text != null && LinkSite.Text != null && LinkChep.Text != null && DesT.Text != null))
                    {
                        if (nn != null)
                        {
                            foreach (var item in nn)
                            {
                                if (item.NameOfTitle == NameTit)
                                {
                                    k++;
                                }
                            }
                            if (k == 0)
                            {

                                if (LCMatch && LCTR)
                                {

                                    Title newtitle = new Title
                                    {
                                        CodeTitle = IdTit + 1,
                                        NameOfTitle = NameTit,
                                        ReleaseDate = (DateTime)DateTit,
                                        Publisher = PublicTit,
                                        Genre = GenreTit,
                                        Photo = System.IO.Path.GetFileName(_filepath),

                                        CodeAuthor = CodeAuth + 1,
                                        CodeTranslator = CodeTrans + 1,
                                        CodeDescription = CodeDes + 1,

                                        CodeCodeTypeOfComics = priority,

                                        CodeTranslation = prioritystat,
                                        Link = LinkTit,
                                        CodePhChepter = CodeChep + 1

                                    };
                                    Author newauthor = new Author
                                    {
                                        CodeAuthor = CodeAuth + 1,
                                        Author1 = AuthTit

                                    };
                                    Translator newtranslator = new Translator
                                    {

                                        CodeTranslator = CodeTrans + 1,
                                        Translator1 = TranT.Text
                                    };
                                    Description newdescription = new Description
                                    {
                                        CodeDescription = CodeDes + 1,
                                        Description1 = DescrTit
                                    };
                                    Photochepter phchep = new Photochepter
                                    {
                                        CodePhChepter = CodeChep + 1,
                                        CodeTitle = IdTit + 1,
                                        PathPhChepter = LinkChepTit
                                    };

                                    db.Title.Add(newtitle);
                                    db.Author.Add(newauthor);
                                    db.Translator.Add(newtranslator);
                                    db.Description.Add(newdescription);
                                    db.Photochepter.Add(phchep);
                                    db.SaveChanges();
                                    MessageBox.Show("Комикс успешно добавлен");

                                    NameT.Text = "";
                                    DateT.SelectedDate = null;
                                    
                                    PubT.Text = "";
                                    AuT.Text = "";
                                    GenT.Text = "";
                                    TranT.Text = "";
                                    LinkSite.Text = "";
                                    LinkChep.Text = "";

                                    DesT.Text = "";
                                    _filepath = "";
                                }
                                else
                                {
                                    MessageBox.Show("Error");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Такой комикс уже существует");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Вы не заполнили все поля!");
                    }
                }

                catch
                {
                    MessageBox.Show("Не удалось добавить тайтл!");

                }
            }
        }
        
    
        public void Reg (object sender, TextChangedEventArgs args)
        {
           
            string LinkReg = LinkSite.Text.Trim();
            bool LCMatch = lr.IsMatch(LinkReg);

            if (!LCMatch)
            {
                LinkSite.Foreground = Brushes.Red;
            }
            else
            {
                LinkSite.Foreground = Brushes.Black;
            }

          
        }
         public void RegChep(object sender, TextChangedEventArgs args)
        {
            string LinkChepTitReg = LinkChep.Text.Trim();
            bool LCTR = lr.IsMatch(LinkChepTitReg);
            if (!LCTR)
            {
                LinkChep.Foreground = Brushes.Red;
            }
            else
            {
                LinkChep.Foreground = Brushes.Black;
            }

        }
        
        private string _filepath;
       
        private void AddImgTit(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            if(dlg.ShowDialog().GetValueOrDefault(false))
            {
                _filepath = dlg.FileName;

                var file = System.IO.Path.GetFileName(_filepath);
            }
        }

        private void DropTitle(object sender, RoutedEventArgs e)
        {
            NameT.Text = "";
            DateT.SelectedDate = null;
          
            PubT.Text = "";
            AuT.Text = "";
            GenT.Text = "";
            TranT.Text = "";
            LinkSite.Text = "";
            LinkChep.Text = "";

            DesT.Text = "";
            _filepath = "";

            
           
        }
    }
}
