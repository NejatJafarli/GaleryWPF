using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        public ObservableCollection<TilesImageUC> ViewModeTilesImages { get; set; }
        public ObservableCollection<SmallIconImageUC> ViewModeSmallIcon { get; set; }



        public MyImage SelectedImage
        {
            get { return (MyImage)GetValue(SelectedImageProperty); }
            set { SetValue(SelectedImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedImageProperty =
            DependencyProperty.Register("SelectedImage", typeof(MyImage), typeof(MainWindow));

        public List<string> Types { get; set; }
        public MainWindow()
        {

            InitializeComponent();
            DataContext = this;
            //Images = new ObservableCollection<MyImage>();
            Types = new List<string>();
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
                .RuleFor(I => I.ImageSource, I => I.Image.PicsumUrl(170, 185)).Generate(10);


            Images = new ObservableCollection<MyImage>(CreateImages);
            ViewModeTilesImages = new ObservableCollection<TilesImageUC>();
            ViewModeSmallIcon = new ObservableCollection<SmallIconImageUC>();

            var time=DateTime.Now;

            for (int i = 0; i < Images.Count; i++)
            {
                Images[i].CreatedTime = $"{time.ToShortDateString()} {time.ToShortTimeString()}";

                var temp = new TilesImageUC();
                temp.ImageSource = Images[i];
                ViewModeTilesImages.Add(temp);
                ViewModeTilesImages[i].Width = 180;
                ViewModeTilesImages[i].Height = 185;

                var temp2 = new SmallIconImageUC();
                temp2.ImageSource = Images[i];
                ViewModeSmallIcon.Add(temp2);
                ViewModeSmallIcon[i].Width = 250;
                ViewModeSmallIcon[i].Height = 23;

            }


            //Images.Add(new MyImage("NoBody",300,"PNG"));


        }

        private void MenuTiles_Click(object sender, RoutedEventArgs e)
        {

            Panel.SetZIndex(ViewTiles, 1);
            Panel.SetZIndex(ViewDetails, 0);
            Panel.SetZIndex(SmallIconView, 0);
        }

        private void MenuDetails_Click(object sender, RoutedEventArgs e)
        {
            Panel.SetZIndex(ViewDetails, 1);
            Panel.SetZIndex(ViewTiles, 0);
            Panel.SetZIndex(SmallIconView, 0);

        }

        private void MenuSmall_Click(object sender, RoutedEventArgs e)
        {
            Panel.SetZIndex(ViewDetails, 0);
            Panel.SetZIndex(ViewTiles, 0);
            Panel.SetZIndex(SmallIconView, 1);
        }
        private void Image_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }
        private void Image_Drop(object sender, DragEventArgs e)
        {
            var temp = new MyImage();
            string path = "";
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (var item in files)
            {
                path = item;
            }
            var random = new Random();

            var temp4 = path.Split("\\").ToList();

            temp.ImageName = temp4[temp4.Count - 1].Split('.')[0];

            temp.ImageSource = path;

            temp.Type = $"{temp4[temp4.Count - 1].Split('.')[1].ToUpper()} File";

            DateTime Time = File.GetCreationTime(path);
            temp.CreatedTime = $"{Time.ToShortDateString()} {Time.ToShortTimeString()}";

            FileInfo fi = new FileInfo(path);

            temp.Size = $"{fi.Length /1_000_000}mb";

            Images.Add(temp);
            var Tiles=new TilesImageUC();
            Tiles.ImageSource = temp;
            ViewModeTilesImages.Add(Tiles);
            ViewModeTilesImages[ViewModeTilesImages.Count-1].Width = 180;
            ViewModeTilesImages[ViewModeTilesImages.Count-1].Height = 185;

            var temp2 = new SmallIconImageUC();
            temp2.ImageSource = temp;
            ViewModeSmallIcon.Add(temp2);
            ViewModeSmallIcon[ViewModeSmallIcon.Count-1].Width = 250;
            ViewModeSmallIcon[ViewModeSmallIcon.Count-1].Height = 23;


        }
    }
}
