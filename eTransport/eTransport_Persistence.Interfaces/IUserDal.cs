using System.Collections.Generic;
using eTransport_Persistence.Interfaces;
using eTransport_Utility;

namespace eTransport_Persistence.Interfaces
{
    public interface IUserDal
    {
        List<UserDto> GetUsers();
        int AddRequest(string userId, RequestDto request);
        bool RemoveRequest(string userId, RequestDto request);
        List<RequestDto> GetRequests(string userId);
        bool EditRequest(string userId, RequestDto request);
    }
}