using Micro_Project.MyDbClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Micro_Project
{
    public class MyDbContext: DbContext
    {

        internal object clients;

        public MyDbContext() : base("DbConnectionString")
        {

        }

        public DbSet<RegHotel> RegHotels { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<HotelRoom> HotelRooms { get; set; }

    }
}
