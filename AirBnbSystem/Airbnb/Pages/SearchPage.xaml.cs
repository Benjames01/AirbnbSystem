using AirBnbSystem.Airbnb.Models;
using AirBnbSystem.Airbnb.Utils;
using System.Windows;
using System.Windows.Controls;

namespace AirBnbSystem.Airbnb.Pages
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {

        SearchType searchType;

        AdminWindow adminWindow;

        string[] propertyFilters = new string[] {
            "Property ID",
            "Property Name",
            "Host ID",
            "Host Name",
            "Number of Host's Properties",
            "Latitude",
            "Longitude",
            "Room Type",
            "Price",
            "Minimum Number of Nights",
            "Availability"
            };

        string[] defaultFilters = new string[] {
            "Name",
            "Number in collection"
        };


        public SearchPage(AdminWindow adminWindow)
        {
            this.adminWindow = adminWindow;
            InitializeComponent();
        }

        public enum SearchType
        {
            District,
            Neighbourhood,
            Property
        }

        private void RadioBtn_Checked(object sender, System.Windows.RoutedEventArgs e)
        {

            RadioButton[] searchTypeRadioBtns = new RadioButton[]
           {
                districtRadioBtn,
                neighbourRadioBtn,
                propertyRadioBtn
           };

            RadioButton button = (RadioButton)sender;

            int type = 0;

            searchType = SearchType.District;

            for (int i = 0; i < searchTypeRadioBtns.Length; i++)
            {

                if (button.Content.Equals(searchTypeRadioBtns[i].Content))
                {
                    searchTypeRadioBtns[i].IsChecked = true;
                    type = i;
                }
                else if(!button.Content.Equals(searchTypeRadioBtns[i].Content))
                {
                    searchTypeRadioBtns[i].IsChecked = false;
                }
            }

            switch (type)
            {
                case ((int)SearchType.District):
                    searchType = SearchType.District;
                    break;

                case ((int)SearchType.Neighbourhood):
                    searchType = SearchType.Neighbourhood;
                    break;

                case ((int)SearchType.Property):
                    searchType = SearchType.Property;
                    break;
            }

            UpdateFilters(searchType);
        }

        void EnablePropertySearch()
        {

            filterComboBox.ItemsSource = defaultFilters;

            districtComboBox.IsEnabled = true;
            neighbourComboBox.IsEnabled = true;
            propertyComboBox.IsEnabled = true;
        }

        void EnableNeighbourhoodSearch()
        {
            filterComboBox.ItemsSource = defaultFilters;

            districtComboBox.IsEnabled = true;
            neighbourComboBox.IsEnabled = true;
            propertyComboBox.IsEnabled = false;
        }

        void EnableDistrictSearch()
        {
            filterComboBox.ItemsSource = defaultFilters;

            districtComboBox.IsEnabled = true;
            districtComboBox.SelectedItem = null;

            neighbourComboBox.IsEnabled = false;
            propertyComboBox.IsEnabled = false;
            
        }

        private void UpdateFilters(SearchType searchType)
        {
            switch (searchType)
            {
                case (SearchType.Property):
                    EnablePropertySearch();
                    break;
                case (SearchType.District):
                    EnableDistrictSearch();
                    break;
                case (SearchType.Neighbourhood):
                    EnableNeighbourhoodSearch();
                    break;
            }

            District[] districts = AirbnbMain.GetInstance().GetDistricts();

            districtComboBox.ItemsSource = districts;
            resultsListBox.ItemsSource = districts;

            districtComboBox.SelectedItem = null;
            neighbourComboBox.ItemsSource = null;
            propertyComboBox.ItemsSource = null;

            resultsListBox.SelectedItem = null;
        }

        void DistrictComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(districtComboBox.SelectedItem != null)
            {
                filterComboBox.ItemsSource = defaultFilters;
                string name = ((District)districtComboBox.SelectedItem).GetName();

                Neighbourhood[] neighbourhoods = DataUtils.FindDistrictFromName(AirbnbMain.GetInstance().GetDistricts(), name).GetNeighbourhoods();

                neighbourComboBox.ItemsSource = neighbourhoods;

                if (searchType == SearchType.Neighbourhood || searchType == SearchType.Property )
                {
                    resultsListBox.ItemsSource = neighbourComboBox.ItemsSource;
                }
                else if (searchType == SearchType.District)
                {
                    resultsListBox.ItemsSource = new District[] { (District)districtComboBox.SelectedItem };
                }
            }
        }

        void NeighbourComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (neighbourComboBox.SelectedItem != null && districtComboBox.SelectedItem != null)
            {
                string name = ((District)districtComboBox.SelectedItem).GetName();
                string neighbourhoodName = ((Neighbourhood)neighbourComboBox.SelectedItem).GetName();

                if (searchType == SearchType.Property)
                {
                    propertyComboBox.ItemsSource = DataUtils.FindNeighbourhoodFromName(DataUtils
                    .FindDistrictFromName(AirbnbMain.GetInstance().GetDistricts(), name)
                    .GetNeighbourhoods(), neighbourhoodName).GetProperties();

                    resultsListBox.ItemsSource = propertyComboBox.ItemsSource;
                    filterComboBox.ItemsSource = propertyFilters;
                }
                else
                {
                    resultsListBox.ItemsSource = new Neighbourhood[]{(Neighbourhood)neighbourComboBox.SelectedItem};
                }
            }
        }
        private void PropertyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(searchType == SearchType.Property)
            {
                resultsListBox.ItemsSource = new Property[] { (Property)propertyComboBox.SelectedItem };
            }
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {

            string search;
            if(searchFieldTxt.Text != null && filterComboBox.SelectedItem != null)
            {
                if (filterComboBox.SelectedItem.ToString().Equals("Name")) {
                    search = searchFieldTxt.Text;
                    if (searchType == SearchType.District)
                    {
                        AirbnbCollection[] collection = (AirbnbCollection[])AirbnbMain.GetInstance().GetDistricts();

                        AirbnbCollection[] results = DataUtils.FindAirbnbCollectionsFromRegexName(collection, search);

                        if (results.Length > 0)
                        {
                            resultsListBox.ItemsSource = results;
                        } else
                        {
                            MessageBox.Show("0 results found for: " + search);
                        }
                    }
                }
            }

        }

        private void ResultsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (resultsListBox.SelectedItem == null)
                return;

            if (districtComboBox.SelectedItem == null || searchType == SearchType.District)
            {
                District district = (District)resultsListBox.SelectedItem;

                districtComboBox.SelectedItem = district;

                if(searchType != SearchType.District)
                    resultsListBox.ItemsSource = district.GetNeighbourhoods();

            } else if (neighbourComboBox.SelectedItem == null || searchType == SearchType.Neighbourhood)
            {
                Neighbourhood neighbourhood = (Neighbourhood)resultsListBox.SelectedItem;
                neighbourComboBox.SelectedItem = neighbourhood;

                if (searchType == SearchType.Property)
                {
                    resultsListBox.ItemsSource = neighbourhood.GetProperties();
                    filterComboBox.ItemsSource = propertyFilters;
                }

            } else if (propertyComboBox.SelectedItem == null && searchType == SearchType.Property)
            {
                propertyComboBox.SelectedItem = (Property) resultsListBox.SelectedItem;
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            adminWindow.ChangeFrame(new AddItemPage((int) searchType));
        }
    }
}
