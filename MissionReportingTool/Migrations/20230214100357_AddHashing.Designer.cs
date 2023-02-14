﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MissionReportingTool.Repositories;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MissionReportingTool.Migrations
{
    [DbContext(typeof(SundownContext))]
    [Migration("20230214100357_AddHashing")]
    partial class AddHashing
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MissionReportingTool.Entities.MissionImageEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("CameraName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("MissionReportId")
                        .HasColumnType("bigint");

                    b.Property<string>("RoverName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RoverStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("MissionImage");
                });

            modelBuilder.Entity("MissionReportingTool.Entities.MissionReportEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("FinalisationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("MissionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("MissionReports");
                });

            modelBuilder.Entity("MissionReportingTool.Entities.UserEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CodeName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Avatar = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/ff/Yuri_Gagarin_in_1963.jpg/640px-Yuri_Gagarin_in_1963.jpg",
                            CodeName = "First Man",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeletedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "yuga@mtr.moon",
                            FirstName = "Yury",
                            LastName = "Gagarin",
                            Password = "1000.HzNyhtnzMa7fgGJsGmuEkg==.lP60NqH2IyjM6QcCPtsIOcPQ8PY1e6aaoCPkPVRJ6/c=",
                            Role = "ASTRONAUT",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Username = "yuga"
                        },
                        new
                        {
                            Id = 2L,
                            Avatar = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ae/Alan_Shepard_astronaut_in_spacesuit.jpg/640pxAlan_Shepard_astronaut_in_spacesuit.jpg",
                            CodeName = "Shepard",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeletedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "alsh@mtr.moon",
                            FirstName = "Alan",
                            LastName = "Shepard",
                            Password = "1000.oe9VQGho1VaIIoKEn7G/XA==.XpphCNVk4KmZcg9apKnoGHhIl1uBXjLUHLcQbljH+9M=",
                            Role = "ASTRONAUT",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Username = "alsh"
                        },
                        new
                        {
                            Id = 3L,
                            Avatar = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/61/RIAN_archive_612748_Valentina_Tereshkova.jpg/640pxRIAN_archive_612748_Valentina_Tereshkova.jpg",
                            CodeName = "Valentine",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeletedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "vate@mtr.moon",
                            FirstName = "Valentina",
                            LastName = "Tereshkova",
                            Password = "1000.BUMxWM6Z4MiVxlYWSJOGTQ==.ENn4jM5Dp9ZdbL6CgikXUUqnFTVg1OhUJ5o8XSPBwvY=",
                            Role = "ASTRONAUT",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Username = "vate"
                        },
                        new
                        {
                            Id = 4L,
                            Avatar = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/04/Guion_Bluford.jpg/640px-Guion_Bluford.jpg",
                            CodeName = "Bluey",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeletedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "gubl@mtr.moon",
                            FirstName = "Guion",
                            LastName = "Bluford",
                            Password = "1000.reJwC1RLKmFRXMFJPLtAKw==.QChVF7+ZSveGpkcHpWv8DfbKgREuK2lmigwdsa0uB6k=",
                            Role = "ASTRONAUT",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Username = "gubl"
                        },
                        new
                        {
                            Id = 5L,
                            Avatar = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/66/ISS-44_Andreas_Mogensen_in_the_Columbus_module.jpg/640pxISS-44_Andreas_Mogensen_in_the_Columbus_module.jpg",
                            CodeName = "Great Dane",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeletedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "anmo@mtr.moon",
                            FirstName = "Andreas",
                            LastName = "Mogensen",
                            Password = "1000.k+ygpGe7K4U53oDyvkdwzA==.Nn8kiN1iRWDBt2vBjEkdDWhQbOCVyYazxH8V3y5p5is=",
                            Role = "ASTRONAUT",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Username = "anmo"
                        },
                        new
                        {
                            Id = 6L,
                            Avatar = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b3/Yi_Soyeon_%28NASA_-_JSC2008-E-004174%29.jpg/640px-Yi_So-yeon_%28NASA_-_JSC2008-E-004174%29.jpg",
                            CodeName = "Neon",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeletedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "yiso@mtr.moon",
                            FirstName = "Yi",
                            LastName = "So-Yeon",
                            Password = "1000.taSKdgfZ9Bxt+ie1PZzd9w==.ZM/rLlYzi9O6o1GPZ99XuDAHLISs7wAe764TMqN3T98=",
                            Role = "ASTRONAUT",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Username = "yiso"
                        },
                        new
                        {
                            Id = 7L,
                            Avatar = "https://thumbs.dreamstime.com/b/admin-sign-laptop-icon-stock-vector-166205404.jpg",
                            CodeName = "SuperUser",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeletedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@system.com",
                            FirstName = "Admin",
                            LastName = "Adminsen",
                            Password = "1000.AE00gX35YIMYkP6R9ZxBRQ==.HX8sXson15lIqi9aOCWQixkE8ucOJEBcpn55fLjGkSA=",
                            Role = "ADMIN",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Username = "admin"
                        },
                        new
                        {
                            Id = 8L,
                            Avatar = "https://img.freepik.com/free-vector/flat-illustration-biotechnology-concept_23-2148880770.jpg?w=2000",
                            CodeName = "ScienceGuy",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeletedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "science@science.com",
                            FirstName = "John",
                            LastName = "Doe",
                            Password = "1000.Yt4aGG8O8E3RGCh66z1fhQ==.s3IPPAJ6HA4bsSLEYltXdm40OKLAk4Q/X8x+/EEgzpc=",
                            Role = "SCIENTIEST",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Username = "science"
                        },
                        new
                        {
                            Id = 9L,
                            Avatar = "https://www.shutterstock.com/image-illustration/cartoon-military-general-helmet-on-600w-62645065.jpg",
                            CodeName = "Space General",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeletedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "general@space.com",
                            FirstName = "S.",
                            LastName = "Down",
                            Password = "1000.j7dB/eQQkQ1WMhOhFBSlAg==.DUd/OnRi+RLbf9kq4Y2/RjuTnDSGckzEDXN/5876AOc=",
                            Role = "GENERAL",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Username = "general"
                        });
                });

            modelBuilder.Entity("MissionReportingTool.Entitites.LandingEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CalculatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Facility")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Landings");
                });
#pragma warning restore 612, 618
        }
    }
}
