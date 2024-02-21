﻿// <auto-generated />
using System;
using DataAccsess.Concrete.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(ContextDb))]
    [Migration("20240219172847_updateDatabase")]
    partial class updateDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Core.Entities.Concrete.OperationClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OperationClaims");
                });

            modelBuilder.Entity("Core.Entities.Concrete.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("MailConfirm")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("MailConfirmDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("MailConfirmValue")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Core.Entities.Concrete.UserOperationClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("OperationClaimId")
                        .HasColumnType("integer");

                    b.Property<int>("UnitId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("UserOperationClaims");
                });

            modelBuilder.Entity("Core.Entities.Concrete.UserUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("UnitId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("UserUnits");
                });

            modelBuilder.Entity("Entities.Concrete.Line", b =>
                {
                    b.Property<int>("LineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("LineId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LineName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("LineType")
                        .HasColumnType("integer");

                    b.Property<int?>("TransferCenterId")
                        .HasColumnType("integer");

                    b.HasKey("LineId");

                    b.ToTable("Lines");
                });

            modelBuilder.Entity("Entities.Concrete.MailParameter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Port")
                        .HasColumnType("integer");

                    b.Property<string>("SMTP")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("SSL")
                        .HasColumnType("boolean");

                    b.Property<int>("UnitId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("MailParameters");
                });

            modelBuilder.Entity("Entities.Concrete.MailTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UnitId")
                        .HasColumnType("integer");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("MailTemplates");
                });

            modelBuilder.Entity("Entities.Concrete.Station", b =>
                {
                    b.Property<int>("StationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("StationId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("LineId")
                        .HasColumnType("integer");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("integer");

                    b.Property<string>("StationName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("UnitId")
                        .HasColumnType("integer");

                    b.HasKey("StationId");

                    b.HasIndex("LineId");

                    b.HasIndex("UnitId");

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("Entities.Concrete.Unit", b =>
                {
                    b.Property<int>("UnitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("UnitId"));

                    b.Property<string>("AddressDetail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gsm")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ManagerName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ManagerSurname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NeighbourHood")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UnitName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UnitId");

                    b.ToTable("Units");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Unit");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Entities.Concrete.Agenta", b =>
                {
                    b.HasBaseType("Entities.Concrete.Unit");

                    b.Property<int>("TransferCenterId")
                        .HasColumnType("integer");

                    b.HasIndex("TransferCenterId");

                    b.HasDiscriminator().HasValue("Agenta");

                    b.HasData(
                        new
                        {
                            UnitId = 1,
                            AddressDetail = "Adres Detay",
                            City = "Antalya",
                            ConcurrencyStamp = "4963c818-4d1f-458d-9449-046592a5ac63",
                            Description = "açıklama",
                            District = "kepez",
                            Email = "furkantsn@gmail.com",
                            Gsm = "123123",
                            IsDeleted = false,
                            ManagerName = "Furkan",
                            ManagerSurname = "Taşan",
                            NeighbourHood = "Güneş Mh.",
                            PhoneNumber = "123123123",
                            Street = "6033sk.",
                            UnitName = "Antalya",
                            TransferCenterId = 4
                        },
                        new
                        {
                            UnitId = 2,
                            AddressDetail = "Adres Detay",
                            City = "Malatya",
                            ConcurrencyStamp = "58480a04-1111-4881-82c2-d609f3f6b1d6",
                            Description = "açıklama",
                            District = "Malatya",
                            Email = "furkantsn@gmail.com",
                            Gsm = "123123",
                            IsDeleted = false,
                            ManagerName = "Furkan",
                            ManagerSurname = "Taşan",
                            NeighbourHood = "Güneş Mh.",
                            PhoneNumber = "123123123",
                            Street = "6033sk.",
                            UnitName = "Malatya",
                            TransferCenterId = 5
                        },
                        new
                        {
                            UnitId = 3,
                            AddressDetail = "Adres Detay",
                            City = "Elazığ",
                            ConcurrencyStamp = "e56f87ce-658f-485f-a931-e93f126ad295",
                            Description = "açıklama",
                            District = "Elazığ",
                            Email = "furkantsn@gmail.com",
                            Gsm = "123123",
                            IsDeleted = false,
                            ManagerName = "Arif",
                            ManagerSurname = "Arif",
                            NeighbourHood = "Güneş Mh.",
                            PhoneNumber = "123123123",
                            Street = "6033sk.",
                            UnitName = "Elazığ",
                            TransferCenterId = 6
                        });
                });

            modelBuilder.Entity("Entities.Concrete.TransferCenter", b =>
                {
                    b.HasBaseType("Entities.Concrete.Unit");

                    b.HasDiscriminator().HasValue("TransferCenter");

                    b.HasData(
                        new
                        {
                            UnitId = 4,
                            AddressDetail = "Adres Detay",
                            City = "Antalya",
                            ConcurrencyStamp = "e5853924-791c-4843-bd63-28a921f243ed",
                            Description = "açıklama",
                            District = "kepez",
                            Email = "furkantsn@gmail.com",
                            Gsm = "123123",
                            IsDeleted = false,
                            ManagerName = "Furkan",
                            ManagerSurname = "Taşan",
                            NeighbourHood = "Güneş Mh.",
                            PhoneNumber = "123123123",
                            Street = "6033sk.",
                            UnitName = "Antalya"
                        },
                        new
                        {
                            UnitId = 5,
                            AddressDetail = "Adres Detay",
                            City = "Malatya",
                            ConcurrencyStamp = "5029ff9e-b4f8-4d1a-a101-eb1869aa7406",
                            Description = "açıklama",
                            District = "Malatya",
                            Email = "furkantsn@gmail.com",
                            Gsm = "123123",
                            IsDeleted = false,
                            ManagerName = "Furkan",
                            ManagerSurname = "Taşan",
                            NeighbourHood = "Güneş Mh.",
                            PhoneNumber = "123123123",
                            Street = "6033sk.",
                            UnitName = "Malatya"
                        },
                        new
                        {
                            UnitId = 6,
                            AddressDetail = "Adres Detay",
                            City = "Elazığ",
                            ConcurrencyStamp = "23a20b6d-6c4b-4354-b70b-9051886deb2e",
                            Description = "açıklama",
                            District = "Elazığ",
                            Email = "furkantsn@gmail.com",
                            Gsm = "123123",
                            IsDeleted = false,
                            ManagerName = "Arif",
                            ManagerSurname = "Arif",
                            NeighbourHood = "Güneş Mh.",
                            PhoneNumber = "123123123",
                            Street = "6033sk.",
                            UnitName = "Elazığ"
                        });
                });

            modelBuilder.Entity("Entities.Concrete.Station", b =>
                {
                    b.HasOne("Entities.Concrete.Line", "Line")
                        .WithMany("Stations")
                        .HasForeignKey("LineId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Entities.Concrete.Unit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId");

                    b.Navigation("Line");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("Entities.Concrete.Agenta", b =>
                {
                    b.HasOne("Entities.Concrete.TransferCenter", "TransferCenter")
                        .WithMany("Agentas")
                        .HasForeignKey("TransferCenterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TransferCenter");
                });

            modelBuilder.Entity("Entities.Concrete.Line", b =>
                {
                    b.Navigation("Stations");
                });

            modelBuilder.Entity("Entities.Concrete.TransferCenter", b =>
                {
                    b.Navigation("Agentas");
                });
#pragma warning restore 612, 618
        }
    }
}
