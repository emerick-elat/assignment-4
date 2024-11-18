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
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DBConnectionString = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                    table.UniqueConstraint("AK_Tenants_Name", x => x.Name);
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "DBConnectionString", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Server=.;Database=BankDB;Trusted_Connection=True;TrustServerCertificate=True;", null, "DefaultConnectionString" },
                    { 2, "Server=.;Database=BankDB1;Trusted_Connection=True;TrustServerCertificate=True;", null, "Customer1" },
                    { 3, "Server=.;Database=BankDB2;Trusted_Connection=True;TrustServerCertificate=True;", null, "Customer2" },
                    { 4, "Server=.;Database=BankDB3;Trusted_Connection=True;TrustServerCertificate=True;", null, "Customer3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_Name",
                table: "Tenants",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
