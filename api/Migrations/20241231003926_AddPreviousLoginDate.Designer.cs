﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using api.Data;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241231003926_AddPreviousLoginDate")]
    partial class AddPreviousLoginDate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("api.Models.AuditLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("email");

                    b.Property<string>("Event")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("event");

                    b.Property<string>("FailureReason")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("failure_reason");

                    b.Property<string>("IpAddress")
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)")
                        .HasColumnName("ip_address");

                    b.Property<bool>("Success")
                        .HasColumnType("boolean")
                        .HasColumnName("success");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.HasKey("Id");

                    b.ToTable("audit_logs", (string)null);
                });

            modelBuilder.Entity("api.Models.MarketMover", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("ChangePercent")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<long>("Volume")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("market_movers", (string)null);
                });

            modelBuilder.Entity("api.Models.StockData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Change")
                        .HasColumnType("numeric")
                        .HasColumnName("change");

                    b.Property<decimal>("ChangePercent")
                        .HasColumnType("numeric")
                        .HasColumnName("change_percent");

                    b.Property<decimal?>("High")
                        .HasColumnType("numeric")
                        .HasColumnName("high");

                    b.Property<decimal?>("Low")
                        .HasColumnType("numeric")
                        .HasColumnName("low");

                    b.Property<decimal>("MarketCap")
                        .HasColumnType("numeric")
                        .HasColumnName("market_cap");

                    b.Property<decimal?>("Open")
                        .HasColumnType("numeric")
                        .HasColumnName("open");

                    b.Property<decimal?>("PreviousClose")
                        .HasColumnType("numeric")
                        .HasColumnName("previous_close");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("symbol");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.Property<long>("Volume")
                        .HasColumnType("bigint")
                        .HasColumnName("volume");

                    b.HasKey("Id");

                    b.HasIndex("Symbol");

                    b.HasIndex("Timestamp");

                    b.ToTable("stock_data", (string)null);
                });

            modelBuilder.Entity("api.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("email");

                    b.Property<int>("FailedLogins")
                        .HasColumnType("integer")
                        .HasColumnName("failed_logins");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean")
                        .HasColumnName("is_admin");

                    b.Property<DateTime?>("LastLoginDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_login_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<int>("NumberOfLogins")
                        .HasColumnType("integer");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<DateTime?>("PreviousLoginDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("previous_login_date");

                    b.Property<string>("ResetToken")
                        .HasColumnType("text")
                        .HasColumnName("reset_token");

                    b.Property<DateTime?>("ResetTokenExpiry")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("reset_token_expiry");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("api.Models.UserFavoriteStock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("added_at");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("symbol");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "Symbol")
                        .IsUnique();

                    b.ToTable("user_favorite_stocks", (string)null);
                });

            modelBuilder.Entity("api.Models.UserOwnedStock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Notes")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("notes");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("purchase_date");

                    b.Property<decimal>("PurchasePrice")
                        .HasColumnType("numeric")
                        .HasColumnName("purchase_price");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("numeric")
                        .HasColumnName("quantity");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("symbol");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "Symbol");

                    b.ToTable("user_owned_stocks", (string)null);
                });

            modelBuilder.Entity("api.Models.UserSavedCrypto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Change")
                        .HasColumnType("numeric")
                        .HasColumnName("change");

                    b.Property<decimal>("ChangePercent")
                        .HasColumnType("numeric")
                        .HasColumnName("change_percent");

                    b.Property<decimal>("High24h")
                        .HasColumnType("numeric")
                        .HasColumnName("high_24h");

                    b.Property<decimal>("Low24h")
                        .HasColumnType("numeric")
                        .HasColumnName("low_24h");

                    b.Property<decimal>("Open")
                        .HasColumnType("numeric")
                        .HasColumnName("open");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<DateTime>("SavedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("saved_at");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("symbol");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<decimal>("Volume")
                        .HasColumnType("numeric")
                        .HasColumnName("volume");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "Symbol")
                        .IsUnique();

                    b.ToTable("user_saved_cryptos", (string)null);
                });

            modelBuilder.Entity("api.Models.UserSavedStock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Change")
                        .HasColumnType("numeric")
                        .HasColumnName("change");

                    b.Property<decimal>("ChangePercent")
                        .HasColumnType("numeric")
                        .HasColumnName("change_percent");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<decimal?>("High")
                        .HasColumnType("numeric")
                        .HasColumnName("high");

                    b.Property<decimal?>("Low")
                        .HasColumnType("numeric")
                        .HasColumnName("low");

                    b.Property<decimal>("MarketCap")
                        .HasColumnType("numeric")
                        .HasColumnName("market_cap");

                    b.Property<decimal?>("Open")
                        .HasColumnType("numeric")
                        .HasColumnName("open");

                    b.Property<decimal?>("PreviousClose")
                        .HasColumnType("numeric")
                        .HasColumnName("previous_close");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<DateTime>("SavedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("saved_at");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("symbol");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<long>("Volume")
                        .HasColumnType("bigint")
                        .HasColumnName("volume");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "Symbol")
                        .IsUnique();

                    b.ToTable("user_saved_stocks", (string)null);
                });

            modelBuilder.Entity("api.Models.UserFavoriteStock", b =>
                {
                    b.HasOne("api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("api.Models.UserOwnedStock", b =>
                {
                    b.HasOne("api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("api.Models.UserSavedCrypto", b =>
                {
                    b.HasOne("api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("api.Models.UserSavedStock", b =>
                {
                    b.HasOne("api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
