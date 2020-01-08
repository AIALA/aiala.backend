using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data.Activities;
using aiala.Backend.Data.Pictures;
using aiala.Backend.Data.Places;
using aiala.Backend.Data.Schedule;
using aiala.Backend.Data.Tasks;
using aiala.Backend.Data.Templates;
using xappido.Directory.Data;

namespace aiala.Backend.Data
{
    public class AppDbContext : DirectoryDbContext<Tenant, Account, User>
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<AppTask> Tasks { get; set; }

        public DbSet<Step> Steps { get; set; }

        public DbSet<Day> Days { get; set; }

        public DbSet<ScheduledTask> ScheduledTasks { get; set; }

        public DbSet<ScheduledStep> ScheduledSteps { get; set; }

        public DbSet<DayTemplate> DayTemplates { get; set; }

        public DbSet<ScheduledTaskTemplate> ScheduledTaskTemplates { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<EmergencyActivity> EmergencyActivities { get; set; }

        public DbSet<ScheduledStepActivity> ScheduledStepActivities { get; set; }

        public DbSet<ScheduledTaskActivity> ScheduledTaskActivities { get; set; }

        public DbSet<GeneralActivity> GeneralActivities { get; set; }

        public DbSet<PictureActivity> PictureActivities { get; set; }

        public DbSet<Place> Places { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<AiPictureTag> AiPictureTags { get; set; }

        public DbSet<AiPictureMetadata> AiPictureMetadatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("aiala");

            modelBuilder.Entity<Place>()
                .HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<AppTask>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<AiPictureMetadata>()
                .HasOne(apm => apm.Picture)
                .WithOne(p => p.AiMetadata)
                .HasForeignKey<AiPictureMetadata>(apm => apm.PictureId);

            modelBuilder.Entity<ScheduledTask>()
                .HasOne(t => t.Place)
                .WithOne(p => p.Task);

            base.OnModelCreating(modelBuilder);
        }
    }
}
