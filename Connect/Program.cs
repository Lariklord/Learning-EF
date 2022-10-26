using Connect;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

using(helloappdbContext db = new helloappdbContext())
{
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
    Console.WriteLine("Данные:");
    foreach (var item in db.Users)
    {
        Console.WriteLine($"{item.Id}.{item.Name}-{item.Age}");
    }
    Console.WriteLine();
}

using(helloappdbContext db = new helloappdbContext())
{
    await db.Users.AddRangeAsync(new User("Kostya",17),new User("Serega",19));
    await db.SaveChangesAsync();

    Console.WriteLine("Данные после добавления:");
    foreach (var item in db.Users)
    {
        Console.WriteLine($"{item.Id}.{item.Name}-{item.Age}");
    }
    Console.WriteLine();
}

using (helloappdbContext db = new helloappdbContext())
{
    var user = await db.Users.FirstOrDefaultAsync(x => x.Name == "Serega");
    if(user!=null)
    {
        user.Name = "Sereja";
        await db.SaveChangesAsync();
    }
    Console.WriteLine("Данные после редактирования:");
    foreach (var item in db.Users)
    {
        Console.WriteLine($"{item.Id}.{item.Name}-{item.Age}");
    }
    Console.WriteLine();
}

using (helloappdbContext db = new helloappdbContext())
{
    var user = await db.Users.FirstOrDefaultAsync();
    if (user != null)
    {
        db.Users.Remove(user);
        await db.SaveChangesAsync();
    }
    Console.WriteLine("Данные после удаления:");
    foreach (var item in db.Users)
    {
        Console.WriteLine($"{item.Id}.{item.Name}-{item.Age}");
    }
    Console.WriteLine();
}

