using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHSM_PhoneApp.Entities
{
    class StationDataDetailEntity
    {
        public List<StationDataDetail> Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    class StationDataDetail
    {
        public string STATION_NO { get; set; }
        public string STATION_NAME { get; set; }
        public string VTYPE_NAME { get; set; }
        public string PROVINCE_NAME { get; set; }
        public string ELEMENT_NAME { get; set; }
        public DateTime TIMESTAMP { get; set; }
        public float VALUE { get; set; }
        public string SYMBOL { get; set; }
        public float TOTAL { get; set; }
    }
}
