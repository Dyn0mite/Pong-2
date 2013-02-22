using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Pong
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Instanziieren der benötigten Objekte
        DispatcherTimer timer = new DispatcherTimer();
        Canvas theCanvas = new Canvas();
        Ellipse ball = new Ellipse();
        Rectangle rec1 = new Rectangle();
        Rectangle rec2 = new Rectangle();
        Label lblScoreP1 = new Label();
        Label lblScoreP2 = new Label();
        
        // Geschwindigkeits Variablen
        double v = 0.0;
        double vRec1 = 0.0;
        double vRec2 = 0.0;

        // Punktezähler
        int scoreP1 = 0;
        int scoreP2 = 0;

        bool p1Win;
        bool p2Win;
        
        // Richtung des Balls
        bool goingRight = true;
        bool goingDown = true;
        
        /// <summary>
        /// Konstruktor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            timer.Tick += physics;
            timer.Tick += WinHandler;
            timer.Interval = TimeSpan.FromSeconds(0.05);
            timer.IsEnabled = false;

            this.KeyDown += new KeyEventHandler(Key_KeyDown);
            this.KeyUp += new KeyEventHandler(Key_KeyUp);

            //miEinstellungen.IsEnabled = false;
        }

        /// <summary>
        /// Startbutton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miStart_Click(object sender, RoutedEventArgs e)
        {
            theCanvas.Children.Clear();
            theGrid.Children.Remove(BuildInfo.tb);
            theGrid.Children.Remove(BuildSettings.lblBallAcceleration);
            theGrid.Children.Remove(BuildSettings.lblWinConditions);
            theGrid.Children.Remove(BuildSettings.tBox);
            theGrid.Children.Remove(BuildSettings.tBox2);
            theGrid.Children.Remove(BuildSettings.bOK);

            timer.IsEnabled = false;

            BuildPlayfield pf = new BuildPlayfield(theGrid);
            this.theCanvas = pf.theCanvas;
            this.ball = pf.ball;
            this.rec1 = pf.rec1;
            this.rec2 = pf.rec2;
            this.lblScoreP1 = pf.lblScoreP1;
            this.lblScoreP2 = pf.lblScoreP2;
            this.v = BuildSettings.vBall;

            // zurücksetzen des Punktestands
            scoreP1 = 0;
            p1Win = false;
            scoreP2 = 0;
            p2Win = false;

            
            if (pf.GameStartet == true)
            {
                timer.IsEnabled = true;
            }
        }

        /// <summary>
        /// Beendenbutton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miBeenden_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Hilfebutton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miHilfe_Click(object sender, RoutedEventArgs e)
        {
            BuildInfo bi = new BuildInfo(theGrid, theCanvas);
            timer.IsEnabled = false;
        }

        /// <summary>
        /// Einstellungsbutton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miEinstellungen_Click(object sender, RoutedEventArgs e)
        {
            BuildSettings bs = new BuildSettings(theGrid, theCanvas);
            timer.IsEnabled = false;
        }
        
        /// <summary>
        /// Physics - Position des Balls, Bewegung der Paddel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void physics(object sender, EventArgs e)
        {
            // Ball geht nach unten und stößt vom Rand ab
            double y = Canvas.GetTop(ball);
            if (goingDown)
            {
                y += v;
            }
            else
            {
                y -= v;
            }

            if (y + ball.Height > theCanvas.ActualHeight)
            {
                goingDown = false;
                y = theCanvas.ActualHeight - ball.Height;
            }
            else if (y < 0.0)
            {
                goingDown = true;
                y = 0.0;
            }

            Canvas.SetTop(ball, y);

            // Ball geht nach rechts und stößt am Rand oder an einem Paddel ab
            double x = Canvas.GetLeft(ball);
            if (goingRight)
            {
                x += v;
            }
            else
            {
                x -= v;
            }

            if (x + ball.Width > theCanvas.ActualWidth)
            {
                goingRight = false;
                scoreP1++;
                lblScoreP1.Content = scoreP1;
                CheckWin(scoreP1, scoreP2);
                x = theCanvas.ActualWidth - ball.Width;
            }
            else if (x < 0.0)
            {
                goingRight = true;
                scoreP2++;
                CheckWin(scoreP1, scoreP2);
                lblScoreP2.Content = scoreP2;
                x = 0.0;
            }
            else if (CheckCollision(rec2, ball))
            {
                goingRight = false;
            }
            else if (CheckCollision(rec1, ball))
            {
                goingRight = true;
            }

            Canvas.SetLeft(ball, x);

            // Position für die beiden Paddel
            double yRec1 = Canvas.GetTop(rec1);
            if (yRec1 + vRec1 < 0.0)
            {
                yRec1 = 0.0;
            }
            else if (yRec1 + rec1.Height + vRec1 > theCanvas.ActualHeight)
            {
                yRec1 = theCanvas.ActualHeight - rec1.Height;
            }
            else
            {
                Canvas.SetTop(rec1, yRec1 + vRec1);
            }

            double yRec2 = Canvas.GetTop(rec2);
            if (yRec2 + vRec2 < 0.0)
            {
                yRec2 = 0.0;
            }
            else if (yRec2 + rec2.Height + vRec2 > theCanvas.ActualHeight)
            {
                yRec2 = theCanvas.ActualHeight - rec2.Height;
            }
            else
            {
                Canvas.SetTop(rec2, yRec2 + vRec2);
            }
        }

        /// <summary>
        /// Key_KeyDown - Event beim drücken einer Taste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Key_KeyDown(Object sender, KeyEventArgs e)
        {
            switch (e.Key)
	        {
                case Key.Down:
                    vRec2 = 5;
                    break;
                case Key.Up:
                    vRec2 = -5;
                    break;
                case Key.S:
                    vRec1 = 5;
                    break;
                case Key.W:
                    vRec1 = -5;
                    break;
                default:
                    break;
	        } 
        }

        /// <summary>
        /// Key_KeyUp - Event, beim loslassen einer Taste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Key_KeyUp(Object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    vRec2 = 0;
                    break;
                case Key.Up:
                    vRec2 = 0;
                    break;
                case Key.S:
                    vRec1 = 0;
                    break;
                case Key.W:
                    vRec1 = 0;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Kollisionsabfrage
        /// </summary>
        /// <param name="ctl1">erstes Framework Objekt</param>
        /// <param name="ctl2">zweites Framework Objekt</param>
        /// <returns>bool - true bei Kollision</returns>
        private bool CheckCollision(FrameworkElement ctl1, FrameworkElement ctl2)
        {
            bool retval = false;
            Point ptTopLeft = new Point(Convert.ToDouble(ctl1.GetValue(Canvas.LeftProperty)), Convert.ToDouble(ctl1.GetValue(Canvas.TopProperty)));
            Point ptBottomRight = new Point(Convert.ToDouble(ctl1.GetValue(Canvas.LeftProperty)) + ctl1.Width, Convert.ToDouble(ctl1.GetValue(Canvas.TopProperty)) + ctl1.Height);
            Rect r1 = new Rect(ptTopLeft, ptBottomRight);

            Point ptTopLeft2 = new Point(Convert.ToDouble(ctl2.GetValue(Canvas.LeftProperty)), Convert.ToDouble(ctl2.GetValue(Canvas.TopProperty)));
            Point ptBottomRight2 = new Point(Convert.ToDouble(ctl2.GetValue(Canvas.LeftProperty)) + ctl2.Width, Convert.ToDouble(ctl2.GetValue(Canvas.TopProperty)) + ctl2.Height);
            Rect r2 = new Rect(ptTopLeft2, ptBottomRight2);

            r1.Intersect(r2);
            if (!r1.IsEmpty)
            {
                retval = true;
            }
            return retval;
        }

        private void CheckWin(int scoreP1, int scoreP2)
        {
            if (scoreP1 == BuildSettings.points)
            {
                p1Win = true;
            }
            else if (scoreP2 == BuildSettings.points)
            {
                p2Win = true;
            }
        }

        private void WinHandler(object sender, EventArgs e)
        {
            if (p1Win)
            {
                MessageBox.Show("Der linke Spieler gewinnt!");
                timer.Stop();
            }
            else if (p2Win)
            {
                MessageBox.Show("Der rechte Spieler gewinnt!");
                timer.Stop();
            }
        }
    }
}
