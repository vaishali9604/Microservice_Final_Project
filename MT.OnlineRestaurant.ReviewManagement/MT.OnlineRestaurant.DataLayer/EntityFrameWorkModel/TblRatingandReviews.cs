using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel
{
    public partial class TblRatingandReviews
    {
        public int Rating { get; set; }
        public string Reviews { get; set; }
        public int TblRestaurantId { get; set; }
        public int Id { get; set; }
        public int? TblCustomerId { get; set; }
        public int? UserCreated { get; set; }
        public int UserModified { get; set; }
        public DateTime RecordTimeStamp { get; set; }
        public DateTime RecordTimeStampCreated { get; set; }

        public TblRestaurant TblRestaurant { get; set; }
        
    }
}
