using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristPlaces.DataAccess.Entities
{
    public class PlaceEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public byte[] Image { get; set; } = null!;

        public Guid RegionId { get; set; }

        public RegionEntity Region { get; set; } = null!;
    }
}
