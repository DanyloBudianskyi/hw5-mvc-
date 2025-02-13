using System.Text.Json;

namespace hw5_mvc_.Models
{
    public class SkillService
    {
        public SkillService()
        {
            Load();
        }

        private string skillsDataFile = "Skills.json";

        private List<Skill> Skills { get; set; } = [];

        public void Load()
        {
            if (File.Exists(skillsDataFile))
            {
                Skills = JsonSerializer.Deserialize<List<Skill>>(File.ReadAllText(skillsDataFile));
            }
        }

        public List<Skill> GetAll()
        {
            return Skills;
        }

        public void Add(Skill model)
        {
            Skills.Add(model);
            SaveChanges();
        }
        public void Delete(Skill user)
        {
            if (user.Icon != null)
            {
                File.Delete($@"{Directory.GetCurrentDirectory()}/wwwroot/Icons/{user.Icon}");
            }

            Skills.Remove(user);
            SaveChanges();
        }
        public Skill? FindById(int id)
        {
            return Skills.FirstOrDefault(x => x.Id == id);
        }

        public void SaveChanges()
        {
            File.WriteAllText(skillsDataFile, JsonSerializer.Serialize(Skills));
        }
    }

}
