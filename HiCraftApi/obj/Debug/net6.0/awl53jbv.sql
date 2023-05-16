BEGIN TRANSACTION;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230515113816_hicraft')
BEGIN
    DELETE FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230515113816_hicraft';
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130636_Bioo')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'Bio');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Users] ALTER COLUMN [Bio] nvarchar(max) NOT NULL;
    ALTER TABLE [Users] ADD DEFAULT N'' FOR [Bio];
END;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130636_Bioo')
BEGIN
    DELETE FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230514130636_Bioo';
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    ALTER TABLE [Reviews] DROP CONSTRAINT [FK_Reviews_Users_ClientID];
END;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    ALTER TABLE [Reviews] DROP CONSTRAINT [FK_Reviews_Users_CraftsmanId];
END;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    DROP TABLE [ServiceRequests];
END;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    DROP INDEX [IX_Reviews_ClientID] ON [Reviews];
END;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    DROP INDEX [IX_Reviews_CraftsmanId] ON [Reviews];
END;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'Bio');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Users] DROP COLUMN [Bio];
END;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reviews]') AND [c].[name] = N'CraftsmanId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Reviews] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Reviews] DROP COLUMN [CraftsmanId];
END;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reviews]') AND [c].[name] = N'ClientID');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Reviews] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Reviews] ALTER COLUMN [ClientID] nvarchar(max) NOT NULL;
END;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    ALTER TABLE [Reviews] ADD [ClientNameId] nvarchar(450) NULL;
END;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    ALTER TABLE [Reviews] ADD [CraftManModelId] nvarchar(450) NULL;
END;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    CREATE INDEX [IX_Reviews_ClientNameId] ON [Reviews] ([ClientNameId]);
END;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    CREATE INDEX [IX_Reviews_CraftManModelId] ON [Reviews] ([CraftManModelId]);
END;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    ALTER TABLE [Reviews] ADD CONSTRAINT [FK_Reviews_Users_ClientNameId] FOREIGN KEY ([ClientNameId]) REFERENCES [Users] ([Id]);
END;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    ALTER TABLE [Reviews] ADD CONSTRAINT [FK_Reviews_Users_CraftManModelId] FOREIGN KEY ([CraftManModelId]) REFERENCES [Users] ([Id]);
END;
GO

IF EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514130436_Bio')
BEGIN
    DELETE FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230514130436_Bio';
END;
GO

COMMIT;
GO

