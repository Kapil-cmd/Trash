using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class User
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class UserDetailsViewModel
    {
        public long UserId { get; set; }
            public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetName { get; set; }
        public long? CountryId { get; set; }   
        public long? CountyId { get; set; }   
        public long? AddressId { get; set; }
        public string ImageAddress { get; set; }
        public string PostalCode { get; set; }
    }
}
