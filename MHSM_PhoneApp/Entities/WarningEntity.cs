using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MHSM_PhoneApp.Entities
{
    class WarningEntity
    {
        public DataWarning Data { get; set; }
    }
    class DataWarning
    {
        public List<Warning> list { get; set; }
    }
    class Warning
    {
        public string ElementName { get; set; }
        public string StationName { get; set; }
        public string ProvinceName { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
        public DateTime CreateDate { get; set; }
        public int IsRead { get; set; }
        public int ID { get; set; }
        public string[] UrlImage { get; set; }
        public string[] UrlVideo { get; set; }
    }
    class NowDataWarning
    {
        public string DataSave { get; set; }
        public string StationProvinceName { get; set; }
        public BitmapImage Icon { get; set; }
        public string Title { get; set; }
    }
    class WarningInfo
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
    class WarningDetailInfo{
            public DataDetaiWarning Data { get; set; }
    }
    class DataDetaiWarning
    {
        public DetaiWarning list { get; set; }
    }
    class DetaiWarning
    {
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
        public string TITLE_SPL { get; set; }
        public string ICON { get; set; }
    }
}
