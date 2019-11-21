namespace AirBnbSystem.Airbnb.Models
{
    internal class District : AirbnbCollection
    {
        private Neighbourhood[] neighbourhoods;

        public District()
        {
        }

        public District(string name) : base(name)
        {
            this.neighbourhoods = null;
        }

        public District(string name, Neighbourhood[] neighbourhoods) : base(name)
        {
            this.SetNumInCollection(neighbourhoods.Length);
            this.neighbourhoods = neighbourhoods;
        }

        public Neighbourhood[] GetNeighbourhoods()
        {
            return neighbourhoods;
        }

        public void SetNeighbourhoods(Neighbourhood[] neighbourhoods)
        {
            this.neighbourhoods = neighbourhoods;
        }
    }
}