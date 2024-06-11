using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Platform.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using Avalonia.Media.Imaging;
using Bitmap = Avalonia.Media.Imaging.Bitmap;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System.Diagnostics.Metrics;
using System.ComponentModel.Design;

namespace session1;

public partial class Editt : Window
{
    string file;
    int help;
    string fio;
    List<ProductInfo> product = new List<ProductInfo>();
    List<ProductInfo> editProduct = new List<ProductInfo>();
    List<ProductInfo> baskProd = new List<ProductInfo>();
    List<ProductInfo> baskH = new List<ProductInfo>();
    List<string> categoriProd = new List<string> { "Продукты", "Техника", "Одежда", "Игрушки" };
    public Editt()
    {
        InitializeComponent();
    }
    public Editt(List<ProductInfo> edit, List<ProductInfo> products, string f, List<ProductInfo> basket, List<ProductInfo> basketH)
    {
        InitializeComponent();
        fio = f;
        foreach (ProductInfo item in edit)
        {
            name.Text = item.Name;
            description.Text = item.Description;
            manufacturer.Text = item.Manufacturer;
            price.Text = Convert.ToString(item.Price);
            amount.Text = Convert.ToString(item.Amount);
            measurement.Text = item.Measurement;
            if (item.fileName != null)
            {
                im.Source = new Bitmap(item.fileName);
            }
            editProduct.Add(item);
        }
        foreach (ProductInfo item in products)
        {
            product.Add(item);
        }
        foreach (ProductInfo item in basket)
        {
            baskProd.Add(item);
        }
        foreach (ProductInfo item in basketH)
        {
            baskH.Add(item);
        }
    }
    public void Edit(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        
        foreach (ProductInfo item in editProduct)
        {
            foreach (ProductInfo item2 in product)
            {
                if (item2.Name == item.Name)
                {
                    item2.Name = name.Text;
                    item2.Description = description.Text;
                    item2.Manufacturer = manufacturer.Text;
                    item2.Price = Convert.ToDouble(price.Text);
                    item2.Amount = Convert.ToInt32(amount.Text);
                    item2.Measurement = measurement.Text;
                    item2.Category = Convert.ToString(category.SelectedIndex);
                    if(help > 0)
                    {
                        item2.fileName = file;
                    }
                    else if (help < 0)
                    {
                        item2.fileName = item.fileName;
                    }
                    break;
                }
            }
        }
        foreach(ProductInfo item in product)
        {
            foreach (string a in categoriProd)
            {
                if (item.Category == Convert.ToString(categoriProd.IndexOf(a)))
                {
                    item.Category = Convert.ToString(a);
                    break;
                }
            }
        }
        new Admin(product, fio, baskProd, baskH).Show();
        Close();
    }
    private string _photo;
    public async void Pict(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        help++;
        OpenFileDialog openFileDialog = new OpenFileDialog();
        var topLevel = await openFileDialog.ShowAsync(this);
        file = String.Join("", topLevel);
        Random random = new Random();
        string photo = "photo" + random.Next(1, 10000) + ".jpg";
        _photo = photo;
        im.Source = new Bitmap(file);
    }
}