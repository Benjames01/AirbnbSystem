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

            return (float) totalPrice / totalProperties;
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

            if (district.GetNumInCollection() <= 0)
                return 0f;

            for (int i = 0; i < district.GetNumInCollection(); i++)
            {
                if (district.GetNeighbourhood(i).GetNumInCollection() > 0)
                {
                    for (int y = 0; y < district.GetNeighbourhood(i).GetNumInCollection(); y++)
                    {
                        totalAvailability += district.GetNeighbourhood(i).GetProperties()[y].GetAvailableDaysPerYear();
                        totalProperties++;
                    }
                }

            }

            return (float)totalAvailability / totalProperties;
        }

        public static float GetDistrictAverageHostProperties(District district)
        {
            int totalHosts = 0;
            int totalProperties = 0;

            if (district.GetNumInCollection() <= 0)
                return 0f;

            for (int i = 0; i < district.GetNumInCollection(); i++)
            {
                if (district.GetNeighbourhood(i).GetNumInCollection() > 0)
                {
                    for (int y = 0; y < district.GetNeighbourhood(i).GetNumInCollection(); y++)
                    {
                        totalProperties += district.GetNeighbourhood(i).GetProperties()[y].GetNumHostProperties();
                        totalHosts++;
                    }
                }

            }

            return (float)totalProperties / totalHosts;
        }


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
                            hosts = new string[]{host};
                        } else if(!contains)
                        {
                            ResizeArray(ref hosts, hosts.Length + 1);
                            hosts[hosts.Length -1] = host;
                        }

                        totalProperties++;
                    }
                }
            }

            return totalProperties / hosts.Length;
        }


        public static bool CheckIfContains(string[] array, string value)
        {
            if (array == null)
                return false;

            for (int i = 0; i < array.Length; i++)
            {
                if(array[i] == value)
                {
                    return true;
                }
            }

            return false;
        }


        public static GraphData[] GetGraphData(District district)
        {
            GraphData[] dataSet = new GraphData[district.GetNeighbourhoods().Length];

            for (int i = 0; i < district.GetNeighbourhoods().Length; i++)
            {
                float averagePrice = GetNeighbourhoodAveragePropertyPrice(district.GetNeighbourhood(i));
                GraphData data = new GraphData
                {
                    name = district.GetNeighbourhood(i).GetName(),
                    averagePrice = averagePrice
                };
                dataSet[i] = data;
            }

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
                    for (int y = 1; y < 3; y++)
                    {
                        if (str.Length <= i+y)
                            return startingChars;
                        if (str[i + y] != ' ')
                            startingChars += str[i + y];
                    }
                    startingChars += " ";
                    found = false;
                }
            }

            return startingChars;
        }

        public struct GraphData
        {
            public string name;
            public float averagePrice;
        }

    }
}