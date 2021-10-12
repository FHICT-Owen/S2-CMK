using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eTransport_Utility
{
    public class UserDto
    {
        #region properties
        [Key]
        [Required]
        public string UserId { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
        public List<RequestDto> RequestList { get; set; }
        #endregion

        public UserDto(string userId, bool isAdmin, List<RequestDto> requestList)
        {
            UserId = userId;
            IsAdmin = isAdmin;
            RequestList = requestList;
        }
    }
}
