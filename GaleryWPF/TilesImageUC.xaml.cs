using System;
using System.Collections.Generic;
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
    /// Interaction logic for TilesImageUC.xaml
    /// </summary>
    public partial class TilesImageUC : UserControl
    {


        public MyImage ImageSource
        {
            get { return (MyImage)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(MyImage), typeof(TilesImageUC));


        public TilesImageUC()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
