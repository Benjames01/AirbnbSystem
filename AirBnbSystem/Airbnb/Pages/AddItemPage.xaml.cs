using AirBnbSystem.Airbnb.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AirBnbSystem.Airbnb.Pages
{
    /// <summary>
    /// Interaction logic for AddItemPage.xaml
    /// </summary>
    public partial class AddItemPage : Page
    {
        private string[] items = new string[]
        {
            "District",
            "Neighbourhood",
            "Property"
        };

        public AddItemPage(int itemType)
        {
            InitializeComponent();
            LoadData(itemType);
        }

        private void LoadData(int itemType)
        {
            itemTypeComboBox.ItemsSource = items;
            itemTypeComboBox.SelectedIndex = itemType;

            ChangeTab(itemType);
        }

        private void AddItemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!itemNameTxt.Text.Equals("") && itemNameTxt.Text != null)
            {
                if (itemTypeComboBox.SelectedIndex == 0)
                {
                    DistrictAddButtonClick();
                }
                else if (itemTypeComboBox.SelectedIndex == 1)
                {
                    NeighbourhoodAddButtonClick();
                }
            }
            else
            {
                MessageBox.Show("Please enter a name before adding!");
            }
        }

        private void NeighbourhoodAddButtonClick()
        {
            Neighbourhood neighbourhood = new Neighbourhood();
            neighbourhood.SetName(itemNameTxt.Text);
            
            if(districtComboBox.SelectedItem != null)
            {
                ((District)districtComboBox.SelectedItem).AddNeighbourhood(neighbourhood);

                itemNameTxt.Text = null;
                districtComboBox.SelectedItem = null;

                MessageBox.Show("Successfully added item!");
            } else
            {
                MessageBox.Show("Please select a district before adding neighbourhood!");
            }
           
        }

        private void DistrictAddButtonClick()
        {
            District district = new District();
            district.SetName(itemNameTxt.Text);

            itemNameTxt.Text = null;

            AirbnbMain.GetInstance().AddDistrict(district);

            MessageBox.Show("Successfully added item!");
        }

        private void ChangeTab(int itemType)
        {
            switch (itemType)
            {
                case (0): // District
                    Dispatcher.BeginInvoke((Action)(() => tabControl.SelectedIndex = 0));

                    districtComboBox.Visibility = Visibility.Hidden;
                    districtLbl.Visibility = Visibility.Hidden;

                    break;

                case (1): // Neighbourhood
                    Dispatcher.BeginInvoke((Action)(() => tabControl.SelectedIndex = 0));

                    districtComboBox.ItemsSource = AirbnbMain.GetInstance().GetDistricts();

                    districtComboBox.Visibility = Visibility.Visible;
                    districtLbl.Visibility = Visibility.Visible;

                    break;

                case (2): // Property
                    Dispatcher.BeginInvoke((Action)(() => tabControl.SelectedIndex = 1));

                    propertyDistrictCombo.ItemsSource = AirbnbMain.GetInstance().GetDistricts();

                    propertyDistrictCombo.SelectedItem = null;
                    propertyNeighbourhoodCombo.ItemsSource = null;

                    break;
            }
        }

        private void ItemTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeTab(itemTypeComboBox.SelectedIndex);
        }

        private void PropertyDistrictCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(propertyDistrictCombo.SelectedItem != null)
            {
                propertyNeighbourhoodCombo.ItemsSource = ((District)propertyDistrictCombo.SelectedItem).GetNeighbourhoods();
            }    
        }
    }
}