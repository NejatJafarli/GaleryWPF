using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace GaleryWPF
{
    public class MyImage
    {
        public string CreatedTime { get; set; }
        public string ImageSource { get; set; }
        public string ImageName { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public MyImage(string name, int size, string type, string Path)
        {
            var temp = DateTime.Now;
            CreatedTime = $"{temp.ToShortDateString()} {temp.ToShortTimeString()}";
            ImageName = name;
            Size = $"{size}MB";
            Type = type;
            ImageSource = Path;


        }
        public MyImage(DateTime temp)
        {

            CreatedTime = $"{temp.ToShortDateString()} {temp.ToShortTimeString()}";

        }
        public MyImage()
        {

        }
    }
}
