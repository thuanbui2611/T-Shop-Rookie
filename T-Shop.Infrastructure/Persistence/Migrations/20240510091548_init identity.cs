using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace T_Shop.Infrastructure.Migrations
{
    public partial class initidentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("077639ee-e1bb-4949-8185-305b2916257f"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("046e9684-ee35-4ed0-a860-48f834bf5332"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("0c60e000-7de5-4291-89da-7c79fcf21427"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("0e8cf19a-6432-482b-bd68-1bc088a83d05"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("15c8e0f8-f168-4624-aac7-2398440119a3"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("204dbde1-5e1e-404e-8cf4-2950351f8ac9"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("206463da-3dfe-4b27-9def-88c92c0f69aa"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("22efa751-881f-4b82-ba8b-1ea0fa91d0c5"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("235a207c-79cc-4e3d-8c18-724c677b4d10"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("24e18eba-d78c-484f-a29f-406d5c445b65"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("264858ac-c654-452b-9491-a3e71baea32e"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("26b9e1ed-502f-45b1-bf2d-f4906fd64969"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("29d1bae2-a396-4cc8-805c-66dc07a72129"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("32834c4b-2c41-452d-a532-8a4c0719737e"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("36365cdc-3d45-4b3d-a712-65d9d43159d8"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("38f65a99-b877-4d27-9708-e1042bcaf4a8"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("4a072176-1904-4fe8-add2-c9f4a6cf66ca"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("525a8b9a-58ad-41fd-8dbf-619750774052"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("5324cedc-de16-4527-9ac4-7ced93ae3f9f"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("5a75391b-9145-482a-89e1-976fdd97ef63"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("5bc21063-d5e4-4352-874d-1b30c2987e65"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("613db9dd-ca44-4502-9e40-75f5ee12120f"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("68926ca6-cef9-4d5e-997d-3e679f1124d6"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("6aa34f7e-b29d-4311-92db-02dfa502ec2c"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("7611f5fa-48a2-400e-aee8-0c18d98ef601"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("76fbfd4e-00bf-480f-97e5-ea3e311ceed8"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("812c0465-e278-4a33-a1af-773ebf71e430"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("8407e4fb-614c-46ae-a932-a0a8b9e250ac"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("943c99d0-cbc5-40d6-97b4-f8234b7d3fb4"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("944aa228-4774-48fb-95ae-2ca567b0617a"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("94dd4d20-b288-4643-a321-cad7c9496289"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("97fdca4f-d6c1-44aa-934d-4cc6a01012c2"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("a2e08dee-1059-4658-a06f-aebdda4ff89f"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("ab12932a-84bb-4f33-893a-ca76d3475a6e"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("acac2545-72a2-497d-9e92-b40223c82299"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("b86b6488-378e-48d7-8c2f-61beb30552d8"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("bd1f389d-f2ce-438a-948b-725c9f19e0e4"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("bf114e9a-a8cc-4b75-86a0-ba2b4bda27b1"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("c146640c-814e-456b-bdf9-b286833272de"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("c78ce2fb-ce64-4d27-a8f4-7c8213362399"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("ccc95d89-f814-4109-9e3d-fbf15a2d2b33"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("cde59207-766f-437f-84fc-9a4decf5552b"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("cecf42f9-6dff-4669-8bda-0481b4b53315"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("d4893841-a899-4923-b095-979ae8b77735"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("d72ea2e6-a71c-4fec-99fe-f61b84a8af93"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("d8b7b4ea-64aa-4c28-9c12-4bcc47ccdd3c"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("da6a21a8-884a-48b6-aa4f-aad33c6b5ea0"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("dace0360-2fb7-43f0-96e4-2a742c556a79"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("e8ba087a-3cd9-46a3-8e5d-536da6185020"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("f55932c1-7f16-4f45-9be2-4bb9690b18b7"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("fa7d520e-ebc0-4ad6-83d4-d99763f65e49"));

            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("0c5390cc-3816-4576-94a8-6b7822046087"));

            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("1d462595-6426-4fee-b441-c4211ffc006b"));

            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("21ba1988-613f-4f87-83d5-7ac61537e102"));

            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("5de9c997-158d-4560-beaa-d9c0c93fcbbc"));

            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("6bf5f935-efe4-4672-acca-65141d6eab42"));

            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("9faf2697-b623-4849-ae76-5ddbe4b72eac"));

            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("a264dd50-0232-4a61-b81b-3ec5c51eec81"));

            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("aac49e46-fe61-4529-9b46-7c5959b98150"));

            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("c341573b-e057-4528-bc6a-c65599e0ca5f"));

            migrationBuilder.CreateTable(
                name: "table_roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "table_users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "text", nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    gender = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: false),
                    avatar = table.Column<string>(type: "text", nullable: true),
                    is_locked = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "table_roleclaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_roleclaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_table_roleclaims_table_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "table_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "table_userclaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_userclaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_table_userclaims_table_users_UserId",
                        column: x => x.UserId,
                        principalTable: "table_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "table_userlogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_userlogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_table_userlogins_table_users_UserId",
                        column: x => x.UserId,
                        principalTable: "table_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "table_userroles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_userroles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_table_userroles_table_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "table_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_table_userroles_table_users_UserId",
                        column: x => x.UserId,
                        principalTable: "table_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "table_usertokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_usertokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_table_usertokens_table_users_UserId",
                        column: x => x.UserId,
                        principalTable: "table_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "table_category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0c2335a2-198e-4249-ab58-0b2efe786eca"), "Category 1" },
                    { new Guid("118c0104-7a65-4d62-b84b-541f7bb7c7fb"), "Category 2" },
                    { new Guid("30a6466d-3072-421d-944c-e5804068f2cf"), "Category 9" },
                    { new Guid("578067c9-b98c-4d28-84f4-d56eca2977eb"), "Category 4" },
                    { new Guid("5f36e0cb-4e0b-407e-96f1-7ba4fd5eaa2d"), "Category 10" },
                    { new Guid("690710bb-8ce9-4ef0-9a29-f4bf6b7ac49f"), "Category 3" },
                    { new Guid("76f175f0-ad3d-4a1e-9d70-048759c700a1"), "Category 6" },
                    { new Guid("90bf0c0d-28a8-4a69-b945-a19ba3d025c0"), "Category 7" },
                    { new Guid("c488f17d-4ae4-4d9c-a185-133011b797ac"), "Category 5" },
                    { new Guid("eb7ddb8a-15d4-410f-8aea-b92c8fa43ff3"), "Category 8" }
                });

            migrationBuilder.InsertData(
                table: "table_product",
                columns: new[] { "Id", "FK_category_id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("015a2ac8-e680-4b84-b81c-c888aa68b2f6"), new Guid("5f36e0cb-4e0b-407e-96f1-7ba4fd5eaa2d"), "This is description for product 45", "Product 45" },
                    { new Guid("0b1c2b57-a28a-4b2d-85a9-0200360ea508"), new Guid("5f36e0cb-4e0b-407e-96f1-7ba4fd5eaa2d"), "This is description for product 44", "Product 44" },
                    { new Guid("1494bf36-d8d1-4cdd-9e57-1c0dde5ee08b"), new Guid("5f36e0cb-4e0b-407e-96f1-7ba4fd5eaa2d"), "This is description for product 34", "Product 34" },
                    { new Guid("16b5f375-e019-4dac-b2cb-c0a00799f4e7"), new Guid("30a6466d-3072-421d-944c-e5804068f2cf"), "This is description for product 42", "Product 42" },
                    { new Guid("1c4146c8-0c25-4545-80ab-9de95abe48d2"), new Guid("690710bb-8ce9-4ef0-9a29-f4bf6b7ac49f"), "This is description for product 8", "Product 8" },
                    { new Guid("23e54217-a00c-4881-88db-7a9550d8bc6c"), new Guid("690710bb-8ce9-4ef0-9a29-f4bf6b7ac49f"), "This is description for product 23", "Product 23" },
                    { new Guid("2495dbb3-c009-4804-9e75-cb9dbaaafbd0"), new Guid("30a6466d-3072-421d-944c-e5804068f2cf"), "This is description for product 33", "Product 33" },
                    { new Guid("24d788f1-210c-408b-9628-2c0ea64f3a35"), new Guid("eb7ddb8a-15d4-410f-8aea-b92c8fa43ff3"), "This is description for product 40", "Product 40" },
                    { new Guid("275b5648-a15e-4bc3-9ebd-bc6c0576e19d"), new Guid("690710bb-8ce9-4ef0-9a29-f4bf6b7ac49f"), "This is description for product 22", "Product 22" },
                    { new Guid("2d35ccc2-8c9b-4f11-a334-437cc81b4898"), new Guid("76f175f0-ad3d-4a1e-9d70-048759c700a1"), "This is description for product 37", "Product 37" },
                    { new Guid("30e0d42c-ba6f-4ce7-bb04-bb0545f7908f"), new Guid("690710bb-8ce9-4ef0-9a29-f4bf6b7ac49f"), "This is description for product 35", "Product 35" },
                    { new Guid("36e51683-f681-430d-adf1-c664f45dc90c"), new Guid("eb7ddb8a-15d4-410f-8aea-b92c8fa43ff3"), "This is description for product 39", "Product 39" },
                    { new Guid("3d9a656f-d3dc-481f-83b6-7f3a90305335"), new Guid("76f175f0-ad3d-4a1e-9d70-048759c700a1"), "This is description for product 7", "Product 7" },
                    { new Guid("40687eb4-6d3c-4199-80cb-5fcb048115a7"), new Guid("eb7ddb8a-15d4-410f-8aea-b92c8fa43ff3"), "This is description for product 36", "Product 36" },
                    { new Guid("4209b9ff-5eeb-46d3-9486-1d13953c9958"), new Guid("30a6466d-3072-421d-944c-e5804068f2cf"), "This is description for product 26", "Product 26" },
                    { new Guid("46cc6553-b23d-4600-aa38-1c6cbc5a1fab"), new Guid("578067c9-b98c-4d28-84f4-d56eca2977eb"), "This is description for product 5", "Product 5" },
                    { new Guid("534fd1b9-bd12-4b01-86e6-2e0c0942f0df"), new Guid("5f36e0cb-4e0b-407e-96f1-7ba4fd5eaa2d"), "This is description for product 11", "Product 11" },
                    { new Guid("564298ef-303d-469e-8ca8-0ef46993843b"), new Guid("578067c9-b98c-4d28-84f4-d56eca2977eb"), "This is description for product 6", "Product 6" },
                    { new Guid("5d75ffad-6a03-470f-b6a3-935cd0e00840"), new Guid("578067c9-b98c-4d28-84f4-d56eca2977eb"), "This is description for product 24", "Product 24" },
                    { new Guid("6071d6af-55a2-4f71-ad3f-9ff7784058e0"), new Guid("5f36e0cb-4e0b-407e-96f1-7ba4fd5eaa2d"), "This is description for product 50", "Product 50" },
                    { new Guid("64669b2c-8420-4972-b6e3-dab1861abca0"), new Guid("eb7ddb8a-15d4-410f-8aea-b92c8fa43ff3"), "This is description for product 9", "Product 9" },
                    { new Guid("6c754343-8261-4fd6-aed5-7468b30efa00"), new Guid("578067c9-b98c-4d28-84f4-d56eca2977eb"), "This is description for product 29", "Product 29" },
                    { new Guid("6cdc3ebc-026b-4e45-b9e8-436f0d911caa"), new Guid("30a6466d-3072-421d-944c-e5804068f2cf"), "This is description for product 10", "Product 10" },
                    { new Guid("6d81efc9-c6d0-49a2-bfde-3464dda86fa4"), new Guid("30a6466d-3072-421d-944c-e5804068f2cf"), "This is description for product 38", "Product 38" },
                    { new Guid("6e366946-7ae2-443d-bde3-4193c1d8d165"), new Guid("76f175f0-ad3d-4a1e-9d70-048759c700a1"), "This is description for product 28", "Product 28" },
                    { new Guid("82ab0d01-12ad-47b1-8849-ac6060dbb31f"), new Guid("118c0104-7a65-4d62-b84b-541f7bb7c7fb"), "This is description for product 13", "Product 13" },
                    { new Guid("86d1f6b2-2100-491b-b972-6275a4d150a3"), new Guid("118c0104-7a65-4d62-b84b-541f7bb7c7fb"), "This is description for product 43", "Product 43" },
                    { new Guid("8bff6e57-b499-4845-a01d-64cb80c620af"), new Guid("76f175f0-ad3d-4a1e-9d70-048759c700a1"), "This is description for product 14", "Product 14" },
                    { new Guid("90a205cd-cb03-4a29-94b5-4162ba929390"), new Guid("90bf0c0d-28a8-4a69-b945-a19ba3d025c0"), "This is description for product 47", "Product 47" },
                    { new Guid("9191cc77-6570-4f06-b610-0b58cb98f0a6"), new Guid("eb7ddb8a-15d4-410f-8aea-b92c8fa43ff3"), "This is description for product 4", "Product 4" },
                    { new Guid("9378fc40-30e8-40cf-a29c-df69e2d7fba2"), new Guid("5f36e0cb-4e0b-407e-96f1-7ba4fd5eaa2d"), "This is description for product 17", "Product 17" },
                    { new Guid("96603428-d2c8-4c86-bd4c-d04f895e846e"), new Guid("690710bb-8ce9-4ef0-9a29-f4bf6b7ac49f"), "This is description for product 48", "Product 48" },
                    { new Guid("a93fdd17-647c-447b-b364-420a586d904c"), new Guid("690710bb-8ce9-4ef0-9a29-f4bf6b7ac49f"), "This is description for product 12", "Product 12" },
                    { new Guid("b8604078-e7c7-4de2-9bb7-f66b33bc03bf"), new Guid("c488f17d-4ae4-4d9c-a185-133011b797ac"), "This is description for product 27", "Product 27" },
                    { new Guid("b89555f8-3318-4f16-b573-f6512988382b"), new Guid("578067c9-b98c-4d28-84f4-d56eca2977eb"), "This is description for product 3", "Product 3" },
                    { new Guid("b9bc1e84-fdcf-471b-bcc4-769231d2cd02"), new Guid("118c0104-7a65-4d62-b84b-541f7bb7c7fb"), "This is description for product 20", "Product 20" },
                    { new Guid("c0e8fc20-af48-41fb-b67c-3557196b13fe"), new Guid("5f36e0cb-4e0b-407e-96f1-7ba4fd5eaa2d"), "This is description for product 15", "Product 15" },
                    { new Guid("c6f69d7b-efd7-4406-83f0-19d7493c8dcc"), new Guid("90bf0c0d-28a8-4a69-b945-a19ba3d025c0"), "This is description for product 46", "Product 46" },
                    { new Guid("c7a01852-be4c-44de-a730-6a61c209cb29"), new Guid("90bf0c0d-28a8-4a69-b945-a19ba3d025c0"), "This is description for product 16", "Product 16" },
                    { new Guid("c955add9-8e18-46a3-a40e-8150d5870454"), new Guid("118c0104-7a65-4d62-b84b-541f7bb7c7fb"), "This is description for product 21", "Product 21" },
                    { new Guid("c9a48986-598b-4972-88f4-8ebc680791b1"), new Guid("578067c9-b98c-4d28-84f4-d56eca2977eb"), "This is description for product 1", "Product 1" },
                    { new Guid("cbe21e9e-b852-44d6-b217-184edd29f15b"), new Guid("118c0104-7a65-4d62-b84b-541f7bb7c7fb"), "This is description for product 19", "Product 19" },
                    { new Guid("d39f3bee-1e4f-4286-94a3-7ccead8eb954"), new Guid("76f175f0-ad3d-4a1e-9d70-048759c700a1"), "This is description for product 25", "Product 25" },
                    { new Guid("d551fa60-f25e-4ae4-a640-5cfded9513f4"), new Guid("5f36e0cb-4e0b-407e-96f1-7ba4fd5eaa2d"), "This is description for product 49", "Product 49" },
                    { new Guid("d97b36c1-a911-4e47-a51f-0946d8e2fa87"), new Guid("76f175f0-ad3d-4a1e-9d70-048759c700a1"), "This is description for product 2", "Product 2" },
                    { new Guid("ebab8e76-507b-4931-a296-6c29dd696fd8"), new Guid("5f36e0cb-4e0b-407e-96f1-7ba4fd5eaa2d"), "This is description for product 41", "Product 41" },
                    { new Guid("ec771ebc-76c1-4ae6-b0c3-1a35b7835165"), new Guid("eb7ddb8a-15d4-410f-8aea-b92c8fa43ff3"), "This is description for product 31", "Product 31" },
                    { new Guid("f5e79bb7-8920-4ee2-bc4a-70b9d097bdad"), new Guid("30a6466d-3072-421d-944c-e5804068f2cf"), "This is description for product 18", "Product 18" },
                    { new Guid("f9db7740-cfe5-4bc3-baab-fc8478a190ce"), new Guid("c488f17d-4ae4-4d9c-a185-133011b797ac"), "This is description for product 32", "Product 32" },
                    { new Guid("ffc4676b-b8a2-4f97-babd-9b8bc27e528e"), new Guid("90bf0c0d-28a8-4a69-b945-a19ba3d025c0"), "This is description for product 30", "Product 30" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_table_roleclaims_RoleId",
                table: "table_roleclaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "table_roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_table_userclaims_UserId",
                table: "table_userclaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_table_userlogins_UserId",
                table: "table_userlogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_table_userroles_RoleId",
                table: "table_userroles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "table_users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "table_users",
                column: "NormalizedUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "table_roleclaims");

            migrationBuilder.DropTable(
                name: "table_userclaims");

            migrationBuilder.DropTable(
                name: "table_userlogins");

            migrationBuilder.DropTable(
                name: "table_userroles");

            migrationBuilder.DropTable(
                name: "table_usertokens");

            migrationBuilder.DropTable(
                name: "table_roles");

            migrationBuilder.DropTable(
                name: "table_users");

            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("0c2335a2-198e-4249-ab58-0b2efe786eca"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("015a2ac8-e680-4b84-b81c-c888aa68b2f6"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("0b1c2b57-a28a-4b2d-85a9-0200360ea508"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("1494bf36-d8d1-4cdd-9e57-1c0dde5ee08b"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("16b5f375-e019-4dac-b2cb-c0a00799f4e7"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("1c4146c8-0c25-4545-80ab-9de95abe48d2"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("23e54217-a00c-4881-88db-7a9550d8bc6c"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("2495dbb3-c009-4804-9e75-cb9dbaaafbd0"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("24d788f1-210c-408b-9628-2c0ea64f3a35"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("275b5648-a15e-4bc3-9ebd-bc6c0576e19d"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("2d35ccc2-8c9b-4f11-a334-437cc81b4898"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("30e0d42c-ba6f-4ce7-bb04-bb0545f7908f"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("36e51683-f681-430d-adf1-c664f45dc90c"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("3d9a656f-d3dc-481f-83b6-7f3a90305335"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("40687eb4-6d3c-4199-80cb-5fcb048115a7"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("4209b9ff-5eeb-46d3-9486-1d13953c9958"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("46cc6553-b23d-4600-aa38-1c6cbc5a1fab"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("534fd1b9-bd12-4b01-86e6-2e0c0942f0df"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("564298ef-303d-469e-8ca8-0ef46993843b"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("5d75ffad-6a03-470f-b6a3-935cd0e00840"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("6071d6af-55a2-4f71-ad3f-9ff7784058e0"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("64669b2c-8420-4972-b6e3-dab1861abca0"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("6c754343-8261-4fd6-aed5-7468b30efa00"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("6cdc3ebc-026b-4e45-b9e8-436f0d911caa"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("6d81efc9-c6d0-49a2-bfde-3464dda86fa4"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("6e366946-7ae2-443d-bde3-4193c1d8d165"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("82ab0d01-12ad-47b1-8849-ac6060dbb31f"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("86d1f6b2-2100-491b-b972-6275a4d150a3"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("8bff6e57-b499-4845-a01d-64cb80c620af"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("90a205cd-cb03-4a29-94b5-4162ba929390"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("9191cc77-6570-4f06-b610-0b58cb98f0a6"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("9378fc40-30e8-40cf-a29c-df69e2d7fba2"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("96603428-d2c8-4c86-bd4c-d04f895e846e"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("a93fdd17-647c-447b-b364-420a586d904c"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("b8604078-e7c7-4de2-9bb7-f66b33bc03bf"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("b89555f8-3318-4f16-b573-f6512988382b"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("b9bc1e84-fdcf-471b-bcc4-769231d2cd02"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("c0e8fc20-af48-41fb-b67c-3557196b13fe"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("c6f69d7b-efd7-4406-83f0-19d7493c8dcc"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("c7a01852-be4c-44de-a730-6a61c209cb29"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("c955add9-8e18-46a3-a40e-8150d5870454"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("c9a48986-598b-4972-88f4-8ebc680791b1"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("cbe21e9e-b852-44d6-b217-184edd29f15b"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("d39f3bee-1e4f-4286-94a3-7ccead8eb954"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("d551fa60-f25e-4ae4-a640-5cfded9513f4"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("d97b36c1-a911-4e47-a51f-0946d8e2fa87"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("ebab8e76-507b-4931-a296-6c29dd696fd8"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("ec771ebc-76c1-4ae6-b0c3-1a35b7835165"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("f5e79bb7-8920-4ee2-bc4a-70b9d097bdad"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("f9db7740-cfe5-4bc3-baab-fc8478a190ce"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("ffc4676b-b8a2-4f97-babd-9b8bc27e528e"));

            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("118c0104-7a65-4d62-b84b-541f7bb7c7fb"));

            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("30a6466d-3072-421d-944c-e5804068f2cf"));

            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("578067c9-b98c-4d28-84f4-d56eca2977eb"));

            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("5f36e0cb-4e0b-407e-96f1-7ba4fd5eaa2d"));

            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("690710bb-8ce9-4ef0-9a29-f4bf6b7ac49f"));

            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("76f175f0-ad3d-4a1e-9d70-048759c700a1"));

            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("90bf0c0d-28a8-4a69-b945-a19ba3d025c0"));

            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("c488f17d-4ae4-4d9c-a185-133011b797ac"));

            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("eb7ddb8a-15d4-410f-8aea-b92c8fa43ff3"));

            migrationBuilder.InsertData(
                table: "table_category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("077639ee-e1bb-4949-8185-305b2916257f"), "Category1" },
                    { new Guid("0c5390cc-3816-4576-94a8-6b7822046087"), "Category8" },
                    { new Guid("1d462595-6426-4fee-b441-c4211ffc006b"), "Category3" },
                    { new Guid("21ba1988-613f-4f87-83d5-7ac61537e102"), "Category7" },
                    { new Guid("5de9c997-158d-4560-beaa-d9c0c93fcbbc"), "Category10" },
                    { new Guid("6bf5f935-efe4-4672-acca-65141d6eab42"), "Category6" },
                    { new Guid("9faf2697-b623-4849-ae76-5ddbe4b72eac"), "Category9" },
                    { new Guid("a264dd50-0232-4a61-b81b-3ec5c51eec81"), "Category5" },
                    { new Guid("aac49e46-fe61-4529-9b46-7c5959b98150"), "Category2" },
                    { new Guid("c341573b-e057-4528-bc6a-c65599e0ca5f"), "Category4" }
                });

            migrationBuilder.InsertData(
                table: "table_product",
                columns: new[] { "Id", "FK_category_id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("046e9684-ee35-4ed0-a860-48f834bf5332"), new Guid("0c5390cc-3816-4576-94a8-6b7822046087"), "This is description for product33", "Product33" },
                    { new Guid("0c60e000-7de5-4291-89da-7c79fcf21427"), new Guid("c341573b-e057-4528-bc6a-c65599e0ca5f"), "This is description for product37", "Product37" },
                    { new Guid("0e8cf19a-6432-482b-bd68-1bc088a83d05"), new Guid("21ba1988-613f-4f87-83d5-7ac61537e102"), "This is description for product36", "Product36" },
                    { new Guid("15c8e0f8-f168-4624-aac7-2398440119a3"), new Guid("a264dd50-0232-4a61-b81b-3ec5c51eec81"), "This is description for product46", "Product46" },
                    { new Guid("204dbde1-5e1e-404e-8cf4-2950351f8ac9"), new Guid("1d462595-6426-4fee-b441-c4211ffc006b"), "This is description for product3", "Product3" },
                    { new Guid("206463da-3dfe-4b27-9def-88c92c0f69aa"), new Guid("a264dd50-0232-4a61-b81b-3ec5c51eec81"), "This is description for product7", "Product7" },
                    { new Guid("22efa751-881f-4b82-ba8b-1ea0fa91d0c5"), new Guid("c341573b-e057-4528-bc6a-c65599e0ca5f"), "This is description for product4", "Product4" },
                    { new Guid("235a207c-79cc-4e3d-8c18-724c677b4d10"), new Guid("0c5390cc-3816-4576-94a8-6b7822046087"), "This is description for product27", "Product27" },
                    { new Guid("24e18eba-d78c-484f-a29f-406d5c445b65"), new Guid("c341573b-e057-4528-bc6a-c65599e0ca5f"), "This is description for product48", "Product48" },
                    { new Guid("264858ac-c654-452b-9491-a3e71baea32e"), new Guid("c341573b-e057-4528-bc6a-c65599e0ca5f"), "This is description for product23", "Product23" },
                    { new Guid("26b9e1ed-502f-45b1-bf2d-f4906fd64969"), new Guid("1d462595-6426-4fee-b441-c4211ffc006b"), "This is description for product26", "Product26" },
                    { new Guid("29d1bae2-a396-4cc8-805c-66dc07a72129"), new Guid("c341573b-e057-4528-bc6a-c65599e0ca5f"), "This is description for product47", "Product47" },
                    { new Guid("32834c4b-2c41-452d-a532-8a4c0719737e"), new Guid("0c5390cc-3816-4576-94a8-6b7822046087"), "This is description for product35", "Product35" },
                    { new Guid("36365cdc-3d45-4b3d-a712-65d9d43159d8"), new Guid("0c5390cc-3816-4576-94a8-6b7822046087"), "This is description for product43", "Product43" },
                    { new Guid("38f65a99-b877-4d27-9708-e1042bcaf4a8"), new Guid("21ba1988-613f-4f87-83d5-7ac61537e102"), "This is description for product12", "Product12" },
                    { new Guid("4a072176-1904-4fe8-add2-c9f4a6cf66ca"), new Guid("c341573b-e057-4528-bc6a-c65599e0ca5f"), "This is description for product16", "Product16" },
                    { new Guid("525a8b9a-58ad-41fd-8dbf-619750774052"), new Guid("c341573b-e057-4528-bc6a-c65599e0ca5f"), "This is description for product24", "Product24" },
                    { new Guid("5324cedc-de16-4527-9ac4-7ced93ae3f9f"), new Guid("1d462595-6426-4fee-b441-c4211ffc006b"), "This is description for product44", "Product44" },
                    { new Guid("5a75391b-9145-482a-89e1-976fdd97ef63"), new Guid("aac49e46-fe61-4529-9b46-7c5959b98150"), "This is description for product38", "Product38" },
                    { new Guid("5bc21063-d5e4-4352-874d-1b30c2987e65"), new Guid("1d462595-6426-4fee-b441-c4211ffc006b"), "This is description for product28", "Product28" },
                    { new Guid("613db9dd-ca44-4502-9e40-75f5ee12120f"), new Guid("c341573b-e057-4528-bc6a-c65599e0ca5f"), "This is description for product31", "Product31" },
                    { new Guid("68926ca6-cef9-4d5e-997d-3e679f1124d6"), new Guid("5de9c997-158d-4560-beaa-d9c0c93fcbbc"), "This is description for product18", "Product18" },
                    { new Guid("6aa34f7e-b29d-4311-92db-02dfa502ec2c"), new Guid("21ba1988-613f-4f87-83d5-7ac61537e102"), "This is description for product6", "Product6" },
                    { new Guid("7611f5fa-48a2-400e-aee8-0c18d98ef601"), new Guid("0c5390cc-3816-4576-94a8-6b7822046087"), "This is description for product50", "Product50" },
                    { new Guid("76fbfd4e-00bf-480f-97e5-ea3e311ceed8"), new Guid("c341573b-e057-4528-bc6a-c65599e0ca5f"), "This is description for product22", "Product22" },
                    { new Guid("812c0465-e278-4a33-a1af-773ebf71e430"), new Guid("0c5390cc-3816-4576-94a8-6b7822046087"), "This is description for product34", "Product34" },
                    { new Guid("8407e4fb-614c-46ae-a932-a0a8b9e250ac"), new Guid("6bf5f935-efe4-4672-acca-65141d6eab42"), "This is description for product1", "Product1" },
                    { new Guid("943c99d0-cbc5-40d6-97b4-f8234b7d3fb4"), new Guid("21ba1988-613f-4f87-83d5-7ac61537e102"), "This is description for product2", "Product2" },
                    { new Guid("944aa228-4774-48fb-95ae-2ca567b0617a"), new Guid("6bf5f935-efe4-4672-acca-65141d6eab42"), "This is description for product49", "Product49" },
                    { new Guid("94dd4d20-b288-4643-a321-cad7c9496289"), new Guid("9faf2697-b623-4849-ae76-5ddbe4b72eac"), "This is description for product41", "Product41" },
                    { new Guid("97fdca4f-d6c1-44aa-934d-4cc6a01012c2"), new Guid("5de9c997-158d-4560-beaa-d9c0c93fcbbc"), "This is description for product29", "Product29" },
                    { new Guid("a2e08dee-1059-4658-a06f-aebdda4ff89f"), new Guid("c341573b-e057-4528-bc6a-c65599e0ca5f"), "This is description for product40", "Product40" },
                    { new Guid("ab12932a-84bb-4f33-893a-ca76d3475a6e"), new Guid("6bf5f935-efe4-4672-acca-65141d6eab42"), "This is description for product39", "Product39" },
                    { new Guid("acac2545-72a2-497d-9e92-b40223c82299"), new Guid("1d462595-6426-4fee-b441-c4211ffc006b"), "This is description for product9", "Product9" },
                    { new Guid("b86b6488-378e-48d7-8c2f-61beb30552d8"), new Guid("aac49e46-fe61-4529-9b46-7c5959b98150"), "This is description for product21", "Product21" },
                    { new Guid("bd1f389d-f2ce-438a-948b-725c9f19e0e4"), new Guid("9faf2697-b623-4849-ae76-5ddbe4b72eac"), "This is description for product30", "Product30" },
                    { new Guid("bf114e9a-a8cc-4b75-86a0-ba2b4bda27b1"), new Guid("9faf2697-b623-4849-ae76-5ddbe4b72eac"), "This is description for product32", "Product32" },
                    { new Guid("c146640c-814e-456b-bdf9-b286833272de"), new Guid("c341573b-e057-4528-bc6a-c65599e0ca5f"), "This is description for product10", "Product10" },
                    { new Guid("c78ce2fb-ce64-4d27-a8f4-7c8213362399"), new Guid("21ba1988-613f-4f87-83d5-7ac61537e102"), "This is description for product14", "Product14" },
                    { new Guid("ccc95d89-f814-4109-9e3d-fbf15a2d2b33"), new Guid("c341573b-e057-4528-bc6a-c65599e0ca5f"), "This is description for product8", "Product8" },
                    { new Guid("cde59207-766f-437f-84fc-9a4decf5552b"), new Guid("9faf2697-b623-4849-ae76-5ddbe4b72eac"), "This is description for product20", "Product20" },
                    { new Guid("cecf42f9-6dff-4669-8bda-0481b4b53315"), new Guid("21ba1988-613f-4f87-83d5-7ac61537e102"), "This is description for product5", "Product5" },
                    { new Guid("d4893841-a899-4923-b095-979ae8b77735"), new Guid("5de9c997-158d-4560-beaa-d9c0c93fcbbc"), "This is description for product25", "Product25" },
                    { new Guid("d72ea2e6-a71c-4fec-99fe-f61b84a8af93"), new Guid("6bf5f935-efe4-4672-acca-65141d6eab42"), "This is description for product19", "Product19" },
                    { new Guid("d8b7b4ea-64aa-4c28-9c12-4bcc47ccdd3c"), new Guid("aac49e46-fe61-4529-9b46-7c5959b98150"), "This is description for product45", "Product45" },
                    { new Guid("da6a21a8-884a-48b6-aa4f-aad33c6b5ea0"), new Guid("1d462595-6426-4fee-b441-c4211ffc006b"), "This is description for product11", "Product11" },
                    { new Guid("dace0360-2fb7-43f0-96e4-2a742c556a79"), new Guid("c341573b-e057-4528-bc6a-c65599e0ca5f"), "This is description for product13", "Product13" },
                    { new Guid("e8ba087a-3cd9-46a3-8e5d-536da6185020"), new Guid("0c5390cc-3816-4576-94a8-6b7822046087"), "This is description for product42", "Product42" },
                    { new Guid("f55932c1-7f16-4f45-9be2-4bb9690b18b7"), new Guid("9faf2697-b623-4849-ae76-5ddbe4b72eac"), "This is description for product17", "Product17" },
                    { new Guid("fa7d520e-ebc0-4ad6-83d4-d99763f65e49"), new Guid("6bf5f935-efe4-4672-acca-65141d6eab42"), "This is description for product15", "Product15" }
                });
        }
    }
}
