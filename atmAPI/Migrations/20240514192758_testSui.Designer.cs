﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using atmAPI.Data;

#nullable disable

namespace atmAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240514192758_testSui")]
    partial class testSui
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("atmAPI.Models.Account", b =>
                {
                    b.Property<int>("account_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("account_id"));

                    b.Property<string>("account_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("balance")
                        .HasColumnType("integer");

                    b.Property<int>("client_id")
                        .HasColumnType("integer");

                    b.Property<char>("currency")
                        .HasColumnType("character(1)");

                    b.HasKey("account_id");

                    b.HasIndex("client_id");

                    b.ToTable("accounts");
                });

            modelBuilder.Entity("atmAPI.Models.Client", b =>
                {
                    b.Property<int>("client_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("client_id"));

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("age")
                        .HasColumnType("integer");

                    b.Property<string>("client_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("client_phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("client_surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("pin")
                        .HasColumnType("integer");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("client_id");

                    b.ToTable("clients");
                });

            modelBuilder.Entity("atmAPI.Models.Transaction", b =>
                {
                    b.Property<int>("transaction_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("transaction_id"));

                    b.Property<int>("account_id")
                        .HasColumnType("integer");

                    b.Property<int>("amount")
                        .HasColumnType("integer");

                    b.Property<string>("transaction_type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("transaction_id");

                    b.HasIndex("account_id");

                    b.ToTable("transactions");
                });

            modelBuilder.Entity("atmAPI.Models.Account", b =>
                {
                    b.HasOne("atmAPI.Models.Client", "client")
                        .WithMany("accounts")
                        .HasForeignKey("client_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("client");
                });

            modelBuilder.Entity("atmAPI.Models.Transaction", b =>
                {
                    b.HasOne("atmAPI.Models.Account", "account")
                        .WithMany("transactions")
                        .HasForeignKey("account_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("account");
                });

            modelBuilder.Entity("atmAPI.Models.Account", b =>
                {
                    b.Navigation("transactions");
                });

            modelBuilder.Entity("atmAPI.Models.Client", b =>
                {
                    b.Navigation("accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
