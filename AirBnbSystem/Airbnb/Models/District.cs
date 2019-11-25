using AirBnbSystem.Airbnb.Utils;

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

        public Neighbourhood GetNeighbourhood(int index)
        {
            return neighbourhoods[index];
        }
        
        public void SetNeighbourhood(Neighbourhood neighbourhood, int index)
        {
            neighbourhoods[index] = neighbourhood;
        }

        public void AddNeighbourhood(Neighbourhood neighbourhood)
        {
            if (neighbourhoods == null)
            {
                neighbourhoods = new Neighbourhood[1]
                {
                    neighbourhood,
                };
                return;
            }
            else
            {
                DataUtils.ResizeArray<Neighbourhood>(ref neighbourhoods, neighbourhoods.Length + 1);

                neighbourhoods[neighbourhoods.Length - 1] = neighbourhood;
            }
        }
    }
}