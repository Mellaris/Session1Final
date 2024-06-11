using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.Design;

namespace session1;

public partial class User : Window
{
    List<ProductInfo> product = new List<ProductInfo>();
    List<ProductInfo> basketProductH = new List<ProductInfo>();
    List<ProductInfo> basketProduct = new List<ProductInfo>();
    List<ProductInfo> help = new List<ProductInfo>();
    List<ProductInfo> help2 = new List<ProductInfo>();
    List<string> proizv = new List<string>();
    string p;
    int c = 0;
    int b = 0;
    int positive = 0;
    public User()
    {
        InitializeComponent();
    }
    public User(string f, List<ProductInfo> prod, List<ProductInfo> bask, List<ProductInfo> baskH)
    {
        InitializeComponent();
        fio.Text = f;
        positive = 0;
        foreach (ProductInfo produc in prod)
        {
            product.Add(produc);
        }
        foreach (ProductInfo produc in bask)
        {
            basketProduct.Add(produc);
        }
        Vizov(product);
        kol.Text = product.Count.ToString();
        foreach (ProductInfo produc in product)
        {
            proizv.Add(produc.Manufacturer);
        }
        proizv = proizv.Distinct().ToList();
        var comboBox = this.FindControl<ComboBox>("Combo");
        comboBox.Items.Add("Все производители");
        foreach (var item in proizv)
        {
            var comboBoxItem = new ComboBoxItem { Content = item };
            comboBox.Items.Add(comboBoxItem);
        }
    }
    public List<string> Items
    {
        get { return proizv; }
        set { proizv = value; }
    }
    public void Exit(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        new MainWindow(product, basketProduct, basketProductH).Show();
        Close();
    }
    public void SortPlus(object sender, RoutedEventArgs e)
    {
        b = 0;
        if (positive == 1)
        {
            help.Sort((x, y) => x.Price.CompareTo(y.Price));
            foreach (ProductInfo chg in help)
            {
                chg.edit = help.IndexOf(chg);
                chg.id = help.IndexOf(chg);
            }
            b = 1;
            Vizov(help);
        }
        else if (positive == 2)
        {
            help2.Sort((x, y) => x.Price.CompareTo(y.Price));
            foreach (ProductInfo chg in help2)
            {
                chg.edit = help2.IndexOf(chg);
                chg.id = help2.IndexOf(chg);
            }
            b = 2;
            Vizov(help2);
        }
        else
        {
            product.Sort((x, y) => x.Price.CompareTo(y.Price));
            foreach (ProductInfo chg in product)
            {
                chg.edit = product.IndexOf(chg);
                chg.id = product.IndexOf(chg);
            }
            Vizov(product);
        }
    }
    public void SortMinus(object sender, RoutedEventArgs e)
    {
        b = 0;
        if (positive == 1)
        {
            help.Sort((x, y) => y.Price.CompareTo(x.Price));
            foreach (ProductInfo chg in help)
            {
                chg.edit = help.IndexOf(chg);
                chg.id = help.IndexOf(chg);
            }
            Vizov(help);
            b = 1;
        }
        else if (positive == 2)
        {
            help2.Sort((x, y) => y.Price.CompareTo(x.Price));
            foreach (ProductInfo chg in help2)
            {
                chg.edit = help2.IndexOf(chg);
                chg.id = help2.IndexOf(chg);
            }
            Vizov(help2);
            b = 2;
        }
        else
        {
            product.Sort((x, y) => y.Price.CompareTo(x.Price));
            foreach (ProductInfo chg in product)
            {
                chg.edit = product.IndexOf(chg);
                chg.id = product.IndexOf(chg);
            }
            Vizov(product);
        }
    }
    public void Search(object sender, KeyEventArgs e)
    {
       
        help.Clear();
        p = poisk.Text;
        p = p.ToLower();
        List<string> result = p?.Split(' ').ToList();
        if (result.Count > 0)
        {
            c = 3;
            if (help2.Count == 0)
            {
                //var matchProd = product.Where(
                //    p => p.Name.Contains)
                var matchProd = product.Where(products => result.All(s => products.Name.ToLower().Contains(s) || products.Price.ToString().Contains(s) || products.Manufacturer.ToLower().Contains(s) || products.Amount.ToString().Contains(s) || products.Category.ToLower().Contains(s) || products.Description.ToLower().Contains(s) || products.Measurement.ToLower().Contains(s)));
                foreach (ProductInfo chg in matchProd)
                {
                    help.Add(chg);
                }

            }
            else if (help2.Count > 0)
            {
                var matchProd = help2.Where(products => result.All(s => products.Name.ToLower().Contains(s) || products.Price.ToString().Contains(s) || products.Manufacturer.ToLower().Contains(s) || products.Amount.ToString().Contains(s) || products.Category.ToLower().Contains(s) || products.Description.ToLower().Contains(s) || products.Measurement.ToLower().Contains(s)));
                foreach (ProductInfo chg in matchProd)
                {
                    help.Add(chg);
                }
            }
            Vizov(help);
            if (help.Count == 0)
            {
                kolTov.Text = product.Count.ToString();
                Vizov(product);
            }
            else
            {
                kolTov.Text = help.Count.ToString();
                positive = 1;
            }
        }

        else if (result.Count == 0)
        {
            c = 1;
            if(help2.Count > 0)
            {
                Vizov(help2);
                kolTov.Text = help2.Count.ToString();
            }
            else
            {
                Vizov(product);
                kolTov.Text = product.Count.ToString();
            }
        }
    }
    public void Point(object sender, SelectionChangedEventArgs e)
    {
        int manHelp;
        string manuf = "";
        manHelp = Combo.SelectedIndex;
        help2.Clear();
        if (Convert.ToInt32(manHelp) > 0)
        {
            manHelp = manHelp - 1;
            foreach (string c in proizv)
            {
                if (Convert.ToString(proizv.IndexOf(c)) == Convert.ToString(manHelp))
                {
                    manuf = c;
                    break;
                }
            }
            if (help.Count > 0)
            {
                var filterProducts = help.Where(p => p.Manufacturer == manuf);
                foreach (ProductInfo strProduct in filterProducts)
                {
                    help2.Add(strProduct);
                }
                Vizov(help2);
                kolTov.Text = help2.Count.ToString();
                positive = 2;
            }
            else
            {
                var filterProducts = product.Where(p => p.Manufacturer == manuf);
                foreach (ProductInfo strProduct in filterProducts)
                {
                    help2.Add(strProduct);
                }
                Vizov(help2);
                kolTov.Text = help2.Count.ToString();
                positive = 2;
            }
            manHelp = manHelp + 1;
        }
        else if (Convert.ToInt32(manHelp) == 0)
        {
            positive = 0;
            if(c == 3)
            {
                Vizov(help);
                kolTov.Text = help.Count.ToString();
            }
            else
            {
                Vizov(product);
            }
        }
    }

