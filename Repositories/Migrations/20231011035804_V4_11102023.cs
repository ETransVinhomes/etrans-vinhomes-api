using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class V4_11102023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_End_Location_RouteLocation",
                table: "RouteLocation");

            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("40b4f927-a314-463d-88ab-610c317e4d38"));

            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("ee3d06ec-d040-4845-9347-791aaba10e0b"));

            migrationBuilder.DeleteData(
                table: "Provider",
                keyColumn: "Id",
                keyValue: new Guid("5bd3802c-dbf1-4655-82e3-dc7d0269720e"));

            migrationBuilder.DeleteData(
                table: "Provider",
                keyColumn: "Id",
                keyValue: new Guid("95d277cc-3838-4be1-9fc7-100bb5aede2b"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ticket",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "Order",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "LocationType",
                columns: new[] { "Id", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate", "Name" },
                values: new object[,]
                {
                    { new Guid("b43b68fa-dd9a-4f0a-bf8a-4a970af3863c"), null, new DateTime(2023, 10, 11, 10, 58, 4, 808, DateTimeKind.Local).AddTicks(9132), null, null, false, null, null, "Indoor" },
                    { new Guid("d29fbe67-b65d-4097-8f71-f32950cebf89"), null, new DateTime(2023, 10, 11, 10, 58, 4, 808, DateTimeKind.Local).AddTicks(9134), null, null, false, null, null, "Outdoor" }
                });

            migrationBuilder.InsertData(
                table: "Provider",
                columns: new[] { "Id", "Address", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "ExternalId", "IsDeleted", "ModificationBy", "ModificationDate", "Name", "PhoneNumber", "Status" },
                values: new object[,]
                {
                    { new Guid("17d29532-9089-47e8-a8ae-6e3c40613f43"), "VietNam", null, new DateTime(2023, 10, 11, 10, 58, 4, 808, DateTimeKind.Local).AddTicks(8905), null, null, new Guid("00000000-0000-0000-0000-000000000000"), false, null, null, "VinETrans", "0778020298", "Active" },
                    { new Guid("a2a27fd8-8413-42f1-8745-e5a85bb4df65"), "Norway", null, new DateTime(2023, 10, 11, 10, 58, 4, 808, DateTimeKind.Local).AddTicks(8921), null, null, new Guid("00000000-0000-0000-0000-000000000000"), false, null, null, "Phuong Trang ETrans", "0778020298", "Active" }
                });

            migrationBuilder.AddForeignKey(
                name: "Location_RouteLocation",
                table: "RouteLocation",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Location_RouteLocation",
                table: "RouteLocation");

            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("b43b68fa-dd9a-4f0a-bf8a-4a970af3863c"));

            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("d29fbe67-b65d-4097-8f71-f32950cebf89"));

            migrationBuilder.DeleteData(
                table: "Provider",
                keyColumn: "Id",
                keyValue: new Guid("17d29532-9089-47e8-a8ae-6e3c40613f43"));

            migrationBuilder.DeleteData(
                table: "Provider",
                keyColumn: "Id",
                keyValue: new Guid("a2a27fd8-8413-42f1-8745-e5a85bb4df65"));

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Order");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ticket",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "LocationType",
                columns: new[] { "Id", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate", "Name" },
                values: new object[,]
                {
                    { new Guid("40b4f927-a314-463d-88ab-610c317e4d38"), null, new DateTime(2023, 10, 10, 10, 48, 17, 618, DateTimeKind.Local).AddTicks(8443), null, null, false, null, null, "Outdoor" },
                    { new Guid("ee3d06ec-d040-4845-9347-791aaba10e0b"), null, new DateTime(2023, 10, 10, 10, 48, 17, 618, DateTimeKind.Local).AddTicks(8433), null, null, false, null, null, "Indoor" }
                });

            migrationBuilder.InsertData(
                table: "Provider",
                columns: new[] { "Id", "Address", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "ExternalId", "IsDeleted", "ModificationBy", "ModificationDate", "Name", "PhoneNumber", "Status" },
                values: new object[,]
                {
                    { new Guid("5bd3802c-dbf1-4655-82e3-dc7d0269720e"), "VietNam", null, new DateTime(2023, 10, 10, 10, 48, 17, 618, DateTimeKind.Local).AddTicks(8248), null, null, new Guid("00000000-0000-0000-0000-000000000000"), false, null, null, "VinETrans", "0778020298", "Active" },
                    { new Guid("95d277cc-3838-4be1-9fc7-100bb5aede2b"), "Norway", null, new DateTime(2023, 10, 10, 10, 48, 17, 618, DateTimeKind.Local).AddTicks(8262), null, null, new Guid("00000000-0000-0000-0000-000000000000"), false, null, null, "Phuong Trang ETrans", "0778020298", "Active" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_End_Location_RouteLocation",
                table: "RouteLocation",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
