using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHSM_PhoneApp.Entities
{
    class NotiTypeEntity
    {
        public List<NotiTypeData> Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    class NotiTypeData
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public int ICONID { get; set; }
        public int IS_ACTIVE { get; set; }
    }
}
