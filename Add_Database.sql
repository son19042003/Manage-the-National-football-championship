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

--			player
INSERT INTO [dbo].[Players] (
    [firstName], 
    [lastName], 
    [birthday], 
    [height], 
    [nationality], 
    [typePlayer], 
    [position], 
    [number], 
    [avatar], 
    [linkFb], 
    [linkIg], 
    [clubId], 
    [isInClub]
)
VALUES
-- ARSENAL
-- Cầu thủ 1
('Aaron', 'Ramsdale', '1998-05-14', 188, 'England', 1, 'Goalkeeper', 1, 'default.png', 'https://facebook.com/ramsdale', 'https://instagram.com/ramsdale', 'CLB000001', 1),
-- Cầu thủ 2
('Ben', 'White', '1997-10-08', 182, 'England', 1, 'Defender', 4, 'default.png', 'https://facebook.com/benwhite', 'https://instagram.com/benwhite', 'CLB000001', 1),
-- Cầu thủ 3
('Gabriel', 'Magalhães', '1997-12-19', 190, 'Brazil', 0, 'Defender', 6, 'default.png', 'https://facebook.com/gabriel', 'https://instagram.com/gabriel', 'CLB000001', 1),
-- Cầu thủ 4
('William', 'Saliba', '2001-03-24', 192, 'France', 0, 'Defender', 12, 'default.png', 'https://facebook.com/saliba', 'https://instagram.com/saliba', 'CLB000001', 1),
-- Cầu thủ 5
('Kieran', 'Tierney', '1997-06-05', 178, 'Scotland', 0, 'Defender', 3, 'default.png', 'https://facebook.com/tierney', 'https://instagram.com/tierney', 'CLB000001', 1),
-- Cầu thủ 6
('Declan', 'Rice', '1999-01-14', 185, 'England', 1, 'Midfielder', 41, 'default.png', 'https://facebook.com/rice', 'https://instagram.com/rice', 'CLB000001', 1),
-- Cầu thủ 7
('Martin', 'Ødegaard', '1998-12-17', 178, 'Norway', 0, 'Midfielder', 8, 'default.png', 'https://facebook.com/odegaard', 'https://instagram.com/odegaard', 'CLB000001', 1),
-- Cầu thủ 8
('Kai', 'Havertz', '1999-06-11', 190, 'Germany', 0, 'Midfielder', 29, 'default.png', 'https://facebook.com/havertz', 'https://instagram.com/havertz', 'CLB000001', 1),
-- Cầu thủ 9
('Bukayo', 'Saka', '2001-09-05', 178, 'England', 1, 'Forward', 7, 'default.png', 'https://facebook.com/saka', 'https://instagram.com/saka', 'CLB000001', 1),
-- Cầu thủ 10
('Gabriel', 'Martinelli', '2001-06-18', 178, 'Brazil', 0, 'Forward', 11, 'default.png', 'https://facebook.com/martinelli', 'https://instagram.com/martinelli', 'CLB000001', 1),
-- Cầu thủ 11
('Leandro', 'Trossard', '1994-12-04', 172, 'Belgium', 0, 'Forward', 19, 'default.png', 'https://facebook.com/trossard', 'https://instagram.com/trossard', 'CLB000001', 1),
-- Cầu thủ 12
('Eddie', 'Nketiah', '1999-05-30', 180, 'England', 1, 'Forward', 14, 'default.png', 'https://facebook.com/nketiah', 'https://instagram.com/nketiah', 'CLB000001', 1),
-- Cầu thủ 13
('Jorginho', '', '1991-12-20', 180, 'Italy', 0, 'Midfielder', 20, 'default.png', 'https://facebook.com/jorginho', 'https://instagram.com/jorginho', 'CLB000001', 1),
-- Cầu thủ 14
('Reiss', 'Nelson', '1999-12-10', 175, 'England', 1, 'Forward', 24, 'default.png', 'https://facebook.com/nelson', 'https://instagram.com/nelson', 'CLB000001', 1),
-- Cầu thủ 15
('Fabio', 'Vieira', '2000-05-30', 170, 'Portugal', 0, 'Midfielder', 21, 'default.png', 'https://facebook.com/vieira', 'https://instagram.com/vieira', 'CLB000001', 1),
-- Cầu thủ 16
('Gabriel', 'Jesus', '1997-04-03', 175, 'Brazil', 0, 'Forward', 9, 'default.png', 'https://facebook.com/jesus', 'https://instagram.com/jesus', 'CLB000001', 1),
-- Cầu thủ 17
('Takehiro', 'Tomiyasu', '1998-11-05', 188, 'Japan', 0, 'Defender', 18, 'default.png', 'https://facebook.com/tomiyasu', 'https://instagram.com/tomiyasu', 'CLB000001', 1),
-- Cầu thủ 18
('Emile', 'Smith Rowe', '2000-07-28', 182, 'England', 1, 'Midfielder', 10, 'default.png', 'https://facebook.com/smithrowe', 'https://instagram.com/smithrowe', 'CLB000001', 1),
-- Cầu thủ 19
('Mohamed', 'Elneny', '1992-07-11', 180, 'Egypt', 0, 'Midfielder', 25, 'default.png', 'https://facebook.com/elneny', 'https://instagram.com/elneny', 'CLB000001', 1),
-- Cầu thủ 20
('Rob', 'Holding', '1995-09-20', 187, 'England', 1, 'Defender', 16, 'default.png', 'https://facebook.com/holding', 'https://instagram.com/holding', 'CLB000001', 1),

--Aston Villa
-- Cầu thủ 1
('Emiliano', 'Martinez', '1992-09-02', 195, 'Argentina', 0, 'Goalkeeper', 1, 'default.png', 'https://facebook.com/emilianomartinez', 'https://instagram.com/emilianomartinez', 'CLB000002', 1),
-- Cầu thủ 2
('Robin', 'Olsen', '1990-01-08', 196, 'Sweden', 0, 'Goalkeeper', 25, 'default.png', 'https://facebook.com/robinolsen', 'https://instagram.com/robinolsen', 'CLB000002', 1),
-- Cầu thủ 3
('Ezri', 'Konsa', '1997-10-23', 183, 'England', 1, 'Defender', 4, 'default.png', 'https://facebook.com/ezrikonsa', 'https://instagram.com/ezrikonsa', 'CLB000002', 1),
-- Cầu thủ 4
('Tyrone', 'Mings', '1993-03-13', 196, 'England', 1, 'Defender', 5, 'default.png', 'https://facebook.com/tyronemings', 'https://instagram.com/tyronemings', 'CLB000002', 1),
-- Cầu thủ 5
('Diego', 'Carlos', '1993-03-15', 186, 'Brazil', 0, 'Defender', 3, 'default.png', 'https://facebook.com/diegocarlos', 'https://instagram.com/diegocarlos', 'CLB000002', 1),
-- Cầu thủ 6
('Lucas', 'Digne', '1993-07-20', 178, 'France', 0, 'Defender', 27, 'default.png', 'https://facebook.com/lucasdigne', 'https://instagram.com/lucasdigne', 'CLB000002', 1),
-- Cầu thủ 7
('Matty', 'Cash', '1997-08-07', 185, 'Poland', 0, 'Defender', 2, 'default.png', 'https://facebook.com/mattycash', 'https://instagram.com/mattycash', 'CLB000002', 1),
-- Cầu thủ 8
('Boubacar', 'Kamara', '1999-11-23', 184, 'France', 0, 'Midfielder', 44, 'default.png', 'https://facebook.com/boubacarkamara', 'https://instagram.com/boubacarkamara', 'CLB000002', 1),
-- Cầu thủ 9
('Douglas', 'Luiz', '1998-05-09', 178, 'Brazil', 0, 'Midfielder', 6, 'default.png', 'https://facebook.com/douglasluiz', 'https://instagram.com/douglasluiz', 'CLB000002', 1),
-- Cầu thủ 10
('John', 'McGinn', '1994-10-18', 178, 'Scotland', 0, 'Midfielder', 7, 'default.png', 'https://facebook.com/johnmcginn', 'https://instagram.com/johnmcginn', 'CLB000002', 1),
-- Cầu thủ 11
('Jacob', 'Ramsey', '2001-05-28', 180, 'England', 1, 'Midfielder', 41, 'default.png', 'https://facebook.com/jacobramsey', 'https://instagram.com/jacobramsey', 'CLB000002', 1),
-- Cầu thủ 12
('Leon', 'Bailey', '1997-08-09', 178, 'Jamaica', 0, 'Forward', 31, 'default.png', 'https://facebook.com/leonbailey', 'https://instagram.com/leonbailey', 'CLB000002', 1),
-- Cầu thủ 13
('Emiliano', 'Buendia', '1996-12-25', 172, 'Argentina', 0, 'Forward', 10, 'default.png', 'https://facebook.com/emilianobuendia', 'https://instagram.com/emilianobuendia', 'CLB000002', 1),
-- Cầu thủ 14
('Ollie', 'Watkins', '1995-12-30', 180, 'England', 1, 'Forward', 11, 'default.png', 'https://facebook.com/olliewatkins', 'https://instagram.com/olliewatkins', 'CLB000002', 1),
-- Cầu thủ 15
('Cameron', 'Archer', '2001-12-09', 178, 'England', 1, 'Forward', 35, 'default.png', 'https://facebook.com/cameronarcher', 'https://instagram.com/cameronarcher', 'CLB000002', 1),
-- Cầu thủ 16
('Bertrand', 'Traore', '1995-09-06', 181, 'Burkina Faso', 0, 'Forward', 9, 'default.png', 'https://facebook.com/bertrandtraore', 'https://instagram.com/bertrandtraore', 'CLB000002', 1),
-- Cầu thủ 17
('Jaden', 'Philogene', '2002-02-08', 178, 'England', 1, 'Midfielder', 43, 'default.png', 'https://facebook.com/jadenphilogene', 'https://instagram.com/jadenphilogene', 'CLB000002', 1),
-- Cầu thủ 18
('Ashley', 'Young', '1985-07-09', 175, 'England', 1, 'Defender', 18, 'default.png', 'https://facebook.com/ashleyyoung', 'https://instagram.com/ashleyyoung', 'CLB000002', 1),
-- Cầu thủ 19
('Marvelous', 'Nakamba', '1994-01-19', 178, 'Zimbabwe', 0, 'Midfielder', 19, 'default.png', 'https://facebook.com/marvelousnakamba', 'https://instagram.com/marvelousnakamba', 'CLB000002', 1),
-- Cầu thủ 20
('Kortney', 'Hause', '1995-07-16', 191, 'England', 1, 'Defender', 30, 'default.png', 'https://facebook.com/kortneyhause', 'https://instagram.com/kortneyhause', 'CLB000002', 1),

