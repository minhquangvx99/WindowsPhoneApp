using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHSM_PhoneApp.Entities
{
    class DistrictByProvinceEntity
    {
        public DistrictByProvinceData Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    class DistrictByProvinceData
    {
        public List<DistrictByProvinceDataList> list { get; set; }
    }
    class DistrictByProvinceDataList
    {
        public List<DistrictInProvinceData> DistrictInProvince { get; set; }
        public string PROVINCE_NO { get; set; }
        public string PROVINCE_NAME { get; set; }
    }
    class DistrictInProvinceData
    {
        public string DISTRICT_NO { get; set; }
        public string DISTRICT_NAME { get; set; }
    }

}
