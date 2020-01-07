using AirBnbSystem.Airbnb;
using AirBnbSystem.Airbnb.Models;
using AirBnbSystem.Airbnb.Utils;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using static AirBnbSystem.Airbnb.Utils.DataUtils;

namespace AirBnbSystem
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AnalyticsWindow : Window
    {
        private MainWindow mainWindow;

        public AnalyticsWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

            // Set combobox item source to the districts from the AirbnbMain instance
            districtComboBox.ItemsSource = AirbnbMain.GetInstance().GetDistricts();
        }

        private void DragBar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Let the user drag the window
            this.DragMove();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            // Show the main menu screen and then close this window
            mainWindow.Show();
            this.Close();
        }

        private void districtComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If an item isn't selected update labels to zero
            if ((District)districtComboBox.SelectedItem == null)
            {
                avgPriceLbl.Content = "0.00";
                avgAvailabilityLbl.Content = "0.00";
                numberNeighbourhoodseLbl.Content = "0";
                avgPropPerHostLbl.Content = "0.0";

                return;
            }

            // Update the analytics information
            this.Dispatcher.Invoke(() =>
            {
                District district = (District)districtComboBox.SelectedItem;
                avgPropPerHostLbl.Content = DataUtils.GetDistrictAverageHostProperties(district).ToString("n1");
                numberNeighbourhoodseLbl.Content = district.GetNumInCollection();
                avgPriceLbl.Content = DataUtils.GetDistrictAveragePropertyPrice(district).ToString("n2");
                avgAvailabilityLbl.Content = DataUtils.GetDistrictAverageAvailablity(district).ToString("n1");
                DrawGraph();
            });
        }

        private void DrawGraph()
        {
            numPropertiesToPriceGraph.Children.Clear();

            // Settings for the graph
            const double margins = 10;

            double xMinimum = margins;
            double xMaximum = numPropertiesToPriceGraph.Width - margins;

            double yMinimum = margins;
            double yMaximum = numPropertiesToPriceGraph.Height - margins;

            const double stepUp = 10;

            // Setting up the x axis for the graph
            GeometryGroup xAxisGeom = new GeometryGroup();
            xAxisGeom.Children.Add(
                new LineGeometry(
                new Point(0, yMinimum),
                new Point(numPropertiesToPriceGraph.Width, yMinimum)));

            Path xAxisPath = new Path
            {
                StrokeThickness = 1,
                Stroke = Brushes.Gray,
                Data = xAxisGeom
            };

            // Add the x axis to the geometry group
            numPropertiesToPriceGraph.Children.Add(xAxisPath);

            // Setting up the y axis
            GeometryGroup yAxisGeom = new GeometryGroup();
            yAxisGeom.Children.Add(
                new LineGeometry(
                new Point(margins, 0),
                new Point(margins, numPropertiesToPriceGraph.Height)));

            for (double y = stepUp; y <= numPropertiesToPriceGraph.Height - stepUp; y += stepUp)
            {
                TextBlock text = new TextBlock
                {
                    Text = y.ToString(),
                    FontSize = 10,
                    Foreground = new SolidColorBrush(Colors.Black)
                };

                Canvas.SetLeft(text, margins - 20);
                Canvas.SetTop(text, y - stepUp);

                numPropertiesToPriceGraph.Children.Add(text);

                yAxisGeom.Children.Add(
                    new LineGeometry(
                    new Point(margins - 20 / 2, y),
                    new Point(margins + 20 / 2, y)));
            }

            Path yAxisPath = new Path
            {
                StrokeThickness = 1,
                Stroke = Brushes.Gray,
                Data = yAxisGeom
            };

            numPropertiesToPriceGraph.Children.Add(yAxisPath);

            GraphData[] dataSet = DataUtils.GetGraphData((District)districtComboBox.SelectedItem);

            double width = (xMaximum - xMinimum) / dataSet.Length;
            for (int i = 0; i < dataSet.Length; i++)
            {
                PointCollection points = new PointCollection();

                int yTop = (int)dataSet[i].averagePrice;
                if (yTop < yMinimum) yTop = (int)yMinimum + 8;
                if (yTop > yMaximum) yTop = (int)yMaximum - 8;

                int yBottom = yTop - 8;

                TextBlock text = new TextBlock
                {
                    Text = DataUtils.GetStartingCharactersOfString(dataSet[i].name),
                    FontSize = 8,
                    Foreground = new SolidColorBrush(Colors.Black)
                };

                double y = -stepUp;

                if (i % 2 == 0 && i % 3 != 0)
                {
                    y -= stepUp * 2;
                }
                else if (i % 3 == 0)
                {
                    y -= stepUp;
                }

                Canvas.SetLeft(text, i * width + xMinimum);
                Canvas.SetTop(text, y);

                numPropertiesToPriceGraph.Children.Add(text);

                points.Add(new Point(i * width + xMinimum + 15, yTop));
                points.Add(new Point(i * width + xMinimum + 15, yBottom));

                Polyline polyline = new Polyline
                {
                    StrokeThickness = 1.5,
                    Stroke = Brushes.Black,
                    Points = points
                };

                numPropertiesToPriceGraph.Children.Add(polyline);
            }
        }
    }
}