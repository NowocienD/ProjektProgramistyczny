# Założenia
 - nie wrzucamy na gita kodu który się nie kompiluje. Puknt dotyczy również poszczególnych commitów
 - dbamy o styleCop i jego warrningi
 - pełna zgodność z REST API
 - używamy tylko camelCase lub PascalCase 
 - robimy API wiec każdy kontroler ma ustowiony route na api/*. 
 - baza danych SQL Server z Entity Framework Core
 - nowe zadanie = nowy branch  
 - klasy z modelem zawierają w nazwie słowo DAO np. AttendaceDAO,
 - klasy kontrolerów zawierają w nazwie słowo Controller np. AccountController,
 - interfejsy zacznają nazwę od I
 - wszystkie potrzebne nugety powinny juz być w projekcie
 - zachowujcie strukturę folderów i nie mieszajcie ich funckvji (każdy folder to też inny namespace):
 

# Branche i zadania

 - Zadania są widoczne jako issues, każde zadanie ma swój numer
 - Branche tworzymy z *developa* i merge requesty zgłaszamy do *developa*
 - Na *mastera* wgrywana jest co jakiś czas nowa wersja
 - Nazwy branchy:

> feature-nr_zadania 

lub 

>  bugfix-nr_zadania

# Konflikty

Jak naprawić konflikty pojawiające sie przy merge requestach?
1. Przełączyć się na Developa

    `$ git checkout develop`

2. Dociągnąć zmiany

    `$ git pull`

3. Wrócić na naszą  gałąź

    `$ git checkout feature-X`

4. Scalić gałęzie lokalnie

    `$ git merge develop`

5. W plikach pojawią się błędy (konflikty), nalezy je naprawić, zacommitować i pushnąć zmiany


# Skrypty
 - prawy przycisk myszy na skrypcie -> run with PowerShell
 - ewentualnie w PowerShellu, bashu lub innym cmd wywolanie tej komendy (musicie być w odpowiednim folderze)

skrypt _BuildAndRun.ps1 buduje i uruchamia serwer i nasłuchuje na porcie 8080

##### Nie działa?
PowerShell run as Administrator
```sh
$ Set-ExecutionPolicy unrestricted
$ Y
```
# Uruchamianie rozwiazania 
zawsze kolejnosc: przywracanie -> budowanie -> uruchamianie
przywracanie jest konieczne tylko po dodaniu jakiejś NuGetowej paczki co nie bedzie bardzo czeste ale moze się zdarzać lub referencji na projekt.

Jeśli coś na tym etapie nie działa to prawdopodonie dlatego ze nie jesteś w folderze API.

# Przywracanie
##### Sposób 1:
skrypt _restore.ps1 

##### Sposób 2:
PowerShell  
```sh
$ dotnet restore
```
# Kompilowanie 

##### Sposób 1:
skrypt _build.ps1 

##### Sposób 2:
PowerShell  
```sh
$ dotnet build --no-restore
```
lub jeżeli nie robiłeś wcześniej restore
```sh
$ dotnet build
```
##### Sposób 3:
F6 w VS2019 jak nie działa to wiecej info w uwagach.

# Uruchamianie

##### Sposób 1:
skrypt _run.ps1

##### Sposób 2:
PowerShell
```sh
$ dotnet run
```
##### Sposób 3:
zielona strzałka w VS ale jak nie działa to wiecej info w uwagach.


# Konfiguracja 
wg uznania jednak na poczatek proponuje tak
#### appsettings.json
```
{
  "CorsSettings": {
    "Origins": [ "http://localhost:3000" ]
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "DevelopmentSettings": {
    "IsDevelopment": false
  },
  "TokenSettings": {
    "SecretKey": "toJestKluczWgKtoregoDokonujeSieSzyfrowanie.LubieMalePenisyIDoktoraPaducha"
  }
}

```

#### styleCop.ruleset
minimalna konfiguracja!
```
<?xml version="1.0" encoding="utf-8"?>
<RuleSet Name="New Rule Set" Description=" " ToolsVersion="16.0">
  <Rules AnalyzerId="StyleCop.Analyzers" RuleNamespace="StyleCop.Analyzers">
    <Rule Id="SA1302" Action="Warning" />
    <Rule Id="CS0105" Action="Warning" />
    <Rule Id="SA1210" Action="Warning" />
    <Rule Id="IDE0005" Action="Warning" />
    <Rule Id="SA1600" Action="None" />
    <Rule Id="SA1200" Action="None" />
    <Rule Id="SA1633" Action="None" />
    <Rule Id="SA1101" Action="None" />
    <Rule Id="SA0001" Action="None" />
    <Rule Id="SA1028" Action="None" />
  </Rules>
</RuleSet>
```
# #Frontend

# Oprogramowanie konieczne do uruchomienia
 - [YARN](https://yarnpkg.com/lang/en/)
 - [Node.js](https://nodejs.org/en/)

# Uruchamianie
Aby uruchomić frontend należy:
(bash otwarty w folderze frontend)
1.  zainstalować zależności
    ```sh
    $ yarn install
    ```
2. Od tej pory można odpalać front poleceniem

    ```sh
    $ yarn start
    ```
3. `CTRL + C` zabija proces i wyłącza serwer

# Uwagi
- Front powinien sam odpalić przeglądarke z odpowiednim adresem po wpisaniu polecenia `yarn start`.
Jeśli się to jednak nie stanie, strona powinna być widoczna na (http://localhost:3000).
- Frontend przeładowuje się sam i nie ma potrzeby przekompilowania go.
Po dokonaniu jakichkolwiek zmian i zapisaniu ich, strona się odświeży.
