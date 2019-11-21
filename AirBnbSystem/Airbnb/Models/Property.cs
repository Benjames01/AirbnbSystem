namespace AirBnbSystem.Airbnb.Models
{
    internal class Property
    {
        private string id;
        private string name;

        private string hostId;
        private string hostName;

        private int numHostProperties;

        private float latitude;
        private float longitude;

        private string roomType;

        private int price;

        private int minNightsStay;
        private int availableDaysPerYear;

        public Property()
        {
        }

        public Property(string id, string name, string hostId, string hostName,
            int numHostProperties, float latitude, float longitude, string roomType,
            int price, int minNightsStay, int availableDaysPerYear)
        {
            this.id = id;
            this.name = name;

            this.hostId = hostId;
            this.hostName = hostName;

            this.numHostProperties = numHostProperties;

            this.latitude = latitude;
            this.longitude = longitude;

            this.roomType = roomType;

            this.price = price;

            this.minNightsStay = minNightsStay;
            this.availableDaysPerYear = availableDaysPerYear;
        }

        public string GetId()
        {
            return id;
        }

        public void SetId(string id)
        {
            this.id = id;
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public string GetHostId()
        {
            return hostId;
        }

        public void SetHostId(string hostId)
        {
            this.hostId = hostId;
        }

        public string GetHostName()
        {
            return hostName;
        }

        public void SetHostName(string hostName)
        {
            this.hostName = hostName;
        }

        public int GetNumHostProperties()
        {
            return numHostProperties;
        }

        public void SetNumHostProperties(int numHostProperties)
        {
            this.numHostProperties = numHostProperties;
        }

        public float GetLatitude()
        {
            return latitude;
        }

        public void SetLatitude(float latitude)
        {
            this.latitude = latitude;
        }

        public float GetLongitude()
        {
            return longitude;
        }

        public void SetLongitude(float longitude)
        {
            this.longitude = longitude;
        }

        public string GetRoomType()
        {
            return roomType;
        }

        public void SetRoomType(string roomType)
        {
            this.roomType = roomType;
        }

        public int GetPrice()
        {
            return price;
        }

        public void SetPrice(int price)
        {
            this.price = price;
        }

        public int GetMinNightsStay()
        {
            return minNightsStay;
        }

        public void SetMinNightsStay(int minNightsStay)
        {
            this.minNightsStay = minNightsStay;
        }

        public int GetAvailableDaysPerYear()
        {
            return availableDaysPerYear;
        }

        public void SetAvailableDaysPerYear(int availableDaysPerYear)
        {
            this.availableDaysPerYear = availableDaysPerYear;
        }

        public override string ToString()
        {
            return this.GetName();
        }
    }
}