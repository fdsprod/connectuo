CREATE TABLE [ClientProcess] (
    [ServerId] integer NOT NULL,
    [Pid] integer NOT NULL,
    CONSTRAINT [PK_ClientProcess] PRIMARY KEY ([ServerId], [Pid])
)
CREATE TABLE [Config] (
    [Key] nvarchar(255) PRIMARY KEY NOT NULL,
    [Value] nvarchar(255)
)
CREATE TABLE [LocalPatches] (
    [ShardId] integer NOT NULL,
    [PatchUrl] nvarchar(255) NOT NULL,
    [Version] integer NOT NULL,
    CONSTRAINT [PK_LocalPatches] PRIMARY KEY ([ShardId], [PatchUrl], [Version])
)
CREATE TABLE [Server] (
    [Id] integer PRIMARY KEY NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [Description] nvarchar(4000),
    [Url] nvarchar(255) NOT NULL,
    [Era] integer NOT NULL,
    [ShardType] integer NOT NULL,
    [Lang] integer NOT NULL,
    [RemoveEncryption] bit NOT NULL,
    [AllowRazor] bit NOT NULL,
    [HostAddress] nvarchar(255) NOT NULL,
    [Port] integer NOT NULL,
    [Status] integer NOT NULL,
    [AvgOnline] integer NOT NULL,
    [MaxOnline] integer NOT NULL,
    [CurOnline] integer NOT NULL,
    [UpTime] float NOT NULL,
    [Data] image,
    [ServerClientVersion] nvarchar(20),
    [HasPatches] bit NOT NULL,
    [Public] bit NOT NULL,
    [Favorite] bit NOT NULL
)
CREATE TABLE [ServerPatchState] (
    [ServerId] integer PRIMARY KEY NOT NULL,
    [Patching] bit NOT NULL
)
CREATE TABLE [ServerPatches] (
    [ShardId] int NOT NULL,
    [PatchUrl] varchar(255) NOT NULL,
    [Version] int NOT NULL,
    CONSTRAINT [PK_ServerPatches] PRIMARY KEY ([ShardId], [PatchUrl])
)