use FootballManagement

--			Club
INSERT INTO [dbo].[Club] (clubId, clubName, shortName, stadium, logo, linkFb, linkIg)
VALUES
('CLB000001', 'Arsenal', 'ARS', 'Emirates Stadium', NULL, 'https://facebook.com/Arsenal', 'https://instagram.com/Arsenal'),
('CLB000002', 'Aston Villa', 'AVL', 'Villa Park', NULL, 'https://facebook.com/AstonVilla', 'https://instagram.com/AstonVilla'),
('CLB000003', 'Bournemouth', 'BOU', 'Vitality Stadium', NULL, 'https://facebook.com/AFCBournemouth', 'https://instagram.com/AFCBournemouth'),
('CLB000004', 'Brentford', 'BRE', 'Gtech Community Stadium', NULL, 'https://facebook.com/BrentfordFC', 'https://instagram.com/BrentfordFC'),
('CLB000005', 'Brighton', 'BHA', 'Amex Stadium', NULL, 'https://facebook.com/BrightonAndHoveAlbion', 'https://instagram.com/BrightonAndHoveAlbion'),
('CLB000006', 'Burnley', 'BUR', 'Turf Moor', NULL, 'https://facebook.com/BurnleyFC', 'https://instagram.com/BurnleyOfficial'),
('CLB000007', 'Chelsea', 'CHE', 'Stamford Bridge', NULL, 'https://facebook.com/ChelseaFC', 'https://instagram.com/ChelseaFC'),
('CLB000008', 'Crystal Palace', 'CRY', 'Selhurst Park', NULL, 'https://facebook.com/OfficialCrystalPalace', 'https://instagram.com/CPFC'),
('CLB000009', 'Everton', 'EVE', 'Goodison Park', NULL, 'https://facebook.com/Everton', 'https://instagram.com/Everton'),
('CLB000010', 'Fulham', 'FUL', 'Craven Cottage', NULL, 'https://facebook.com/FulhamFC', 'https://instagram.com/FulhamFC'),
('CLB000011', 'Liverpool', 'LIV', 'Anfield', NULL, 'https://facebook.com/LiverpoolFC', 'https://instagram.com/LiverpoolFC'),
('CLB000012', 'Luton Town', 'LUT', 'Kenilworth Road', NULL, 'https://facebook.com/LutonTownFC', 'https://instagram.com/LutonTownFC'),
('CLB000013', 'Manchester City', 'MCI', 'Etihad Stadium', NULL, 'https://facebook.com/ManCity', 'https://instagram.com/ManCity'),
('CLB000014', 'Manchester United', 'MUN', 'Old Trafford', NULL, 'https://facebook.com/ManUtd', 'https://instagram.com/ManUtd'),
('CLB000015', 'Newcastle United', 'NEW', 'St James'' Park', NULL, 'https://facebook.com/NewcastleUnited', 'https://instagram.com/NewcastleUnited'),
('CLB000016', 'Nottingham Forest', 'NOT', 'City Ground', NULL, 'https://facebook.com/NottinghamForest', 'https://instagram.com/OfficialNFFC'),
('CLB000017', 'Sheffield United', 'SHU', 'Bramall Lane', NULL, 'https://facebook.com/SheffieldUnited', 'https://instagram.com/SheffieldUnited'),
('CLB000018', 'Tottenham Hotspur', 'TOT', 'Tottenham Hotspur Stadium', NULL, 'https://facebook.com/TottenhamHotspur', 'https://instagram.com/SpursOfficial'),
('CLB000019', 'West Ham United', 'WHU', 'London Stadium', NULL, 'https://facebook.com/WestHam', 'https://instagram.com/WestHam'),
('CLB000020', 'Wolverhampton Wanderers', 'WOL', 'Molineux Stadium', NULL, 'https://facebook.com/Wolves', 'https://instagram.com/Wolves');


--			Rule
INSERT INTO [dbo].[Rule] (minAge, maxForeignPlayers, minPlayer, maxPlayer)
VALUES (16, 17, 25, 35);

--			TypeGoal
INSERT INTO [dbo].[TypeGoal] (typeGName, typeGDes)
VALUES 
('Own Goal', 'Bàn th?ng ph?n l??i nhà'),
('Penalty', 'Bàn th?ng t? ch?m ph?t ??n'),
('Other', 'Các tr??ng h?p khác');

--			Standing
INSERT INTO [dbo].[Standing] (clubId, played, won, drawn, lost, goalF, goalA, points)
SELECT clubId, 0, 0, 0, 0, 0, 0, 0
FROM [dbo].[Club];
