using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHSM_PhoneApp.Entities
{
    class ProjectEntity
    {
        public List<ProjectData> Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    class ProjectData
    {
        public int PROJECT_ID { get; set; }
        public string PROJECT_NO { get; set; }
        public string PROJECT_NAME { get; set; }
        public int IS_ACTIVE { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public DateTime MODIFIED_DATE { get; set; }
        public int ICON_ACTIVE { get; set; }
        public int ICON_UNACTIVE { get; set; }
    }
}
