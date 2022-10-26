using Start;

using(ApplicationContext db = new ApplicationContext())
{
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();

    
    db.Users.Add(new User { Name = "Vlad", Age = 18 });
    db.Users.Add(new User { Name = "Oleg", Age = 37 });

    db.SaveChanges();
    

    Console.WriteLine("Объекты успешно сохранены");

    var users = db.Users.ToList();

    Console.WriteLine("Список объектов:");
    foreach (var user in users)
    {
        Console.WriteLine($"{user.Id}.{user.Name}-{user.Age}");
    }

}
