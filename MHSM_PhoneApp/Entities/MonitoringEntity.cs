using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHSM_PhoneApp.Entities
{
    class MonitoringEntity
    {
        public List<MonitoringData> Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    class MonitoringData
    {
        public string STATION_NO { get; set; }
        public string STATION_NAME { get; set; }
        public string PROVINCE_NAME { get; set; }
        public string DATE_TIME { get; set; }
        public List<ListValueType> LIST_VALUE_TYPE { get; set; }
    }
    class ListValueType
    {
        public string VTYPE_ID { get; set; }
        public string VTYPE_NAME { get; set; }
        public string VALUE { get; set; }
        public string SYMBOL { get; set; }
    }
    class DataForScreen
    {
        public string STATION_NAME { get; set; }
        public string PROVINCE_NAME { get; set; }
        public string DAY { get; set; }
        public string HOUR { get; set; }
    }
    class DataDetailForScreen
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Content1 { get; set; }
    }
}
