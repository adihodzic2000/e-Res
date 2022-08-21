# e-Res

UPUTE ZA POKRETANJE DESKTOP (WPF) I MOBILNE (FLUTTER) APLIKACIJE
1. Nakon preuzimanja projekta u cmd-u otići na putanju .../e-Res/e-Res (eres je mobilni dio, e-Res api i desktop aplikacija)
2. Ukucati komandu "docker-compose up --build"
3. Otvoriti novi cmd window sa istom putanjom (.../e-Res/e-Res), nakon čega treba izvršiti kopiranje baze u container komandom "docker cp ERes.bak e-res_eres-sql_1:/var/opt/mssql/data"
4. (Zbog veće količine podataka i međusobne zavisnosti između tabela potrebno je izvršiti restore baze na sljedeći način) Ukucati sljedeće dvije komande
 - docker exec -it e-res_eres-sql_1 /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "QWElkj132!" -Q "RESTORE FILELISTONLY FROM DISK = '/var/opt/mssql/data/ERes.bak'" 
 - docker exec -it e-res_eres-sql_1 /opt/mssql-tools/bin/sqlcmd   -S localhost -U SA -P "QWElkj132!"  -Q "RESTORE DATABASE ERes FROM DISK = '/var/opt/mssql/data/ERes.bak' WITH MOVE 'ERes' TO '/var/opt/mssql/data/ERes.mdf', MOVE 'ERes_Log' TO '/var/opt/mssql/data/ERes.ldf'"
5. UKOLIKO NIJE USPJEŠNO IZVRŠENO RESTORE BAZE, otvoriti SQL SERVER MANAGEMENT STUDIO sa imenom servera "localhost, 1401", i kredencijalima user=sa; Password=QWElkj132!
 5.1. na putanji .../e-Res/e-Res nalazi se file InsertData.sql kojeg treba otvoriti u SQL SERVER MANAGEMENT STUDIO, nakon čega ga treba pokrenuti 5-6 puta kako bi se uspješno izvršio restore baze
6. Nakon što je izvršen restore baze, pristupamo mobilnom i desktop aplikaciji, otvaramo lokaciju u cmdu .../e-Res/eres (mobilni dio) i pokrećemo komandu "flutter pub get",
zatim u Visual Studio otvaramo api i desktop dio, potrebno je u solutionu naglasit da će se samo koristiti WPF projekat, uraditi rebuild WPF dijela, nakon čega treba pritisnuti CTRL+F5
7. Kredencijali za prijavu na desktop aplikaciji su sljedeći:
 -za hotel (username: adi, password: adi123),
 -za apartman (username: adi_ap, password: adi123),
 -za mobilni dio (username: mobile, password: mobile)