    public void ListBox_DoubleTapped(object? sender, PointerReleasedEventArgs e)
    {
        if (fio.Text != "Гость" && fio.Text != "Men")
        {
            bool error = false;
            basketProductH.Add(product[Products.SelectedIndex]);
            if (basketProduct.Count > 0)
            {
                foreach (ProductInfo c in basketProductH)
                {
                    foreach (ProductInfo ch in basketProduct)
                    {
                        if (c.edit == ch.edit)
                        {
                            error = true;
                        }
                    }
                    if (error != true)
                    {
                        basketProduct.Add(c);
                    }
                    error = false;
                }
            }
            else
            {
                basketProduct.Add(product[Products.SelectedIndex]);
            }
        }

    }
    private void Vizov(List<ProductInfo> product)
    {
        Products.ItemsSource = product.Select(x => new
        {
            gridColor = x.Amount == 0 ? Brushes.Gray : Brushes.Lavender,
            x.Amount,
            x.Name,
            x.Description,
            x.Manufacturer,
            x.Price,
            x.ProductImage,
            x.Measurement,
            x.Category,
            x.id,
            x.edit,
        }).ToList();
    }
    public void Bask(object sender, RoutedEventArgs e)
    {

        new Basket(fio.Text, product, basketProduct, basketProductH).Show();
        Close();

    }
}