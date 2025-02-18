using hw5_mvc_.Controllers;
using System.Text.Json;

namespace hw5_mvc_.Models
{
    public class UserSkillService
    {
        public UserSkillService()
        {
            Load();
        }

        private string userDataFile = "UserSkills.json";

        private List<UserSkill> UserSkills { get; set; } = new List<UserSkill> ();

        public void Load()
        {
            if (File.Exists(userDataFile))
            {
                UserSkills = JsonSerializer.Deserialize<List<UserSkill>>(File.ReadAllText(userDataFile));
            }
        }

        public List<UserSkill> GetAll()
        {
            return UserSkills;
        }

        public void Add(UserSkill model)
        {
            UserSkills.Add(model);
        }
        public void Delete(UserSkill user)
        {
            UserSkills.Remove(user);
        }
        public UserSkill? FindById(int id)
        {
            return UserSkills.FirstOrDefault(x => x.Id == id);
        }
        public void SaveChanges()
        {
            File.WriteAllText(userDataFile, JsonSerializer.Serialize(UserSkills));
        }
    }

}
