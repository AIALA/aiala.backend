﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using aiala.Backend.Data;

namespace aiala.Backend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190201153949_UserManagement")]
    partial class UserManagement
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("aiala")
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("aiala.Backend.Data.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ActiveAccountId");

                    b.Property<string>("Email");

                    b.Property<string>("ExternalUserId");

                    b.Property<string>("Firstname");

                    b.Property<string>("Lastname");

                    b.HasKey("Id");

                    b.ToTable("Users","directory");
                });

            modelBuilder.Entity("aiala.Backend.Data.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Enabled");

                    b.Property<string>("Name");

                    b.Property<int>("TenantType");

                    b.HasKey("Id");

                    b.ToTable("Tenants","directory");
                });

            modelBuilder.Entity("aiala.Backend.Data.Member", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<bool>("Enabled");

                    b.Property<string>("Firstname");

                    b.Property<Guid?>("InvitationId");

                    b.Property<string>("Lastname");

                    b.Property<int>("RoleType");

                    b.Property<Guid>("TenantId");

                    b.Property<Guid?>("UserId");

                    b.Property<string>("WhatsAppNumber");

                    b.HasKey("Id");

                    b.HasIndex("InvitationId");

                    b.HasIndex("TenantId");

                    b.HasIndex("UserId");

                    b.ToTable("Accounts","directory");
                });

            modelBuilder.Entity("xappido.Directory.Domain.App<aiala.Backend.Data.Group>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Enabled");

                    b.Property<string>("Key");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Apps","directory");
                });

            modelBuilder.Entity("xappido.Directory.Domain.Invitation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("Accepted");

                    b.Property<string>("ConfirmationToken");

                    b.Property<DateTimeOffset>("Created");

                    b.Property<string>("Culture");

                    b.Property<DateTimeOffset?>("Declined");

                    b.Property<string>("Email");

                    b.Property<string>("Message");

                    b.Property<int>("Resent");

                    b.Property<DateTimeOffset>("ValidUntil");

                    b.HasKey("Id");

                    b.ToTable("Invitations","directory");
                });

            modelBuilder.Entity("xappido.Directory.Domain.PermissionAssignment<aiala.Backend.Data.Group, aiala.Backend.Data.Member, aiala.Backend.Data.AppUser>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AccountId");

                    b.Property<Guid?>("PermissionGroupId");

                    b.Property<string>("PermissionType");

                    b.Property<Guid?>("TenantId");

                    b.Property<DateTimeOffset?>("ValidFrom");

                    b.Property<DateTimeOffset?>("ValidTo");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("PermissionGroupId");

                    b.HasIndex("TenantId");

                    b.ToTable("PermissionAssignments","directory");
                });

            modelBuilder.Entity("xappido.Directory.Domain.PermissionGroup<aiala.Backend.Data.Group>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AppId");

                    b.Property<string>("Name");

                    b.Property<Guid?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("AppId");

                    b.HasIndex("TenantId");

                    b.ToTable("PermissionGroups","directory");
                });

            modelBuilder.Entity("xappido.Directory.Domain.PermissionGroupAssignment<aiala.Backend.Data.Group, aiala.Backend.Data.Member, aiala.Backend.Data.AppUser>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AccountId");

                    b.Property<Guid>("GroupId");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("GroupId");

                    b.ToTable("PermissionGroupAssignments","directory");
                });

            modelBuilder.Entity("xappido.Directory.Domain.Registration<aiala.Backend.Data.Group>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("ApprovalRequired");

                    b.Property<string>("ApprovalToken");

                    b.Property<DateTimeOffset?>("Approved");

                    b.Property<string>("ApprovedBy");

                    b.Property<DateTimeOffset?>("Completed");

                    b.Property<string>("ConfirmationToken")
                        .IsRequired();

                    b.Property<DateTimeOffset?>("Confirmed");

                    b.Property<DateTimeOffset>("Created");

                    b.Property<Guid?>("CreatedAccountId");

                    b.Property<Guid?>("CreatedSubscriptionId");

                    b.Property<string>("Culture");

                    b.Property<string>("Email");

                    b.Property<Guid?>("SubscriptionTypeId");

                    b.Property<Guid?>("TenantId");

                    b.Property<int>("TenantType");

                    b.Property<string>("UserId");

                    b.Property<string>("Values")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionTypeId");

                    b.ToTable("Registrations","directory");
                });

            modelBuilder.Entity("xappido.Directory.Domain.Subscription<aiala.Backend.Data.Group>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("Created");

                    b.Property<Guid?>("SubscriptionTypeId");

                    b.Property<Guid?>("TenantId");

                    b.Property<DateTimeOffset?>("ValidFrom");

                    b.Property<DateTimeOffset?>("ValidTo");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionTypeId");

                    b.HasIndex("TenantId");

                    b.ToTable("Subscriptions","directory");
                });

            modelBuilder.Entity("xappido.Directory.Domain.SubscriptionActivation<aiala.Backend.Data.Group>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActivationType");

                    b.Property<bool>("ApprovalRequired");

                    b.Property<string>("Approvers");

                    b.Property<Guid?>("DefaultTenantId");

                    b.Property<int>("DefaultTenantType");

                    b.Property<Guid?>("SubscriptionTypeId");

                    b.HasKey("Id");

                    b.HasIndex("DefaultTenantId");

                    b.HasIndex("SubscriptionTypeId");

                    b.ToTable("SubscriptionActivations","directory");
                });

            modelBuilder.Entity("xappido.Directory.Domain.SubscriptionActivationCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("Activated");

                    b.Property<string>("ActivationKey");

                    b.Property<Guid?>("SubscriptionActivationId");

                    b.Property<DateTimeOffset?>("ValidFrom");

                    b.Property<DateTimeOffset?>("ValidTo");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionActivationId");

                    b.ToTable("SubscriptionActivationCodes","directory");
                });

            modelBuilder.Entity("xappido.Directory.Domain.SubscriptionType<aiala.Backend.Data.Group>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AppId");

                    b.Property<string>("Description");

                    b.Property<bool>("Enabled");

                    b.Property<string>("Features");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("SubscriptionLengthInDays");

                    b.HasKey("Id");

                    b.HasIndex("AppId");

                    b.ToTable("SubscriptionTypes","directory");
                });

            modelBuilder.Entity("xappido.Directory.Settings.AccountSetting<aiala.Backend.Data.Member, aiala.Backend.Data.Group, aiala.Backend.Data.AppUser>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AccountId");

                    b.Property<string>("SettingTypeKey");

                    b.Property<string>("SettingTypeTypeId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("SettingTypeKey", "SettingTypeTypeId");

                    b.ToTable("AccountSettings","directory");
                });

            modelBuilder.Entity("xappido.Directory.Settings.TenantSetting<aiala.Backend.Data.Group>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SettingTypeKey");

                    b.Property<string>("SettingTypeTypeId");

                    b.Property<Guid?>("TenantId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.HasIndex("SettingTypeKey", "SettingTypeTypeId");

                    b.ToTable("TenantSettings","directory");
                });

            modelBuilder.Entity("xappido.Directory.Settings.UserSetting<aiala.Backend.Data.AppUser>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SettingTypeKey");

                    b.Property<string>("SettingTypeTypeId");

                    b.Property<Guid?>("UserId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("SettingTypeKey", "SettingTypeTypeId");

                    b.ToTable("UserSettings","directory");
                });

            modelBuilder.Entity("xappido.Settings.Models.SettingType", b =>
                {
                    b.Property<string>("Key");

                    b.Property<string>("TypeId");

                    b.Property<string>("Category");

                    b.Property<int>("DataType");

                    b.Property<string>("DefaultValue");

                    b.Property<int>("Order");

                    b.HasKey("Key", "TypeId");

                    b.ToTable("SettingTypes","directory");
                });

            modelBuilder.Entity("aiala.Backend.Data.Member", b =>
                {
                    b.HasOne("xappido.Directory.Domain.Invitation", "Invitation")
                        .WithMany()
                        .HasForeignKey("InvitationId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("aiala.Backend.Data.Group", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("aiala.Backend.Data.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("xappido.Directory.Domain.PermissionAssignment<aiala.Backend.Data.Group, aiala.Backend.Data.Member, aiala.Backend.Data.AppUser>", b =>
                {
                    b.HasOne("aiala.Backend.Data.Member", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.HasOne("xappido.Directory.Domain.PermissionGroup<aiala.Backend.Data.Group>", "PermissionGroup")
                        .WithMany()
                        .HasForeignKey("PermissionGroupId");

                    b.HasOne("aiala.Backend.Data.Group", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("xappido.Directory.Domain.PermissionGroup<aiala.Backend.Data.Group>", b =>
                {
                    b.HasOne("xappido.Directory.Domain.App<aiala.Backend.Data.Group>", "App")
                        .WithMany()
                        .HasForeignKey("AppId");

                    b.HasOne("aiala.Backend.Data.Group", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("xappido.Directory.Domain.PermissionGroupAssignment<aiala.Backend.Data.Group, aiala.Backend.Data.Member, aiala.Backend.Data.AppUser>", b =>
                {
                    b.HasOne("aiala.Backend.Data.Member", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("xappido.Directory.Domain.PermissionGroup<aiala.Backend.Data.Group>", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("xappido.Directory.Domain.Registration<aiala.Backend.Data.Group>", b =>
                {
                    b.HasOne("xappido.Directory.Domain.SubscriptionType<aiala.Backend.Data.Group>", "SubscriptionType")
                        .WithMany()
                        .HasForeignKey("SubscriptionTypeId");
                });

            modelBuilder.Entity("xappido.Directory.Domain.Subscription<aiala.Backend.Data.Group>", b =>
                {
                    b.HasOne("xappido.Directory.Domain.SubscriptionType<aiala.Backend.Data.Group>", "SubscriptionType")
                        .WithMany("Subscriptions")
                        .HasForeignKey("SubscriptionTypeId");

                    b.HasOne("aiala.Backend.Data.Group", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("xappido.Directory.Domain.SubscriptionActivation<aiala.Backend.Data.Group>", b =>
                {
                    b.HasOne("aiala.Backend.Data.Group", "DefaultTenant")
                        .WithMany()
                        .HasForeignKey("DefaultTenantId");

                    b.HasOne("xappido.Directory.Domain.SubscriptionType<aiala.Backend.Data.Group>", "SubscriptionType")
                        .WithMany("Activations")
                        .HasForeignKey("SubscriptionTypeId");
                });

            modelBuilder.Entity("xappido.Directory.Domain.SubscriptionActivationCode", b =>
                {
                    b.HasOne("xappido.Directory.Domain.SubscriptionActivation<aiala.Backend.Data.Group>")
                        .WithMany("Codes")
                        .HasForeignKey("SubscriptionActivationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("xappido.Directory.Domain.SubscriptionType<aiala.Backend.Data.Group>", b =>
                {
                    b.HasOne("xappido.Directory.Domain.App<aiala.Backend.Data.Group>", "App")
                        .WithMany("SubscriptionTypes")
                        .HasForeignKey("AppId");
                });

            modelBuilder.Entity("xappido.Directory.Settings.AccountSetting<aiala.Backend.Data.Member, aiala.Backend.Data.Group, aiala.Backend.Data.AppUser>", b =>
                {
                    b.HasOne("aiala.Backend.Data.Member", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.HasOne("xappido.Settings.Models.SettingType", "SettingType")
                        .WithMany()
                        .HasForeignKey("SettingTypeKey", "SettingTypeTypeId");
                });

            modelBuilder.Entity("xappido.Directory.Settings.TenantSetting<aiala.Backend.Data.Group>", b =>
                {
                    b.HasOne("aiala.Backend.Data.Group", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");

                    b.HasOne("xappido.Settings.Models.SettingType", "SettingType")
                        .WithMany()
                        .HasForeignKey("SettingTypeKey", "SettingTypeTypeId");
                });

            modelBuilder.Entity("xappido.Directory.Settings.UserSetting<aiala.Backend.Data.AppUser>", b =>
                {
                    b.HasOne("aiala.Backend.Data.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.HasOne("xappido.Settings.Models.SettingType", "SettingType")
                        .WithMany()
                        .HasForeignKey("SettingTypeKey", "SettingTypeTypeId");
                });
#pragma warning restore 612, 618
        }
    }
}
