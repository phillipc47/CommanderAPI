﻿// <auto-generated />
using Commander.Data.SQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Commander.Migrations
{
    [DbContext(typeof(CommanderContext))]
    partial class CommanderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Commander.Models.Database.CommandCategoryModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "EF Core"
                        },
                        new
                        {
                            Id = 2,
                            Description = "User Secrets"
                        });
                });

            modelBuilder.Entity("Commander.Models.Database.CommandModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("HowTo")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("Line")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Commands");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            HowTo = "Create Migrations",
                            Line = "dotnet ef migrations add <Name>"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            HowTo = "Run Migrations",
                            Line = "dotnet ef database update"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 2,
                            HowTo = "Create User Secret",
                            Line = "dotnet user-secrets set <Key> <Value>"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 2,
                            HowTo = "List All User Secret",
                            Line = "dotnet user-secrets list"
                        });
                });

            modelBuilder.Entity("Commander.Models.Database.CommandModel", b =>
                {
                    b.HasOne("Commander.Models.Database.CommandCategoryModel", "Category")
                        .WithMany("Commands")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
