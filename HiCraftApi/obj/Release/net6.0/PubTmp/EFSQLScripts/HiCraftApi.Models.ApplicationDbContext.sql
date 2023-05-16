IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324144210_Intial ')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324144210_Intial ')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [FirstName] nvarchar(50) NOT NULL,
        [LastName] nvarchar(50) NOT NULL,
        [Role] int NOT NULL,
        [ProfilePicture] varbinary(max) NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324144210_Intial ')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324144210_Intial ')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324144210_Intial ')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324144210_Intial ')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324144210_Intial ')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324144210_Intial ')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324144210_Intial ')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324144210_Intial ')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324144210_Intial ')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324144210_Intial ')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324144210_Intial ')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324144210_Intial ')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324144210_Intial ')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230324144210_Intial ', N'6.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [AspNetRoleClaims] DROP CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [AspNetUserClaims] DROP CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [AspNetUserLogins] DROP CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [AspNetUserTokens] DROP CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [AspNetUserTokens] DROP CONSTRAINT [PK_AspNetUserTokens];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [AspNetUsers] DROP CONSTRAINT [PK_AspNetUsers];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [PK_AspNetUserRoles];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [AspNetUserLogins] DROP CONSTRAINT [PK_AspNetUserLogins];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [AspNetUserClaims] DROP CONSTRAINT [PK_AspNetUserClaims];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [AspNetRoles] DROP CONSTRAINT [PK_AspNetRoles];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [AspNetRoleClaims] DROP CONSTRAINT [PK_AspNetRoleClaims];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    EXEC sp_rename N'[AspNetUserTokens]', N'UserToken';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    EXEC sp_rename N'[AspNetUsers]', N'Users';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    EXEC sp_rename N'[AspNetUserRoles]', N'UserRole';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    EXEC sp_rename N'[AspNetUserLogins]', N'UsersLogin';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    EXEC sp_rename N'[AspNetUserClaims]', N'UserClaim';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    EXEC sp_rename N'[AspNetRoles]', N'Roles';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    EXEC sp_rename N'[AspNetRoleClaims]', N'RoleClaim';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    EXEC sp_rename N'[UserRole].[IX_AspNetUserRoles_RoleId]', N'IX_UserRole_RoleId', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    EXEC sp_rename N'[UsersLogin].[IX_AspNetUserLogins_UserId]', N'IX_UsersLogin_UserId', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    EXEC sp_rename N'[UserClaim].[IX_AspNetUserClaims_UserId]', N'IX_UserClaim_UserId', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    EXEC sp_rename N'[RoleClaim].[IX_AspNetRoleClaims_RoleId]', N'IX_RoleClaim_RoleId', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [UserToken] ADD CONSTRAINT [PK_UserToken] PRIMARY KEY ([UserId], [LoginProvider], [Name]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [Users] ADD CONSTRAINT [PK_Users] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [UserRole] ADD CONSTRAINT [PK_UserRole] PRIMARY KEY ([UserId], [RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [UsersLogin] ADD CONSTRAINT [PK_UsersLogin] PRIMARY KEY ([LoginProvider], [ProviderKey]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [UserClaim] ADD CONSTRAINT [PK_UserClaim] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [Roles] ADD CONSTRAINT [PK_Roles] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [RoleClaim] ADD CONSTRAINT [PK_RoleClaim] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [RoleClaim] ADD CONSTRAINT [FK_RoleClaim_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [UserClaim] ADD CONSTRAINT [FK_UserClaim_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [UserRole] ADD CONSTRAINT [FK_UserRole_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [UserRole] ADD CONSTRAINT [FK_UserRole_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [UsersLogin] ADD CONSTRAINT [FK_UsersLogin_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    ALTER TABLE [UserToken] ADD CONSTRAINT [FK_UserToken_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230324145116_Rename Tabels')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230324145116_Rename Tabels', N'6.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230325112448_Add Roles')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'NormalizedName', N'ConcurrencyStamp') AND [object_id] = OBJECT_ID(N'[Roles]'))
        SET IDENTITY_INSERT [Roles] ON;
    EXEC(N'INSERT INTO [Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
    VALUES (N''9d4c41c3-7b69-426c-b7ce-2fa160d2a7a8'', N''Custmer'', N''CUSTMER'', N''f3b48c15-60fb-4be2-9bc7-23bc094c9d1a'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'NormalizedName', N'ConcurrencyStamp') AND [object_id] = OBJECT_ID(N'[Roles]'))
        SET IDENTITY_INSERT [Roles] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230325112448_Add Roles')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'NormalizedName', N'ConcurrencyStamp') AND [object_id] = OBJECT_ID(N'[Roles]'))
        SET IDENTITY_INSERT [Roles] ON;
    EXEC(N'INSERT INTO [Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
    VALUES (N''f4c94796-0081-4208-aa2e-2f199b64572f'', N''CraftMan'', N''CRAFTMAN'', N''cf5b6644-a55a-4f98-bd89-7d169fcc524f'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'NormalizedName', N'ConcurrencyStamp') AND [object_id] = OBJECT_ID(N'[Roles]'))
        SET IDENTITY_INSERT [Roles] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230325112448_Add Roles')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230325112448_Add Roles', N'6.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230326135142_add classes')
BEGIN
    ALTER TABLE [Users] ADD [CommentID] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230326135142_add classes')
BEGIN
    ALTER TABLE [Users] ADD [CraftmanId] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230326135142_add classes')
BEGIN
    ALTER TABLE [Users] ADD [Discriminator] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230326135142_add classes')
BEGIN
    ALTER TABLE [Users] ADD [Location] nvarchar(50) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230326135142_add classes')
BEGIN
    ALTER TABLE [Users] ADD [SpecializID] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230326135142_add classes')
BEGIN
    CREATE TABLE [Image] (
        [Id] uniqueidentifier NOT NULL,
        [Photo] varbinary(max) NOT NULL,
        [craftManid] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_Image] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Image_Users_craftManid] FOREIGN KEY ([craftManid]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230326135142_add classes')
BEGIN
    CREATE TABLE [Reviews] (
        [Id] int NOT NULL IDENTITY,
        [ClientID] nvarchar(max) NOT NULL,
        [ClientNameId] nvarchar(450) NULL,
        [Details] nvarchar(max) NOT NULL,
        [RateOFthisWork] real NOT NULL,
        [CraftManModelId] nvarchar(450) NULL,
        CONSTRAINT [PK_Reviews] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Reviews_Users_ClientNameId] FOREIGN KEY ([ClientNameId]) REFERENCES [Users] ([Id]),
        CONSTRAINT [FK_Reviews_Users_CraftManModelId] FOREIGN KEY ([CraftManModelId]) REFERENCES [Users] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230326135142_add classes')
BEGIN
    CREATE TABLE [Specializations] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_Specializations] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230326135142_add classes')
BEGIN
    CREATE INDEX [IX_Users_SpecializID] ON [Users] ([SpecializID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230326135142_add classes')
BEGIN
    CREATE INDEX [IX_Image_craftManid] ON [Image] ([craftManid]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230326135142_add classes')
BEGIN
    CREATE INDEX [IX_Reviews_ClientNameId] ON [Reviews] ([ClientNameId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230326135142_add classes')
BEGIN
    CREATE INDEX [IX_Reviews_CraftManModelId] ON [Reviews] ([CraftManModelId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230326135142_add classes')
BEGIN
    ALTER TABLE [Users] ADD CONSTRAINT [FK_Users_Specializations_SpecializID] FOREIGN KEY ([SpecializID]) REFERENCES [Specializations] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230326135142_add classes')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230326135142_add classes', N'6.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230326183425_add Imagesod past')
BEGIN
    DROP TABLE [Image];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230326183425_add Imagesod past')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'CraftmanId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Users] DROP COLUMN [CraftmanId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230326183425_add Imagesod past')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230326183425_add Imagesod past', N'6.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230326191919_realltion between the craftman and it''s specialization')
BEGIN
    ALTER TABLE [Specializations] ADD [CraftMenId] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230326191919_realltion between the craftman and it''s specialization')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230326191919_realltion between the craftman and it''s specialization', N'6.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230326192528_drop realltion between the craftman and it''s specialization')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Specializations]') AND [c].[name] = N'CraftMenId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Specializations] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Specializations] DROP COLUMN [CraftMenId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230326192528_drop realltion between the craftman and it''s specialization')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230326192528_drop realltion between the craftman and it''s specialization', N'6.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230327164649_ImagesOfPastWorks')
BEGIN
    ALTER TABLE [Users] ADD [ImagesOfPastWorksID] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230327164649_ImagesOfPastWorks')
BEGIN
    CREATE TABLE [ImageOfPastWork] (
        [Id] int NOT NULL IDENTITY,
        [Images] varbinary(max) NOT NULL,
        [CraftManId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_ImageOfPastWork] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ImageOfPastWork_Users_CraftManId] FOREIGN KEY ([CraftManId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230327164649_ImagesOfPastWorks')
BEGIN
    CREATE INDEX [IX_ImageOfPastWork_CraftManId] ON [ImageOfPastWork] ([CraftManId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230327164649_ImagesOfPastWorks')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230327164649_ImagesOfPastWorks', N'6.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430064800_las')
BEGIN
    ALTER TABLE [ImageOfPastWork] DROP CONSTRAINT [FK_ImageOfPastWork_Users_CraftManId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430064800_las')
BEGIN
    ALTER TABLE [ImageOfPastWork] DROP CONSTRAINT [PK_ImageOfPastWork];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430064800_las')
BEGIN
    EXEC sp_rename N'[ImageOfPastWork]', N'ImageOfPastWorks';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430064800_las')
BEGIN
    EXEC sp_rename N'[ImageOfPastWorks].[IX_ImageOfPastWork_CraftManId]', N'IX_ImageOfPastWorks_CraftManId', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430064800_las')
BEGIN
    ALTER TABLE [ImageOfPastWorks] ADD CONSTRAINT [PK_ImageOfPastWorks] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430064800_las')
BEGIN
    ALTER TABLE [ImageOfPastWorks] ADD CONSTRAINT [FK_ImageOfPastWorks_Users_CraftManId] FOREIGN KEY ([CraftManId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430064800_las')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230430064800_las', N'6.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430090645_password')
BEGIN
    CREATE TABLE [ResetPasswordTokens] (
        [Id] nvarchar(450) NOT NULL,
        [Token] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [ExpirationTime] datetime2 NOT NULL,
        CONSTRAINT [PK_ResetPasswordTokens] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230430090645_password')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230430090645_password', N'6.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    ALTER TABLE [Reviews] DROP CONSTRAINT [FK_Reviews_Users_ClientNameId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    ALTER TABLE [Reviews] DROP CONSTRAINT [FK_Reviews_Users_CraftManModelId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    DROP INDEX [IX_Reviews_ClientNameId] ON [Reviews];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    DROP INDEX [IX_Reviews_CraftManModelId] ON [Reviews];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reviews]') AND [c].[name] = N'ClientNameId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Reviews] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Reviews] DROP COLUMN [ClientNameId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reviews]') AND [c].[name] = N'CraftManModelId');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Reviews] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Reviews] DROP COLUMN [CraftManModelId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    ALTER TABLE [Users] ADD [Bio] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reviews]') AND [c].[name] = N'ClientID');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Reviews] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Reviews] ALTER COLUMN [ClientID] nvarchar(450) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    ALTER TABLE [Reviews] ADD [CraftsmanId] nvarchar(450) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    CREATE TABLE [ServiceRequests] (
        [Id] int NOT NULL IDENTITY,
        [CustomerId] nvarchar(450) NOT NULL,
        [CraftmanId] nvarchar(450) NOT NULL,
        [Request Details] nvarchar(max) NOT NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_ServiceRequests] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ServiceRequests_Users_CraftmanId] FOREIGN KEY ([CraftmanId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ServiceRequests_Users_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    CREATE INDEX [IX_Reviews_ClientID] ON [Reviews] ([ClientID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    CREATE INDEX [IX_Reviews_CraftsmanId] ON [Reviews] ([CraftsmanId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    CREATE INDEX [IX_ServiceRequests_CraftmanId] ON [ServiceRequests] ([CraftmanId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    CREATE INDEX [IX_ServiceRequests_CustomerId] ON [ServiceRequests] ([CustomerId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    ALTER TABLE [Reviews] ADD CONSTRAINT [FK_Reviews_Users_ClientID] FOREIGN KEY ([ClientID]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    ALTER TABLE [Reviews] ADD CONSTRAINT [FK_Reviews_Users_CraftsmanId] FOREIGN KEY ([CraftsmanId]) REFERENCES [Users] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230514130436_Bio', N'6.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130636_Bioo')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'Bio');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Users] ALTER COLUMN [Bio] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130636_Bioo')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230514130636_Bioo', N'6.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230515113816_hicraft')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230515113816_hicraft', N'6.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230515114637_addcolumnbio')
BEGIN
    EXEC sp_rename N'[Users].[Bio]', N'Bios', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230515114637_addcolumnbio')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230515114637_addcolumnbio', N'6.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516113909_remove some relationships')
BEGIN
    ALTER TABLE [ServiceRequests] DROP CONSTRAINT [FK_ServiceRequests_Users_CraftmanId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516113909_remove some relationships')
BEGIN
    ALTER TABLE [ServiceRequests] DROP CONSTRAINT [FK_ServiceRequests_Users_CustomerId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516113909_remove some relationships')
BEGIN
    DROP INDEX [IX_ServiceRequests_CraftmanId] ON [ServiceRequests];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516113909_remove some relationships')
BEGIN
    DROP INDEX [IX_ServiceRequests_CustomerId] ON [ServiceRequests];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516113909_remove some relationships')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ServiceRequests]') AND [c].[name] = N'CustomerId');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [ServiceRequests] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [ServiceRequests] ALTER COLUMN [CustomerId] nvarchar(max) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516113909_remove some relationships')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ServiceRequests]') AND [c].[name] = N'CraftmanId');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [ServiceRequests] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [ServiceRequests] ALTER COLUMN [CraftmanId] nvarchar(max) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516113909_remove some relationships')
BEGIN
    ALTER TABLE [ServiceRequests] ADD [CraftManModelId] nvarchar(450) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516113909_remove some relationships')
BEGIN
    ALTER TABLE [ServiceRequests] ADD [CustmerId] nvarchar(450) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516113909_remove some relationships')
BEGIN
    CREATE INDEX [IX_ServiceRequests_CraftManModelId] ON [ServiceRequests] ([CraftManModelId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516113909_remove some relationships')
BEGIN
    CREATE INDEX [IX_ServiceRequests_CustmerId] ON [ServiceRequests] ([CustmerId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516113909_remove some relationships')
BEGIN
    ALTER TABLE [ServiceRequests] ADD CONSTRAINT [FK_ServiceRequests_Users_CraftManModelId] FOREIGN KEY ([CraftManModelId]) REFERENCES [Users] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516113909_remove some relationships')
BEGIN
    ALTER TABLE [ServiceRequests] ADD CONSTRAINT [FK_ServiceRequests_Users_CustmerId] FOREIGN KEY ([CustmerId]) REFERENCES [Users] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516113909_remove some relationships')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230516113909_remove some relationships', N'6.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516114943_changes on review table')
BEGIN
    ALTER TABLE [Reviews] DROP CONSTRAINT [FK_Reviews_Users_ClientID];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516114943_changes on review table')
BEGIN
    ALTER TABLE [Reviews] DROP CONSTRAINT [FK_Reviews_Users_CraftsmanId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516114943_changes on review table')
BEGIN
    DROP INDEX [IX_Reviews_ClientID] ON [Reviews];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516114943_changes on review table')
BEGIN
    DROP INDEX [IX_Reviews_CraftsmanId] ON [Reviews];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516114943_changes on review table')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reviews]') AND [c].[name] = N'CraftsmanId');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Reviews] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Reviews] ALTER COLUMN [CraftsmanId] nvarchar(max) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516114943_changes on review table')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reviews]') AND [c].[name] = N'ClientID');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Reviews] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Reviews] ALTER COLUMN [ClientID] nvarchar(max) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516114943_changes on review table')
BEGIN
    ALTER TABLE [Reviews] ADD [CraftManModelId] nvarchar(450) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516114943_changes on review table')
BEGIN
    ALTER TABLE [Reviews] ADD [CustmerId] nvarchar(450) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516114943_changes on review table')
BEGIN
    CREATE INDEX [IX_Reviews_CraftManModelId] ON [Reviews] ([CraftManModelId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516114943_changes on review table')
BEGIN
    CREATE INDEX [IX_Reviews_CustmerId] ON [Reviews] ([CustmerId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516114943_changes on review table')
BEGIN
    ALTER TABLE [Reviews] ADD CONSTRAINT [FK_Reviews_Users_CraftManModelId] FOREIGN KEY ([CraftManModelId]) REFERENCES [Users] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516114943_changes on review table')
BEGIN
    ALTER TABLE [Reviews] ADD CONSTRAINT [FK_Reviews_Users_CustmerId] FOREIGN KEY ([CustmerId]) REFERENCES [Users] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516114943_changes on review table')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230516114943_changes on review table', N'6.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516142831_update review')
BEGIN
    EXEC sp_rename N'[Reviews].[CraftsmanId]', N'CraftmanId', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230516142831_update review')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230516142831_update review', N'6.0.0');
END;
GO

COMMIT;
GO

