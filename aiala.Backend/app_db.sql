info: Microsoft.EntityFrameworkCore.Infrastructure[10403]
      Entity Framework Core 2.1.14-servicing-32113 initialized 'AppDbContext' using provider 'Microsoft.EntityFrameworkCore.SqlServer' with options: MigrationsAssembly=aiala.Backend 
IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF SCHEMA_ID(N'aiala') IS NULL EXEC(N'CREATE SCHEMA [aiala];');

GO

CREATE TABLE [aiala].[Apps] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Key] nvarchar(max) NULL,
    [Enabled] bit NOT NULL,
    CONSTRAINT [PK_Apps] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [aiala].[SettingTypes] (
    [Key] nvarchar(450) NOT NULL,
    [TypeId] nvarchar(450) NOT NULL,
    [Order] int NOT NULL,
    [Category] nvarchar(max) NULL,
    [DefaultValue] nvarchar(max) NULL,
    [DataType] int NOT NULL,
    CONSTRAINT [PK_SettingTypes] PRIMARY KEY ([Key], [TypeId])
);

GO

CREATE TABLE [aiala].[Tenants] (
    [Id] uniqueidentifier NOT NULL,
    [Enabled] bit NOT NULL,
    [Name] nvarchar(max) NULL,
    [TenantType] int NOT NULL,
    CONSTRAINT [PK_Tenants] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [aiala].[Users] (
    [Id] uniqueidentifier NOT NULL,
    [ActiveAccountId] uniqueidentifier NULL,
    [ExternalUserId] nvarchar(max) NULL,
    [Firstname] nvarchar(max) NULL,
    [Lastname] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [aiala].[SubscriptionTypes] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Enabled] bit NOT NULL,
    [Description] nvarchar(max) NULL,
    [Features] nvarchar(max) NULL,
    [AppId] uniqueidentifier NULL,
    [SubscriptionLengthInDays] int NULL,
    CONSTRAINT [PK_SubscriptionTypes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SubscriptionTypes_Apps_AppId] FOREIGN KEY ([AppId]) REFERENCES [aiala].[Apps] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [aiala].[PermissionGroups] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [AppId] uniqueidentifier NULL,
    [TenantId] uniqueidentifier NULL,
    CONSTRAINT [PK_PermissionGroups] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PermissionGroups_Apps_AppId] FOREIGN KEY ([AppId]) REFERENCES [aiala].[Apps] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PermissionGroups_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [aiala].[Tenants] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [aiala].[TenantSettings] (
    [Id] uniqueidentifier NOT NULL,
    [Value] nvarchar(max) NULL,
    [SettingTypeKey] nvarchar(450) NULL,
    [SettingTypeTypeId] nvarchar(450) NULL,
    [TenantId] uniqueidentifier NULL,
    CONSTRAINT [PK_TenantSettings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TenantSettings_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [aiala].[Tenants] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TenantSettings_SettingTypes_SettingTypeKey_SettingTypeTypeId] FOREIGN KEY ([SettingTypeKey], [SettingTypeTypeId]) REFERENCES [aiala].[SettingTypes] ([Key], [TypeId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [aiala].[Accounts] (
    [Id] uniqueidentifier NOT NULL,
    [Enabled] bit NOT NULL,
    [TenantId] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Accounts_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [aiala].[Tenants] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Accounts_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [aiala].[Users] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [aiala].[UserSettings] (
    [Id] uniqueidentifier NOT NULL,
    [Value] nvarchar(max) NULL,
    [SettingTypeKey] nvarchar(450) NULL,
    [SettingTypeTypeId] nvarchar(450) NULL,
    [UserId] uniqueidentifier NULL,
    CONSTRAINT [PK_UserSettings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserSettings_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [aiala].[Users] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_UserSettings_SettingTypes_SettingTypeKey_SettingTypeTypeId] FOREIGN KEY ([SettingTypeKey], [SettingTypeTypeId]) REFERENCES [aiala].[SettingTypes] ([Key], [TypeId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [aiala].[Registrations] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] nvarchar(max) NULL,
    [CreatedAccountId] uniqueidentifier NULL,
    [CreatedSubscriptionId] uniqueidentifier NULL,
    [TenantId] uniqueidentifier NULL,
    [TenantType] int NOT NULL,
    [Email] nvarchar(max) NULL,
    [Created] datetimeoffset NOT NULL,
    [ApprovalRequired] bit NOT NULL,
    [Approved] datetimeoffset NULL,
    [ApprovedBy] nvarchar(max) NULL,
    [ApprovalToken] nvarchar(max) NULL,
    [Confirmed] datetimeoffset NULL,
    [Completed] datetimeoffset NULL,
    [SubscriptionTypeId] uniqueidentifier NULL,
    [ConfirmationToken] nvarchar(max) NOT NULL,
    [Values] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Registrations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Registrations_SubscriptionTypes_SubscriptionTypeId] FOREIGN KEY ([SubscriptionTypeId]) REFERENCES [aiala].[SubscriptionTypes] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [aiala].[SubscriptionActivations] (
    [Id] uniqueidentifier NOT NULL,
    [DefaultTenantType] int NOT NULL,
    [DefaultTenantId] uniqueidentifier NULL,
    [SubscriptionTypeId] uniqueidentifier NULL,
    [ActivationType] int NOT NULL,
    [ApprovalRequired] bit NOT NULL,
    [Approvers] nvarchar(max) NULL,
    CONSTRAINT [PK_SubscriptionActivations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SubscriptionActivations_Tenants_DefaultTenantId] FOREIGN KEY ([DefaultTenantId]) REFERENCES [aiala].[Tenants] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SubscriptionActivations_SubscriptionTypes_SubscriptionTypeId] FOREIGN KEY ([SubscriptionTypeId]) REFERENCES [aiala].[SubscriptionTypes] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [aiala].[Subscriptions] (
    [Id] uniqueidentifier NOT NULL,
    [Created] datetimeoffset NOT NULL,
    [TenantId] uniqueidentifier NULL,
    [SubscriptionTypeId] uniqueidentifier NULL,
    [ValidFrom] datetimeoffset NULL,
    [ValidTo] datetimeoffset NULL,
    CONSTRAINT [PK_Subscriptions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Subscriptions_SubscriptionTypes_SubscriptionTypeId] FOREIGN KEY ([SubscriptionTypeId]) REFERENCES [aiala].[SubscriptionTypes] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Subscriptions_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [aiala].[Tenants] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [aiala].[AccountSettings] (
    [Id] uniqueidentifier NOT NULL,
    [Value] nvarchar(max) NULL,
    [SettingTypeKey] nvarchar(450) NULL,
    [SettingTypeTypeId] nvarchar(450) NULL,
    [AccountId] uniqueidentifier NULL,
    CONSTRAINT [PK_AccountSettings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AccountSettings_Accounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [aiala].[Accounts] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AccountSettings_SettingTypes_SettingTypeKey_SettingTypeTypeId] FOREIGN KEY ([SettingTypeKey], [SettingTypeTypeId]) REFERENCES [aiala].[SettingTypes] ([Key], [TypeId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [aiala].[PermissionAssignments] (
    [Id] uniqueidentifier NOT NULL,
    [PermissionGroupId] uniqueidentifier NULL,
    [TenantId] uniqueidentifier NULL,
    [AccountId] uniqueidentifier NULL,
    [PermissionType] nvarchar(max) NULL,
    [ValidFrom] datetimeoffset NULL,
    [ValidTo] datetimeoffset NULL,
    CONSTRAINT [PK_PermissionAssignments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PermissionAssignments_Accounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [aiala].[Accounts] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PermissionAssignments_PermissionGroups_PermissionGroupId] FOREIGN KEY ([PermissionGroupId]) REFERENCES [aiala].[PermissionGroups] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PermissionAssignments_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [aiala].[Tenants] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [aiala].[PermissionGroupAssignments] (
    [Id] uniqueidentifier NOT NULL,
    [AccountId] uniqueidentifier NOT NULL,
    [GroupId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_PermissionGroupAssignments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PermissionGroupAssignments_Accounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [aiala].[Accounts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PermissionGroupAssignments_PermissionGroups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [aiala].[PermissionGroups] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [aiala].[SubscriptionActivationCodes] (
    [Id] uniqueidentifier NOT NULL,
    [ActivationKey] nvarchar(max) NULL,
    [ValidFrom] datetimeoffset NULL,
    [ValidTo] datetimeoffset NULL,
    [Activated] datetimeoffset NULL,
    [SubscriptionActivationId] uniqueidentifier NULL,
    CONSTRAINT [PK_SubscriptionActivationCodes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SubscriptionActivationCodes_SubscriptionActivations_SubscriptionActivationId] FOREIGN KEY ([SubscriptionActivationId]) REFERENCES [aiala].[SubscriptionActivations] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Accounts_TenantId] ON [aiala].[Accounts] ([TenantId]);

GO

CREATE INDEX [IX_Accounts_UserId] ON [aiala].[Accounts] ([UserId]);

GO

CREATE INDEX [IX_AccountSettings_AccountId] ON [aiala].[AccountSettings] ([AccountId]);

GO

CREATE INDEX [IX_AccountSettings_SettingTypeKey_SettingTypeTypeId] ON [aiala].[AccountSettings] ([SettingTypeKey], [SettingTypeTypeId]);

GO

CREATE INDEX [IX_PermissionAssignments_AccountId] ON [aiala].[PermissionAssignments] ([AccountId]);

GO

CREATE INDEX [IX_PermissionAssignments_PermissionGroupId] ON [aiala].[PermissionAssignments] ([PermissionGroupId]);

GO

CREATE INDEX [IX_PermissionAssignments_TenantId] ON [aiala].[PermissionAssignments] ([TenantId]);

GO

CREATE INDEX [IX_PermissionGroupAssignments_AccountId] ON [aiala].[PermissionGroupAssignments] ([AccountId]);

GO

CREATE INDEX [IX_PermissionGroupAssignments_GroupId] ON [aiala].[PermissionGroupAssignments] ([GroupId]);

GO

CREATE INDEX [IX_PermissionGroups_AppId] ON [aiala].[PermissionGroups] ([AppId]);

GO

CREATE INDEX [IX_PermissionGroups_TenantId] ON [aiala].[PermissionGroups] ([TenantId]);

GO

CREATE INDEX [IX_Registrations_SubscriptionTypeId] ON [aiala].[Registrations] ([SubscriptionTypeId]);

GO

CREATE INDEX [IX_SubscriptionActivationCodes_SubscriptionActivationId] ON [aiala].[SubscriptionActivationCodes] ([SubscriptionActivationId]);

GO

CREATE INDEX [IX_SubscriptionActivations_DefaultTenantId] ON [aiala].[SubscriptionActivations] ([DefaultTenantId]);

GO

CREATE INDEX [IX_SubscriptionActivations_SubscriptionTypeId] ON [aiala].[SubscriptionActivations] ([SubscriptionTypeId]);

GO

CREATE INDEX [IX_Subscriptions_SubscriptionTypeId] ON [aiala].[Subscriptions] ([SubscriptionTypeId]);

GO

CREATE INDEX [IX_Subscriptions_TenantId] ON [aiala].[Subscriptions] ([TenantId]);

GO

CREATE INDEX [IX_SubscriptionTypes_AppId] ON [aiala].[SubscriptionTypes] ([AppId]);

GO

CREATE INDEX [IX_TenantSettings_TenantId] ON [aiala].[TenantSettings] ([TenantId]);

GO

CREATE INDEX [IX_TenantSettings_SettingTypeKey_SettingTypeTypeId] ON [aiala].[TenantSettings] ([SettingTypeKey], [SettingTypeTypeId]);

GO

CREATE INDEX [IX_UserSettings_UserId] ON [aiala].[UserSettings] ([UserId]);

GO

CREATE INDEX [IX_UserSettings_SettingTypeKey_SettingTypeTypeId] ON [aiala].[UserSettings] ([SettingTypeKey], [SettingTypeTypeId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190111085316_InitialMigration', N'2.1.14-servicing-32113');

GO

ALTER TABLE [aiala].[Registrations] ADD [Culture] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190124101003_UpdateDirectory', N'2.1.14-servicing-32113');

GO

IF SCHEMA_ID(N'directory') IS NULL EXEC(N'CREATE SCHEMA [directory];');

GO

ALTER SCHEMA [directory] TRANSFER [aiala].[UserSettings];

GO

ALTER SCHEMA [directory] TRANSFER [aiala].[Users];

GO

ALTER SCHEMA [directory] TRANSFER [aiala].[TenantSettings];

GO

ALTER SCHEMA [directory] TRANSFER [aiala].[Tenants];

GO

ALTER SCHEMA [directory] TRANSFER [aiala].[SubscriptionTypes];

GO

ALTER SCHEMA [directory] TRANSFER [aiala].[Subscriptions];

GO

ALTER SCHEMA [directory] TRANSFER [aiala].[SubscriptionActivations];

GO

ALTER SCHEMA [directory] TRANSFER [aiala].[SubscriptionActivationCodes];

GO

ALTER SCHEMA [directory] TRANSFER [aiala].[SettingTypes];

GO

ALTER SCHEMA [directory] TRANSFER [aiala].[Registrations];

GO

ALTER SCHEMA [directory] TRANSFER [aiala].[PermissionGroups];

GO

ALTER SCHEMA [directory] TRANSFER [aiala].[PermissionGroupAssignments];

GO

ALTER SCHEMA [directory] TRANSFER [aiala].[PermissionAssignments];

GO

ALTER SCHEMA [directory] TRANSFER [aiala].[Apps];

GO

ALTER SCHEMA [directory] TRANSFER [aiala].[AccountSettings];

GO

ALTER SCHEMA [directory] TRANSFER [aiala].[Accounts];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190128135053_DirectoryPermissions', N'2.1.14-servicing-32113');

GO

ALTER TABLE [directory].[Accounts] DROP CONSTRAINT [FK_Accounts_Users_UserId];

GO

IF SCHEMA_ID(N'aiala') IS NULL EXEC(N'CREATE SCHEMA [aiala];');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[directory].[Accounts]') AND [c].[name] = N'UserId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [directory].[Accounts] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [directory].[Accounts] ALTER COLUMN [UserId] uniqueidentifier NULL;

GO

ALTER TABLE [directory].[Accounts] ADD [Email] nvarchar(max) NULL;

GO

ALTER TABLE [directory].[Accounts] ADD [Firstname] nvarchar(max) NULL;

GO

ALTER TABLE [directory].[Accounts] ADD [InvitationId] uniqueidentifier NULL;

GO

ALTER TABLE [directory].[Accounts] ADD [Lastname] nvarchar(max) NULL;

GO

ALTER TABLE [directory].[Accounts] ADD [RoleType] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [directory].[Accounts] ADD [WhatsAppNumber] nvarchar(max) NULL;

GO

CREATE TABLE [directory].[Invitations] (
    [Id] uniqueidentifier NOT NULL,
    [Created] datetimeoffset NOT NULL,
    [ValidUntil] datetimeoffset NOT NULL,
    [Email] nvarchar(max) NULL,
    [ConfirmationToken] nvarchar(max) NULL,
    [Culture] nvarchar(max) NULL,
    [Resent] int NOT NULL,
    [Message] nvarchar(max) NULL,
    [Accepted] datetimeoffset NULL,
    [Declined] datetimeoffset NULL,
    CONSTRAINT [PK_Invitations] PRIMARY KEY ([Id])
);

GO

CREATE INDEX [IX_Accounts_InvitationId] ON [directory].[Accounts] ([InvitationId]);

GO

ALTER TABLE [directory].[Accounts] ADD CONSTRAINT [FK_Accounts_Invitations_InvitationId] FOREIGN KEY ([InvitationId]) REFERENCES [directory].[Invitations] ([Id]) ON DELETE SET NULL;

GO

ALTER TABLE [directory].[Accounts] ADD CONSTRAINT [FK_Accounts_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [directory].[Users] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190201153949_UserManagement', N'2.1.14-servicing-32113');

GO

IF SCHEMA_ID(N'aiala') IS NULL EXEC(N'CREATE SCHEMA [aiala];');

GO

CREATE TABLE [aiala].[Tasks] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [PictureUrl] nvarchar(max) NULL,
    [Duration] time NOT NULL,
    [Location] nvarchar(max) NULL,
    [LastModified] datetimeoffset NOT NULL,
    [Revision] bigint NOT NULL,
    [AuthorId] uniqueidentifier NULL,
    [GroupId] uniqueidentifier NULL,
    CONSTRAINT [PK_Tasks] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Tasks_Accounts_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [directory].[Accounts] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Tasks_Tenants_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [directory].[Tenants] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Tasks_AuthorId] ON [aiala].[Tasks] ([AuthorId]);

GO

CREATE INDEX [IX_Tasks_GroupId] ON [aiala].[Tasks] ([GroupId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190204152245_AddTask', N'2.1.14-servicing-32113');

GO

CREATE TABLE [aiala].[Steps] (
    [Id] uniqueidentifier NOT NULL,
    [Text] nvarchar(max) NULL,
    [AppTaskId] uniqueidentifier NULL,
    CONSTRAINT [PK_Steps] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Steps_Tasks_AppTaskId] FOREIGN KEY ([AppTaskId]) REFERENCES [aiala].[Tasks] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Steps_AppTaskId] ON [aiala].[Steps] ([AppTaskId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190206100424_AddSteps', N'2.1.14-servicing-32113');

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[aiala].[Tasks]') AND [c].[name] = N'PictureUrl');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [aiala].[Tasks] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [aiala].[Tasks] DROP COLUMN [PictureUrl];

GO

ALTER TABLE [aiala].[Tasks] ADD [PictureId] uniqueidentifier NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190207121119_StorePictureIds', N'2.1.14-servicing-32113');

GO

ALTER TABLE [aiala].[Steps] ADD [Order] int NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190208144457_AddStepsOrder', N'2.1.14-servicing-32113');

GO

ALTER TABLE [directory].[Subscriptions] DROP CONSTRAINT [FK_Subscriptions_Tenants_TenantId];

GO

CREATE TABLE [directory].[Countries] (
    [Id] uniqueidentifier NOT NULL,
    [Enabled] bit NOT NULL,
    [Code] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [directory].[Translations] (
    [Id] uniqueidentifier NOT NULL,
    [Entity] nvarchar(450) NULL,
    [Reference] nvarchar(450) NULL,
    [Culture] nvarchar(450) NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_Translations] PRIMARY KEY ([Id])
);

GO

CREATE UNIQUE INDEX [IX_Translations_Entity_Reference_Culture] ON [directory].[Translations] ([Entity], [Reference], [Culture]) WHERE [Entity] IS NOT NULL AND [Reference] IS NOT NULL AND [Culture] IS NOT NULL;

GO

ALTER TABLE [directory].[Subscriptions] ADD CONSTRAINT [FK_Subscriptions_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [directory].[Tenants] ([Id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190213145747_UpdatePortal', N'2.1.14-servicing-32113');

GO

CREATE TABLE [aiala].[Days] (
    [Id] uniqueidentifier NOT NULL,
    [Date] datetimeoffset NOT NULL,
    [Name] nvarchar(max) NULL,
    [GroupId] uniqueidentifier NULL,
    CONSTRAINT [PK_Days] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Days_Tenants_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [directory].[Tenants] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [aiala].[ScheduledTasks] (
    [Id] uniqueidentifier NOT NULL,
    [Start] datetimeoffset NOT NULL,
    [End] datetimeoffset NOT NULL,
    [DayId] uniqueidentifier NULL,
    CONSTRAINT [PK_ScheduledTasks] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ScheduledTasks_Days_DayId] FOREIGN KEY ([DayId]) REFERENCES [aiala].[Days] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Days_GroupId] ON [aiala].[Days] ([GroupId]);

GO

CREATE INDEX [IX_ScheduledTasks_DayId] ON [aiala].[ScheduledTasks] ([DayId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190215095229_AddScheduleEntities', N'2.1.14-servicing-32113');

GO

ALTER TABLE [aiala].[ScheduledTasks] ADD [DefaultDuration] nvarchar(max) NULL;

GO

ALTER TABLE [aiala].[ScheduledTasks] ADD [Name] nvarchar(max) NULL;

GO

ALTER TABLE [aiala].[ScheduledTasks] ADD [PictureUrl] nvarchar(max) NULL;

GO

CREATE TABLE [aiala].[ScheduledSteps] (
    [Id] uniqueidentifier NOT NULL,
    [Text] nvarchar(max) NULL,
    [Order] int NOT NULL,
    [ScheduledTaskId] uniqueidentifier NULL,
    CONSTRAINT [PK_ScheduledSteps] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ScheduledSteps_ScheduledTasks_ScheduledTaskId] FOREIGN KEY ([ScheduledTaskId]) REFERENCES [aiala].[ScheduledTasks] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_ScheduledSteps_ScheduledTaskId] ON [aiala].[ScheduledSteps] ([ScheduledTaskId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190221090600_AddScheduledTasksDetails', N'2.1.14-servicing-32113');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190221092652_RenameDefaultDurationOnTask', N'2.1.14-servicing-32113');

GO

ALTER TABLE [aiala].[ScheduledTasks] ADD [Location] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190221093630_UpdateScheduledTaskDetails', N'2.1.14-servicing-32113');

GO

ALTER TABLE [aiala].[Days] DROP CONSTRAINT [FK_Days_Tenants_GroupId];

GO

ALTER TABLE [aiala].[Tasks] DROP CONSTRAINT [FK_Tasks_Tenants_GroupId];

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[directory].[Accounts]') AND [c].[name] = N'RoleType');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [directory].[Accounts] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [directory].[Accounts] DROP COLUMN [RoleType];

GO

EXEC sp_rename N'[aiala].[Tasks].[GroupId]', N'TenantId', N'COLUMN';

GO

EXEC sp_rename N'[aiala].[Tasks].[IX_Tasks_GroupId]', N'IX_Tasks_TenantId', N'INDEX';

GO

EXEC sp_rename N'[aiala].[Days].[GroupId]', N'TenantId', N'COLUMN';

GO

EXEC sp_rename N'[aiala].[Days].[IX_Days_GroupId]', N'IX_Days_TenantId', N'INDEX';

GO

ALTER TABLE [directory].[Invitations] ADD [InviterId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';

GO

ALTER TABLE [aiala].[Days] ADD CONSTRAINT [FK_Days_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [directory].[Tenants] ([Id]) ON DELETE NO ACTION;

GO

ALTER TABLE [aiala].[Tasks] ADD CONSTRAINT [FK_Tasks_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [directory].[Tenants] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190222065719_UpdateXpdoPortal', N'2.1.14-servicing-32113');

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[aiala].[ScheduledTasks]') AND [c].[name] = N'Start');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [aiala].[ScheduledTasks] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [aiala].[ScheduledTasks] ALTER COLUMN [Start] time NOT NULL;

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[aiala].[ScheduledTasks]') AND [c].[name] = N'End');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [aiala].[ScheduledTasks] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [aiala].[ScheduledTasks] ALTER COLUMN [End] time NOT NULL;

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[aiala].[ScheduledTasks]') AND [c].[name] = N'DefaultDuration');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [aiala].[ScheduledTasks] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [aiala].[ScheduledTasks] ALTER COLUMN [DefaultDuration] time NOT NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190226155624_UseTimeSpanForStartEnd', N'2.1.14-servicing-32113');

GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[aiala].[ScheduledTasks]') AND [c].[name] = N'PictureUrl');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [aiala].[ScheduledTasks] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [aiala].[ScheduledTasks] DROP COLUMN [PictureUrl];

GO

ALTER TABLE [aiala].[ScheduledTasks] ADD [PictureId] uniqueidentifier NULL;

GO

ALTER TABLE [aiala].[ScheduledTasks] ADD [TaskId] uniqueidentifier NULL;

GO

CREATE INDEX [IX_ScheduledTasks_TaskId] ON [aiala].[ScheduledTasks] ([TaskId]);

GO

ALTER TABLE [aiala].[ScheduledTasks] ADD CONSTRAINT [FK_ScheduledTasks_Tasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [aiala].[Tasks] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190228093249_AddTaskLinks', N'2.1.14-servicing-32113');

GO

CREATE TABLE [aiala].[DayTemplates] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [TenantId] uniqueidentifier NULL,
    CONSTRAINT [PK_DayTemplates] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DayTemplates_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [directory].[Tenants] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [aiala].[ScheduledTaskTemplates] (
    [Id] uniqueidentifier NOT NULL,
    [Start] time NOT NULL,
    [End] time NOT NULL,
    [TaskId] uniqueidentifier NOT NULL,
    [DayTemplateId] uniqueidentifier NULL,
    CONSTRAINT [PK_ScheduledTaskTemplates] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ScheduledTaskTemplates_DayTemplates_DayTemplateId] FOREIGN KEY ([DayTemplateId]) REFERENCES [aiala].[DayTemplates] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ScheduledTaskTemplates_Tasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [aiala].[Tasks] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_DayTemplates_TenantId] ON [aiala].[DayTemplates] ([TenantId]);

GO

CREATE INDEX [IX_ScheduledTaskTemplates_DayTemplateId] ON [aiala].[ScheduledTaskTemplates] ([DayTemplateId]);

GO

CREATE INDEX [IX_ScheduledTaskTemplates_TaskId] ON [aiala].[ScheduledTaskTemplates] ([TaskId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190228143448_AddTemplates', N'2.1.14-servicing-32113');

GO

ALTER TABLE [aiala].[DayTemplates] ADD [DayName] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190301102402_AddDayNameToTemplate', N'2.1.14-servicing-32113');

GO

ALTER TABLE [aiala].[ScheduledSteps] DROP CONSTRAINT [FK_ScheduledSteps_ScheduledTasks_ScheduledTaskId];

GO

EXEC sp_rename N'[aiala].[ScheduledSteps].[ScheduledTaskId]', N'TaskId', N'COLUMN';

GO

EXEC sp_rename N'[aiala].[ScheduledSteps].[IX_ScheduledSteps_ScheduledTaskId]', N'IX_ScheduledSteps_TaskId', N'INDEX';

GO

ALTER TABLE [aiala].[ScheduledSteps] ADD [State] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [aiala].[ScheduledSteps] ADD CONSTRAINT [FK_ScheduledSteps_ScheduledTasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [aiala].[ScheduledTasks] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190304134531_AddStepState', N'2.1.14-servicing-32113');

GO

ALTER TABLE [aiala].[ScheduledSteps] DROP CONSTRAINT [FK_ScheduledSteps_ScheduledTasks_TaskId];

GO

EXEC sp_rename N'[aiala].[ScheduledSteps].[TaskId]', N'ScheduledTaskId', N'COLUMN';

GO

EXEC sp_rename N'[aiala].[ScheduledSteps].[IX_ScheduledSteps_TaskId]', N'IX_ScheduledSteps_ScheduledTaskId', N'INDEX';

GO

ALTER TABLE [aiala].[ScheduledSteps] ADD CONSTRAINT [FK_ScheduledSteps_ScheduledTasks_ScheduledTaskId] FOREIGN KEY ([ScheduledTaskId]) REFERENCES [aiala].[ScheduledTasks] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190304162647_RemoveStepNavigationProperties', N'2.1.14-servicing-32113');

GO

ALTER TABLE [aiala].[ScheduledTasks] ADD [Feedback] int NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190304171601_AddFeedbackToTask', N'2.1.14-servicing-32113');

GO

DROP TABLE [directory].[AccountSettings];

GO

DROP TABLE [directory].[TenantSettings];

GO

DROP TABLE [directory].[UserSettings];

GO

DROP TABLE [directory].[SettingTypes];

GO

CREATE TABLE [directory].[Settings] (
    [Id] uniqueidentifier NOT NULL,
    [Value] nvarchar(max) NULL,
    [Key] nvarchar(max) NULL,
    [Type] int NOT NULL,
    [AccountId] uniqueidentifier NULL,
    [TenantId] uniqueidentifier NULL,
    [UserId] uniqueidentifier NULL,
    CONSTRAINT [PK_Settings] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190308164004_AddSettings', N'2.1.14-servicing-32113');

GO

ALTER TABLE [directory].[Accounts] ADD [ActivityId] uniqueidentifier NULL;

GO

CREATE TABLE [aiala].[Activities] (
    [Id] uniqueidentifier NOT NULL,
    [CorrelationId] uniqueidentifier NOT NULL,
    [Type] int NOT NULL,
    [TimeCreated] datetimeoffset NOT NULL,
    [TenantId] uniqueidentifier NULL,
    CONSTRAINT [PK_Activities] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Activities_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [directory].[Tenants] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Accounts_ActivityId] ON [directory].[Accounts] ([ActivityId]);

GO

CREATE INDEX [IX_Activities_TenantId] ON [aiala].[Activities] ([TenantId]);

GO

ALTER TABLE [directory].[Accounts] ADD CONSTRAINT [FK_Accounts_Activities_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [aiala].[Activities] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190311170043_AddActivities', N'2.1.14-servicing-32113');

GO

ALTER TABLE [directory].[Accounts] DROP CONSTRAINT [FK_Accounts_Activities_ActivityId];

GO

DROP INDEX [IX_Accounts_ActivityId] ON [directory].[Accounts];

GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[directory].[Accounts]') AND [c].[name] = N'ActivityId');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [directory].[Accounts] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [directory].[Accounts] DROP COLUMN [ActivityId];

GO

EXEC sp_rename N'[aiala].[Activities].[TimeCreated]', N'Metadata_TimeCreated', N'COLUMN';

GO

ALTER TABLE [aiala].[Activities] ADD [Metadata_Latitude] decimal(9, 6) NULL;

GO

ALTER TABLE [aiala].[Activities] ADD [Metadata_Longitude] decimal(9, 6) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190312101124_AddActivitiesMetadata', N'2.1.14-servicing-32113');

GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[aiala].[Activities]') AND [c].[name] = N'CorrelationId');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [aiala].[Activities] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [aiala].[Activities] DROP COLUMN [CorrelationId];

GO

EXEC sp_rename N'[aiala].[Activities].[Metadata_TimeCreated]', N'TimeCreated', N'COLUMN';

GO

EXEC sp_rename N'[aiala].[Activities].[Metadata_Longitude]', N'Longitude', N'COLUMN';

GO

EXEC sp_rename N'[aiala].[Activities].[Metadata_Latitude]', N'Latitude', N'COLUMN';

GO

ALTER TABLE [aiala].[Activities] ADD [ActiveTaskId] uniqueidentifier NULL;

GO

ALTER TABLE [aiala].[Activities] ADD [ActivityData] nvarchar(max) NULL;

GO

ALTER TABLE [aiala].[Activities] ADD [Discriminator] nvarchar(max) NOT NULL DEFAULT N'';

GO

ALTER TABLE [aiala].[Activities] ADD [Timestamp] datetimeoffset NOT NULL DEFAULT '0001-01-01T00:00:00.000+00:00';

GO

ALTER TABLE [aiala].[Activities] ADD [EmergencyId] uniqueidentifier NULL;

GO

ALTER TABLE [aiala].[Activities] ADD [StepId] uniqueidentifier NULL;

GO

ALTER TABLE [aiala].[Activities] ADD [TaskId] uniqueidentifier NULL;

GO

CREATE INDEX [IX_Activities_ActiveTaskId] ON [aiala].[Activities] ([ActiveTaskId]);

GO

CREATE INDEX [IX_Activities_StepId] ON [aiala].[Activities] ([StepId]);

GO

CREATE INDEX [IX_Activities_TaskId] ON [aiala].[Activities] ([TaskId]);

GO

ALTER TABLE [aiala].[Activities] ADD CONSTRAINT [FK_Activities_ScheduledTasks_ActiveTaskId] FOREIGN KEY ([ActiveTaskId]) REFERENCES [aiala].[ScheduledTasks] ([Id]) ON DELETE NO ACTION;

GO

ALTER TABLE [aiala].[Activities] ADD CONSTRAINT [FK_Activities_ScheduledSteps_StepId] FOREIGN KEY ([StepId]) REFERENCES [aiala].[ScheduledSteps] ([Id]) ON DELETE NO ACTION;

GO

ALTER TABLE [aiala].[Activities] ADD CONSTRAINT [FK_Activities_ScheduledTasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [aiala].[ScheduledTasks] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190312125903_AddMultipleActivitiesEntities', N'2.1.14-servicing-32113');

GO

EXEC sp_rename N'[directory].[Accounts].[WhatsAppNumber]', N'PhoneNumber', N'COLUMN';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190313085928_RenamePhoneNumber', N'2.1.14-servicing-32113');

GO

CREATE TABLE [aiala].[Places] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [PictureId] uniqueidentifier NOT NULL,
    [Latitude] decimal(9, 6) NOT NULL,
    [Longitude] decimal(9, 6) NOT NULL,
    [TenantId] uniqueidentifier NULL,
    CONSTRAINT [PK_Places] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Places_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [directory].[Tenants] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Places_TenantId] ON [aiala].[Places] ([TenantId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190314144233_AddPlaces', N'2.1.14-servicing-32113');

GO

ALTER TABLE [aiala].[Places] ADD [IsDeleted] bit NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190318102128_AddIsDeletedToStep', N'2.1.14-servicing-32113');

GO

ALTER TABLE [aiala].[ScheduledSteps] DROP CONSTRAINT [FK_ScheduledSteps_ScheduledTasks_ScheduledTaskId];

GO

ALTER TABLE [aiala].[Steps] DROP CONSTRAINT [FK_Steps_Tasks_AppTaskId];

GO

EXEC sp_rename N'[aiala].[Steps].[AppTaskId]', N'TaskId', N'COLUMN';

GO

EXEC sp_rename N'[aiala].[Steps].[IX_Steps_AppTaskId]', N'IX_Steps_TaskId', N'INDEX';

GO

EXEC sp_rename N'[aiala].[ScheduledSteps].[ScheduledTaskId]', N'TaskId', N'COLUMN';

GO

EXEC sp_rename N'[aiala].[ScheduledSteps].[IX_ScheduledSteps_ScheduledTaskId]', N'IX_ScheduledSteps_TaskId', N'INDEX';

GO

ALTER TABLE [aiala].[ScheduledSteps] ADD CONSTRAINT [FK_ScheduledSteps_ScheduledTasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [aiala].[ScheduledTasks] ([Id]) ON DELETE NO ACTION;

GO

ALTER TABLE [aiala].[Steps] ADD CONSTRAINT [FK_Steps_Tasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [aiala].[Tasks] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190318135408_AddForeignKeys', N'2.1.14-servicing-32113');

GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[aiala].[Tasks]') AND [c].[name] = N'Location');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [aiala].[Tasks] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [aiala].[Tasks] DROP COLUMN [Location];

GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[aiala].[Tasks]') AND [c].[name] = N'Revision');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [aiala].[Tasks] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [aiala].[Tasks] DROP COLUMN [Revision];

GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[aiala].[ScheduledTasks]') AND [c].[name] = N'Location');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [aiala].[ScheduledTasks] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [aiala].[ScheduledTasks] DROP COLUMN [Location];

GO

ALTER TABLE [aiala].[Tasks] ADD [PlaceId] uniqueidentifier NULL;

GO

CREATE TABLE [aiala].[ScheduledPlace] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [PictureId] uniqueidentifier NOT NULL,
    [Latitude] decimal(9, 6) NOT NULL,
    [Longitude] decimal(9, 6) NOT NULL,
    [TaskId] uniqueidentifier NOT NULL,
    [PlaceId] uniqueidentifier NULL,
    CONSTRAINT [PK_ScheduledPlace] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ScheduledPlace_Places_PlaceId] FOREIGN KEY ([PlaceId]) REFERENCES [aiala].[Places] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ScheduledPlace_ScheduledTasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [aiala].[ScheduledTasks] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Tasks_PlaceId] ON [aiala].[Tasks] ([PlaceId]);

GO

CREATE INDEX [IX_ScheduledPlace_PlaceId] ON [aiala].[ScheduledPlace] ([PlaceId]);

GO

CREATE UNIQUE INDEX [IX_ScheduledPlace_TaskId] ON [aiala].[ScheduledPlace] ([TaskId]);

GO

ALTER TABLE [aiala].[Tasks] ADD CONSTRAINT [FK_Tasks_Places_PlaceId] FOREIGN KEY ([PlaceId]) REFERENCES [aiala].[Places] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190319080650_AddPlaceToTask', N'2.1.14-servicing-32113');

GO

ALTER TABLE [aiala].[Tasks] ADD [IsDeleted] bit NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190321104428_AddTaskIsDeleted', N'2.1.14-servicing-32113');

GO

ALTER TABLE [aiala].[Tasks] ADD [EmergencyContact1Id] uniqueidentifier NULL;

GO

ALTER TABLE [aiala].[Tasks] ADD [EmergencyContact2Id] uniqueidentifier NULL;

GO

ALTER TABLE [aiala].[Tasks] ADD [UseTaskContacts] bit NOT NULL DEFAULT 0;

GO

CREATE INDEX [IX_Tasks_EmergencyContact1Id] ON [aiala].[Tasks] ([EmergencyContact1Id]);

GO

CREATE INDEX [IX_Tasks_EmergencyContact2Id] ON [aiala].[Tasks] ([EmergencyContact2Id]);

GO

ALTER TABLE [aiala].[Tasks] ADD CONSTRAINT [FK_Tasks_Accounts_EmergencyContact1Id] FOREIGN KEY ([EmergencyContact1Id]) REFERENCES [directory].[Accounts] ([Id]) ON DELETE NO ACTION;

GO

ALTER TABLE [aiala].[Tasks] ADD CONSTRAINT [FK_Tasks_Accounts_EmergencyContact2Id] FOREIGN KEY ([EmergencyContact2Id]) REFERENCES [directory].[Accounts] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190322124201_AddEmergencyContactsToTask', N'2.1.14-servicing-32113');

GO

ALTER TABLE [aiala].[ScheduledTasks] ADD [EmergencyContact1Id] uniqueidentifier NULL;

GO

ALTER TABLE [aiala].[ScheduledTasks] ADD [EmergencyContact2Id] uniqueidentifier NULL;

GO

ALTER TABLE [aiala].[ScheduledTasks] ADD [UseTaskContacts] bit NOT NULL DEFAULT 0;

GO

CREATE INDEX [IX_ScheduledTasks_EmergencyContact1Id] ON [aiala].[ScheduledTasks] ([EmergencyContact1Id]);

GO

CREATE INDEX [IX_ScheduledTasks_EmergencyContact2Id] ON [aiala].[ScheduledTasks] ([EmergencyContact2Id]);

GO

ALTER TABLE [aiala].[ScheduledTasks] ADD CONSTRAINT [FK_ScheduledTasks_Accounts_EmergencyContact1Id] FOREIGN KEY ([EmergencyContact1Id]) REFERENCES [directory].[Accounts] ([Id]) ON DELETE NO ACTION;

GO

ALTER TABLE [aiala].[ScheduledTasks] ADD CONSTRAINT [FK_ScheduledTasks_Accounts_EmergencyContact2Id] FOREIGN KEY ([EmergencyContact2Id]) REFERENCES [directory].[Accounts] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190322155553_AddEmergencyContactToSchedule', N'2.1.14-servicing-32113');

GO

ALTER TABLE [aiala].[Activities] ADD [ActiveStepId] uniqueidentifier NULL;

GO

CREATE INDEX [IX_Activities_ActiveStepId] ON [aiala].[Activities] ([ActiveStepId]);

GO

ALTER TABLE [aiala].[Activities] ADD CONSTRAINT [FK_Activities_ScheduledSteps_ActiveStepId] FOREIGN KEY ([ActiveStepId]) REFERENCES [aiala].[ScheduledSteps] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190327145304_AddActiveStepToActivity', N'2.1.14-servicing-32113');

GO

ALTER TABLE [aiala].[ScheduledTasks] ADD [ExpirationOffset] time NOT NULL DEFAULT '00:00:00';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190401084842_AddTaskExpirationOffset', N'2.1.14-servicing-32113');

GO

CREATE TABLE [aiala].[Pictures] (
    [Id] uniqueidentifier NOT NULL,
    [Description] nvarchar(max) NULL,
    [LastModifiedAt] datetimeoffset NOT NULL,
    CONSTRAINT [PK_Pictures] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [aiala].[PicturesTags] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [Confidence] real NOT NULL,
    [PictureId] uniqueidentifier NULL,
    CONSTRAINT [PK_PicturesTags] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PicturesTags_Pictures_PictureId] FOREIGN KEY ([PictureId]) REFERENCES [aiala].[Pictures] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_PicturesTags_PictureId] ON [aiala].[PicturesTags] ([PictureId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190411094208_AddPictures', N'2.1.14-servicing-32113');

GO

ALTER TABLE [aiala].[Pictures] ADD [PictureType] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [aiala].[Pictures] ADD [TenantId] uniqueidentifier NULL;

GO

ALTER TABLE [aiala].[Activities] ADD [PictureId] uniqueidentifier NULL;

GO

ALTER TABLE [aiala].[Activities] ADD [PictureId1] uniqueidentifier NULL;

GO

CREATE INDEX [IX_Pictures_TenantId] ON [aiala].[Pictures] ([TenantId]);

GO

CREATE INDEX [IX_Activities_PictureId] ON [aiala].[Activities] ([PictureId]);

GO

CREATE INDEX [IX_Activities_PictureId1] ON [aiala].[Activities] ([PictureId1]);

GO

ALTER TABLE [aiala].[Activities] ADD CONSTRAINT [FK_Activities_Pictures_PictureId] FOREIGN KEY ([PictureId]) REFERENCES [aiala].[Pictures] ([Id]) ON DELETE NO ACTION;

GO

ALTER TABLE [aiala].[Activities] ADD CONSTRAINT [FK_Activities_Pictures_PictureId1] FOREIGN KEY ([PictureId1]) REFERENCES [aiala].[Pictures] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [aiala].[Pictures] ADD CONSTRAINT [FK_Pictures_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [directory].[Tenants] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190411131546_AddPictureActivities', N'2.1.14-servicing-32113');

GO

ALTER TABLE [aiala].[Pictures] ADD [DescriptionConfidence] real NOT NULL DEFAULT CAST(0 AS real);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190411152239_AddDescriptionConfidence', N'2.1.14-servicing-32113');

GO

ALTER TABLE [aiala].[Activities] DROP CONSTRAINT [FK_Activities_Pictures_PictureId];

GO

ALTER TABLE [aiala].[Activities] DROP CONSTRAINT [FK_Activities_Pictures_PictureId1];

GO

ALTER TABLE [directory].[Accounts] DROP CONSTRAINT [FK_Accounts_Invitations_InvitationId];

GO

ALTER TABLE [directory].[Accounts] DROP CONSTRAINT [FK_Accounts_Users_UserId];

GO

DROP TABLE [directory].[Invitations];

GO

DROP INDEX [IX_Accounts_InvitationId] ON [directory].[Accounts];

GO

DROP INDEX [IX_Activities_PictureId1] ON [aiala].[Activities];

GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[directory].[Accounts]') AND [c].[name] = N'Email');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [directory].[Accounts] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [directory].[Accounts] DROP COLUMN [Email];

GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[aiala].[Activities]') AND [c].[name] = N'PictureId1');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [aiala].[Activities] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [aiala].[Activities] DROP COLUMN [PictureId1];

GO

EXEC sp_rename N'[directory].[Registrations].[UserId]', N'ExternalUserId', N'COLUMN';

GO

EXEC sp_rename N'[directory].[Accounts].[Lastname]', N'Invitation_Message', N'COLUMN';

GO

EXEC sp_rename N'[directory].[Accounts].[InvitationId]', N'Invitation_InviterId', N'COLUMN';

GO

EXEC sp_rename N'[directory].[Accounts].[Firstname]', N'Invitation_ConfirmationToken', N'COLUMN';

GO

ALTER TABLE [directory].[Users] ADD [Culture] nvarchar(10) NULL;

GO

ALTER TABLE [directory].[Tenants] ADD [Culture] nvarchar(10) NULL;

GO

ALTER TABLE [directory].[Tenants] ADD [Region] nvarchar(100) NULL;

GO

DROP INDEX [IX_Accounts_UserId] ON [directory].[Accounts];
DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[directory].[Accounts]') AND [c].[name] = N'UserId');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [directory].[Accounts] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [directory].[Accounts] ALTER COLUMN [UserId] uniqueidentifier NOT NULL;
CREATE INDEX [IX_Accounts_UserId] ON [directory].[Accounts] ([UserId]);

GO

ALTER TABLE [directory].[Accounts] ADD [Invitation_Accepted] datetimeoffset NULL;

GO

ALTER TABLE [directory].[Accounts] ADD [Invitation_Created] datetimeoffset NULL;

GO

ALTER TABLE [directory].[Accounts] ADD [Invitation_Declined] datetimeoffset NULL;

GO

ALTER TABLE [directory].[Accounts] ADD [Invitation_Resent] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [directory].[Accounts] ADD [Invitation_ValidUntil] datetimeoffset NULL;

GO

ALTER TABLE [directory].[Accounts] ADD [Invitation_Status] AS CAST
                    (
                    CASE
                        WHEN [Invitation_Declined] IS NOT NULL THEN 3
                        WHEN [Invitation_Accepted] IS NOT NULL THEN 4
                        WHEN [Invitation_ValidUntil] IS NOT NULL AND [Invitation_ValidUntil] < GETUTCDATE() THEN 2
                        WHEN [Invitation_Created] IS NOT NULL THEN 1
                        ELSE 0
                    END AS INT
                    );

GO

ALTER TABLE [aiala].[Activities] ADD CONSTRAINT [FK_Activities_Pictures_PictureId] FOREIGN KEY ([PictureId]) REFERENCES [aiala].[Pictures] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [directory].[Accounts] ADD CONSTRAINT [FK_Accounts_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [directory].[Users] ([Id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190423093642_UpdateDirectoryAgain', N'2.1.14-servicing-32113');

GO

ALTER TABLE [aiala].[Pictures] ADD [StorageDirectLink] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190424095846_AddStorageDirectLinkToPicture', N'2.1.14-servicing-32113');

GO

EXEC sp_rename N'[aiala].[Pictures].[LastModifiedAt]', N'CreatedAt', N'COLUMN';

GO

ALTER TABLE [aiala].[PicturesTags] ADD [HasHumanConfidence] bit NOT NULL DEFAULT 0;

GO

ALTER TABLE [aiala].[Pictures] ADD [HasHumanConfidence] bit NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190425132704_UpdatePictureAddHumanConfidence', N'2.1.14-servicing-32113');

GO

DROP TABLE [aiala].[PicturesTags];

GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[aiala].[Pictures]') AND [c].[name] = N'Description');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [aiala].[Pictures] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [aiala].[Pictures] DROP COLUMN [Description];

GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[aiala].[Pictures]') AND [c].[name] = N'DescriptionConfidence');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [aiala].[Pictures] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [aiala].[Pictures] DROP COLUMN [DescriptionConfidence];

GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[aiala].[Pictures]') AND [c].[name] = N'HasHumanConfidence');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [aiala].[Pictures] DROP CONSTRAINT [' + @var17 + '];');
ALTER TABLE [aiala].[Pictures] DROP COLUMN [HasHumanConfidence];

GO

ALTER TABLE [directory].[Countries] ADD [CallingCode] nvarchar(max) NULL;

GO

DECLARE @var18 sysname;
SELECT @var18 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[aiala].[ScheduledPlace]') AND [c].[name] = N'PictureId');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [aiala].[ScheduledPlace] DROP CONSTRAINT [' + @var18 + '];');
ALTER TABLE [aiala].[ScheduledPlace] ALTER COLUMN [PictureId] uniqueidentifier NULL;

GO

DECLARE @var19 sysname;
SELECT @var19 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[aiala].[Places]') AND [c].[name] = N'PictureId');
IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [aiala].[Places] DROP CONSTRAINT [' + @var19 + '];');
ALTER TABLE [aiala].[Places] ALTER COLUMN [PictureId] uniqueidentifier NULL;

GO

CREATE TABLE [aiala].[AiPictureMetadatas] (
    [Id] uniqueidentifier NOT NULL,
    [Description] nvarchar(max) NULL,
    [HasHumanConfidence] bit NOT NULL,
    [DescriptionConfidence] real NOT NULL,
    [PictureId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AiPictureMetadatas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AiPictureMetadatas_Pictures_PictureId] FOREIGN KEY ([PictureId]) REFERENCES [aiala].[Pictures] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [aiala].[AiPictureTags] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [HasHumanConfidence] bit NOT NULL,
    [Confidence] real NOT NULL,
    [PictureId] uniqueidentifier NULL,
    CONSTRAINT [PK_AiPictureTags] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AiPictureTags_AiPictureMetadatas_PictureId] FOREIGN KEY ([PictureId]) REFERENCES [aiala].[AiPictureMetadatas] ([Id]) ON DELETE NO ACTION
);

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CallingCode', N'Code', N'Enabled', N'Name') AND [object_id] = OBJECT_ID(N'[directory].[Countries]'))
    SET IDENTITY_INSERT [directory].[Countries] ON;
INSERT INTO [directory].[Countries] ([Id], [CallingCode], [Code], [Enabled], [Name])
VALUES ('44e996a7-fb2c-41bf-917f-02f3dd79f639', N'+93', N'AF', 1, N'Afghanistan'),
('c41c146e-66c6-4b79-83bc-d18117231f31', N'+687', N'NC', 1, N'New Caledonia'),
('7659bcc4-41cf-4846-bab8-2bae6f938359', N'+64', N'NZ', 1, N'New Zealand'),
('2a032347-4030-4119-babc-95e1da3e8e16', N'+505', N'NI', 1, N'Nicaragua'),
('c00c5c0b-c6be-4d27-b4db-302443d57439', N'+227', N'NE', 1, N'Niger'),
('a53ee786-5790-445b-95cc-cdf563cb599e', N'+234', N'NG', 1, N'Nigeria'),
('aef0117a-7970-4a60-9fc1-279c8c8b6eb6', N'+683', N'NU', 1, N'Niue'),
('158eb17f-f08d-42cc-95a4-057f7133ae51', N'+672', N'NF', 1, N'Norfolk Island'),
('c50719ef-6ccb-41d7-9244-5c82dde5e804', N'+850', N'KP', 1, N'Korea (Democratic People''s Republic of)'),
('1b6384c7-92a9-42b3-b996-c0b134ab1170', N'+1670', N'MP', 1, N'Northern Mariana Islands'),
('3abddad7-fa8f-456b-a590-33c5083b98dd', N'+47', N'NO', 1, N'Norway'),
('be94fb67-a08c-419f-a9bc-dda56d4b5096', N'+968', N'OM', 1, N'Oman'),
('5c62dbd2-c02e-4ae9-8b14-031c6c874659', N'+92', N'PK', 1, N'Pakistan'),
('cc76e0b5-40d7-40cc-ac59-c6dcd1b29cf0', N'+680', N'PW', 1, N'Palau'),
('e6e85a31-ae53-43ee-b21a-f6ed0538b020', N'+970', N'PS', 1, N'Palestine, State of'),
('64197b44-3373-4b0f-ada8-64a0066029ff', N'+507', N'PA', 1, N'Panama'),
('da4bd7d8-23a5-4a40-804f-e9a67cfa93a6', N'+675', N'PG', 1, N'Papua New Guinea'),
('c5f5f3ee-f4e1-4569-8e92-58a2b4a569cb', N'+595', N'PY', 1, N'Paraguay'),
('994c8be2-8fa3-43ee-a725-41895002f499', N'+51', N'PE', 1, N'Peru'),
('04549a74-b458-4bc6-94de-d339d5ed12e1', N'+63', N'PH', 1, N'Philippines'),
('7fbe832c-04f2-4f09-932c-af642d295032', N'+64', N'PN', 1, N'Pitcairn'),
('c92e3b7f-e7aa-4b23-bd38-8ef653b7f7a2', N'+48', N'PL', 1, N'Poland'),
('1855fe95-33ad-4a68-bcc3-c57ed85ce329', N'+351', N'pt', 1, N'Portugal'),
('ac050c4f-a0bc-4e0d-88ca-2853b9438d85', N'+1787', N'PR', 1, N'Puerto Rico'),
('ac714d22-4b36-4776-b842-728d72319087', N'+974', N'QA', 1, N'Qatar'),
('d55e3ff4-05e5-41f3-9e63-7a330f920950', N'+383', N'XK', 1, N'Republic of Kosovo'),
('7401ab85-99a8-40bd-b5b0-c4d94d25aa69', N'+262', N'RE', 1, N'Réunion'),
('1d27b72f-31d6-4568-86cc-7b6ae7e206a3', N'+40', N'RO', 1, N'Romania'),
('c8ac2c56-36fd-40f2-9ea9-6974f2387a0e', N'+31', N'nl', 1, N'Netherlands'),
('302ecafb-2dcb-466c-ab01-cf12d7b1544e', N'+7', N'RU', 1, N'Russian Federation'),
('15d6f9ea-1026-4ad3-88f8-f3e8f1eb6f6e', N'+977', N'NP', 1, N'Nepal'),
('ecd1a960-7846-4599-b78c-ae0bbfbf8f54', N'+264', N'NA', 1, N'Namibia'),
('c4fb5e61-c37f-4e5f-b70b-65c7ff3a24b8', N'+218', N'LY', 1, N'Libya'),
('2a7a9ede-fcf3-4455-9cd2-daaf2f07851d', N'+423', N'LI', 1, N'Liechtenstein'),
('08051e14-d13e-4e9e-90a6-0f3c4fc7b6d8', N'+370', N'LT', 1, N'Lithuania'),
('363ae089-3a30-478d-b49f-5c7b9bdafe50', N'+352', N'LU', 1, N'Luxembourg'),
('0e14972b-4f7c-4d6d-938b-ca6360b0872b', N'+853', N'MO', 1, N'Macao'),
('39035db4-10a0-4070-9fa6-b703a9a79199', N'+389', N'MK', 1, N'Macedonia (the former Yugoslav Republic of)'),
('603218e7-8773-44ef-a1de-34b7d5d7b21e', N'+261', N'MG', 1, N'Madagascar'),
('b07d1ada-7a09-484e-aa77-97ce1b769ac7', N'+265', N'MW', 1, N'Malawi'),
('c895cb3b-0be9-44e5-8bd9-3c5ddbeac938', N'+60', N'MY', 1, N'Malaysia'),
('39dcf65d-b256-4c48-9ffe-2cfd92d9adb1', N'+960', N'MV', 1, N'Maldives'),
('d6ecbf46-a0a5-4e61-89f9-c9acd0782532', N'+223', N'ML', 1, N'Mali'),
('dc48c0ec-8c3b-4d06-9283-935fa5453730', N'+356', N'MT', 1, N'Malta'),
('c2195d27-4ca3-46f9-b289-8250e32dd019', N'+692', N'MH', 1, N'Marshall Islands'),
('cb7d0d8e-7612-44b6-a1d6-9ac33762a823', N'+596', N'MQ', 1, N'Martinique'),
('02778799-6b45-47be-923f-7a1cd2e287a2', N'+222', N'MR', 1, N'Mauritania'),
('76819034-2d0b-4296-bb93-4c61927f9d8d', N'+230', N'MU', 1, N'Mauritius'),
('9e974584-f073-415c-b535-9bf6f8f83fbc', N'+262', N'YT', 1, N'Mayotte'),
('23440164-15e7-4aa2-ba50-6699b81b31ff', N'+52', N'MX', 1, N'Mexico'),
('8328c807-7bb3-4c7e-82d8-d72b0c148204', N'+691', N'FM', 1, N'Micronesia (Federated States of)'),
('80ddc377-d6d3-40d0-aa85-b17907310d2b', N'+373', N'MD', 1, N'Moldova (Republic of)'),
('f7dcbb8a-90d6-4008-89f9-fa8cd241d66f', N'+377', N'MC', 1, N'Monaco'),
('2c691e17-979b-4192-a106-3ccc4da056a6', N'+976', N'MN', 1, N'Mongolia'),
('a2141dc6-5c4b-400f-bdb1-211ac5081954', N'+382', N'ME', 1, N'Montenegro'),
('0da60206-a3f5-4a18-a8b6-630e12719c82', N'+1664', N'MS', 1, N'Montserrat'),
('d8b3c897-69e1-4bd0-aecf-668b0dadffc7', N'+212', N'MA', 1, N'Morocco'),
('ce56bc91-b97f-4f14-8c02-7e4ec2c6b1be', N'+258', N'MZ', 1, N'Mozambique'),
('cbb6a6af-e1e0-4d6d-a908-58b8f130576e', N'+95', N'MM', 1, N'Myanmar'),
('8cce699e-b033-4b9b-902e-6483da7ca19a', N'+674', N'NR', 1, N'Nauru'),
('bdbdeb23-9199-4a6c-b0f5-33014965fe40', N'+250', N'RW', 1, N'Rwanda'),
('afc0c551-cec0-4999-a9fe-7f92710785ac', N'+590', N'BL', 1, N'Saint Barthélemy'),
('fcdc7d01-ea17-4ac8-b712-f778ee2de9f2', N'+290', N'SH', 1, N'Saint Helena, Ascension and Tristan da Cunha'),
('f1e7967a-0237-4ec2-95bc-f92fb9cf5537', N'+886', N'TW', 1, N'Taiwan'),
('e796d639-b073-4de5-b761-907ea7fc4ffc', N'+992', N'TJ', 1, N'Tajikistan'),
('22936236-016e-4c06-aff9-d33d56c7c901', N'+255', N'TZ', 1, N'Tanzania, United Republic of'),
('5198fbd1-17c2-4130-b3ba-377c37c893af', N'+66', N'TH', 1, N'Thailand'),
('431af217-da9c-49c0-862d-346c3f154807', N'+670', N'TL', 1, N'Timor-Leste'),
('91296619-d79b-4f9f-b0b1-5356f17bf26e', N'+228', N'TG', 1, N'Togo'),
('6022bae6-98fd-4aa6-8900-b747f6c35044', N'+690', N'TK', 1, N'Tokelau'),
('7945287d-cd87-4153-a98d-341a50f701bf', N'+676', N'TO', 1, N'Tonga'),
('68f56a68-74d2-4ff9-99cb-a946d96edd1b', N'+1868', N'TT', 1, N'Trinidad and Tobago'),
('6a1bb25b-a7c8-4a72-bbfa-e3e7f0a7112b', N'+216', N'TN', 1, N'Tunisia'),
('7b1130ba-25c5-4886-8683-9955f720c67e', N'+90', N'TR', 1, N'Turkey'),
('6aa8a2e4-5b28-4fc0-a1ad-e80ad2426ede', N'+993', N'TM', 1, N'Turkmenistan'),
('e445207e-d79a-40b1-a108-3f6f8a37d503', N'+1649', N'TC', 1, N'Turks and Caicos Islands'),
('1eb5343a-b090-4efc-a684-ba5d5a613516', N'+688', N'TV', 1, N'Tuvalu'),
('d2fc318d-ef9a-49d3-8db3-1264b8415728', N'+256', N'UG', 1, N'Uganda'),
('bb691dd6-b2e1-4a61-8329-58f233d66d3a', N'+380', N'UA', 1, N'Ukraine'),
('54d6b82d-49da-44a5-8387-44eb555c8d44', N'+971', N'AE', 1, N'United Arab Emirates'),
('9f53e4c4-bc1e-4d26-b0a0-d1d589f9e443', N'+44', N'GB', 1, N'United Kingdom of Great Britain and Northern Ireland'),
('b54fac30-8dbd-4c34-921b-ecb780d45e05', N'+1', N'US', 1, N'United States of America'),
('05e5ab87-dc04-404e-925d-ac6c90d82427', N'+598', N'UY', 1, N'Uruguay'),
('83e157d8-1021-4d51-9a4a-2d627e769456', N'+998', N'UZ', 1, N'Uzbekistan'),
('dd4b36b5-4499-48c7-ae89-b8f18c4b6fb9', N'+678', N'VU', 1, N'Vanuatu'),
('feb9d6d5-2327-4065-9cbc-a80d0b0ebc89', N'+58', N'VE', 1, N'Venezuela (Bolivarian Republic of)'),
('c046fa0d-300b-4d38-b811-34aebae66b83', N'+84', N'VN', 1, N'Viet Nam'),
('7973277f-00e4-4518-be78-a811f244bed5', N'+681', N'WF', 1, N'Wallis and Futuna'),
('12d54c0e-6444-43fb-b27e-d82e319480e3', N'+212', N'EH', 1, N'Western Sahara'),
('8f532a74-0192-43ce-88e2-b57335b1daaf', N'+967', N'YE', 1, N'Yemen'),
('bc198476-8c8b-433e-80ad-c78d93b65758', N'+963', N'SY', 1, N'Syrian Arab Republic'),
('f7fd2d44-31cc-4b71-8afd-cedf532cc605', N'+41', N'CH', 1, N'Switzerland'),
('453ffcfa-6e29-41db-99a1-72d0067abf30', N'+46', N'SE', 1, N'Sweden'),
('900794cd-2b60-41da-97ae-c7bc082f9cf9', N'+268', N'SZ', 1, N'Swaziland'),
('fcb3d8e1-2a9c-470f-b7c5-15dea06477c9', N'+1869', N'KN', 1, N'Saint Kitts and Nevis'),
('e0be6610-da6e-4d3d-8699-8825fb4ee337', N'+1758', N'LC', 1, N'Saint Lucia'),
('bba868d5-0661-4421-8141-b13297e96218', N'+590', N'MF', 1, N'Saint Martin (French part)'),
('a0abe09e-395c-48d3-bef7-3f9282b1c60d', N'+508', N'PM', 1, N'Saint Pierre and Miquelon'),
('7f8afd92-61bd-477c-bf78-12d62b3e5cb5', N'+1784', N'VC', 1, N'Saint Vincent and the Grenadines'),
('64684184-0951-47e4-bac3-282314ccc2bd', N'+685', N'WS', 1, N'Samoa'),
('f59c01d4-367d-4773-8c4c-2b47e4f2e3a0', N'+378', N'SM', 1, N'San Marino'),
('c052a003-3341-442e-825b-fcbc3c208498', N'+239', N'ST', 1, N'Sao Tome and Principe'),
('2a7a8f6b-dda3-4799-8491-f4d03825899a', N'+966', N'SA', 1, N'Saudi Arabia'),
('c3cdf8e2-45c4-4f11-a91d-fa1a43bcd9f9', N'+221', N'SN', 1, N'Senegal'),
('f2fde10e-0e24-4a72-9f1f-2c248bae340c', N'+381', N'RS', 1, N'Serbia'),
('36a1b84a-849d-4fb1-ba49-8b8d85011554', N'+248', N'SC', 1, N'Seychelles'),
('142dac4e-e495-4c29-aaac-5aa6c2b2f630', N'+232', N'SL', 1, N'Sierra Leone'),
('c2473d84-7c6b-4b32-a624-77419bef5104', N'+231', N'LR', 1, N'Liberia'),
('ede02c3f-9b24-4d85-a0c7-021e5386c861', N'+65', N'SG', 1, N'Singapore'),
('efa9f8fb-92f8-46da-8171-b2947811b8cf', N'+421', N'SK', 1, N'Slovakia'),
('5ad56577-7098-4515-b010-06324670a069', N'+386', N'SI', 1, N'Slovenia'),
('871803b6-865a-4aee-8e4d-95189da19c50', N'+677', N'SB', 1, N'Solomon Islands'),
('6aeaa091-8c4b-47a9-94f0-6e2b9636bf72', N'+252', N'SO', 1, N'Somalia'),
('4474dd19-7cff-4df2-9de6-f1f52b9aa94c', N'+27', N'ZA', 1, N'South Africa'),
('3fdd5f8d-f594-41d3-aeb6-ffabcfe62f36', N'+500', N'GS', 1, N'South Georgia and the South Sandwich Islands'),
('8a951ed8-ca8c-4e8a-8232-d728d8a8a3e5', N'+82', N'KR', 1, N'Korea (Republic of)'),
('a9501973-f190-471f-b299-acd6085ccfbc', N'+211', N'SS', 1, N'South Sudan'),
('c6b48dac-ceaf-45f6-a71c-70dcb28e3dd3', N'+34', N'es', 1, N'Spain'),
('20b93646-a854-4cbc-9e45-f32cfe536f00', N'+94', N'LK', 1, N'Sri Lanka'),
('c355b129-8496-4f66-b39b-a05b1e140f24', N'+249', N'SD', 1, N'Sudan'),
('0b153c4a-481f-4cbb-a488-d8e71ea05ff3', N'+597', N'SR', 1, N'Suriname'),
('0516741a-702a-4c5a-9b7e-92f21356b336', N'+4779', N'SJ', 1, N'Svalbard and Jan Mayen'),
('0bf9b7ac-0e84-41bd-8e97-b81352e451f2', N'+1721', N'SX', 1, N'Sint Maarten (Dutch part)'),
('81a28038-c55d-4eca-8e68-668b2864fd98', N'+266', N'LS', 1, N'Lesotho'),
('743de50e-6fa1-4272-97e1-06bc862fe505', N'+961', N'LB', 1, N'Lebanon'),
('ae7b0ee4-e00f-436d-a9cc-55eb493a392f', N'+371', N'LV', 1, N'Latvia'),
('d83ca70f-d185-4c87-b82a-6cba91de7531', N'+', N'UM', 1, N'United States Minor Outlying Islands'),
('f2c63602-3db9-401f-a8bc-e352359dd77d', N'+1284', N'VG', 1, N'Virgin Islands (British)'),
('b21c14d5-09e3-4f22-952e-f739db8b2dc0', N'+1 340', N'VI', 1, N'Virgin Islands (U.S.)'),
('234e687b-9acf-4268-9d64-1b622f43e673', N'+673', N'BN', 1, N'Brunei Darussalam'),
('db9524e7-418a-47ca-a207-982af4c59666', N'+359', N'BG', 1, N'Bulgaria'),
('8216f3c7-2a58-42e8-acd4-308021850354', N'+226', N'BF', 1, N'Burkina Faso'),
('56aca540-4ae2-43cf-bdff-72ea9b868780', N'+257', N'BI', 1, N'Burundi'),
('9651830d-20e4-4222-aa36-1b596500670d', N'+855', N'KH', 1, N'Cambodia'),
('dda9bd73-0cb2-423d-adcc-ee0d393b22b5', N'+237', N'CM', 1, N'Cameroon'),
('f83ba14a-2758-45ef-93df-10170b0759c3', N'+1', N'CA', 1, N'Canada'),
('bf5f1f0f-9b12-41a4-a1aa-438c6a3af860', N'+238', N'CV', 1, N'Cabo Verde'),
('aa85fc2b-e3a5-4849-b869-724d61bc383e', N'+1345', N'KY', 1, N'Cayman Islands'),
('f30caf70-5235-4f4d-b300-c63efa33b1b1', N'+236', N'CF', 1, N'Central African Republic'),
('7cf19a97-a4f6-4a07-8dee-8175b1c3cd7d', N'+235', N'TD', 1, N'Chad'),
('668e3ca5-8bfb-4f8c-8c98-207e3889f0fe', N'+56', N'CL', 1, N'Chile'),
('8e6e14e7-d429-4837-bb2b-e84f6cc3299a', N'+86', N'CN', 1, N'China'),
('a349e604-c2d1-403f-82ec-01c41c26825e', N'+61', N'CX', 1, N'Christmas Island'),
('c7524e4a-6065-4fa6-a33b-215ae8f87e86', N'+61', N'CC', 1, N'Cocos (Keeling) Islands'),
('4cdcb59b-7021-4c86-8fa2-87b807460ad9', N'+57', N'CO', 1, N'Colombia'),
('50edd070-117c-4b0a-ad32-97607b3b4fb5', N'+269', N'KM', 1, N'Comoros'),
('0fe76a29-bd01-4578-b770-f1f51dbf7172', N'+242', N'CG', 1, N'Congo'),
('f28a2a31-b33e-410a-8418-5f84240a37d2', N'+243', N'CD', 1, N'Congo (Democratic Republic of the)'),
('47b942da-f582-4a85-9347-edf92e14ea86', N'+682', N'CK', 1, N'Cook Islands'),
('3fea9a2d-c0ef-40a3-8128-c589e95c3537', N'+506', N'CR', 1, N'Costa Rica'),
('9c22df3d-bbbf-4a8a-a0ef-b1720bc43463', N'+385', N'hr', 1, N'Croatia'),
('583cb448-7e2d-4c30-9835-343bbf977e9b', N'+53', N'CU', 1, N'Cuba'),
('31387bd8-b5d7-41c0-9807-d39d3a4453dc', N'+599', N'CW', 1, N'Curaçao'),
('2e38cb7e-7dc3-40a7-a947-533fd1134105', N'+246', N'IO', 1, N'British Indian Ocean Territory'),
('d6a33871-b015-4015-ac21-9a95ae8c89e5', N'+55', N'br', 1, N'Brazil'),
('9d19efbd-70e8-4817-afbb-fb735ff78ad7', N'+', N'BV', 1, N'Bouvet Island'),
('92414cfd-4ba3-469d-b951-3ff06eecffdc', N'+267', N'BW', 1, N'Botswana'),
('0b4f312f-3456-4cc9-ba85-acb48b9e7e9e', N'+358', N'AX', 1, N'Åland Islands'),
('7a551b34-cb89-40bd-820e-7bf0c5603536', N'+355', N'AL', 1, N'Albania'),
('0a1fadde-e103-40f3-94f8-ae58e70ee91d', N'+213', N'DZ', 1, N'Algeria'),
('f0b72653-ebba-457a-b38b-9a17762f6141', N'+1684', N'AS', 1, N'American Samoa'),
('c6d869a6-9592-465c-bb33-3635511aae8e', N'+376', N'AD', 1, N'Andorra'),
('d42a610c-d6a2-4376-b8fa-e0cc24998d96', N'+244', N'AO', 1, N'Angola'),
('cdede895-15cf-4b19-a518-8da0211f76b7', N'+1264', N'AI', 1, N'Anguilla'),
('a228d247-1f29-487b-8c88-a7af387391c6', N'+672', N'AQ', 1, N'Antarctica'),
('874ef0b2-ca3e-41ef-af1b-2aea2c0a5769', N'+1268', N'AG', 1, N'Antigua and Barbuda'),
('ca39433d-6984-4567-a717-3fc948ea8d52', N'+54', N'AR', 1, N'Argentina'),
('6eeb4317-ea6c-493d-9af0-d1976d7100de', N'+374', N'AM', 1, N'Armenia'),
('c64c69c8-9060-4251-852b-1f40c2df9124', N'+297', N'AW', 1, N'Aruba'),
('714c382f-db01-4fca-bdfe-f0a7b244236f', N'+61', N'AU', 1, N'Australia'),
('ed266508-e26e-4708-8d21-23154619c3fa', N'+357', N'CY', 1, N'Cyprus'),
('c75b1cfd-ebe2-4255-a6b6-28643f872152', N'+43', N'AT', 1, N'Austria'),
('34d69641-9b8b-4c79-9e14-7ad3f49a7314', N'+1242', N'BS', 1, N'Bahamas'),
('477d8fbc-e80a-489e-aba2-736293a699fb', N'+973', N'BH', 1, N'Bahrain'),
('1cc45f40-784c-43b2-9b88-70d8b3cec6c3', N'+880', N'BD', 1, N'Bangladesh'),
('ab43ea54-d4b6-4b56-bf9e-91f73f8516ea', N'+1246', N'BB', 1, N'Barbados'),
('a380394e-7990-4864-bca2-2832527192d6', N'+375', N'BY', 1, N'Belarus'),
('845bda9a-97c2-48ca-bb31-8f25ceb2bcab', N'+32', N'BE', 1, N'Belgium'),
('c978ac1e-7cfe-412b-a231-3195e0b31279', N'+501', N'BZ', 1, N'Belize'),
('54372766-53d0-4c1c-9784-71e056d9c7d9', N'+229', N'BJ', 1, N'Benin'),
('725a2cd5-cd9d-4f5f-9abc-e340f0b5e07a', N'+1441', N'BM', 1, N'Bermuda'),
('6c99985d-4372-4d21-97c4-822863b28cf9', N'+975', N'BT', 1, N'Bhutan'),
('5c0d4d2b-9905-4485-9fc0-d8858ab2d3a8', N'+591', N'BO', 1, N'Bolivia (Plurinational State of)'),
('290a4868-b537-41bf-b514-b36ddde77e32', N'+5997', N'BQ', 1, N'Bonaire, Sint Eustatius and Saba'),
('529b34dd-12f1-4b52-9270-bd122ea8cc5f', N'+387', N'BA', 1, N'Bosnia and Herzegovina'),
('dc9ffe90-bb21-4e89-aea2-267ce1d029fd', N'+994', N'AZ', 1, N'Azerbaijan'),
('77c07dae-8595-4c6a-870f-02d6461dd202', N'+260', N'ZM', 1, N'Zambia'),
('b89fdca4-86a7-487a-b647-d2defee26a13', N'+420', N'CZ', 1, N'Czech Republic'),
('ee76f5bd-7d77-440f-9aaf-186af4e13ba5', N'+253', N'DJ', 1, N'Djibouti'),
('4f0eb234-b1f0-4410-846c-6bb4bab31519', N'+592', N'GY', 1, N'Guyana'),
('7e9acba5-29d1-400c-b198-3d235bc0c9dc', N'+509', N'HT', 1, N'Haiti'),
('d9cf3244-5eb0-4f47-ae9c-e48b948ad068', N'+', N'HM', 1, N'Heard Island and McDonald Islands'),
('818d2dc0-a89a-4c46-85a9-4f363e084187', N'+379', N'VA', 1, N'Holy See'),
('bd2c6e92-2765-4dc3-b79e-8d7c0ac6fdd4', N'+504', N'HN', 1, N'Honduras'),
('ebf23b09-1151-4855-a3f6-e9fd51fef715', N'+852', N'HK', 1, N'Hong Kong'),
('78eb9514-06e7-493e-a495-f84c531a7791', N'+36', N'HU', 1, N'Hungary'),
('f202c2af-1dd0-4882-99f0-082cb5776b2a', N'+354', N'IS', 1, N'Iceland'),
('1c6c52d5-c467-47d4-a18a-84b367ba4311', N'+91', N'IN', 1, N'India'),
('88b339cf-a9c9-4e44-ab05-5c6e3cadcb6d', N'+62', N'id', 1, N'Indonesia'),
('fce81138-2079-4bd2-ac13-e9e08e486232', N'+225', N'CI', 1, N'Côte d''Ivoire'),
('e2a89a18-6f76-4318-a87d-6e1eb5d913d1', N'+98', N'IR', 1, N'Iran (Islamic Republic of)'),
('ab0bfe63-8589-412a-afd5-f3e105d20568', N'+964', N'IQ', 1, N'Iraq'),
('5a547753-2720-4ef8-9fd7-ffadd1036c72', N'+353', N'IE', 1, N'Ireland'),
('cdafe458-b575-4207-823a-8c28e6630c70', N'+44', N'IM', 1, N'Isle of Man'),
('c9c252be-8cfe-4f34-ba7d-3c8b11b551c7', N'+972', N'IL', 1, N'Israel'),
('d9a2e10d-8735-405e-9ea1-a00a7784dd56', N'+39', N'it', 1, N'Italy'),
('e8407507-47db-4672-89f0-6052fc97ac5b', N'+1876', N'JM', 1, N'Jamaica'),
('cf2b6264-a1fe-43a2-b9d0-f92f41a62da8', N'+81', N'JP', 1, N'Japan'),
('9343700a-a38e-44a9-b62f-6a069a17fdb6', N'+44', N'JE', 1, N'Jersey'),
('dacf22ad-417b-436f-a73f-59a47b2324b4', N'+962', N'JO', 1, N'Jordan'),
('ba3775f7-cb5c-4437-b1f2-ca7156560e53', N'+76', N'KZ', 1, N'Kazakhstan'),
('66e5ac2b-c3ee-49c3-a25a-437b3d918e91', N'+254', N'KE', 1, N'Kenya'),
('098ad535-c846-4473-8a6d-a89f1f762e93', N'+686', N'KI', 1, N'Kiribati'),
('92a086ad-64cc-4209-a1a7-c2002c1dfe69', N'+965', N'KW', 1, N'Kuwait'),
('2cdba5e4-4235-4c15-8eff-9dbbde91c8cc', N'+996', N'KG', 1, N'Kyrgyzstan'),
('a7e1fca7-dcc0-4873-9d1f-c170b5bf25a9', N'+856', N'LA', 1, N'Lao People''s Democratic Republic'),
('abddcd95-2dda-48e1-9e41-0503df5d489c', N'+245', N'GW', 1, N'Guinea-Bissau'),
('670cba88-5a11-42b9-9377-f49805633bf4', N'+224', N'GN', 1, N'Guinea'),
('c0fb1455-53a6-44e6-88dd-1809082063c7', N'+44', N'GG', 1, N'Guernsey'),
('61662609-003a-4097-95f8-6598bf26a4ef', N'+502', N'GT', 1, N'Guatemala'),
('4f720a85-1987-4d83-8739-b71595daaa8b', N'+1767', N'DM', 1, N'Dominica'),
('168ca0f0-7e9f-499f-96e8-4131fc0f70a6', N'+1809', N'DO', 1, N'Dominican Republic'),
('8a54bca0-807d-4cdf-8047-d317433757f5', N'+593', N'EC', 1, N'Ecuador'),
('41a606dd-4c52-438d-a9ec-df14591cbf3e', N'+20', N'EG', 1, N'Egypt'),
('11b4d693-dd81-4b9b-8c80-50a2fae110d3', N'+503', N'SV', 1, N'El Salvador'),
('fb4d2c5a-5f93-4130-a03a-6885d2ff906b', N'+240', N'GQ', 1, N'Equatorial Guinea'),
('0d41eff4-8d55-43e1-b94d-39631d7bfb54', N'+291', N'ER', 1, N'Eritrea'),
('d9bc2c41-5b53-473c-869c-6cab75c50a59', N'+372', N'EE', 1, N'Estonia'),
('b9fdfe8b-b370-4ffe-9160-d5a76af13a39', N'+251', N'ET', 1, N'Ethiopia'),
('c57a4fb1-b16e-4371-8553-e120064084f9', N'+500', N'FK', 1, N'Falkland Islands (Malvinas)'),
('495cc6a5-0016-44e1-8004-e7fbfd41a630', N'+298', N'FO', 1, N'Faroe Islands'),
('6e2cb01a-d6c3-4ca2-8b13-48b3367735d2', N'+679', N'FJ', 1, N'Fiji'),
('b4dde644-e38a-48ce-883e-0a9d8dd65758', N'+358', N'FI', 1, N'Finland'),
('f1ac9aeb-e891-4315-ac5e-5e9beebc3305', N'+45', N'DK', 1, N'Denmark'),
('cc7464a1-8300-450d-bd12-9e93c5307a09', N'+33', N'fr', 1, N'France'),
('95a16c33-9300-4336-bc0b-c2e02220885e', N'+689', N'PF', 1, N'French Polynesia'),
('fce57936-adc2-4a55-90be-b4c3476fd23e', N'+', N'TF', 1, N'French Southern Territories'),
('8c772dab-d696-403e-96ba-5d3c3284dfc1', N'+241', N'GA', 1, N'Gabon'),
('068f8b83-66a2-47ba-8285-7e815cf9c677', N'+220', N'GM', 1, N'Gambia'),
('fa1f6ca9-2c38-4ab6-b361-1af88e919ae5', N'+995', N'GE', 1, N'Georgia'),
('b04eb0d3-e4f6-4803-8bac-913ed58cd7d1', N'+49', N'de', 1, N'Germany'),
('cfe3c5b5-d629-4887-83f9-ea9e3ea32ae8', N'+233', N'GH', 1, N'Ghana'),
('0a25ab32-695f-4ecb-a47c-e0da577d36ee', N'+350', N'GI', 1, N'Gibraltar'),
('b7b1727a-e52f-45af-8a06-8337320eb960', N'+30', N'GR', 1, N'Greece'),
('8a8d21ac-da3d-47f0-8049-84fbbaeee508', N'+299', N'GL', 1, N'Greenland'),
('27e8ba9e-58fc-48a1-8e92-f3f8bc846557', N'+1473', N'GD', 1, N'Grenada'),
('4f3cbce7-8ab8-44e5-a58f-4b4a7da09f43', N'+590', N'GP', 1, N'Guadeloupe'),
('fbb1feed-b0d0-4a50-9e88-bb541fc56537', N'+1671', N'GU', 1, N'Guam'),
('c0eb726b-84d4-442d-bb98-4eb0f9d628b6', N'+594', N'GF', 1, N'French Guiana'),
('6c234af4-d025-4815-924e-1d98609128ae', N'+263', N'ZW', 1, N'Zimbabwe');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CallingCode', N'Code', N'Enabled', N'Name') AND [object_id] = OBJECT_ID(N'[directory].[Countries]'))
    SET IDENTITY_INSERT [directory].[Countries] OFF;

GO

CREATE INDEX [IX_Tasks_PictureId] ON [aiala].[Tasks] ([PictureId]);

GO

CREATE INDEX [IX_ScheduledTasks_PictureId] ON [aiala].[ScheduledTasks] ([PictureId]);

GO

CREATE INDEX [IX_ScheduledPlace_PictureId] ON [aiala].[ScheduledPlace] ([PictureId]);

GO

CREATE INDEX [IX_Places_PictureId] ON [aiala].[Places] ([PictureId]);

GO

CREATE UNIQUE INDEX [IX_AiPictureMetadatas_PictureId] ON [aiala].[AiPictureMetadatas] ([PictureId]);

GO

CREATE INDEX [IX_AiPictureTags_PictureId] ON [aiala].[AiPictureTags] ([PictureId]);

GO

UPDATE aiala.Places SET PictureId = NULL

GO

ALTER TABLE [aiala].[Places] ADD CONSTRAINT [FK_Places_Pictures_PictureId] FOREIGN KEY ([PictureId]) REFERENCES [aiala].[Pictures] ([Id]) ON DELETE NO ACTION;

GO

UPDATE aiala.ScheduledPlace SET PictureId = NULL

GO

ALTER TABLE [aiala].[ScheduledPlace] ADD CONSTRAINT [FK_ScheduledPlace_Pictures_PictureId] FOREIGN KEY ([PictureId]) REFERENCES [aiala].[Pictures] ([Id]) ON DELETE NO ACTION;

GO

UPDATE aiala.ScheduledTasks SET PictureId = NULL

GO

ALTER TABLE [aiala].[ScheduledTasks] ADD CONSTRAINT [FK_ScheduledTasks_Pictures_PictureId] FOREIGN KEY ([PictureId]) REFERENCES [aiala].[Pictures] ([Id]) ON DELETE NO ACTION;

GO

UPDATE aiala.Tasks SET PictureId = NULL

GO

ALTER TABLE [aiala].[Tasks] ADD CONSTRAINT [FK_Tasks_Pictures_PictureId] FOREIGN KEY ([PictureId]) REFERENCES [aiala].[Pictures] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190527155708_GeneralizePicture', N'2.1.14-servicing-32113');

GO

ALTER TABLE [aiala].[Activities] DROP CONSTRAINT [FK_Activities_Pictures_PictureId];

GO

ALTER TABLE [aiala].[Tasks] ADD [FreeFormPlace] nvarchar(max) NULL;

GO

ALTER TABLE [aiala].[Activities] ADD CONSTRAINT [FK_Activities_Pictures_PictureId] FOREIGN KEY ([PictureId]) REFERENCES [aiala].[Pictures] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190603154213_AddFreeFormPlace', N'2.1.14-servicing-32113');

GO

ALTER TABLE [aiala].[ScheduledTasks] ADD [FreeFormPlace] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190604071126_AddFreeFormPlaceToSchedule', N'2.1.14-servicing-32113');

GO

ALTER TABLE [directory].[Accounts] ADD [PictureId] uniqueidentifier NULL;

GO

CREATE INDEX [IX_Accounts_PictureId] ON [directory].[Accounts] ([PictureId]);

GO

ALTER TABLE [directory].[Accounts] ADD CONSTRAINT [FK_Accounts_Pictures_PictureId] FOREIGN KEY ([PictureId]) REFERENCES [aiala].[Pictures] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190611121617_AddPictureToAccount', N'2.1.14-servicing-32113');

GO


