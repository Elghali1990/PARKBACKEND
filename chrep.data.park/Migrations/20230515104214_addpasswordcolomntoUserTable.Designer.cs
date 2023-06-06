﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using chrep.data.park.SqlServer;

#nullable disable

namespace chrep.data.park.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230515104214_addpasswordcolomntoUserTable")]
    partial class addpasswordcolomntoUserTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("chrep.core.park.Models.Demande", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateBack")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateDemande")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateDepart")
                        .HasColumnType("datetime2");

                    b.Property<string>("Detail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan?>("HourBack")
                        .HasColumnType("time");

                    b.Property<TimeSpan?>("HourDepart")
                        .HasColumnType("time");

                    b.Property<string>("Objet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusEnum")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Demandes");
                });

            modelBuilder.Entity("chrep.core.park.Models.Mission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ChauffeurId")
                        .HasColumnType("int");

                    b.Property<string>("ChauffeurName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateDepart")
                        .HasColumnType("datetime2");

                    b.Property<int>("DemandeId")
                        .HasColumnType("int");

                    b.Property<TimeSpan?>("HourDepart")
                        .HasColumnType("time");

                    b.Property<string>("Instruction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MissionType")
                        .HasColumnType("int");

                    b.Property<string>("Observation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DemandeId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Missions");
                });

            modelBuilder.Entity("chrep.core.park.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("chrep.core.park.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tocken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserTypeEnum")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("chrep.core.park.Models.UserDemande", b =>
                {
                    b.Property<int>("DemandeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("DemandeOwner")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DemandeId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserDemande");
                });

            modelBuilder.Entity("chrep.core.park.Models.UserMission", b =>
                {
                    b.Property<int>("MissionId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAbsent")
                        .HasColumnType("bit");

                    b.HasKey("MissionId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserMission");
                });

            modelBuilder.Entity("chrep.core.park.Models.UserRole", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("chrep.core.park.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Marque")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Matricule")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeVehicule")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type_Matricule")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("nvarchar(max)")
                        .HasComputedColumnSql("[Matricule] + '-' + [TypeVehicule]");

                    b.HasKey("Id");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("chrep.core.park.Models.Mission", b =>
                {
                    b.HasOne("chrep.core.park.Models.Demande", "Demande")
                        .WithMany()
                        .HasForeignKey("DemandeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("chrep.core.park.Models.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId");

                    b.Navigation("Demande");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("chrep.core.park.Models.UserDemande", b =>
                {
                    b.HasOne("chrep.core.park.Models.Demande", null)
                        .WithMany()
                        .HasForeignKey("DemandeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("chrep.core.park.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("chrep.core.park.Models.UserMission", b =>
                {
                    b.HasOne("chrep.core.park.Models.Mission", null)
                        .WithMany()
                        .HasForeignKey("MissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("chrep.core.park.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("chrep.core.park.Models.UserRole", b =>
                {
                    b.HasOne("chrep.core.park.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("chrep.core.park.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
