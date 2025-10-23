namespace Models;
public class User
{    
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }   

    public override string ToString()
    {
        return $"Name: {Name}, Age: {Age}";
    }
}