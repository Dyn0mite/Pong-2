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
    class BuildPlayfield
    {
        // Felder
        bool gameStartet = false;
        public Canvas theCanvas = new Canvas();
        public Ellipse ball = new Ellipse();
        public Rectangle rec1 = new Rectangle();
        public Rectangle rec2 = new Rectangle();
        public Label lblScoreP1 = new Label();
        public Label lblScoreP2 = new Label();

        // Eigenschaften
        public bool GameStartet
        {
            get { return gameStartet; }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="grid">Das XAML-Grid</param>
        public BuildPlayfield(Grid grid)
        {
            // Die Leinwand
            theCanvas.Name = "theCanvas";
            theCanvas.Height = 350;
            theCanvas.Width = 490;
            theCanvas.HorizontalAlignment = HorizontalAlignment.Stretch;
            theCanvas.VerticalAlignment = VerticalAlignment.Bottom;
            
            grid.Children.Add(theCanvas);

            // Der Ball
            ball.Name = "eBall";
            ball.Height = 20;
            ball.Width = 20;
            ball.Stroke = Brushes.Black;
            ball.Fill = Brushes.Black;
            Canvas.SetTop(ball, 200);
            Canvas.SetLeft(ball, 200);

            theCanvas.Children.Add(ball);

            // Die Paddel
            // Paddel 1
            rec1.Name = "rec1";
            rec1.Height = 80;
            rec1.Width = 10;
            rec1.Stroke = Brushes.Black;
            rec1.Fill = Brushes.Black;
            Canvas.SetTop(rec1, 135);
            Canvas.SetLeft(rec1, 20);
            
            theCanvas.Children.Add(rec1);

            // Paddel 2
            rec2.Name = "rec2";
            rec2.Height = 80;
            rec2.Width = 10;
            rec2.Stroke = Brushes.Black;
            rec2.Fill = Brushes.Black;
            Canvas.SetTop(rec2, 135);
            Canvas.SetLeft(rec2, 460);

            theCanvas.Children.Add(rec2);

            // Punkteanzeige Spieler 1
            lblScoreP1.Name = "lblScoreP1";
            lblScoreP1.Content = 0;
            lblScoreP1.FontWeight = FontWeights.Bold;
            lblScoreP1.FontSize = 20;
            Canvas.SetTop(lblScoreP1, 10);
            Canvas.SetLeft(lblScoreP1, 100);

            theCanvas.Children.Add(lblScoreP1);

            // Punkteanzeige Spieler 2
            lblScoreP2.Name = "lblScoreP2";
            lblScoreP2.Content = 0;
            lblScoreP2.FontWeight = FontWeights.Bold;
            lblScoreP2.FontSize = 20;
            Canvas.SetTop(lblScoreP2, 10);
            Canvas.SetRight(lblScoreP2, 100);

            theCanvas.Children.Add(lblScoreP2);

            gameStartet = true;
        }
    }
}
