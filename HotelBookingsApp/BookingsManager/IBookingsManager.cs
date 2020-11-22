using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingsApp
{
    public interface IBookingsManager
    {
        bool IsRoomAvailable(int room, DateTime date);

        void AddBooking(string guest, int room, DateTime date);

        IEnumerable<int> GetAvailableRooms(DateTime date);
        
    }
}
