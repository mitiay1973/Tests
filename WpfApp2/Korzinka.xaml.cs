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
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для Korzinka.xaml
    /// </summary>
    public partial class Korzinka : Window
    {
        public bool itog = false;
        List<Basket> baskets = Entities.GetContext().Basket.ToList();
        public Korzinka()
        {
            InitializeComponent();
            List.ItemsSource = baskets;
            var allTypes = Entities.GetContext().Avtors.ToList();
            allTypes.Insert(0, new Avtors
            {
                Avtor = "Пункты выдачи"
            });
            combo.ItemsSource = allTypes;
            combo.SelectedIndex = 0;
            int c = 0;
            for (int i = 0; i < baskets.Count; i++)
            {
                c += Convert.ToInt32(baskets[i].Books.Cost) * Convert.ToInt32(baskets[i].koll);
            }
            ItogCost.Content = Convert.ToString(c + " руб");
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            Entities.GetContext().Basket.RemoveRange(baskets);
            Entities.GetContext().SaveChanges();
            itog = true;
            Close();

        }

        private void pynkt_Click(object sender, RoutedEventArgs e)
        {
           PrintDialog printDialog = new PrintDialog();
            printDialog.PrintVisual(List, "");
        }

        private async void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            await Task.Delay(100);
            List<Basket> b = new List<Basket> { new Basket() };
            List<Basket> b1 = Entities.GetContext().Basket.ToList();
            b[0] = List.SelectedItem as Basket;
            for (int i = 0; i < b1.Count; i++)
            {
                if (b1[i].ID == b[0].ID)
                {
                    b1[i].koll -= 1;
                }
            }
            for (int i = 0; i < b1.Count; i++)
            {
                if (b1[i].ID == b[0].ID)
                {
                    if (b1[i].koll == 0)
                    {
                        Entities.GetContext().Basket.Remove(b1[i]);
                    }
                }
            }
            Entities.GetContext().SaveChanges();
            List.ItemsSource = Entities.GetContext().Basket.ToList();
            b1 = Entities.GetContext().Basket.ToList();
            int c = 0;
            for (int i = 0; i < b1.Count; i++)
            {
                c += Convert.ToInt32(b1[i].Books.Cost) * Convert.ToInt32(b1[i].koll);
            }
            ItogCost.Content = Convert.ToString(c + " руб");
        }

        private async void Label_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            await Task.Delay(100);
            List<Basket> b = new List<Basket> { new Basket() };
            List<Basket> b1 = Entities.GetContext().Basket.ToList();
            b[0] = List.SelectedItem as Basket;
            for (int i = 0; i < b1.Count; i++)
            {
                if (b1[i].ID == b[0].ID)
                {
                    b1[i].koll += 1;
                }
            }
            Entities.GetContext().SaveChanges();
            List.ItemsSource = Entities.GetContext().Basket.ToList();
            b1 = Entities.GetContext().Basket.ToList();
            int c = 0;
            for (int i = 0; i < b1.Count; i++)
            {
                c += Convert.ToInt32(b1[i].Books.Cost) * Convert.ToInt32(b1[i].koll);
            }
            ItogCost.Content = Convert.ToString(c + " руб");
        }
    }
}
