# PLAYLISTE TIL EKSAMEN

1. Præsenter casen (fordi så viser man at man har forstået hvad man skal lave). Det vil sige at man kort beskriver systemets ønskede funktionalitet aka. systembeskrivelsen.
   - 

2. Præsentation af systemudviklingsværktøjer, herunder systemudviklingsmetoden og projektstyringen.
    - Systemudviklingsmetoden: Agil og iterativ, inception, elaboration, construction, testing. Hvad er formålet med hver af disse?
      - Agil: er en "tankegang" hvor man ikke låser sig fast
      - Iterativ: en del af den agile tankegang for man går "tilbage" til sidst skrid og ser om det man har tænkt var rightig eller om man er blevet kloger på noget?
      - Inception: Actors, requirment specification
      - Elaboration: OOA(Object oriented), Entities, ER Diagram, OOD, Attributes,Design Diagram 
      - Construction: DB, DataAccess, API, Clientside UI/UX
    - Inception: Hvordan har man lavet kravspec? Hvad er et godt krav?
      - check systembeskrivelse og find functionality?, ingen design choices, kort, precise,  
    - Elaboration: Hvordan kommer man fra Inception til Elaboration? Vi anvendte værktøjerne:
        - "Find navneord" => Entiteter
        - Lavede analyse diagrammer (ER), hvad er relationerne mellem entiteterne.
        - Bagefter lavede vi design diagrammer, ER og UML.
    - Projekstyring på GitHub: Vis github projektet og issues.

3. Begynd at vise hvordan UI virker ift. de funktionelle krav.

4. Construction: 
    * Hvad er et klient-server system?¨
      - website/frontend to API?
    * UI/UX kode, hvordan kommunikerer klienten med serveren? HTTP metoderne og JSON.
      - HTTP: er en protocol som includer post eller get
      - JSON: er det Dataformat jeg bruger når data skal sends. 
    * Arkitektur med Separation of Concerns: Modulær opbygning med klassebiblioteker og applikationer.
    * API: endpoint (IP + port) og IIS'ens rolle
      - IIS: den der står for at route til den rightige port?
      - IP + port: !kan ikke huske spør mads!
        * Controllere og deres action methods, herunder URI'en
            - vis koden
            - URI: is a part of a url, its the identifier also know as the part that comes after the "/"
    * Data Access: Repository design pattern.
      - we har en base klasse og nogle klasse som arver fra den.
    * Database: SQL + tabeller, kolonner, rækker, PK, FK.
      - SQL:  
      - tabeller:
      - Kolonner:
      - Rækker:
      - PK:
      - FK:
    * OOP:
        * Indkapsling: Hvad er formålet og hvordan anvender man det? Eks.: Entities.
        * Komposition/aggregation: Hvad er formålet og hvordan anvender man det? Eks.: Entities.
        * Arv: Hvad er formålet og hvordan anvender man det? Eks.: DataAccess.
    
-
# Order
