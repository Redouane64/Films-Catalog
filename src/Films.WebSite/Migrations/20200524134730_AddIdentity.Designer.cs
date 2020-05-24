﻿// <auto-generated />
using Films.WebSite.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FilmsLibrary.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200524134730_AddIdentity")]
    partial class AddIdentity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FilmsLibrary.Domain.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatorId")
                        .HasColumnName("creator_id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasColumnName("director")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<short>("ReleaseYear")
                        .HasColumnName("year")
                        .HasColumnType("smallint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("nvarchar(MAX)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("films","catalog");
                });

            modelBuilder.Entity("FilmsLibrary.Domain.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("users","catalog");
                });

            modelBuilder.Entity("FilmsLibrary.Domain.Film", b =>
                {
                    b.HasOne("FilmsLibrary.Domain.User", "Creator")
                        .WithMany("Films")
                        .HasForeignKey("CreatorId");
                });
#pragma warning restore 612, 618
        }
    }
}
