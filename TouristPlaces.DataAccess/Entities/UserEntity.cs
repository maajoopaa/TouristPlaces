using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristPlaces.DataAccess.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public byte IsAdmin { get; set; } = 0;
    }
}
