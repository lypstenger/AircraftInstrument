using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemuxTS
{
    /// <summary>
    /// Logique d'interaction pour altimetre.xaml
    /// </summary>
    public partial class altimetre : UserControl
    {
        private Storyboard sb;
        private Storyboard sb1000;
        private Storyboard sb10000;
        private EasingDoubleKeyFrame keyFramecent;
        private EasingDoubleKeyFrame keyFramemille;
        private EasingDoubleKeyFrame keyFramedixmille;
        public altimetre()
        {
            InitializeComponent();
            this.DataContext = this;

            sb = Grd.Resources["rotatecent"] as Storyboard;
            sb1000 = Grd.Resources["rotatemille"] as Storyboard;
            sb10000 = Grd.Resources["rotatedixmille"] as Storyboard;
            keyFramecent = (sb.Children[0]
              as DoubleAnimationUsingKeyFrames).KeyFrames[0]
              as EasingDoubleKeyFrame;
            keyFramemille = (sb1000.Children[0]
               as DoubleAnimationUsingKeyFrames).KeyFrames[0]
               as EasingDoubleKeyFrame;
            keyFramedixmille = (sb10000.Children[0]
               as DoubleAnimationUsingKeyFrames).KeyFrames[0]
               as EasingDoubleKeyFrame;

            keyFramecent.Value = 0;
        }

        public double Anglecent { get; set; }
        public double Anglemille { get; set; }
        public double Angledixmille { get; set; }
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

           

            // pour 1 c'est 36 degré
            Anglecent =36* Altitude/100 ;
            Anglemille = Anglecent / 10;
            Angledixmille = Anglecent / 100;

             if (keyFramecent != null)
            {
                keyFramecent.Value = Anglecent;
                sb.Begin();
            }
            if (keyFramemille != null)
            {
                keyFramemille.Value = Anglemille;
                sb1000.Begin();
            }
            if (keyFramedixmille != null)
            {
                keyFramedixmille.Value = Angledixmille;
                sb10000.Begin();
            }

        }

        [Category("Communes")]
        public double Altitude
        {
            get
            {

                return (double)GetValue(ValueProperty);
            }
            set
            {

                SetValue(ValueProperty, value);
            }
        }

        public static readonly DependencyProperty ValueProperty =
         DependencyProperty.Register("Altitude", typeof(double),
        typeof(altimetre), new PropertyMetadata(null));


    }
}
