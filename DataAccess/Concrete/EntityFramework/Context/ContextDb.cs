using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccsess.Concrete.EntityFramework.Context
{
    public class ContextDb : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // PostgreSQL bağlantı dizesini kullanarak yapılandırma
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=DbKargo;UserName=postgres;Password=1234");
        }

       
        public DbSet<Unit> Units { get; set; }
        public DbSet<Agenta> Agentas { get; set; }
        public DbSet<TransferCenter> TransferCenters { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<MailParameter> MailParameters { get; set; }
        public DbSet<MailTemplate> MailTemplates { get; set; }
        public DbSet<UserUnit> UserUnits { get; set; }

        public void Configure(EntityTypeBuilder<Line> builder)
        {
            // LineType alanını int olarak belirtme
            builder.Property(l => l.LineType)
                   .HasConversion<int>(); // Enum'i int'e dönüştür

            // Diğer konfigürasyonlar buraya eklenebilir
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            // Unit sınıfı için Fluent API
            modelBuilder.Entity<Unit>()
            .HasKey(u => u.UnitId); // Unit sınıfının primary key'i belirlendi

            modelBuilder.Entity<Unit>()
            .Property(u => u.UnitId)
            .UseIdentityAlwaysColumn(); // Otomatik artan olarak ayarlandı

            modelBuilder.Entity<MailParameter>()
           .Property(u => u.Id)
           .UseIdentityAlwaysColumn(); // Otomatik artan olarak ayarlandı

            modelBuilder.Entity<MailTemplate>()
           .Property(u => u.Id)
           .UseIdentityAlwaysColumn(); // Otomatik artan olarak ayarlandı

            modelBuilder.Entity<User>()
           .Property(u => u.Id)
           .UseIdentityAlwaysColumn(); // Otomatik artan olarak ayarlandı

            modelBuilder.Entity<OperationClaim>()
          .Property(u => u.Id)
          .UseIdentityAlwaysColumn(); // Otomatik artan olarak ayarlandı

            modelBuilder.Entity<UserOperationClaim>()
            .Property(u => u.Id)
            .UseIdentityAlwaysColumn(); // Otomatik artan olarak ayarlandı

            modelBuilder.Entity<Line>()
         .Property(u => u.LineId)
         .UseIdentityAlwaysColumn(); // Otomatik artan olarak ayarlandı

            modelBuilder.Entity<Station>()
       .Property(u => u.StationId)
       .UseIdentityAlwaysColumn(); // Otomatik artan olarak ayarlandı

            modelBuilder.Entity<UserUnit>()
 .Property(u => u.Id)
 .UseIdentityAlwaysColumn(); // Otomatik artan olarak ayarlandı


            modelBuilder.Entity<TransferCenter>()
            .HasMany(tc => tc.Agentas)
            .WithOne(a => a.TransferCenter)
            .OnDelete(DeleteBehavior.Cascade); // TransferCenter'dan Agenta'ya one-to-many ilişkisi belirlendi


            modelBuilder.Entity<Agenta>()
                .HasOne(a => a.TransferCenter)
                .WithMany(tc => tc.Agentas)
                .OnDelete(DeleteBehavior.Restrict); // Agenta'dan TransferCenter'a many-to-one ilişkisi belirlendi

            // Line sınıfı için Fluent API
            modelBuilder.Entity<Line>()
                .HasKey(l => l.LineId); // Line sınıfının primary key'i belirlendi
            modelBuilder.Entity<Line>()
                .HasMany(l => l.Stations)
                .WithOne(s => s.Line)
                .HasForeignKey(s => s.LineId)
                .OnDelete(DeleteBehavior.Cascade); // Line'dan Station'a one-to-many ilişkisi belirlendi

            // Station sınıfı için Fluent API
            modelBuilder.Entity<Station>()
                .HasKey(s => s.StationId); // Station sınıfının primary key'i belirlendi
            modelBuilder.Entity<Station>()
                .HasOne(s => s.Line)
                .WithMany(l => l.Stations)
                .HasForeignKey(s => s.LineId)
                .OnDelete(DeleteBehavior.Restrict); // Station'dan Line'a many-to-one ilişkisi belirlendi

            modelBuilder.Entity<Agenta>()
               .HasData(
                new Agenta()
                {
                    UnitId = 1,
                    UnitName = "Antalya",
                    ManagerName = "Furkan",
                    ManagerSurname = "Taşan",
                    PhoneNumber = "123123123",
                    Gsm = "123123",
                    Email = "furkantsn@gmail.com",
                    Description = "açıklama",
                    City = "Antalya",
                    District = "kepez",
                    NeighbourHood = "Güneş Mh.",
                    Street = "6033sk.",
                    AddressDetail = "Adres Detay",
                    IsDeleted = false,
                    TransferCenterId=4
                },
                 new Agenta()
                 {
                     UnitId = 2,
                     UnitName = "Malatya",
                     ManagerName = "Furkan",
                     ManagerSurname = "Taşan",
                     PhoneNumber = "123123123",
                     Gsm = "123123",
                     Email = "furkantsn@gmail.com",
                     Description = "açıklama",
                     City = "Malatya",
                     District = "Malatya",
                     NeighbourHood = "Güneş Mh.",
                     Street = "6033sk.",
                     AddressDetail = "Adres Detay",
                     IsDeleted = false,
                      TransferCenterId = 5
                 }, new Agenta()
                 {
                     UnitId = 3,
                     UnitName = "Elazığ",
                     ManagerName = "Arif",
                     ManagerSurname = "Arif",
                     PhoneNumber = "123123123",
                     Gsm = "123123",
                     Email = "furkantsn@gmail.com",
                     Description = "açıklama",
                     City = "Elazığ",
                     District = "Elazığ",
                     NeighbourHood = "Güneş Mh.",
                     Street = "6033sk.",
                     AddressDetail = "Adres Detay",
                     IsDeleted = false,
                     TransferCenterId = 6
                 });

            modelBuilder.Entity<TransferCenter>()
               .HasData(
                new TransferCenter()
                {
                    UnitId = 4,
                    UnitName = "Antalya",
                    ManagerName = "Furkan",
                    ManagerSurname = "Taşan",
                    PhoneNumber = "123123123",
                    Gsm = "123123",
                    Email = "furkantsn@gmail.com",
                    Description = "açıklama",
                    City = "Antalya",
                    District = "kepez",
                    NeighbourHood = "Güneş Mh.",
                    Street = "6033sk.",
                    AddressDetail = "Adres Detay",
                    IsDeleted = false
                },
                 new TransferCenter()
                 {
                     UnitId = 5,
                     UnitName = "Malatya",
                     ManagerName = "Furkan",
                     ManagerSurname = "Taşan",
                     PhoneNumber = "123123123",
                     Gsm = "123123",
                     Email = "furkantsn@gmail.com",
                     Description = "açıklama",
                     City = "Malatya",
                     District = "Malatya",
                     NeighbourHood = "Güneş Mh.",
                     Street = "6033sk.",
                     AddressDetail = "Adres Detay",
                     IsDeleted = false
                 }, new TransferCenter()
                 {
                     UnitId = 6,
                     UnitName = "Elazığ",
                     ManagerName = "Arif",
                     ManagerSurname = "Arif",
                     PhoneNumber = "123123123",
                     Gsm = "123123",
                     Email = "furkantsn@gmail.com",
                     Description = "açıklama",
                     City = "Elazığ",
                     District = "Elazığ",
                     NeighbourHood = "Güneş Mh.",
                     Street = "6033sk.",
                     AddressDetail = "Adres Detay",
                     IsDeleted = false
                 }

               );
        }

    }
}

