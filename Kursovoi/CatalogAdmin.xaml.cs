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
        //CURSOVOIContext db;
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
                        Margin = new Thickness(30, 20, 0, 0)
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
                        Margin = new Thickness(30, 20, 0, 0)
                    };

                    TitleCatalog.Children.Add(btnsearch);
                    btnsearch.Click += Image_MouseLeftButtonDown;
                }

            }
        }


        private void GoFilterGENRE(object sender, RoutedEventArgs e)
        {


            MCSGen.AddHandler(RadioButton.CheckedEvent, new RoutedEventHandler(RadioButton_Click));

            TitleCatalog.Children.Clear();
            using (CURSOVOIContext db = new CURSOVOIContext())
            {
                //SelectedValue = "Школа";

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
                        Margin = new Thickness(30, 20, 0, 0)
                    };
                    TitleCatalog.Children.Add(btn);
                    btn.Click += Image_MouseLeftButtonDown;
                }
            }
        }

        private void GOFilterSKIP(object sender, RoutedEventArgs e)
        {
            TitleCatalog.Children.Clear();
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
                        Margin = new Thickness(30, 20, 0, 0)
                    };
                    btn.Click += Image_MouseLeftButtonDown;
                    TitleCatalog.Children.Add(btn);
                }
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

        private void AddTitle(object sender, RoutedEventArgs e)
        {
            using (CURSOVOIContext db = new CURSOVOIContext())
            {
                
                try {
                
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
                    Link = LinkTit

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
                   

                    }
                    catch
                    {
                        MessageBox.Show("Не удалось добавить тайтл!");
                        
                    }
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
            //RadioButton radioButton = (RadioButton)sender;
          
                PubT.Text = "";
            AuT.Text = "";
            GenT.Text = "";
            TranT.Text = "";
            LinkSite.Text = "";

            DesT.Text = "";
            _filepath = "";
        }
    }
}