--Bournemouth
-- Cầu thủ 1
('Neto', 'Murara', '1989-07-19', 190, 'Brazil', 0, 'Goalkeeper', 1, 'default.png', 'https://facebook.com/netomurara', 'https://instagram.com/netomurara', 'CLB000003', 1),
-- Cầu thủ 2
('Mark', 'Travers', '1999-05-18', 191, 'Ireland', 0, 'Goalkeeper', 21, 'default.png', 'https://facebook.com/marktravers', 'https://instagram.com/marktravers', 'CLB000003', 1),
-- Cầu thủ 3
('Chris', 'Mepham', '1997-11-05', 190, 'Wales', 0, 'Defender', 6, 'default.png', 'https://facebook.com/chrismepham', 'https://instagram.com/chrismepham', 'CLB000003', 1),
-- Cầu thủ 4
('Lloyd', 'Kelly', '1998-10-01', 185, 'England', 1, 'Defender', 5, 'default.png', 'https://facebook.com/lloydkelly', 'https://instagram.com/lloydkelly', 'CLB000003', 1),
-- Cầu thủ 5
('Adam', 'Smith', '1991-04-29', 181, 'England', 1, 'Defender', 15, 'default.png', 'https://facebook.com/adamsmith', 'https://instagram.com/adamsmith', 'CLB000003', 1),
-- Cầu thủ 6
('Marcos', 'Senesi', '1997-05-10', 184, 'Argentina', 0, 'Defender', 25, 'default.png', 'https://facebook.com/marcossenesi', 'https://instagram.com/marcossenesi', 'CLB000003', 1),
-- Cầu thủ 7
('Lewis', 'Cook', '1997-02-03', 175, 'England', 1, 'Midfielder', 4, 'default.png', 'https://facebook.com/lewiscook', 'https://instagram.com/lewiscook', 'CLB000003', 1),
-- Cầu thủ 8
('Joe', 'Rothwell', '1995-01-11', 180, 'England', 1, 'Midfielder', 8, 'default.png', 'https://facebook.com/joerothwell', 'https://instagram.com/joerothwell', 'CLB000003', 1),
-- Cầu thủ 9
('Jefferson', 'Lerma', '1994-10-25', 179, 'Colombia', 0, 'Midfielder', 16, 'default.png', 'https://facebook.com/jeffersonlerma', 'https://instagram.com/jeffersonlerma', 'CLB000003', 1),
-- Cầu thủ 10
('Philip', 'Billing', '1996-06-11', 193, 'Denmark', 0, 'Midfielder', 29, 'default.png', 'https://facebook.com/philipbilling', 'https://instagram.com/philipbilling', 'CLB000003', 1),
-- Cầu thủ 11
('Ryan', 'Christie', '1995-02-22', 178, 'Scotland', 0, 'Midfielder', 10, 'default.png', 'https://facebook.com/ryanchristie', 'https://instagram.com/ryanchristie', 'CLB000003', 1),
-- Cầu thủ 12
('Dominic', 'Solanke', '1997-09-14', 187, 'England', 1, 'Forward', 9, 'default.png', 'https://facebook.com/dominicsolanke', 'https://instagram.com/dominicsolanke', 'CLB000003', 1),
-- Cầu thủ 13
('Kieffer', 'Moore', '1992-08-08', 196, 'Wales', 0, 'Forward', 21, 'default.png', 'https://facebook.com/kieffermoore', 'https://instagram.com/kieffermoore', 'CLB000003', 1),
-- Cầu thủ 14
('Jaidon', 'Anthony', '1999-12-01', 182, 'England', 1, 'Forward', 32, 'default.png', 'https://facebook.com/jaidonanthony', 'https://instagram.com/jaidonanthony', 'CLB000003', 1),
-- Cầu thủ 15
('David', 'Brooks', '1997-07-08', 173, 'Wales', 0, 'Forward', 7, 'default.png', 'https://facebook.com/davidbrooks', 'https://instagram.com/davidbrooks', 'CLB000003', 1),
-- Cầu thủ 16
('Marcus', 'Tavernier', '1999-03-22', 176, 'England', 1, 'Midfielder', 11, 'default.png', 'https://facebook.com/marcustavernier', 'https://instagram.com/marcustavernier', 'CLB000003', 1),
-- Cầu thủ 17
('Jordan', 'Zemura', '1999-11-14', 179, 'Zimbabwe', 0, 'Defender', 33, 'default.png', 'https://facebook.com/jordanzemura', 'https://instagram.com/jordanzemura', 'CLB000003', 1),
-- Cầu thủ 18
('Junior', 'Stanislas', '1989-11-26', 183, 'England', 1, 'Forward', 19, 'default.png', 'https://facebook.com/juniorstanislas', 'https://instagram.com/juniorstanislas', 'CLB000003', 1),
-- Cầu thủ 19
('Ben', 'Pearson', '1995-01-04', 177, 'England', 1, 'Midfielder', 22, 'default.png', 'https://facebook.com/benpearson', 'https://instagram.com/benpearson', 'CLB000003', 1),
-- Cầu thủ 20
('James', 'Hill', '2002-01-10', 185, 'England', 1, 'Defender', 14, 'default.png', 'https://facebook.com/jameshill', 'https://instagram.com/jameshill', 'CLB000003', 1),

--Brentford
-- Cầu thủ 1
('David', 'Raya', '1995-09-15', 183, 'Spain', 0, 'Goalkeeper', 1, 'default.png', 'https://facebook.com/davidraya', 'https://instagram.com/davidraya', 'CLB000004', 1),
-- Cầu thủ 2
('Thomas', 'Strakosha', '1995-03-19', 192, 'Albania', 0, 'Goalkeeper', 22, 'default.png', 'https://facebook.com/thomasstrakosha', 'https://instagram.com/thomasstrakosha', 'CLB000004', 1),
-- Cầu thủ 3
('Aaron', 'Hickey', '2002-06-10', 175, 'Scotland', 0, 'Defender', 2, 'default.png', 'https://facebook.com/aaronhickey', 'https://instagram.com/aaronhickey', 'CLB000004', 1),
-- Cầu thủ 4
('Ben', 'Mee', '1989-09-21', 180, 'England', 1, 'Defender', 16, 'default.png', 'https://facebook.com/benmee', 'https://instagram.com/benmee', 'CLB000004', 1),
-- Cầu thủ 5
('Ethan', 'Pinnock', '1993-05-29', 187, 'Jamaica', 0, 'Defender', 5, 'default.png', 'https://facebook.com/ethanpinnock', 'https://instagram.com/ethanpinnock', 'CLB000004', 1),
-- Cầu thủ 6
('Rico', 'Henry', '1997-07-08', 170, 'England', 1, 'Defender', 3, 'default.png', 'https://facebook.com/ricohenry', 'https://instagram.com/ricohenry', 'CLB000004', 1),
-- Cầu thủ 7
('Mads', 'Roerslev', '1999-06-24', 184, 'Denmark', 0, 'Defender', 30, 'default.png', 'https://facebook.com/madsroerslev', 'https://instagram.com/madsroerslev', 'CLB000004', 1),
-- Cầu thủ 8
('Vitaly', 'Janelt', '1998-05-10', 184, 'Germany', 0, 'Midfielder', 27, 'default.png', 'https://facebook.com/vitalyjanelt', 'https://instagram.com/vitalyjanelt', 'CLB000004', 1),
-- Cầu thủ 9
('Christian', 'Nørgaard', '1994-03-10', 185, 'Denmark', 0, 'Midfielder', 6, 'default.png', 'https://facebook.com/christiannorgaard', 'https://instagram.com/christiannorgaard', 'CLB000004', 1),
-- Cầu thủ 10
('Mathias', 'Jensen', '1996-01-01', 178, 'Denmark', 0, 'Midfielder', 8, 'default.png', 'https://facebook.com/mathiasjensen', 'https://instagram.com/mathiasjensen', 'CLB000004', 1),
-- Cầu thủ 11
('Josh', 'Dasilva', '1998-10-23', 184, 'England', 1, 'Midfielder', 10, 'default.png', 'https://facebook.com/joshdasilva', 'https://instagram.com/joshdasilva', 'CLB000004', 1),
-- Cầu thủ 12
('Shandon', 'Baptiste', '1998-04-08', 176, 'Grenada', 0, 'Midfielder', 26, 'default.png', 'https://facebook.com/shandonbaptiste', 'https://instagram.com/shandonbaptiste', 'CLB000004', 1),
-- Cầu thủ 13
('Bryan', 'Mbeumo', '1999-08-07', 171, 'Cameroon', 0, 'Forward', 19, 'default.png', 'https://facebook.com/bryanmbeumo', 'https://instagram.com/bryanmbeumo', 'CLB000004', 1),
-- Cầu thủ 14
('Ivan', 'Toney', '1996-03-16', 179, 'England', 1, 'Forward', 17, 'default.png', 'https://facebook.com/ivantoney', 'https://instagram.com/ivantoney', 'CLB000004', 1),
-- Cầu thủ 15
('Keane', 'Lewis-Potter', '2001-02-22', 170, 'England', 1, 'Forward', 23, 'default.png', 'https://facebook.com/keanelewispotter', 'https://instagram.com/keanelewispotter', 'CLB000004', 1),
-- Cầu thủ 16
('Yoane', 'Wissa', '1996-09-03', 180, 'Congo', 0, 'Forward', 11, 'default.png', 'https://facebook.com/yoanewissa', 'https://instagram.com/yoanewissa', 'CLB000004', 1),
-- Cầu thủ 17
('Frank', 'Onyeka', '1998-01-01', 184, 'Nigeria', 0, 'Midfielder', 15, 'default.png', 'https://facebook.com/frankonyeka', 'https://instagram.com/frankonyeka', 'CLB000004', 1),
-- Cầu thủ 18
('Mikkel', 'Damsgaard', '2000-07-03', 180, 'Denmark', 0, 'Midfielder', 24, 'default.png', 'https://facebook.com/mikkeldamsgaard', 'https://instagram.com/mikkeldamsgaard', 'CLB000004', 1),
-- Cầu thủ 19
('Saman', 'Ghoddos', '1993-09-06', 176, 'Iran', 0, 'Forward', 14, 'default.png', 'https://facebook.com/samanghoddos', 'https://instagram.com/samanghoddos', 'CLB000004', 1),
-- Cầu thủ 20
('Charlie', 'Goode', '1995-08-03', 196, 'England', 1, 'Defender', 4, 'default.png', 'https://facebook.com/charliegoode', 'https://instagram.com/charliegoode', 'CLB000004', 1),

--Brighton
-- Cầu thủ 1
('Jason', 'Steele', '1990-08-18', 191, 'England', 1, 'Goalkeeper', 23, 'default.png', 'https://facebook.com/jasonsteele', 'https://instagram.com/jasonsteele', 'CLB000005', 1),
-- Cầu thủ 2
('Bart', 'Verbruggen', '2002-08-18', 193, 'Netherlands', 0, 'Goalkeeper', 1, 'default.png', 'https://facebook.com/bartverbruggen', 'https://instagram.com/bartverbruggen', 'CLB000005', 1),
-- Cầu thủ 3
('Lewis', 'Dunk', '1991-11-21', 192, 'England', 1, 'Defender', 5, 'default.png', 'https://facebook.com/lewisdunk', 'https://instagram.com/lewisdunk', 'CLB000005', 1),
-- Cầu thủ 4
('Adam', 'Webster', '1995-01-04', 191, 'England', 1, 'Defender', 4, 'default.png', 'https://facebook.com/adamwebster', 'https://instagram.com/adamwebster', 'CLB000005', 1),
-- Cầu thủ 5
('Pervis', 'Estupiñán', '1998-01-21', 175, 'Ecuador', 0, 'Defender', 30, 'default.png', 'https://facebook.com/pervisestupinan', 'https://instagram.com/pervisestupinan', 'CLB000005', 1),
-- Cầu thủ 6
('Joël', 'Veltman', '1992-01-15', 184, 'Netherlands', 0, 'Defender', 34, 'default.png', 'https://facebook.com/joelveltman', 'https://instagram.com/joelveltman', 'CLB000005', 1),
-- Cầu thủ 7
('Pascal', 'Gross', '1991-06-15', 181, 'Germany', 0, 'Midfielder', 13, 'default.png', 'https://facebook.com/pascalgross', 'https://instagram.com/pascalgross', 'CLB000005', 1),
-- Cầu thủ 8
('Billy', 'Gilmour', '2001-06-11', 170, 'Scotland', 0, 'Midfielder', 11, 'default.png', 'https://facebook.com/billygilmour', 'https://instagram.com/billygilmour', 'CLB000005', 1),
-- Cầu thủ 9
('Carlos', 'Baleba', '2004-01-03', 183, 'Cameroon', 0, 'Midfielder', 40, 'default.png', 'https://facebook.com/carlosbaleba', 'https://instagram.com/carlosbaleba', 'CLB000005', 1),
-- Cầu thủ 10
('Moises', 'Caicedo', '2001-11-02', 178, 'Ecuador', 0, 'Midfielder', 25, 'default.png', 'https://facebook.com/moisescaicedo', 'https://instagram.com/moisescaicedo', 'CLB000005', 1),
-- Cầu thủ 11
('Solly', 'March', '1994-07-20', 180, 'England', 1, 'Midfielder', 7, 'default.png', 'https://facebook.com/sollymarch', 'https://instagram.com/sollymarch', 'CLB000005', 1),
-- Cầu thủ 12
('Kaoru', 'Mitoma', '1997-05-20', 178, 'Japan', 0, 'Forward', 22, 'default.png', 'https://facebook.com/kaorumitoma', 'https://instagram.com/kaorumitoma', 'CLB000005', 1),
-- Cầu thủ 13
('Evan', 'Ferguson', '2004-10-19', 183, 'Ireland', 0, 'Forward', 28, 'default.png', 'https://facebook.com/evanferguson', 'https://instagram.com/evanferguson', 'CLB000005', 1),
-- Cầu thủ 14
('Julio', 'Enciso', '2004-01-23', 173, 'Paraguay', 0, 'Forward', 10, 'default.png', 'https://facebook.com/julioenciso', 'https://instagram.com/julioenciso', 'CLB000005', 1),
-- Cầu thủ 15
('Danny', 'Welbeck', '1990-11-26', 185, 'England', 1, 'Forward', 18, 'default.png', 'https://facebook.com/dannywelbeck', 'https://instagram.com/dannywelbeck', 'CLB000005', 1),
-- Cầu thủ 16
('Facundo', 'Buonanotte', '2004-12-23', 177, 'Argentina', 0, 'Midfielder', 20, 'default.png', 'https://facebook.com/facundobuonanotte', 'https://instagram.com/facundobuonanotte', 'CLB000005', 1),
-- Cầu thủ 17
('Simon', 'Adingra', '2002-01-01', 175, 'Ivory Coast', 0, 'Forward', 16, 'default.png', 'https://facebook.com/simonadingra', 'https://instagram.com/simonadingra', 'CLB000005', 1),
-- Cầu thủ 18
('Jan', 'Paul', '1993-04-08', 190, 'Netherlands', 0, 'Defender', 29, 'default.png', 'https://facebook.com/janpaul', 'https://instagram.com/janpaul', 'CLB000005', 1),
-- Cầu thủ 19
('Steven', 'Alzate', '1998-09-08', 180, 'Colombia', 0, 'Midfielder', 17, 'default.png', 'https://facebook.com/stevenalzate', 'https://instagram.com/stevenalzate', 'CLB000005', 1),
-- Cầu thủ 20
('Andrew', 'Moran', '2003-01-15', 172, 'Ireland', 0, 'Midfielder', 14, 'default.png', 'https://facebook.com/andrewmoran', 'https://instagram.com/andrewmoran', 'CLB000005', 1),

