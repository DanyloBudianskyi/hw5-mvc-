using hw5_mvc_.Controllers;
using System.Text.Json;

namespace hw5_mvc_.Models
{
    public class UserInfoService
    {
        public UserInfoService()
        {
            Load();
        }

        private string userDataFile = "Users.json";

        private List<UserInfo> UserInfos { get; set; } = [];

        public void Load()
        {
            if (File.Exists(userDataFile))
            {
                UserInfos = JsonSerializer.Deserialize<List<UserInfo>>(File.ReadAllText(userDataFile));
            }
        }

        public List<UserInfo> GetAll()
        {
            return UserInfos;
        }

        public void Add(UserInfo model)
        {
            UserInfos.Add(model);
        }
        public void Delete(UserInfo user)
        {
            UserInfos.Remove(user);
        }
        public UserInfo? FindById(int id)
        {
            return UserInfos.FirstOrDefault(x => x.Id == id);
        }

        public void SaveChanges()
        {
            File.WriteAllText(userDataFile, JsonSerializer.Serialize(UserInfos));
        }
    }

}
