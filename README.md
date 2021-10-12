# S2-CMK
##  Oplevering 1
In deze eerste sprint heb ik de analyse documentatie gemaakt voor mijn persoonlijk project. De onderdelen voor de analyse die in het analyse document zijn te vinden zijn:

- Projectbeschrijving
- Functionele requirements met berperkingen en kwaliteitseisen
- Technische requirements
- Use-case diagram
- Use-case omschrijvingen
- Schermontwerpen
- Conceptueel model

Verder heb ik ook nog een planning gemaakt voor de eerste sprint en deze in een apart document gezet. Ik heb al een begin gemaakt aan het project in ASP.NET Core en wil ook gebruik gaan maken van het aangeraden Blazor framework. De reden dat ik voor Blazor heb gekozen is dat je je alleen bezig hoeft te houden met 1 programmeertaal, aangezien Blazor alle functionaliteit, die je normaalgesproken met Javascript zou moeten schrijven, in c# te schrijven is.

##  Oplevering 2

In de tweede sprint heb ik voornamelijk gewerkt aan de ontwerp fase van mijn project en al een stukje algoritmiek. De onderdelen die ik belangrijk vind om beoordeelt te hebben zijn:

- Nieuw conceptueel model in analyse document
- Klassendiagram
- Database model
- Architectuur met persistentie en logica lagen
- Algoritmiek opdracht: Circustrein
- Geupdate, maar nog altijd leeg, class libraries in de applicatie

Ik heb verder deze sprint nog onderzoek gedaan naar het blazor framework buiten het maken van de documentatie om zodat ik de volgende sprint echt kan beginnen met het programmeerwerk. Zowel mijn analyse als ontwerp document heb ik in een pdf in de hoofdfolder gezet van mijn documenten folder.

##  Oplevering 3

In de derde sprint heb ik gewerkt aan het verbeteren van mijn documentatie en heb ik een sterk begin gemaakt met het programmeren van mijn verschillende klassen. De onderdelen waar ik aan heb gewerkt zijn:

- Nieuw testplan in analyse documentatie
- Geupdate Architectuur plaatje
- Geupdate v2 Klassendiagram
- Geupdate v6 Klassendiagram
- Schrijven van code voor alle entiteiten die in persistence en logic zitten incl. communicatie tussen de lagen

Ik heb deze sprint flink wat code al geschreven en ga in de volgende sprint verder met het maken van de nog ontbrekende functies en het implementeren van de communicatie naar de UI. Ik wil circustrein ook nog gaan uitbreiden met een database zodat ik aan kan tonen dat ik basis CRUD ook onder de knie heb.

##  Oplevering 4

In de vierde sprint heb ik gewerkt aan het aantonen van mijn beheersing van CRUD en een begin gelegd met het maken van pagina's voor de front-end. Om nog wat meer in detail er op in te gaan heb ik hieronder omschreven waaraan ik heb gewerkt:

- Verbeterd testplan in analyse documentatie
- Architectuur wederom geupdate zodat ik juist kan weergeven wat ik anders doe met mijn front-end
- Klassendiagram v6 geupdate voor de hierboven genoemde reden
- Nieuwe folder met de naam "JS CRUD" waarin alle files staan om CRUD aan te tonen
- Aantal nieuwe pagina's gemaakt voor de front-end

Ik ben op dit moment nog aan het wachten op een workshop over hoe we de front-end op een snelle en correcte manier met de back-end kunnen laten communiceren.

##  Oplevering 5

In de vijfde sprint ben ik voornamelijk bezig geweest met een basis versie van error afhandeling en het implementeren van mijn UI. Ik heb een groot aantal dingen in mijn logica omgegooid en nog een aantal ander dingen verandert en deze staan hieronder beschreven:

- Alle interfaces weggehaald die zorgden voor communicatie tussen de logica en de UI
- Gebruik van singleton patterns met padlocks voor alle collecties (padlocks zorgen voor afhandelen van functies in juiste volgorde)
- Klassendiagram v6 aangepast om aan te tonen dat de interfaces tussen de hierboven genoemde lagen weg zijn

De volgende sprint ga ik aan de volgende onderdelen werken om mijn semester succesvol af te ronden:

- Binnen circustrein unittesten maken die volgens een subset die alles juist afdekken te maken.
- Voor acceptatietesten/testplan verscherpen door middel van voorgaande cases bijvoorbeeld gebruiken.
- Exceptions van systeemfouten verder afhandelen.
- Voor uitvoer van testplan een testrapport maken en binnen het rapport ervoor zorgen dat je 2 kolommen hebt waarvan je in de een je resultaat kan noteren en de ander je opmerkingen bij de test kan noteren.