--Burnley
-- Cầu thủ 1
('James', 'Trafford', '2002-10-10', 191, 'England', 1, 'Goalkeeper', 1, 'default.png', 'https://facebook.com/jamestrafford', 'https://instagram.com/jamestrafford', 'CLB000006', 1),
-- Cầu thủ 2
('Arijanet', 'Muric', '1998-11-07', 198, 'Kosovo', 0, 'Goalkeeper', 49, 'default.png', 'https://facebook.com/arijanetmuric', 'https://instagram.com/arijanetmuric', 'CLB000006', 1),
-- Cầu thủ 3
('Connor', 'Roberts', '1995-09-23', 175, 'Wales', 0, 'Defender', 14, 'default.png', 'https://facebook.com/connorroberts', 'https://instagram.com/connorroberts', 'CLB000006', 1),
-- Cầu thủ 4
('Jordan', 'Beyer', '2000-05-19', 187, 'Germany', 0, 'Defender', 36, 'default.png', 'https://facebook.com/jordanbeyer', 'https://instagram.com/jordanbeyer', 'CLB000006', 1),
-- Cầu thủ 5
('Hjalmar', 'Ekdal', '1998-10-21', 192, 'Sweden', 0, 'Defender', 18, 'default.png', 'https://facebook.com/hjalmarekdal', 'https://instagram.com/hjalmarekdal', 'CLB000006', 1),
-- Cầu thủ 6
('Ameen', 'Al-Dakhil', '2002-03-06', 189, 'Belgium', 0, 'Defender', 28, 'default.png', 'https://facebook.com/ameenaldakhil', 'https://instagram.com/ameenaldakhil', 'CLB000006', 1),
-- Cầu thủ 7
('Charlie', 'Taylor', '1993-09-18', 176, 'England', 1, 'Defender', 3, 'default.png', 'https://facebook.com/charlietaylor', 'https://instagram.com/charlietaylor', 'CLB000006', 1),
-- Cầu thủ 8
('Josh', 'Brownhill', '1995-12-19', 178, 'England', 1, 'Midfielder', 8, 'default.png', 'https://facebook.com/joshbrownhill', 'https://instagram.com/joshbrownhill', 'CLB000006', 1),
-- Cầu thủ 9
('Sander', 'Berge', '1998-02-14', 195, 'Norway', 0, 'Midfielder', 16, 'default.png', 'https://facebook.com/sanderberge', 'https://instagram.com/sanderberge', 'CLB000006', 1),
-- Cầu thủ 10
('Aaron', 'Ramsey', '2003-01-21', 182, 'England', 1, 'Midfielder', 15, 'default.png', 'https://facebook.com/aaronramsey', 'https://instagram.com/aaronramsey', 'CLB000006', 1),
-- Cầu thủ 11
('Jack', 'Cork', '1989-06-25', 183, 'England', 1, 'Midfielder', 4, 'default.png', 'https://facebook.com/jackcork', 'https://instagram.com/jackcork', 'CLB000006', 1),
-- Cầu thủ 12
('Luca', 'Koleosho', '2004-09-15', 176, 'United States', 0, 'Forward', 22, 'default.png', 'https://facebook.com/lucakoleosho', 'https://instagram.com/lucakoleosho', 'CLB000006', 1),
-- Cầu thủ 13
('Zeki', 'Amdouni', '2001-12-04', 182, 'Switzerland', 0, 'Forward', 25, 'default.png', 'https://facebook.com/zekiamdouni', 'https://instagram.com/zekiamdouni', 'CLB000006', 1),
-- Cầu thủ 14
('Jay', 'Rodriguez', '1989-07-29', 185, 'England', 1, 'Forward', 9, 'default.png', 'https://facebook.com/jayrodriguez', 'https://instagram.com/jayrodriguez', 'CLB000006', 1),
-- Cầu thủ 15
('Anass', 'Zaroury', '2000-11-07', 176, 'Morocco', 0, 'Forward', 19, 'default.png', 'https://facebook.com/anasszaroury', 'https://instagram.com/anasszaroury', 'CLB000006', 1),
-- Cầu thủ 16
('Nathan', 'Redmond', '1994-03-06', 173, 'England', 1, 'Forward', 11, 'default.png', 'https://facebook.com/nathanredmond', 'https://instagram.com/nathanredmond', 'CLB000006', 1),
-- Cầu thủ 17
('Lyle', 'Foster', '2000-09-03', 185, 'South Africa', 0, 'Forward', 17, 'default.png', 'https://facebook.com/lylefoster', 'https://instagram.com/lylefoster', 'CLB000006', 1),
-- Cầu thủ 18
('Vitinho', 'Da Silva', '1999-07-23', 180, 'Brazil', 0, 'Defender', 2, 'default.png', 'https://facebook.com/vitinhodasilva', 'https://instagram.com/vitinhodasilva', 'CLB000006', 1),
-- Cầu thủ 19
('Manuel', 'Benson', '1997-03-28', 172, 'Belgium', 0, 'Forward', 10, 'default.png', 'https://facebook.com/manuelbenson', 'https://instagram.com/manuelbenson', 'CLB000006', 1),
-- Cầu thủ 20
('Samuel', 'Bastien', '1996-09-26', 180, 'DR Congo', 0, 'Midfielder', 7, 'default.png', 'https://facebook.com/samuelbastien', 'https://instagram.com/samuelbastien', 'CLB000006', 1),

--Chelsea
-- Cầu thủ 1
('Kepa', 'Arrizabalaga', '1994-10-03', 186, 'Spain', 0, 'Goalkeeper', 1, 'default.png', 'https://facebook.com/kepaarrizabalaga', 'https://instagram.com/kepaarrizabalaga', 'CLB000007', 1),
-- Cầu thủ 2
('Robert', 'Sánchez', '1997-11-18', 197, 'Spain', 0, 'Goalkeeper', 31, 'default.png', 'https://facebook.com/robertsanchez', 'https://instagram.com/robertsanchez', 'CLB000007', 1),
-- Cầu thủ 3
('Thiago', 'Silva', '1984-09-22', 181, 'Brazil', 0, 'Defender', 6, 'default.png', 'https://facebook.com/thiagosilva', 'https://instagram.com/thiagosilva', 'CLB000007', 1),
-- Cầu thủ 4
('Reece', 'James', '1999-12-08', 182, 'England', 1, 'Defender', 24, 'default.png', 'https://facebook.com/reecejames', 'https://instagram.com/reecejames', 'CLB000007', 1),
-- Cầu thủ 5
('Ben', 'Chilwell', '1996-12-21', 178, 'England', 1, 'Defender', 21, 'default.png', 'https://facebook.com/benchilwell', 'https://instagram.com/benchilwell', 'CLB000007', 1),
-- Cầu thủ 6
('Levi', 'Colwill', '2003-02-26', 187, 'England', 1, 'Defender', 26, 'default.png', 'https://facebook.com/levicolwill', 'https://instagram.com/levicolwill', 'CLB000007', 1),
-- Cầu thủ 7
('Marc', 'Cucurella', '1998-07-22', 175, 'Spain', 0, 'Defender', 3, 'default.png', 'https://facebook.com/marccucurella', 'https://instagram.com/marccucurella', 'CLB000007', 1),
-- Cầu thủ 8
('Enzo', 'Fernández', '2001-01-17', 178, 'Argentina', 0, 'Midfielder', 8, 'default.png', 'https://facebook.com/enzofernandez', 'https://instagram.com/enzofernandez', 'CLB000007', 1),
-- Cầu thủ 9
('Moisés', 'Caicedo', '2001-11-02', 178, 'Ecuador', 0, 'Midfielder', 25, 'default.png', 'https://facebook.com/moisescaicedo', 'https://instagram.com/moisescaicedo', 'CLB000007', 1),
-- Cầu thủ 10
('Conor', 'Gallagher', '2000-02-06', 182, 'England', 1, 'Midfielder', 23, 'default.png', 'https://facebook.com/conorgallagher', 'https://instagram.com/conorgallagher', 'CLB000007', 1),
-- Cầu thủ 11
('Noni', 'Madueke', '2002-03-10', 182, 'England', 1, 'Forward', 31, 'default.png', 'https://facebook.com/nonimadueke', 'https://instagram.com/nonimadueke', 'CLB000007', 1),
-- Cầu thủ 12
('Raheem', 'Sterling', '1994-12-08', 170, 'England', 1, 'Forward', 7, 'default.png', 'https://facebook.com/raheemsterling', 'https://instagram.com/raheemsterling', 'CLB000007', 1),
-- Cầu thủ 13
('Mykhailo', 'Mudryk', '2001-01-05', 175, 'Ukraine', 0, 'Forward', 10, 'default.png', 'https://facebook.com/mykhailomudryk', 'https://instagram.com/mykhailomudryk', 'CLB000007', 1),
-- Cầu thủ 14
('Christopher', 'Nkunku', '1997-11-14', 175, 'France', 0, 'Forward', 18, 'default.png', 'https://facebook.com/christophernkunku', 'https://instagram.com/christophernkunku', 'CLB000007', 1),
-- Cầu thủ 15
('Nicolas', 'Jackson', '2001-06-20', 186, 'Senegal', 0, 'Forward', 15, 'default.png', 'https://facebook.com/nicolasjackson', 'https://instagram.com/nicolasjackson', 'CLB000007', 1),
-- Cầu thủ 16
('Armando', 'Broja', '2001-09-10', 191, 'Albania', 0, 'Forward', 19, 'default.png', 'https://facebook.com/armandobroja', 'https://instagram.com/armandobroja', 'CLB000007', 1),
-- Cầu thủ 17
('Malo', 'Gusto', '2003-05-19', 179, 'France', 0, 'Defender', 27, 'default.png', 'https://facebook.com/malogusto', 'https://instagram.com/malogusto', 'CLB000007', 1),
-- Cầu thủ 18
('Ian', 'Maatsen', '2002-03-10', 176, 'Netherlands', 0, 'Defender', 29, 'default.png', 'https://facebook.com/ianmaatsen', 'https://instagram.com/ianmaatsen', 'CLB000007', 1),
-- Cầu thủ 19
('Lesley', 'Ugochukwu', '2004-03-26', 188, 'France', 0, 'Midfielder', 16, 'default.png', 'https://facebook.com/lesleyugochukwu', 'https://instagram.com/lesleyugochukwu', 'CLB000007', 1),
-- Cầu thủ 20
('Carney', 'Chukwuemeka', '2003-10-20', 182, 'England', 1, 'Midfielder', 17, 'default.png', 'https://facebook.com/carneychukwuemeka', 'https://instagram.com/carneychukwuemeka', 'CLB000007', 1),

