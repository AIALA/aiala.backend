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
    [Migration("20190318135408_AddForeignKeys")]
    partial class AddForeignKeys
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("aiala")
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("aiala.Backend.Data.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<bool>("Enabled");

                    b.Property<string>("Firstname");

                    b.Property<Guid?>("InvitationId");

                    b.Property<string>("Lastname");

                    b.Property<string>("PhoneNumber");

                    b.Property<Guid>("TenantId");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("InvitationId");

                    b.HasIndex("TenantId");

                    b.HasIndex("UserId");

                    b.ToTable("Accounts","directory");
                });

            modelBuilder.Entity("aiala.Backend.Data.Activities.Activity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ActiveTaskId");

                    b.Property<string>("ActivityData");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<decimal?>("Latitude")
                        .HasColumnType("decimal(9, 6)");

                    b.Property<decimal?>("Longitude")
                        .HasColumnType("decimal(9, 6)");

                    b.Property<Guid?>("TenantId");

                    b.Property<DateTimeOffset>("TimeCreated");

                    b.Property<DateTimeOffset>("Timestamp");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ActiveTaskId");

                    b.HasIndex("TenantId");

                    b.ToTable("Activities");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Activity");
                });

            modelBuilder.Entity("aiala.Backend.Data.Places.Place", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("decimal(9, 6)");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("decimal(9, 6)");

                    b.Property<string>("Name");

                    b.Property<Guid>("PictureId");

                    b.Property<Guid?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("aiala.Backend.Data.Schedule.Day", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("Date");

                    b.Property<string>("Name");

                    b.Property<Guid?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("Days");
                });

            modelBuilder.Entity("aiala.Backend.Data.Schedule.ScheduledStep", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Order");

                    b.Property<int>("State");

                    b.Property<Guid?>("TaskId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("ScheduledSteps");
                });

            modelBuilder.Entity("aiala.Backend.Data.Schedule.ScheduledTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("DayId");

                    b.Property<TimeSpan>("DefaultDuration");

                    b.Property<TimeSpan>("End");

                    b.Property<int>("Feedback");

                    b.Property<string>("Location");

                    b.Property<string>("Name");

                    b.Property<Guid?>("PictureId");

                    b.Property<TimeSpan>("Start");

                    b.Property<Guid?>("TaskId");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.HasIndex("TaskId");

                    b.ToTable("ScheduledTasks");
                });

            modelBuilder.Entity("aiala.Backend.Data.Tasks.AppTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AuthorId");

                    b.Property<TimeSpan>("Duration");

                    b.Property<DateTimeOffset>("LastModified");

                    b.Property<string>("Location");

                    b.Property<string>("Name");

                    b.Property<Guid?>("PictureId");

                    b.Property<long>("Revision");

                    b.Property<Guid?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("TenantId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("aiala.Backend.Data.Tasks.Step", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Order");

                    b.Property<Guid?>("TaskId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("Steps");
                });

            modelBuilder.Entity("aiala.Backend.Data.Templates.DayTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DayName");

                    b.Property<string>("Name");

                    b.Property<Guid?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("DayTemplates");
                });

            modelBuilder.Entity("aiala.Backend.Data.Templates.ScheduledTaskTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("DayTemplateId");

                    b.Property<TimeSpan>("End");

                    b.Property<TimeSpan>("Start");

                    b.Property<Guid>("TaskId");

                    b.HasKey("Id");

                    b.HasIndex("DayTemplateId");

                    b.HasIndex("TaskId");

                    b.ToTable("ScheduledTaskTemplates");
                });

            modelBuilder.Entity("aiala.Backend.Data.Tenant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Enabled");

                    b.Property<string>("Name");

                    b.Property<int>("TenantType");

                    b.HasKey("Id");

                    b.ToTable("Tenants","directory");
                });

            modelBuilder.Entity("aiala.Backend.Data.User", b =>
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

            modelBuilder.Entity("xappido.Directory.Domain.App<aiala.Backend.Data.Tenant>", b =>
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

            modelBuilder.Entity("xappido.Directory.Domain.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<bool>("Enabled");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Countries","directory");
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

                    b.Property<Guid>("InviterId");

                    b.Property<string>("Message");

                    b.Property<int>("Resent");

                    b.Property<DateTimeOffset>("ValidUntil");

                    b.HasKey("Id");

                    b.ToTable("Invitations","directory");
                });

            modelBuilder.Entity("xappido.Directory.Domain.Model.Setting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AccountId");

                    b.Property<string>("Key");

                    b.Property<Guid?>("TenantId");

                    b.Property<int>("Type");

                    b.Property<Guid?>("UserId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("Settings","directory");
                });

            modelBuilder.Entity("xappido.Directory.Domain.PermissionAssignment<aiala.Backend.Data.Tenant, aiala.Backend.Data.Account, aiala.Backend.Data.User>", b =>
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

            modelBuilder.Entity("xappido.Directory.Domain.PermissionGroup<aiala.Backend.Data.Tenant>", b =>
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

            modelBuilder.Entity("xappido.Directory.Domain.PermissionGroupAssignment<aiala.Backend.Data.Tenant, aiala.Backend.Data.Account, aiala.Backend.Data.User>", b =>
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

            modelBuilder.Entity("xappido.Directory.Domain.Registration<aiala.Backend.Data.Tenant>", b =>
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

            modelBuilder.Entity("xappido.Directory.Domain.Subscription<aiala.Backend.Data.Tenant>", b =>
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

            modelBuilder.Entity("xappido.Directory.Domain.SubscriptionActivation<aiala.Backend.Data.Tenant>", b =>
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

            modelBuilder.Entity("xappido.Directory.Domain.SubscriptionType<aiala.Backend.Data.Tenant>", b =>
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

            modelBuilder.Entity("xappido.Directory.Domain.Translation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Culture");

                    b.Property<string>("Entity");

                    b.Property<string>("Reference");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("Entity", "Reference", "Culture")
                        .IsUnique()
                        .HasFilter("[Entity] IS NOT NULL AND [Reference] IS NOT NULL AND [Culture] IS NOT NULL");

                    b.ToTable("Translations","directory");
                });

            modelBuilder.Entity("aiala.Backend.Data.Activities.EmergencyActivity", b =>
                {
                    b.HasBaseType("aiala.Backend.Data.Activities.Activity");

                    b.Property<Guid>("EmergencyId");

                    b.ToTable("EmergencyActivity");

                    b.HasDiscriminator().HasValue("EmergencyActivity");
                });

            modelBuilder.Entity("aiala.Backend.Data.Activities.ScheduledStepActivity", b =>
                {
                    b.HasBaseType("aiala.Backend.Data.Activities.Activity");

                    b.Property<Guid?>("StepId");

                    b.HasIndex("StepId");

                    b.ToTable("ScheduledStepActivity");

                    b.HasDiscriminator().HasValue("ScheduledStepActivity");
                });

            modelBuilder.Entity("aiala.Backend.Data.Activities.ScheduledTaskActivity", b =>
                {
                    b.HasBaseType("aiala.Backend.Data.Activities.Activity");

                    b.Property<Guid?>("TaskId");

                    b.HasIndex("TaskId");

                    b.ToTable("ScheduledTaskActivity");

                    b.HasDiscriminator().HasValue("ScheduledTaskActivity");
                });

            modelBuilder.Entity("aiala.Backend.Data.Account", b =>
                {
                    b.HasOne("xappido.Directory.Domain.Invitation", "Invitation")
                        .WithMany()
                        .HasForeignKey("InvitationId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("aiala.Backend.Data.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("aiala.Backend.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("aiala.Backend.Data.Activities.Activity", b =>
                {
                    b.HasOne("aiala.Backend.Data.Schedule.ScheduledTask", "ActiveTask")
                        .WithMany()
                        .HasForeignKey("ActiveTaskId");

                    b.HasOne("aiala.Backend.Data.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("aiala.Backend.Data.Places.Place", b =>
                {
                    b.HasOne("aiala.Backend.Data.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("aiala.Backend.Data.Schedule.Day", b =>
                {
                    b.HasOne("aiala.Backend.Data.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("aiala.Backend.Data.Schedule.ScheduledStep", b =>
                {
                    b.HasOne("aiala.Backend.Data.Schedule.ScheduledTask", "Task")
                        .WithMany("Steps")
                        .HasForeignKey("TaskId");
                });

            modelBuilder.Entity("aiala.Backend.Data.Schedule.ScheduledTask", b =>
                {
                    b.HasOne("aiala.Backend.Data.Schedule.Day", "Day")
                        .WithMany("Tasks")
                        .HasForeignKey("DayId");

                    b.HasOne("aiala.Backend.Data.Tasks.AppTask", "Task")
                        .WithMany()
                        .HasForeignKey("TaskId");
                });

            modelBuilder.Entity("aiala.Backend.Data.Tasks.AppTask", b =>
                {
                    b.HasOne("aiala.Backend.Data.Account", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("aiala.Backend.Data.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("aiala.Backend.Data.Tasks.Step", b =>
                {
                    b.HasOne("aiala.Backend.Data.Tasks.AppTask", "Task")
                        .WithMany("Steps")
                        .HasForeignKey("TaskId");
                });

            modelBuilder.Entity("aiala.Backend.Data.Templates.DayTemplate", b =>
                {
                    b.HasOne("aiala.Backend.Data.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("aiala.Backend.Data.Templates.ScheduledTaskTemplate", b =>
                {
                    b.HasOne("aiala.Backend.Data.Templates.DayTemplate", "DayTemplate")
                        .WithMany("Tasks")
                        .HasForeignKey("DayTemplateId");

                    b.HasOne("aiala.Backend.Data.Tasks.AppTask", "Task")
                        .WithMany()
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("xappido.Directory.Domain.PermissionAssignment<aiala.Backend.Data.Tenant, aiala.Backend.Data.Account, aiala.Backend.Data.User>", b =>
                {
                    b.HasOne("aiala.Backend.Data.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.HasOne("xappido.Directory.Domain.PermissionGroup<aiala.Backend.Data.Tenant>", "PermissionGroup")
                        .WithMany()
                        .HasForeignKey("PermissionGroupId");

                    b.HasOne("aiala.Backend.Data.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("xappido.Directory.Domain.PermissionGroup<aiala.Backend.Data.Tenant>", b =>
                {
                    b.HasOne("xappido.Directory.Domain.App<aiala.Backend.Data.Tenant>", "App")
                        .WithMany()
                        .HasForeignKey("AppId");

                    b.HasOne("aiala.Backend.Data.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("xappido.Directory.Domain.PermissionGroupAssignment<aiala.Backend.Data.Tenant, aiala.Backend.Data.Account, aiala.Backend.Data.User>", b =>
                {
                    b.HasOne("aiala.Backend.Data.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("xappido.Directory.Domain.PermissionGroup<aiala.Backend.Data.Tenant>", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("xappido.Directory.Domain.Registration<aiala.Backend.Data.Tenant>", b =>
                {
                    b.HasOne("xappido.Directory.Domain.SubscriptionType<aiala.Backend.Data.Tenant>", "SubscriptionType")
                        .WithMany()
                        .HasForeignKey("SubscriptionTypeId");
                });

            modelBuilder.Entity("xappido.Directory.Domain.Subscription<aiala.Backend.Data.Tenant>", b =>
                {
                    b.HasOne("xappido.Directory.Domain.SubscriptionType<aiala.Backend.Data.Tenant>", "SubscriptionType")
                        .WithMany("Subscriptions")
                        .HasForeignKey("SubscriptionTypeId");

                    b.HasOne("aiala.Backend.Data.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("xappido.Directory.Domain.SubscriptionActivation<aiala.Backend.Data.Tenant>", b =>
                {
                    b.HasOne("aiala.Backend.Data.Tenant", "DefaultTenant")
                        .WithMany()
                        .HasForeignKey("DefaultTenantId");

                    b.HasOne("xappido.Directory.Domain.SubscriptionType<aiala.Backend.Data.Tenant>", "SubscriptionType")
                        .WithMany("Activations")
                        .HasForeignKey("SubscriptionTypeId");
                });

            modelBuilder.Entity("xappido.Directory.Domain.SubscriptionActivationCode", b =>
                {
                    b.HasOne("xappido.Directory.Domain.SubscriptionActivation<aiala.Backend.Data.Tenant>")
                        .WithMany("Codes")
                        .HasForeignKey("SubscriptionActivationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("xappido.Directory.Domain.SubscriptionType<aiala.Backend.Data.Tenant>", b =>
                {
                    b.HasOne("xappido.Directory.Domain.App<aiala.Backend.Data.Tenant>", "App")
                        .WithMany("SubscriptionTypes")
                        .HasForeignKey("AppId");
                });

            modelBuilder.Entity("aiala.Backend.Data.Activities.ScheduledStepActivity", b =>
                {
                    b.HasOne("aiala.Backend.Data.Schedule.ScheduledStep", "Step")
                        .WithMany()
                        .HasForeignKey("StepId");
                });

            modelBuilder.Entity("aiala.Backend.Data.Activities.ScheduledTaskActivity", b =>
                {
                    b.HasOne("aiala.Backend.Data.Schedule.ScheduledTask", "Task")
                        .WithMany()
                        .HasForeignKey("TaskId");
                });
#pragma warning restore 612, 618
        }
    }
}
