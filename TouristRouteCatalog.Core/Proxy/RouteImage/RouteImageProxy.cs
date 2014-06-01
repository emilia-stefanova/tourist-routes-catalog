using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristRouteCatalog.Core.Proxy
{
    public class RouteImageProxy
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Път към снимката")]
        public string ImageLocation { get; set; }
        public int RouteId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        [Required]
        [DisplayName("Име")]
        public string Name { get; set; }
        [DisplayName("Описание")]
        public string Description { get; set; }
    }
}
