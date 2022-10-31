using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHSM_PhoneApp.Entities
{
    class DataMiningEntity
    {
        public List<DataMining> Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    class DataMining
    {
        public int ST_ID { get; set; }
        public string STATION_NAME { get; set; }
        public string PROVINCE_NAME { get; set; }
        public string DISTRICT_NAME { get; set; }
        public string WARD_NAME { get; set; }
    }
    class DataMiningDetailEntity
    {
        public List<DataMiningDetail> Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    class DataMiningDetail
    {
        public string STATION_NO { get; set; }
        public string STATION_NAME { get; set; }
        public string DATA_TIME { get; set; }
        public string SYMBOL { get; set; }
        public string VTYPE_NAME { get; set; }
        public string VALUE { get; set; }
        public string TOTAL { get; set; }
    }
}
