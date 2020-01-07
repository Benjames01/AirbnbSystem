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

        private int itemType = 0;

        private object obj;

        private string districtName;

        private AirbnbMain instance = AirbnbMain.GetInstance();

        public AddItemPage()
        {
        }

        public AddItemPage(int itemType)
        {
            InitializeComponent();
            LoadData(itemType);

            this.itemType = itemType;

            propertyFormTxts = new TextBox[]
            {
                propertyIdTxt,          // 0
                propertyNameTxt,        // 1
                propertyLatitudeTxt,    // 2
                propertyLongitudeTxt,   // 3
                propertyPriceTxt,       // 4
                propertyMinNightsTxt,   // 5
                hostIdTxt,              // 6
                hostNameTxt,            // 7
                numHostPropertiesTxt,   // 8
                daysAvailableTxt,       // 9
                roomTypeTxt             // 10
            };
        }

        public void SetDistrictName(string name)
        {
            districtName = name;
        }

        public void AddObj<T>(T obj)
        {
            itemTypeComboBox.Visibility = Visibility.Hidden;
            saveItemBtn.Visibility = Visibility.Visible;
            districtLbl.Visibility = Visibility.Hidden;
            districtComboBox.Visibility = Visibility.Hidden;

            this.obj = obj;

            if (itemType == 0)
            {
                itemNameTxt.Text = ((District)this.obj).GetName();
            }
            else if (itemType == 1)
            {
                itemNameTxt.Text = ((Neighbourhood)this.obj).GetName();
            }
            else if (itemType == 2)
            {
                propertySaveBtn.Visibility = Visibility.Visible;

                propertyDistrictLbl.Visibility = Visibility.Hidden;
                propertyNeighbourLbl.Visibility = Visibility.Hidden;
                propertyDistrictCombo.Visibility = Visibility.Hidden;
                propertyNeighbourhoodCombo.Visibility = Visibility.Hidden;

                Dispatcher.BeginInvoke((Action)(() => tabControl.SelectedIndex = 1));

                propertyFormTxts[0].Text = ((Property)this.obj).GetId();
                propertyFormTxts[1].Text = ((Property)this.obj).GetName();

                propertyFormTxts[6].Text = ((Property)this.obj).GetHostId();
                propertyFormTxts[7].Text = ((Property)this.obj).GetHostName();

                propertyFormTxts[5].Text = ((Property)this.obj).GetMinNightsStay().ToString(); // if parsed
                propertyFormTxts[2].Text = ((Property)this.obj).GetLatitude().ToString();
                propertyFormTxts[3].Text = ((Property)this.obj).GetLongitude().ToString();
                propertyFormTxts[8].Text = ((Property)this.obj).GetNumHostProperties().ToString();
                propertyFormTxts[9].Text = ((Property)this.obj).GetAvailableDaysPerYear().ToString();
                propertyFormTxts[4].Text = ((Property)this.obj).GetPrice().ToString();

                propertyFormTxts[10].Text = ((Property)this.obj).GetRoomType();
            }
        }

        private bool SaveDistrict()
        {
            if (itemNameTxt.Text.Equals("") && itemNameTxt.Text != null)
            {
                MessageBox.Show("Name must not be empty!");
                return false;
            }
            else
            {
                ((District)obj).SetName(itemNameTxt.Text);
                return true;
            }
        }

        private bool SaveNeighbourhood()
        {
            if (itemNameTxt.Text.Length == 0 && itemNameTxt.Text != null)
            {
                MessageBox.Show("Name must not be empty!");
                return false;
            }
            else
            {
                ((Neighbourhood)obj).SetName(itemNameTxt.Text);
                return true;
            }
        }

        private bool SaveProperty()
        {
            bool parsed = true;

            // For every property get the relevant text box and check if valid
            for (int i = 0; i < propertyFormTxts.Length; i++)
            {
                TextBox text = propertyFormTxts[i];

                // If not valid send an error message and return false
                if (!CheckValid(text))
                {
                    MessageBox.Show("Please make sure all fields are filled out!");
                    return false;
                }
            }

            // Assign property variables to correct data types from strings
            float latitude = DataUtils.StringToFloat(propertyFormTxts[2].Text);
            float longitude = DataUtils.StringToFloat(propertyFormTxts[3].Text);
            int price = DataUtils.StringToInt(propertyFormTxts[4].Text);
            int minNightsStay = DataUtils.StringToInt(propertyFormTxts[5].Text);
            int numProperty = DataUtils.StringToInt(propertyFormTxts[8].Text);
            int daysAvailable = DataUtils.StringToInt(propertyFormTxts[9].Text);

            // Parse all of the data to make sure its valid
            parsed = (ValidateFloat(latitude, "Please enter a float value above or below 0 for latitude!") &&
                 ValidateFloat(longitude, "Please enter a float value above or below 0 for longitude!") &&
                 ValidateInt(price, "Please enter an integer value above 0 for price!") &&
                 ValidateInt(minNightsStay, "Please enter an integer value above 0 for min nights stay!") &&
                 ValidateInt(numProperty, "Please enter an integer value above 0 for host properties!") &&
                 ValidateInt(daysAvailable, "Please enter an integer value above 0 and below 366 for days available!")) ? true : false;

            // Data Validation

            // 366 for leap year
            if (daysAvailable > 366 || daysAvailable < 0)
            {
                MessageBox.Show("Days available must be between 0 and 366");
                return false;
            }
            // 366 for leap year
            if (minNightsStay > 366 || minNightsStay < 0)
            {
                MessageBox.Show("Minimum nights stay must be between 0 and 366");
                return false;
            }

            // If parsed, safe to set the properties of the object
            if (parsed)
            {
                ((Property)obj).SetId(propertyFormTxts[0].Text);
                ((Property)obj).SetName(propertyFormTxts[1].Text);

                ((Property)obj).SetHostId(propertyFormTxts[6].Text);
                ((Property)obj).SetHostName(propertyFormTxts[7].Text);

                ((Property)obj).SetMinNightsStay(minNightsStay); // if parsed
                ((Property)obj).SetLatitude(latitude); // if parsed
                ((Property)obj).SetLongitude(longitude); //if parsed
                ((Property)obj).SetNumHostProperties(numProperty); //if parsed
                ((Property)obj).SetAvailableDaysPerYear(daysAvailable); //if parsed
                ((Property)obj).SetPrice(price); // if parsed

                ((Property)obj).SetRoomType(propertyFormTxts[10].Text);
                return true;
            }
            return false;
        }

        private void SaveItemBtn_Click(object sender, RoutedEventArgs e)
        {
            bool saved = false;
            if (itemType == 0) // District
            {
                saved = SaveDistrict();
            }
            else if (itemType == 1) // District
            {
                saved = SaveNeighbourhood();
            }
            else if (itemType == 2) // District
            {
                saved = SaveProperty();
            }

            // If function ran successfully ('returned true') then safe to save data
            if (saved)
            {
                instance.SaveData();

                // User confirmation
                MessageBox.Show("Successfully Saved!");

                // Change frame back to the search page
                instance.GetAdminWindow().ChangeFrame(new SearchPage(instance.GetAdminWindow()));
            }
        }

        private void AddItemBtn_Click(object sender, RoutedEventArgs e)
        {
            // Check if string is not null or empty
            if (itemNameTxt.Text.Length != 0 && itemNameTxt.Text != null)
            {
                if (itemTypeComboBox.SelectedIndex == 0) // District
                {
                    DistrictAddButtonClick();
                }
                else if (itemTypeComboBox.SelectedIndex == 1) // Neighbourhood
                {
                    NeighbourhoodAddButtonClick();
                }

                // Save data
                instance.SaveData();
            }
            else
            {
                MessageBox.Show("Please enter a name before adding!");
            }
        }

        private void LoadData(int itemType)
        {
            // Set the combobox item source to our items, then change to relevant tab
            itemTypeComboBox.ItemsSource = items;
            itemTypeComboBox.SelectedIndex = itemType;

            ChangeTab(itemType);
        }

        private void NeighbourhoodAddButtonClick()
        {
            Neighbourhood neighbourhood = new Neighbourhood();
            neighbourhood.SetName(itemNameTxt.Text);

            if (districtComboBox.SelectedItem != null)
            {
                ((District)districtComboBox.SelectedItem).AddNeighbourhood(neighbourhood);

                ((District)districtComboBox.SelectedItem).SetNumInCollection(((District)districtComboBox.SelectedItem).GetNumInCollection() + 1);

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

            instance.AddDistrict(district);
            MessageBox.Show("Successfully added item!");
        }

        private void ChangeTab(int itemType)
        {
            switch (itemType)
            {
                case (0): // District
                    Dispatcher.BeginInvoke((Action)(() => tabControl.SelectedIndex = 0));
                    // Hide irrelevant ui elements
                    districtComboBox.Visibility = Visibility.Hidden;
                    propertyDistrictLbl.Visibility = Visibility.Hidden;

                    break;

                case (1): // Neighbourhood
                    Dispatcher.BeginInvoke((Action)(() => tabControl.SelectedIndex = 0));
                    // Set item source to our districts
                    districtComboBox.ItemsSource = instance.GetDistricts();
                    // Enable relevant ui elements
                    districtComboBox.Visibility = Visibility.Visible;
                    districtLbl.Visibility = Visibility.Visible;

                    break;

                case (2): // Property
                    Dispatcher.BeginInvoke((Action)(() => tabControl.SelectedIndex = 1));
                    // Set item source to our districts
                    propertyDistrictCombo.ItemsSource = instance.GetDistricts();
                    // Clear selections
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
            // If a district is selected add its neighbourhoods to the item source for the neighbourhood combobox
            if (propertyDistrictCombo.SelectedItem != null)
            {
                propertyNeighbourhoodCombo.ItemsSource = ((District)propertyDistrictCombo.SelectedItem).GetNeighbourhoods();
            }
        }

        // Same as saving except creating a new property and adding to existing property array
        private void PropertyAddButtonClick(object sender, RoutedEventArgs e)
        {
            // Data validation - make sure a property and neighbourhood are selected
            if (propertyDistrictCombo.SelectedItem == null || propertyNeighbourhoodCombo.SelectedItem == null)
            {
                MessageBox.Show("Please select a district and neighbourhood before adding a property!");
                return;
            }
            else
            {
                bool parsed = false;
                Property property = new Property();

                for (int i = 0; i < propertyFormTxts.Length; i++)
                {
                    TextBox text = propertyFormTxts[i];

                    if (!CheckValid(text))
                    {
                        MessageBox.Show("Please make sure all fields are filled out!");
                        return;
                    }
                }

                float latitude = DataUtils.StringToFloat(propertyFormTxts[2].Text);
                float longitude = DataUtils.StringToFloat(propertyFormTxts[3].Text);
                int price = DataUtils.StringToInt(propertyFormTxts[4].Text);
                int minNightsStay = DataUtils.StringToInt(propertyFormTxts[5].Text);
                int numProperty = DataUtils.StringToInt(propertyFormTxts[8].Text);
                int daysAvailable = DataUtils.StringToInt(propertyFormTxts[9].Text);

                parsed = (ValidateFloat(latitude, "Please enter a float value above or below 0 for latitude!") &&
                     ValidateFloat(longitude, "Please enter a float value above or below 0 for longitude!") &&
                     ValidateInt(price, "Please enter an integer value above 0 for price!") &&
                     ValidateInt(minNightsStay, "Please enter an integer value above 0 for min nights stay!") &&
                     ValidateInt(numProperty, "Please enter a valid number above 0 for host properties!") &&
                     ValidateInt(daysAvailable, "Please enter a valid number above 0 for days available!")) ? true : false;

                // 366 for leap year
                if (daysAvailable > 366 || daysAvailable < 0)
                {
                    MessageBox.Show("Days available must be between 0 and 366");
                    return;
                }
                if (minNightsStay > 366 || minNightsStay < 0)
                {
                    MessageBox.Show("Minimum nights stay must be between 0 and 366");
                    return;
                }

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

                    District district = DataUtils.FindDistrictFromName(instance.GetDistricts(), propertyDistrictCombo.Text);

                    district.GetNeighbourhood(propertyNeighbourhoodCombo.SelectedIndex).AddProperty(property);
                    district.GetNeighbourhood(propertyNeighbourhoodCombo.SelectedIndex)
                        .SetNumInCollection(district.GetNeighbourhood(propertyNeighbourhoodCombo.SelectedIndex).GetNumInCollection() + 1);

                    instance.SetDistrictAt(district, propertyDistrictCombo.SelectedIndex);

                    instance.SaveData();

                    MessageBox.Show("Successfully Added!");

                    for (int i = 0; i < propertyFormTxts.Length; i++)
                    {
                        propertyFormTxts[i].Text = null;
                    }
                    propertyDistrictCombo.SelectedItem = null;
                    propertyNeighbourhoodCombo.ItemsSource = null;

                    AdminWindow adminWindow = instance.GetAdminWindow();

                    adminWindow.ChangeFrame(new SearchPage(adminWindow));
                }
            }
        }

        private bool ValidateInt(int i, string errorMessage)
        {
            if (i <= 0)
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
            if (!String.IsNullOrWhiteSpace(text.Text))
            {
                return true;
            }
            return false;
        }
    }
}