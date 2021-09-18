using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GaleryWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window

    {
        public ObservableCollection<MyImage> Images { get; set; }
        public ObservableCollection<TilesImageUC> MyPropertyes { get; set; }



        public Image MyProperty
        {
            get { return (Image)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", typeof(Image), typeof(MainWindow));



        public MainWindow()
        {

            InitializeComponent();
            DataContext = this;
            //Images = new ObservableCollection<MyImage>();

            List<string> Types = new List<string>();
            Types.Add("PNG File");
            Types.Add("JPG File");
            Types.Add("TIFF File");
            Types.Add("BMP File");
            Types.Add("RAF File");
            Types.Add("JPEG File");


            var CreateImages = new Bogus.Faker<MyImage>()
                .RuleFor(I => I.ImageName, I => I.Name.FullName())
                .RuleFor(I => I.Size, I => $"{I.Random.Number(0, 1000)}MB")
                .RuleFor(I => I.Type, I => $"{Types[I.Random.Int(0, Types.Count - 1)]}")
                .RuleFor(I => I.ImageSource, I => I.Image.Image(170, 185, true, false)).Generate(100);

            Images = new ObservableCollection<MyImage>(CreateImages);
            MyPropertyes = new ObservableCollection<TilesImageUC>();

            MyProperty = new Image();
            MyProperty.Source = new BitmapImage(new Uri(Images[1].ImageSource));



            for (int i = 0; i < Images.Count; i++)
            {
                var temp = new TilesImageUC();
                temp.ImageSource = Images[i];
                MyPropertyes.Add(temp);
            MyPropertyes[i].Width = 180;
            MyPropertyes[i].Height = 185;

            }




            //Images.Add(new MyImage("NoBody",300,"PNG"));


        }

        private void MenuTiles_Click(object sender, RoutedEventArgs e)
        {

            Panel.SetZIndex(ViewTiles,1);
            Panel.SetZIndex(ViewDetails,0);
        }

        private void MenuDetails_Click(object sender, RoutedEventArgs e)
        {
            Panel.SetZIndex(ViewTiles, 0);
            Panel.SetZIndex(ViewDetails, 1);
        }
    }
}
