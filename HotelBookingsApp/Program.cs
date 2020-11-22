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
            bm.AddBooking("Grimm", 102, DateTime.Now.AddDays(2));

            var is102Available = bm.IsRoomAvailable(102, DateTime.Now.AddDays(2));
            //This should be false if AddBooking worked...
            Console.WriteLine(is102Available);

            var is204Available = bm.IsRoomAvailable(204, DateTime.Now);
            //This should be true. (Rooms are in Rooms table.)
            Console.WriteLine(is204Available);

            var availRooms = bm.GetAvailableRooms(DateTime.Now);
            availRooms.ToList().ForEach(ar => Console.WriteLine(ar));

        }
    }
}
