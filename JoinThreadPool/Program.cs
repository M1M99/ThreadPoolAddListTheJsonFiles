using JoinThreadPool;
using System.Text.Json;
User user = new();
User user2 = new();

//// 5 json filedan melumati oxuyub eyni liste yigmaq lazimdir.
//// istifadechi birinci single ve ya multiple thread ishletmeyini sechir.
//// eger single sechibse 1 thread uzerinden file'lardan oxuyub umumi liste elave edir.
//// eger multiple sechilibse her bir file uchun 1 thread yaradib. onun uzerinden umumi liste elave edir
//// ThreadPool'dan istifade etmelisiz!


//for (var i = 0; i < 5; i++)
//{
//    Faker<User> faker = new();
//    var users = faker.RuleFor(u => u.Name, f => f.Person.FirstName)
//    .RuleFor(u => u.Surname, f => f.Person.LastName)
//    .RuleFor(u => u.Email, f => f.Internet.Email())                               // for json files
//    .RuleFor(u => u.DateOfBirth, f => f.Person.DateOfBirth)
//    .Generate(50);

//    var t = JsonSerializer.Serialize(users);
//    File.WriteAllText($"{i + 1}.json", t);
//}


void generateUsersForOneThread()
{
    var List = new List<string>() {
        File.ReadAllText("C:\\Users\\ASUS\\source\\repos\\JoinThreadPool\\JoinThreadPool\\bin\\Debug\\net8.0\\1.json"),
        File.ReadAllText("C:\\Users\\ASUS\\source\\repos\\JoinThreadPool\\JoinThreadPool\\bin\\Debug\\net8.0\\2.json"),
        File.ReadAllText("C:\\Users\\ASUS\\source\\repos\\JoinThreadPool\\JoinThreadPool\\bin\\Debug\\net8.0\\3.json"),
        File.ReadAllText("C:\\Users\\ASUS\\source\\repos\\JoinThreadPool\\JoinThreadPool\\bin\\Debug\\net8.0\\4.json"),
        File.ReadAllText("C:\\Users\\ASUS\\source\\repos\\JoinThreadPool\\JoinThreadPool\\bin\\Debug\\net8.0\\5.json"),

    };
    User user1 = new();
    var a = 0;
    foreach (var item in List)
    {
        var items = JsonSerializer.Deserialize<List<User>>(item);
        foreach (var item1 in items)
        {
            user.Users.Add(item1);
        }
    }

    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Bunlari Ekrana Cixarmaqdaki Meqsedim Odurki Yeni Liste Elave Etdi! Elave Edilmish Listdekileri Ekrana Cixardim");
    Console.ResetColor();
    foreach (var item in user.Users)
    {
        Console.WriteLine($"{a + 1}. {item.ToString()}");
        a++;
    }
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Bunlari Ekrana Cixarmaqdaki Meqsedim Odurki Yeni Liste Elave Etdi! Elave Edilmish Listdekileri Ekrana Cixardim");
    Console.ResetColor();
    Environment.Exit(0);
}
object lock1 = new();
void generateUsersForMultiThread()
{
    Console.WriteLine("Click Enter!");
    var List = new List<string>() {
        "C:\\Users\\ASUS\\source\\repos\\JoinThreadPool\\JoinThreadPool\\bin\\Debug\\net8.0\\1.json",
        "C:\\Users\\ASUS\\source\\repos\\JoinThreadPool\\JoinThreadPool\\bin\\Debug\\net8.0\\2.json",
        "C:\\Users\\ASUS\\source\\repos\\JoinThreadPool\\JoinThreadPool\\bin\\Debug\\net8.0\\3.json",
        "C:\\Users\\ASUS\\source\\repos\\JoinThreadPool\\JoinThreadPool\\bin\\Debug\\net8.0\\4.json",
        "C:\\Users\\ASUS\\source\\repos\\JoinThreadPool\\JoinThreadPool\\bin\\Debug\\net8.0\\5.json"
    };
    var a = 0;
    foreach (var item in List)
    {
        ThreadPool.QueueUserWorkItem((o) =>
        {
            var t = User.a = new();
            var t1 = File.ReadAllText(item);
            var items1 = JsonSerializer.Deserialize<List<User>>(t1); // multi threading
            foreach (var item in items1)
            {
                lock (lock1)
                {
                    user2.Users.Add(item);
                }
            }
        });
    }

    Console.ReadKey();
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Bunlari Ekrana Cixarmaqdaki Meqsedim Odurki Yeni Liste Elave Etd! Elave Edilmish Listdekileri Ekrana Cixardim");
    Console.ResetColor() ;
    foreach (var item in user2.Users)
    {
        Console.WriteLine($"{a + 1}. {item.ToString()}");
        a++;
    }
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Bunlari Ekrana Cixarmaqdaki Meqsedim Odurki Yeni Liste Elave Etdi! Elave Edilmish Listdekileri Ekrana Cixardim\n(Multi Thread)");
    Console.ResetColor();
    Console.ReadKey();
    Environment.Exit(0);
    Console.Clear();

}
while (true)
{
begin:
    Console.Write("1. Single Thread\n2. Multiple thread\nMake Choise : ");
    var choise = Console.ReadKey();
    switch (choise.Key)
    {
        case ConsoleKey.D1:
            Console.Clear();
            ThreadPool.QueueUserWorkItem((o) => { generateUsersForOneThread(); }); // bunu threadde yazmaga ehtiyac yoxdu onsuzda mainde thread var.
            Console.ReadKey();
            break;
            break;
        case ConsoleKey.D2:
            Console.Clear();
            generateUsersForMultiThread();
            break;
        default:
            Console.Clear();
            goto begin;
    }
}