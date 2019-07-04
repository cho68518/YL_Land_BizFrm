using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YL_COMM.BizFrm
{
    public class clsAddress
    {
        public class Meta
        {
            public int totalCount { get; set; }
            public int page { get; set; }
            public int count { get; set; }
        }

        public class AddressElement
        {
            public List<string> types { get; set; }
            public string longName { get; set; }
            public string shortName { get; set; }
            public string code { get; set; }
        }

        public class Address
        {
            public string roadAddress { get; set; }
            public string jibunAddress { get; set; }
            public string englishAddress { get; set; }
            public List<AddressElement> addressElements { get; set; }
            public string x { get; set; }
            public string y { get; set; }
            public double distance { get; set; }
        }

        public class RootObject
        {
            public string status { get; set; }
            public Meta meta { get; set; }
            public List<Address> addresses { get; set; }
            public string errorMessage { get; set; }
        }

    }
}
