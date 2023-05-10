
using DevTeams.Data;

public class ProgramUI
{
    //Globally scoped variable container with the Developer Repository Data
    private readonly DevloperRepository _dRepo = new DevloperRepository();
    private DevTeamRepository _dTRepo;
    public ProgramUI()
    {
        _dTRepo = new DevTeamRepository(_dRepo);
    }

    public void Run()
    {
        RunApplication();
    }

    private void RunApplication()
    {
        bool isRunning = true;
        while (isRunning)
        {
            Console.Clear();
            System.Console.WriteLine("Welcome to Komodo DevTeams\n" +
            "=======Developer Management==========\n" +
            "1. View All Developers\n" +
            "2. View Developers by Id\n" +
            "3. Add Developer\n" +
            "4. Update Existing Developer\n" +
            "5. Delete Existing Developer\n\n" +
            "==========Dev Team Management========\n" +
            "6. View All DevTeams\n" +
            "7. View DevTeam by Id\n" +
            "8. Add DevTeam\n" +
            "9. Update Existing DevTeam\n" +
            "10. Delete Existing DevTeam\n\n" +
            "=========Bonus===========\n" +
            "11. Developers with Pluralsight Account\n" +
            "12. Add Multiple Developers to a team\n" +
            "==========Exit App===========\n" +
            "00. Exit Application");

            string userInput = Console.ReadLine()!;

            switch (userInput)
            {
                case "1":
                    ViewAllDevelopers();
                    break;
                case "2":
                    ViewDeveloperById();
                    break;
                case "3":
                    AddDeveloper();
                    break;
                case "4":
                    UpdateExistingDeveloper();
                    break;
                case "5":
                    DeleteExistingDeveloper();
                    break;
                case "6":
                    ViewAllDevTeams();
                    break;
                case "7":
                    ViewDevTeamById();
                    break;
                case "8":
                    AddDevTeam();
                    break;
                case "9":
                    UpdateExistingDevTeam();
                    break;
                case "10":
                    DeleteExistingDevTeam();
                    break;
                case "11":
                    DevelopersWithPluralsight();
                    break;
                case "12":
                    AddMultipleDevelopersToATeam();
                    break;
                case "00":
                    isRunning = ExitApplication();
                    break;
                default:
                    System.Console.WriteLine("Invalid Selection.");
                    break;



            }

        }
    }

    private bool ExitApplication()
    {
        Console.Clear();
        System.Console.WriteLine("Thanks for using Komodo DevTeams!");
        PressAnyKey();
        Console.Clear();
        return false;
    }

    private void PressAnyKey()
    {
        System.Console.WriteLine("Press any key to continue.");
        Console.ReadKey();

    }

    private void DisplayDevData(Developer dev)
    {
        Console.WriteLine(dev);
    }

    private void DevelopersWithPluralsight()
    {
        Console.Clear();
        List<Developer> devsWoPS = _dRepo.GetDevelopersWithoutPluralsight();

        if (devsWoPS.Count() > 0)
        {
            foreach (Developer dev in devsWoPS)
            {
                DisplayDevData(dev);
            }
        }
        else
        {
            System.Console.WriteLine("Every Developer has Pluralsight!");
        }
        PressAnyKey();
    }

