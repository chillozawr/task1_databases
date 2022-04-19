using Microsoft.EntityFrameworkCore;

namespace task1_databases;

public class CompanyContext : DbContext
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomClass> RoomClasses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=HotelEF;Username=postgres;Password=Ng1w6foxgr");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoomClass>().HasData(
            new RoomClass {RoomClassId = 1, ClassOfRoom = "Эконом", Price = 2000},
            new RoomClass {RoomClassId = 2, ClassOfRoom = "Бизнес", Price = 3000},
            new RoomClass {RoomClassId = 3, ClassOfRoom = "Люкс", Price = 5000}
            );
        modelBuilder.Entity<Room>().HasData(
            new Room {RoomId = 1, IsFree = true, RoomSize = 2, RoomClassId = 1},
            new Room {RoomId = 2, IsFree = false, RoomSize = 1, RoomClassId = 1},
            new Room {RoomId = 3, IsFree = false, RoomSize = 3, RoomClassId = 2},
            new Room {RoomId = 4, IsFree = false, RoomSize = 1, RoomClassId = 2},
            new Room {RoomId = 5, IsFree = false, RoomSize = 2, RoomClassId = 3},
            new Room {RoomId = 6, IsFree = true, RoomSize = 3, RoomClassId = 3}
            );
    }
    // public void CreateDbIfNotExist()
    // {
    //     this.Database.EnsureDeleted();
    //     this.Database.EnsureCreated();
    // }
}

public class Room
{
    public int RoomId { get; set; }
    public bool IsFree { get; set; }
    public int RoomSize { get; set; }
    
    public int RoomClassId { get; set; }
    public RoomClass? RoomClass { get; set; }
    
}

public class RoomClass
{
    public int RoomClassId { get; set; }
    public string ClassOfRoom { get; set; }
    public int Price { get; set; }
}