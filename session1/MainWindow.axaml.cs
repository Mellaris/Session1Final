using Avalonia.Controls;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace session1
{
    public partial class MainWindow : Window
    {
        List<Person> people = new List<Person>();
        List<Person> users = new List<Person>();
        List<ProductInfo> product = new List<ProductInfo>();
        List<ProductInfo> bask = new List<ProductInfo>();
        List<ProductInfo> baskH = new List<ProductInfo>();
        string fio = "Гость";
        public MainWindow()
        {
            InitializeComponent();
            
        }
        public MainWindow(List<ProductInfo> prod, List<ProductInfo> basketProd, List<ProductInfo> basketProdH)
        {
            InitializeComponent(); 
            foreach (ProductInfo person in prod)
            {
                product.Add(person);
            }
            foreach (ProductInfo product in basketProd)
            {
                bask.Add(product);
            }
            foreach(ProductInfo product in basketProdH)
            {
                baskH.Add(product);
            }
        }

        public void Visitor(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            new User(fio, product, bask, baskH).Show();
            Close();
        }
        public void Open(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            people.Add(new Person() { NameV = "Admin", Password = "Admin" });
            users.Add(new Person() { NameV = "User", Password = "User" });
            users.Add(new Person() { NameV = "Men", Password = "Men" });
            foreach (Person person in people)
            {
                if(person.NameV == nameUser.Text)
                {
                    fio = nameUser.Text;
                    new Admin(product,fio,bask, baskH).Show();
                    Close();
                    break;
                }
            }
            foreach (Person person in users)
            {
                if (person.NameV == nameUser.Text)
                {
                    fio = nameUser.Text;
                    new User(fio, product, bask, baskH).Show();
                    Close();
                    break;
                }
            }
        }
    }
    public class Person
    {
        public string NameV { get; set; }
        public string Password { get; set; }
    }
}