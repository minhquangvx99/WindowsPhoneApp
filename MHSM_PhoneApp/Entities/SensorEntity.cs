using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MHSM_PhoneApp.Entities
{
    class SensorEntity
    {
        public SensorData Data { get; set; }
    }
    class SensorData
    {
        public int totalRow { get; set; }
        public int pageIndex { get; set; }
        public List<SensorInfo> list { get; set; }
    }
    class SensorInfo
    {
        public int SENSOR_ID { get; set; }
        public string SENSOR_NO { get; set; }
        public string STATION_NO { get; set; }
        public string STATION_NAME { get; set; }
        public string PROJECT_NO { get; set; }
        public string PROJECT_NAME { get; set; }
        public string SENSOR_NAME { get; set; }
        public float ABSOLUTE_HIGHT { get; set; }
        public int CONFIG_ID { get; set; }
        public int MFREQ_ID { get; set; }
        public string MFREQ_VALUE { get; set; }
        public string VTYPE_NAME { get; set; }
        public string VTYPE_ID { get; set; }
        public string VTYPE_NO { get; set; }
        public int IS_ACTIVE_CONFIG { get; set; }
        public string IS_ACTIVE_CONFIG_STRING { get; set; }
        public string ELEMENT_ID { get; set; }
        public string ELEMENT_NAME { get; set; }
        public int IS_ACTIVE { get; set; }
        public int PRIORITY { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public DateTime MODIFIED_DATE { get; set; }
    }
    class PostSensor
    {
        public string SENSOR_NO { get; set; }
        public string SENSOR_NAME { get; set; }
        public string STATION_NO { get; set; }
        public string PROJECT_NO { get; set; }
        public int ABSOLUTE_HIGHT { get; set; }
        public int MFREQ_ID { get; set; }
        public string ELEMENT_ID { get; set; }
        public string VTYPE_ID { get; set; }
        public int IS_ACTIVE { get; set; }
        public int PAGEINDEX { get; set; }
        public int PAGESIZE { get; set; }
    }
    class SensorList
    {
        public string SENSOR_NAME { get; set; }
        public string PROJECT_NO { get; set; }
        public string ELEMENT_NAME { get; set; }
        public string SENSOR_NO { get; set; }
        public BitmapImage ACTIVE_IMAGE { get; set; }
    }
}