--Crystal Palace
-- Cầu thủ 1
('Sam', 'Johnstone', '1993-03-25', 193, 'England', 1, 'Goalkeeper', 1, 'default.png', 'https://facebook.com/samjohnstone', 'https://instagram.com/samjohnstone', 'CLB000008', 1),
-- Cầu thủ 2
('Vicente', 'Guaita', '1987-01-10', 190, 'Spain', 0, 'Goalkeeper', 13, 'default.png', 'https://facebook.com/vicenteguaita', 'https://instagram.com/vicenteguaita', 'CLB000008', 1),
-- Cầu thủ 3
('Joachim', 'Andersen', '1996-05-31', 192, 'Denmark', 0, 'Defender', 16, 'default.png', 'https://facebook.com/joachimandersen', 'https://instagram.com/joachimandersen', 'CLB000008', 1),
-- Cầu thủ 4
('Marc', 'Guehi', '2000-07-13', 185, 'England', 1, 'Defender', 6, 'default.png', 'https://facebook.com/marcguehi', 'https://instagram.com/marcguehi', 'CLB000008', 1),
-- Cầu thủ 5
('Tyrick', 'Mitchell', '1999-09-01', 175, 'England', 1, 'Defender', 3, 'default.png', 'https://facebook.com/tyrickmitchell', 'https://instagram.com/tyrickmitchell', 'CLB000008', 1),
-- Cầu thủ 6
('Joel', 'Ward', '1989-10-29', 186, 'England', 1, 'Defender', 2, 'default.png', 'https://facebook.com/joelward', 'https://instagram.com/joelward', 'CLB000008', 1),
-- Cầu thủ 7
('James', 'Tomkins', '1989-03-29', 192, 'England', 1, 'Defender', 5, 'default.png', 'https://facebook.com/jamestomkins', 'https://instagram.com/jamestomkins', 'CLB000008', 1),
-- Cầu thủ 8
('Cheick', 'Doucoure', '2000-01-08', 180, 'Mali', 0, 'Midfielder', 28, 'default.png', 'https://facebook.com/cheickdoucoure', 'https://instagram.com/cheickdoucoure', 'CLB000008', 1),
-- Cầu thủ 9
('Jeffrey', 'Schlupp', '1992-12-23', 178, 'Ghana', 0, 'Midfielder', 15, 'default.png', 'https://facebook.com/jeffreyschlupp', 'https://instagram.com/jeffreyschlupp', 'CLB000008', 1),
-- Cầu thủ 10
('Will', 'Hughes', '1995-04-17', 185, 'England', 1, 'Midfielder', 19, 'default.png', 'https://facebook.com/willhughes', 'https://instagram.com/willhughes', 'CLB000008', 1),
-- Cầu thủ 11
('Eberechi', 'Eze', '1998-06-29', 180, 'England', 1, 'Midfielder', 10, 'default.png', 'https://facebook.com/eberechieze', 'https://instagram.com/eberechieze', 'CLB000008', 1),
-- Cầu thủ 12
('Michael', 'Olise', '2001-12-12', 184, 'France', 0, 'Midfielder', 7, 'default.png', 'https://facebook.com/michaelolise', 'https://instagram.com/michaelolise', 'CLB000008', 1),
-- Cầu thủ 13
('Jordan', 'Ayew', '1991-09-11', 182, 'Ghana', 0, 'Forward', 9, 'default.png', 'https://facebook.com/jordanayew', 'https://instagram.com/jordanayew', 'CLB000008', 1),
-- Cầu thủ 14
('Odsonne', 'Edouard', '1998-01-16', 187, 'France', 0, 'Forward', 22, 'default.png', 'https://facebook.com/odsonneedouard', 'https://instagram.com/odsonneedouard', 'CLB000008', 1),
-- Cầu thủ 15
('Jean-Philippe', 'Mateta', '1997-06-28', 192, 'France', 0, 'Forward', 14, 'default.png', 'https://facebook.com/jeanphilippemateta', 'https://instagram.com/jeanphilippemateta', 'CLB000008', 1),
-- Cầu thủ 16
('Jairo', 'Riedewald', '1996-09-09', 185, 'Netherlands', 0, 'Midfielder', 44, 'default.png', 'https://facebook.com/jairoriedewald', 'https://instagram.com/jairoriedewald', 'CLB000008', 1),
-- Cầu thủ 17
('Chris', 'Richards', '2000-03-28', 188, 'USA', 0, 'Defender', 26, 'default.png', 'https://facebook.com/chrisrichards', 'https://instagram.com/chrisrichards', 'CLB000008', 1),
-- Cầu thủ 18
('Nathaniel', 'Clyne', '1991-04-05', 175, 'England', 1, 'Defender', 17, 'default.png', 'https://facebook.com/nathanielclyne', 'https://instagram.com/nathanielclyne', 'CLB000008', 1),
-- Cầu thủ 19
('Remi', 'Matthews', '1994-02-10', 185, 'England', 1, 'Goalkeeper', 31, 'default.png', 'https://facebook.com/remimatthews', 'https://instagram.com/remimatthews', 'CLB000008', 1),
-- Cầu thủ 20
('Malcolm', 'Ebiowei', '2003-09-04', 185, 'England', 1, 'Forward', 23, 'default.png', 'https://facebook.com/malcolmebiowei', 'https://instagram.com/malcolmebiowei', 'CLB000008', 1),

--Everton
-- Cầu thủ 1
('Jordan', 'Pickford', '1994-03-07', 185, 'England', 1, 'Goalkeeper', 1, 'default.png', 'https://facebook.com/jordanpickford', 'https://instagram.com/jordanpickford', 'CLB000009', 1),
-- Cầu thủ 2
('Asmir', 'Begovic', '1987-06-20', 200, 'Bosnia and Herzegovina', 0, 'Goalkeeper', 15, 'default.png', 'https://facebook.com/asmirbegovic', 'https://instagram.com/asmirbegovic', 'CLB000009', 1),
-- Cầu thủ 3
('Michael', 'Keane', '1993-01-11', 191, 'England', 1, 'Defender', 5, 'default.png', 'https://facebook.com/michaelkeane', 'https://instagram.com/michaelkeane', 'CLB000009', 1),
-- Cầu thủ 4
('James', 'Tarkowski', '1992-11-19', 185, 'England', 1, 'Defender', 6, 'default.png', 'https://facebook.com/jamestarkowski', 'https://instagram.com/jamestarkowski', 'CLB000009', 1),
-- Cầu thủ 5
('Seamus', 'Coleman', '1988-10-11', 177, 'Ireland', 0, 'Defender', 23, 'default.png', 'https://facebook.com/seamuscoleman', 'https://instagram.com/seamuscoleman', 'CLB000009', 1),
-- Cầu thủ 6
('Ben', 'Godfrey', '1998-01-15', 185, 'England', 1, 'Defender', 22, 'default.png', 'https://facebook.com/bengodfrey', 'https://instagram.com/bengodfrey', 'CLB000009', 1),
-- Cầu thủ 7
('Vitalii', 'Mykolenko', '1999-05-29', 181, 'Ukraine', 0, 'Defender', 19, 'default.png', 'https://facebook.com/vitaliimykolenko', 'https://instagram.com/vitaliimykolenko', 'CLB000009', 1),
-- Cầu thủ 8
('Idrissa', 'Gueye', '1989-09-26', 174, 'Senegal', 0, 'Midfielder', 27, 'default.png', 'https://facebook.com/idrissagueye', 'https://instagram.com/idrissagueye', 'CLB000009', 1),
-- Cầu thủ 9
('Amadou', 'Onana', '2001-08-16', 195, 'Belgium', 0, 'Midfielder', 8, 'default.png', 'https://facebook.com/amadouonana', 'https://instagram.com/amadouonana', 'CLB000009', 1),
-- Cầu thủ 10
('Tom', 'Davies', '1998-06-30', 180, 'England', 1, 'Midfielder', 26, 'default.png', 'https://facebook.com/tomdavies', 'https://instagram.com/tomdavies', 'CLB000009', 1),
-- Cầu thủ 11
('Alex', 'Iwobi', '1996-05-03', 180, 'Nigeria', 0, 'Midfielder', 17, 'default.png', 'https://facebook.com/alexiwobi', 'https://instagram.com/alexiwobi', 'CLB000009', 1),
-- Cầu thủ 12
('Demarai', 'Gray', '1996-06-28', 178, 'England', 1, 'Midfielder', 11, 'default.png', 'https://facebook.com/demaraigray', 'https://instagram.com/demaraigray', 'CLB000009', 1),
-- Cầu thủ 13
('Dominic', 'Calvert-Lewin', '1997-03-16', 187, 'England', 1, 'Forward', 9, 'default.png', 'https://facebook.com/dominiccalvertlewin', 'https://instagram.com/dominiccalvertlewin', 'CLB000009', 1),
-- Cầu thủ 14
('Neal', 'Maupay', '1996-08-14', 173, 'France', 0, 'Forward', 20, 'default.png', 'https://facebook.com/nealmaupay', 'https://instagram.com/nealmaupay', 'CLB000009', 1),
-- Cầu thủ 15
('Dwight', 'McNeil', '1999-11-22', 183, 'England', 1, 'Forward', 7, 'default.png', 'https://facebook.com/dwightmcneil', 'https://instagram.com/dwightmcneil', 'CLB000009', 1),
-- Cầu thủ 16
('Nathan', 'Patterson', '2001-10-16', 181, 'Scotland', 0, 'Defender', 3, 'default.png', 'https://facebook.com/nathanpatterson', 'https://instagram.com/nathanpatterson', 'CLB000009', 1),
-- Cầu thủ 17
('Ellis', 'Simms', '2001-01-05', 191, 'England', 1, 'Forward', 50, 'default.png', 'https://facebook.com/ellissimms', 'https://instagram.com/ellissimms', 'CLB000009', 1),
-- Cầu thủ 18
('Andros', 'Townsend', '1991-07-16', 181, 'England', 1, 'Midfielder', 10, 'default.png', 'https://facebook.com/androstownsend', 'https://instagram.com/androstownsend', 'CLB000009', 1),
-- Cầu thủ 19
('James', 'Garner', '2001-03-13', 182, 'England', 1, 'Midfielder', 37, 'default.png', 'https://facebook.com/jamesgarner', 'https://instagram.com/jamesgarner', 'CLB000009', 1),
-- Cầu thủ 20
('Ruben', 'Vinagre', '1999-04-09', 175, 'Portugal', 0, 'Defender', 29, 'default.png', 'https://facebook.com/rubenvinagre', 'https://instagram.com/rubenvinagre', 'CLB000009', 1),

--Fulham
-- Cầu thủ 1
('Bernd', 'Leno', '1992-03-04', 190, 'Germany', 0, 'Goalkeeper', 17, 'default.png', 'https://facebook.com/berndleno', 'https://instagram.com/berndleno', 'CLB000010', 1),
-- Cầu thủ 2
('Sasa', 'Lukic', '1996-08-13', 188, 'Serbia', 0, 'Midfielder', 8, 'default.png', 'https://facebook.com/sasalukic', 'https://instagram.com/sasalukic', 'CLB000010', 1),
-- Cầu thủ 3
('Antonee', 'Robinson', '1997-08-08', 178, 'United States', 0, 'Defender', 5, 'default.png', 'https://facebook.com/antoneerobinson', 'https://instagram.com/antoneerobinson', 'CLB000010', 1),
-- Cầu thủ 4
('Tim', 'Ream', '1987-10-05', 188, 'United States', 0, 'Defender', 13, 'default.png', 'https://facebook.com/timream', 'https://instagram.com/timream', 'CLB000010', 1),
-- Cầu thủ 5
('Tosin', 'Adarabioyo', '1997-09-24', 191, 'England', 1, 'Defender', 4, 'default.png', 'https://facebook.com/tosinadarabioyo', 'https://instagram.com/tosinadarabioyo', 'CLB000010', 1),
-- Cầu thủ 6
('Willian', 'Borges', '1988-08-09', 175, 'Brazil', 0, 'Winger', 20, 'default.png', 'https://facebook.com/willianborges', 'https://instagram.com/willianborges', 'CLB000010', 1),
-- Cầu thủ 7
('Andreas', 'Pereira', '1996-01-01', 175, 'Brazil', 0, 'Midfielder', 18, 'default.png', 'https://facebook.com/andreaspereira', 'https://instagram.com/andreaspereira', 'CLB000010', 1),
-- Cầu thủ 8
('Harry', 'Wilson', '1997-03-22', 175, 'Wales', 0, 'Winger', 7, 'default.png', 'https://facebook.com/harrywilson', 'https://instagram.com/harrywilson', 'CLB000010', 1),
-- Cầu thủ 9
('Carlos', 'Vinicius', '1995-03-25', 188, 'Brazil', 0, 'Forward', 9, 'default.png', 'https://facebook.com/carlosvinicius', 'https://instagram.com/carlosvinicius', 'CLB000010', 1),
-- Cầu thủ 10
('Bobby', 'Decordova-Reid', '1993-12-02', 175, 'Jamaica', 0, 'Forward', 14, 'default.png', 'https://facebook.com/bobbydecordova', 'https://instagram.com/bobbydecordova', 'CLB000010', 1),
-- Cầu thủ 11
('Joao', 'Palhinha', '1995-07-09', 185, 'Portugal', 0, 'Midfielder', 6, 'default.png', 'https://facebook.com/joaopalhinha', 'https://instagram.com/joaopalhinha', 'CLB000010', 1),
-- Cầu thủ 12
('Shane', 'Duffy', '1992-01-19', 190, 'Republic of Ireland', 0, 'Defender', 5, 'default.png', 'https://facebook.com/shaneduffy', 'https://instagram.com/shaneduffy', 'CLB000010', 1),
-- Cầu thủ 13
('Tom', 'Cairney', '1991-01-20', 175, 'England', 1, 'Midfielder', 10, 'default.png', 'https://facebook.com/tomcairney', 'https://instagram.com/tomcairney', 'CLB000010', 1),
-- Cầu thủ 14
('Kenny', 'Tete', '1996-10-09', 176, 'Netherlands', 0, 'Defender', 2, 'default.png', 'https://facebook.com/kennytete', 'https://instagram.com/kennytete', 'CLB000010', 1),
-- Cầu thủ 15
('Kevin', 'Mbabu', '1995-04-19', 180, 'Switzerland', 0, 'Defender', 23, 'default.png', 'https://facebook.com/kevinmbabu', 'https://instagram.com/kevinmbabu', 'CLB000010', 1),
-- Cầu thủ 16
('Vinicius', 'Júnior', '2000-07-12', 176, 'Brazil', 0, 'Forward', 21, 'default.png', 'https://facebook.com/viniciusjunior', 'https://instagram.com/viniciusjunior', 'CLB000010', 1),
-- Cầu thủ 17
('Pablo', 'Mari', '1993-08-31', 190, 'Spain', 0, 'Defender', 4, 'default.png', 'https://facebook.com/pablomari', 'https://instagram.com/pablomari', 'CLB000010', 1),
-- Cầu thủ 18
('António', 'Adán', '1987-05-13', 190, 'Portugal', 0, 'Goalkeeper', 1, 'default.png', 'https://facebook.com/antonioadan', 'https://instagram.com/antonioadan', 'CLB000010', 1),
-- Cầu thủ 19
('Issa', 'Diop', '1997-01-09', 190, 'France', 0, 'Defender', 6, 'default.png', 'https://facebook.com/issadiop', 'https://instagram.com/issadiop', 'CLB000010', 1),
-- Cầu thủ 20
('Neeskens', 'Kebano', '1992-11-10', 179, 'Congo DR', 0, 'Midfielder', 7, 'default.png', 'https://facebook.com/neeskenskebano', 'https://instagram.com/neeskenskebano', 'CLB000010', 1),

