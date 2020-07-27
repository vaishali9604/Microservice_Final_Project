using Microsoft.EntityFrameworkCore;
using MT.OnlineRestaurant.Logging.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel
{
   public partial class RestaurantManagementContext : DbContext
    {
        public RestaurantManagementContext()
        {
        }

        public RestaurantManagementContext(DbContextOptions<RestaurantManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblRatingandReviews> TblRatingandReviews { get; set; }
        public virtual DbSet<TblRestaurant> TblRestaurant { get; set; }
        public virtual DbSet<LoggingInfo> LoggingInfo { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                // optionsBuilder.UseSqlServer(@"Server=tcp:capstoneteam1server.database.windows.net,1433;Initial Catalog=RestaurantManagement;Persist Security Info=False;user id=cpadmin;password=Mindtree@12;");
                //optionsBuilder.UseSqlServer(DbConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<LoggingInfo>(entity =>
            {
                entity.Property(e => e.ActionName)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ControllerName)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Description).HasDefaultValueSql("('')");

                entity.Property(e => e.RecordTimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('')");
            });
            modelBuilder.Entity<TblRatingandReviews>(entity =>
            {
                entity.ToTable("tblRatingandReviews");

                entity.Property(e => e.Id).HasColumnName("ID");


                entity.Property(e => e.Reviews)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Rating)
                    .HasColumnName("Rating")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RecordTimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RecordTimeStampCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TblCustomerId)
                    .HasColumnName("tblCustomerId")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TblRestaurantId)
                    .HasColumnName("tblRestaurantID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserCreated).HasDefaultValueSql("((0))");

                entity.Property(e => e.UserModified).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.TblRestaurant)
                    .WithMany(p => p.TblRatingandReviews)
                    .HasForeignKey(d => d.TblRestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblRating_tblRestaurantID");
            });

            modelBuilder.Entity<TblRestaurant>(entity =>
            {
                entity.ToTable("tblRestaurant");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.CloseTime)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ContactNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(225)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.OpeningTime)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecordTimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RecordTimeStampCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");



                entity.Property(e => e.UserCreated).HasDefaultValueSql("((0))");

                entity.Property(e => e.UserModified).HasDefaultValueSql("((0))");

                entity.Property(e => e.Website)
                    .IsRequired()
                    .HasMaxLength(225)
                    .HasDefaultValueSql("('')");


            });


        }
    }
}

