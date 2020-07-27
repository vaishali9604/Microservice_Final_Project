﻿// <auto-generated />
using System;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MT.OnlineRestaurant.DataLayer.Migrations
{
    [DbContext(typeof(RestaurantManagementContext))]
    partial class RestaurantManagementContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel.LoggingInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActionName")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(250);

                    b.Property<string>("ControllerName")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(250);

                    b.Property<string>("Description")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')");

                    b.Property<DateTime?>("RecordTimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("('')");

                    b.HasKey("Id");

                    b.ToTable("LoggingInfo");
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel.TblRatingandReviews", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Rating")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Rating")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime>("RecordTimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime>("RecordTimeStampCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("Reviews")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')");

                    b.Property<int?>("TblCustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tblCustomerId")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("TblRestaurantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tblRestaurantID")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("UserCreated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("UserModified")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("((0))");

                    b.HasKey("Id");

                    b.HasIndex("TblRestaurantId");

                    b.ToTable("tblRatingandReviews");
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel.TblRestaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsUnicode(false);

                    b.Property<string>("CloseTime")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(5);

                    b.Property<string>("ContactNo")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(20);

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(225);

                    b.Property<string>("OpeningTime")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(5);

                    b.Property<DateTime>("RecordTimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime>("RecordTimeStampCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("UserCreated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("UserModified")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("Website")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(225);

                    b.HasKey("Id");

                    b.ToTable("tblRestaurant");
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel.TblRatingandReviews", b =>
                {
                    b.HasOne("MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel.TblRestaurant", "TblRestaurant")
                        .WithMany("TblRatingandReviews")
                        .HasForeignKey("TblRestaurantId")
                        .HasConstraintName("FK_tblRating_tblRestaurantID");
                });
#pragma warning restore 612, 618
        }
    }
}