--Liverpool
-- Cầu thủ 1
('Alisson', 'Becker', '1992-10-02', 193, 'Brazil', 0, 'Goalkeeper', 1, 'default.png', 'https://facebook.com/alissonbecker', 'https://instagram.com/alissonbecker', 'CLB000011', 1),
-- Cầu thủ 2
('Virgil', 'van Dijk', '1991-07-08', 193, 'Netherlands', 0, 'Defender', 4, 'default.png', 'https://facebook.com/virgilvandijk', 'https://instagram.com/virgilvandijk', 'CLB000011', 1),
-- Cầu thủ 3
('Trent', 'Alexander-Arnold', '1998-10-07', 180, 'England', 1, 'Defender', 66, 'default.png', 'https://facebook.com/trentalexanderarnold', 'https://instagram.com/trentalexanderarnold', 'CLB000011', 1),
-- Cầu thủ 4
('Mohamed', 'Salah', '1992-06-15', 175, 'Egypt', 0, 'Forward', 11, 'default.png', 'https://facebook.com/mohamedsalah', 'https://instagram.com/mohamedsalah', 'CLB000011', 1),
-- Cầu thủ 5
('Sadio', 'Mane', '1992-04-10', 175, 'Senegal', 0, 'Forward', 10, 'default.png', 'https://facebook.com/sadiomane', 'https://instagram.com/sadiomane', 'CLB000011', 1),
-- Cầu thủ 6
('Roberto', 'Firmino', '1991-10-02', 181, 'Brazil', 0, 'Forward', 9, 'default.png', 'https://facebook.com/robertofirmino', 'https://instagram.com/robertofirmino', 'CLB000011', 1),
-- Cầu thủ 7
('Fabinho', 'Tavares', '1993-10-23', 188, 'Brazil', 0, 'Midfielder', 3, 'default.png', 'https://facebook.com/fabinho', 'https://instagram.com/fabinho', 'CLB000011', 1),
-- Cầu thủ 8
('James', 'Milner', '1986-01-04', 175, 'England', 1, 'Midfielder', 7, 'default.png', 'https://facebook.com/jamesmilner', 'https://instagram.com/jamesmilner', 'CLB000011', 1),
-- Cầu thủ 9
('Jordan', 'Henderson', '1990-06-17', 182, 'England', 1, 'Midfielder', 14, 'default.png', 'https://facebook.com/jordanhenderson', 'https://instagram.com/jordanhenderson', 'CLB000011', 1),
-- Cầu thủ 10
('Luis', 'Diaz', '1997-01-13', 179, 'Colombia', 0, 'Winger', 23, 'default.png', 'https://facebook.com/luisdiaz', 'https://instagram.com/luisdiaz', 'CLB000011', 1),
-- Cầu thủ 11
('Diogo', 'Jota', '1996-12-04', 179, 'Portugal', 0, 'Forward', 20, 'default.png', 'https://facebook.com/diogojota', 'https://instagram.com/diogojota', 'CLB000011', 1),
-- Cầu thủ 12
('Joe', 'Gomez', '1997-05-23', 183, 'England', 1, 'Defender', 12, 'default.png', 'https://facebook.com/joegomez', 'https://instagram.com/joegomez', 'CLB000011', 1),
-- Cầu thủ 13
('Joel', 'Matip', '1991-08-08', 195, 'Cameroon', 0, 'Defender', 32, 'default.png', 'https://facebook.com/joelmatip', 'https://instagram.com/joelmatip', 'CLB000011', 1),
-- Cầu thủ 14
('Ibrahima', 'Konate', '1999-05-25', 192, 'France', 0, 'Defender', 5, 'default.png', 'https://facebook.com/ibrahimakonate', 'https://instagram.com/ibrahimakonate', 'CLB000011', 1),
-- Cầu thủ 15
('Andrew', 'Robertson', '1994-03-11', 178, 'Scotland', 0, 'Defender', 26, 'default.png', 'https://facebook.com/andrewrobertson', 'https://instagram.com/andrewrobertson', 'CLB000011', 1),
-- Cầu thủ 16
('Kostas', 'Tsimikas', '1996-05-12', 179, 'Greece', 0, 'Defender', 21, 'default.png', 'https://facebook.com/kostatsimikas', 'https://instagram.com/kostatsimikas', 'CLB000011', 1),
-- Cầu thủ 17
('Thiago', 'Alcantara', '1991-04-11', 174, 'Spain', 0, 'Midfielder', 6, 'default.png', 'https://facebook.com/thiagoalcantara', 'https://instagram.com/thiagoalcantara', 'CLB000011', 1),
-- Cầu thủ 18
('Naby', 'Keita', '1995-02-10', 174, 'Guinea', 0, 'Midfielder', 8, 'default.png', 'https://facebook.com/nabykeita', 'https://instagram.com/nabykeita', 'CLB000011', 1),
-- Cầu thủ 19
('Alex', 'Oxlade-Chamberlain', '1993-08-15', 183, 'England', 1, 'Midfielder', 15, 'default.png', 'https://facebook.com/alexoxladechamberlain', 'https://instagram.com/alexoxladechamberlain', 'CLB000011', 1),
-- Cầu thủ 20
('Kelleher', 'Caoimhin', '1998-11-23', 183, 'Ireland', 0, 'Goalkeeper', 62, 'default.png', 'https://facebook.com/kellehercaoimhin', 'https://instagram.com/kellehercaoimhin', 'CLB000011', 1),

--Luton Town
-- Cầu thủ 1
('James', 'Bree', '1997-12-07', 183, 'England', 1, 'Defender', 2, 'default.png', 'https://facebook.com/jamesbree', 'https://instagram.com/jamesbree', 'CLB000012', 1),
-- Cầu thủ 2
('Allan', 'Campbell', '1998-11-04', 180, 'Scotland', 0, 'Midfielder', 6, 'default.png', 'https://facebook.com/allancampbell', 'https://instagram.com/allancampbell', 'CLB000012', 1),
-- Cầu thủ 3
('Tom', 'Lockyer', '1993-01-25', 185, 'Wales', 0, 'Defender', 4, 'default.png', 'https://facebook.com/tomlockyer', 'https://instagram.com/tomlockyer', 'CLB000012', 1),
-- Cầu thủ 4
('Elijah', 'Adebayo', '1998-12-20', 188, 'England', 1, 'Forward', 9, 'default.png', 'https://facebook.com/elijahadebayo', 'https://instagram.com/elijahadebayo', 'CLB000012', 1),
-- Cầu thủ 5
('Carlton', 'Morris', '1996-01-16', 187, 'England', 1, 'Forward', 10, 'default.png', 'https://facebook.com/carltonmorris', 'https://instagram.com/carltonmorris', 'CLB000012', 1),
-- Cầu thủ 6
('Pelly', 'Ruddock', '1994-09-25', 180, 'England', 1, 'Midfielder', 22, 'default.png', 'https://facebook.com/pellyruddock', 'https://instagram.com/pellyruddock', 'CLB000012', 1),
-- Cầu thủ 7
('Harry', 'Cornick', '1995-07-03', 176, 'England', 1, 'Forward', 11, 'default.png', 'https://facebook.com/harrycornick', 'https://instagram.com/harrycornick', 'CLB000012', 1),
-- Cầu thủ 8
('Dan', 'Potts', '1993-11-12', 180, 'England', 1, 'Defender', 3, 'default.png', 'https://facebook.com/danpotts', 'https://instagram.com/danpotts', 'CLB000012', 1),
-- Cầu thủ 9
('Nathanael', 'Ogbeta', '2001-11-27', 178, 'England', 1, 'Defender', 15, 'default.png', 'https://facebook.com/nathanaelogbeta', 'https://instagram.com/nathanaelogbeta', 'CLB000012', 1),
-- Cầu thủ 10
('Sonny', 'Bradley', '1992-10-25', 190, 'England', 1, 'Defender', 5, 'default.png', 'https://facebook.com/sonnybradley', 'https://instagram.com/sonnybradley', 'CLB000012', 1),
-- Cầu thủ 11
('Reece', 'Browning', '1997-01-19', 180, 'England', 1, 'Defender', 12, 'default.png', 'https://facebook.com/reecebrowning', 'https://instagram.com/reecebrowning', 'CLB000012', 1),
-- Cầu thủ 12
('Gabriel', 'Osho', '1997-03-15', 183, 'England', 1, 'Defender', 16, 'default.png', 'https://facebook.com/gabrielosho', 'https://instagram.com/gabrielosho', 'CLB000012', 1),
-- Cầu thủ 13
('Dion', 'Conroy', '1996-02-12', 181, 'Scotland', 0, 'Defender', 17, 'default.png', 'https://facebook.com/dionconroy', 'https://instagram.com/dionconroy', 'CLB000012', 1),
-- Cầu thủ 14
('Henri', 'Lansbury', '1990-10-12', 178, 'England', 1, 'Midfielder', 23, 'default.png', 'https://facebook.com/henrilansbury', 'https://instagram.com/henrilansbury', 'CLB000012', 1),
-- Cầu thủ 15
('Luke', 'Berry', '1995-12-13', 183, 'England', 1, 'Midfielder', 18, 'default.png', 'https://facebook.com/lukberry', 'https://instagram.com/lukberry', 'CLB000012', 1),
-- Cầu thủ 16
('Fred', 'Onyedinma', '1997-03-23', 180, 'England', 1, 'Midfielder', 8, 'default.png', 'https://facebook.com/fredonyedinma', 'https://instagram.com/fredonyedinma', 'CLB000012', 1),
-- Cầu thủ 17
('Jordan', 'Clark', '1993-07-29', 178, 'England', 1, 'Midfielder', 21, 'default.png', 'https://facebook.com/jordanclark', 'https://instagram.com/jordanclark', 'CLB000012', 1),
-- Cầu thủ 18
('Reece', 'Doyle', '2000-11-11', 175, 'England', 1, 'Midfielder', 20, 'default.png', 'https://facebook.com/reecedoyle', 'https://instagram.com/reecedoyle', 'CLB000012', 1),
-- Cầu thủ 19
('Liam', 'Manning', '1997-07-06', 183, 'England', 1, 'Defender', 13, 'default.png', 'https://facebook.com/liammanning', 'https://instagram.com/liammanning', 'CLB000012', 1),
-- Cầu thủ 20
('Reggie', 'Lambert', '1998-05-17', 175, 'England', 1, 'Forward', 7, 'default.png', 'https://facebook.com/reggielambert', 'https://instagram.com/reggielambert', 'CLB000012', 1),

--Manchester City
-- Cầu thủ 1
('Kyle', 'Walker', '1990-05-28', 183, 'England', 1, 'Defender', 2, 'default.png', 'https://facebook.com/kylewalker', 'https://instagram.com/kylewalker', 'CLB000013', 1),
-- Cầu thủ 2
('Phil', 'Foden', '2000-05-28', 171, 'England', 1, 'Midfielder', 47, 'default.png', 'https://facebook.com/philipfoden', 'https://instagram.com/philipfoden', 'CLB000013', 1),
-- Cầu thủ 3
('Jack', 'Grealish', '1995-09-10', 180, 'England', 1, 'Midfielder', 10, 'default.png', 'https://facebook.com/jackgrealish', 'https://instagram.com/jackgrealish', 'CLB000013', 1),
-- Cầu thủ 4
('Riyad', 'Mahrez', '1991-02-21', 179, 'Algeria', 0, 'Forward', 26, 'default.png', 'https://facebook.com/riyadmahrez', 'https://instagram.com/riyadmahrez', 'CLB000013', 1),
-- Cầu thủ 5
('Bernardo', 'Silva', '1994-08-10', 173, 'Portugal', 0, 'Midfielder', 20, 'default.png', 'https://facebook.com/bernardosilva', 'https://instagram.com/bernardosilva', 'CLB000013', 1),
-- Cầu thủ 6
('Kevin', 'De Bruyne', '1991-06-28', 181, 'Belgium', 0, 'Midfielder', 17, 'default.png', 'https://facebook.com/kevindebruyne', 'https://instagram.com/kevindebruyne', 'CLB000013', 1),
-- Cầu thủ 7
('Erling', 'Haaland', '2000-07-21', 194, 'Norway', 0, 'Forward', 9, 'default.png', 'https://facebook.com/erlinghaaland', 'https://instagram.com/erlinghaaland', 'CLB000013', 1),
-- Cầu thủ 8
('Rodri', 'Hernández', '1996-06-22', 188, 'Spain', 0, 'Midfielder', 16, 'default.png', 'https://facebook.com/rodrihernandez', 'https://instagram.com/rodrihernandez', 'CLB000013', 1),
-- Cầu thủ 9
('Aymeric', 'Laporte', '1994-05-27', 188, 'Spain', 0, 'Defender', 14, 'default.png', 'https://facebook.com/aymericlaporte', 'https://instagram.com/aymericlaporte', 'CLB000013', 1),
-- Cầu thủ 10
('John', 'Stones', '1994-05-28', 188, 'England', 1, 'Defender', 5, 'default.png', 'https://facebook.com/johnstones', 'https://instagram.com/johnstones', 'CLB000013', 1),
-- Cầu thủ 11
('Nathan', 'Aké', '1995-02-18', 183, 'Netherlands', 0, 'Defender', 6, 'default.png', 'https://facebook.com/nathanake', 'https://instagram.com/nathanake', 'CLB000013', 1),
-- Cầu thủ 12
('Ilkay', 'Gündogan', '1990-10-24', 180, 'Germany', 0, 'Midfielder', 8, 'default.png', 'https://facebook.com/ilkaygundogan', 'https://instagram.com/ilkaygundogan', 'CLB000013', 1),
-- Cầu thủ 13
('João', 'Cancelo', '1994-05-27', 183, 'Portugal', 0, 'Defender', 7, 'default.png', 'https://facebook.com/joaocancelo', 'https://instagram.com/joaocancelo', 'CLB000013', 1),
-- Cầu thủ 14
('Zack', 'Steffen', '1995-04-02', 188, 'USA', 0, 'Goalkeeper', 13, 'default.png', 'https://facebook.com/zacksteffen', 'https://instagram.com/zacksteffen', 'CLB000013', 1),
-- Cầu thủ 15
('Kalvin', 'Phillips', '1995-12-02', 177, 'England', 1, 'Midfielder', 4, 'default.png', 'https://facebook.com/kalvinphillips', 'https://instagram.com/kalvinphillips', 'CLB000013', 1),
-- Cầu thủ 16
('Philippe', 'Coutinho', '1992-06-12', 172, 'Brazil', 0, 'Midfielder', 14, 'default.png', 'https://facebook.com/philippecoutinho', 'https://instagram.com/philippecoutinho', 'CLB000013', 1),
-- Cầu thủ 17
('Sergio', 'Agüero', '1988-06-02', 173, 'Argentina', 0, 'Forward', 10, 'default.png', 'https://facebook.com/sergioaguero', 'https://instagram.com/sergioaguero', 'CLB000013', 1),
-- Cầu thủ 18
('Gabriel', 'Jesus', '1997-04-03', 175, 'Brazil', 0, 'Forward', 9, 'default.png', 'https://facebook.com/gabrieljesus', 'https://instagram.com/gabrieljesus', 'CLB000013', 1),
-- Cầu thủ 19
('Benjamin', 'Mendy', '1994-07-17', 184, 'France', 0, 'Defender', 22, 'default.png', 'https://facebook.com/benjaminmendy', 'https://instagram.com/benjaminmendy', 'CLB000013', 1),
-- Cầu thủ 20
('Fernando', 'Rodrigues', '1995-08-15', 183, 'Brazil', 0, 'Defender', 23, 'default.png', 'https://facebook.com/fernandorodrigues', 'https://instagram.com/fernandorodrigues', 'CLB000013', 1),

