using Microsoft.EntityFrameworkCore;
using Npgsql;
using task1_databases;

static void point1()
{
    Console.WriteLine("Вывести список свободных комнат (номер, класс), отсортированных по стоимости аренды.");
    CompanyContext db = new CompanyContext();
    var freeRooms = db.Rooms
                                    .Include(p => p.RoomClass)
                                    .Where(p =>p.IsFree == true)
                                    .OrderBy(p=>p.RoomClass.Price);
    Console.WriteLine("Номер комнаты\tСтоимость");
    foreach (var room in freeRooms)
    {
        Console.WriteLine($"{room.RoomId}\t\t{room.RoomClass.Price}");
    }
    Console.WriteLine();
}

static void point2()
{
    Console.WriteLine("Рассчитать ежедневную прибыль отеля.");
    CompanyContext db = new CompanyContext();
    var dailyProfit = db.Rooms
        .Include(p => p.RoomClass)
        .Sum(p=>p.RoomClass.Price);
    
    var currentDailyProfit = db.Rooms
        .Include(p => p.RoomClass)
        .Where(p => !p.IsFree)
        .Sum(p=>p.RoomClass.Price);
    
    Console.WriteLine($"Ежедневная прибыль, если все комнаты свободны: {dailyProfit}" +
                      $"\nТекущая ежедневная прибыль: {currentDailyProfit}\n");
}

static void point3()
{
    Console.WriteLine("Рассчитать загруженность отеля (отношение числа сданных к общему числу комнат).");
    CompanyContext db = new CompanyContext();
    var capacity = (float)db.Rooms
        .Include(p => p.RoomClass)
        .Count(p=>p.IsFree) / (float)db.Rooms.Count();
    // db.Rooms.Count();
    Console.WriteLine($"Загруженность отеля: {capacity}\n");
}

static void point4()
{
    Console.WriteLine("Вывести пары: {класс, общее число комнат}");
    CompanyContext db = new CompanyContext();
    var pairs = db.Rooms
        .Include(p => p.RoomClass)
        .GroupBy(p=>p.RoomClass.ClassOfRoom)
        .Select(g => new
        {
            g.Key,
            Count = g.Count()
        });
    foreach (var pair in pairs)
    {
        Console.WriteLine($"{pair}");
    }
    Console.WriteLine();
}

static void point5()
{
    Console.WriteLine("Извлечь самую дорогую свободную комнату, пометить как занятую.");
    CompanyContext db = new CompanyContext();
    var maxPrice = db.Rooms
        .Include(p => p.RoomClass)
        .Max(p => p.RoomClass.Price);
    var freeMostExpensive = db.Rooms
        .Include(p => p.RoomClass)
        .FirstOrDefault(p => p.IsFree && p.RoomClass.Price == maxPrice);
    Console.WriteLine($"До изменения: {freeMostExpensive.RoomId} {freeMostExpensive.IsFree}");
    freeMostExpensive.IsFree = false;
    db.SaveChanges();
    Console.WriteLine($"После изменения: {freeMostExpensive.RoomId} {freeMostExpensive.IsFree}\n");
    
}

