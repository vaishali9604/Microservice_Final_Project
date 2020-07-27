using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel
{
    public class LoggingInfo
    {


        public int Id { get; set; }
        public string Description { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public DateTime? RecordTimeStamp { get; set; }
    }
}
