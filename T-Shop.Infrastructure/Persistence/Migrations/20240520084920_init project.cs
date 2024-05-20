using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace T_Shop.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initproject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "table_brand",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "table_cart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_cart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "table_color",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    HexColor = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_color", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "table_order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ShippingAddress = table.Column<string>(type: "text", nullable: false),
                    PaymentIntentID = table.Column<string>(type: "text", nullable: false),
                    ClientSecret = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_order", x => x.Id);
                });

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
                name: "table_type",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "table_users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "text", nullable: true),
                    date_of_birth = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    gender = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    avatar = table.Column<string>(type: "text", nullable: true),
                    is_locked = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
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
                name: "table_model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    FK_brand_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_model", x => x.Id);
                    table.ForeignKey(
                        name: "FK_table_model_table_brand_FK_brand_id",
                        column: x => x.FK_brand_id,
                        principalTable: "table_brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "table_transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_table_transaction_table_order_FK_order_id",
                        column: x => x.FK_order_id,
                        principalTable: "table_order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "table_product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_model_id = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_color_id = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Variant = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsOnStock = table.Column<bool>(type: "boolean", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_table_product_table_color_FK_color_id",
                        column: x => x.FK_color_id,
                        principalTable: "table_color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_table_product_table_model_FK_model_id",
                        column: x => x.FK_model_id,
                        principalTable: "table_model",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_table_product_table_type_FK_type_id",
                        column: x => x.FK_type_id,
                        principalTable: "table_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "table_cart_items",
                columns: table => new
                {
                    PK_FK_cart_id = table.Column<Guid>(type: "uuid", nullable: false),
                    PK_FK_product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_cart_items", x => new { x.PK_FK_cart_id, x.PK_FK_product_id });
                    table.ForeignKey(
                        name: "FK_table_cart_items_table_cart_PK_FK_cart_id",
                        column: x => x.PK_FK_cart_id,
                        principalTable: "table_cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_table_cart_items_table_product_PK_FK_product_id",
                        column: x => x.PK_FK_product_id,
                        principalTable: "table_product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "table_order_detail",
                columns: table => new
                {
                    PK_FK_order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    PK_FK_product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_order_detail", x => new { x.PK_FK_order_id, x.PK_FK_product_id });
                    table.ForeignKey(
                        name: "FK_table_order_detail_table_order_PK_FK_order_id",
                        column: x => x.PK_FK_order_id,
                        principalTable: "table_order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_table_order_detail_table_product_PK_FK_product_id",
                        column: x => x.PK_FK_product_id,
                        principalTable: "table_product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "table_product_image",
                columns: table => new
                {
                    ImageID = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    IsMain = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_product_image", x => new { x.FK_product_id, x.ImageID });
                    table.ForeignKey(
                        name: "FK_table_product_image_table_product_FK_product_id",
                        column: x => x.FK_product_id,
                        principalTable: "table_product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "table_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3eb7521c-235f-4265-ad65-8d9d18ea86e4"), null, "Admin", "ADMIN" },
                    { new Guid("4f74d942-2b79-4510-8248-bc75bd5f0a89"), null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_table_cart_items_PK_FK_product_id",
                table: "table_cart_items",
                column: "PK_FK_product_id");

            migrationBuilder.CreateIndex(
                name: "IX_table_model_FK_brand_id",
                table: "table_model",
                column: "FK_brand_id");

            migrationBuilder.CreateIndex(
                name: "IX_table_order_detail_PK_FK_product_id",
                table: "table_order_detail",
                column: "PK_FK_product_id");

            migrationBuilder.CreateIndex(
                name: "IX_table_product_FK_color_id_FK_model_id_Variant",
                table: "table_product",
                columns: new[] { "FK_color_id", "FK_model_id", "Variant" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_table_product_FK_model_id",
                table: "table_product",
                column: "FK_model_id");

            migrationBuilder.CreateIndex(
                name: "IX_table_product_FK_type_id",
                table: "table_product",
                column: "FK_type_id");

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
                name: "IX_table_transaction_FK_order_id",
                table: "table_transaction",
                column: "FK_order_id",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "table_cart_items");

            migrationBuilder.DropTable(
                name: "table_order_detail");

            migrationBuilder.DropTable(
                name: "table_product_image");

            migrationBuilder.DropTable(
                name: "table_roleclaims");

            migrationBuilder.DropTable(
                name: "table_transaction");

            migrationBuilder.DropTable(
                name: "table_userclaims");

            migrationBuilder.DropTable(
                name: "table_userlogins");

            migrationBuilder.DropTable(
                name: "table_userroles");

            migrationBuilder.DropTable(
                name: "table_usertokens");

            migrationBuilder.DropTable(
                name: "table_cart");

            migrationBuilder.DropTable(
                name: "table_product");

            migrationBuilder.DropTable(
                name: "table_order");

            migrationBuilder.DropTable(
                name: "table_roles");

            migrationBuilder.DropTable(
                name: "table_users");

            migrationBuilder.DropTable(
                name: "table_color");

            migrationBuilder.DropTable(
                name: "table_model");

            migrationBuilder.DropTable(
                name: "table_type");

            migrationBuilder.DropTable(
                name: "table_brand");
        }
    }
}
