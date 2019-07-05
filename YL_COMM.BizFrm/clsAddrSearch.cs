using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YL_COMM.BizFrm
{
    public class clsAddrSearch
    {
        public class Meta
        {
            public int totalCount { get; set; }
            public int count { get; set; }
        }

        public class Place
        {
            public string name { get; set; }
            public string road_address { get; set; }
            public string jibun_address { get; set; }
            public string phone_number { get; set; }
            public string x { get; set; }
            public string y { get; set; }
            public double distance { get; set; }
            public string sessionId { get; set; }
        }

        public class RootObject
        {
            public string status { get; set; }
            public Meta meta { get; set; }
            public List<Place> places { get; set; }
            public string errorMessage { get; set; }
        }
    }
}
