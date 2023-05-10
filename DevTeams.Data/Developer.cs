
//P.O.C.O -> Plain old cSharp Object
//Domain Object

public class Developer
{
    public Developer()
    {

    }

    public Developer(string firstName, string lastName, bool hasPluralsight)
    {
        FirstName = firstName;
        LastName = lastName;
        HasPluralsight = hasPluralsight;
    }

    //We Need a Primary Key
    public int ID { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }

    public bool HasPluralsight { get; set; }

    public void Add(Developer developer)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        var str = $"ID: {ID}\n" +
                  $"Full Name: {FullName}" +
                  $"Has Pluralsight Access: {HasPluralsight}\n" +
                  "============================================";

        return str;
    }
}



