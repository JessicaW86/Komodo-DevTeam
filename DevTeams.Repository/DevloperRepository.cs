//using for global 


using DevTeams.Data;

public class DevloperRepository
    {
        public DevloperRepository()
        {
            seed();
        }
        private List<Developer> _developerDb = new List<Developer>();

        private int _count = 0;

        public bool AddDeveloper(Developer developer)
        {
            if(developer is null)
            {
                return false;
            }
            else
            {
                _count++;
                developer.ID =  _count;
                _developerDb.Add(developer);
                return true;
            }
        }
        
        public List<Developer> GetAllDeveloper()
        {
            return _developerDb;
        }

        public Developer GetDeveloperById (int developerId)
        {
            foreach(Developer developer in _developerDb)
            {
                   if(developer.ID == developerId)
          {
            return developer;
          }
            }
          return null!;
        }
        

        public bool UpdateDeveloper(int developerId, Developer newDevData)
        {
            Developer oldDevData = GetDeveloperById(developerId);

            if(oldDevData != null)
            {
                oldDevData.FirstName = newDevData.FirstName;
                oldDevData.LastName = newDevData.LastName;
                oldDevData.HasPluralsight = newDevData.HasPluralsight;
                return true;
            }
            return false;
        }
        public bool DeleteDeveloper(int developerId)
        {
            Developer oldDevData = GetDeveloperById(developerId);
            if(oldDevData != null)
            {
                return _developerDb.Remove(oldDevData);
            }
            return false;
        }

public List<Developer> GetDevelopersWithoutPluralsight()
{
    List<Developer> devsWithoutPS = new List<Developer>();
    foreach(Developer developer in _developerDb)
    {
        if(developer.HasPluralsight == false)
        {
            devsWithoutPS.Add(developer);
        }
    }

    return devsWithoutPS;
}

        private void seed()
        {
            Developer Sidney = new Developer
            {
                FirstName = "Sindey",
                LastName = "Williams",
                HasPluralsight = true
            };

            Developer Noah = new Developer
             {
                FirstName = "Noah",
                LastName = "Brandon",
                HasPluralsight = true
             };

            Developer Sofi = new Developer
             {
                FirstName = "Sofi",
                LastName = "Watson",
                 HasPluralsight = false
             };



            AddDeveloper(Sidney);
            AddDeveloper(Noah);
            AddDeveloper(Sofi);
        }

    public List<Developer> GetAllDevelopers()
    {
        throw new NotImplementedException();
    }
}
