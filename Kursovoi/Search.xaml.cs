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
    /// Логика взаимодействия для Search.xaml
    /// </summary>
    public partial class Search : Page
    {
        public Search()
        {
            InitializeComponent();
            
        }
       
        public void SearchCom(object sender, RoutedEventArgs e)
        { /*
            string NameToSearch = nameFiltr.Text.Trim();
            Catalog cat = new Catalog();
            var wrapTiC = cat.TitleCatalog.Children;
            
            
            wrapTiC.Clear();
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
                            Height = 134,
                            Width = 100,
                            Margin = new Thickness(30, 20, 0, 0)
                        };
                        
                        cat.TitleCatalog.Children.Add(btnsearch);
                        btnsearch.Click += cat.Image_MouseLeftButtonDown;
                }
                
            }
       */  }
       
       
    }
}
