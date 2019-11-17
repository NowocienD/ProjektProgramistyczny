#Backend

# Uwagi
 - plik sln jest dla Api. Reszte otwieramy używajac opcji otworz folder. Macie ochote stworzcie sobie reszte slnów 
 - jak ktoś nie ma Windowsa 10 to niech wypierdala
 - jak ktoś nie ma x64 to niech wypierdala
 - jak komuś nie działa vs2019 to niech wypierdala
 - jak ktoś chce pisać w javie to niech wypierdala

# Założenia
 - nie wrzucamy na gita kodu który się nie kompiluje. Puknt dotyczy również poszczególnych commitów
 - dbamy o styleCop i jego warrningi.
 - pełna zgodność z REST API
 - używamy tylko camelCase lub PascalCase 
 - robimy API wiec każdy kontroler ma ustowiony route na api/*. 
 - baza danych SQL Express z nHibernate chyba ze macie lepszy pomysł albo chęci i umiejetności na zabawę z czymś innym
 - nowe zadanie = nowy branch  
 - klasy z modelem zawierają w nazwie słowo Models np. AccountModels,
 - klasy kontrolerów zawierają w nazwie słowo Controller np. AccountController,
 - zachowujcie strukturę folderów i nie mieszajcie ich funckji (każdy folder to też inny namespace)
 -- GradeBook.API tylko dla kontrolerów i bliskiej okolicy 
 -- Gradebook.Services logika
 -- Gradebook.Services/Helpers helpery
 -- Gradebook.Services.Core interfejsy
 -- Gradebook.Services.Core/DTO dto
 -- GgradeBook.Models powiazania z baza danych
 - jak chcecie pisać UnitTesty to tworzycie w folderze backend z identyczną nazwą zakonczoną *.Tests
 - dodajac paczki z NuGeta uważajcie co robicie bo potrafią zasyfiac pliki csproj. Polecam ręczne robienie albo przynajmniej zweryfikowanie zawartości!

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
przywracanie jest konieczne tylko po dodaniu jakiejś NuGetowej paczki co nie bedzie bardzo czeste ale moze się zdarzać

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
  "AllowedHosts": "*"
}
```

#### styleCop.ruleset
minimalna konfiguracja!
```
<?xml version="1.0" encoding="utf-8"?>
<RuleSet Name="New Rule Set" Description=" " ToolsVersion="16.0">
  <Rules AnalyzerId="StyleCop.Analyzers" RuleNamespace="StyleCop.Analyzers">
    <Rule Id="SA1302" Action="Error" />
    <Rule Id="CS0105" Action="Error" />
    <Rule Id="SA1210" Action="Error" />
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