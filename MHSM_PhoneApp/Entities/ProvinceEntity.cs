using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHSM_PhoneApp.Entities
{
    class ProvinceEntity
    {
        public List<ProvinceData> Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    class ProvinceData
    {
        public string PROVINCE_NO { get; set; }
        public string PROVINCE_NAME { get; set; }
        public string SITE_NO { get; set; }
        public int IS_ACTIVE { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public DateTime MODIFIED_DATE { get; set; }
    }
}
