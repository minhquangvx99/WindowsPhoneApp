using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHSM_PhoneApp.Entities
{
    class FilterStationEntity
    {
        public FilterStationData Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    class FilterStationData
    {
        public List<MeasureEntity> list_measure { get; set; }
        public List<ValueTypeEntity> list_valuetype { get; set; }
    }
    class MeasureEntity
    {
        public string ELEMENT_ID { get; set; }
        public string ELEMENT_NAME { get; set; }
    }
    class ValueTypeEntity
    {
        public string VTYPE_ID { get; set; }
        public string VTYPE_NAME { get; set; }
        public string ELEMENT_ID { get; set; }
    }
}
