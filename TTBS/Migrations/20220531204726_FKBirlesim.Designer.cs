﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TTBS.Infrastructure;

#nullable disable

namespace TTBS.Migrations
{
    [DbContext(typeof(TTBSContext))]
    [Migration("20220531204726_FKBirlesim")]
    partial class FKBirlesim
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TTBS.Core.Entities.AltKomisyon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Kodu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("KomisyonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("KomisyonId");

                    b.ToTable("AltKomisyon", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.Birlesim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BaslangicTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("BirlesimNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BitisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("YasamaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("YasamaId");

                    b.ToTable("Birlesim");
                });

            modelBuilder.Entity("TTBS.Core.Entities.ClaimEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Claims");
                });

            modelBuilder.Entity("TTBS.Core.Entities.Donem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BaslangicTarihi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("BitisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DonemAd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DonemKod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DonemSecTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("EskiAd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("KısaAd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MeclisKod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MevcutUye")
                        .HasColumnType("int");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UyeTamsayi")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Donem", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.Grup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Grup", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.Komisyon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Kodu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KomisyonTipi")
                        .HasColumnType("int");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Tarih")
                        .HasColumnType("datetime2");

                    b.Property<string>("Yeri")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Komisyon");
                });

            modelBuilder.Entity("TTBS.Core.Entities.RoleClaimEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClaimId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ClaimId");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("TTBS.Core.Entities.RoleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleStatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RoleEntity");
                });

            modelBuilder.Entity("TTBS.Core.Entities.StenoGorev", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GorevDakika")
                        .HasColumnType("int");

                    b.Property<int>("GorevSaniye")
                        .HasColumnType("int");

                    b.Property<int>("GorevStatu")
                        .HasColumnType("int");

                    b.Property<DateTime?>("GörevTarihi")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("StenoPlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StenografId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StenoPlanId");

                    b.HasIndex("StenografId");

                    b.ToTable("StenoGorev", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.Stenograf", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AdSoyad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SiraNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StenoGorevTuru")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Stenograf", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.StenoGrup", b =>
                {
                    b.Property<Guid>("StenoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GrupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BirlesimId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("StenoId", "GrupId", "BirlesimId");

                    b.HasIndex("BirlesimId");

                    b.HasIndex("GrupId");

                    b.ToTable("StenoGrup", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.StenoIzin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BaslangicTarihi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("BitisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("IzinTuru")
                        .HasColumnType("int");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("StenografId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StenografId");

                    b.ToTable("StenoIzin", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.StenoPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BaslangicTarihi")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("BirlesimId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BitisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GorevAd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GorevStatu")
                        .HasColumnType("int");

                    b.Property<int>("GorevTuru")
                        .HasColumnType("int");

                    b.Property<string>("GorevYeri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("KomisyonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StenoSayisi")
                        .HasColumnType("int");

                    b.Property<int>("StenoSure")
                        .HasColumnType("int");

                    b.Property<int>("UzmanStenoSayisi")
                        .HasColumnType("int");

                    b.Property<int>("UzmanStenoSure")
                        .HasColumnType("int");

                    b.Property<Guid?>("YasamaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BirlesimId");

                    b.HasIndex("KomisyonId");

                    b.HasIndex("YasamaId");

                    b.ToTable("StenoPlan", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TTBS.Core.Entities.UserRoleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoleEntity");
                });

            modelBuilder.Entity("TTBS.Core.Entities.Yasama", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BaslangicTarihi")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("BitisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DonemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("YasamaYili")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DonemId");

                    b.ToTable("Yasama", (string)null);
                });

            modelBuilder.Entity("TTBS.Core.Entities.AltKomisyon", b =>
                {
                    b.HasOne("TTBS.Core.Entities.Komisyon", "Komisyon")
                        .WithMany("AltKomisyons")
                        .HasForeignKey("KomisyonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Komisyon");
                });

            modelBuilder.Entity("TTBS.Core.Entities.Birlesim", b =>
                {
                    b.HasOne("TTBS.Core.Entities.Yasama", "Yasaama")
                        .WithMany("Birlesims")
                        .HasForeignKey("YasamaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Yasaama");
                });

            modelBuilder.Entity("TTBS.Core.Entities.RoleClaimEntity", b =>
                {
                    b.HasOne("TTBS.Core.Entities.ClaimEntity", "Claim")
                        .WithMany("RoleClaims")
                        .HasForeignKey("ClaimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TTBS.Core.Entities.RoleEntity", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Claim");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TTBS.Core.Entities.StenoGorev", b =>
                {
                    b.HasOne("TTBS.Core.Entities.StenoPlan", "StenoPlan")
                        .WithMany("StenoGorevs")
                        .HasForeignKey("StenoPlanId");

                    b.HasOne("TTBS.Core.Entities.Stenograf", "Stenograf")
                        .WithMany("StenoGorevs")
                        .HasForeignKey("StenografId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StenoPlan");

                    b.Navigation("Stenograf");
                });

            modelBuilder.Entity("TTBS.Core.Entities.StenoGrup", b =>
                {
                    b.HasOne("TTBS.Core.Entities.Birlesim", "Birlesim")
                        .WithMany("StenoGrups")
                        .HasForeignKey("BirlesimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TTBS.Core.Entities.Grup", "Grup")
                        .WithMany("StenoGrups")
                        .HasForeignKey("GrupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TTBS.Core.Entities.Stenograf", "Stenograf")
                        .WithMany("StenoGrups")
                        .HasForeignKey("StenoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Birlesim");

                    b.Navigation("Grup");

                    b.Navigation("Stenograf");
                });

            modelBuilder.Entity("TTBS.Core.Entities.StenoIzin", b =>
                {
                    b.HasOne("TTBS.Core.Entities.Stenograf", "Stenograf")
                        .WithMany()
                        .HasForeignKey("StenografId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stenograf");
                });

            modelBuilder.Entity("TTBS.Core.Entities.StenoPlan", b =>
                {
                    b.HasOne("TTBS.Core.Entities.Birlesim", "Birlesim")
                        .WithMany("StenoPlans")
                        .HasForeignKey("BirlesimId");

                    b.HasOne("TTBS.Core.Entities.Komisyon", "Komisyon")
                        .WithMany("StenoPlans")
                        .HasForeignKey("KomisyonId");

                    b.HasOne("TTBS.Core.Entities.Yasama", null)
                        .WithMany("StenoPlans")
                        .HasForeignKey("YasamaId");

                    b.Navigation("Birlesim");

                    b.Navigation("Komisyon");
                });

            modelBuilder.Entity("TTBS.Core.Entities.UserRoleEntity", b =>
                {
                    b.HasOne("TTBS.Core.Entities.RoleEntity", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TTBS.Core.Entities.UserEntity", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TTBS.Core.Entities.Yasama", b =>
                {
                    b.HasOne("TTBS.Core.Entities.Donem", "Donem")
                        .WithMany("Yasamas")
                        .HasForeignKey("DonemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Donem");
                });

            modelBuilder.Entity("TTBS.Core.Entities.Birlesim", b =>
                {
                    b.Navigation("StenoGrups");

                    b.Navigation("StenoPlans");
                });

            modelBuilder.Entity("TTBS.Core.Entities.ClaimEntity", b =>
                {
                    b.Navigation("RoleClaims");
                });

            modelBuilder.Entity("TTBS.Core.Entities.Donem", b =>
                {
                    b.Navigation("Yasamas");
                });

            modelBuilder.Entity("TTBS.Core.Entities.Grup", b =>
                {
                    b.Navigation("StenoGrups");
                });

            modelBuilder.Entity("TTBS.Core.Entities.Komisyon", b =>
                {
                    b.Navigation("AltKomisyons");

                    b.Navigation("StenoPlans");
                });

            modelBuilder.Entity("TTBS.Core.Entities.RoleEntity", b =>
                {
                    b.Navigation("RoleClaims");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("TTBS.Core.Entities.Stenograf", b =>
                {
                    b.Navigation("StenoGorevs");

                    b.Navigation("StenoGrups");
                });

            modelBuilder.Entity("TTBS.Core.Entities.StenoPlan", b =>
                {
                    b.Navigation("StenoGorevs");
                });

            modelBuilder.Entity("TTBS.Core.Entities.UserEntity", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("TTBS.Core.Entities.Yasama", b =>
                {
                    b.Navigation("Birlesims");

                    b.Navigation("StenoPlans");
                });
#pragma warning restore 612, 618
        }
    }
}
