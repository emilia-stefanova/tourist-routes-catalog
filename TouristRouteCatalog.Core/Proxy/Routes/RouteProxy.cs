using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TouristRouteCatalog.Core.Proxy
{
    public class RouteProxy
    {
        private static List<string> _defaultImages = new List<string>()
        {
            "http://tmacfitness.com/wp-content/uploads/2013/04/Beauty-of-nature-random-4884759-1280-800.jpg",
            "http://3.bp.blogspot.com/-5A5xpicPF5g/T8srguvp3TI/AAAAAAAAEPs/bLuFIK0gDss/s1600/nature-wallpaper-23.jpg",
            "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcTNiR0iGQJ81w5_IO80kUW6DVzxGRWUqgjEIGbGztTCcorr2532",
            "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQPPmONktTv0-m0vzR1mXXcudkYCZ9o2ZXNMPJ2uUKSwsf9SMy8",
            "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcTMYQ4D1RD0Dgt9jzEXIEEAmUy7y2A01IguOlwAQLxYE-6yxzCg",
            "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcSfcrJqSNyO5hSOiuIdtmFoekp44eKHRpToqWT0y453lxiP6pnX",
            "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcRQ6-b-AtD-MfDXMXStWvSITgAPlf7E341Xpzx-4FHmE18Ufzq8",
            "http://changeyourlifein40days.com/wp-content/uploads/2013/07/Our-Surrendered-Nature.jpg"
        };

        private static int currentDefaultImageIndex = -1;



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
        [Required(ErrorMessage="Името е задължително")]
        public string Name { get; set; }

        [DisplayName("Трудност")]
        [Required(ErrorMessage = "Трудността е задължително")]
        public int DifficultyLevel { get; set; }

        [DisplayName("Продължителност")]
        [Required(ErrorMessage = "Продължителността е задължително")]
        public Nullable<long> Duration { get; set; }

        [DisplayName("Описание")]
        [Required(ErrorMessage = "Описаниете е задължително")]
        public string Description { get; set; }

        [DisplayName("Сезони")]
        [Required(ErrorMessage = "Полето Сезони е задължително")]
        public string Seasons { get; set; }

        [DisplayName("Градски Транспорт")]
        [Required(ErrorMessage = "Полето градски транспорт е задължително")]
        public string PublicTransport { get; set; }

        public ICollection<CampsiteProxy> Campsites { get; set; }

        public ICollection<WaterSourceProxy> WaterSources { get; set; }

        public ICollection<RouteGeoPointProxy> GeoPoints { get; set; }

        public ICollection<RouteImageProxy> Images { get; set; }

        public RouteImageProxy DefaultImage
        {
            get
            {
                return Images.FirstOrDefault() ??
                    new RouteImageProxy()
                    {
                        Description = "Гледка",
                        ImageLocation = GetDefaultImageLocation()
                    };
            }
        }

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
                        serializedPoints = string.Format("{0}{1}:{2}", serializedPoints, item.Latitude.ToString(CultureInfo.InvariantCulture), item.Longitude.ToString(CultureInfo.InvariantCulture));
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
                        geoPoint.Latitude = double.Parse(geoLocations[0], CultureInfo.InvariantCulture);
                        geoPoint.Longitude = double.Parse(geoLocations[1], CultureInfo.InvariantCulture);
                        points.Add(geoPoint);
                    }
                }
                GeoPoints = points;
            }
        }

        private static string GetDefaultImageLocation()
        {
            currentDefaultImageIndex += 1;
            return _defaultImages[currentDefaultImageIndex % _defaultImages.Count];
        }

    }
}
