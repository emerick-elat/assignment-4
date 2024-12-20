﻿// <auto-generated />
using System;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations.Bank
{
    [DbContext(typeof(BankContext))]
    [Migration("20241119125632_InitialCreateDB")]
    partial class InitialCreateDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BankRoleCustomer", b =>
                {
                    b.Property<int>("BankRolesId")
                        .HasColumnType("int");

                    b.Property<int>("CustomersId")
                        .HasColumnType("int");

                    b.HasKey("BankRolesId", "CustomersId");

                    b.HasIndex("CustomersId");

                    b.ToTable("BankRoleCustomer");
                });

            modelBuilder.Entity("Entities.Account", b =>
                {
                    b.Property<string>("AccountNumber")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.HasKey("AccountNumber");

                    b.HasIndex("CustomerId");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            AccountNumber = "1111111111111111",
                            CustomerId = 1
                        },
                        new
                        {
                            AccountNumber = "1111111111111110",
                            CustomerId = 1
                        });
                });

            modelBuilder.Entity("Entities.BankRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("NormalizedName")
                        .IsUnique();

                    b.ToTable("BankRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConcurrencyStamp = "",
                            Name = "Super Admin",
                            NormalizedName = "SUPER ADMIN"
                        },
                        new
                        {
                            Id = 2,
                            ConcurrencyStamp = "",
                            Name = "Customer",
                            NormalizedName = "CUSTOMER"
                        },
                        new
                        {
                            Id = 3,
                            ConcurrencyStamp = "",
                            Name = "Agent",
                            NormalizedName = "AGENT"
                        });
                });

            modelBuilder.Entity("Entities.Currency", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<decimal>("ValueToUSD")
                        .HasColumnType("decimal(10,5)");

                    b.HasKey("Code");

                    b.HasAlternateKey("Symbol");

                    b.ToTable("Currencies");

                    b.HasData(
                        new
                        {
                            Code = "USD",
                            Name = "US Dollar",
                            Symbol = "$",
                            ValueToUSD = 1m
                        },
                        new
                        {
                            Code = "EUR",
                            Name = "Euro",
                            Symbol = "€",
                            ValueToUSD = 1.08m
                        },
                        new
                        {
                            Code = "GBP",
                            Name = "Pound Sterling",
                            Symbol = "£",
                            ValueToUSD = 1.30m
                        },
                        new
                        {
                            Code = "YEN",
                            Name = "Yen",
                            Symbol = "¥",
                            ValueToUSD = 0.0067m
                        },
                        new
                        {
                            Code = "RUB",
                            Name = "Ruble",
                            Symbol = "₽",
                            ValueToUSD = 0.0103118m
                        });
                });

            modelBuilder.Entity("Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("EncryptedPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsEmailConfirmed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@smartbank.com",
                            EncryptedPassword = "AQAAAAIAAYagAAAAEJSOsPcN9rfn6MOai4xyilTlmaX2Pasz8Gv6VOP1VCIfljAblWVvZvfQD17HNPdk3A==",
                            FirstName = "System",
                            IsEmailConfirmed = true,
                            LastName = "Account",
                            NormalizedEmail = "ADMIN@SMARTBANK.COM",
                            NormalizedUserName = "ADMIN",
                            PhoneNumber = "+37061224853",
                            UserName = "admin@smartbank.com"
                        });
                });

            modelBuilder.Entity("Entities.CustomerBankRole", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("BankRoleId")
                        .HasColumnType("int");

                    b.HasKey("CustomerId", "BankRoleId");

                    b.HasIndex("BankRoleId");

                    b.ToTable("CustomerBankRoles");

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            BankRoleId = 1
                        });
                });

            modelBuilder.Entity("Entities.ScheduledPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(10,5)");

                    b.Property<DateTime>("EndOfValidityDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Periodicity")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartOfValidityDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ScheduledPayments");
                });

            modelBuilder.Entity("Entities.ScheduledPaymentItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ScheduledPaymentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ScheduledPaymentId");

                    b.ToTable("ScheduledPaymentItems");
                });

            modelBuilder.Entity("Entities.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(10,5)");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("EUR");

                    b.Property<string>("DestinationAccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("SourceAccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(16)");

                    b.Property<DateTime>("TransactionDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2024, 11, 19, 14, 56, 32, 667, DateTimeKind.Local).AddTicks(3193));

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("TransactionId");

                    b.HasIndex("DestinationAccountId");

                    b.HasIndex("SourceAccountId");

                    b.ToTable("Transactions");

                    b.HasData(
                        new
                        {
                            TransactionId = 1,
                            Amount = 10000m,
                            Currency = "EUR",
                            DestinationAccountId = "1111111111111111",
                            SourceAccountId = "1111111111111110",
                            TransactionDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = 0
                        });
                });

            modelBuilder.Entity("BankRoleCustomer", b =>
                {
                    b.HasOne("Entities.BankRole", null)
                        .WithMany()
                        .HasForeignKey("BankRolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Customer", null)
                        .WithMany()
                        .HasForeignKey("CustomersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Account", b =>
                {
                    b.HasOne("Entities.Customer", "Customer")
                        .WithMany("Accounts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Entities.CustomerBankRole", b =>
                {
                    b.HasOne("Entities.BankRole", "BankRole")
                        .WithMany("CustomerBankRoles")
                        .HasForeignKey("BankRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Customer", "Customer")
                        .WithMany("CustomerBankRoles")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BankRole");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Entities.ScheduledPaymentItem", b =>
                {
                    b.HasOne("Entities.ScheduledPayment", "ScheduledPayment")
                        .WithMany("ScheduledPaymentItems")
                        .HasForeignKey("ScheduledPaymentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ScheduledPayment");
                });

            modelBuilder.Entity("Entities.Transaction", b =>
                {
                    b.HasOne("Entities.Account", "DestinationAccount")
                        .WithMany("TransactionsTo")
                        .HasForeignKey("DestinationAccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Entities.Account", "SourceAccount")
                        .WithMany("TransactionsFrom")
                        .HasForeignKey("SourceAccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DestinationAccount");

                    b.Navigation("SourceAccount");
                });

            modelBuilder.Entity("Entities.Account", b =>
                {
                    b.Navigation("TransactionsFrom");

                    b.Navigation("TransactionsTo");
                });

            modelBuilder.Entity("Entities.BankRole", b =>
                {
                    b.Navigation("CustomerBankRoles");
                });

            modelBuilder.Entity("Entities.Customer", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("CustomerBankRoles");
                });

            modelBuilder.Entity("Entities.ScheduledPayment", b =>
                {
                    b.Navigation("ScheduledPaymentItems");
                });
#pragma warning restore 612, 618
        }
    }
}
