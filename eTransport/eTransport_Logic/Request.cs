using System.ComponentModel.DataAnnotations;
using eTransport_Persistence.Factories;
using eTransport_Persistence.Interfaces;
using eTransport_Utility;
using eTransport_Utility.Exceptions;
using System.Linq;

namespace eTransport_Logic
{
    public class Request
    {
        #region properties
        private readonly IUserDal _dal;
        [Required]
        public int RequestId { get; private set; }
        [Required]
        public string Materials { get; private set; }
        [Required]
        public MaterialState MaterialType { get; private set; }
        [Required]
        public float CargoHeight { get; private set; }
        [Required]
        public float CargoWidth { get; private set; }
        [Required]
        public float CargoLength { get; private set; }
        [Required]
        public float CargoVolume { get; private set; }
        [Required]
        public string PickupLocation { get; private set; }
        public string Comments { get; private set; }
        [Required]
        public bool IsApproved { get; private set; }
        [Required]
        public bool IsComplete { get; private set; }
        [Required]
        public bool IsLinked { get; private set; }
        #endregion

        public Request(int requestId, string materials, MaterialState materialType, float cargoHeight, float cargoWidth, float cargoLength, float cargoVolume, string pickupLocation, string comments, bool isApproved, bool isComplete, bool isLinked)
        {
            RequestId = requestId;
            Materials = materials;
            MaterialType = materialType;
            CargoHeight = cargoHeight;
            CargoWidth = cargoWidth;
            CargoLength = cargoLength;
            CargoVolume = cargoVolume;
            PickupLocation = pickupLocation;
            Comments = comments;
            IsApproved = isApproved;
            IsComplete = isComplete;
            IsLinked = isLinked;
            _dal = UserFactory.CreateUserDal();
        }

        public Request(RequestDto requestDto)
            : this(requestDto.RequestId, requestDto.Materials, requestDto.MaterialType, requestDto.CargoHeight, requestDto.CargoWidth, requestDto.CargoLength, requestDto.CargoVolume, requestDto.PickupLocation, requestDto.Comments, requestDto.IsApproved, requestDto.IsComplete, requestDto.IsLinked)
        {
        }

        public RequestDto ConvertToDto()
        {
            return new RequestDto(RequestId, Materials, MaterialType, CargoHeight, CargoWidth, CargoLength, CargoVolume, PickupLocation, Comments, IsApproved, IsComplete, IsLinked);
        }

        public void EditId(Request request, int id)
        {
            request.RequestId = id;
        }

        public static RequestDto GetRequest(string userId, int requestId)
        {
            var user = UserCollection.Instance._usersList.SingleOrDefault(result => result.UserId == userId);
            var entry = user?.RequestList.SingleOrDefault(result => result.RequestId == requestId);
            return entry?.ConvertToDto();
        }

        public bool EditRequest(string userId, string materials, MaterialState materialType, float cargoHeight, float cargoWidth, float cargoLength, float cargoVolume, string pickupLocation, string comments)
        {
            if (IsComplete) throw new LogicException("This request has already been completed!");
            Materials = materials;
            MaterialType = materialType;
            CargoHeight = cargoHeight;
            CargoWidth = cargoWidth;
            CargoLength = cargoLength;
            CargoVolume = cargoVolume;
            PickupLocation = pickupLocation;
            Comments = comments;
            // Automatically sets approval to false when edit is used
            IsApproved = false;

            return _dal.EditRequest(userId, ConvertToDto());
        }

        public bool Link(string userId)
        {
            if (IsLinked) throw new LogicException("This request is already linked!");
            if (IsApproved) throw new LogicException("This request has already been approved!");
            if (IsComplete) throw new LogicException("This request has already been completed!");
            IsLinked = true;

            return _dal.EditRequest(userId,ConvertToDto());
        }

        public bool Unlink(string userId)
        {
            if (IsLinked == false) throw new LogicException("This request is already unlinked!");
            if (IsApproved) throw new LogicException("This request has already been approved!");
            if (IsComplete) throw new LogicException("This request has already been completed!");
            IsLinked = false;

            return _dal.EditRequest(userId, ConvertToDto());
        }

        public bool Approve(string userId, string adminId, int requestId)
        {
            if (UserCollection.CheckAdmin(adminId) == false) throw new PermissionException("You do not have enough privileges!");
            var user = UserCollection.Instance._usersList.SingleOrDefault(result => result.UserId == userId);
            var entry = user?.RequestList.SingleOrDefault(result => result.RequestId == requestId);
            if (entry == null) return false;
            if (entry.IsLinked == false) return false;
            if (entry.IsApproved) return false;
            if (entry.IsComplete) return false;
            entry.IsApproved = true;

            return _dal.EditRequest(userId, entry.ConvertToDto());
        }

        public bool Complete(string userId, string adminId, int requestId)
        {
            if(UserCollection.CheckAdmin(adminId) == false) throw new PermissionException("You do not have enough privileges!");
            var user = UserCollection.Instance._usersList.SingleOrDefault(result => result.UserId == userId);
            var entry = user?.RequestList.SingleOrDefault(result => result.RequestId == requestId);
            if (entry == null) return false;
            if (entry.IsLinked == false) return false;
            if (entry.IsApproved == false) return false;
            if (entry.IsComplete) return false;
            entry.IsComplete = true;

            return _dal.EditRequest(userId, entry.ConvertToDto());
        }
    }
}
