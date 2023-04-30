﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Server.Data;

#nullable disable

namespace Server.Migrations
{
    [DbContext(typeof(IquraWordsDbContext))]
    partial class IquraWordsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Server.Models.Cluster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Collection_Id")
                        .HasColumnType("integer")
                        .HasColumnName("Collection_Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Collection_Id");

                    b.ToTable("Cluster");
                });

            modelBuilder.Entity("Server.Models.Collection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool?>("IsPrivate")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Collection");
                });

            modelBuilder.Entity("Server.Models.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Language");
                });

            modelBuilder.Entity("Server.Models.LikeTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Collection_Id")
                        .HasColumnType("integer")
                        .HasColumnName("Collection_Id");

                    b.Property<int>("User_Id")
                        .HasColumnType("integer")
                        .HasColumnName("User_Id");

                    b.HasKey("Id");

                    b.HasIndex("Collection_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("LikeTable");
                });

            modelBuilder.Entity("Server.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Avatar_url")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Registration_date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Server.Models.UserWord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date_Added")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Date_Leaned")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("User_id")
                        .HasColumnType("integer");

                    b.Property<int>("WordMeaning_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("User_id");

                    b.HasIndex("WordMeaning_id");

                    b.ToTable("UserWord");
                });

            modelBuilder.Entity("Server.Models.Word", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Language_Id")
                        .HasColumnType("integer")
                        .HasColumnName("Language_Id");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<string>("Term")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Language_Id");

                    b.ToTable("Word");
                });

            modelBuilder.Entity("Server.Models.WordMeaning", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Cluster_Id")
                        .HasColumnType("integer")
                        .HasColumnName("Cluster_Id");

                    b.Property<string>("Image_URL")
                        .HasColumnType("text");

                    b.Property<int>("Meaning_Id")
                        .HasColumnType("integer")
                        .HasColumnName("Meaning_Id");

                    b.Property<int>("Term_Id")
                        .HasColumnType("integer")
                        .HasColumnName("Term_Id");

                    b.Property<int>("Verified")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Cluster_Id");

                    b.HasIndex("Meaning_Id");

                    b.HasIndex("Term_Id");

                    b.ToTable("WordMeaning");
                });

            modelBuilder.Entity("Server.Models.Cluster", b =>
                {
                    b.HasOne("Server.Models.Collection", "Collection")
                        .WithMany("Clusters")
                        .HasForeignKey("Collection_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Collection");
                });

            modelBuilder.Entity("Server.Models.LikeTable", b =>
                {
                    b.HasOne("Server.Models.Collection", "Collection")
                        .WithMany("LikeTable")
                        .HasForeignKey("Collection_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Models.User", "User")
                        .WithMany("LikeTable")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Collection");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Server.Models.UserWord", b =>
                {
                    b.HasOne("Server.Models.User", "User")
                        .WithMany("UserWords")
                        .HasForeignKey("User_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Models.WordMeaning", "WordMeaning")
                        .WithMany("UserWords")
                        .HasForeignKey("WordMeaning_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("WordMeaning");
                });

            modelBuilder.Entity("Server.Models.Word", b =>
                {
                    b.HasOne("Server.Models.Language", "Language")
                        .WithMany("Words")
                        .HasForeignKey("Language_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");
                });

            modelBuilder.Entity("Server.Models.WordMeaning", b =>
                {
                    b.HasOne("Server.Models.Cluster", "Cluster")
                        .WithMany("WordMeanings")
                        .HasForeignKey("Cluster_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Models.Word", "Meaning")
                        .WithMany("Meanings")
                        .HasForeignKey("Meaning_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Models.Word", "Term")
                        .WithMany("Terms")
                        .HasForeignKey("Term_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cluster");

                    b.Navigation("Meaning");

                    b.Navigation("Term");
                });

            modelBuilder.Entity("Server.Models.Cluster", b =>
                {
                    b.Navigation("WordMeanings");
                });

            modelBuilder.Entity("Server.Models.Collection", b =>
                {
                    b.Navigation("Clusters");

                    b.Navigation("LikeTable");
                });

            modelBuilder.Entity("Server.Models.Language", b =>
                {
                    b.Navigation("Words");
                });

            modelBuilder.Entity("Server.Models.User", b =>
                {
                    b.Navigation("LikeTable");

                    b.Navigation("UserWords");
                });

            modelBuilder.Entity("Server.Models.Word", b =>
                {
                    b.Navigation("Meanings");

                    b.Navigation("Terms");
                });

            modelBuilder.Entity("Server.Models.WordMeaning", b =>
                {
                    b.Navigation("UserWords");
                });
#pragma warning restore 612, 618
        }
    }
}