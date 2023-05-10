//ctrl + b -> hide explore
namespace DevTeams.Data
{
    public class DeveloperTeam
    {
        //empty constructor
        public DeveloperTeam(){}
       
        //partial constructor
        public DeveloperTeam(string teamName)
        {
            TeamName = teamName;
        }

        //full constructor
        public DeveloperTeam(string teamName, List<Developer> developers)
        {
            TeamName = teamName;
            Developers = developers;
        }
        //[Key]
        public int ID { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public List<Developer> Developers { get; set; } = new List<Developer>();
    }
}