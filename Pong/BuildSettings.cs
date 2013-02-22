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

namespace Pong
{
    class BuildSettings
    {
        // Felder
        private Grid theGrid = new Grid();
        public static Label lblBallAcceleration = new Label();
        public static Label lblWinConditions = new Label();
        public static TextBox tBox = new TextBox();
        public static TextBox tBox2 = new TextBox();
        public static Button bOK = new Button();
        public static double vBall = 10.0;
        public static int points = 5;

        // Konstruktor
        public BuildSettings(Grid grid, Canvas canvas)
        {
            theGrid = grid;
            canvas.Children.Clear();
            theGrid.Children.Remove(BuildInfo.tb);

            // Das Label für die Ballgeschwindigkeit
            lblBallAcceleration.Name = "lblBallAcceleration";
            lblBallAcceleration.Height = 25;
            lblBallAcceleration.Content = "Ballgeschwindigkeit: (aktuell = "+ vBall +")";
            lblBallAcceleration.Margin = new Thickness(15, -155, 0, 0);

            theGrid.Children.Add(lblBallAcceleration);

            // Die Textbox für die Ballgeschwindigkeit
            tBox.Name = "tbInputBallAcceleration";
            tBox.Height = 20;
            tBox.Width = 50;
            tBox.Margin = new Thickness(0, -150, 0, 0);

            theGrid.Children.Add(tBox);

            // Das Label für die Siegbedingung
            lblWinConditions.Name = "lblWinConditions";
            lblWinConditions.Height = 25;
            lblWinConditions.Content = "Siegbedingung: (aktuell = " + points + ")";
            lblWinConditions.Margin = new Thickness(15, -105, 0, 0);

            theGrid.Children.Add(lblWinConditions);

            // Die Textbox für die Siegbedingung
            tBox2.Name = "tbInputWinConditions";
            tBox2.Height = 20;
            tBox2.Width = 50;
            tBox2.Margin = new Thickness(0, -100, 0, 0);

            theGrid.Children.Add(tBox2);

            // Der Button
            bOK.Name = "bOK";
            bOK.Content = "OK";
            bOK.Height = 30;
            bOK.Width = 50;
            bOK.Click += bOK_Click;

            theGrid.Children.Add(bOK);
        }

        void bOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tBox.Text != "" || tBox.Text != "0")
                {
                    vBall = Convert.ToDouble(tBox.Text);
                }
            }
            catch (Exception)
            {
                vBall = 10.0;
            }

            try
            {
                if (tBox2.Text != "" || tBox2.Text != "0")
                {
                    points = Convert.ToInt16(tBox2.Text);
                }
            }
            catch (Exception)
            {
                points = 5;
            }
            
            theGrid.Children.RemoveRange(1, 5);

        }
    }
}
