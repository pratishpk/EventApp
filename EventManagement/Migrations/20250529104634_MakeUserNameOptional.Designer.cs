﻿// <auto-generated />
using System;
using EventManagement.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EventManagement.Migrations
{
    [DbContext(typeof(EventDbContext))]
    [Migration("20250529104634_MakeUserNameOptional")]
    partial class MakeUserNameOptional
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EventManagement.Model.EventDetails", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("EventId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventId"));

                    b.Property<string>("Description")
                        .HasColumnType("varchar(max)")
                        .HasColumnName("Description");

                    b.Property<string>("EventCategory")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("EventCategory");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("datetime")
                        .HasColumnName("EventDate");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("EventName");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Status");

                    b.HasKey("EventId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("EventManagement.Model.ParticipantEventDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EventId")
                        .HasColumnType("int")
                        .HasColumnName("EventId");

                    b.Property<string>("IsAttended")
                        .IsRequired()
                        .HasColumnType("varchar(3)")
                        .HasColumnName("IsAttended");

                    b.Property<string>("ParticipantEmailId")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("ParticipantEmailId");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("ParticipantEmailId");

                    b.ToTable("ParticipantEvents");
                });

            modelBuilder.Entity("EventManagement.Model.SessionInfo", b =>
                {
                    b.Property<int>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SessionId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SessionId"));

                    b.Property<string>("Description")
                        .HasColumnType("varchar(max)")
                        .HasColumnName("Description");

                    b.Property<int>("EventId")
                        .HasColumnType("int")
                        .HasColumnName("EventId");

                    b.Property<DateTime>("SessionEnd")
                        .HasColumnType("datetime")
                        .HasColumnName("SessionEnd");

                    b.Property<DateTime>("SessionStart")
                        .HasColumnType("datetime")
                        .HasColumnName("SessionStart");

                    b.Property<string>("SessionTitle")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("SessionTitle");

                    b.Property<string>("SessionUrl")
                        .HasColumnType("varchar(max)")
                        .HasColumnName("SessionUrl");

                    b.Property<int?>("SpeakerId")
                        .HasColumnType("int")
                        .HasColumnName("SpeakerId");

                    b.HasKey("SessionId");

                    b.HasIndex("EventId");

                    b.HasIndex("SpeakerId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("EventManagement.Model.SpeakersDetails", b =>
                {
                    b.Property<int>("SpeakerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SpeakerId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SpeakerId"));

                    b.Property<string>("SpeakerName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("SpeakerName");

                    b.HasKey("SpeakerId");

                    b.ToTable("Speakers");
                });

            modelBuilder.Entity("EventManagement.Model.UserInfo", b =>
                {
                    b.Property<string>("EmailId")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("EmailId");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Password");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Role");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("UserName");

                    b.HasKey("EmailId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EventManagement.Model.ParticipantEventDetails", b =>
                {
                    b.HasOne("EventManagement.Model.EventDetails", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventManagement.Model.UserInfo", "Participant")
                        .WithMany()
                        .HasForeignKey("ParticipantEmailId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Participant");
                });

            modelBuilder.Entity("EventManagement.Model.SessionInfo", b =>
                {
                    b.HasOne("EventManagement.Model.EventDetails", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventManagement.Model.SpeakersDetails", "Speaker")
                        .WithMany()
                        .HasForeignKey("SpeakerId");

                    b.Navigation("Event");

                    b.Navigation("Speaker");
                });
#pragma warning restore 612, 618
        }
    }
}
