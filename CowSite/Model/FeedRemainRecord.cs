using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CowSite.Model
{
    public class FeedRemainRecord
    {
        public int ID { get; set; }
        public string GroupName { get; set; }
        public float RemainQuantity { get; set; }
        public DateTime RecordTime { get; set; }
    }
}