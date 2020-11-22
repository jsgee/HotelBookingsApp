using System;
using System.Collections.Generic;

#nullable disable

namespace HotelBookingsApp
{
    public partial class Room
    {
        public Room()
        {
            BookedRooms = new HashSet<BookedRoom>();
        }

        public int Id { get; set; }
        public int RoomId { get; set; }

        public virtual ICollection<BookedRoom> BookedRooms { get; set; }
    }
}
