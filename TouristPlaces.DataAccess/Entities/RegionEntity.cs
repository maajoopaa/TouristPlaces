using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristPlaces.DataAccess.Entities
{
    public class RegionEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public virtual ICollection<PlaceEntity> Places { get; set; } = [];
    }
}
