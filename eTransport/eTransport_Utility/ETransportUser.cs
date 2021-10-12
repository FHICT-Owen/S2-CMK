using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace eTransport_Utility
{
    public class ETransportUser : IdentityUser
    {
        [PersonalData]
        public bool IsAdmin { get; set; }
        [PersonalData] public List<RequestDto> RequestList { get; set; } = new List<RequestDto>();
    }
}