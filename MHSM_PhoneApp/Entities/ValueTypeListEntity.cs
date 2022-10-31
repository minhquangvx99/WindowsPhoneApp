using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHSM_PhoneApp.Entities
{
    class ValueTypeListEntity
    {
        public List<ValueTypeData> Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    class ValueTypeData
    {
        public string VTYPE_ID { get; set; }
        public string VTYPE_NO { get; set; }
        public int MFREQ_ID { get; set; }
        public string ELEMENT_ID { get; set; }
        public string VTYPE_NAME { get; set; }
        public int IS_ACTIVE { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public DateTime MODIFIED_DATE { get; set; }
        public int UNIT_ID { get; set; }
        public string TABLE_STORAGE { get; set; }
    }
}
