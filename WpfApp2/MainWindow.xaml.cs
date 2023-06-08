using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      
        public MainWindow()
        {
            InitializeComponent();
            List<Basket> b = Entities.GetContext().Basket.ToList();
            List.ItemsSource=Entities.GetContext().Books.ToList();
            if(b.Count > 0 )
            {
                int cont = 0;
                for (int i = 0; i < b.Count; i++)
                {
                    cont += Convert.ToInt32(b[i].koll);
                }
                kolKorz.Content = Convert.ToString(cont) + " шт.";
                korz.Visibility=Visibility.Visible;
                kolKorz.Visibility=Visibility.Visible;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            List<Basket> b = Entities.GetContext().Basket.ToList();
            List<Books> books = new List<Books> { new Books() };
            books[0]=List.SelectedItem as Books;
            int k = 1;
            bool pr = false;
            int p=0;
            for (int i = 0; i < b.Count; i++)
            {
                if (b[i].id_Book == books[0].Id_Product)
                {
                    if (books[0].koll> b[i].koll)
                    {
                        k = (int)b[i].koll + 1;
                        pr = true;
                        p = i;
                    }

                }
            }
            if(pr==true)
            {
                b[p].koll = k;
            }
            else
            {

                    var add = new Basket { id_Book = books[0].Id_Product, koll = k };
                    Entities.GetContext().Basket.Add(add);

            }
            Entities.GetContext().SaveChanges();
            int cont = 0;
            List<Basket> b1 = Entities.GetContext().Basket.ToList();
            for (int i = 0; i < b1.Count; i++)
            {
                cont += Convert.ToInt32(b1[i].koll);
            }
            kolKorz.Content = Convert.ToString(cont) + " шт.";
            korz.Visibility = Visibility.Visible;
            kolKorz.Visibility = Visibility.Visible;
            int c = 0;
            for (int i = 0; i < b1.Count; i++)
            {
                c += Convert.ToInt32(b1[i].Books.Cost) * Convert.ToInt32(b1[i].koll);
            }
            MessageBox.Show(Convert.ToString(c));
        }

        private void korz_Click(object sender, RoutedEventArgs e)
        {
            Korzinka korzinka = new Korzinka();
            korzinka.ShowDialog();
            if (korzinka.itog == true)
            {      
                    kolKorz.Content =  "";
                    korz.Visibility = Visibility.Collapsed;
                    kolKorz.Visibility = Visibility.Collapsed;
            }
        }
    }
}
