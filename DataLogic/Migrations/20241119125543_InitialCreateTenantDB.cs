using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateTenantDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Server = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatabaseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DBUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DBPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTrustedConnection = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    TrusServerCertificate = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                    table.UniqueConstraint("AK_Tenants_Name", x => x.Name);
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "DBPassword", "DBUser", "DatabaseName", "Description", "Name", "Server" },
                values: new object[,]
                {
                    { 1, null, null, "VentionBankDB", null, "DefaultConnectionString", "." },
                    { 2, "#Include <stdio.h>", "sa", "BankDB1", null, "Customer1", "local" }
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "DBPassword", "DBUser", "DatabaseName", "Description", "Name", "Server", "TrusServerCertificate" },
                values: new object[,]
                {
                    { 3, "#Lithu@nia&Baltic$>", "sa", "BkDB2", null, "Customer2", ".", true },
                    { 4, "P@$$w0rd!", "sa", "BankDB3", null, "Customer3", ".", true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
