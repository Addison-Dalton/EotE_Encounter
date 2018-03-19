﻿// <auto-generated />
using EotE_Encounter.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace EotE_Encounter.Migrations
{
    [DbContext(typeof(EncounterContext))]
    partial class EncounterContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EotE_Encounter.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte>("Advantages");

                    b.Property<int>("EncounterId");

                    b.Property<short>("IniativeScore");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("Notes")
                        .HasMaxLength(1000);

                    b.Property<bool>("Saved");

                    b.Property<byte>("Succeses");

                    b.Property<byte>("Triumphs");

                    b.HasKey("Id");

                    b.HasIndex("EncounterId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("EotE_Encounter.Models.Encounter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Notes")
                        .HasMaxLength(2000);

                    b.Property<byte>("Round");

                    b.HasKey("Id");

                    b.ToTable("Encounters");
                });

            modelBuilder.Entity("EotE_Encounter.Models.Character", b =>
                {
                    b.HasOne("EotE_Encounter.Models.Encounter", "Encounter")
                        .WithMany("Characters")
                        .HasForeignKey("EncounterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
