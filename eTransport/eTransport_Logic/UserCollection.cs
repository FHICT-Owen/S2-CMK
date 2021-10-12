using eTransport_Persistence.Factories;
using eTransport_Persistence.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace eTransport_Logic
{
    public class UserCollection
    {
        private static UserCollection _instance;
        private static readonly object Padlock = new object();
        public readonly List<User> _usersList;
        private readonly IUserDal _dal;
        public UserCollection()
        {
            _dal = UserFactory.CreateUserDal();
            _usersList = new List<User>();
        }

        public static UserCollection Instance
        {
            get
            {
                lock (Padlock)
                {
                    // ReSharper disable once InvertIf
                    if (_instance == null)
                    {
                        _instance = new UserCollection();
                        _instance.ReloadUsers();
                    }
                    return _instance;
                }
            }
        }

        private void ReloadUsers()
        {
            var users = _dal.GetUsers();
            _usersList.Clear();
            foreach (var dto in users)
            {
                _usersList.Add(new User(dto));
            }
        }

        public static bool CheckAdmin(string userId)
        {
            var targetUser = Instance._usersList.SingleOrDefault(user => user.UserId == userId);
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (targetUser == null) return false;
            return targetUser.IsAdmin;
        }

        public static IReadOnlyCollection<User> GetUsers()
        {
            return Instance._usersList.AsReadOnly();
        }
    }
}