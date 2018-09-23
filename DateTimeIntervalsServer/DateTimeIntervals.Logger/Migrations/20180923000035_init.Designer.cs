﻿// <auto-generated />
using DateTimeIntervals.Logger.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DateTimeIntervals.Logger.Migrations
{
    [DbContext(typeof(LoggerContext))]
    [Migration("20180923000035_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DateTimeIntervals.Logger.DomainModels.LogData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("DurationMs");

                    b.Property<string>("RemoteAddr");

                    b.Property<string>("RequestMethod");

                    b.Property<string>("RequestPath");

                    b.Property<string>("RequestProtocol");

                    b.Property<string>("RequestTimestamp");

                    b.Property<int>("ResponseStatus");

                    b.Property<string>("User");

                    b.Property<string>("UserAgent");

                    b.HasKey("Id");

                    b.ToTable("LogData");
                });
#pragma warning restore 612, 618
        }
    }
}
