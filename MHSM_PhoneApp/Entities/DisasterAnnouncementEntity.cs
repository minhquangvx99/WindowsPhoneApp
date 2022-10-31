using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MHSM_PhoneApp.Entities
{
    class DisasterAnnouncementEntity
    {
        public DisasterAnnouncemenData Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    class DisasterAnnouncemenData
    {
        public List<DisasterAnnouncemenDataList> list { get; set; }
        public int totalRow { get; set; }
        public int pageIndex { get; set; }
    }
    class DisasterAnnouncementDetailEntity
    {
        public DisasterAnnouncemenDetailData Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    class DisasterAnnouncemenDetailData
    {
        public DisasterAnnouncemenDataList list { get; set; }
    }
    class DisasterAnnouncemenDataList
    {
        public int WARNING_ID { get; set; }
        public string WARNING_TITLE { get; set; }
        public string WARNING_CONTENT { get; set; }
        public string PROVINCE_NAME { get; set; }
        public int USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string LATTITUDE { get; set; }
        public string LONGTITUDE { get; set; }
        public string DISTRICTS_NAME { get; set; }
        public string DISTRICT_NO { get; set; }
        public string PHONENUMBER { get; set; }
        public string EMAIL { get; set; }
        public int WARNINGTYPE { get; set; }
        public int STATE { get; set; }
        public DateTime STARTTIME_WARNING { get; set; }
        public string[] URL_IMAGE_SPLIT { get; set; }
        public string[] URL_VIDEO_SPLIT { get; set; }
        public string ICON { get; set; }
        public int NOTIFICATION_ID { get; set; }
        public BitmapImage IconBitmapImage { get; set; }
        public BitmapImage IsRead { get; set; }
    }
    class PostDisasterAnnouncemenSearchAPI
    {
        public string DISTRICT_NO { get; set; }
        public string PROVINCE_NO { get; set; }
        public int USER_ID { get; set; }
        public int TYPE_WARNING { get; set; }
        public string STARTDATE { get; set; }
        public string ENDATE { get; set; }
        public int PAGEINDEX { get; set; }
        public int PAGESIZE { get; set; }
    }
}
