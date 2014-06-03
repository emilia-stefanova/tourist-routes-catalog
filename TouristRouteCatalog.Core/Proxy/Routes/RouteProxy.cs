using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TouristRouteCatalog.Core.Proxy
{
    public class RouteProxy
    {
        public RouteProxy()
        {

        }

        public RouteProxy(TouristRouteCatalog.Core.DAL.Route r)
        {
            Id = r.Id;
            CreatorId = r.CreatorId;
            Name = r.Name;
            DifficultyLevel = r.DifficultyLevel;
            Duration = r.Duration;
            Description = r.Description;
            Seasons = r.Seasons;
            PublicTransport = r.PublicTransport;
        }

        public int Id { get; set; }

        public int CreatorId { get; set; }

        [DisplayName("Име")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Трудност")]
        [Required]
        public int DifficultyLevel { get; set; }

        [DisplayName("Продължителност")]
        [Required]
        public Nullable<long> Duration { get; set; }

        [DisplayName("Описание")]
        [Required]
        public string Description { get; set; }

        [DisplayName("Сезони")]
        [Required]
        public string Seasons { get; set; }

        [DisplayName("Градски Транспорт")]
        [Required]
        public string PublicTransport { get; set; }

        public ICollection<CampsiteProxy> Campsites { get; set; }

        public ICollection<WaterSourceProxy> WaterSources { get; set; }

        public ICollection<RouteGeoPointProxy> GeoPoints { get; set; }

        public ICollection<RouteImageProxy> Images { get; set; }

        public string SerializedGeoPoints
        {
            get
            {
                string serializedPoints = "";
                if (GeoPoints != null)
                {
                    foreach (var item in GeoPoints)
                    {
                        if (!string.IsNullOrEmpty(serializedPoints))
                        {
                            serializedPoints += ";";
                        }
                        serializedPoints = string.Format("{0}{1}:{2}", serializedPoints, item.Latitude, item.Longitude);
                    }
                }
                return serializedPoints;
            }
            set
            {
                List<RouteGeoPointProxy> points = new List<RouteGeoPointProxy>();
                if (value != null)
                {
                    string[] allPoints = value.Split(new char[] { ';' });
                    for (int i = 0; i < allPoints.Length; i++)
                    {
                        string[] geoLocations = allPoints[i].Split(new char[] { ':' });
                        RouteGeoPointProxy geoPoint = new RouteGeoPointProxy();
                        geoPoint.Latitude = double.Parse(geoLocations[0]);
                        geoPoint.Longitude = double.Parse(geoLocations[1]);
                        points.Add(geoPoint);
                    }
                }
                GeoPoints = points;
            }
        }

    }
}
