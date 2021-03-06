SET IDENTITY_INSERT [ids].[AspNetUsers] ON 
GO
INSERT [sts].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Enabled], [Culture]) VALUES (N'c902c28e-4782-4c5b-bb85-2edbe6ac75a3', N'developer@aiala.local', N'DEVELOPER@AIALA.LOCAL', N'developer@aiala.local', N'DEVELOPER@AIALA.LOCAL', 1, N'AQAAAAEAACcQAAAAELS0JbYRarhP1vG8h37K3aDcss3NAOZ+WKUuwzwcqiSU4Of1oOnUWFeBUyrruSwxJQ==', N'3KKTJL4F56LUKSR7FMKIWMKUZGCFBSCR', N'0181a596-e98c-4678-8d74-a5e1d15c7037', NULL, 0, 0, NULL, 1, 0, 1, N'en-US')
GO
SET IDENTITY_INSERT [ids].[AspNetUsers] OFF
GO
SET IDENTITY_INSERT [ids].[ApiResources] ON 
GO
INSERT [ids].[ApiResources] ([Id], [Enabled], [Name], [DisplayName], [Description], [Created], [LastAccessed], [NonEditable], [Updated]) VALUES (1, 1, N'msft.aiala.sts', N'STS Management API', N'API access for management of STS users, clients, resources, etc.', CAST(N'2019-04-10T14:37:04.0075969' AS DateTime2), NULL, 0, NULL)
GO
INSERT [ids].[ApiResources] ([Id], [Enabled], [Name], [DisplayName], [Description], [Created], [LastAccessed], [NonEditable], [Updated]) VALUES (2, 1, N'msft.aiala.portal.api', N'MSFT Portal API', N'', CAST(N'2019-04-10T14:40:42.7500000' AS DateTime2), CAST(N'2019-04-10T14:40:42.7500000' AS DateTime2), 0, CAST(N'2019-04-10T14:40:42.7500000' AS DateTime2))
GO
SET IDENTITY_INSERT [ids].[ApiResources] OFF
GO
SET IDENTITY_INSERT [ids].[ApiScopes] ON 
GO
INSERT [ids].[ApiScopes] ([Id], [Name], [DisplayName], [Description], [Required], [Emphasize], [ShowInDiscoveryDocument], [ApiResourceId]) VALUES (1, N'user-management', N'User Management', N'Required scope to manage users on the STS.', 1, 1, 1, 1)
GO
INSERT [ids].[ApiScopes] ([Id], [Name], [DisplayName], [Description], [Required], [Emphasize], [ShowInDiscoveryDocument], [ApiResourceId]) VALUES (2, N'client-management', N'Client Management', N'Required scope to manage clients for accessing the STS API.', 1, 1, 1, 1)
GO
INSERT [ids].[ApiScopes] ([Id], [Name], [DisplayName], [Description], [Required], [Emphasize], [ShowInDiscoveryDocument], [ApiResourceId]) VALUES (3, N'api-management', N'API Resource Management', N'Required scope to manage api resources.', 1, 1, 1, 1)
GO
INSERT [ids].[ApiScopes] ([Id], [Name], [DisplayName], [Description], [Required], [Emphasize], [ShowInDiscoveryDocument], [ApiResourceId]) VALUES (4, N'directory', N'Directory', N'Directory', 1, 1, 1, 2)
GO
SET IDENTITY_INSERT [ids].[ApiScopes] OFF
GO
SET IDENTITY_INSERT [ids].[ApiSecrets] ON 
GO
INSERT [ids].[ApiSecrets] ([Id], [Description], [Value], [Expiration], [Type], [ApiResourceId], [Created]) VALUES (1, NULL, N'J5hxJOoN6Z7+T7VdNyvp2yJ3c0JSUIy14i3datJoUio=', NULL, N'SharedSecret', 1, CAST(N'2019-04-10T14:37:04.0285298' AS DateTime2))
GO
INSERT [ids].[ApiSecrets] ([Id], [Description], [Value], [Expiration], [Type], [ApiResourceId], [Created]) VALUES (2, N'', N'Vj+jMlaKLgRnqhAgwa1OhdBSW1k79OtykUmZqg8zIdk=', CAST(N'3001-01-01T00:00:00.0000000' AS DateTime2), N'SharedSecret', 2, CAST(N'3001-01-01T00:00:00.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [ids].[ApiSecrets] OFF
GO
SET IDENTITY_INSERT [ids].[Clients] ON 
GO
INSERT [ids].[Clients] ([Id], [Enabled], [ClientId], [ProtocolType], [RequireClientSecret], [ClientName], [Description], [ClientUri], [LogoUri], [RequireConsent], [AllowRememberConsent], [AlwaysIncludeUserClaimsInIdToken], [RequirePkce], [AllowPlainTextPkce], [AllowAccessTokensViaBrowser], [FrontChannelLogoutUri], [FrontChannelLogoutSessionRequired], [BackChannelLogoutUri], [BackChannelLogoutSessionRequired], [AllowOfflineAccess], [IdentityTokenLifetime], [AccessTokenLifetime], [AuthorizationCodeLifetime], [ConsentLifetime], [AbsoluteRefreshTokenLifetime], [SlidingRefreshTokenLifetime], [RefreshTokenUsage], [UpdateAccessTokenClaimsOnRefresh], [RefreshTokenExpiration], [AccessTokenType], [EnableLocalLogin], [IncludeJwtId], [AlwaysSendClientClaims], [ClientClaimsPrefix], [PairWiseSubjectSalt], [Created], [DeviceCodeLifetime], [LastAccessed], [NonEditable], [Updated], [UserCodeType], [UserSsoLifetime]) VALUES (1, 1, N'sts.management', N'oidc', 1, N'STS Management Client', N'Client for management of STS users, clients, resources, etc.', NULL, NULL, 0, 1, 0, 0, 0, 0, NULL, 1, NULL, 1, 0, 300, 120, 300, NULL, 2592000, 1296000, 1, 0, 1, 1, 0, 0, 0, N'client_', NULL, CAST(N'2019-04-10T14:37:04.3621290' AS DateTime2), 300, NULL, 0, NULL, NULL, NULL)
GO
INSERT [ids].[Clients] ([Id], [Enabled], [ClientId], [ProtocolType], [RequireClientSecret], [ClientName], [Description], [ClientUri], [LogoUri], [RequireConsent], [AllowRememberConsent], [AlwaysIncludeUserClaimsInIdToken], [RequirePkce], [AllowPlainTextPkce], [AllowAccessTokensViaBrowser], [FrontChannelLogoutUri], [FrontChannelLogoutSessionRequired], [BackChannelLogoutUri], [BackChannelLogoutSessionRequired], [AllowOfflineAccess], [IdentityTokenLifetime], [AccessTokenLifetime], [AuthorizationCodeLifetime], [ConsentLifetime], [AbsoluteRefreshTokenLifetime], [SlidingRefreshTokenLifetime], [RefreshTokenUsage], [UpdateAccessTokenClaimsOnRefresh], [RefreshTokenExpiration], [AccessTokenType], [EnableLocalLogin], [IncludeJwtId], [AlwaysSendClientClaims], [ClientClaimsPrefix], [PairWiseSubjectSalt], [Created], [DeviceCodeLifetime], [LastAccessed], [NonEditable], [Updated], [UserCodeType], [UserSsoLifetime]) VALUES (2, 1, N'msft.aiala.webapp', N'oidc', 0, NULL, NULL, NULL, NULL, 0, 1, 1, 0, 1, 1, NULL, 1, NULL, 1, 1, 3600, 3600, 300, NULL, 2592000, 1296000, 1, 0, 1, 0, 1, 1, 0, N'client_', NULL, CAST(N'2019-04-10T14:40:42.8733333' AS DateTime2), 0, CAST(N'2019-04-10T14:40:42.8733333' AS DateTime2), 0, CAST(N'2019-04-10T14:40:42.8733333' AS DateTime2), NULL, NULL)
GO
INSERT [ids].[Clients] ([Id], [Enabled], [ClientId], [ProtocolType], [RequireClientSecret], [ClientName], [Description], [ClientUri], [LogoUri], [RequireConsent], [AllowRememberConsent], [AlwaysIncludeUserClaimsInIdToken], [RequirePkce], [AllowPlainTextPkce], [AllowAccessTokensViaBrowser], [FrontChannelLogoutUri], [FrontChannelLogoutSessionRequired], [BackChannelLogoutUri], [BackChannelLogoutSessionRequired], [AllowOfflineAccess], [IdentityTokenLifetime], [AccessTokenLifetime], [AuthorizationCodeLifetime], [ConsentLifetime], [AbsoluteRefreshTokenLifetime], [SlidingRefreshTokenLifetime], [RefreshTokenUsage], [UpdateAccessTokenClaimsOnRefresh], [RefreshTokenExpiration], [AccessTokenType], [EnableLocalLogin], [IncludeJwtId], [AlwaysSendClientClaims], [ClientClaimsPrefix], [PairWiseSubjectSalt], [Created], [DeviceCodeLifetime], [LastAccessed], [NonEditable], [Updated], [UserCodeType], [UserSsoLifetime]) VALUES (4, 1, N'aiala.mobile', N'oidc', 1, N'AIALA Mobile App', NULL, NULL, NULL, 0, 1, 0, 0, 0, 0, NULL, 1, NULL, 1, 1, 36000, 36000, 36000, 0, 259200, 1296000, 1, 0, 1, 0, 1, 0, 0, N'client_', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 300, NULL, 0, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [ids].[Clients] OFF
GO
SET IDENTITY_INSERT [ids].[ClientScopes] ON 
GO
INSERT [ids].[ClientScopes] ([Id], [Scope], [ClientId]) VALUES (1, N'user-management', 1)
GO
INSERT [ids].[ClientScopes] ([Id], [Scope], [ClientId]) VALUES (2, N'client-management', 1)
GO
INSERT [ids].[ClientScopes] ([Id], [Scope], [ClientId]) VALUES (3, N'api-management', 1)
GO
INSERT [ids].[ClientScopes] ([Id], [Scope], [ClientId]) VALUES (4, N'directory', 2)
GO
INSERT [ids].[ClientScopes] ([Id], [Scope], [ClientId]) VALUES (5, N'profile', 2)
GO
INSERT [ids].[ClientScopes] ([Id], [Scope], [ClientId]) VALUES (6, N'openid', 2)
GO
INSERT [ids].[ClientScopes] ([Id], [Scope], [ClientId]) VALUES (11, N'openid', 4)
GO
INSERT [ids].[ClientScopes] ([Id], [Scope], [ClientId]) VALUES (12, N'offline_access', 4)
GO
INSERT [ids].[ClientScopes] ([Id], [Scope], [ClientId]) VALUES (13, N'profile', 4)
GO
INSERT [ids].[ClientScopes] ([Id], [Scope], [ClientId]) VALUES (14, N'directory', 4)
GO
SET IDENTITY_INSERT [ids].[ClientScopes] OFF
GO
SET IDENTITY_INSERT [ids].[ClientSecrets] ON 
GO
INSERT [ids].[ClientSecrets] ([Id], [Description], [Value], [Expiration], [Type], [ClientId], [Created]) VALUES (1, NULL, N'WtDZkvtHxBmiXjwxosF73rzr7cH805nllrVwzDwfH8E=', NULL, N'SharedSecret', 1, CAST(N'2019-04-10T14:37:04.3625339' AS DateTime2))
GO
INSERT [ids].[ClientSecrets] ([Id], [Description], [Value], [Expiration], [Type], [ClientId], [Created]) VALUES (3, NULL, N'6Dy+NhLfVZY74D8ue7sqzpJNnpIXivSOkJgDx6WkAUg=', NULL, N'SharedSecret', 4, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [ids].[ClientSecrets] OFF
GO
SET IDENTITY_INSERT [ids].[ClientCorsOrigins] ON 
GO
INSERT [ids].[ClientCorsOrigins] ([Id], [Origin], [ClientId]) VALUES (1, N'http://localhost:4567', 2)
GO
SET IDENTITY_INSERT [ids].[ClientCorsOrigins] OFF
GO
SET IDENTITY_INSERT [ids].[ClientGrantTypes] ON 
GO
INSERT [ids].[ClientGrantTypes] ([Id], [GrantType], [ClientId]) VALUES (1, N'client_credentials', 1)
GO
INSERT [ids].[ClientGrantTypes] ([Id], [GrantType], [ClientId]) VALUES (2, N'implicit', 2)
GO
INSERT [ids].[ClientGrantTypes] ([Id], [GrantType], [ClientId]) VALUES (6, N'hybrid', 4)
GO
SET IDENTITY_INSERT [ids].[ClientGrantTypes] OFF
GO
SET IDENTITY_INSERT [ids].[ClientPostLogoutRedirectUris] ON 
GO
INSERT [ids].[ClientPostLogoutRedirectUris] ([Id], [PostLogoutRedirectUri], [ClientId]) VALUES (7, N'http://localhost:4567', 2)
GO
INSERT [ids].[ClientPostLogoutRedirectUris] ([Id], [PostLogoutRedirectUri], [ClientId]) VALUES (8, N'aiala.mobile://callback', 4)
GO
INSERT [ids].[ClientPostLogoutRedirectUris] ([Id], [PostLogoutRedirectUri], [ClientId]) VALUES (10, N'http://localhost:4567/portal/en-us', 2)
GO
SET IDENTITY_INSERT [ids].[ClientPostLogoutRedirectUris] OFF
GO
SET IDENTITY_INSERT [ids].[ClientRedirectUris] ON 
GO
INSERT [ids].[ClientRedirectUris] ([Id], [RedirectUri], [ClientId]) VALUES (8, N'http://localhost:4567', 2)
GO
INSERT [ids].[ClientRedirectUris] ([Id], [RedirectUri], [ClientId]) VALUES (9, N'aiala.mobile://callback', 4)
GO
GO
INSERT [ids].[ClientRedirectUris] ([Id], [RedirectUri], [ClientId]) VALUES (13, N'http://localhost:4567/silent_renew.html', 2)
GO
INSERT [ids].[ClientRedirectUris] ([Id], [RedirectUri], [ClientId]) VALUES (14, N'http://localhost:4567/portal/en-us', 2)
GO
SET IDENTITY_INSERT [ids].[ClientRedirectUris] OFF
GO
SET IDENTITY_INSERT [ids].[IdentityResources] ON 
GO
INSERT [ids].[IdentityResources] ([Id], [Enabled], [Name], [DisplayName], [Description], [Required], [Emphasize], [ShowInDiscoveryDocument], [Created], [NonEditable], [Updated]) VALUES (1, 1, N'openid', N'Your user identifier', NULL, 1, 0, 1, CAST(N'2019-04-10T14:37:03.1283809' AS DateTime2), 0, NULL)
GO
INSERT [ids].[IdentityResources] ([Id], [Enabled], [Name], [DisplayName], [Description], [Required], [Emphasize], [ShowInDiscoveryDocument], [Created], [NonEditable], [Updated]) VALUES (2, 1, N'profile', N'User profile', N'Your user profile information (first name, last name, etc.)', 0, 1, 1, CAST(N'2019-04-10T14:37:03.3052638' AS DateTime2), 0, NULL)
GO
INSERT [ids].[IdentityResources] ([Id], [Enabled], [Name], [DisplayName], [Description], [Required], [Emphasize], [ShowInDiscoveryDocument], [Created], [NonEditable], [Updated]) VALUES (3, 1, N'email', N'Your email address', NULL, 0, 1, 1, CAST(N'2019-04-10T14:37:03.3322567' AS DateTime2), 0, NULL)
GO
INSERT [ids].[IdentityResources] ([Id], [Enabled], [Name], [DisplayName], [Description], [Required], [Emphasize], [ShowInDiscoveryDocument], [Created], [NonEditable], [Updated]) VALUES (4, 1, N'phone', N'Your phone number', NULL, 0, 1, 1, CAST(N'2019-04-10T14:37:03.3572207' AS DateTime2), 0, NULL)
GO
INSERT [ids].[IdentityResources] ([Id], [Enabled], [Name], [DisplayName], [Description], [Required], [Emphasize], [ShowInDiscoveryDocument], [Created], [NonEditable], [Updated]) VALUES (5, 1, N'address', N'Your postal address', NULL, 0, 1, 1, CAST(N'2019-04-10T14:37:03.3784694' AS DateTime2), 0, NULL)
GO
SET IDENTITY_INSERT [ids].[IdentityResources] OFF
GO
SET IDENTITY_INSERT [ids].[IdentityClaims] ON 
GO
INSERT [ids].[IdentityClaims] ([Id], [Type], [IdentityResourceId]) VALUES (1, N'sub', 1)
GO
INSERT [ids].[IdentityClaims] ([Id], [Type], [IdentityResourceId]) VALUES (2, N'phone_number', 4)
GO
INSERT [ids].[IdentityClaims] ([Id], [Type], [IdentityResourceId]) VALUES (3, N'email_verified', 3)
GO
INSERT [ids].[IdentityClaims] ([Id], [Type], [IdentityResourceId]) VALUES (4, N'email', 3)
GO
INSERT [ids].[IdentityClaims] ([Id], [Type], [IdentityResourceId]) VALUES (5, N'updated_at', 2)
GO
INSERT [ids].[IdentityClaims] ([Id], [Type], [IdentityResourceId]) VALUES (6, N'locale', 2)
GO
INSERT [ids].[IdentityClaims] ([Id], [Type], [IdentityResourceId]) VALUES (7, N'zoneinfo', 2)
GO
INSERT [ids].[IdentityClaims] ([Id], [Type], [IdentityResourceId]) VALUES (8, N'birthdate', 2)
GO
INSERT [ids].[IdentityClaims] ([Id], [Type], [IdentityResourceId]) VALUES (9, N'gender', 2)
GO
INSERT [ids].[IdentityClaims] ([Id], [Type], [IdentityResourceId]) VALUES (10, N'website', 2)
GO
INSERT [ids].[IdentityClaims] ([Id], [Type], [IdentityResourceId]) VALUES (11, N'picture', 2)
GO
INSERT [ids].[IdentityClaims] ([Id], [Type], [IdentityResourceId]) VALUES (12, N'profile', 2)
GO
INSERT [ids].[IdentityClaims] ([Id], [Type], [IdentityResourceId]) VALUES (13, N'preferred_username', 2)
GO
INSERT [ids].[IdentityClaims] ([Id], [Type], [IdentityResourceId]) VALUES (14, N'nickname', 2)
GO
INSERT [ids].[IdentityClaims] ([Id], [Type], [IdentityResourceId]) VALUES (15, N'middle_name', 2)
GO
INSERT [ids].[IdentityClaims] ([Id], [Type], [IdentityResourceId]) VALUES (16, N'given_name', 2)
GO
INSERT [ids].[IdentityClaims] ([Id], [Type], [IdentityResourceId]) VALUES (17, N'family_name', 2)
GO
INSERT [ids].[IdentityClaims] ([Id], [Type], [IdentityResourceId]) VALUES (18, N'name', 2)
GO
INSERT [ids].[IdentityClaims] ([Id], [Type], [IdentityResourceId]) VALUES (19, N'phone_number_verified', 4)
GO
INSERT [ids].[IdentityClaims] ([Id], [Type], [IdentityResourceId]) VALUES (20, N'address', 5)
GO
SET IDENTITY_INSERT [ids].[IdentityClaims] OFF