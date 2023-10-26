using Newtonsoft.Json;
namespace NamesJsonTask;
class Program
{
    private static string json;

    static void Main(string[] args)
    {
        string dirpath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Telebe");
        Directory.CreateDirectory(dirpath);
        json = Path.Combine(Directory.GetCurrentDirectory(), "..", "..","..",   "Telebe", "names.json");

        if (!File.Exists(json))
        {
            File.Create(json).Close();
        }        
        List<string> names = new List<string> { "Sebuhi", "Nicat", "Ayxan" };
        Serialize(names);
        Add("Sabuhi");
        Add("Taleh");
        Add("Ramiz");
        Delete("Ayxan");
        Delete("Ramiz");

        if (Search("Nicat"))
        {
            Console.WriteLine("Nicat adi avr.");
        }
        else
        {
            Console.WriteLine("Listde Nicat yoxdur.");
        }       
        Console.ReadLine();
    }
    public static void Add(string name)
    {
        List<string> names = Deserialize();
        if (names == null)
        {
            names = new();
        }
        names.Add(name);
        Serialize(names);
        Console.WriteLine($"{name} elave oludnu.");
    }
    public static void Delete(string name)
    {
        List<string> names = Deserialize();

        if (names.Contains(name))
        {
            names.Remove(name);
            Console.WriteLine($"{name} silindi.");
            Serialize(names);
        }
        else
        {
            Console.WriteLine($"{name} bele biri yoxdur.");
        }
    }
    public static bool Search(string search)
    {
        List<string> names = Deserialize();
        return names.Any(s => s.Contains(search));
    }
    public static void Serialize(List<string> names)
    {
        string result = JsonConvert.SerializeObject(names);

        using (StreamWriter sw = new(json))
        {
            sw.Write(result);
        }
    }
    public static List<string> Deserialize()
    {
        string result;
        using (StreamReader sr = new(json))
        {
            result = sr.ReadToEnd();
        }
        return JsonConvert.DeserializeObject<List<string>>(result);
    }    
    public static void ShowAllNames()
    {
        Console.WriteLine("Adlar:");
        List<string> names = Deserialize();
        names.ForEach(a => Console.WriteLine(a));
    }
}