static void point6()
{
    Console.WriteLine("Найти самую дешёвую в расчёте на человека комнату и превратить её в Люксовую.");
    CompanyContext db = new CompanyContext();
    string connectionString = "Host=localhost;Database=HotelEF;Username=postgres;Password=Ng1w6foxgr";

    var pgExpression =
        "SELECT \"RoomId\", (\"Price\" / \"RoomSize\") as \"relation\" FROM public.\"Rooms\" LEFT JOIN public.\"RoomClasses\" " +
        "ON \"Rooms\".\"RoomClassId\" = \"RoomClasses\".\"RoomClassId\"" +
        "GROUP by (\"Price\" / \"RoomSize\"), \"RoomId\" order by \"relation\"";
    ///////////////////////////////////////////
    var freeRooms = db.Rooms
        .Select(p=>new {p.RoomId, p.IsFree, p.RoomSize, p.RoomClassId});
    // Console.WriteLine("Номер комнаты\tСтоимость");
    foreach (var room in freeRooms)
    {
        Console.WriteLine(room);
    }
    ///////////////////////////////////////////
    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
    {
        connection.Open();
        NpgsqlCommand command = new NpgsqlCommand(pgExpression, connection);
        NpgsqlDataReader reader = command.ExecuteReader();
        reader.Read();
        var row = reader.GetInt32(0);
        var pgUpdExpression = $"UPDATE public.\"Rooms\" SET \"RoomClassId\" = 3 WHERE \"RoomId\" = {row}";
        reader.Close();

        NpgsqlCommand command1 = new NpgsqlCommand(pgUpdExpression, connection);
        var count = command1.ExecuteNonQuery();
        Console.WriteLine($"Обновлено объектов: {count}");
        
    }
    ///////////////////////////////////////////
    var freeRooms2 = db.Rooms
        .Select(p=>new {p.RoomId, p.IsFree, p.RoomSize, p.RoomClassId});
    // Console.WriteLine("Номер комнаты\tСтоимость");
    foreach (var room in freeRooms2)
    {
        Console.WriteLine(room);
    }
    Console.WriteLine("\n");
    ///////////////////////////////////////////
    db.Database.Migrate();
}

static void point7()
{
    Console.WriteLine("Найти самый занятый постояльцами класс " +
                      "(отношение числа занятых комнат к общему числу комнат в классе) и" +
                      " добавить в отель новую комнату этого класса.");
    CompanyContext db = new CompanyContext();
    string connectionString = "Host=localhost;Database=HotelEF;Username=postgres;Password=Ng1w6foxgr";

    var pg =
        "select \"id11\", (Cast (count1 as real) / Cast (count2 as real)) as relation " +
        "from ( " +
        "select * from " +
        "(SELECT \"RoomClassId\" as id11, " +
        "COUNT(\"IsFree\") as count1 " +
        "FROM PUBLIC.\"Rooms\" " +
        "WHERE \"IsFree\" = FALSE " +
        "GROUP BY \"RoomClassId\") t1 " +
        "LEFT JOIN " +
        "(Select \"RoomClassId\", " +
        "count(\"RoomClassId\") as count2 " +
        "From public.\"Rooms\" " +
        "Group by \"RoomClassId\") t2 " +
        "on \"t1\".\"id11\" = \"t2\".\"RoomClassId\") as relation " +
        "order by relation desc ";
    ////////////////////////////////////////////
    var freeRooms = db.Rooms
        .Select(p=>new {p.RoomId, p.IsFree, p.RoomSize, p.RoomClassId});
    // Console.WriteLine("Номер комнаты\tСтоимость");
    foreach (var room in freeRooms)
    {
        Console.WriteLine(room);
    }
    ///////////////////////////////////////////
    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
    {
        connection.Open();
        NpgsqlCommand command = new NpgsqlCommand(pg, connection);
        NpgsqlDataReader reader = command.ExecuteReader();
        reader.Read();
        var row = (int)reader.GetDouble(0);
        var pgInsExpression = "insert into public.\"Rooms\" (\"IsFree\", \"RoomSize\", \"RoomClassId\")" +
                              $"values (true, 2, {row})";
        reader.Close();

        NpgsqlCommand command1 = new NpgsqlCommand(pgInsExpression, connection);
        var count = command1.ExecuteNonQuery();
        Console.WriteLine($"Обновлено объектов: {count}");
        
    }
    var freeRooms2 = db.Rooms
        .Select(p=>new {p.RoomId, p.IsFree, p.RoomSize, p.RoomClassId});
    // Console.WriteLine("Номер комнаты\tСтоимость");
    foreach (var room in freeRooms2)
    {
        Console.WriteLine(room);
    }
}

point1();
point2();
point3();
point4();
point5();
point6();
point7();