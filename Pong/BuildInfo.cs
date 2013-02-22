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
    class BuildInfo
    {
        // Felder
        public static TextBlock tb = new TextBlock();
        
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="grid">Das XAML-Grid</param>
        /// <param name="canvas">Die Leinwand</param>
        public BuildInfo(Grid grid, Canvas canvas)
        {
            canvas.Children.Clear();
            grid.Children.Remove(tb);
            grid.Children.Remove(BuildSettings.lblBallAcceleration);
            grid.Children.Remove(BuildSettings.lblWinConditions);
            grid.Children.Remove(BuildSettings.tBox);
            grid.Children.Remove(BuildSettings.tBox2);
            grid.Children.Remove(BuildSettings.bOK);
            
            // Der Textblock
            tb.VerticalAlignment = VerticalAlignment.Center;
            tb.HorizontalAlignment = HorizontalAlignment.Center;
            tb.FontSize = 20;
            tb.TextAlignment = TextAlignment.Center;
            tb.Text = "Steuerung\n\nlinkes Paddel:\nHoch - W\nRunter - S\n\nrechtes Paddel:\nHoch - Pfeil nach oben\nRunter - Pfeil nach unten";
                        
            grid.Children.Add(tb);
        }
    }
}
