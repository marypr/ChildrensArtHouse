SET IDENTITY_INSERT [dbo].[Kruzhki] ON
INSERT INTO [dbo].[Kruzhki] ([Id], [Название_кружка], [Секция], [Руководитель], [Цена], [Колво_учеников], [Колво_занятий]) VALUES (7, N'Техники', N'Modeling', N'Лапина    ', CAST(125.0000 AS SmallMoney), 3, 5)
SET IDENTITY_INSERT [dbo].[Kruzhki] OFF
