INSERT [directory].[Tenants] ([Id], [Enabled], [Name], [TenantType], [Culture], [Region]) VALUES (N'802c7e28-8ffb-452c-b837-cf33aaca453a', 1, N'Randy McRandom', 20, N'en-US', NULL)
GO
INSERT [aiala].[Places] ([Id], [Name], [PictureId], [Latitude], [Longitude], [TenantId], [IsDeleted]) VALUES (N'20048a19-d552-4a55-a281-b6a28629ad41', N'Office', NULL, CAST(46.960337 AS Decimal(9, 6)), CAST(7.456875 AS Decimal(9, 6)), N'802c7e28-8ffb-452c-b837-cf33aaca453a', 0)
GO
INSERT [directory].[Users] ([Id], [ActiveAccountId], [ExternalUserId], [Firstname], [Lastname], [Email], [Culture]) VALUES (N'484c3aab-9a53-4796-a49a-ff3811c2f43b', N'da799e8e-1758-4e4b-a699-1a3946e432f4', N'c902c28e-4782-4c5b-bb85-2edbe6ac75a3', N'Randy', N'McRandom', N'developer@aiala.local', N'de')
GO
INSERT [directory].[Accounts] ([Id], [Enabled], [TenantId], [UserId], [Invitation_ConfirmationToken], [Invitation_InviterId], [Invitation_Message], [PhoneNumber], [Invitation_Accepted], [Invitation_Created], [Invitation_Declined], [Invitation_Resent], [Invitation_ValidUntil], [PictureId]) VALUES (N'da799e8e-1758-4e4b-a699-1a3946e432f4', 1, N'802c7e28-8ffb-452c-b837-cf33aaca453a', N'484c3aab-9a53-4796-a49a-ff3811c2f43b', NULL, NULL, NULL, NULL, CAST(N'2019-05-13T12:43:01.0802363+00:00' AS DateTimeOffset), NULL, NULL, 0, NULL, NULL)
GO
INSERT [aiala].[Tasks] ([Id], [Name], [Duration], [LastModified], [AuthorId], [TenantId], [PictureId], [PlaceId], [IsDeleted], [EmergencyContact1Id], [EmergencyContact2Id], [UseTaskContacts], [FreeFormPlace]) VALUES (N'3e46fe51-8b27-419c-9b8a-899079770a6a', N'Have Breakfast', CAST(N'00:30:00' AS Time), CAST(N'2019-05-13T12:46:33.4860643+00:00' AS DateTimeOffset), N'da799e8e-1758-4e4b-a699-1a3946e432f4', N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL, NULL, 0, NULL, NULL, 0, NULL)
GO
INSERT [aiala].[Steps] ([Id], [Text], [TaskId], [Order]) VALUES (N'868a1532-8ff2-4dc7-a7da-09a6170d8fe4', N'have breakfast', N'3e46fe51-8b27-419c-9b8a-899079770a6a', 2)
GO
INSERT [directory].[PermissionGroups] ([Id], [Name], [AppId], [TenantId]) VALUES (N'457a8ec6-969f-4aed-a41d-85ca68f37f52', N'Mobile App User', NULL, NULL)
GO
INSERT [directory].[PermissionGroups] ([Id], [Name], [AppId], [TenantId]) VALUES (N'be77a3dc-3504-40f1-9ddb-a58bed4f22f7', N'Web App User', NULL, NULL)
GO
INSERT [directory].[PermissionGroups] ([Id], [Name], [AppId], [TenantId]) VALUES (N'802c7e28-8ffb-452c-b837-cf33aaca453a', N'Administrator', NULL, NULL)
GO
INSERT [aiala].[DayTemplates] ([Id], [Name], [TenantId], [DayName]) VALUES (N'98652f03-f15e-4c32-91fa-2cc5f9e972a6', N'Regular schoolday', N'802c7e28-8ffb-452c-b837-cf33aaca453a', N'Schoolday')
GO
INSERT [directory].[PermissionAssignments] ([Id], [PermissionGroupId], [TenantId], [AccountId], [PermissionType], [ValidFrom], [ValidTo]) VALUES (N'3fa61f2c-7e43-4421-9b14-1378f3b5d611', N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL, NULL, N'aiala.Backend.Authorization.Permissions.ScheduleManagementPermission', NULL, NULL)
GO
INSERT [directory].[PermissionAssignments] ([Id], [PermissionGroupId], [TenantId], [AccountId], [PermissionType], [ValidFrom], [ValidTo]) VALUES (N'3f8a7287-e9da-4e36-bbc7-17f27a79694b', N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL, NULL, N'xappido.Directory.Permissions.InviteUsersPermission', NULL, NULL)
GO
INSERT [directory].[PermissionAssignments] ([Id], [PermissionGroupId], [TenantId], [AccountId], [PermissionType], [ValidFrom], [ValidTo]) VALUES (N'50811f1b-f409-4c35-8690-1e57ebe0e4d2', N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL, NULL, N'aiala.Backend.Authorization.Permissions.ScheduleConsumptionPermission', NULL, NULL)
GO
INSERT [directory].[PermissionAssignments] ([Id], [PermissionGroupId], [TenantId], [AccountId], [PermissionType], [ValidFrom], [ValidTo]) VALUES (N'fe094685-036b-4e29-8df6-3ba550590ea4', N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL, NULL, N'xappido.Directory.Permissions.ManageUsersPermission', NULL, NULL)
GO
INSERT [directory].[PermissionAssignments] ([Id], [PermissionGroupId], [TenantId], [AccountId], [PermissionType], [ValidFrom], [ValidTo]) VALUES (N'41880380-0d84-41d4-99bf-3cf453c15036', N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL, NULL, N'xappido.Directory.Permissions.ManageUsersPermission', NULL, NULL)
GO
INSERT [directory].[PermissionAssignments] ([Id], [PermissionGroupId], [TenantId], [AccountId], [PermissionType], [ValidFrom], [ValidTo]) VALUES (N'c92fa89c-11a2-45c8-afd1-4dc3e55cc2c7', N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL, NULL, N'aiala.Backend.Authorization.Permissions.GetSchedulePermission', NULL, NULL)
GO
INSERT [directory].[PermissionAssignments] ([Id], [PermissionGroupId], [TenantId], [AccountId], [PermissionType], [ValidFrom], [ValidTo]) VALUES (N'aeb20c25-f112-40e1-a793-4fa77d222732', N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL, NULL, N'xappido.Directory.Permissions.DeleteProfilePicturePermission', NULL, NULL)
GO
INSERT [directory].[PermissionAssignments] ([Id], [PermissionGroupId], [TenantId], [AccountId], [PermissionType], [ValidFrom], [ValidTo]) VALUES (N'bb3ce92f-99ae-4baa-90cc-5a8640435aad', N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL, NULL, N'xappido.Directory.Permissions.DeleteProfilePicturePermission', NULL, NULL)
GO
INSERT [directory].[PermissionAssignments] ([Id], [PermissionGroupId], [TenantId], [AccountId], [PermissionType], [ValidFrom], [ValidTo]) VALUES (N'aef8ed06-35c4-4fc5-88fb-5bdb16f6d744', N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL, NULL, N'aiala.Backend.Authorization.Permissions.ScheduleConsumptionPermission', NULL, NULL)
GO
INSERT [directory].[PermissionAssignments] ([Id], [PermissionGroupId], [TenantId], [AccountId], [PermissionType], [ValidFrom], [ValidTo]) VALUES (N'1217bfba-34cd-4ca0-b450-77381f05a7a6', N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL, NULL, N'xappido.Directory.Permissions.UploadProfilePicturePermission', NULL, NULL)
GO
INSERT [directory].[PermissionAssignments] ([Id], [PermissionGroupId], [TenantId], [AccountId], [PermissionType], [ValidFrom], [ValidTo]) VALUES (N'41d0d7c5-6131-4c63-8c1b-89955887aa92', N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL, NULL, N'xappido.Directory.Permissions.InviteUsersPermission', NULL, NULL)
GO
INSERT [directory].[PermissionAssignments] ([Id], [PermissionGroupId], [TenantId], [AccountId], [PermissionType], [ValidFrom], [ValidTo]) VALUES (N'0681fefd-341e-48e2-9871-9098e30b9f80', N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL, NULL, N'xappido.Directory.Permissions.UploadProfilePicturePermission', NULL, NULL)
GO
INSERT [directory].[PermissionAssignments] ([Id], [PermissionGroupId], [TenantId], [AccountId], [PermissionType], [ValidFrom], [ValidTo]) VALUES (N'c9665974-9a06-4f13-af91-9d2e4fe6ea21', N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL, NULL, N'aiala.Backend.Authorization.Permissions.ScheduleManagementPermission', NULL, NULL)
GO
INSERT [directory].[PermissionAssignments] ([Id], [PermissionGroupId], [TenantId], [AccountId], [PermissionType], [ValidFrom], [ValidTo]) VALUES (N'ebab3b8c-1f2c-4790-83a7-9da7c55ba707', N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL, NULL, N'xappido.Directory.Settings.Authorization.ManageTenantSettingsPermission', NULL, NULL)
GO
INSERT [directory].[PermissionAssignments] ([Id], [PermissionGroupId], [TenantId], [AccountId], [PermissionType], [ValidFrom], [ValidTo]) VALUES (N'd86c528b-9d7c-4dc7-b454-adb2787a1dfb', N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL, NULL, N'xappido.Directory.Permissions.ManageUsersAccessPermission', NULL, NULL)
GO
INSERT [directory].[PermissionAssignments] ([Id], [PermissionGroupId], [TenantId], [AccountId], [PermissionType], [ValidFrom], [ValidTo]) VALUES (N'04db63ad-b63d-4060-8950-b65d7c397103', N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL, NULL, N'xappido.Directory.Settings.Authorization.ManageTenantSettingsPermission', NULL, NULL)
GO
INSERT [directory].[PermissionAssignments] ([Id], [PermissionGroupId], [TenantId], [AccountId], [PermissionType], [ValidFrom], [ValidTo]) VALUES (N'fbec2639-c010-4681-91ea-db6760a16901', N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL, NULL, N'xappido.Directory.Permissions.ManageUsersAccessPermission', NULL, NULL)
GO
INSERT [directory].[PermissionAssignments] ([Id], [PermissionGroupId], [TenantId], [AccountId], [PermissionType], [ValidFrom], [ValidTo]) VALUES (N'cbf2a006-df5d-441c-ba83-fb183734f1b1', N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL, NULL, N'aiala.Backend.Authorization.Permissions.GetSchedulePermission', NULL, NULL)
GO
INSERT [directory].[Registrations] ([Id], [ExternalUserId], [CreatedAccountId], [CreatedSubscriptionId], [TenantId], [TenantType], [Email], [Created], [ApprovalRequired], [Approved], [ApprovedBy], [ApprovalToken], [Confirmed], [Completed], [SubscriptionTypeId], [ConfirmationToken], [Values], [Culture]) VALUES (N'8216494b-c01f-4d8f-ad24-bfdd20ba8acd', N'c902c28e-4782-4c5b-bb85-2edbe6ac75a3', N'da799e8e-1758-4e4b-a699-1a3946e432f4', NULL, NULL, 10, N'developer@aiala.local', CAST(N'2019-05-13T12:41:18.7300089+00:00' AS DateTimeOffset), 0, NULL, NULL, NULL, CAST(N'2019-05-13T12:43:00.7471202+00:00' AS DateTimeOffset), CAST(N'2019-05-13T12:43:01.0802445+00:00' AS DateTimeOffset), NULL, N'CfDJ8KmILaGQ5BdJoEJpjmArYDF3wl/KPHFHYmGlqykqKf40WC1Fcb2c9LzXukUb6SJLEHTwqZzy+CEsayuu56WCnrTNWx11nQq6vXbSgym3EK40YF+9s0EEuV3F+l0xm0FvkrWfjXQohHkLEKkitOIbS/S2W6qIwXxA3a7tiPWyzBDpqrBJ02+tIkzNudGjz05c1o+AcTK45Zwxh2MgRK0YW1HNNpeLIyaVg4Mbm+7jiRvFz+atnikBDCkjffHbj1bLeA==', N'{"TimeZoneId":"W. Europe Standard Time","SubscriptionTypeId":null,"ActivationCode":null,"Token":null,"Firstname":"Randy","Lastname":"McRandom","Company":"Randy McRandom","Culture":"en-US","Email":"developer@aiala.local","Password":"***REDACTED***","PasswordConfirmation":"***REDACTED***"}', N'en-US')
GO
INSERT [directory].[PermissionGroupAssignments] ([Id], [AccountId], [GroupId]) VALUES (N'6e0ea27b-7b1f-448e-a058-1537acdb2934', N'0af4150b-521f-4680-ac1e-9b86b438e10b', N'802c7e28-8ffb-452c-b837-cf33aaca453a')
GO
INSERT [directory].[PermissionGroupAssignments] ([Id], [AccountId], [GroupId]) VALUES (N'617a8a76-4da3-48fd-9a22-19c6c047086f', N'b2144aa1-9dbf-4a28-9075-979465a22962', N'802c7e28-8ffb-452c-b837-cf33aaca453a')
GO
INSERT [directory].[PermissionGroupAssignments] ([Id], [AccountId], [GroupId]) VALUES (N'fc3291fd-346b-4921-a8b1-1f28c0646215', N'da799e8e-1758-4e4b-a699-1a3946e432f4', N'802c7e28-8ffb-452c-b837-cf33aaca453a')
GO
INSERT [directory].[PermissionGroupAssignments] ([Id], [AccountId], [GroupId]) VALUES (N'86c88da4-82f4-4ff9-aaef-236aca3e44cc', N'f7497241-60ad-4397-8536-c924390ef99b', N'802c7e28-8ffb-452c-b837-cf33aaca453a')
GO
INSERT [directory].[PermissionGroupAssignments] ([Id], [AccountId], [GroupId]) VALUES (N'df24c7de-6528-4639-b0c9-2845491eae57', N'1bacb6fc-b532-45fc-90f9-adc0bedab8f4', N'802c7e28-8ffb-452c-b837-cf33aaca453a')
GO
INSERT [directory].[PermissionGroupAssignments] ([Id], [AccountId], [GroupId]) VALUES (N'9e4241d5-7233-474b-9749-2b97adb11794', N'0b8f4a48-2243-4d85-ab40-8cd2fa2f04a6', N'802c7e28-8ffb-452c-b837-cf33aaca453a')
GO
INSERT [directory].[PermissionGroupAssignments] ([Id], [AccountId], [GroupId]) VALUES (N'cd94f582-0f20-403a-ac1f-2c32ef644677', N'951caa4d-ec7b-414f-9cc0-7fc0c2b23c12', N'802c7e28-8ffb-452c-b837-cf33aaca453a')
GO
INSERT [directory].[PermissionGroupAssignments] ([Id], [AccountId], [GroupId]) VALUES (N'27842849-5f68-418b-9cc8-4005cc322f29', N'20d2f641-f96b-4c59-b481-5c1f07079ddf', N'802c7e28-8ffb-452c-b837-cf33aaca453a')
GO
INSERT [directory].[PermissionGroupAssignments] ([Id], [AccountId], [GroupId]) VALUES (N'8fb3c56d-9438-4919-9ed5-41559d1bf132', N'b1c348e1-25d9-478e-9d6c-ef31938049e5', N'802c7e28-8ffb-452c-b837-cf33aaca453a')
GO
INSERT [directory].[PermissionGroupAssignments] ([Id], [AccountId], [GroupId]) VALUES (N'9f330c2e-37cd-4fc9-941c-4288762b377f', N'2e95886e-7ecb-4f07-b8e9-e25b3828072a', N'802c7e28-8ffb-452c-b837-cf33aaca453a')
GO
INSERT [directory].[PermissionGroupAssignments] ([Id], [AccountId], [GroupId]) VALUES (N'e381bacd-316d-43dd-8173-44b4a826c1be', N'd390e9de-f368-4ef7-9486-d2be61c9b8a0', N'802c7e28-8ffb-452c-b837-cf33aaca453a')
GO
INSERT [directory].[PermissionGroupAssignments] ([Id], [AccountId], [GroupId]) VALUES (N'c7a8357e-c19f-428b-87f2-47c0901c80ea', N'33f310c2-7d48-4161-b02a-22e080c3bbfb', N'802c7e28-8ffb-452c-b837-cf33aaca453a')
GO
INSERT [directory].[PermissionGroupAssignments] ([Id], [AccountId], [GroupId]) VALUES (N'5f8967b8-800a-49a6-9609-54406d4dc2a5', N'8556f3bd-210b-4ecd-8763-4f3412b8415f', N'802c7e28-8ffb-452c-b837-cf33aaca453a')
GO
INSERT [directory].[PermissionGroupAssignments] ([Id], [AccountId], [GroupId]) VALUES (N'0eae1fda-bac6-476d-94d8-61e1cc76085e', N'1336e5c3-8be1-442a-98f5-b47343a9b021', N'802c7e28-8ffb-452c-b837-cf33aaca453a')
GO
INSERT [directory].[PermissionGroupAssignments] ([Id], [AccountId], [GroupId]) VALUES (N'e7eb27b7-057c-4763-a336-6417b62a3a76', N'05a43111-1688-4d19-ab2c-d00ed897a75b', N'802c7e28-8ffb-452c-b837-cf33aaca453a')
GO
INSERT [directory].[PermissionGroupAssignments] ([Id], [AccountId], [GroupId]) VALUES (N'cf019c03-c490-4140-8561-70cba6edd56a', N'e7b7b44a-d99b-4a42-9388-dbdcff733b13', N'802c7e28-8ffb-452c-b837-cf33aaca453a')
GO
INSERT [directory].[PermissionGroupAssignments] ([Id], [AccountId], [GroupId]) VALUES (N'a60a1ce8-b05e-46ac-9f45-80ed7eb4e62c', N'8a5ac6f8-7f1f-42c8-81bb-ac916f2852f9', N'802c7e28-8ffb-452c-b837-cf33aaca453a')
GO
INSERT [directory].[PermissionGroupAssignments] ([Id], [AccountId], [GroupId]) VALUES (N'ce613a49-d784-4d7b-bab1-82b0d4671ad5', N'06043953-a100-4042-bf89-6cc9260d17c1', N'802c7e28-8ffb-452c-b837-cf33aaca453a')
GO
INSERT [directory].[PermissionGroupAssignments] ([Id], [AccountId], [GroupId]) VALUES (N'da25ffb0-9841-43a2-a0d4-a7821edd8cee', N'569c03f8-df04-450d-8bef-91602b4727f2', N'802c7e28-8ffb-452c-b837-cf33aaca453a')
GO
INSERT [directory].[PermissionGroupAssignments] ([Id], [AccountId], [GroupId]) VALUES (N'd791a2b0-783f-44b2-823d-e45b9c2012bf', N'729fe9e4-046e-4b6e-9cfd-60ecfa148272', N'802c7e28-8ffb-452c-b837-cf33aaca453a')
GO
INSERT [directory].[PermissionGroupAssignments] ([Id], [AccountId], [GroupId]) VALUES (N'21588ecf-02d2-4913-b67f-e5a230bc5c5a', N'da71fe2d-f002-4c5c-b472-bc5932642bea', N'802c7e28-8ffb-452c-b837-cf33aaca453a')
GO
INSERT [directory].[PermissionGroupAssignments] ([Id], [AccountId], [GroupId]) VALUES (N'cee43083-25b2-4c35-93b1-ee153d7f9b68', N'7e1a4c04-ff11-4505-bfce-5a742aecbff8', N'802c7e28-8ffb-452c-b837-cf33aaca453a')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'a349e604-c2d1-403f-82ec-01c41c26825e', 1, N'CX', N'Christmas Island', N'+61')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'ede02c3f-9b24-4d85-a0c7-021e5386c861', 1, N'SG', N'Singapore', N'+65')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'77c07dae-8595-4c6a-870f-02d6461dd202', 1, N'ZM', N'Zambia', N'+260')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'44e996a7-fb2c-41bf-917f-02f3dd79f639', 1, N'AF', N'Afghanistan', N'+93')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'5c62dbd2-c02e-4ae9-8b14-031c6c874659', 1, N'PK', N'Pakistan', N'+92')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'abddcd95-2dda-48e1-9e41-0503df5d489c', 1, N'GW', N'Guinea-Bissau', N'+245')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'158eb17f-f08d-42cc-95a4-057f7133ae51', 1, N'NF', N'Norfolk Island', N'+672')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'5ad56577-7098-4515-b010-06324670a069', 1, N'SI', N'Slovenia', N'+386')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'743de50e-6fa1-4272-97e1-06bc862fe505', 1, N'LB', N'Lebanon', N'+961')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'f202c2af-1dd0-4882-99f0-082cb5776b2a', 1, N'IS', N'Iceland', N'+354')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'b4dde644-e38a-48ce-883e-0a9d8dd65758', 1, N'FI', N'Finland', N'+358')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'08051e14-d13e-4e9e-90a6-0f3c4fc7b6d8', 1, N'LT', N'Lithuania', N'+370')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'f83ba14a-2758-45ef-93df-10170b0759c3', 1, N'CA', N'Canada', N'+1')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'd2fc318d-ef9a-49d3-8db3-1264b8415728', 1, N'UG', N'Uganda', N'+256')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'7f8afd92-61bd-477c-bf78-12d62b3e5cb5', 1, N'VC', N'Saint Vincent and the Grenadines', N'+1784')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'fcb3d8e1-2a9c-470f-b7c5-15dea06477c9', 1, N'KN', N'Saint Kitts and Nevis', N'+1869')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'c0fb1455-53a6-44e6-88dd-1809082063c7', 1, N'GG', N'Guernsey', N'+44')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'ee76f5bd-7d77-440f-9aaf-186af4e13ba5', 1, N'DJ', N'Djibouti', N'+253')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'fa1f6ca9-2c38-4ab6-b361-1af88e919ae5', 1, N'GE', N'Georgia', N'+995')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'9651830d-20e4-4222-aa36-1b596500670d', 1, N'KH', N'Cambodia', N'+855')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'234e687b-9acf-4268-9d64-1b622f43e673', 1, N'BN', N'Brunei Darussalam', N'+673')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'6c234af4-d025-4815-924e-1d98609128ae', 1, N'ZW', N'Zimbabwe', N'+263')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'c64c69c8-9060-4251-852b-1f40c2df9124', 1, N'AW', N'Aruba', N'+297')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'668e3ca5-8bfb-4f8c-8c98-207e3889f0fe', 1, N'CL', N'Chile', N'+56')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'a2141dc6-5c4b-400f-bdb1-211ac5081954', 1, N'ME', N'Montenegro', N'+382')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'c7524e4a-6065-4fa6-a33b-215ae8f87e86', 1, N'CC', N'Cocos (Keeling) Islands', N'+61')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'ed266508-e26e-4708-8d21-23154619c3fa', 1, N'CY', N'Cyprus', N'+357')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'dc9ffe90-bb21-4e89-aea2-267ce1d029fd', 1, N'AZ', N'Azerbaijan', N'+994')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'aef0117a-7970-4a60-9fc1-279c8c8b6eb6', 1, N'NU', N'Niue', N'+683')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'64684184-0951-47e4-bac3-282314ccc2bd', 1, N'WS', N'Samoa', N'+685')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'a380394e-7990-4864-bca2-2832527192d6', 1, N'BY', N'Belarus', N'+375')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'ac050c4f-a0bc-4e0d-88ca-2853b9438d85', 1, N'PR', N'Puerto Rico', N'+1787')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'c75b1cfd-ebe2-4255-a6b6-28643f872152', 1, N'AT', N'Austria', N'+43')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'874ef0b2-ca3e-41ef-af1b-2aea2c0a5769', 1, N'AG', N'Antigua and Barbuda', N'+1268')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'f59c01d4-367d-4773-8c4c-2b47e4f2e3a0', 1, N'SM', N'San Marino', N'+378')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'7659bcc4-41cf-4846-bab8-2bae6f938359', 1, N'NZ', N'New Zealand', N'+64')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'f2fde10e-0e24-4a72-9f1f-2c248bae340c', 1, N'RS', N'Serbia', N'+381')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'39dcf65d-b256-4c48-9ffe-2cfd92d9adb1', 1, N'MV', N'Maldives', N'+960')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'83e157d8-1021-4d51-9a4a-2d627e769456', 1, N'UZ', N'Uzbekistan', N'+998')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'c00c5c0b-c6be-4d27-b4db-302443d57439', 1, N'NE', N'Niger', N'+227')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'8216f3c7-2a58-42e8-acd4-308021850354', 1, N'BF', N'Burkina Faso', N'+226')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'c978ac1e-7cfe-412b-a231-3195e0b31279', 1, N'BZ', N'Belize', N'+501')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'bdbdeb23-9199-4a6c-b0f5-33014965fe40', 1, N'RW', N'Rwanda', N'+250')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'3abddad7-fa8f-456b-a590-33c5083b98dd', 1, N'NO', N'Norway', N'+47')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'7945287d-cd87-4153-a98d-341a50f701bf', 1, N'TO', N'Tonga', N'+676')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'583cb448-7e2d-4c30-9835-343bbf977e9b', 1, N'CU', N'Cuba', N'+53')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'431af217-da9c-49c0-862d-346c3f154807', 1, N'TL', N'Timor-Leste', N'+670')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'c046fa0d-300b-4d38-b811-34aebae66b83', 1, N'VN', N'Viet Nam', N'+84')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'603218e7-8773-44ef-a1de-34b7d5d7b21e', 1, N'MG', N'Madagascar', N'+261')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'c6d869a6-9592-465c-bb33-3635511aae8e', 1, N'AD', N'Andorra', N'+376')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'5198fbd1-17c2-4130-b3ba-377c37c893af', 1, N'TH', N'Thailand', N'+66')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'0d41eff4-8d55-43e1-b94d-39631d7bfb54', 1, N'ER', N'Eritrea', N'+291')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'c895cb3b-0be9-44e5-8bd9-3c5ddbeac938', 1, N'MY', N'Malaysia', N'+60')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'c9c252be-8cfe-4f34-ba7d-3c8b11b551c7', 1, N'IL', N'Israel', N'+972')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'2c691e17-979b-4192-a106-3ccc4da056a6', 1, N'MN', N'Mongolia', N'+976')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'7e9acba5-29d1-400c-b198-3d235bc0c9dc', 1, N'HT', N'Haiti', N'+509')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'e445207e-d79a-40b1-a108-3f6f8a37d503', 1, N'TC', N'Turks and Caicos Islands', N'+1649')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'a0abe09e-395c-48d3-bef7-3f9282b1c60d', 1, N'PM', N'Saint Pierre and Miquelon', N'+508')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'ca39433d-6984-4567-a717-3fc948ea8d52', 1, N'AR', N'Argentina', N'+54')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'92414cfd-4ba3-469d-b951-3ff06eecffdc', 1, N'BW', N'Botswana', N'+267')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'168ca0f0-7e9f-499f-96e8-4131fc0f70a6', 1, N'DO', N'Dominican Republic', N'+1809')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'994c8be2-8fa3-43ee-a725-41895002f499', 1, N'PE', N'Peru', N'+51')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'66e5ac2b-c3ee-49c3-a25a-437b3d918e91', 1, N'KE', N'Kenya', N'+254')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'bf5f1f0f-9b12-41a4-a1aa-438c6a3af860', 1, N'CV', N'Cabo Verde', N'+238')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'54d6b82d-49da-44a5-8387-44eb555c8d44', 1, N'AE', N'United Arab Emirates', N'+971')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'6e2cb01a-d6c3-4ca2-8b13-48b3367735d2', 1, N'FJ', N'Fiji', N'+679')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'4f3cbce7-8ab8-44e5-a58f-4b4a7da09f43', 1, N'GP', N'Guadeloupe', N'+590')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'76819034-2d0b-4296-bb93-4c61927f9d8d', 1, N'MU', N'Mauritius', N'+230')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'c0eb726b-84d4-442d-bb98-4eb0f9d628b6', 1, N'GF', N'French Guiana', N'+594')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'818d2dc0-a89a-4c46-85a9-4f363e084187', 1, N'VA', N'Holy See', N'+379')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'11b4d693-dd81-4b9b-8c80-50a2fae110d3', 1, N'SV', N'El Salvador', N'+503')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'2e38cb7e-7dc3-40a7-a947-533fd1134105', 1, N'IO', N'British Indian Ocean Territory', N'+246')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'91296619-d79b-4f9f-b0b1-5356f17bf26e', 1, N'TG', N'Togo', N'+228')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'ae7b0ee4-e00f-436d-a9cc-55eb493a392f', 1, N'LV', N'Latvia', N'+371')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'c5f5f3ee-f4e1-4569-8e92-58a2b4a569cb', 1, N'PY', N'Paraguay', N'+595')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'cbb6a6af-e1e0-4d6d-a908-58b8f130576e', 1, N'MM', N'Myanmar', N'+95')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'bb691dd6-b2e1-4a61-8329-58f233d66d3a', 1, N'UA', N'Ukraine', N'+380')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'dacf22ad-417b-436f-a73f-59a47b2324b4', 1, N'JO', N'Jordan', N'+962')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'142dac4e-e495-4c29-aaac-5aa6c2b2f630', 1, N'SL', N'Sierra Leone', N'+232')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'88b339cf-a9c9-4e44-ab05-5c6e3cadcb6d', 1, N'id', N'Indonesia', N'+62')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'363ae089-3a30-478d-b49f-5c7b9bdafe50', 1, N'LU', N'Luxembourg', N'+352')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'c50719ef-6ccb-41d7-9244-5c82dde5e804', 1, N'KP', N'Korea (Democratic People''s Republic of)', N'+850')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'8c772dab-d696-403e-96ba-5d3c3284dfc1', 1, N'GA', N'Gabon', N'+241')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'f1ac9aeb-e891-4315-ac5e-5e9beebc3305', 1, N'DK', N'Denmark', N'+45')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'f28a2a31-b33e-410a-8418-5f84240a37d2', 1, N'CD', N'Congo (Democratic Republic of the)', N'+243')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'e8407507-47db-4672-89f0-6052fc97ac5b', 1, N'JM', N'Jamaica', N'+1876')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'0da60206-a3f5-4a18-a8b6-630e12719c82', 1, N'MS', N'Montserrat', N'+1664')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'8cce699e-b033-4b9b-902e-6483da7ca19a', 1, N'NR', N'Nauru', N'+674')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'64197b44-3373-4b0f-ada8-64a0066029ff', 1, N'PA', N'Panama', N'+507')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'61662609-003a-4097-95f8-6598bf26a4ef', 1, N'GT', N'Guatemala', N'+502')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'c4fb5e61-c37f-4e5f-b70b-65c7ff3a24b8', 1, N'LY', N'Libya', N'+218')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'd8b3c897-69e1-4bd0-aecf-668b0dadffc7', 1, N'MA', N'Morocco', N'+212')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'81a28038-c55d-4eca-8e68-668b2864fd98', 1, N'LS', N'Lesotho', N'+266')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'23440164-15e7-4aa2-ba50-6699b81b31ff', 1, N'MX', N'Mexico', N'+52')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'fb4d2c5a-5f93-4130-a03a-6885d2ff906b', 1, N'GQ', N'Equatorial Guinea', N'+240')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'c8ac2c56-36fd-40f2-9ea9-6974f2387a0e', 1, N'nl', N'Netherlands', N'+31')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'9343700a-a38e-44a9-b62f-6a069a17fdb6', 1, N'JE', N'Jersey', N'+44')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'4f0eb234-b1f0-4410-846c-6bb4bab31519', 1, N'GY', N'Guyana', N'+592')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'd9bc2c41-5b53-473c-869c-6cab75c50a59', 1, N'EE', N'Estonia', N'+372')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'd83ca70f-d185-4c87-b82a-6cba91de7531', 1, N'UM', N'United States Minor Outlying Islands', N'+')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'e2a89a18-6f76-4318-a87d-6e1eb5d913d1', 1, N'IR', N'Iran (Islamic Republic of)', N'+98')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'6aeaa091-8c4b-47a9-94f0-6e2b9636bf72', 1, N'SO', N'Somalia', N'+252')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'1cc45f40-784c-43b2-9b88-70d8b3cec6c3', 1, N'BD', N'Bangladesh', N'+880')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'c6b48dac-ceaf-45f6-a71c-70dcb28e3dd3', 1, N'es', N'Spain', N'+34')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'54372766-53d0-4c1c-9784-71e056d9c7d9', 1, N'BJ', N'Benin', N'+229')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'aa85fc2b-e3a5-4849-b869-724d61bc383e', 1, N'KY', N'Cayman Islands', N'+1345')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'ac714d22-4b36-4776-b842-728d72319087', 1, N'QA', N'Qatar', N'+974')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'453ffcfa-6e29-41db-99a1-72d0067abf30', 1, N'SE', N'Sweden', N'+46')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'56aca540-4ae2-43cf-bdff-72ea9b868780', 1, N'BI', N'Burundi', N'+257')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'477d8fbc-e80a-489e-aba2-736293a699fb', 1, N'BH', N'Bahrain', N'+973')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'c2473d84-7c6b-4b32-a624-77419bef5104', 1, N'LR', N'Liberia', N'+231')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'02778799-6b45-47be-923f-7a1cd2e287a2', 1, N'MR', N'Mauritania', N'+222')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'd55e3ff4-05e5-41f3-9e63-7a330f920950', 1, N'XK', N'Republic of Kosovo', N'+383')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'34d69641-9b8b-4c79-9e14-7ad3f49a7314', 1, N'BS', N'Bahamas', N'+1242')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'1d27b72f-31d6-4568-86cc-7b6ae7e206a3', 1, N'RO', N'Romania', N'+40')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'7a551b34-cb89-40bd-820e-7bf0c5603536', 1, N'AL', N'Albania', N'+355')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'ce56bc91-b97f-4f14-8c02-7e4ec2c6b1be', 1, N'MZ', N'Mozambique', N'+258')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'068f8b83-66a2-47ba-8285-7e815cf9c677', 1, N'GM', N'Gambia', N'+220')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'afc0c551-cec0-4999-a9fe-7f92710785ac', 1, N'BL', N'Saint Barthélemy', N'+590')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'7cf19a97-a4f6-4a07-8dee-8175b1c3cd7d', 1, N'TD', N'Chad', N'+235')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'6c99985d-4372-4d21-97c4-822863b28cf9', 1, N'BT', N'Bhutan', N'+975')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'c2195d27-4ca3-46f9-b289-8250e32dd019', 1, N'MH', N'Marshall Islands', N'+692')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'b7b1727a-e52f-45af-8a06-8337320eb960', 1, N'GR', N'Greece', N'+30')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'1c6c52d5-c467-47d4-a18a-84b367ba4311', 1, N'IN', N'India', N'+91')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'8a8d21ac-da3d-47f0-8049-84fbbaeee508', 1, N'GL', N'Greenland', N'+299')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'4cdcb59b-7021-4c86-8fa2-87b807460ad9', 1, N'CO', N'Colombia', N'+57')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'e0be6610-da6e-4d3d-8699-8825fb4ee337', 1, N'LC', N'Saint Lucia', N'+1758')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'36a1b84a-849d-4fb1-ba49-8b8d85011554', 1, N'SC', N'Seychelles', N'+248')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'cdafe458-b575-4207-823a-8c28e6630c70', 1, N'IM', N'Isle of Man', N'+44')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'bd2c6e92-2765-4dc3-b79e-8d7c0ac6fdd4', 1, N'HN', N'Honduras', N'+504')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'cdede895-15cf-4b19-a518-8da0211f76b7', 1, N'AI', N'Anguilla', N'+1264')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'c92e3b7f-e7aa-4b23-bd38-8ef653b7f7a2', 1, N'PL', N'Poland', N'+48')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'845bda9a-97c2-48ca-bb31-8f25ceb2bcab', 1, N'BE', N'Belgium', N'+32')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'e796d639-b073-4de5-b761-907ea7fc4ffc', 1, N'TJ', N'Tajikistan', N'+992')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'b04eb0d3-e4f6-4803-8bac-913ed58cd7d1', 1, N'de', N'Germany', N'+49')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'ab43ea54-d4b6-4b56-bf9e-91f73f8516ea', 1, N'BB', N'Barbados', N'+1246')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'0516741a-702a-4c5a-9b7e-92f21356b336', 1, N'SJ', N'Svalbard and Jan Mayen', N'+4779')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'dc48c0ec-8c3b-4d06-9283-935fa5453730', 1, N'MT', N'Malta', N'+356')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'871803b6-865a-4aee-8e4d-95189da19c50', 1, N'SB', N'Solomon Islands', N'+677')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'2a032347-4030-4119-babc-95e1da3e8e16', 1, N'NI', N'Nicaragua', N'+505')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'50edd070-117c-4b0a-ad32-97607b3b4fb5', 1, N'KM', N'Comoros', N'+269')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'b07d1ada-7a09-484e-aa77-97ce1b769ac7', 1, N'MW', N'Malawi', N'+265')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'db9524e7-418a-47ca-a207-982af4c59666', 1, N'BG', N'Bulgaria', N'+359')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'7b1130ba-25c5-4886-8683-9955f720c67e', 1, N'TR', N'Turkey', N'+90')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'f0b72653-ebba-457a-b38b-9a17762f6141', 1, N'AS', N'American Samoa', N'+1684')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'd6a33871-b015-4015-ac21-9a95ae8c89e5', 1, N'br', N'Brazil', N'+55')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'cb7d0d8e-7612-44b6-a1d6-9ac33762a823', 1, N'MQ', N'Martinique', N'+596')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'9e974584-f073-415c-b535-9bf6f8f83fbc', 1, N'YT', N'Mayotte', N'+262')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'2cdba5e4-4235-4c15-8eff-9dbbde91c8cc', 1, N'KG', N'Kyrgyzstan', N'+996')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'cc7464a1-8300-450d-bd12-9e93c5307a09', 1, N'fr', N'France', N'+33')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'd9a2e10d-8735-405e-9ea1-a00a7784dd56', 1, N'it', N'Italy', N'+39')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'c355b129-8496-4f66-b39b-a05b1e140f24', 1, N'SD', N'Sudan', N'+249')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'a228d247-1f29-487b-8c88-a7af387391c6', 1, N'AQ', N'Antarctica', N'+672')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'feb9d6d5-2327-4065-9cbc-a80d0b0ebc89', 1, N'VE', N'Venezuela (Bolivarian Republic of)', N'+58')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'7973277f-00e4-4518-be78-a811f244bed5', 1, N'WF', N'Wallis and Futuna', N'+681')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'098ad535-c846-4473-8a6d-a89f1f762e93', 1, N'KI', N'Kiribati', N'+686')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'68f56a68-74d2-4ff9-99cb-a946d96edd1b', 1, N'TT', N'Trinidad and Tobago', N'+1868')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'05e5ab87-dc04-404e-925d-ac6c90d82427', 1, N'UY', N'Uruguay', N'+598')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'0b4f312f-3456-4cc9-ba85-acb48b9e7e9e', 1, N'AX', N'Åland Islands', N'+358')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'a9501973-f190-471f-b299-acd6085ccfbc', 1, N'SS', N'South Sudan', N'+211')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'ecd1a960-7846-4599-b78c-ae0bbfbf8f54', 1, N'NA', N'Namibia', N'+264')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'0a1fadde-e103-40f3-94f8-ae58e70ee91d', 1, N'DZ', N'Algeria', N'+213')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'7fbe832c-04f2-4f09-932c-af642d295032', 1, N'PN', N'Pitcairn', N'+64')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'bba868d5-0661-4421-8141-b13297e96218', 1, N'MF', N'Saint Martin (French part)', N'+590')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'9c22df3d-bbbf-4a8a-a0ef-b1720bc43463', 1, N'hr', N'Croatia', N'+385')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'80ddc377-d6d3-40d0-aa85-b17907310d2b', 1, N'MD', N'Moldova (Republic of)', N'+373')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'efa9f8fb-92f8-46da-8171-b2947811b8cf', 1, N'SK', N'Slovakia', N'+421')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'290a4868-b537-41bf-b514-b36ddde77e32', 1, N'BQ', N'Bonaire, Sint Eustatius and Saba', N'+5997')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'fce57936-adc2-4a55-90be-b4c3476fd23e', 1, N'TF', N'French Southern Territories', N'+')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'8f532a74-0192-43ce-88e2-b57335b1daaf', 1, N'YE', N'Yemen', N'+967')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'39035db4-10a0-4070-9fa6-b703a9a79199', 1, N'MK', N'Macedonia (the former Yugoslav Republic of)', N'+389')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'4f720a85-1987-4d83-8739-b71595daaa8b', 1, N'DM', N'Dominica', N'+1767')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'6022bae6-98fd-4aa6-8900-b747f6c35044', 1, N'TK', N'Tokelau', N'+690')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'0bf9b7ac-0e84-41bd-8e97-b81352e451f2', 1, N'SX', N'Sint Maarten (Dutch part)', N'+1721')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'dd4b36b5-4499-48c7-ae89-b8f18c4b6fb9', 1, N'VU', N'Vanuatu', N'+678')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'1eb5343a-b090-4efc-a684-ba5d5a613516', 1, N'TV', N'Tuvalu', N'+688')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'fbb1feed-b0d0-4a50-9e88-bb541fc56537', 1, N'GU', N'Guam', N'+1671')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'529b34dd-12f1-4b52-9270-bd122ea8cc5f', 1, N'BA', N'Bosnia and Herzegovina', N'+387')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'1b6384c7-92a9-42b3-b996-c0b134ab1170', 1, N'MP', N'Northern Mariana Islands', N'+1670')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'a7e1fca7-dcc0-4873-9d1f-c170b5bf25a9', 1, N'LA', N'Lao People''s Democratic Republic', N'+856')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'92a086ad-64cc-4209-a1a7-c2002c1dfe69', 1, N'KW', N'Kuwait', N'+965')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'95a16c33-9300-4336-bc0b-c2e02220885e', 1, N'PF', N'French Polynesia', N'+689')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'7401ab85-99a8-40bd-b5b0-c4d94d25aa69', 1, N'RE', N'Réunion', N'+262')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'1855fe95-33ad-4a68-bcc3-c57ed85ce329', 1, N'pt', N'Portugal', N'+351')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'3fea9a2d-c0ef-40a3-8128-c589e95c3537', 1, N'CR', N'Costa Rica', N'+506')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'f30caf70-5235-4f4d-b300-c63efa33b1b1', 1, N'CF', N'Central African Republic', N'+236')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'cc76e0b5-40d7-40cc-ac59-c6dcd1b29cf0', 1, N'PW', N'Palau', N'+680')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'bc198476-8c8b-433e-80ad-c78d93b65758', 1, N'SY', N'Syrian Arab Republic', N'+963')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'900794cd-2b60-41da-97ae-c7bc082f9cf9', 1, N'SZ', N'Swaziland', N'+268')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'd6ecbf46-a0a5-4e61-89f9-c9acd0782532', 1, N'ML', N'Mali', N'+223')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'0e14972b-4f7c-4d6d-938b-ca6360b0872b', 1, N'MO', N'Macao', N'+853')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'ba3775f7-cb5c-4437-b1f2-ca7156560e53', 1, N'KZ', N'Kazakhstan', N'+76')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'a53ee786-5790-445b-95cc-cdf563cb599e', 1, N'NG', N'Nigeria', N'+234')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'f7fd2d44-31cc-4b71-8afd-cedf532cc605', 1, N'CH', N'Switzerland', N'+41')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'302ecafb-2dcb-466c-ab01-cf12d7b1544e', 1, N'RU', N'Russian Federation', N'+7')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'c41c146e-66c6-4b79-83bc-d18117231f31', 1, N'NC', N'New Caledonia', N'+687')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'6eeb4317-ea6c-493d-9af0-d1976d7100de', 1, N'AM', N'Armenia', N'+374')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'9f53e4c4-bc1e-4d26-b0a0-d1d589f9e443', 1, N'GB', N'United Kingdom of Great Britain and Northern Ireland', N'+44')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'b89fdca4-86a7-487a-b647-d2defee26a13', 1, N'CZ', N'Czech Republic', N'+420')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'8a54bca0-807d-4cdf-8047-d317433757f5', 1, N'EC', N'Ecuador', N'+593')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'04549a74-b458-4bc6-94de-d339d5ed12e1', 1, N'PH', N'Philippines', N'+63')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'22936236-016e-4c06-aff9-d33d56c7c901', 1, N'TZ', N'Tanzania, United Republic of', N'+255')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'31387bd8-b5d7-41c0-9807-d39d3a4453dc', 1, N'CW', N'Curaçao', N'+599')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'b9fdfe8b-b370-4ffe-9160-d5a76af13a39', 1, N'ET', N'Ethiopia', N'+251')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'8a951ed8-ca8c-4e8a-8232-d728d8a8a3e5', 1, N'KR', N'Korea (Republic of)', N'+82')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'8328c807-7bb3-4c7e-82d8-d72b0c148204', 1, N'FM', N'Micronesia (Federated States of)', N'+691')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'12d54c0e-6444-43fb-b27e-d82e319480e3', 1, N'EH', N'Western Sahara', N'+212')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'5c0d4d2b-9905-4485-9fc0-d8858ab2d3a8', 1, N'BO', N'Bolivia (Plurinational State of)', N'+591')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'0b153c4a-481f-4cbb-a488-d8e71ea05ff3', 1, N'SR', N'Suriname', N'+597')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'2a7a9ede-fcf3-4455-9cd2-daaf2f07851d', 1, N'LI', N'Liechtenstein', N'+423')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'be94fb67-a08c-419f-a9bc-dda56d4b5096', 1, N'OM', N'Oman', N'+968')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'41a606dd-4c52-438d-a9ec-df14591cbf3e', 1, N'EG', N'Egypt', N'+20')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'd42a610c-d6a2-4376-b8fa-e0cc24998d96', 1, N'AO', N'Angola', N'+244')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'0a25ab32-695f-4ecb-a47c-e0da577d36ee', 1, N'GI', N'Gibraltar', N'+350')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'c57a4fb1-b16e-4371-8553-e120064084f9', 1, N'FK', N'Falkland Islands (Malvinas)', N'+500')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'725a2cd5-cd9d-4f5f-9abc-e340f0b5e07a', 1, N'BM', N'Bermuda', N'+1441')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'f2c63602-3db9-401f-a8bc-e352359dd77d', 1, N'VG', N'Virgin Islands (British)', N'+1284')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'6a1bb25b-a7c8-4a72-bbfa-e3e7f0a7112b', 1, N'TN', N'Tunisia', N'+216')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'd9cf3244-5eb0-4f47-ae9c-e48b948ad068', 1, N'HM', N'Heard Island and McDonald Islands', N'+')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'495cc6a5-0016-44e1-8004-e7fbfd41a630', 1, N'FO', N'Faroe Islands', N'+298')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'6aa8a2e4-5b28-4fc0-a1ad-e80ad2426ede', 1, N'TM', N'Turkmenistan', N'+993')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'8e6e14e7-d429-4837-bb2b-e84f6cc3299a', 1, N'CN', N'China', N'+86')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'da4bd7d8-23a5-4a40-804f-e9a67cfa93a6', 1, N'PG', N'Papua New Guinea', N'+675')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'fce81138-2079-4bd2-ac13-e9e08e486232', 1, N'CI', N'Côte d''Ivoire', N'+225')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'ebf23b09-1151-4855-a3f6-e9fd51fef715', 1, N'HK', N'Hong Kong', N'+852')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'cfe3c5b5-d629-4887-83f9-ea9e3ea32ae8', 1, N'GH', N'Ghana', N'+233')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'b54fac30-8dbd-4c34-921b-ecb780d45e05', 1, N'US', N'United States of America', N'+1')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'47b942da-f582-4a85-9347-edf92e14ea86', 1, N'CK', N'Cook Islands', N'+682')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'dda9bd73-0cb2-423d-adcc-ee0d393b22b5', 1, N'CM', N'Cameroon', N'+237')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'714c382f-db01-4fca-bdfe-f0a7b244236f', 1, N'AU', N'Australia', N'+61')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'0fe76a29-bd01-4578-b770-f1f51dbf7172', 1, N'CG', N'Congo', N'+242')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'4474dd19-7cff-4df2-9de6-f1f52b9aa94c', 1, N'ZA', N'South Africa', N'+27')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'20b93646-a854-4cbc-9e45-f32cfe536f00', 1, N'LK', N'Sri Lanka', N'+94')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'ab0bfe63-8589-412a-afd5-f3e105d20568', 1, N'IQ', N'Iraq', N'+964')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'15d6f9ea-1026-4ad3-88f8-f3e8f1eb6f6e', 1, N'NP', N'Nepal', N'+977')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'27e8ba9e-58fc-48a1-8e92-f3f8bc846557', 1, N'GD', N'Grenada', N'+1473')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'670cba88-5a11-42b9-9377-f49805633bf4', 1, N'GN', N'Guinea', N'+224')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'2a7a8f6b-dda3-4799-8491-f4d03825899a', 1, N'SA', N'Saudi Arabia', N'+966')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'e6e85a31-ae53-43ee-b21a-f6ed0538b020', 1, N'PS', N'Palestine, State of', N'+970')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'b21c14d5-09e3-4f22-952e-f739db8b2dc0', 1, N'VI', N'Virgin Islands (U.S.)', N'+1 340')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'fcdc7d01-ea17-4ac8-b712-f778ee2de9f2', 1, N'SH', N'Saint Helena, Ascension and Tristan da Cunha', N'+290')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'78eb9514-06e7-493e-a495-f84c531a7791', 1, N'HU', N'Hungary', N'+36')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'cf2b6264-a1fe-43a2-b9d0-f92f41a62da8', 1, N'JP', N'Japan', N'+81')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'f1e7967a-0237-4ec2-95bc-f92fb9cf5537', 1, N'TW', N'Taiwan', N'+886')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'c3cdf8e2-45c4-4f11-a91d-fa1a43bcd9f9', 1, N'SN', N'Senegal', N'+221')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'f7dcbb8a-90d6-4008-89f9-fa8cd241d66f', 1, N'MC', N'Monaco', N'+377')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'9d19efbd-70e8-4817-afbb-fb735ff78ad7', 1, N'BV', N'Bouvet Island', N'+')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'c052a003-3341-442e-825b-fcbc3c208498', 1, N'ST', N'Sao Tome and Principe', N'+239')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'3fdd5f8d-f594-41d3-aeb6-ffabcfe62f36', 1, N'GS', N'South Georgia and the South Sandwich Islands', N'+500')
GO
INSERT [directory].[Countries] ([Id], [Enabled], [Code], [Name], [CallingCode]) VALUES (N'5a547753-2720-4ef8-9fd7-ffadd1036c72', 1, N'IE', N'Ireland', N'+353')
GO
INSERT [directory].[Settings] ([Id], [Value], [Key], [Type], [AccountId], [TenantId], [UserId]) VALUES (N'522720a6-3f8a-4d0f-aaeb-01269a63add1', N'"Good, releax yourself."', N'EmergencyTextImproving', 1, NULL, N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL)
GO
INSERT [directory].[Settings] ([Id], [Value], [Key], [Type], [AccountId], [TenantId], [UserId]) VALUES (N'a10b6868-03c8-4055-af63-09d96a363c64', N'"W. Europe Standard Time"', N'TimeZoneId', 1, NULL, N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL)
GO
INSERT [directory].[Settings] ([Id], [Value], [Key], [Type], [AccountId], [TenantId], [UserId]) VALUES (N'6376378b-eb5f-4e97-a9e7-3e59a29d93db', N'"20048a19-d552-4a55-a281-b6a28629ad41"', N'Place2Id', 1, NULL, N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL)
GO
INSERT [directory].[Settings] ([Id], [Value], [Key], [Type], [AccountId], [TenantId], [UserId]) VALUES (N'67783300-19b6-4b8b-b07c-6dd73b72035d', N'"da799e8e-1758-4e4b-a699-1a3946e432f4"', N'EmergencyContact2Id', 1, NULL, N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL)
GO
INSERT [directory].[Settings] ([Id], [Value], [Key], [Type], [AccountId], [TenantId], [UserId]) VALUES (N'0328fb82-df75-4f87-be03-b798e9a21e29', N'"Try to calm down a bit."', N'EmergencyTextBad', 1, NULL, N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL)
GO
INSERT [directory].[Settings] ([Id], [Value], [Key], [Type], [AccountId], [TenantId], [UserId]) VALUES (N'c7aa4805-e7bd-428b-81b8-d5c2e92f3e37', N'"20048a19-d552-4a55-a281-b6a28629ad41"', N'Place1Id', 1, NULL, N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL)
GO
INSERT [directory].[Settings] ([Id], [Value], [Key], [Type], [AccountId], [TenantId], [UserId]) VALUES (N'89510b6a-f925-4efa-81f2-eda0ebdc7043', N'"da799e8e-1758-4e4b-a699-1a3946e432f4"', N'EmergencyContact1Id', 1, NULL, N'802c7e28-8ffb-452c-b837-cf33aaca453a', NULL)
GO