--Manchester United
-- Cầu thủ 1
('David', 'De Gea', '1990-11-07', 192, 'Spain', 0, 'Goalkeeper', 1, 'default.png', 'https://facebook.com/daviddegea', 'https://instagram.com/daviddegea', 'CLB000014', 1),
-- Cầu thủ 2
('Harry', 'Maguire', '1993-03-05', 194, 'England', 1, 'Defender', 5, 'default.png', 'https://facebook.com/harrymaguire', 'https://instagram.com/harrymaguire', 'CLB000014', 1),
-- Cầu thủ 3
('Bruno', 'Fernandes', '1994-09-08', 179, 'Portugal', 0, 'Midfielder', 18, 'default.png', 'https://facebook.com/brunofernandes', 'https://instagram.com/brunofernandes', 'CLB000014', 1),
-- Cầu thủ 4
('Marcus', 'Rashford', '1997-10-31', 180, 'England', 1, 'Forward', 10, 'default.png', 'https://facebook.com/marcusrashford', 'https://instagram.com/marcusrashford', 'CLB000014', 1),
-- Cầu thủ 5
('Jadon', 'Sancho', '2000-03-25', 180, 'England', 1, 'Forward', 25, 'default.png', 'https://facebook.com/jadonsancho', 'https://instagram.com/jadonsancho', 'CLB000014', 1),
-- Cầu thủ 6
('Cristiano', 'Ronaldo', '1985-02-05', 187, 'Portugal', 0, 'Forward', 7, 'default.png', 'https://facebook.com/cristianoronaldo', 'https://instagram.com/cristianoronaldo', 'CLB000014', 1),
-- Cầu thủ 7
('Paul', 'Pogba', '1993-03-15', 191, 'France', 0, 'Midfielder', 6, 'default.png', 'https://facebook.com/paulpogba', 'https://instagram.com/paulpogba', 'CLB000014', 1),
-- Cầu thủ 8
('Raphaël', 'Varane', '1993-04-25', 191, 'France', 0, 'Defender', 19, 'default.png', 'https://facebook.com/raphaelvarane', 'https://instagram.com/raphaelvarane', 'CLB000014', 1),
-- Cầu thủ 9
('Anthony', 'Martial', '1995-12-05', 181, 'France', 0, 'Forward', 9, 'default.png', 'https://facebook.com/anthonymartial', 'https://instagram.com/anthonymartial', 'CLB000014', 1),
-- Cầu thủ 10
('Luke', 'Shaw', '1995-07-12', 185, 'England', 1, 'Defender', 23, 'default.png', 'https://facebook.com/lukeshaw', 'https://instagram.com/lukeshaw', 'CLB000014', 1),
-- Cầu thủ 11
('Edinson', 'Cavani', '1987-02-14', 184, 'Uruguay', 0, 'Forward', 21, 'default.png', 'https://facebook.com/edinsoncavani', 'https://instagram.com/edinsoncavani', 'CLB000014', 1),
-- Cầu thủ 12
('Aaron', 'Wan-Bissaka', '1997-11-26', 183, 'England', 1, 'Defender', 29, 'default.png', 'https://facebook.com/aaronwanbissaka', 'https://instagram.com/aaronwanbissaka', 'CLB000014', 1),
-- Cầu thủ 13
('Victor', 'Lindelöf', '1994-07-17', 187, 'Sweden', 0, 'Defender', 2, 'default.png', 'https://facebook.com/victorlindelof', 'https://instagram.com/victorlindelof', 'CLB000014', 1),
-- Cầu thủ 14
('Donny', 'van de Beek', '1997-04-18', 183, 'Netherlands', 0, 'Midfielder', 34, 'default.png', 'https://facebook.com/donnyvandebeek', 'https://instagram.com/donnyvandebeek', 'CLB000014', 1),
-- Cầu thủ 15
('Nemanja', 'Matic', '1988-08-01', 194, 'Serbia', 0, 'Midfielder', 31, 'default.png', 'https://facebook.com/nemanjamatic', 'https://instagram.com/nemanjamatic', 'CLB000014', 1),
-- Cầu thủ 16
('Juan', 'Mata', '1988-04-28', 170, 'Spain', 0, 'Midfielder', 8, 'default.png', 'https://facebook.com/juanmata', 'https://instagram.com/juanmata', 'CLB000014', 1),
-- Cầu thủ 17
('Alex', 'Telles', '1992-12-15', 178, 'Brazil', 0, 'Defender', 27, 'default.png', 'https://facebook.com/alextelles', 'https://instagram.com/alextelles', 'CLB000014', 1),
-- Cầu thủ 18
('Daniel', 'James', '1997-11-10', 171, 'Wales', 0, 'Forward', 20, 'default.png', 'https://facebook.com/danieljames', 'https://instagram.com/danieljames', 'CLB000014', 1),
-- Cầu thủ 19
('Andreas', 'Pereira', '1996-01-01', 179, 'Brazil', 0, 'Midfielder', 15, 'default.png', 'https://facebook.com/andreaspereira', 'https://instagram.com/andreaspereira', 'CLB000014', 1),
-- Cầu thủ 20
('Teden', 'Mengi', '2002-01-30', 185, 'England', 1, 'Defender', 43, 'default.png', 'https://facebook.com/tedenmengi', 'https://instagram.com/tedenmengi', 'CLB000014', 1),

--Newcastle United
-- Cầu thủ 1
('Nick', 'Pope', '1992-04-19', 191, 'England', 1, 'Goalkeeper', 22, 'default.png', 'https://facebook.com/nickpope', 'https://instagram.com/nickpope', 'CLB000015', 1),
-- Cầu thủ 2
('Kieran', 'Trippier', '1990-09-19', 178, 'England', 1, 'Defender', 2, 'default.png', 'https://facebook.com/kierantrippier', 'https://instagram.com/kierantrippier', 'CLB000015', 1),
-- Cầu thủ 3
('Sven', 'Botman', '2000-01-12', 190, 'Netherlands', 0, 'Defender', 4, 'default.png', 'https://facebook.com/svenbotman', 'https://instagram.com/svenbotman', 'CLB000015', 1),
-- Cầu thủ 4
('Fabian', 'Schar', '1991-12-20', 186, 'Switzerland', 0, 'Defender', 5, 'default.png', 'https://facebook.com/fabianschar', 'https://instagram.com/fabianschar', 'CLB000015', 1),
-- Cầu thủ 5
('Dan', 'Burn', '1992-11-09', 198, 'England', 1, 'Defender', 33, 'default.png', 'https://facebook.com/danburn', 'https://instagram.com/danburn', 'CLB000015', 1),
-- Cầu thủ 6
('Bruno', 'Guimarães', '1997-11-16', 178, 'Brazil', 0, 'Midfielder', 39, 'default.png', 'https://facebook.com/brunoguimaraes', 'https://instagram.com/brunoguimaraes', 'CLB000015', 1),
-- Cầu thủ 7
('Joe', 'Willock', '1999-08-20', 183, 'England', 1, 'Midfielder', 28, 'default.png', 'https://facebook.com/joewillock', 'https://instagram.com/joewillock', 'CLB000015', 1),
-- Cầu thủ 8
('Sean', 'Longstaff', '1997-10-30', 183, 'England', 1, 'Midfielder', 36, 'default.png', 'https://facebook.com/seanlongstaff', 'https://instagram.com/seanlongstaff', 'CLB000015', 1),
-- Cầu thủ 9
('Miguel', 'Almirón', '1994-02-10', 170, 'Paraguay', 0, 'Forward', 24, 'default.png', 'https://facebook.com/miguelalmiron', 'https://instagram.com/miguelalmiron', 'CLB000015', 1),
-- Cầu thủ 10
('Allan', 'Saint-Maximin', '1997-03-12', 180, 'France', 0, 'Forward', 10, 'default.png', 'https://facebook.com/allan.saintmaximin', 'https://instagram.com/allan.saintmaximin', 'CLB000015', 1),
-- Cầu thủ 11
('Alexander', 'Isak', '1999-09-21', 192, 'Sweden', 0, 'Forward', 14, 'default.png', 'https://facebook.com/alexanderisak', 'https://instagram.com/alexanderisak', 'CLB000015', 1),
-- Cầu thủ 12
('Callum', 'Wilson', '1992-02-27', 183, 'England', 1, 'Forward', 9, 'default.png', 'https://facebook.com/callumwilson', 'https://instagram.com/callumwilson', 'CLB000015', 1),
-- Cầu thủ 13
('Joelinton', 'Andrade', '1996-08-14', 189, 'Brazil', 0, 'Midfielder', 7, 'default.png', 'https://facebook.com/joelinton', 'https://instagram.com/joelinton', 'CLB000015', 1),
-- Cầu thủ 14
('Matt', 'Targett', '1996-01-02', 180, 'England', 1, 'Defender', 13, 'default.png', 'https://facebook.com/mattargett', 'https://instagram.com/mattargett', 'CLB000015', 1),
-- Cầu thủ 15
('Ryan', 'Fraser', '1994-02-24', 165, 'Scotland', 0, 'Forward', 21, 'default.png', 'https://facebook.com/ryanfraser', 'https://instagram.com/ryanfraser', 'CLB000015', 1),
-- Cầu thủ 16
('Loris', 'Karius', '1993-06-22', 190, 'Germany', 0, 'Goalkeeper', 18, 'default.png', 'https://facebook.com/loriskarius', 'https://instagram.com/loriskarius', 'CLB000015', 1),
-- Cầu thủ 17
('Jamaal', 'Lascelles', '1993-11-11', 188, 'England', 1, 'Defender', 6, 'default.png', 'https://facebook.com/jamaallascelles', 'https://instagram.com/jamaallascelles', 'CLB000015', 1),
-- Cầu thủ 18
('Matt', 'Ritchie', '1989-09-10', 175, 'Scotland', 0, 'Midfielder', 11, 'default.png', 'https://facebook.com/mattritchie', 'https://instagram.com/mattritchie', 'CLB000015', 1),
-- Cầu thủ 19
('Dan', 'Langley', '2001-06-12', 189, 'England', 1, 'Goalkeeper', 0, 'default.png', 'https://facebook.com/danlangley', 'https://instagram.com/danlangley', 'CLB000015', 1),
-- Cầu thủ 20
('Sven', 'Botman', '2000-01-12', 190, 'Netherlands', 0, 'Defender', 4, 'default.png', 'https://facebook.com/svenbotman', 'https://instagram.com/svenbotman', 'CLB000015', 1),

