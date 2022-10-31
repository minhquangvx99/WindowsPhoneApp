using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHSM_PhoneApp.Entities
{
    class StationEntity
    {
        public StationData Data { get; set; }
    }
    class StationData{
        public int pageIndex { get; set; }
        public List<StationInfo> list { get; set; }
    }
    class DataOfStation
    {
        public StationInfo Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    class StationInfo{
        public int PROJECT_ID { get; set; }
        public string PROJECT_NAME { get; set; }
        public int ST_ID { get; set; }
        public string STATION_ID { get; set; }
        public string STATION_NO { get; set; }
        public string STATION_NAME { get; set; }
        public string STATION_LONGNAME { get; set; }
        public string COUNTRY_NO { get; set; }
        public string COUNTRY_NAME { get; set; }
        public string SITE_NO { get; set; }
        public string SITE_NAME { get; set; }
        public string PROVINCE_NO { get; set; }
        public string PROVINCE_NAME { get; set; }
        public string DISTRICT_NO { get; set; }
        public string DISTRICT_NAME { get; set; }
        public string WARD_NO { get; set; }
        public string WARD_NAME { get; set; }
        public string RIVER_NO { get; set; }
        public string RIVER_NAME { get; set; }
        public float STATION_LATITUDE { get; set; }
        public float STATION_LONGITUDE { get; set; }
        public string STATION_ADDRESS { get; set; }
        public DateTime EFFECTIVE_DATE { get; set; }
        public DateTime EXPIRATION_DATE { get; set; }
        public int DATA_TYPE_LOGGER_ID { get; set; }
        public string DATA_TYPE_LOGGER { get; set; }
        public string METHOD { get; set; }
        public int IS_ACTIVE { get; set; }
        public string IS_ACTIVE_STRING { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public string CREATE_DATE_STRING { get; set; }
        public DateTime MODIFIED_DATE { get; set; }
        public string MODIFIED_DATE_STRING { get; set; }
        public int MONITORING_SYSTEM_TYPE { get; set; }
        public string MONITORING_SYSTEM_TYPE_STRING { get; set; }
        public string STATION_HEIGHT { get; set; }
        public int TFREQ_ID { get; set; }
        public string STATION_TYPE { get; set; }
    }
    class PostStation
    {
        public string STATION_NO_OR_NAME { get; set; }
        public string METHOD { get; set; }
        public string PROJECT_ID { get; set; }
        public string SITE_NO { get; set; }
        public string PROVINCE_NO { get; set; }
        public int IS_ACTIVE { get; set; }
        public int PAGEINDEX { get; set; }
        public int PAGESIZE { get; set; }
    }

    class StationDataEntity{
        public DataStation Data { get; set; }
    }
    class DataStation{
        public int totalRow { get; set; }
        public List<ListDataStation> list { get; set; }
    }
    class ListDataStation{
        public string VTYPE_ID { get; set; }
        public string VTYPE_NO { get; set; }
        public string VTYPE_NAME { get; set; }
        public string ELEMENT_ID { get; set; }
        public string ELEMENT_NAME { get; set; }
        public DateTime TIMESTAMP { get; set; }
        public string TIMESTAMP_STRING { get; set; }
        public float Value { get; set; }
        public string UnitName { get; set; }
        public string DataSave { get; set; }
        public string STATION_NAME { get; set; }
        public string PROVINCE_NAME { get; set; }
    }
}
