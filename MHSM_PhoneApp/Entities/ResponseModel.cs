using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHSM_PhoneApp.Entities
{
    class ResponseModel
    {
        public DataResponse Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Trace { get; set; }
    }
    class DataResponse
    {
        public bool Success { get; set; }
        public DataUser Data { get; set; }
    }
    public class DataUser
    {
        public User user { get; set; }
        public string token { get; set; }
    }
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public int PosittionId { get; set; }
        public int DepartmentId { get; set; }
        public int Status { get; set; }
        public string Avatar { get; set; }
        public string RoleIds { get; set; }
    }
}
