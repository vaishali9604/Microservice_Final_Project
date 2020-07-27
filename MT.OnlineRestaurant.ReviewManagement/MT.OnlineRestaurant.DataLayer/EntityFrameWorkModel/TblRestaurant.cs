using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel
{
    public partial class TblRestaurant
    {
        public string Name { get; set; }

        public string ContactNo { get; set; }
        public int Id { get; set; }
        public int UserCreated { get; set; }
        public int UserModified { get; set; }
        public DateTime RecordTimeStamp { get; set; }
        public DateTime RecordTimeStampCreated { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string OpeningTime { get; set; }
        public string CloseTime { get; set; }
        public ICollection<TblRatingandReviews> TblRatingandReviews { get; set; }
    }
}
