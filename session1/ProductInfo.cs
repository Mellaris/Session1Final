using Avalonia.Controls;
using Avalonia.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using System.Collections;
using System.Xml.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Media;
using System.ComponentModel.DataAnnotations;

namespace session1
{
    public class ProductInfo()
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public double Price { get; set; }
        public int Amount { get; set; }
        public string Measurement { get; set; }
        public string Category { get; set; }
        public string fileName { get; set; }
        public int id { get; set; }
        public int edit { get; set; }
        public Avalonia.Media.Color gridColor { get; set; }
        
        public Bitmap ProductImage
        {
            get
            {
                return new Bitmap(fileName);
            }
            set { }
        }
    }
}
