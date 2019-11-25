using AirBnbSystem.Airbnb.Models;
using AirBnbSystem.Airbnb.Utils;
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

        private TextBox[] propertyFormTxts;


        public AddItemPage(int itemType)
        {
            InitializeComponent();
            LoadData(itemType);

            propertyFormTxts = new TextBox[]
            {
                propertyIdTxt,
                propertyNameTxt,
                propertyLatitudeTxt,
                propertyLongitudeTxt,
                propertyPriceTxt,
                propertyMinNightsTxt,
                hostIdTxt,
                hostNameTxt,
                numHostPropertiesTxt,
                daysAvailableTxt,
                roomTypeTxt
            };
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

            if (districtComboBox.SelectedItem != null)
            {
                ((District)districtComboBox.SelectedItem).AddNeighbourhood(neighbourhood);

                itemNameTxt.Text = null;
                districtComboBox.SelectedItem = null;

                MessageBox.Show("Successfully added item!");
            }
            else
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
            if (propertyDistrictCombo.SelectedItem != null)
            {
                propertyNeighbourhoodCombo.ItemsSource = ((District)propertyDistrictCombo.SelectedItem).GetNeighbourhoods();
            }
        }


        private void PropertyAddButtonClick(object sender, RoutedEventArgs e)
        {
            if (propertyDistrictCombo.SelectedItem == null || propertyNeighbourhoodCombo.SelectedItem == null)
            {
                MessageBox.Show("Please select a district and neighbourhood before adding a property!");
            }
            else
            {
                bool parsed = true;
                Property property = new Property();

                for(int i = 0; i < propertyFormTxts.Length; i++)
                {
                    TextBox text = propertyFormTxts[i];

                    if (!CheckValid(text))
                    {
                        MessageBox.Show("Please make sure all fields are filled out!");
                        return;
                    }
                }

                float latitude = DataUtils.StringToFloat(propertyFormTxts[2].Text);
                parsed = ValidateFloat(latitude, "Please enter a float value above or below 0 for latitude!");
                float longitude = DataUtils.StringToFloat(propertyFormTxts[3].Text);
                parsed = ValidateFloat(latitude, "Please enter a float value above or below 0 for longitude!");
                int price = DataUtils.StringToInt(propertyFormTxts[4].Text);
                parsed = ValidateInt(price, "Please enter an integer value above 0 for price!");

                int minNightsStay = DataUtils.StringToInt(propertyFormTxts[5].Text);
                parsed = ValidateInt(minNightsStay, "Please enter an integer value above 0 for min nights stay!");

                int numProperty = DataUtils.StringToInt(propertyFormTxts[8].Text);
                parsed = ValidateInt(numProperty, "Please enter a valid number above 0 for host properties!");

                int daysAvailable = DataUtils.StringToInt(propertyFormTxts[9].Text);
                parsed = ValidateInt(daysAvailable, "Please enter a valid number above 0 for days available!");              

                if (parsed)
                {
                    property.SetId(propertyFormTxts[0].Text);
                    property.SetName(propertyFormTxts[1].Text);

                    property.SetHostId(propertyFormTxts[6].Text);
                    property.SetHostName(propertyFormTxts[7].Text);

                    property.SetMinNightsStay(minNightsStay); // if parsed
                    property.SetLatitude(latitude); // if parsed
                    property.SetLongitude(longitude); //if parsed
                    property.SetNumHostProperties(numProperty); //if parsed
                    property.SetAvailableDaysPerYear(daysAvailable); //if parsed
                    property.SetPrice(price); // if parsed

                    property.SetRoomType(propertyFormTxts[10].Text);

                    Neighbourhood neighbourhood = (Neighbourhood)(propertyNeighbourhoodCombo.SelectedItem);
                    neighbourhood.AddProperty(property);

                    ((District)propertyDistrictCombo.SelectedItem).SetNeighbourhood(neighbourhood, propertyNeighbourhoodCombo.SelectedIndex);

                    MessageBox.Show("Successfully Added!");

                    for (int i = 0; i < propertyFormTxts.Length; i++)
                    {
                        propertyFormTxts[i].Text = null;
                    }
                    propertyDistrictCombo.SelectedItem = null;
                    propertyNeighbourhoodCombo.ItemsSource = null;
                }


            }
        }

        private bool ValidateInt(int i, string errorMessage)
        {
            if(i <= 0)
            {
                MessageBox.Show(errorMessage);
                return false;
            }
            return true;
        }

        private bool ValidateFloat(float i, string errorMessage)
        {
            if (i != 0f)
            {             
                return true;
            }

            MessageBox.Show(errorMessage);
            return false;
        }

        private bool CheckValid(TextBox text)
        {
            if(!String.IsNullOrWhiteSpace(text.Text))
            {
                return true;
            }
            return false;
        }
    }
}