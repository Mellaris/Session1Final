using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;

namespace session1;

public partial class Basket : Window
{
    List<ProductInfo> basketProducts = new List<ProductInfo>();
    List<ProductInfo> basketProductsH = new List<ProductInfo>();
    List<ProductInfo> products = new List<ProductInfo>();
    string f;
    double sumProd;
    public Basket()
    {
        InitializeComponent();
    }
    public Basket(string fio, List<ProductInfo> prod, List<ProductInfo> bask, List<ProductInfo> baskH)
    {
        InitializeComponent();
        f = fio;
        foreach (ProductInfo product in bask)
        {
            basketProducts.Add(product);
        }
        foreach (ProductInfo item in prod)
        {
            products.Add(item);
        }
        Products.ItemsSource = basketProducts.ToList();
        Sum();
    }
    public void Back(object sender, RoutedEventArgs e)
    {
        new User(f, products, basketProducts, basketProductsH).Show();
        Close();
    }
    public void Delete(object sender, RoutedEventArgs e)
    {
        int selectDel = (int)(sender as Button).Tag;
        foreach (ProductInfo product in basketProducts)
        {
            if(selectDel == product.id)
            {
                basketProducts.RemoveAt(basketProducts.IndexOf(product));
                break;
            }
        }
        Products.ItemsSource = basketProducts.ToList();
        Sum();
    }
    public void Sum()
    {
        sumProd = 0;
        foreach (ProductInfo chg in basketProducts)
        {
            sumProd = sumProd + chg.Price;
        }
        summa.Text = sumProd.ToString();
    }
}