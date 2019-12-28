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
    }
}