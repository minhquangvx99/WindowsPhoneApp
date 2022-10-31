using MHSM_PhoneApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHSM_PhoneApp
{
    public class ConfigClass
    {
        public const string DOMAIN = "http://c2g5.thoitietnguyhiem.gov.vn/gateway/";

        // ACCOUNT
        public const string LOGIN = "user/mobile/login";
        public const string UPDATE_AVATAR = "user/UpdateAvatar";
        public const string LOGOUT = "user/mobile/logout";
        public const string CHANGE_PASSWORD = "user/mobile/changepassword";
        public const string REGISTER = "User/RegisterAccountMobile";
        public const string DELETE_ACCOUNT = "User/Delete/";

        // HOME
        public const string LIST_HOME_WARNING =
          "MNotification/getlistwarning_scHomeformobile?user_id=";
        public const string CHECKPOINT = "Map/checkpoint_inProvinceMobile";

        // PROJECT
        public const string LIST_PROJECT = "Project/getall";

        // LOCATION
        public const string LIST_SITE = "Site/getall";
        public const string LIST_PROVINCE = "Province/getall";
        public const string ALL_PROVINCE_BY_SITE = "Province/getallprovincebysite";

        // MEASURE
        public const string LIST_MEASURE_ELEMENT = "MeasureElement/getall";
        public const string LIST_MEASURE_ELEMENT_FILTER = "MeasureElement/getalldatafilter";

        // STATION
        public const string LIST_STATION = "Station/getfilterpagingformobileVer2";
        public const string DETAIL_STATION = "Station/detail/";
        public const string DATA_FILTER = "config/getfilterStation";
        public const string MONITORING_DATA = "MonitoringData/getmonitoringdatamobile";

        // MONITORING
        public const string DATA_MINING_REPORT = "MonitoringData/getdataminingmobilereport";

        // EXPLOIT
        public const string LIST_STATION_BY_MULTIOPTION =
          "MonitoringData/getliststationbymultioption";
        public const string EXPLOIT_DATA = "MonitoringData/getmonitoringmobilereport";

        // SENSOR
        public const string LIST_SENSOR = "Sensor/getfilterpaging";

        // WARNING
        public const string LIST_WARNING_THRESHOLD = "MNotification/getlistnotiformobile";
        public const string LIST_STATION_BY_PROVINCE = "Station/getStaionByProvince";
        public const string WARNING_THRESHOLD_DETAILS =
          "MNotification/getnotidetailformobile?Notification_ID=";
        public const string LIST_CALAMITY = "WarningDisaster/getlistnoti_disasterformobile";
        public const string LIST_DISTRICT_BY_PROVINCE = "District/getalldistrictbyprovince";
        public const string LIST_NOTI_TYPE = "WarningDisasterType/getall";
        public const string CALAMITY_DETAILS = "WarningDisaster/getnotidetail_disasterformobile?id=";
        public const string SEND_WARNING = "WarningDisaster/createoredit?type=0";
        public const string UPDATE_STATE_NOTI = "MNotification/updatestatewarning?";
        public const string UPLOAD_MULTI_FILES = "warningdisaster/UploadMultiFile";

        // WEATHER
        public const string LIST_WEATHER = "";
        public const string DOMAIN_WEATHER = "https://m.api.weathervietnam.vn/";
        public const string WEATHER_NOW = "api/weather/realdata?Lat=21.01609&Lon=105.811644";
        public const string WEATHER_DETAIL = "api/weather/detaildaily?Lat=21.0445866&Lon=105.7827859";
        public const string WEATHER_BY_DAY = "api/weather/daily?Lat=21.01609&Lon=105.811644";

        // LOGGER
        public const string LOGGER_DETAILS = "DataLoggerType/GetByID";

        // VTYPE
        public const string VTYPE_LIST = "ValueType/getbystation";
        public const string VTYPE_LIST_BY_ELEMENT = "ValueType/getvtypebyelement?element_id=";


        // REVIEW
        public const string REVIEW = "Config/GetAllConfigEnvironment";

        public static string[] MONITORING_SYSTEM = {"","Hệ thống quan trắc quốc gia","Hệ thống quan trắc chuyên dùng","Hệ thống quan trắc quốc tế"};

        public const int automaticHydrology = 15;
        public const int waterResource = 14;
        public const int automaticMeteorology = 16;
        public const string Uri = "http://c2g5.thoitietnguyhiem.gov.vn/pages/MonitoringMapMobile";


        // Public Info
        public static DataUser UserData = new DataUser();

    }
}
