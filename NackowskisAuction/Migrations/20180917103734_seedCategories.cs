using Microsoft.EntityFrameworkCore.Migrations;

namespace NackowskisAuctionHouse.Migrations
{
    public partial class seedCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "2de8d9f2-d911-47b7-8133-24b14a8aef4b", "81b6dfc3-550a-4971-b6fe-040ad2a18b85" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "9edfb9fd-ab3a-44d4-882d-c5834afc6c86", "d9de2171-b771-4246-ae64-8289b34abb72" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "730b8ee0-3576-4f13-8ad5-5b5f84264263", "bf744211-3ee9-460b-8295-523deba955ff", "Admin", "ADMIN" },
                    { "9c4c3c57-7924-4172-b1b3-1efdfb5411a1", "c0204e81-729e-42af-9d59-49dd601377b6", "Regular", "REGULAR" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Klockor" },
                    { 2, "Smycken" },
                    { 3, "Tavlor" },
                    { 4, "Ädelstenar" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "730b8ee0-3576-4f13-8ad5-5b5f84264263", "bf744211-3ee9-460b-8295-523deba955ff" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "9c4c3c57-7924-4172-b1b3-1efdfb5411a1", "c0204e81-729e-42af-9d59-49dd601377b6" });

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9edfb9fd-ab3a-44d4-882d-c5834afc6c86", "d9de2171-b771-4246-ae64-8289b34abb72", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2de8d9f2-d911-47b7-8133-24b14a8aef4b", "81b6dfc3-550a-4971-b6fe-040ad2a18b85", "Regular", "REGULAR" });
        }
    }
}
