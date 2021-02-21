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

namespace AircraftInstrument
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void JoyStick_PositionJoy(string arg1)
        {
            this.Dispatcher.BeginInvoke(new Action(delegate
            {
                LbXpos.Content = arg1.Split(new string[] { ";" }, StringSplitOptions.None)[1];
                LbYpos.Content = arg1.Split(new string[] { ";" }, StringSplitOptions.None)[3];
                LbZpos.Content = arg1.Split(new string[] { ";" }, StringSplitOptions.None)[5];
                SdY_Altitude.Value = SdY_Altitude.Value + Convert.ToDouble( LbYpos.Content);
                SdY.Value= Convert.ToDouble(LbYpos.Content);

                Compass.Heading= Compass.Heading+ Convert.ToDouble(LbXpos.Content)/10;
                SdZ.Value = SdZ.Value + Convert.ToDouble(LbZpos.Content)/10;


            }));

        }
    }
}
