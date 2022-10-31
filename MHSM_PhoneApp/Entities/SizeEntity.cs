using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHSM_PhoneApp.Entities
{
    class SizeEntity
    {
        public List<SizeData> Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    class SizeData
    {
        public string SITE_NO { get; set; }
        public string SITE_SHORTNAME { get; set; }
        public string SITE_NAME { get; set; }
        public int IS_ACTIVE { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public DateTime MODIFIED_DATE { get; set; }
    }
}