--Nottingham Forest
-- Cầu thủ 1
('Keylor', 'Navas', '1986-12-15', 185, 'Costa Rica', 0, 'Goalkeeper', 1, 'default.png', 'https://facebook.com/keylornavas', 'https://instagram.com/keylornavas', 'CLB000016', 1),
-- Cầu thủ 2
('Willy', 'Boly', '1990-02-03', 188, 'Ivory Coast', 0, 'Defender', 15, 'default.png', 'https://facebook.com/willyboly', 'https://instagram.com/willyboly', 'CLB000016', 1),
-- Cầu thủ 3
('Renan', 'Lodi', '1998-04-08', 175, 'Brazil', 0, 'Defender', 3, 'default.png', 'https://facebook.com/renanlodi', 'https://instagram.com/renanlodi', 'CLB000016', 1),
-- Cầu thủ 4
('Neco', 'Williams', '2001-04-13', 173, 'Wales', 0, 'Defender', 22, 'default.png', 'https://facebook.com/necowilliams', 'https://instagram.com/necowilliams', 'CLB000016', 1),
-- Cầu thủ 5
('Scott', 'McKenna', '1996-11-15', 183, 'Scotland', 0, 'Defender', 26, 'default.png', 'https://facebook.com/scottmckenna', 'https://instagram.com/scottmckenna', 'CLB000016', 1),
-- Cầu thủ 6
('Orel', 'Mangala', '1998-02-13', 180, 'France', 0, 'Midfielder', 6, 'default.png', 'https://facebook.com/orel', 'https://instagram.com/orel', 'CLB000016', 1),
-- Cầu thủ 7
('Morgan', 'Gibbs-White', '2000-11-02', 183, 'England', 1, 'Midfielder', 10, 'default.png', 'https://facebook.com/morgangibbswhite', 'https://instagram.com/morgangibbswhite', 'CLB000016', 1),
-- Cầu thủ 8
('Jack', 'Colback', '1989-10-24', 180, 'England', 1, 'Midfielder', 14, 'default.png', 'https://facebook.com/jackcolback', 'https://instagram.com/jackcolback', 'CLB000016', 1),
-- Cầu thủ 9
('John', 'Bostock', '1992-01-15', 180, 'England', 1, 'Midfielder', 34, 'default.png', 'https://facebook.com/johnbostock', 'https://instagram.com/johnbostock', 'CLB000016', 1),
-- Cầu thủ 10
('Taiwo', 'Awoniyi', '1997-08-12', 184, 'Nigeria', 0, 'Forward', 45, 'default.png', 'https://facebook.com/taiwoawoniyi', 'https://instagram.com/taiwoawoniyi', 'CLB000016', 1),
-- Cầu thủ 11
('Brennan', 'Johnson', '2001-01-23', 183, 'Wales', 0, 'Forward', 20, 'default.png', 'https://facebook.com/brennanjohnson', 'https://instagram.com/brennanjohnson', 'CLB000016', 1),
-- Cầu thủ 12
('Sam', 'Surridge', '1998-03-23', 183, 'England', 1, 'Forward', 24, 'default.png', 'https://facebook.com/samsurridge', 'https://instagram.com/samsurridge', 'CLB000016', 1),
-- Cầu thủ 13
('Emmanuel', 'Dennis', '1997-11-15', 176, 'Nigeria', 0, 'Forward', 25, 'default.png', 'https://facebook.com/emmanueldennis', 'https://instagram.com/emmanueldennis', 'CLB000016', 1),
-- Cầu thủ 14
('Chris', 'Wood', '1991-01-07', 190, 'New Zealand', 0, 'Forward', 39, 'default.png', 'https://facebook.com/chriswood', 'https://instagram.com/chriswood', 'CLB000016', 1),
-- Cầu thủ 15
('Morgan', 'Rogers', '2002-11-25', 173, 'England', 1, 'Forward', 29, 'default.png', 'https://facebook.com/morganrogers', 'https://instagram.com/morganrogers', 'CLB000016', 1),
-- Cầu thủ 16
('Harry', 'Toffolo', '1995-08-03', 175, 'England', 1, 'Defender', 11, 'default.png', 'https://facebook.com/harrytoffolo', 'https://instagram.com/harrytoffolo', 'CLB000016', 1),
-- Cầu thủ 17
('Brandon', 'Williams', '2000-09-11', 174, 'England', 1, 'Defender', 38, 'default.png', 'https://facebook.com/brandonwilliams', 'https://instagram.com/brandonwilliams', 'CLB000016', 1),
-- Cầu thủ 18
('Renato', 'Sanches', '1997-08-18', 180, 'Portugal', 0, 'Midfielder', 8, 'default.png', 'https://facebook.com/renatosanches', 'https://instagram.com/renatosanches', 'CLB000016', 1),
-- Cầu thủ 19
('Felipe', 'González', '1989-11-16', 189, 'Brazil', 0, 'Defender', 33, 'default.png', 'https://facebook.com/felipegonzalez', 'https://instagram.com/felipegonzalez', 'CLB000016', 1),
-- Cầu thủ 20
('Wayne', 'Hennessey', '1987-01-24', 196, 'Wales', 0, 'Goalkeeper', 13, 'default.png', 'https://facebook.com/waynehennessey', 'https://instagram.com/waynehennessey', 'CLB000016', 1),

--Sheffield United
-- Cầu thủ 1
('Wesley', 'Foderingham', '1991-01-14', 191, 'England', 1, 'Goalkeeper', 1, 'default.png', 'https://facebook.com/wesleyfoderingham', 'https://instagram.com/wesleyfoderingham', 'CLB000017', 1),
-- Cầu thủ 2
('Daniel', 'Jebbison', '2002-08-12', 180, 'England', 1, 'Forward', 20, 'default.png', 'https://facebook.com/danieljebbison', 'https://instagram.com/danieljebbison', 'CLB000017', 1),
-- Cầu thủ 3
('Billy', 'Sharp', '1986-02-05', 180, 'England', 1, 'Forward', 10, 'default.png', 'https://facebook.com/billysharp', 'https://instagram.com/billysharp', 'CLB000017', 1),
-- Cầu thủ 4
('Oliver', 'McBurnie', '1996-06-12', 190, 'Scotland', 0, 'Forward', 9, 'default.png', 'https://facebook.com/olivermcburnie', 'https://instagram.com/olivermcburnie', 'CLB000017', 1),
-- Cầu thủ 5
('John', 'Fleck', '1991-03-24', 174, 'Scotland', 0, 'Midfielder', 4, 'default.png', 'https://facebook.com/johnfleck', 'https://instagram.com/johnfleck', 'CLB000017', 1),
-- Cầu thủ 6
('Sander', 'Berge', '1998-02-14', 190, 'Norway', 0, 'Midfielder', 8, 'default.png', 'https://facebook.com/sanderberge', 'https://instagram.com/sanderberge', 'CLB000017', 1),
-- Cầu thủ 7
('Enda', 'Stevens', '1990-07-09', 178, 'Ireland', 0, 'Defender', 3, 'default.png', 'https://facebook.com/endastevens', 'https://instagram.com/endastevens', 'CLB000017', 1),
-- Cầu thủ 8
('Jack', 'Robinson', '1993-09-15', 183, 'England', 1, 'Defender', 6, 'default.png', 'https://facebook.com/jackrobinson', 'https://instagram.com/jackrobinson', 'CLB000017', 1),
-- Cầu thủ 9
('Chris', 'Basham', '1988-07-20', 182, 'England', 1, 'Defender', 12, 'default.png', 'https://facebook.com/chrisbasham', 'https://instagram.com/chrisbasham', 'CLB000017', 1),
-- Cầu thủ 10
('Jack', 'O''Connell', '1993-11-15', 183, 'England', 1, 'Defender', 5, 'default.png', 'https://facebook.com/jackoconnell', 'https://instagram.com/jackoconnell', 'CLB000017', 1),
-- Cầu thủ 11
('Max', 'Lowe', '1997-06-21', 175, 'England', 1, 'Defender', 7, 'default.png', 'https://facebook.com/maxlowe', 'https://instagram.com/maxlowe', 'CLB000017', 1),
-- Cầu thủ 12
('Ben', 'Osborn', '1994-11-05', 178, 'England', 1, 'Midfielder', 11, 'default.png', 'https://facebook.com/benosborn', 'https://instagram.com/benosborn', 'CLB000017', 1),
-- Cầu thủ 13
('Lys', 'Mousset', '1996-02-08', 185, 'France', 0, 'Forward', 7, 'default.png', 'https://facebook.com/lysmousset', 'https://instagram.com/lysmousset', 'CLB000017', 1),
-- Cầu thủ 14
('Rhian', 'Brewster', '2000-04-01', 183, 'England', 1, 'Forward', 19, 'default.png', 'https://facebook.com/rhianbrewster', 'https://instagram.com/rhianbrewster', 'CLB000017', 1),
-- Cầu thủ 15
('Fleury', 'Garnier', '1997-12-04', 187, 'France', 0, 'Defender', 16, 'default.png', 'https://facebook.com/fleurygarnier', 'https://instagram.com/fleurygarnier', 'CLB000017', 1),
-- Cầu thủ 16
('Morgan', 'Gibbs-White', '2000-11-02', 183, 'England', 1, 'Midfielder', 10, 'default.png', 'https://facebook.com/morgangibbswhite', 'https://instagram.com/morgangibbswhite', 'CLB000017', 1),
-- Cầu thủ 17
('Billy', 'Reid', '1999-02-07', 173, 'England', 1, 'Midfielder', 4, 'default.png', 'https://facebook.com/billyreid', 'https://instagram.com/billyreid', 'CLB000017', 1),
-- Cầu thủ 18
('Jayden', 'Bogle', '2000-11-27', 180, 'England', 1, 'Defender', 22, 'default.png', 'https://facebook.com/jaydenbogle', 'https://instagram.com/jaydenbogle', 'CLB000017', 1),
-- Cầu thủ 19
('Oliver', 'Norwood', '1991-04-04', 175, 'Northern Ireland', 0, 'Midfielder', 16, 'default.png', 'https://facebook.com/olivernorwood', 'https://instagram.com/olivernorwood', 'CLB000017', 1),
-- Cầu thủ 20
('David', 'McGoldrick', '1987-11-29', 178, 'Ireland', 0, 'Forward', 17, 'default.png', 'https://facebook.com/davidmcgoldrick', 'https://instagram.com/davidmcgoldrick', 'CLB000017', 1),

--Tottenham Hotspur
-- Cầu thủ 1
('Hugo', 'Lloris', '1986-12-26', 188, 'France', 0, 'Goalkeeper', 1, 'default.png', 'https://facebook.com/hugolloris', 'https://instagram.com/hugolloris', 'CLB000018', 1),
-- Cầu thủ 2
('Harry', 'Kane', '1993-07-28', 188, 'England', 1, 'Forward', 10, 'default.png', 'https://facebook.com/harrykane', 'https://instagram.com/harrykane', 'CLB000018', 1),
-- Cầu thủ 3
('Son', 'Heung-min', '1992-07-08', 183, 'South Korea', 0, 'Forward', 7, 'default.png', 'https://facebook.com/sonheungmin', 'https://instagram.com/sonheungmin', 'CLB000018', 1),
-- Cầu thủ 4
('Richarlison', '', '1997-05-10', 180, 'Brazil', 0, 'Forward', 9, 'default.png', 'https://facebook.com/richarlison', 'https://instagram.com/richarlison', 'CLB000018', 1),
-- Cầu thủ 5
('Dejan', 'Kulusevski', '2000-04-25', 186, 'Sweden', 0, 'Forward', 21, 'default.png', 'https://facebook.com/dejankulusevski', 'https://instagram.com/dejankulusevski', 'CLB000018', 1),
-- Cầu thủ 6
('Pierre', 'Emile Højbjerg', '1996-08-05', 182, 'Denmark', 0, 'Midfielder', 5, 'default.png', 'https://facebook.com/pierreemilehojbjerg', 'https://instagram.com/pierreemilehojbjerg', 'CLB000018', 1),
-- Cầu thủ 7
('Oliver', 'Skipp', '2000-09-16', 183, 'England', 1, 'Midfielder', 4, 'default.png', 'https://facebook.com/oliverskipp', 'https://instagram.com/oliverskipp', 'CLB000018', 1),
-- Cầu thủ 8
('Rodrigo', 'Bentancur', '1997-06-25', 184, 'Uruguay', 0, 'Midfielder', 30, 'default.png', 'https://facebook.com/rodrigobentancur', 'https://instagram.com/rodrigobentancur', 'CLB000018', 1),
-- Cầu thủ 9
('Emerson', 'Royal', '1999-01-14', 181, 'Brazil', 0, 'Defender', 12, 'default.png', 'https://facebook.com/emersonroyal', 'https://instagram.com/emersonroyal', 'CLB000018', 1),
-- Cầu thủ 10
('Clement', 'Lenglet', '1995-06-17', 187, 'France', 0, 'Defender', 34, 'default.png', 'https://facebook.com/clementlenglet', 'https://instagram.com/clementlenglet', 'CLB000018', 1),
-- Cầu thủ 11
('Ben', 'Davis', '1993-04-24', 183, 'Wales', 0, 'Defender', 33, 'default.png', 'https://facebook.com/bendavis', 'https://instagram.com/bendavis', 'CLB000018', 1),
-- Cầu thủ 12
('Cristian', 'Romero', '1998-04-27', 185, 'Argentina', 0, 'Defender', 17, 'default.png', 'https://facebook.com/cristianromero', 'https://instagram.com/cristianromero', 'CLB000018', 1),
-- Cầu thủ 13
('Japhet', 'Tanganga', '1999-03-31', 183, 'England', 1, 'Defender', 25, 'default.png', 'https://facebook.com/japhettanganga', 'https://instagram.com/japhettanganga', 'CLB000018', 1),
-- Cầu thủ 14
('Ryan', 'Sessegnon', '2000-05-18', 178, 'England', 1, 'Defender', 19, 'default.png', 'https://facebook.com/ryansessegnon', 'https://instagram.com/ryansessegnon', 'CLB000018', 1),
-- Cầu thủ 15
('Davinson', 'Sánchez', '1996-06-12', 187, 'Colombia', 0, 'Defender', 6, 'default.png', 'https://facebook.com/davinsonsanchez', 'https://instagram.com/davinsonsanchez', 'CLB000018', 1),
-- Cầu thủ 16
('Matt', 'Doherty', '1992-01-16', 182, 'Ireland', 0, 'Defender', 2, 'default.png', 'https://facebook.com/mattdoherty', 'https://instagram.com/mattdoherty', 'CLB000018', 1),
-- Cầu thủ 17
('Hwang', 'Hee-chan', '1996-02-26', 179, 'South Korea', 0, 'Forward', 18, 'default.png', 'https://facebook.com/hwangheechan', 'https://instagram.com/hwangheechan', 'CLB000018', 1),
-- Cầu thủ 18
('Tanguy', 'Ndombele', '1996-12-03', 180, 'France', 0, 'Midfielder', 28, 'default.png', 'https://facebook.com/tanguyndombele', 'https://instagram.com/tanguyndombele', 'CLB000018', 1),
-- Cầu thủ 19
('Harry', 'Winks', '1996-02-02', 180, 'England', 1, 'Midfielder', 8, 'default.png', 'https://facebook.com/harrywinks', 'https://instagram.com/harrywinks', 'CLB000018', 1),
-- Cầu thủ 20
('Alfie', 'Whiteman', '1997-06-10', 187, 'England', 1, 'Goalkeeper', 40, 'default.png', 'https://facebook.com/alfiewhiteman', 'https://instagram.com/alfiewhiteman', 'CLB000018', 1),

