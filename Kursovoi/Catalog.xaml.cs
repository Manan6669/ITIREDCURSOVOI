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

namespace Kursovoi
{
    /// <summary>
    /// Логика взаимодействия для Catalog.xaml
    /// </summary>
    public partial class Catalog : Page
    {
        public int n;
        //CURSOVOIContext db;
        public Catalog()
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
                        Margin = new Thickness(30, 20, 0, 5)
                    };
                    btn.Click += Image_MouseLeftButtonDown;
                    TitleCatalog.Children.Add(btn);
                }
                Application.Current.Resources["ForSearch"] = pic;
                Application.Current.Resources["TC"] = TitleCatalog;
                
            };
            
             Search ser = new Search();
           // ser.SearchCom();
        }
       
        public  void Image_MouseLeftButtonDown(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("ShabTitle.xaml", UriKind.Relative));
            var buttonName = (sender as Button).Name;
            Application.Current.Resources["TT"] = buttonName;
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



        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {

            RadioButton selectedRadio = (RadioButton)e.Source;
            string value = selectedRadio.Content.ToString();
            if (selectedRadio.IsChecked == true)
            {

                Application.Current.Resources["RAD"] = value;

            }
        }
        private void RadioButton_Click2(object sender, RoutedEventArgs e)
        {

            RadioButton selectedRadio = (RadioButton)e.Source;
            string value = selectedRadio.Content.ToString();
            if (selectedRadio.IsChecked == true)
            {

                Application.Current.Resources["RAD2"] = value;
               // value= "TypeOfComics" + TypeOfComics.Code

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
    }
}
