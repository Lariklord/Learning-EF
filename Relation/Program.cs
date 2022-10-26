using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Relation;
/*
using (MyAppContext context = new MyAppContext())
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    Company microsoft = new Company { Name = "Microsoft" };
    Company google = new Company { Name = "Google" };

    context.Companies.AddRange(google, microsoft);

    context.SaveChanges();

    Worker vlad = new Worker { Name = "Vlad",Age=18, Company = microsoft };
    Worker vanya = new Worker { Name="Vanya",Age=17, Company=microsoft };
    Worker sereja = new Worker { Name = "Sereja",Age=19, Company = google };
    Worker kostya = new Worker { Name = "Kostya",Age=18, Company = google };

    context.Workers.AddRange(vlad, vanya, sereja, kostya);

    context.SaveChanges();

    /*
    var users = context.Workers;
    foreach (var item in users)
    {
        Console.WriteLine(item.Name);
    }
    Console.WriteLine();

    var comp = context.Companies.FirstOrDefault();
    context.Companies.Remove(comp);
    context.SaveChanges();

    users = context.Workers;
    foreach (var item in users)
    {
        Console.WriteLine(item.Name);
    }
    
}*/

using (MyAppContext context = new MyAppContext())
{
    var users = context.Workers.Include(x=>x.Company).ToList();
    foreach (var item in users)
    {
        Console.WriteLine($"{item.Name}-{item.Company?.Name}");
    }
    
    Console.WriteLine();
    var companies = context.Companies.Include(x=>x.Workers).ToList();
    foreach (var comp in companies)
    {
        Console.WriteLine(comp.Name);
        foreach (var item in comp.Workers)
        {
            Console.WriteLine(item.Name);
        }
        Console.WriteLine();
    }
}

using (MyAppContext context = new MyAppContext())
{
    var companies = context.Companies.ToList();
    foreach (var item in companies)
    {
        context.Workers.Where(x => x.CompanyId == item.Id).Load();
        Console.WriteLine(item.Name);
        foreach (var user in item.Workers)
        {
            Console.WriteLine(user.Name);
        }
        Console.WriteLine();
    }
}

using (MyAppContext context = new MyAppContext())
{
    var companies = context.Companies.ToList();
    foreach (var item in companies)
    {
        Console.WriteLine(item.Name);
        foreach (var user in item.Workers)
        {
            Console.WriteLine(user.Name);
        }
        Console.WriteLine();
    }
}

using (MyAppContext context = new MyAppContext())
{
    var users = await context.Workers.Include(x=>x.Company).Where(x => x.CompanyId == 1).ToListAsync();
    foreach (var item in users)
    {
        Console.WriteLine($"{item.Name}({item.Age})-{item.Company?.Name}-{item.CompanyId}");
    }
    Console.WriteLine();

    var users1 = await context.Workers.Include(x=>x.Company).Where(y=>y.Company!.Name=="Google").ToListAsync();
    Console.WriteLine("Google\n========================");
    foreach (var item in users1)
    {
        Console.WriteLine($"{item.Name}-{item.Age}");
    }
    Console.WriteLine();

    var users2 = await context.Workers.Include(x => x.Company).Where(p => EF.Functions.Like(p.Name!, "_lad")).ToListAsync();
    foreach (var item in users2)
    {
        Console.WriteLine($"{item.Name}-{item.Age}");
    }
    Console.WriteLine();

    var user = context.Workers.Find(3);
    Console.WriteLine(user?.Id+"-"+user?.Name+"-"+user?.Age);
    Console.WriteLine();

    var users3 = context.Workers.Select(p => new
    {
        Name=p.Name,
        Age=p.Age,
        Company=p.Company!.Name
    });
    foreach (var item in users3)
    {
        Console.WriteLine($"{item.Name}-{item.Age}-{item.Company}");
    }
    Console.WriteLine();

    var users4 = context.Workers.Include(p => p.Company).OrderBy(x => x.Company!.Name).ThenByDescending(y => y.Name);
    foreach (var item in users4)
    {
        Console.WriteLine($"{item.Name}-{item.Age}-{item.Company!.Name}");
    }
    Console.WriteLine();

    var users5 = context.Workers.Include(p => p.Company).Join(context.Companies, u => u.CompanyId, c => c.Id, (u, c) => new { Name = u.Name, Age = u.Age, Company = c.Name });
    foreach (var item in users5)
    {
        Console.WriteLine($"{item.Name}-{item.Age}-{item.Company}");
    }
    Console.WriteLine();

    var group = context.Workers.GroupBy(x => x.Company!.Name).Select(p => new { p.Key, Count = p.Count() });
    foreach (var item in group)
    {
        Console.WriteLine($"{item.Key}-{item.Count}");
    }
    Console.WriteLine();

    var users6 = context.Workers.Where(x => x.Age == 18).Union(context.Workers.Where(x => x.Age == 19));
    foreach (var item in users6)
    {
        Console.WriteLine($"{item.Name}-{item.Age}");
    }
    Console.WriteLine();

    var users7 = context.Workers.Where(x => x.Age == 18).Intersect(context.Workers.Where(x => x.Name!.Contains("Vlad")));
    foreach (var item in users7)
    {
        Console.WriteLine($"{item.Name}-{item.Age}");
    }
    Console.WriteLine();

    var users8 = context.Workers.Where(x => x.Age <20).Except(context.Workers.Where(x => x.Name!.Contains("Vanya")));
    foreach (var item in users8)
    {
        Console.WriteLine($"{item.Name}-{item.Age}");
    }
    Console.WriteLine();

    Console.WriteLine("Google - "+context.Workers.Any(x=>x.Company!.Name=="Google"));
    Console.WriteLine("Google - "+context.Workers.All(x=>x.Company!.Name=="Google"));
    Console.WriteLine("Google - "+context.Workers.Count(x => x.Company!.Name == "Google")+" рабочих");
    Console.WriteLine("Макс - " + context.Workers.Max(x => x.Age));
    Console.WriteLine("Мин - " + context.Workers.Min(x => x.Age));
    Console.WriteLine("Ср - " + context.Workers.Average(x => x.Age));
    Console.WriteLine("Сум - " + context.Workers.Sum(x => x.Age));
    Console.WriteLine();
    Console.WriteLine(context.ChangeTracker.Entries().Count());
    Console.WriteLine();

    var comp = context.Companies.FromSqlRaw("select * from Companies").OrderBy(x=>x.Name);
    foreach (var item in comp)
    {
        Console.WriteLine(item.Name);
    }
    Console.WriteLine();

    var users9 = context.Workers.FromSqlInterpolated($"select * from GetUsersByAge({19})");
    foreach (var item in users9)
    {
        Console.WriteLine($"{item.Name}-{item.Age}");
    }
    Console.WriteLine();

    var users10 = context.GetUsersByAge(19);
    foreach (var item in users10)
    {
        Console.WriteLine($"{item.Name}-{item.Age}");
    }

}