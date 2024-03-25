using University.Models.ModelsConfiguration;

namespace University.DbContexts
{
    public sealed class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Human> Humans { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Curriculum> Curriculums { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public ApplicationDbContext()
            : base()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                        .AddJsonFile("appSettings.json")
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
            modelBuilder.ApplyConfiguration(new HumanTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FacultyTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CurriculumTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GroupsTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StudentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewTypeConfiguration());
        }
    }
}
