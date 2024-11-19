using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations.Bank
{
    /// <inheritdoc />
    public partial class InitialCreateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Symbol = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    ValueToUSD = table.Column<decimal>(type: "decimal(10,5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Code);
                    table.UniqueConstraint("AK_Currencies_Symbol", x => x.Symbol);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEmailConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    EncryptedPassword = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    StartOfValidityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndOfValidityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Periodicity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledPayments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountNumber);
                    table.ForeignKey(
                        name: "FK_Accounts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankRoleCustomer",
                columns: table => new
                {
                    BankRolesId = table.Column<int>(type: "int", nullable: false),
                    CustomersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankRoleCustomer", x => new { x.BankRolesId, x.CustomersId });
                    table.ForeignKey(
                        name: "FK_BankRoleCustomer_BankRoles_BankRolesId",
                        column: x => x.BankRolesId,
                        principalTable: "BankRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankRoleCustomer_Customers_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerBankRoles",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    BankRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerBankRoles", x => new { x.CustomerId, x.BankRoleId });
                    table.ForeignKey(
                        name: "FK_CustomerBankRoles_BankRoles_BankRoleId",
                        column: x => x.BankRoleId,
                        principalTable: "BankRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerBankRoles_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledPaymentItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduledPaymentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledPaymentItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledPaymentItems_ScheduledPayments_ScheduledPaymentId",
                        column: x => x.ScheduledPaymentId,
                        principalTable: "ScheduledPayments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 11, 19, 14, 56, 32, 667, DateTimeKind.Local).AddTicks(3193)),
                    Amount = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SourceAccountId = table.Column<string>(type: "nvarchar(16)", nullable: false),
                    DestinationAccountId = table.Column<string>(type: "nvarchar(16)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "EUR")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_DestinationAccountId",
                        column: x => x.DestinationAccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_SourceAccountId",
                        column: x => x.SourceAccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BankRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "", "Super Admin", "SUPER ADMIN" },
                    { 2, "", "Customer", "CUSTOMER" },
                    { 3, "", "Agent", "AGENT" }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Code", "Description", "Name", "Symbol", "ValueToUSD" },
                values: new object[,]
                {
                    { "EUR", null, "Euro", "€", 1.08m },
                    { "GBP", null, "Pound Sterling", "£", 1.30m },
                    { "RUB", null, "Ruble", "₽", 0.0103118m },
                    { "USD", null, "US Dollar", "$", 1m },
                    { "YEN", null, "Yen", "¥", 0.0067m }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "EncryptedPassword", "FirstName", "IsEmailConfirmed", "LastName", "NormalizedEmail", "NormalizedUserName", "PhoneNumber", "UserName" },
                values: new object[] { 1, "admin@smartbank.com", "AQAAAAIAAYagAAAAEJSOsPcN9rfn6MOai4xyilTlmaX2Pasz8Gv6VOP1VCIfljAblWVvZvfQD17HNPdk3A==", "System", true, "Account", "ADMIN@SMARTBANK.COM", "ADMIN", "+37061224853", "admin@smartbank.com" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountNumber", "CustomerId" },
                values: new object[,]
                {
                    { "1111111111111110", 1 },
                    { "1111111111111111", 1 }
                });

            migrationBuilder.InsertData(
                table: "CustomerBankRoles",
                columns: new[] { "BankRoleId", "CustomerId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "Amount", "Currency", "DestinationAccountId", "SourceAccountId", "Type" },
                values: new object[] { 1, 10000m, "EUR", "1111111111111111", "1111111111111110", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CustomerId",
                table: "Accounts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_BankRoleCustomer_CustomersId",
                table: "BankRoleCustomer",
                column: "CustomersId");

            migrationBuilder.CreateIndex(
                name: "IX_BankRoles_Name",
                table: "BankRoles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankRoles_NormalizedName",
                table: "BankRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBankRoles_BankRoleId",
                table: "CustomerBankRoles",
                column: "BankRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledPaymentItems_ScheduledPaymentId",
                table: "ScheduledPaymentItems",
                column: "ScheduledPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DestinationAccountId",
                table: "Transactions",
                column: "DestinationAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SourceAccountId",
                table: "Transactions",
                column: "SourceAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankRoleCustomer");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "CustomerBankRoles");

            migrationBuilder.DropTable(
                name: "ScheduledPaymentItems");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "BankRoles");

            migrationBuilder.DropTable(
                name: "ScheduledPayments");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
