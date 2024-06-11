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
using System.Text.Unicode;
namespace session1;

public partial class Addendum : Window
{
    string fileName;
    string pr;
    string amoun;
    int help;
    private string _photo;
    string fio;
    List<ProductInfo> products = new List<ProductInfo>();
    List<string> categoriProd = new List<string> { "Продукты", "Техника", "Одежда", "Игрушки" };
    List<ProductInfo> bask = new List<ProductInfo>();
    List<ProductInfo> baskH = new List<ProductInfo>();
    public Addendum()
    {
        InitializeComponent();
    }
    public Addendum(List<ProductInfo> product, string f, List<ProductInfo> baskProd, List<ProductInfo> baskProdH)
    {
        InitializeComponent();
        fio = f;
        foreach (ProductInfo p in product)
        {
            products.Add(p);
        }
        foreach (ProductInfo p in baskProd)
        {
            bask.Add(p);
        }
        foreach (ProductInfo p in baskProdH)
        {
            baskH.Add(p);
        }
        this.bask = bask;
    }
    public void Add(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (help != 1)
        {          
            fileName = "D:/Черновики Авалония/session1/session1/Assets/ppppp.png";
            im.Source = new Bitmap(fileName);
        }
        pr = price.Text;
        amoun = amount.Text;
        if (!string.IsNullOrEmpty(name.Text) && !string.IsNullOrEmpty(description.Text) && !string.IsNullOrEmpty(manufacturer.Text) && !string.IsNullOrEmpty(amount.Text) && !string.IsNullOrEmpty(measurement.Text) && !string.IsNullOrEmpty(pr))
        {
            try
            {
                products.Add(new ProductInfo()
                {
                    Name = name.Text,
                    Description = description.Text,
                    Manufacturer = manufacturer.Text,
                    Price = Convert.ToDouble(pr),
                    Amount = Convert.ToInt32(amoun),
                    Measurement = measurement.Text,
                    Category = Convert.ToString(category.SelectedIndex),
                    fileName = fileName,
                    id = products.Count,
                    edit = products.Count,
                });
                foreach (ProductInfo p in products)
                {
                    foreach (string a in categoriProd)
                    {
                        if (p.Category == Convert.ToString(categoriProd.IndexOf(a)))
                        {
                            p.Category = Convert.ToString(a);
                            break;
                        }
                    }
                }

                new Admin(products, fio, bask, baskH).Show();
                Close();
            }
            catch
            {
                mist.Text = "Ошибка в 4 или 5 поле. Не тот тип данных";
            }
            
            
        }
        else
        {
            mist.Text = "Ошибка. Не все поля заполнены";
        }
    }
    public async void Pict(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        help = 1;
        OpenFileDialog openFileDialog = new OpenFileDialog();
        var topLevel = await openFileDialog.ShowAsync(this);
        fileName = String.Join("", topLevel);
        im.Source = new Bitmap(fileName);
    }
}