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


        public SearchPage()
        {
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

            filterComboBox.ItemsSource = propertyFilters;

            districtComboBox.IsEnabled = true;
            neighbourComboBox.IsEnabled = true;
            propertyComboBox.IsEnabled = true;

            districtComboBox.ItemsSource = AirbnbMain.GetInstance().GetDistricts();
        }

        void EnableDistrictSearch()
        {
            filterComboBox.ItemsSource = defaultFilters;

            districtComboBox.IsEnabled = true;
            neighbourComboBox.IsEnabled = false;
            propertyComboBox.IsEnabled = false;

            districtComboBox.ItemsSource = AirbnbMain.GetInstance().GetDistricts();
            resultsListBox.ItemsSource = AirbnbMain.GetInstance().GetDistricts();
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
                    break;
            }
        }

        void DistrictComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string name = ((District)districtComboBox.SelectedItem).GetName();

            neighbourComboBox.ItemsSource = DataUtils.FindDistrictFromName(AirbnbMain.GetInstance().GetDistricts(), name).GetNeighbourhoods();

            if(searchType == SearchType.Neighbourhood)
            {
                resultsListBox.ItemsSource = neighbourComboBox.ItemsSource;
            } else if(searchType == SearchType.District)
            {
                resultsListBox.ItemsSource = new District[] { (District)districtComboBox.SelectedItem };
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
            if(searchFieldTxt.Text != null)
            {
                search = searchFieldTxt.Text;
                if (searchType == SearchType.District)
                {
                    AirbnbCollection[] collection = (AirbnbCollection[])AirbnbMain.GetInstance().GetDistricts();

                    AirbnbCollection[] results = DataUtils.FindAirbnbCollectionsFromRegexName(collection, search);

                    if(results.Length > 0)
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
}
