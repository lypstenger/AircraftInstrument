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
    /// Logique d'interaction pour variometre.xaml
    /// </summary>
    public partial class variometre : UserControl
    {
        private Storyboard sb;
        private EasingDoubleKeyFrame keyFrame;
        public variometre()
        {
            InitializeComponent();
            this.DataContext = this;

          sb = Grd.Resources["rotate"] as Storyboard;
            keyFrame = (sb.Children[0]
              as DoubleAnimationUsingKeyFrames).KeyFrames[0]
              as EasingDoubleKeyFrame;

            keyFrame.Value = 0;

        }

   

        public double Angle { get; set; }
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            
            
            Angle = DecifeetPm*8.585;

            if (Angle > 176) { Angle = 176; }
            if (Angle < -176) { Angle = -176; }

            if (keyFrame!=null)
            { 
            keyFrame.Value = Angle;
            sb.Begin();
            }

         }

        [Category("Communes")]
        public double DecifeetPm
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
         DependencyProperty.Register("DecifeetPm", typeof(double),
        typeof(variometre), new PropertyMetadata(null));


    }
}