    private void DeleteExistingDeveloper()
    {
        Console.Clear();
        ShowEnlistedDevs();
        Console.WriteLine("=========\n");
        try
        {
            System.Console.WriteLine("Select Developer by Id.");
            int userInputDevId = int.Parse(Console.ReadLine()!);
            var isValidated = ValidateDeveloperDatabase(userInputDevId);

            if (isValidated)
            {
                System.Console.WriteLine("Do you want to delete this Delevoper? y/n");
                string userInputDeleteDev = Console.ReadLine()!.ToLower();
                if (userInputDeleteDev == "y")
                {
                    if (_dRepo.DeleteDeveloper(userInputDevId))
                    {
                        System.Console.WriteLine($"The Developer was Delevted!");
                    }
                    else
                    {
                        System.Console.WriteLine($"The Developer NOT Delevted!");
                    }
                }
            }
            else
            {
                System.Console.WriteLine($"The Developer with the id: {userInputDevId} Doesn't Exist!");
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
            SomethingWentWrong();
        }

        PressAnyKey();
    }

    private void ViewAllDevelopers()
    {
        Console.Clear();
        ShowEnlistedDevs();
        PressAnyKey();


    }

    private void ShowEnlistedDevs()
    {
        Console.Clear();
        Console.WriteLine("=======Developer Listing========");
        List<Developer> devsInDb = _dRepo.GetAllDevelopers();
        ValidateDeveloperDatabaseData(devsInDb);
    }

    private void ValidateDeveloperDatabaseData(List<Developer> devsInDb)
    {
        if (devsInDb.Count > 0)
        {
            Console.Clear();
            foreach (Developer dev in devsInDb)
            {
                DisplayDevData(dev);
            }

        }
        else
        {
            Console.WriteLine("There are no developers in the database.");
        }
    }

    private void ViewDeveloperById()
    {
        Console.Clear();
        ShowEnlistedDevs();
        Console.WriteLine("--------\n");

        //try/catch -> try something. if it doesnt work display the error, swallow the error and continue running the application.
        try
        {
            Console.WriteLine("Select Developer By Id.");
            int userInputDevId = int.Parse(Console.ReadLine()!);
            ValidateDeveloperDatabase(userInputDevId);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            SomethingWentWrong();
        }

        PressAnyKey();
    }

    private bool ValidateDeveloperDatabase(int userInputDevId)
    {
        Developer dev = GetDeveloperDataFromDb(userInputDevId);
        if (dev != null)
        {
            Console.Clear();
            DisplayDevData(dev);
            return true;
        }
        else
        {
            Console.WriteLine($"The Developer with the id: {userInputDevId} doesn't Exist!");
            return false;
        }
    }

    private Developer GetDeveloperDataFromDb(int userInputDevId)
    {
        return _dRepo.GetDeveloperById(userInputDevId);
    }

    private void SomethingWentWrong()
    {
        Console.WriteLine("Something went wrong.\n" +
                          "Please try again\n" +
                          "Returning to Developer Menu\n");
    }


    private void AddDeveloper()
    {
        Console.Clear();

        try
        {
            Developer dev = IntitalizeDevCreationSetup();
            if (_dRepo.AddDeveloper(dev))
            {
                System.Console.WriteLine($"Successfully Added {dev.FullName} to the Database!");
            }
            else
            {
                SomethingWentWrong();
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            SomethingWentWrong();
        }
    }

    private Developer IntitalizeDevCreationSetup()
    {
        //create Developer data in the method
        Developer dev = new Developer();

        Console.WriteLine("== Add Developer Menu ==");

        Console.WriteLine("What is the Developers First Name?");
        dev.FirstName = Console.ReadLine()!;

        Console.WriteLine("What is the Developers LastName?");
        dev.LastName = Console.ReadLine()!;

        Console.WriteLine("Does this Developer have a Pluralsight Account?\n" +
                            "1. yes\n" +
                             "2. no\n");

        bool hasMadeSelection = false;

        string userInputPsAcct = Console.ReadLine()!;


        {
            switch (userInputPsAcct)
            {
                case "1":
                    dev.HasPluralsight = true;
                    break;
                default:
                    dev.HasPluralsight = false;
                    break;
            }

        }
        return dev;
    }

    private void UpdateExistingDeveloper()
    {
        Console.Clear();
        ShowEnlistedDevs();
        Console.WriteLine("---------\n");
        try
        {
            Console.WriteLine("Select Developer by Id.");

            int userInputDevId = int.Parse(Console.ReadLine()!);

            Developer devInDb = GetDeveloperDataFromDb(userInputDevId);

            bool isValidated = ValidateDeveloperDatabase(devInDb.ID);

            if (isValidated)
            {
                Console.WriteLine("Do you want to update this developer y/n");
                string userInputUpdateDev = Console.ReadLine()!.ToLower();

                if (userInputUpdateDev == "y")
                {
                    Developer updateDevData = IntitalizeDevCreationSetup();

                    if (_dRepo.UpdateDeveloper(devInDb.ID, updateDevData))
                    {
                        Console.WriteLine($"The Developer {updateDevData.FullName} has been updated");
                    }
                    else
                    {
                        Console.WriteLine($"The Developer {updateDevData.FullName} WAS NOT updated");
                    }
                }
            }
            else
            {
                System.Console.WriteLine("Developer Doesn't Exist!");
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            SomethingWentWrong();
        }
        PressAnyKey();
    }

    private void AddMultipleDevelopersToATeam()
    {
        try
        {
            Console.Clear();
            System.Console.WriteLine("== Developer Team Listing ==");
            GetDevTeamData();
            List<DeveloperTeam> devTeam = _dTRepo.GetDeveloperTeams();

            if (devTeam.Count() > 0)
            {
                System.Console.WriteLine("Select a Dev Team by Id");
                int userInputDevTeamId = int.Parse(Console.ReadLine()!);
                DeveloperTeam team = _dTRepo.GetDeveloperTeam(userInputDevTeamId);

                List<Developer> auxDevsInDb = _dRepo.GetDevelopersWithoutPluralsight();

                List<Developer> devsToAdd = new List<Developer>();

                if (team != null)
                {
                    bool hasFilledPositions = false;

                    while (!hasFilledPositions)
                    {
                        if (auxDevsInDb.Count() > 0)
                        {
                            DisplayDevelopersInDb(auxDevsInDb);
                            Console.WriteLine("Do you want to add a developer? y/n");
                            string userInputAnyDev = Console.ReadLine().ToLower()!;

                            if(userInputAnyDev == "y")
                            {
                                System.Console.WriteLine("Input developer Id");
                                int userInputDevId = int.Parse(Console.ReadLine()!);
                                Developer dev = _dRepo.GetDeveloperById(userInputDevId);
                                if(dev != null)
                                {
                                    devsToAdd.Add(dev);
                                    auxDevsInDb.Remove(dev);
                                }
                                else
                                {
                                    System.Console.WriteLine("The Developer Doesn't Exist.");
                                    PressAnyKey();
                                }
                            }
                            else
                            {
                                hasFilledPositions = true;
                            }
                        }
                        else
                        {
                            System.Console.WriteLine("There are no Developers in the Database.");
                            PressAnyKey();
                            break;
                        }
                    }
                    if(_dTRepo.AddMultipleDevelopers(team.ID, devsToAdd))
                    {
                        System.Console.WriteLine("Success!");
                    }
                    else
                    {
                        System.Console.WriteLine("Fail!");
                    }
                }
                else
                {
                    System.Console.WriteLine("Sorry invalid DevTeamId.");
                }
            }
            PressAnyKey();
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            SomethingWentWrong();
        }
    }
    private void ViewAllDevTeams()
    {
        Console.Clear();
        System.Console.WriteLine("== Developer Team Listing ==");
        GetDevTeamData();
        PressAnyKey();
    }

    private void GetDevTeamData()
    {
        List<DeveloperTeam> dTeams = _dTRepo.GetDeveloperTeams();
        if (dTeams.Count() > 0)
        {
            foreach (DeveloperTeam team in dTeams)
            {
                DisplayDeveloperTeamData(team);
            }

        }
        else
        {
            System.Console.WriteLine("There are no available Developer Teams!");
        }
    }

    private void DisplayDeveloperTeamData(DeveloperTeam team)
    {
        System.Console.WriteLine(team);
    }

    private void ViewDevTeamById()
    {
        Console.Clear();
        System.Console.WriteLine("== Developer Team Listing ==");
        GetDevTeamData();
        List<DeveloperTeam> devTeam = _dTRepo.GetDeveloperTeams();
        if (devTeam.Count() > 0)
        {
            System.Console.WriteLine("Select DevTeam By Id");
            int userInputDevTeamDataId = int.Parse(Console.ReadLine()!);
            ValidateDevTeamData(userInputDevTeamDataId);

        }
        PressAnyKey();
    }

    private void ValidateDevTeamData(int userInputDevTeamDataId)
    {
        DeveloperTeam team = _dTRepo.GetDeveloperTeam(userInputDevTeamDataId);
        if (team != null)
        {
            DisplayDeveloperTeamData(team);
        }
        else
        {
            System.Console.WriteLine("Sorry the Team Doesn't Exist!");
        }
    }

    private void UpdateExistingDevTeam()
    {
        try
        {
            Console.Clear();
            System.Console.WriteLine("== Developer Team Listing ==");
            GetDevTeamData();
            List<DeveloperTeam> dTeam = _dTRepo.GetDeveloperTeams();
            if (dTeam.Count() > 0)
            {
                System.Console.WriteLine("Please Select a DevTeamId for Update.");
                int userInputDevTeamId = int.Parse(Console.ReadLine()!);
                DeveloperTeam team = _dTRepo.GetDeveloperTeam(userInputDevTeamId);

                if (team != null)
                {
                    DeveloperTeam updatedTeamData = InitializeDTeamCreation();
                    if (_dTRepo.UpdateDevTeam(team.ID, updatedTeamData))
                    {
                        System.Console.WriteLine("Success!");
                    }
                    else
                    {
                        System.Console.WriteLine("Fail");
                    }
                }
                else
                {
                    System.Console.WriteLine("Sorry you used an invalid id.");
                }

            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            SomethingWentWrong();
        }
        PressAnyKey();
    }

    private void AddDevTeam()
    {
        Console.Clear();
        DeveloperTeam dTeam = InitializeDTeamCreation();

        if (_dTRepo.AddDevTeam(dTeam))
        {
            System.Console.WriteLine("Success!");
        }
        else
        {
            System.Console.WriteLine("Fail!");
        }
        PressAnyKey();
    }

    private DeveloperTeam InitializeDTeamCreation()
    {
        try
        {
            DeveloperTeam team = new DeveloperTeam();

            System.Console.WriteLine("Please enter the Team Name.");
            team.TeamName = Console.ReadLine()!;

            //ok, need a bool that will allow us to add members to our team
            bool hasFilledPositions = false;

            //Create a list for dynamic display
            List<Developer> auxDevelopers = _dRepo.GetAllDeveloper();

            while (hasFilledPositions == false)
            {
                System.Console.WriteLine("Does this team have any Developers y/n ?");
                string userInputAnyDevs = Console.ReadLine()!.ToLower();

                if (userInputAnyDevs == "y")
                {
                    if (auxDevelopers.Count() > 0)
                    {
                        DisplayDevelopersInDb(auxDevelopers);
                        System.Console.WriteLine("Select a Developer by Id");
                        int userInputDevId = int.Parse(Console.ReadLine()!);

                        Developer selectdDeveloper = _dRepo.GetDeveloperById(userInputDevId);

                        if (selectdDeveloper != null)
                        {
                            team.Developers.Add(selectdDeveloper);
                            auxDevelopers.Remove(selectdDeveloper);
                        }
                        else
                        {
                            System.Console.WriteLine("Sorry the Developer Doesn't Exist!");
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("There are no Developers in the Database.");
                        PressAnyKey();
                        break;
                    }
                }
                else
                {
                    hasFilledPositions = true;
                }
            }
            return team;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            SomethingWentWrong();
        }
        return null;
    }

    private void DisplayDevelopersInDb(List<Developer> auxDevelopers)
    {
        if (auxDevelopers.Count > 0)
        {
            foreach (Developer dev in auxDevelopers)
            {
                System.Console.WriteLine(dev);
            }
        }
    }

    private void DeleteExistingDevTeam()
    {
        try
        {
            Console.Clear();
            System.Console.WriteLine("== Developer Team Listing ==");
            GetDevTeamData();
            List<DeveloperTeam> dTeam = _dTRepo.GetDeveloperTeams();

            if (dTeam.Count() > 0)
            {
                System.Console.WriteLine("Please select a DeveloperTeam by Id for Selection");
                int userInputDevTeamId = int.Parse(Console.ReadLine()!);
                DeveloperTeam team = _dTRepo.GetDeveloperTeam(userInputDevTeamId);

                if (team != null)
                {
                    if (_dTRepo.DeleteDevTeam(team.ID))
                    {
                        System.Console.WriteLine("Success!");
                    }
                    else
                    {
                        System.Console.WriteLine("Fail.");
                    }
                }
                else
                {
                    System.Console.WriteLine("There aren't any DevTeams to Delete.");
                }
            }
            else
            {
                System.Console.WriteLine("There aren't any available Developer Teams.");
            }

            PressAnyKey();
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            SomethingWentWrong();
        }
    }


}
