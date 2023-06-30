# Games Engine Projekt

- [Games Engine Projekt](#games-engine-projekt)
  * [Projektname](#projektname)
  * [Authoren](#authoren)
  * [Besonderheiten](#besonderheiten)
  * [Steuerung](#steuerung)
  * [Besondere Leistungen](#besondere-leistungen)
  * [Herausforderungen](#herausforderungen)
  * [Gesammelte Erfahrungen](#gesammelte-erfahrungen)
  * [Zeitintensive Aufgaben](#zeitintensive-aufgaben)
  * [Demo Video](#demo-video)
  * [Detailiertes Preview](#detailiertes-preview)
    + [Tilemaps](#tilemaps)
      - [Tilemaps Anlegen](#tilemaps-anlegen)
      - [Tilemapping](#tilemapping)
      - [Karten](#karten)
    + [Partikeleffekte](#partikeleffekte)
      - [Eigenschaften](#eigenschaften)
      - [Regen](#regen)
      - [Glühwürmchen](#gl-hw-rmchen)
    + [Tag-Nacht-Zyklus](#tag-nacht-zyklus)
      - [Verlauf](#verlauf)
      - [Umsetzung](#umsetzung)
    + [Szenenwechsel](#szenenwechsel)
      - [Prinzip](#prinzip)
      - [Optik](#optik)
      - [Funktionsweise](#funktionsweise)
    + [Dialog Box](#dialog-box)
      - [Demo](#demo)
      - [Objektaufbau](#objektaufbau)
    + [Behavior Tree](#behavior-tree)
      - [Demo](#demo-1)
      - [BT Aufbau normale Gegner](#bt-aufbau-normale-gegner)
      - [BT Aufbau Boss](#bt-aufbau-boss)
    + [Pathing](#pathing)
      - [Statisches Pathing](#statisches-pathing)
        * [Definition der Wegpunkte](#definition-der-wegpunkte)
      - [BFS Pfadsuche](#bfs-pfadsuche)
        * [Funktionsweise](#funktionsweise-1)
        * [Belaufbare Tiles finden](#belaufbare-tiles-finden)
    + [Kampfeffekte](#kampfeffekte)
      - [Blitzeffekt](#blitzeffekt)
        * [Demo:](#demo-)
        * [Funktionsweise](#funktionsweise-2)
      - [Projektileffekt](#projektileffekt)
        * [Demo](#demo-2)
        * [Funktionsweise](#funktionsweise-3)
      - [Griffeffekt](#griffeffekt)
        * [Demo](#demo-3)
        * [Funktionsweise](#funktionsweise-4)
    + [Animator](#animator)
      - [BlendTree Struktur](#blendtree-struktur)
      - [BlendTree Aufbau](#blendtree-aufbau)
    + [Entity Dependency Vererbung](#entity-dependency-vererbung)
    + [Movements](#movements)
      - [Bewegung und Kollision](#bewegung-und-kollision)
      - [Schaden und Rückstoß](#schaden-und-r-cksto-)
  * [Verwendete Assets](#verwendete-assets)
  * [Inspiration](#inspiration)

## Projektname
- Spielname: "Luminoth’s Legacy"
- Ilias Name: "Boss-Kampf Entwicklung oder Implementierung verschiedener Behavior Trees in 2D"

## Authoren
- Josefine Ebel
- Steven Kovacs
- Luisa Springer

## Besonderheiten
- Nachdem der Spieler stirbt, muss das Spiel neu gestartet werden
- Beim Start des Spieles gibt es vorher kein Menü, sondern landet gleich im Spiel

## Steuerung
- Bewegung nach links: Pfeil-Links
- Bewegung nach rechts: Pfeil-Rechts
- Bewegung nach oben: Pfeil-Hoch
- Bewegung nach unten: Pfeil-Runter
- Interaktion mit NPC: "D"
- Angriff: "Z"

## Besondere Leistungen
![BearbeiteteThemen](https://github.com/Meloweh/GE1/assets/49780209/3bfeeb27-e83b-4c00-90fb-f40f6595da45)
- Dialogsystem
- Pathing
- Pfadsuche
- Fähigkeiten des Lich's (Blitzeffekt, Energieballeffekt, Greifarm)
- Partikelsysteme (Glühwürmchen, Regen, Fledermaus Spawn)
- Tilemaps & Collidermapping
- Kampfsystem
- Tag-Nacht Zyklus
- Szenenwechsel

## Herausforderungen
- Mergekonflikte mit Git
- Kollisionsermittlung in der Pfadsuche
- Glitch in der Angriffsanimation im BlendTree

## Gesammelte Erfahrungen
- Umgang mit Behavior Trees
- Umgang mit Unity's Sprite Editor und 2D Animationen
- Vertiefung in 2D Animationen und Blendtrees
- Umgang mit Unity's Tilemapsystem
- Umgang mit Unity's Partikelsystemen
- Erstellung von Spezialeffekten
- Implementierung einer Pfadsuche
- Implementierung eines Pathingsystems
- Implementierung eines Dialogsystems
- Implementierung eines Kampfsystems
- Implementierung eines Tag-Nacht-Zyklus

## Zeitintensive Aufgaben
- Erstellung der Umgebung mit Tilemaps
- Sprite Sheets zuschneiden
- Animation Clips erstellen

## Demo Video
[![IMAGE ALT TEXT](http://img.youtube.com/vi/klmC0mQ41Go/0.jpg)](http://www.youtube.com/watch?v=klmC0mQ41Go "Demo")

## Detailiertes Preview
### Tilemaps
#### Tilemaps Anlegen
![TilemapsAnlegen](https://github.com/Meloweh/GE1/assets/49780209/516e31d0-43b9-4275-acbb-f30a42eec0c8)
#### Tilemapping
![Tilemap](https://github.com/Meloweh/GE1/blob/mergeJune18/gifs/image18.gif)
#### Karten
![Karten](https://github.com/Meloweh/GE1/assets/49780209/c539f75a-7d95-4da5-aadb-6209e669a727)
### Partikeleffekte
#### Eigenschaften
![Partikelsysteme](https://github.com/Meloweh/GE1/assets/49780209/eed892d1-06f9-4027-94c2-5cbe61f25bfb)
#### Regen
![Rain](https://github.com/Meloweh/GE1/blob/mergeJune18/gifs/image26.gif)
#### Glühwürmchen
![Fireflies](https://github.com/Meloweh/GE1/blob/mergeJune18/gifs/image27.gif)
### Tag-Nacht-Zyklus
#### Verlauf
![Night Cycle](https://github.com/Meloweh/GE1/blob/mergeJune18/gifs/image31.gif)
#### Umsetzung
![TagNachtZyklus](https://github.com/Meloweh/GE1/assets/49780209/697f7c82-f0c2-47f9-af91-fdaafc7f2d13)
### Szenenwechsel
#### Prinzip
![Szenenwechsel](https://github.com/Meloweh/GE1/assets/49780209/50cbe931-aafa-46df-b4c4-40da43dfc83d)
#### Optik
![Transition](https://github.com/Meloweh/GE1/blob/mergeJune18/gifs/image37.gif)
#### Funktionsweise
![NeueSzeneLaden](https://github.com/Meloweh/GE1/assets/49780209/e0735ffe-11f4-4a3c-88c5-35d76c49fee1)
### Dialog Box
#### Demo
![Dialog Box](https://github.com/Meloweh/GE1/blob/mergeJune18/gifs/image46.gif)
#### Objektaufbau
![DialogSystem](https://github.com/Meloweh/GE1/assets/49780209/0699fb74-bf0f-4be7-a1db-e39dfd65f6a9)
### Behavior Tree
#### Demo
![Behavior Tree](https://github.com/Meloweh/GE1/blob/mergeJune18/gifs/image63.gif)
#### BT Aufbau normale Gegner
![EnemyBehaviorTree](https://github.com/Meloweh/GE1/assets/49780209/e72eba09-2547-49f7-9a3a-c294aeb6e1a6)
#### BT Aufbau Boss
![BossBehaviorTree](https://github.com/Meloweh/GE1/assets/49780209/727e4bf9-7869-4f6e-9fb4-fb4d3d8f9728)
### Pathing
#### Statisches Pathing
![StatischesPathing](https://github.com/Meloweh/GE1/assets/49780209/08f0c742-6619-4f96-a211-83910ef519c3)
##### Definition der Wegpunkte
![Waypoints](https://github.com/Meloweh/GE1/assets/49780209/ed1f0f66-a37f-44c0-a356-faca1b7b6604)
#### BFS Pfadsuche
##### Funktionsweise
![BFSBeispielPart1](https://github.com/Meloweh/GE1/assets/49780209/284b0efa-1075-4cb7-9e17-52b0c7d8e859)
![BFSBeispielPart2](https://github.com/Meloweh/GE1/assets/49780209/c12c77a8-cb08-4d1e-9c3d-29c7be1e6e97)
![BFSBeispielPart3](https://github.com/Meloweh/GE1/assets/49780209/742ebf95-5482-484d-a8ae-087c1fe15900)
##### Belaufbare Tiles finden
![BFSBelaufbareTilesErmitteln](https://github.com/Meloweh/GE1/assets/49780209/b312413e-2055-4b51-9457-842d3ec66fa8)
### Kampfeffekte
#### Blitzeffekt
##### Demo:
![Lightning Attack](https://github.com/Meloweh/GE1/blob/mergeJune18/gifs/image77.gif)
##### Funktionsweise
![BlitzEffekt](https://github.com/Meloweh/GE1/assets/49780209/3138b8c3-0331-4873-aa65-cff0211722ad)
#### Projektileffekt
##### Demo
![Ball Attack](https://github.com/Meloweh/GE1/blob/mergeJune18/gifs/image85.gif)
##### Funktionsweise
![ProjektilEffekt](https://github.com/Meloweh/GE1/assets/49780209/083d849e-7052-4c77-b5bb-15e61d7c0411)
#### Griffeffekt
##### Demo
![Hand Attack](https://github.com/Meloweh/GE1/blob/mergeJune18/gifs/image88.gif)
##### Funktionsweise
![GriffEffekt](https://github.com/Meloweh/GE1/assets/49780209/dbe91e17-7089-471c-b7c7-c025455bda29)
### Animator
#### BlendTree Struktur
![AnimationBlendtrees](https://github.com/Meloweh/GE1/assets/49780209/7d233596-e4fe-4a1f-89af-93c622886326)
#### BlendTree Aufbau
![BlendtreeAufbau](https://github.com/Meloweh/GE1/assets/49780209/2d3637e7-5c54-425d-80bc-ab7fe9c3e4f8)
### Entity Dependency Vererbung
![Vererbung](https://github.com/Meloweh/GE1/assets/49780209/af519d43-e36d-4086-826f-814cef6f3c18)
### Movements
#### Bewegung und Kollision
![MovementUndKollisionen](https://github.com/Meloweh/GE1/assets/49780209/6052fca7-09dd-483f-86c1-ae0a1bbc8cbb)
#### Schaden und Rückstoß
![SchadenUndKnockback](https://github.com/Meloweh/GE1/assets/49780209/c984f1ef-3bc4-4b03-aff1-27ce78458cbf)
![KnockbackBerechnung](https://github.com/Meloweh/GE1/assets/49780209/b29c69c2-c24c-4ec0-a207-a7c3f54dede8)
![MovementUndKollisionen](https://github.com/Meloweh/GE1/assets/49780209/3534f69a-c7ef-4053-9a0d-8fcad7c750de)
## Verwendete Assets
- [Behavior Tree For Everyone](https://assetstore.unity.com/packages/tools/visual-scripting/behavior-designer-behavior-trees-for-everyone-15277)
- Input System v1.5.1 (über Package Manager)
- [Energyball Sprite Sheet](https://www.spriters-resource.com/custom_edited/thelegendofzeldacustoms/sheet/17519/)
- Hand Sprite Sheet
- [Fantasy RPG Heroes Pack](https://franuka.itch.io/fantasy-rpg-heroes-pack)
- [Fantasy RPG Monster Pack](https://franuka.itch.io/fantasy-rpg-monster-pack)
## Inspiration
- [Tilemaps](https://www.youtube.com/watch?v=DTp5zi8_u1U)
- [Character Move](https://www.youtube.com/watch?v=0cycus0Ojnc)
- [Sprite Animation](https://www.youtube.com/watch?v=0cycus0Ojnc)
