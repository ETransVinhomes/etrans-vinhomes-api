using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class V2_ChangeSchema_08102023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("42a44f3c-2995-4d69-9bd3-71f30630f0f3"));

            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("a03be947-c4b0-4905-87b1-0d649c09edb9"));

            migrationBuilder.DeleteData(
                table: "Provider",
                keyColumn: "Id",
                keyValue: new Guid("98b88a43-257c-40b3-a09e-2dc78a96b368"));

            migrationBuilder.DeleteData(
                table: "Provider",
                keyColumn: "Id",
                keyValue: new Guid("c2df8982-24e1-4d78-b0ba-b2e1c7801183"));

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Driver",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Driver",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Provider",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "ExternalId",
                table: "Provider",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<bool>(
                name: "Sex",
                table: "Driver",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Driver",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<Guid>(
                name: "ExternalId",
                table: "Driver",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ExternalId",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "LocationType",
                columns: new[] { "Id", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate", "Name" },
                values: new object[,]
                {
                    { new Guid("0453ff94-f5a1-47f8-be07-8e687ef0513e"), null, new DateTime(2023, 10, 8, 20, 39, 2, 145, DateTimeKind.Local).AddTicks(7730), null, null, false, null, null, "Indoor" },
                    { new Guid("be7348c9-0653-4d1e-92bf-cf144a15976a"), null, new DateTime(2023, 10, 8, 20, 39, 2, 145, DateTimeKind.Local).AddTicks(7733), null, null, false, null, null, "Outdoor" }
                });

            migrationBuilder.InsertData(
                table: "Provider",
                columns: new[] { "Id", "Address", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "ExternalId", "IsDeleted", "ModificationBy", "ModificationDate", "Name", "PhoneNumber", "Status" },
                values: new object[,]
                {
                    { new Guid("563d5c2e-31d7-4d8f-98c5-2ef8c6ca6fc8"), "VietNam", null, new DateTime(2023, 10, 8, 20, 39, 2, 145, DateTimeKind.Local).AddTicks(7479), null, null, new Guid("00000000-0000-0000-0000-000000000000"), false, null, null, "VinETrans", "0778020298", "Active" },
                    { new Guid("ec1813e1-b945-4b1f-a179-52d63e024aab"), "Norway", null, new DateTime(2023, 10, 8, 20, 39, 2, 145, DateTimeKind.Local).AddTicks(7499), null, null, new Guid("00000000-0000-0000-0000-000000000000"), false, null, null, "Phuong Trang ETrans", "0778020298", "Active" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("0453ff94-f5a1-47f8-be07-8e687ef0513e"));

            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("be7348c9-0653-4d1e-92bf-cf144a15976a"));

            migrationBuilder.DeleteData(
                table: "Provider",
                keyColumn: "Id",
                keyValue: new Guid("563d5c2e-31d7-4d8f-98c5-2ef8c6ca6fc8"));

            migrationBuilder.DeleteData(
                table: "Provider",
                keyColumn: "Id",
                keyValue: new Guid("ec1813e1-b945-4b1f-a179-52d63e024aab"));

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Provider");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Driver",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Driver",
                newName: "FirstName");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Provider",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Sex",
                table: "Driver",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Driver",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "LocationType",
                columns: new[] { "Id", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate", "Name" },
                values: new object[,]
                {
                    { new Guid("42a44f3c-2995-4d69-9bd3-71f30630f0f3"), null, new DateTime(2023, 10, 7, 17, 33, 25, 719, DateTimeKind.Local).AddTicks(4218), null, null, false, null, null, "Outdoor" },
                    { new Guid("a03be947-c4b0-4905-87b1-0d649c09edb9"), null, new DateTime(2023, 10, 7, 17, 33, 25, 719, DateTimeKind.Local).AddTicks(4216), null, null, false, null, null, "Indoor" }
                });

            migrationBuilder.InsertData(
                table: "Provider",
                columns: new[] { "Id", "Address", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate", "Name", "PhoneNumber", "Status" },
                values: new object[,]
                {
                    { new Guid("98b88a43-257c-40b3-a09e-2dc78a96b368"), "VietNam", null, new DateTime(2023, 10, 7, 17, 33, 25, 719, DateTimeKind.Local).AddTicks(4018), null, null, false, null, null, "VinETrans", "0778020298", "Active" },
                    { new Guid("c2df8982-24e1-4d78-b0ba-b2e1c7801183"), "Norway", null, new DateTime(2023, 10, 7, 17, 33, 25, 719, DateTimeKind.Local).AddTicks(4037), null, null, false, null, null, "Phuong Trang ETrans", "0778020298", "Active" }
                });
        }
    }
}
