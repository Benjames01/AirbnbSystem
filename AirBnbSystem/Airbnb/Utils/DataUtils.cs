using AirBnbSystem.Airbnb.Models;
using System.Text.RegularExpressions;
using System.Windows;

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

        public static Neighbourhood FindNeighbourhoodFromName(Neighbourhood[] neighbourhoods, string name)
        {
            for (int i = 0; i < neighbourhoods.Length; i++)
            {
                if (neighbourhoods[i].GetName().Equals(name))
                {
                    return neighbourhoods[i];
                }
            }

            return null;
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

    }
}