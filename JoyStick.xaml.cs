using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace dronePatroller
{
    /// <summary>
    /// Logique d'interaction pour JoyStick.xaml
    /// </summary>
    public partial class JoyStick : UserControl
    {
        public JoyStick()
        {
            InitializeComponent();
        }

        Point posjoy;
        Point pointcurrent;
        bool DrapthJoy = false;
        double MovX_Val = 0;
        double MovY_Val = 0;
        double MovYZoom_Val = 0;
        bool rotate = false;
        private void Joy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse el = (Ellipse)sender;
            rotate = true;
            posjoy = e.GetPosition(svg8);
            pointcurrent = e.GetPosition(svg8);

        }
        private void svg8_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed && rotate == true)
            {
                pointcurrent = e.GetPosition(svg8);
                deplace.X = pointcurrent.X - posjoy.X;
                deplace.Y = pointcurrent.Y - posjoy.Y;
                MovX_Val = pointcurrent.X - posjoy.X;
                MovY_Val = pointcurrent.Y - posjoy.Y;


                if (MovX_Val > 45 || MovX_Val < -45 || MovY_Val > 45 || MovY_Val < -45)
                {
                    rotate = false;
                }
               

                if (!DrapthJoy)
                {
                    DrapthJoy = true;
                    Thread Thread_MovCam = new Thread(() => movCam());
                    Thread_MovCam.Priority = ThreadPriority.Lowest;
                    Thread_MovCam.Start();


                }


            }
        }
        public event Action<String> PositionJoy;

           
  
    private void movCam()
        {

            while (DrapthJoy)
            {
                Thread.Sleep(100);
                this.Dispatcher.BeginInvoke(new Action(delegate
                {
                    if (PositionJoy != null)
                    {
                        PositionJoy("X;" + MovX_Val.ToString("0") + ";Y;" + MovY_Val.ToString("0") + ";Z;" + MovYZoom_Val.ToString());
                    }

  
                }));
            }
        }

        private void Joy_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            deplace.X = 0;
            deplace.Y = 0;
            MovX_Val = 0;
            MovY_Val = 0;
            rotate = false;
            DrapthJoy = false;
            Thread Thread_MovCam = new Thread(() => movCam());
            //chx priorité
            Thread_MovCam.Priority = ThreadPriority.Lowest;
            Thread_MovCam.Start();
        }
        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            Joy_MouseLeftButtonUp(null, null);

        }

        private void JoyZoom_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            deplaceZoom.Y = 0;
            MovYZoom_Val = 0;
            rotate =false;
            DrapthJoy = false;
            Thread Thread_MovCam = new Thread(() => movCam());
            Thread_MovCam.Priority = ThreadPriority.Lowest;
            Thread_MovCam.Start();

        }

        private void Curs_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && rotate == true)
            {
                pointcurrentZoom = e.GetPosition(GridZoom);
                deplaceZoom.Y = pointcurrentZoom.Y - posjoyZoom.Y;
                MovYZoom_Val = (pointcurrentZoom.Y - posjoyZoom.Y)*-1;


                if (MovYZoom_Val > 99 || MovYZoom_Val < -99)
                {
                    rotate = false;
                }

            }
            if (!DrapthJoy)
            {
                DrapthJoy = true;
                Thread Thread_MovCam = new Thread(() => movCam());
                Thread_MovCam.Priority = ThreadPriority.Lowest;
                Thread_MovCam.Start();


            }


        }

        Point posjoyZoom;
        Point pointcurrentZoom;

        private void JoyZoom_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse el = (Ellipse)sender;
            rotate = true;
            posjoyZoom = e.GetPosition(GridZoom);
            pointcurrentZoom = e.GetPosition(GridZoom);

        }

        private void Joy_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Ellipse)(sender)).Opacity = 1;
        }

        private void Joy_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Ellipse)(sender)).Opacity = .3;
            JoyZoom_MouseLeftButtonUp(null, null);
        }

    }
}
