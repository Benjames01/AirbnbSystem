using AirBnbSystem.Airbnb.Utils;

namespace AirBnbSystem.Airbnb.Models
{
    internal class Neighbourhood : AirbnbCollection
    {
        private Property[] properties;

        public Neighbourhood(string name, Property[] properties) : base(name)
        {
            this.SetNumInCollection(properties.Length);
            this.properties = properties;
        }

        public Neighbourhood(string name) : base(name)
        {
            this.properties = null;
        }

        public Neighbourhood()
        {
        }

        public Property[] GetProperties()
        {
            return properties;
        }

        public void SetProperties(Property[] properties)
        {
            this.properties = properties;
        }

        public void AddProperty(Property property)
        {
            if (properties == null)
            {
                properties = new Property[1]
                {
                    property,
                };
                return;
            }
            else
            {
                DataUtils.ResizeArray<Property>(ref properties, properties.Length + 1);

                properties[properties.Length - 1] = property;
            }
        }
    }
}