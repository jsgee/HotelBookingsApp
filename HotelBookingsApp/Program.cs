using System;
using System.Linq;

namespace HotelBookingsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IBookingsManager bm = new BookingsManager();
            //The list of available rooms is in the Rooms Table. It won't work if the room isn't listed there do to a key constraint.
            bm.AddBooking("Grimm", 102, DateTime.Now);

            var isAvailable = bm.IsRoomAvailable(102, DateTime.Now);
            //This should be false if AddBooking worked...
            Console.WriteLine(isAvailable);

            var availRooms = bm.GetAvailableRooms(DateTime.Now);
            availRooms.ToList().ForEach(ar => Console.WriteLine(ar));

        }
    }
}
