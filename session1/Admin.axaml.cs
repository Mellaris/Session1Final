using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace session1;

public partial class Admin : Window
{
    List<ProductInfo> product = new List<ProductInfo>();
    List<ProductInfo> editProduct = new List<ProductInfo>();
    List<ProductInfo> basketProduct = new List<ProductInfo>();
    List<ProductInfo> basketProductH = new List<ProductInfo>();
    int i;

    public Admin()
    {
        InitializeComponent();
    }
    public Admin(List<ProductInfo> prod, string f, List<ProductInfo> bask, List<ProductInfo> baskH)
    {
        InitializeComponent();


        fio.Text = f;
        foreach (ProductInfo prodItem in prod)
        {
            product.Add(prodItem);

        }
        foreach (ProductInfo prodItem in bask)
        {
            basketProduct.Add(prodItem);
        }
        foreach (ProductInfo prodItem in baskH)
        {
            basketProductH.Add(prodItem);
        }
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
            x.edit
        }).ToList();
        kol.Text = product.Count.ToString();

    }
    public void Delete(object? sender, Avalonia.Interactivity.RoutedEventArgs e)

    {
        bool help = false;
        int selectDel = (int)(sender as Button).Tag;
        foreach (ProductInfo c in basketProduct)
        {
            if (c.id == selectDel)
            {
                help = true;
                break;
            }
        }
        if (help == false)
        {
            foreach (ProductInfo b in product)
            {
                if (b.id == selectDel)
                {
                    product.RemoveAt(product.IndexOf(b));
                    break;
                }
            }
        }
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
            x.edit
        }).ToList();
        kol.Text = product.Count.ToString();
    }
    public void ListBox_DoubleTapped(object? sender, PointerReleasedEventArgs e)
    {
        editProduct.Add(product[Products.SelectedIndex]);
        new Editt(editProduct, product, fio.Text, basketProduct, basketProductH).Show();
        Close();
    }
    public void Add(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        new Addendum(product, fio.Text, basketProduct, basketProductH).Show();
        Close();
    }
    public void Exit(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        new MainWindow(product, basketProduct, basketProductH).Show();
        Close();
    }

}