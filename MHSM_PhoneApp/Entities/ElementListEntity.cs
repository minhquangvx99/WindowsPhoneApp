using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHSM_PhoneApp.Entities
{
    class ElementListEntity
    {
        public List<ElementData> Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    class ElementData{
        public string ELEMENT_ID { get; set; }
        public string ELEMENT_NO { get; set; }
        public string ELEMENT_NAME { get; set; }
        public int IS_ACTIVE { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public DateTime MODIFIED_DATE { get; set; }
    }
}
