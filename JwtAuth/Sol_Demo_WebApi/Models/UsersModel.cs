using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Sol_Demo_WebApi.Models
{
    [DataContract]
    public class UsersModel
    {
        [DataMember(EmitDefaultValue = false)]
        public int? Id { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String FullName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String Role { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String UserName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String Password { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime DateOfBirth { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String Token { get; set; }
    }
}