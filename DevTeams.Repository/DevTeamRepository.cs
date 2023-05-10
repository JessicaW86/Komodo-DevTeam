


using DevTeams.Data;

public class DevTeamRepository
{
    private readonly DevloperRepository _devRepo;
    private List<DeveloperTeam> _devTeamDb = new List<DeveloperTeam>();
    public DevTeamRepository(DevloperRepository devRepo)
    {
        _devRepo = devRepo;
    }

    //we need a variable container that will hold the collection of Developers
    //we need to auto increment the developer id
    private int _count = 0;

    //C.R.U.D

    //Create
    public bool AddDevTeam(DeveloperTeam devTeam)
    {
        if (devTeam is null)
        {
            return false;
        }
        else
        {
            //increment the _count
            _count++;
            //assign the developerId to _count
            devTeam.ID = _count;
            //save to the database
            _devTeamDb.Add(devTeam);
            return true;
        }
    }

    //Read All
    public List<DeveloperTeam> GetDeveloperTeams()
    {
        return _devTeamDb;
    }

    //Read by Id
    public DeveloperTeam GetDeveloperTeam(int devTeamId)
    {
        foreach (DeveloperTeam team in _devTeamDb)
        {
            if (team.ID == devTeamId)
            {
                return team;
            }
        }
        // return _devTeamDb.SingleOrDefault(team => team.ID == devTeaamId)
        return null;
    }

    //public List<Developer>

    //Update - this is a COMPLETE overwrite of the data
    public bool UpdateDevTeam(int devTeamId, DeveloperTeam newDevTeamData)
    {
        DeveloperTeam oldDevTeamData = GetDeveloperTeam(devTeamId);
        
            if (oldDevTeamData != null)
            {
                oldDevTeamData.TeamName = newDevTeamData.TeamName;
                oldDevTeamData.Developers = newDevTeamData.Developers;
            return true;
            }
        
        return false;
    }
    //Delete
    public bool DeleteDevTeam(int devTeamId)
    {
        DeveloperTeam oldDevTeamData = GetDeveloperTeam(devTeamId);

        if (oldDevTeamData != null)
        {
            return _devTeamDb.Remove(oldDevTeamData);
        }
        return false;
    }

    public bool AddMultipleDevelopers(int devTeamId, List<Developer> developersToAdd)
    {
        DeveloperTeam teamInDb = GetDeveloperTeam(devTeamId);

        if (teamInDb != null)
        {
            teamInDb.Developers.AddRange(developersToAdd);
            return true;
        }
        return false;
    }

    //Seed
    public void Seed()
    {
        DeveloperTeam Js = new DeveloperTeam()
        {
            TeamName = "Java Script Devs"
        };
        Js.Developers.Add(_devRepo.GetDeveloperById(3));
        DeveloperTeam cSharp = new DeveloperTeam()
        {
            TeamName = "C# Sharp Devs",

        };
        cSharp.Developers.Add(_devRepo.GetDeveloperById(1));
        cSharp.Developers.Add(_devRepo.GetDeveloperById(2));

        DeveloperTeam java = new DeveloperTeam()
        {
            TeamName = "Java Devs"
        };

        AddDevTeam(Js);
        AddDevTeam(cSharp);
        AddDevTeam(java);

    }

}
