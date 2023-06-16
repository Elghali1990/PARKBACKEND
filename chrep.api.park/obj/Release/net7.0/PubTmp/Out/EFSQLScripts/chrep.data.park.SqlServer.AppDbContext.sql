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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514123354_initialCreate')
BEGIN
    CREATE TABLE [Roles] (
        [Id] int NOT NULL IDENTITY,
        [RoleName] nvarchar(max) NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514123354_initialCreate')
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(max) NULL,
        [LastName] nvarchar(max) NULL,
        [UserName] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [Phone] nvarchar(max) NULL,
        [Tocken] nvarchar(max) NULL,
        [UserTypeEnum] int NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514123354_initialCreate')
BEGIN
    CREATE TABLE [Vehicles] (
        [Id] int NOT NULL IDENTITY,
        [Matricule] nvarchar(max) NULL,
        [Marque] nvarchar(max) NULL,
        [Model] nvarchar(max) NULL,
        CONSTRAINT [PK_Vehicles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514123354_initialCreate')
BEGIN
    CREATE TABLE [UserRole] (
        [UserId] int NOT NULL,
        [RoleId] int NOT NULL,
        CONSTRAINT [PK_UserRole] PRIMARY KEY ([RoleId], [UserId]),
        CONSTRAINT [FK_UserRole_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UserRole_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514123354_initialCreate')
BEGIN
    CREATE INDEX [IX_UserRole_UserId] ON [UserRole] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514123354_initialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230514123354_initialCreate', N'7.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514152558_add_table_demande_userDemande')
BEGIN
    CREATE TABLE [Demandes] (
        [Id] int NOT NULL IDENTITY,
        [DateDemande] datetime2 NOT NULL,
        [Objet] nvarchar(max) NULL,
        [Detail] nvarchar(max) NULL,
        [DateDepart] datetime2 NULL,
        [HourDepart] time NULL,
        [DateBack] datetime2 NULL,
        [HourBack] time NULL,
        [Observation] nvarchar(max) NULL,
        [StatusEnum] int NOT NULL,
        [UserId] int NULL,
        CONSTRAINT [PK_Demandes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514152558_add_table_demande_userDemande')
BEGIN
    CREATE TABLE [UserDemande] (
        [DemandeId] int NOT NULL,
        [UserId] int NOT NULL,
        [DemandeOwner] nvarchar(max) NULL,
        CONSTRAINT [PK_UserDemande] PRIMARY KEY ([DemandeId], [UserId]),
        CONSTRAINT [FK_UserDemande_Demandes_DemandeId] FOREIGN KEY ([DemandeId]) REFERENCES [Demandes] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UserDemande_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514152558_add_table_demande_userDemande')
BEGIN
    CREATE INDEX [IX_UserDemande_UserId] ON [UserDemande] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514152558_add_table_demande_userDemande')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230514152558_add_table_demande_userDemande', N'7.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514171121_add_table_Mission_UserMission')
BEGIN
    CREATE TABLE [Missions] (
        [Id] int NOT NULL IDENTITY,
        [DateDepart] datetime2 NULL,
        [HourDepart] time NULL,
        [Instruction] nvarchar(max) NULL,
        [Observation] nvarchar(max) NULL,
        [ChauffeurId] int NULL,
        [ChauffeurName] nvarchar(max) NULL,
        [VehicleId] int NULL,
        [DemandeId] int NOT NULL,
        [MissionType] int NOT NULL,
        CONSTRAINT [PK_Missions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Missions_Demandes_DemandeId] FOREIGN KEY ([DemandeId]) REFERENCES [Demandes] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Missions_Vehicles_VehicleId] FOREIGN KEY ([VehicleId]) REFERENCES [Vehicles] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514171121_add_table_Mission_UserMission')
BEGIN
    CREATE TABLE [UserMission] (
        [MissionId] int NOT NULL,
        [UserId] int NOT NULL,
        [IsAbsent] bit NOT NULL,
        CONSTRAINT [PK_UserMission] PRIMARY KEY ([MissionId], [UserId]),
        CONSTRAINT [FK_UserMission_Missions_MissionId] FOREIGN KEY ([MissionId]) REFERENCES [Missions] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UserMission_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514171121_add_table_Mission_UserMission')
BEGIN
    CREATE INDEX [IX_Missions_DemandeId] ON [Missions] ([DemandeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514171121_add_table_Mission_UserMission')
BEGIN
    CREATE INDEX [IX_Missions_VehicleId] ON [Missions] ([VehicleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514171121_add_table_Mission_UserMission')
BEGIN
    CREATE INDEX [IX_UserMission_UserId] ON [UserMission] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230514171121_add_table_Mission_UserMission')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230514171121_add_table_Mission_UserMission', N'7.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230515091942_editTableVehicule')
BEGIN
    ALTER TABLE [Vehicles] ADD [TypeVehicule] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230515091942_editTableVehicule')
BEGIN
    EXEC(N'ALTER TABLE [Vehicles] ADD [Type_Matricule] AS [Matricule] + '', '' + [TypeVehicule]');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230515091942_editTableVehicule')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230515091942_editTableVehicule', N'7.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230515092214_editTableVehiculeSeparator')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Vehicles]') AND [c].[name] = N'Type_Matricule');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Vehicles] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Vehicles] DROP COLUMN [Type_Matricule];
    EXEC(N'ALTER TABLE [Vehicles] ADD [Type_Matricule] AS [Matricule] + ''-'' + [TypeVehicule]');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230515092214_editTableVehiculeSeparator')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230515092214_editTableVehiculeSeparator', N'7.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230515104214_addpasswordcolomntoUserTable')
BEGIN
    ALTER TABLE [Users] ADD [Password] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230515104214_addpasswordcolomntoUserTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230515104214_addpasswordcolomntoUserTable', N'7.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230609092552_addTableAbsence')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UserMission]') AND [c].[name] = N'IsAbsent');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [UserMission] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [UserMission] DROP COLUMN [IsAbsent];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230609092552_addTableAbsence')
BEGIN
    CREATE TABLE [Absences] (
        [Id] int NOT NULL IDENTITY,
        [IsAbsent] bit NOT NULL,
        [UserId] int NOT NULL,
        [MissionId] int NOT NULL,
        CONSTRAINT [PK_Absences] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Absences_Missions_MissionId] FOREIGN KEY ([MissionId]) REFERENCES [Missions] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Absences_Users_MissionId] FOREIGN KEY ([MissionId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230609092552_addTableAbsence')
BEGIN
    CREATE INDEX [IX_Absences_MissionId] ON [Absences] ([MissionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230609092552_addTableAbsence')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230609092552_addTableAbsence', N'7.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230609092938_addForeighkeyUserIdToTableAbsence')
BEGIN
    ALTER TABLE [Absences] DROP CONSTRAINT [FK_Absences_Users_MissionId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230609092938_addForeighkeyUserIdToTableAbsence')
BEGIN
    CREATE INDEX [IX_Absences_UserId] ON [Absences] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230609092938_addForeighkeyUserIdToTableAbsence')
BEGIN
    ALTER TABLE [Absences] ADD CONSTRAINT [FK_Absences_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230609092938_addForeighkeyUserIdToTableAbsence')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230609092938_addForeighkeyUserIdToTableAbsence', N'7.0.0');
END;
GO

COMMIT;
GO

