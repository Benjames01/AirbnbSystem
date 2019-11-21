namespace AirBnbSystem.Airbnb.Models
{
    internal class AirbnbCollection
    {
        private string name;

        private int numInCollection;

        public AirbnbCollection()
        {
        }

        public AirbnbCollection(string name)
        {
            this.name = name;
            this.numInCollection = 0;
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public int GetNumInCollection()
        {
            return numInCollection;
        }

        public void SetNumInCollection(int numInCollection)
        {
            this.numInCollection = numInCollection;
        }

        public override string ToString()
        {
            return this.GetName();
        }
    }
}