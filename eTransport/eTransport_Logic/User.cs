using eTransport_Persistence.Factories;
using eTransport_Persistence.Interfaces;
using eTransport_Utility;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace eTransport_Logic
{
    public class User
    {
        #region properties
        private readonly IUserDal _dal;
        [Required]
        public string UserId { get; }
        [Required]
        public bool IsAdmin { get; }
        public List<Request> RequestList { get; }
        #endregion

        public User(string userId, bool isAdmin, List<Request> requestList)
        {
            UserId = userId;
            IsAdmin = isAdmin;
            RequestList = requestList;
            _dal = UserFactory.CreateUserDal();
        }

        private static List<Request> ConvertList(UserDto userDto)
        {
            var list = userDto.RequestList;
            return list.Select(entry => new Request(entry)).ToList();
        }

        private static List<RequestDto> ConvertList(IEnumerable<Request> requests)
        {
            return requests.Select(entry => new RequestDto(entry.RequestId, entry.Materials, entry.MaterialType, entry.CargoHeight, entry.CargoWidth, 
                entry.CargoLength, entry.CargoVolume, entry.PickupLocation, entry.Comments, entry.IsApproved, entry.IsComplete, entry.IsLinked)).ToList();
        }

        public User(UserDto userDto) : this(userDto.UserId, userDto.IsAdmin, ConvertList(userDto))
        {
        }

        public Request FindRequest(int requestId)
        {
            var foundRequest = RequestList.SingleOrDefault(result => result.RequestId == requestId);
            return foundRequest;
        }

        public int CreateRequest(string materials, MaterialState materialType, float cargoHeight, float cargoWidth, float cargoLength, float cargoVolume, string pickupLocation, string comments)
        {
            var request = new Request(0, materials, materialType, cargoHeight, cargoWidth, cargoLength, cargoVolume, pickupLocation, comments, false, false, false);
            var requestId = _dal.AddRequest(UserId, request.ConvertToDto());
            request.EditId(request, requestId);
            RequestList.Add(request);
            return requestId;
        }

        public bool RemoveRequest(int requestId)
        {
            var request = RequestList.FirstOrDefault(requestEntry => requestEntry.RequestId == requestId);
            if (request == null || request.IsApproved || request.IsComplete || request.IsLinked) return false;
            RequestList.Remove(request);
            return _dal.RemoveRequest(UserId, request.ConvertToDto());
        }

        public void ReloadRequests()
        {
            var requests = _dal.GetRequests(UserId);
            RequestList.Clear();
            foreach (var dto in requests)
            {
                RequestList.Add(new Request(dto));
            }
        }

        public IReadOnlyCollection<RequestDto> GetRequests()
        {
            var convertedList = RequestList.Select(request => new RequestDto(request.RequestId, request.Materials, request.MaterialType, request.CargoHeight, request.CargoWidth, request.CargoLength, request.CargoVolume, request.PickupLocation, request.Comments, request.IsApproved, request.IsComplete, request.IsLinked)).ToList();
            return convertedList.AsReadOnly();
        }
    }
}
