using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristRouteCatalog.Core.DAL;

namespace TouristRouteCatalog.Core.Proxy
{
    public class CampsiteProxy
    {
        public CampsiteProxy()
        {

        }

        public CampsiteProxy(Campsite campsite)
        {
            Id = campsite.Id;
            TypeId = campsite.TypeId;
            Description = campsite.Description;
            Name = campsite.Name;
            Latitude = campsite.Latitude;
            Longitude = campsite.Longitude;
        }

        public int Id { get; set; }

        internal int TypeId { get; set; }

        public CampsiteType Type
        {
            get
            {
                return (CampsiteType)TypeId;
            }
            set
            {
                TypeId = (int)value;
            }
        }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        //public ICollection<RouteProxy> Routes { get; set; }
    }

    public enum CampsiteType
    {
        Hut = 0, // заслон
        MountainHostel = 1, // хижа
        TentSite = 2, // място за разпъване на палатка
        Camping = 3 // къмпинг
    }

}
