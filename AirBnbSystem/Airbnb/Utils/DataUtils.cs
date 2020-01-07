using AirBnbSystem.Airbnb.Models;
using System.Text.RegularExpressions;

namespace AirBnbSystem.Airbnb.Utils
{
    internal class DataUtils
    {
        private DataUtils()
        {
        }

        public static int StringToInt(string str)
        {
            int parsed;

            if (int.TryParse(str, out parsed))
            {
                return parsed;
            }

            return -1;
        }

        public static float StringToFloat(string str)
        {
            float parsed;

            if (float.TryParse(str, out parsed))
            {
                return parsed;
            }

            return 0f;
        }

        public static void ResizeArray<T>(ref T[] array, int newSize)
        {
            T[] temp = new T[newSize];

            for (int i = 0; i < array.Length; i++)
            {
                temp[i] = array[i];
            }

            array = temp;
        }

        public static AirbnbCollection GetAirbnbCollectionFromName(AirbnbCollection[] collection, string name)
        {
            for (int i = 0; i < collection.Length; i++)
            {
                if (collection[i] != null)
                {
                    if (collection[i].GetName().Equals(name))
                    {
                        return collection[i];
                    }
                }
            }
            return null;
        }

        public static int GetAirbnbCollectionIndexFromName(AirbnbCollection[] array, string name)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null)
                {
                    if (array[i].GetName().Equals(name))
                        return i;
                }
            }

            return -1;
        }

        public static int GetPropertyIndexFromID(Property[] properties, string id)
        {
            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].GetId().Equals(id))
                {
                    return i;
                }
            }

            return -1;
        }

        public static void RemoveAtIndex<T>(ref T[] array, int index)
        {
            T[] tmpArray = new T[array.Length - 1];

            int i = 0;
            int y = 0;

            while (i < array.Length)
            {
                if (i != index)
                {
                    tmpArray[y] = array[i];
                    y++;
                }
                i++;
            }

            array = tmpArray;
        }

        public static District FindDistrictFromName(District[] districts, string name)
        {
            for (int i = 0; i < districts.Length; i++)
            {
                if (districts[i] != null && districts[i].GetName().Equals(name))
                {
                    return districts[i];
                }
            }

            return null;
        }

        /*
         * Not in use functionality not required
         */

        public static AirbnbCollection[] FindAirbnbCollectionsFromRegexName(AirbnbCollection[] collections, string name)
        {
            AirbnbCollection[] airbnbCollections = new AirbnbCollection[0];

            Regex rx = new Regex(name, RegexOptions.IgnoreCase);

            for (int i = 0; i < collections.Length; i++)
            {
                if (collections[i] != null)
                {
                    Match match = rx.Match(collections[i].GetName());
                    if (match.Success)
                    {
                        ResizeArray<AirbnbCollection>(ref airbnbCollections, airbnbCollections.Length + 1);
                        airbnbCollections[airbnbCollections.Length - 1] = collections[i];
                    }
                }
            }
            return airbnbCollections;
        }

        public static float GetDistrictAveragePropertyPrice(District district)
        {
            int totalPrice = 0;
            int totalProperties = 0;

            if (district.GetNumInCollection() <= 0)
                return 0f;

            for (int i = 0; i < district.GetNumInCollection(); i++)
            {
                if (district.GetNeighbourhood(i).GetNumInCollection() > 0)
                {
                    for (int y = 0; y < district.GetNeighbourhood(i).GetNumInCollection(); y++)
                    {
                        totalPrice += district.GetNeighbourhood(i).GetProperties()[y].GetPrice();
                        totalProperties++;
                    }
                }
            }

            return (float)totalPrice / totalProperties;
        }

        public static float GetNeighbourhoodAveragePropertyPrice(Neighbourhood neighbourhood)
        {
            int totalPrice = 0;
            int totalProperties = 0;

            if (neighbourhood.GetNumInCollection() <= 0)
                return 0f;

            for (int i = 0; i < neighbourhood.GetNumInCollection(); i++)
            {
                totalPrice += neighbourhood.GetProperties()[i].GetPrice();
                totalProperties++;
            }

            return (float)totalPrice / totalProperties;
        }

        public static float GetDistrictAverageAvailablity(District district)
        {
            int totalAvailability = 0;
            int totalProperties = 0;

            if (district.GetNumInCollection() <= 0)// Check district contains neighbourhoods
                return 0f;

            for (int i = 0; i < district.GetNumInCollection(); i++) // For each neighbourhood in district
            {
                if (district.GetNeighbourhood(i).GetNumInCollection() > 0) // Check neighbourhood contains properties
                {
                    for (int y = 0; y < district.GetNeighbourhood(i).GetNumInCollection(); y++) // For each property in neighbourhood
                    {
                        totalAvailability += district.GetNeighbourhood(i).GetProperties()[y].GetAvailableDaysPerYear(); // Get current property's available days per year, add to totalAvailablity
                        totalProperties++; // increment property count
                    }
                }
            }

            return (float)totalAvailability / totalProperties; // calculate average and return
        }

        public static float GetDistrictAverageHostProperties(District district)
        {
            int totalHosts = 0;
            int totalProperties = 0;

            if (district.GetNumInCollection() <= 0) // Check district contains neighbourhoods
                return 0f;

            for (int i = 0; i < district.GetNumInCollection(); i++) // For each neighbourhood in district
            {
                if (district.GetNeighbourhood(i).GetNumInCollection() > 0) // Check neighbourhood contains properties
                {
                    for (int y = 0; y < district.GetNeighbourhood(i).GetNumInCollection(); y++) // For each property in neighbourhood
                    {
                        totalProperties += district.GetNeighbourhood(i).GetProperties()[y].GetNumHostProperties(); // add current property's number of host's properties to totalProperties variable
                        totalHosts++; // increment host count
                    }
                }
            }

            return (float)totalProperties / totalHosts; // Calculate average and return it
        }

        /**
         * Calculate average number of properties per host
         * (not in use because not multiple properties with same host id,
         * use GetDistrictAverageHostProperties() instead)
         */

        public static int GetDistrictAveragePropertiesPerHost(District district)
        {
            int totalProperties = 0;

            string[] hosts = null;

            if (district.GetNumInCollection() <= 0)
                return 0;

            for (int i = 0; i < district.GetNumInCollection(); i++)
            {
                if (district.GetNeighbourhood(i).GetNumInCollection() > 0)
                {
                    for (int y = 0; y < district.GetNeighbourhood(i).GetNumInCollection(); y++)
                    {
                        string host = district.GetNeighbourhood(i).GetProperties()[y].GetHostId();

                        bool contains = CheckIfContains(hosts, host);

                        if (hosts == null && !contains)
                        {
                            hosts = new string[] { host };
                        }
                        else if (!contains)
                        {
                            ResizeArray(ref hosts, hosts.Length + 1);
                            hosts[hosts.Length - 1] = host;
                        }

                        totalProperties++;
                    }
                }
            }

            return totalProperties / hosts.Length;
        }

        public static bool CheckIfContains(string[] array, string value)
        {
            // if the array doesnt exist yet return false
            if (array == null)
                return false;

            // for each item in the array check array[i] is equal to the value we're checking for, return true if match, false if not
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == value)
                {
                    return true;
                }
            }

            return false;
        }

        // Generate our data set for our graph
        public static GraphData[] GetGraphData(District district)
        {
            // Create a new GraphData array with the same amount of elements as our districts neighbourhood array
            GraphData[] dataSet = new GraphData[district.GetNeighbourhoods().Length];

            // For eacg neighbourhood calculate the average property price
            for (int i = 0; i < district.GetNeighbourhoods().Length; i++)
            {
                float averagePrice = GetNeighbourhoodAveragePropertyPrice(district.GetNeighbourhood(i));

                // Create a new GraphData with the name of the current neighbourhood and the average property price
                GraphData data = new GraphData
                {
                    name = district.GetNeighbourhood(i).GetName(),
                    averagePrice = averagePrice
                };
                // Set the GraphData at i in our dataset to the GraphData we just created
                dataSet[i] = data;
            }
            // Return the dataset
            return dataSet;
        }

        public static string GetStartingCharactersOfString(string str)
        {
            string startingChars = "";

            bool found = true;
            for (int i = 0; i < str.Length; i++)
            {
                // If it's a space set found to true
                if (str[i] == ' ')
                    found = true;
                else if (str[i] != ' ' && found == true)
                {
                    startingChars += (str[i]);
                    // Try and get next 2 characters
                    for (int y = 1; y < 3; y++)
                    {
                        // out of bounds return what we have
                        if (str.Length <= i + y)
                            return startingChars;
                        // get next character and add to our startingChars array
                        if (str[i + y] != ' ')
                            startingChars += str[i + y];
                    }
                    // Add a space between our word
                    startingChars += " ";
                    found = false;
                }
            }
            // Return the starting characters of our string
            return startingChars;
        }

        public struct GraphData
        {
            public string name;
            public float averagePrice;
        }
    }
}