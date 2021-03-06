﻿using AirBnbSystem.Airbnb;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Input;

namespace AirBnbSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectDataFileBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "All files (*.*) | *.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                dataFileTxt.Text = openFileDialog.FileName;
            }
        }

        private void DragBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void LoadDataFileBtn_Click(object sender, RoutedEventArgs e)
        {
            string dataFileName = dataFileTxt.Text;

            if (dataFileName == null || dataFileName.Length == 0)
            {
                MessageBox.Show("Please select a file before loading");
            }
            else
            {
                AirbnbMain.GetInstance().SetFileName(dataFileName);

                if (AirbnbMain.GetInstance().LoadData())
                {
                    MessageBox.Show("Successfully Loaded data.");
                    adminBtn.Visibility = Visibility.Visible;
                    analyticsBtn.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Unable to load data, check data file is correct");
                }
            }
        }

        private void AdminBtn_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow(this);
            adminWindow.Show();

            this.Hide();
        }

        private void analyticsBtn_Click(object sender, RoutedEventArgs e)
        {
            AnalyticsWindow analyticsWindow = new AnalyticsWindow(this);
            analyticsWindow.Show();

            this.Hide();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}