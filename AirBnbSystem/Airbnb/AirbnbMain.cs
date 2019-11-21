using AirBnbSystem.Airbnb.Models;
using AirBnbSystem.Airbnb.Utils;
using System.Diagnostics;
using System.IO;

namespace AirBnbSystem.Airbnb
{
    internal class AirbnbMain
    {
        private static AirbnbMain instance;

        private District[] districts;

        private string fileName;

        private AirbnbMain()
        {
        }

        public static AirbnbMain GetInstance()
        {
            if (instance == null)
            {
                instance = new AirbnbMain();
            }
            return instance;
        }

        public void SetFileName(string fileName)
        {
            this.fileName = fileName;
        }

        public string GetFileName()
        {
            return fileName;
        }

        public District[] GetDistricts()
        {
            return districts;
        }

        public bool LoadData()
        {
            if (fileName == null || fileName == "")
            {
                return false;
            }
            else
            {
                try
                {
                    districts = new District[1];
                    StreamReader reader = new StreamReader(fileName);

                    while (reader.Peek() >= 0)
                    {
                        District district = new District();

                        district.SetName(reader.ReadLine());
                        district.SetNumInCollection(DataUtils.StringToInt(reader.ReadLine()));

                        Neighbourhood[] neighbourhoods = new Neighbourhood[district.GetNumInCollection()];

                        for (int i = 0; i < district.GetNumInCollection(); i++)
                        {
                            Neighbourhood neighbourhood = new Neighbourhood();

                            neighbourhood.SetName(reader.ReadLine());
                            neighbourhood.SetNumInCollection(DataUtils.StringToInt(reader.ReadLine()));

                            Property[] properties = new Property[neighbourhood.GetNumInCollection()];

                            for (int y = 0; y < neighbourhood.GetNumInCollection(); y++)
                            {
                                Property property = new Property();

                                property.SetId(reader.ReadLine());
                                property.SetName(reader.ReadLine());
                                property.SetHostId(reader.ReadLine());
                                property.SetHostName(reader.ReadLine());
                                property.SetNumHostProperties(DataUtils.StringToInt(reader.ReadLine()));
                                property.SetLatitude(DataUtils.StringToFloat(reader.ReadLine()));
                                property.SetLongitude(DataUtils.StringToFloat(reader.ReadLine()));
                                property.SetRoomType(reader.ReadLine());
                                property.SetPrice(DataUtils.StringToInt(reader.ReadLine()));
                                property.SetMinNightsStay(DataUtils.StringToInt(reader.ReadLine()));
                                property.SetAvailableDaysPerYear(DataUtils.StringToInt(reader.ReadLine()));

                                properties[y] = property;
                            }

                            neighbourhood.SetProperties(properties);
                            neighbourhoods[i] = neighbourhood;
                        }

                        district.SetNeighbourhoods(neighbourhoods);
                        DataUtils.ResizeArray<District>(ref districts, districts.Length + 1);

                        districts[districts.Length - 1] = district;
                    }

                    reader.Close();
                }
                catch (FileNotFoundException e)
                {
                    Debug.WriteLine("Unable to find the data file. " + e.Message);
                }
                catch (IOException e)
                {
                    Debug.WriteLine("Error occurred while handling data file. " + e.Message);
                }

                return true;
            }
        }
    }
}