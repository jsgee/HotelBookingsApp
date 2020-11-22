using System;
using System.Collections.Generic;

#nullable disable

namespace HotelBookingsApp
{
    public partial class BookedRoom
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string BookingDate { get; set; }
        public string GuestName { get; set; }

        public virtual Room Room { get; set; }
    }
}