--West Ham United
-- Cầu thủ 1
('Lukasz', 'Fabianski', '1985-04-18', 193, 'Poland', 0, 'Goalkeeper', 1, 'default.png', 'https://facebook.com/lukaszfabianski', 'https://instagram.com/lukaszfabianski', 'CLB000019', 1),
-- Cầu thủ 2
('Declan', 'Rice', '1999-01-14', 185, 'England', 1, 'Midfielder', 41, 'default.png', 'https://facebook.com/declanrice', 'https://instagram.com/declanrice', 'CLB000019', 1),
-- Cầu thủ 3
('Jarrod', 'Bowen', '1996-12-20', 175, 'England', 1, 'Forward', 20, 'default.png', 'https://facebook.com/jarrodbowen', 'https://instagram.com/jarrodbowen', 'CLB000019', 1),
-- Cầu thủ 4
('Michail', 'Antonio', '1990-03-28', 180, 'Jamaica', 0, 'Forward', 30, 'default.png', 'https://facebook.com/michailantonio', 'https://instagram.com/michailantonio', 'CLB000019', 1),
-- Cầu thủ 5
('Tomas', 'Soucek', '1995-02-27', 193, 'Czech Republic', 0, 'Midfielder', 28, 'default.png', 'https://facebook.com/tomassoucek', 'https://instagram.com/tomassoucek', 'CLB000019', 1),
-- Cầu thủ 6
('Vladimir', 'Coufal', '1992-08-22', 183, 'Czech Republic', 0, 'Defender', 5, 'default.png', 'https://facebook.com/vladimircoufal', 'https://instagram.com/vladimircoufal', 'CLB000019', 1),
-- Cầu thủ 7
('Aaron', 'Cresswell', '1990-12-15', 177, 'England', 1, 'Defender', 3, 'default.png', 'https://facebook.com/aaroncresswell', 'https://instagram.com/aaroncresswell', 'CLB000019', 1),
-- Cầu thủ 8
('Pablo', 'Fornals', '1996-02-22', 178, 'Spain', 0, 'Midfielder', 18, 'default.png', 'https://facebook.com/pablofornals', 'https://instagram.com/pablofornals', 'CLB000019', 1),
-- Cầu thủ 9
('Ben', 'Johnson', '2000-01-01', 180, 'England', 1, 'Defender', 2, 'default.png', 'https://facebook.com/benjohnson', 'https://instagram.com/benjohnson', 'CLB000019', 1),
-- Cầu thủ 10
('Andriy', 'Yarmolenko', '1989-10-23', 189, 'Ukraine', 0, 'Forward', 7, 'default.png', 'https://facebook.com/andriyyarmolenko', 'https://instagram.com/andriyyarmolenko', 'CLB000019', 1),
-- Cầu thủ 11
('Alphonse', 'Areola', '1993-02-27', 196, 'France', 0, 'Goalkeeper', 13, 'default.png', 'https://facebook.com/alphonseareola', 'https://instagram.com/alphonseareola', 'CLB000019', 1),
-- Cầu thủ 12
('Issa', 'Diop', '1997-01-09', 192, 'France', 0, 'Defender', 23, 'default.png', 'https://facebook.com/issadiop', 'https://instagram.com/issadiop', 'CLB000019', 1),
-- Cầu thủ 13
('Saïd', 'Benrahma', '1995-08-10', 175, 'Algeria', 0, 'Forward', 9, 'default.png', 'https://facebook.com/saidbenrahma', 'https://instagram.com/saidbenrahma', 'CLB000019', 1),
-- Cầu thủ 14
('Mark', 'Noble', '1987-05-08', 178, 'England', 1, 'Midfielder', 16, 'default.png', 'https://facebook.com/marknoble', 'https://instagram.com/marknoble', 'CLB000019', 1),
-- Cầu thủ 15
('Manuel', 'Lanzini', '1993-02-15', 170, 'Argentina', 0, 'Midfielder', 10, 'default.png', 'https://facebook.com/manuel Lanzini', 'https://instagram.com/manuel Lanzini', 'CLB000019', 1),
-- Cầu thủ 16
('Kurt', 'Zouma', '1994-10-27', 190, 'France', 0, 'Defender', 4, 'default.png', 'https://facebook.com/kurtzouma', 'https://instagram.com/kurtzouma', 'CLB000019', 1),
-- Cầu thủ 17
('Tom', 'Ince', '1992-01-30', 176, 'England', 1, 'Midfielder', 21, 'default.png', 'https://facebook.com/tomince', 'https://instagram.com/tomince', 'CLB000019', 1),
-- Cầu thủ 18
('Flynn', 'Downes', '1999-01-22', 180, 'England', 1, 'Midfielder', 12, 'default.png', 'https://facebook.com/flynndownes', 'https://instagram.com/flynndownes', 'CLB000019', 1),
-- Cầu thủ 19
('Nikola', 'Vlasic', '1997-11-04', 181, 'Croatia', 0, 'Midfielder', 27, 'default.png', 'https://facebook.com/nikolavlasic', 'https://instagram.com/nikolavlasic', 'CLB000019', 1),
-- Cầu thủ 20
('David', 'Moyes', '1993-04-25', 176, 'Scotland', 0, 'Midfielder', 1, 'default.png', 'https://facebook.com/davidmoyes', 'https://instagram.com/davidmoyes', 'CLB000019', 1),

--Wolverhampton Wanderers
-- Cầu thủ 1
('José', 'Sa', '1993-01-17', 190, 'Portugal', 0, 'Goalkeeper', 1, 'default.png', 'https://facebook.com/josesagk', 'https://instagram.com/josesagk', 'CLB000020', 1),
-- Cầu thủ 2
('Rúben', 'Neves', '1997-03-13', 177, 'Portugal', 0, 'Midfielder', 8, 'default.png', 'https://facebook.com/rubenneves', 'https://instagram.com/rubenneves', 'CLB000020', 1),
-- Cầu thủ 3
('Raúl', 'Jiménez', '1991-05-05', 189, 'Mexico', 0, 'Forward', 9, 'default.png', 'https://facebook.com/rauljimenez', 'https://instagram.com/rauljimenez', 'CLB000020', 1),
-- Cầu thủ 4
('Willy', 'Bolí', '1990-02-03', 190, 'Ivory Coast', 0, 'Defender', 15, 'default.png', 'https://facebook.com/willyboli', 'https://instagram.com/willyboli', 'CLB000020', 1),
-- Cầu thủ 5
('Adama', 'Traoré', '1996-01-25', 178, 'Spain', 0, 'Forward', 37, 'default.png', 'https://facebook.com/adamatraore', 'https://instagram.com/adamatraore', 'CLB000020', 1),
-- Cầu thủ 6
('Leander', 'Dendoncker', '1995-08-15', 188, 'Belgium', 0, 'Midfielder', 32, 'default.png', 'https://facebook.com/leanderdendoncker', 'https://instagram.com/leanderdendoncker', 'CLB000020', 1),
-- Cầu thủ 7
('Nelson', 'Semedo', '1993-11-16', 179, 'Portugal', 0, 'Defender', 22, 'default.png', 'https://facebook.com/nelsonsemedo', 'https://instagram.com/nelsonsemedo', 'CLB000020', 1),
-- Cầu thủ 8
('Jonny', 'Otto', '1994-03-05', 175, 'Spain', 0, 'Defender', 19, 'default.png', 'https://facebook.com/jonnyotto', 'https://instagram.com/jonnyotto', 'CLB000020', 1),
-- Cầu thủ 9
('Matthews', 'Coady', '1993-11-25', 184, 'England', 1, 'Defender', 16, 'default.png', 'https://facebook.com/matthewcoady', 'https://instagram.com/matthewcoady', 'CLB000020', 1),
-- Cầu thủ 10
('Marçal', 'de Souza', '1989-05-17', 176, 'Brazil', 0, 'Defender', 12, 'default.png', 'https://facebook.com/marcaldesouza', 'https://instagram.com/marcaldesouza', 'CLB000020', 1),
-- Cầu thủ 11
('Daniel', 'Podence', '1996-10-21', 162, 'Portugal', 0, 'Forward', 10, 'default.png', 'https://facebook.com/danielpodence', 'https://instagram.com/danielpodence', 'CLB000020', 1),
-- Cầu thủ 12
('Rayan', 'Aït-Nouri', '2001-06-06', 180, 'France', 0, 'Defender', 3, 'default.png', 'https://facebook.com/rayanaitnouri', 'https://instagram.com/rayanaitnouri', 'CLB000020', 1),
-- Cầu thủ 13
('José', 'Moutinho', '1986-09-08', 174, 'Portugal', 0, 'Midfielder', 28, 'default.png', 'https://facebook.com/josemoutinho', 'https://instagram.com/josemoutinho', 'CLB000020', 1),
-- Cầu thủ 14
('Raul', 'Moreno', '1990-06-29', 186, 'Colombia', 0, 'Forward', 11, 'default.png', 'https://facebook.com/raulmoreno', 'https://instagram.com/raulmoreno', 'CLB000020', 1),
-- Cầu thủ 15
('Fabio', 'Silva', '2002-07-19', 185, 'Portugal', 0, 'Forward', 17, 'default.png', 'https://facebook.com/fabiosilva', 'https://instagram.com/fabiosilva', 'CLB000020', 1),
-- Cầu thủ 16
('John', 'Ruddy', '1986-10-24', 188, 'England', 1, 'Goalkeeper', 21, 'default.png', 'https://facebook.com/johnruddy', 'https://instagram.com/johnruddy', 'CLB000020', 1),
-- Cầu thủ 17
('Hwang', 'Hee-chan', '1996-01-26', 179, 'South Korea', 0, 'Forward', 26, 'default.png', 'https://facebook.com/hwangheechan', 'https://instagram.com/hwangheechan', 'CLB000020', 1),
-- Cầu thủ 18
('Boubacar', 'Traoré', '1996-09-23', 183, 'Mali', 0, 'Midfielder', 8, 'default.png', 'https://facebook.com/boubacartraore', 'https://instagram.com/boubacartraore', 'CLB000020', 1),
-- Cầu thủ 19
('Bruno', 'Júnior', '1995-04-18', 177, 'Brazil', 0, 'Midfielder', 5, 'default.png', 'https://facebook.com/brunojunior', 'https://instagram.com/brunojunior', 'CLB000020', 1),
-- Cầu thủ 20
('Liam', 'Lynch', '1997-05-13', 183, 'England', 1, 'Defender', 4, 'default.png', 'https://facebook.com/liamlynch', 'https://instagram.com/liamlynch', 'CLB000020', 1);

--ROLES
INSERT INTO [dbo].[Role] (
    [roleName], 
    [roleDes]
)
VALUES
('Admin', 'Management system'),
('Member', 'Member of the tournament'),
('Viewer', 'Viewer of the tournament')

--TypeNews
INSERT INTO [dbo].[TypeNews] (
    [typeNewsName], 
    [typeNewsDes]
)
VALUES
('Normal', 'Normal news'),
('Transfer', 'Transfer news'),
('Injury', 'Injury news')

--Account