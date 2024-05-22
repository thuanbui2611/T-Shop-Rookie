﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using T_Shop.Infrastructure.Persistence;

#nullable disable

namespace T_Shop.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("table_roleclaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("table_userclaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("table_userlogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("table_userroles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("table_usertokens", (string)null);
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.Brand", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("table_brand", (string)null);
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_user_id");

                    b.HasKey("Id");

                    b.ToTable("table_cart", (string)null);
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.CartItem", b =>
                {
                    b.Property<Guid>("CartID")
                        .HasColumnType("uuid")
                        .HasColumnName("PK_FK_cart_id");

                    b.Property<Guid>("ProductID")
                        .HasColumnType("uuid")
                        .HasColumnName("PK_FK_product_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("CartID", "ProductID");

                    b.HasIndex("ProductID");

                    b.ToTable("table_cart_items", (string)null);
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.Color", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("HexColor")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("table_color", (string)null);
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.Model", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<Guid>("BrandID")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_brand_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BrandID");

                    b.ToTable("table_model", (string)null);
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("ClientSecret")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PaymentIntentID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShippingAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_user_id");

                    b.HasKey("Id");

                    b.ToTable("table_order", (string)null);
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.OrderDetail", b =>
                {
                    b.Property<Guid>("OrderID")
                        .HasColumnType("uuid")
                        .HasColumnName("PK_FK_order_id");

                    b.Property<Guid>("ProductID")
                        .HasColumnType("uuid")
                        .HasColumnName("PK_FK_product_id");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("OrderID", "ProductID");

                    b.HasIndex("ProductID");

                    b.ToTable("table_order_detail", (string)null);
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<Guid>("ColorID")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_color_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsOnStock")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ModelID")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_model_id");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<Guid>("TypeID")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_type_id");

                    b.Property<string>("Variant")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ModelID");

                    b.HasIndex("TypeID");

                    b.HasIndex("ColorID", "ModelID", "Variant")
                        .IsUnique();

                    b.ToTable("table_product", (string)null);
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.ProductImage", b =>
                {
                    b.Property<Guid>("ImageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ImagePublicID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsMain")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ProductID")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_product_id");

                    b.HasKey("ImageID");

                    b.HasIndex("ProductID");

                    b.ToTable("table_product_image", (string)null);
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.ProductReview", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ProductID")
                        .HasColumnType("uuid");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TransactionID")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProductID");

                    b.HasIndex("TransactionID")
                        .IsUnique();

                    b.ToTable("table_product_review", (string)null);
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.ProductReviewImage", b =>
                {
                    b.Property<Guid>("ImageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ImagePublicID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ProductReviewID")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_product_review_id");

                    b.HasKey("ImageID");

                    b.HasIndex("ProductReviewID");

                    b.ToTable("table_product_review_image", (string)null);
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CustomerID")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_customer_id");

                    b.Property<Guid>("OrderID")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_order_id");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrderID")
                        .IsUnique();

                    b.ToTable("table_transaction", (string)null);
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.TypeProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("table_type", (string)null);
                });

            modelBuilder.Entity("T_Shop.Infrastructure.Persistence.IdentityModels.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("table_roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("55ea57a5-5eb0-446a-bd16-54de57738815"),
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = new Guid("d2145b7c-6d76-4eb9-9e6c-ab101b754c72"),
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("T_Shop.Infrastructure.Persistence.IdentityModels.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("Avatar")
                        .HasColumnType("text")
                        .HasColumnName("avatar");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FullName")
                        .HasColumnType("text")
                        .HasColumnName("full_name");

                    b.Property<string>("Gender")
                        .HasColumnType("text")
                        .HasColumnName("gender");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("boolean")
                        .HasColumnName("is_locked");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("table_users", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("T_Shop.Infrastructure.Persistence.IdentityModels.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("T_Shop.Infrastructure.Persistence.IdentityModels.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("T_Shop.Infrastructure.Persistence.IdentityModels.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("T_Shop.Infrastructure.Persistence.IdentityModels.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("T_Shop.Infrastructure.Persistence.IdentityModels.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("T_Shop.Infrastructure.Persistence.IdentityModels.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.CartItem", b =>
                {
                    b.HasOne("T_Shop.Domain.Entity.Cart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("T_Shop.Domain.Entity.Product", "Product")
                        .WithMany("CartProducts")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.Model", b =>
                {
                    b.HasOne("T_Shop.Domain.Entity.Brand", "Brand")
                        .WithMany("Models")
                        .HasForeignKey("BrandID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.OrderDetail", b =>
                {
                    b.HasOne("T_Shop.Domain.Entity.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("T_Shop.Domain.Entity.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.Product", b =>
                {
                    b.HasOne("T_Shop.Domain.Entity.Color", "Color")
                        .WithMany("Products")
                        .HasForeignKey("ColorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("T_Shop.Domain.Entity.Model", "Model")
                        .WithMany("Products")
                        .HasForeignKey("ModelID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("T_Shop.Domain.Entity.TypeProduct", "Type")
                        .WithMany("Products")
                        .HasForeignKey("TypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Color");

                    b.Navigation("Model");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.ProductImage", b =>
                {
                    b.HasOne("T_Shop.Domain.Entity.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.ProductReview", b =>
                {
                    b.HasOne("T_Shop.Domain.Entity.Product", "Product")
                        .WithMany("ProductReviews")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("T_Shop.Domain.Entity.Transaction", "Transaction")
                        .WithOne("ProductReview")
                        .HasForeignKey("T_Shop.Domain.Entity.ProductReview", "TransactionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.ProductReviewImage", b =>
                {
                    b.HasOne("T_Shop.Domain.Entity.ProductReview", "ProductReview")
                        .WithMany("ProductReviewImages")
                        .HasForeignKey("ProductReviewID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductReview");
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.Transaction", b =>
                {
                    b.HasOne("T_Shop.Domain.Entity.Order", "Order")
                        .WithOne("Transaction")
                        .HasForeignKey("T_Shop.Domain.Entity.Transaction", "OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.Brand", b =>
                {
                    b.Navigation("Models");
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.Cart", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.Color", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.Model", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.Order", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("Transaction")
                        .IsRequired();
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.Product", b =>
                {
                    b.Navigation("CartProducts");

                    b.Navigation("OrderDetails");

                    b.Navigation("ProductImages");

                    b.Navigation("ProductReviews");
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.ProductReview", b =>
                {
                    b.Navigation("ProductReviewImages");
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.Transaction", b =>
                {
                    b.Navigation("ProductReview")
                        .IsRequired();
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.TypeProduct", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
