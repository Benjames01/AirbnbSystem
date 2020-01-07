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
        private SearchType searchType;

        private AdminWindow adminWindow;

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
                else if (!button.Content.Equals(searchTypeRadioBtns[i].Content))
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

        private void EnablePropertySearch()
        {
            districtComboBox.IsEnabled = true;
            neighbourComboBox.IsEnabled = true;
            propertyComboBox.IsEnabled = true;
        }

        private void EnableNeighbourhoodSearch()
        {
            districtComboBox.IsEnabled = true;
            neighbourComboBox.IsEnabled = true;
            propertyComboBox.IsEnabled = false;
        }

        private void EnableDistrictSearch()
        {
            districtComboBox.IsEnabled = true;
            districtComboBox.SelectedItem = null;

            neighbourComboBox.IsEnabled = false;
            propertyComboBox.IsEnabled = false;
        }

        private void UpdateFilters(SearchType searchType)
        {
            deleteBtn.Visibility = Visibility.Hidden;
            switch (searchType)
            {
                case (SearchType.Property):
                    EnablePropertySearch();
                    deleteBtn.Visibility = Visibility.Visible;
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

        private void DistrictComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (districtComboBox.SelectedItem != null)
            {
                string name = ((District)districtComboBox.SelectedItem).GetName();

                Neighbourhood[] neighbourhoods = ((District)DataUtils
                    .GetAirbnbCollectionFromName(AirbnbMain.GetInstance()
                    .GetDistricts(), name))
                    .GetNeighbourhoods();

                neighbourComboBox.ItemsSource = neighbourhoods;

                if (searchType == SearchType.Neighbourhood || searchType == SearchType.Property)
                {
                    resultsListBox.ItemsSource = neighbourComboBox.ItemsSource;
                }
                else if (searchType == SearchType.District)
                {
                    resultsListBox.ItemsSource = new District[] { (District)districtComboBox.SelectedItem };
                }
            }
        }

        private void NeighbourComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (neighbourComboBox.SelectedItem != null && districtComboBox.SelectedItem != null)
            {
                string name = ((District)districtComboBox.SelectedItem).GetName();
                string neighbourhoodName = ((Neighbourhood)neighbourComboBox.SelectedItem).GetName();

                if (searchType == SearchType.Property)
                {
                    propertyComboBox.ItemsSource =
                        ((Neighbourhood)DataUtils.GetAirbnbCollectionFromName(
                        ((District)DataUtils.GetAirbnbCollectionFromName(
                            AirbnbMain.GetInstance().GetDistricts(), name))
                            .GetNeighbourhoods(), neighbourhoodName))
                            .GetProperties();

                    resultsListBox.ItemsSource = propertyComboBox.ItemsSource;
                }
                else
                {
                    resultsListBox.ItemsSource = new Neighbourhood[] { (Neighbourhood)neighbourComboBox.SelectedItem };
                }
            }
        }

        private void PropertyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (searchType == SearchType.Property)
            {
                resultsListBox.ItemsSource = new Property[] { (Property)propertyComboBox.SelectedItem };
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

                if (searchType != SearchType.District)
                    resultsListBox.ItemsSource = district.GetNeighbourhoods();
            }
            else if (neighbourComboBox.SelectedItem == null || searchType == SearchType.Neighbourhood)
            {
                Neighbourhood neighbourhood = (Neighbourhood)resultsListBox.SelectedItem;
                neighbourComboBox.SelectedItem = neighbourhood;

                if (searchType == SearchType.Property)
                {
                    resultsListBox.ItemsSource = neighbourhood.GetProperties();
                }
            }
            else if (propertyComboBox.SelectedItem == null && searchType == SearchType.Property)
            {
                propertyComboBox.SelectedItem = (Property)resultsListBox.SelectedItem;
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (resultsListBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a property before deleting!");
            }
            else
            {
                Property[] properties = ((Neighbourhood)neighbourComboBox.SelectedItem).GetProperties();
                Property property = (Property)resultsListBox.SelectedItem;

                int index = DataUtils.GetPropertyIndexFromID(properties, property.GetId());

                int i = properties.Length;

                DataUtils.RemoveAtIndex(ref properties, index);

                i = properties.Length;

                ((Neighbourhood)neighbourComboBox.SelectedItem).SetProperties(properties);
                ((Neighbourhood)neighbourComboBox.SelectedItem).SetNumInCollection(properties.Length);

                int neighbourIndex = neighbourComboBox.SelectedIndex;

                ((District)districtComboBox.SelectedItem)
                    .SetNeighbourhood(((Neighbourhood)neighbourComboBox.SelectedItem)
                    , neighbourIndex);

                AirbnbMain.GetInstance().SaveData();

                neighbourComboBox.ItemsSource = ((District)districtComboBox.SelectedItem).GetNeighbourhoods();

                neighbourComboBox.SelectedItem = null;
                propertyComboBox.SelectedItem = null;

                MessageBox.Show("Successfully Deleted Property!");
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            adminWindow.ChangeFrame(new AddItemPage((int)searchType));
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            bool isOpenable = true;

            switch (searchType)
            {
                case (SearchType.Property):
                    if (propertyComboBox.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a property before trying to view/edit!");
                        isOpenable = false;
                    }
                    ViewObject(isOpenable, ((Property)propertyComboBox.SelectedItem));
                    break;

                case (SearchType.District):
                    if (districtComboBox.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a district before trying to view/edit!");
                        isOpenable = false;
                    }
                    ViewObject(isOpenable, ((District)districtComboBox.SelectedItem));
                    break;

                case (SearchType.Neighbourhood):
                    if (neighbourComboBox.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a neighbourhood before trying to view/edit!");
                        isOpenable = false;
                    }
                    else
                    {
                        AddItemPage viewPage = ViewObject(isOpenable, ((Neighbourhood)neighbourComboBox.SelectedItem));
                        if (viewPage != null)
                        {
                            viewPage.SetDistrictName(districtComboBox.Text);
                        }
                    }
                    break;
            }
        }

        private AddItemPage ViewObject<T>(bool isOpenable, T obj)
        {
            AddItemPage viewPage = OpenViewPage(isOpenable);

            if (viewPage != null)
            {
                viewPage.AddObj(obj);
                return viewPage;
            }

            return null;
        }

        private AddItemPage OpenViewPage(bool isOpenable)
        {
            if (isOpenable)
            {
                AddItemPage itemPage = new AddItemPage((int)searchType);
                adminWindow.ChangeFrame(itemPage);

                return itemPage;
            }

            return null;
        }
    }
}