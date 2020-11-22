using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelBookingsApp
{
    public class BookingsManager : IBookingsManager
    {
        public void AddBooking(string guest, int room, DateTime date)
        {
            if (!IsRoomAvailable(room, date))
                throw new Exception("Room isn't available on that date.");

            using (var db = new HotelBookingsDBContext())
            {
                var id = db.Rooms.Where(r => r.RoomId == room).Select(r => r.Id).FirstOrDefault();

                var bookedRoom = new BookedRoom
                {
                    RoomId = id,
                    BookingDate = date.ToShortDateString(),
                    GuestName = guest
                }; ;

                db.BookedRooms.Add(bookedRoom);
                db.SaveChanges();
            }
        }

        public IEnumerable<int> GetAvailableRooms(DateTime date)
        {
            using (var db = new HotelBookingsDBContext())
            {
                var dateString = date.ToShortDateString();

                var bookedRooms = db.BookedRooms
                        .Where(b => b.BookingDate == dateString)
                        .Select(b => b.RoomId);

                var roomIds = db.Rooms.Select(r => r.Id).ToList();
                var availRooms = new List<int>();

                foreach (var room in bookedRooms)
                {
                    if (roomIds.Contains(room))
                        roomIds.Remove(room);
                }

                foreach (var roomId in roomIds)
                    availRooms.Add(db.Rooms.Where(r => r.Id == roomId).Select(r => r.RoomId).FirstOrDefault());

                return availRooms;
            }
        }

        public bool IsRoomAvailable(int room, DateTime date)
        {
            using (var db = new HotelBookingsDBContext())
            {
                var dateString = date.ToShortDateString();
                var isBooked = db.BookedRooms.Any(b => b.Room.RoomId == room && b.BookingDate == dateString);

                return !isBooked;
            }
        }
    }
}
