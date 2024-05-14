﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using T_Shop.Infrastructure.Persistence;

#nullable disable

namespace T_Shop.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240510091548_init identity")]
    partial class initidentity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
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

            modelBuilder.Entity("T_Shop.Domain.Entity.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("table_category", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("0c2335a2-198e-4249-ab58-0b2efe786eca"),
                            Name = "Category 1"
                        },
                        new
                        {
                            Id = new Guid("118c0104-7a65-4d62-b84b-541f7bb7c7fb"),
                            Name = "Category 2"
                        },
                        new
                        {
                            Id = new Guid("690710bb-8ce9-4ef0-9a29-f4bf6b7ac49f"),
                            Name = "Category 3"
                        },
                        new
                        {
                            Id = new Guid("578067c9-b98c-4d28-84f4-d56eca2977eb"),
                            Name = "Category 4"
                        },
                        new
                        {
                            Id = new Guid("c488f17d-4ae4-4d9c-a185-133011b797ac"),
                            Name = "Category 5"
                        },
                        new
                        {
                            Id = new Guid("76f175f0-ad3d-4a1e-9d70-048759c700a1"),
                            Name = "Category 6"
                        },
                        new
                        {
                            Id = new Guid("90bf0c0d-28a8-4a69-b945-a19ba3d025c0"),
                            Name = "Category 7"
                        },
                        new
                        {
                            Id = new Guid("eb7ddb8a-15d4-410f-8aea-b92c8fa43ff3"),
                            Name = "Category 8"
                        },
                        new
                        {
                            Id = new Guid("30a6466d-3072-421d-944c-e5804068f2cf"),
                            Name = "Category 9"
                        },
                        new
                        {
                            Id = new Guid("5f36e0cb-4e0b-407e-96f1-7ba4fd5eaa2d"),
                            Name = "Category 10"
                        });
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_category_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("table_product", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("c9a48986-598b-4972-88f4-8ebc680791b1"),
                            CategoryId = new Guid("578067c9-b98c-4d28-84f4-d56eca2977eb"),
                            Description = "This is description for product 1",
                            Name = "Product 1"
                        },
                        new
                        {
                            Id = new Guid("d97b36c1-a911-4e47-a51f-0946d8e2fa87"),
                            CategoryId = new Guid("76f175f0-ad3d-4a1e-9d70-048759c700a1"),
                            Description = "This is description for product 2",
                            Name = "Product 2"
                        },
                        new
                        {
                            Id = new Guid("b89555f8-3318-4f16-b573-f6512988382b"),
                            CategoryId = new Guid("578067c9-b98c-4d28-84f4-d56eca2977eb"),
                            Description = "This is description for product 3",
                            Name = "Product 3"
                        },
                        new
                        {
                            Id = new Guid("9191cc77-6570-4f06-b610-0b58cb98f0a6"),
                            CategoryId = new Guid("eb7ddb8a-15d4-410f-8aea-b92c8fa43ff3"),
                            Description = "This is description for product 4",
                            Name = "Product 4"
                        },
                        new
                        {
                            Id = new Guid("46cc6553-b23d-4600-aa38-1c6cbc5a1fab"),
                            CategoryId = new Guid("578067c9-b98c-4d28-84f4-d56eca2977eb"),
                            Description = "This is description for product 5",
                            Name = "Product 5"
                        },
                        new
                        {
                            Id = new Guid("564298ef-303d-469e-8ca8-0ef46993843b"),
                            CategoryId = new Guid("578067c9-b98c-4d28-84f4-d56eca2977eb"),
                            Description = "This is description for product 6",
                            Name = "Product 6"
                        },
                        new
                        {
                            Id = new Guid("3d9a656f-d3dc-481f-83b6-7f3a90305335"),
                            CategoryId = new Guid("76f175f0-ad3d-4a1e-9d70-048759c700a1"),
                            Description = "This is description for product 7",
                            Name = "Product 7"
                        },
                        new
                        {
                            Id = new Guid("1c4146c8-0c25-4545-80ab-9de95abe48d2"),
                            CategoryId = new Guid("690710bb-8ce9-4ef0-9a29-f4bf6b7ac49f"),
                            Description = "This is description for product 8",
                            Name = "Product 8"
                        },
                        new
                        {
                            Id = new Guid("64669b2c-8420-4972-b6e3-dab1861abca0"),
                            CategoryId = new Guid("eb7ddb8a-15d4-410f-8aea-b92c8fa43ff3"),
                            Description = "This is description for product 9",
                            Name = "Product 9"
                        },
                        new
                        {
                            Id = new Guid("6cdc3ebc-026b-4e45-b9e8-436f0d911caa"),
                            CategoryId = new Guid("30a6466d-3072-421d-944c-e5804068f2cf"),
                            Description = "This is description for product 10",
                            Name = "Product 10"
                        },
                        new
                        {
                            Id = new Guid("534fd1b9-bd12-4b01-86e6-2e0c0942f0df"),
                            CategoryId = new Guid("5f36e0cb-4e0b-407e-96f1-7ba4fd5eaa2d"),
                            Description = "This is description for product 11",
                            Name = "Product 11"
                        },
                        new
                        {
                            Id = new Guid("a93fdd17-647c-447b-b364-420a586d904c"),
                            CategoryId = new Guid("690710bb-8ce9-4ef0-9a29-f4bf6b7ac49f"),
                            Description = "This is description for product 12",
                            Name = "Product 12"
                        },
                        new
                        {
                            Id = new Guid("82ab0d01-12ad-47b1-8849-ac6060dbb31f"),
                            CategoryId = new Guid("118c0104-7a65-4d62-b84b-541f7bb7c7fb"),
                            Description = "This is description for product 13",
                            Name = "Product 13"
                        },
                        new
                        {
                            Id = new Guid("8bff6e57-b499-4845-a01d-64cb80c620af"),
                            CategoryId = new Guid("76f175f0-ad3d-4a1e-9d70-048759c700a1"),
                            Description = "This is description for product 14",
                            Name = "Product 14"
                        },
                        new
                        {
                            Id = new Guid("c0e8fc20-af48-41fb-b67c-3557196b13fe"),
                            CategoryId = new Guid("5f36e0cb-4e0b-407e-96f1-7ba4fd5eaa2d"),
                            Description = "This is description for product 15",
                            Name = "Product 15"
                        },
                        new
                        {
                            Id = new Guid("c7a01852-be4c-44de-a730-6a61c209cb29"),
                            CategoryId = new Guid("90bf0c0d-28a8-4a69-b945-a19ba3d025c0"),
                            Description = "This is description for product 16",
                            Name = "Product 16"
                        },
                        new
                        {
                            Id = new Guid("9378fc40-30e8-40cf-a29c-df69e2d7fba2"),
                            CategoryId = new Guid("5f36e0cb-4e0b-407e-96f1-7ba4fd5eaa2d"),
                            Description = "This is description for product 17",
                            Name = "Product 17"
                        },
                        new
                        {
                            Id = new Guid("f5e79bb7-8920-4ee2-bc4a-70b9d097bdad"),
                            CategoryId = new Guid("30a6466d-3072-421d-944c-e5804068f2cf"),
                            Description = "This is description for product 18",
                            Name = "Product 18"
                        },
                        new
                        {
                            Id = new Guid("cbe21e9e-b852-44d6-b217-184edd29f15b"),
                            CategoryId = new Guid("118c0104-7a65-4d62-b84b-541f7bb7c7fb"),
                            Description = "This is description for product 19",
                            Name = "Product 19"
                        },
                        new
                        {
                            Id = new Guid("b9bc1e84-fdcf-471b-bcc4-769231d2cd02"),
                            CategoryId = new Guid("118c0104-7a65-4d62-b84b-541f7bb7c7fb"),
                            Description = "This is description for product 20",
                            Name = "Product 20"
                        },
                        new
                        {
                            Id = new Guid("c955add9-8e18-46a3-a40e-8150d5870454"),
                            CategoryId = new Guid("118c0104-7a65-4d62-b84b-541f7bb7c7fb"),
                            Description = "This is description for product 21",
                            Name = "Product 21"
                        },
                        new
                        {
                            Id = new Guid("275b5648-a15e-4bc3-9ebd-bc6c0576e19d"),
                            CategoryId = new Guid("690710bb-8ce9-4ef0-9a29-f4bf6b7ac49f"),
                            Description = "This is description for product 22",
                            Name = "Product 22"
                        },
                        new
                        {
                            Id = new Guid("23e54217-a00c-4881-88db-7a9550d8bc6c"),
                            CategoryId = new Guid("690710bb-8ce9-4ef0-9a29-f4bf6b7ac49f"),
                            Description = "This is description for product 23",
                            Name = "Product 23"
                        },
                        new
                        {
                            Id = new Guid("5d75ffad-6a03-470f-b6a3-935cd0e00840"),
                            CategoryId = new Guid("578067c9-b98c-4d28-84f4-d56eca2977eb"),
                            Description = "This is description for product 24",
                            Name = "Product 24"
                        },
                        new
                        {
                            Id = new Guid("d39f3bee-1e4f-4286-94a3-7ccead8eb954"),
                            CategoryId = new Guid("76f175f0-ad3d-4a1e-9d70-048759c700a1"),
                            Description = "This is description for product 25",
                            Name = "Product 25"
                        },
                        new
                        {
                            Id = new Guid("4209b9ff-5eeb-46d3-9486-1d13953c9958"),
                            CategoryId = new Guid("30a6466d-3072-421d-944c-e5804068f2cf"),
                            Description = "This is description for product 26",
                            Name = "Product 26"
                        },
                        new
                        {
                            Id = new Guid("b8604078-e7c7-4de2-9bb7-f66b33bc03bf"),
                            CategoryId = new Guid("c488f17d-4ae4-4d9c-a185-133011b797ac"),
                            Description = "This is description for product 27",
                            Name = "Product 27"
                        },
                        new
                        {
                            Id = new Guid("6e366946-7ae2-443d-bde3-4193c1d8d165"),
                            CategoryId = new Guid("76f175f0-ad3d-4a1e-9d70-048759c700a1"),
                            Description = "This is description for product 28",
                            Name = "Product 28"
                        },
                        new
                        {
                            Id = new Guid("6c754343-8261-4fd6-aed5-7468b30efa00"),
                            CategoryId = new Guid("578067c9-b98c-4d28-84f4-d56eca2977eb"),
                            Description = "This is description for product 29",
                            Name = "Product 29"
                        },
                        new
                        {
                            Id = new Guid("ffc4676b-b8a2-4f97-babd-9b8bc27e528e"),
                            CategoryId = new Guid("90bf0c0d-28a8-4a69-b945-a19ba3d025c0"),
                            Description = "This is description for product 30",
                            Name = "Product 30"
                        },
                        new
                        {
                            Id = new Guid("ec771ebc-76c1-4ae6-b0c3-1a35b7835165"),
                            CategoryId = new Guid("eb7ddb8a-15d4-410f-8aea-b92c8fa43ff3"),
                            Description = "This is description for product 31",
                            Name = "Product 31"
                        },
                        new
                        {
                            Id = new Guid("f9db7740-cfe5-4bc3-baab-fc8478a190ce"),
                            CategoryId = new Guid("c488f17d-4ae4-4d9c-a185-133011b797ac"),
                            Description = "This is description for product 32",
                            Name = "Product 32"
                        },
                        new
                        {
                            Id = new Guid("2495dbb3-c009-4804-9e75-cb9dbaaafbd0"),
                            CategoryId = new Guid("30a6466d-3072-421d-944c-e5804068f2cf"),
                            Description = "This is description for product 33",
                            Name = "Product 33"
                        },
                        new
                        {
                            Id = new Guid("1494bf36-d8d1-4cdd-9e57-1c0dde5ee08b"),
                            CategoryId = new Guid("5f36e0cb-4e0b-407e-96f1-7ba4fd5eaa2d"),
                            Description = "This is description for product 34",
                            Name = "Product 34"
                        },
                        new
                        {
                            Id = new Guid("30e0d42c-ba6f-4ce7-bb04-bb0545f7908f"),
                            CategoryId = new Guid("690710bb-8ce9-4ef0-9a29-f4bf6b7ac49f"),
                            Description = "This is description for product 35",
                            Name = "Product 35"
                        },
                        new
                        {
                            Id = new Guid("40687eb4-6d3c-4199-80cb-5fcb048115a7"),
                            CategoryId = new Guid("eb7ddb8a-15d4-410f-8aea-b92c8fa43ff3"),
                            Description = "This is description for product 36",
                            Name = "Product 36"
                        },
                        new
                        {
                            Id = new Guid("2d35ccc2-8c9b-4f11-a334-437cc81b4898"),
                            CategoryId = new Guid("76f175f0-ad3d-4a1e-9d70-048759c700a1"),
                            Description = "This is description for product 37",
                            Name = "Product 37"
                        },
                        new
                        {
                            Id = new Guid("6d81efc9-c6d0-49a2-bfde-3464dda86fa4"),
                            CategoryId = new Guid("30a6466d-3072-421d-944c-e5804068f2cf"),
                            Description = "This is description for product 38",
                            Name = "Product 38"
                        },
                        new
                        {
                            Id = new Guid("36e51683-f681-430d-adf1-c664f45dc90c"),
                            CategoryId = new Guid("eb7ddb8a-15d4-410f-8aea-b92c8fa43ff3"),
                            Description = "This is description for product 39",
                            Name = "Product 39"
                        },
                        new
                        {
                            Id = new Guid("24d788f1-210c-408b-9628-2c0ea64f3a35"),
                            CategoryId = new Guid("eb7ddb8a-15d4-410f-8aea-b92c8fa43ff3"),
                            Description = "This is description for product 40",
                            Name = "Product 40"
                        },
                        new
                        {
                            Id = new Guid("ebab8e76-507b-4931-a296-6c29dd696fd8"),
                            CategoryId = new Guid("5f36e0cb-4e0b-407e-96f1-7ba4fd5eaa2d"),
                            Description = "This is description for product 41",
                            Name = "Product 41"
                        },
                        new
                        {
                            Id = new Guid("16b5f375-e019-4dac-b2cb-c0a00799f4e7"),
                            CategoryId = new Guid("30a6466d-3072-421d-944c-e5804068f2cf"),
                            Description = "This is description for product 42",
                            Name = "Product 42"
                        },
                        new
                        {
                            Id = new Guid("86d1f6b2-2100-491b-b972-6275a4d150a3"),
                            CategoryId = new Guid("118c0104-7a65-4d62-b84b-541f7bb7c7fb"),
                            Description = "This is description for product 43",
                            Name = "Product 43"
                        },
                        new
                        {
                            Id = new Guid("0b1c2b57-a28a-4b2d-85a9-0200360ea508"),
                            CategoryId = new Guid("5f36e0cb-4e0b-407e-96f1-7ba4fd5eaa2d"),
                            Description = "This is description for product 44",
                            Name = "Product 44"
                        },
                        new
                        {
                            Id = new Guid("015a2ac8-e680-4b84-b81c-c888aa68b2f6"),
                            CategoryId = new Guid("5f36e0cb-4e0b-407e-96f1-7ba4fd5eaa2d"),
                            Description = "This is description for product 45",
                            Name = "Product 45"
                        },
                        new
                        {
                            Id = new Guid("c6f69d7b-efd7-4406-83f0-19d7493c8dcc"),
                            CategoryId = new Guid("90bf0c0d-28a8-4a69-b945-a19ba3d025c0"),
                            Description = "This is description for product 46",
                            Name = "Product 46"
                        },
                        new
                        {
                            Id = new Guid("90a205cd-cb03-4a29-94b5-4162ba929390"),
                            CategoryId = new Guid("90bf0c0d-28a8-4a69-b945-a19ba3d025c0"),
                            Description = "This is description for product 47",
                            Name = "Product 47"
                        },
                        new
                        {
                            Id = new Guid("96603428-d2c8-4c86-bd4c-d04f895e846e"),
                            CategoryId = new Guid("690710bb-8ce9-4ef0-9a29-f4bf6b7ac49f"),
                            Description = "This is description for product 48",
                            Name = "Product 48"
                        },
                        new
                        {
                            Id = new Guid("d551fa60-f25e-4ae4-a640-5cfded9513f4"),
                            CategoryId = new Guid("5f36e0cb-4e0b-407e-96f1-7ba4fd5eaa2d"),
                            Description = "This is description for product 49",
                            Name = "Product 49"
                        },
                        new
                        {
                            Id = new Guid("6071d6af-55a2-4f71-ad3f-9ff7784058e0"),
                            CategoryId = new Guid("5f36e0cb-4e0b-407e-96f1-7ba4fd5eaa2d"),
                            Description = "This is description for product 50",
                            Name = "Product 50"
                        });
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
                });

            modelBuilder.Entity("T_Shop.Infrastructure.Persistence.IdentityModels.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("Avatar")
                        .HasColumnType("text")
                        .HasColumnName("avatar");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FullName")
                        .IsRequired()
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

            modelBuilder.Entity("T_Shop.Domain.Entity.Product", b =>
                {
                    b.HasOne("T_Shop.Domain.Entity.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("T_Shop.Domain.Entity.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}