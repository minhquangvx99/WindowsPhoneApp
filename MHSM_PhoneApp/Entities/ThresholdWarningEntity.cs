using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MHSM_PhoneApp.Entities
{
    class ThresholdWarningEntity
    {
        public ThresholdWarningData Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    class ThresholdWarningData{
        public List<ThresholdWarningDataList> list { get; set; }
        public int totalRow { get; set; }
        public int pageIndex { get; set; }
    }
    class ThresholdWarningDataList{
        public int Notification_ID { get; set; }
        public string WARNING_NAME { get; set; }
        public string WARNING_CONTENT { get; set; }
        public string VTYPE_NAME { get; set; }
        public string VTYPE_ID { get; set; }
        public string STATION_NAME { get; set; }
        public int ST_ID { get; set; }
        public string WARD_NAME { get; set; }
        public string DISTRICTS_NAME { get; set; }
        public string PROVINCE_NAME { get; set; }
        public int TYPEWARNING { get; set; }
        public DateTime STARTTIME_WARNING { get; set; }
        public string ELEMENT_NAME { get; set; }
        public float VALUE { get; set; }
        public string UNIT_NAME { get; set; }
        public int WARNING_LEVEL { get; set; }
        public string USER_RECEIVE_WARNING { get; set; }
        public int STATE { get; set; }
        public BitmapImage IsRead { get; set; }
        public string TITLE_SPL { get; set; }
        public string ICON { get; set; }
        public BitmapImage IconBitmapImage { get; set; }
    }
    class PostSearchAPI
    {
        public int ST_ID { get; set; }
        public string PROVINCE_NO { get; set; }
        public string STARTDATE { get; set; }
        public string ENDATE { get; set; }
        public int UserID { get; set; }
        public int PAGEINDEX { get; set; }
        public int PAGESIZE { get; set; }
    }
}
