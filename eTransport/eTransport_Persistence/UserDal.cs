using eTransport_Persistence.DbContext;
using eTransport_Persistence.Interfaces;
using eTransport_Utility;
using System.Collections.Generic;
using System.Linq;

namespace eTransport_Persistence
{
    public class UserDal : IUserDal
    {
        public List<UserDto> GetUsers()
        {
            var context = new DalDbContext();
            var list = context.Users.Select(u => new UserDto(u.Id, u.IsAdmin, u.RequestList)).ToList();
            return list;
        }

        public int AddRequest(string userId, RequestDto request)
        {
            var context = new DalDbContext();
            var user = context.Users.SingleOrDefault(res => res.Id == userId);
            user?.RequestList.Add(request);
            context.SaveChanges();
            var id = request.RequestId;
            return id;
        }

        public bool RemoveRequest(string userId, RequestDto request)
        {
            var context = new DalDbContext();
            var user = context.Users.SingleOrDefault(res => res.Id == userId);
            var entry = user?.RequestList.SingleOrDefault(result => result.RequestId == request.RequestId);
            if (entry == null) return false;
            user?.RequestList.Remove(entry);
            return context.SaveChanges() >= 0;
        }

        public List<RequestDto> GetRequests(string userId)
        {
            using var context = new DalDbContext();
            var user = context.Users.SingleOrDefault(res => res.Id == userId);
            var requestList = user?.RequestList;
            if (requestList == null) return new List<RequestDto>();
            return requestList;
        }

        public bool EditRequest(string userId, RequestDto request)
        {
            var context = new DalDbContext();
            var user = context.Users.SingleOrDefault(res => res.Id == userId);
            var entry = user?.RequestList.SingleOrDefault(result => result.RequestId == request.RequestId);
            if (entry == null) return false;
            context.Entry(entry).CurrentValues.SetValues(request);
            return context.SaveChanges() >= 0;
        }
    }
}
