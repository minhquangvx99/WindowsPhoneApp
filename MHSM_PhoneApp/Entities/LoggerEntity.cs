using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHSM_PhoneApp.Entities
{
    class LoggerEntity
    {
        public LoggerData Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    class LoggerData
    {
        public int DATA_TYPE_LOGGER_ID { get; set; }
        public string CONFIGURATION { get; set; }
        public string MODEM { get; set; }
        public string VERSION { get; set; }
        public string FIRMWARE { get; set; }
        public string STATUS { get; set; }
        public string LOGGER_TYPE { get; set; }
        public string CREATE_BY { get; set; }
        public string MODIFIED_BY { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public DateTime MODIFIED_DATE { get; set; }
    }
}
