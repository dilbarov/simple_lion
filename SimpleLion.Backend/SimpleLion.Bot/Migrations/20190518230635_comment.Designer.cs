﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleLion.Bot;

namespace SimpleLion.Bot.Migrations
{
    [DbContext(typeof(BotContext))]
    [Migration("20190518230635_comment")]
    partial class comment
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("SimpleLion.Bot.Models.CommandState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category");

                    b.Property<long>("ChatId");

                    b.Property<string>("Comment");

                    b.Property<string>("CurrentCommand");

                    b.Property<DateTime>("DateEnd");

                    b.Property<DateTime>("DateTime");

                    b.Property<bool>("IsFinished");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("NextCommand");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("CommandStates");
                });
#pragma warning restore 612, 618
        }
    }
}
