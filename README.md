<a id="deutsch"></a>

# Full of Bugs

🇩🇪 Deutsch (diese Version) | 🇬🇧 [English version further down](#english)

Ein gemütlicher, leicht unheimlicher 2D-Top-Down RPG/Exploration-Titel im Pixel-Art-Stil  

Entwickelt im Rahmen einer Praktikumsarbeit an der **SRH Fachschule für Informatik und Medien Heidelberg**  

04.05.2026 – 17.07.2026

---

## Kurzbeschreibung

Vor langer Zeit verschwand die Magie aus der Welt. Um sie zu bewahren, sammelte der Erzmagier Ganar sie über Jahre in Amuletten – den sogenannten **Glimmern**. Doch die Glimmer korrumpierten: Magie und Schatten vermischten sich, und aus den überfüllten Amuletten flohen dunkle Schattenwesen ins Verborgene.

Ein solches korrumpiertes Glimmer befindet sich nun in einem Maulwurfsbau. Ausgerechnet dort, wo sich der Einsiedlerkrebs **Hermbert** vor einem Gewitter versteckt und sich dabei hoffnungslos verirrt hat.

Gespielt wird **Milbert**, eine kleine, neugierige Mistkäferin, die Hermbert im Maulwurfsbau findet und ihn zurück an den heimischen Strand bringen will. Der Bau ist nicht nur dunkel, sondern voller Schattenwesen, die ahnungslosen Käfern auflauern. Nur das Licht, das Hermbert bei sich trägt, bietet ein wenig Schutz.

*Full of Bugs* verbindet Exploration mit einem Farming- und Craftingsystem, rundenbasierten Kämpfen sowie spannenden Quests. Spielgefühl: **cozy, aber leicht unheimlich.**

- **Genre:** RPG | Exploration | relaxing | Story | Pixel
- **Perspektive:** Top-down
- **Zielgruppe:** FSK 12, Fans von Exploration-Games
- **Spieldauer:** Demo ca. 15 Min. | Vollversion ca. 3 Std.
- **Singleplayer**: Ja
- **Multiplayer:** Nein

---

## Steuerung

| Taste | Aktion |
|---|---|
| `W` `A` `S` `D` | Bewegung Spielfigur, Skilltree hin- und herbewegen |
| `E` | Interagieren |
| `I` | Inventar öffnen |
| `T` | Skilltree aufrufen |
| `P` | Pausenmenü öffnen |
| `Leertaste` | Bestätigen (z. B. Dialoge weiterklicken), Skilltree zurücksetzen (nach Zoom/Verschieben) |
| `ESC` | Alle Menüs schließen / Pausenmenü öffnen & schließen, wenn kein anderes Menü offen ist |
| Maustaste links | Im Menü/Inventar Dinge auswählen bzw. anwählen, Drag & Drop, Skill im Skilltree erwerben |
| Maustaste rechts | Auswahl abbrechen (z. B. Samen im Inventar) |
| Mausrad | Zoom im Skilltree, Skilltree hin- und herbewegen |

---

## Objectives (Spielziele)

**Hauptziel:** Den Strand erreichen und Hermbert in Sicherheit bringen.

**Optionale Nebenziele:**
- Alle Quests abschließen
- Alle Crafting-Rezepte finden
- Alle Skills im Skilltree freischalten

**Kern-Gameplay-Loops:**
- **Exploration:** Die Map erkunden, verschlossene Tore finden und die passenden Schlüssel im „dunklen" Teil des Levels aufspüren
- **Kampf:** Außerhalb von Hermberts schützendem Lichtschein füllt sich das Schatten-Barometer – je länger man sich im Dunkeln aufhält, desto wahrscheinlicher ein Kampf gegen Schattenwesen

---

## Charaktere

| | |
|---|---|
| **Milbert / Millie** *(Geotrupes egeriei)* | Hauptfigur, junge Mistkäferin. Liebt Erdbeeren & Honigmelone, fürchtet sich vor der Dunkelheit. Bewaffnet mit der „sagenumwobenen Käsegabel" ihrer Urgroßmutter Dungberta sowie Mistbällchen, ist sie nahezu unaufhaltsam. |
| **Hermbert / Hermie** *(Pagurus bernhardus)* | Einsiedlerkrebs, „mobile Base" mit Gewächshaus und Laterne. Wollte sich vor einem gewitter retten, hat sich nun leider im Maulwurfsbau verirrt. |
| **Friedrich** *(Talpa europaea)* | Der Maulwurf und Endgegner. Alt, verbittert und Träger eines korrumpierten Glimmers. |
| **Schatten** *(Umbra dubia)* | Entflohene, korrumpierte Schattenwesen in drei Gegnertypen: Tank, Allrounder, Glasscannon. |

---

## Systemanforderungen

| | Spezifikation |
|---|---|
| **Gerät** | Dell Laptop (NVMe PC SN810 NVMe WDC, 1024 GB) |
| **CPU** | Intel® Core™ Ultra 7 155H (24 MB Cache, 16 Cores) |
| **RAM** | 16 GB DDR5, 4.800 MHz |
| **GPU** | NVIDIA RTX 1000 Ada Generation Laptop GPU, 6 GB |
| **Engine** | Unity 6000.3.14f1 |

---

## Spiel in Unity öffnen & starten

Wer nicht den fertigen Build nutzt, sondern das Projekt direkt in Unity öffnet:

1. Projekt in Unity (Version 6000.3.14f1) öffnen
2. Im `Project`-Fenster zur Scene **`WelcomeScreen`** navigieren
3. Die **`WelcomeScreen`**-Scene öffnen (Doppelklick)
4. Erst von dieser Scene aus auf Play drücken

> **Wichtig:** Das Spiel muss immer über die `WelcomeScreen`-Scene gestartet werden, nicht über eine andere Scene (z. B. die StartScreen- oder Main-Scene direkt). Grund: Die Manager-Singletons (u. a. `SceneLoadingManager`) werden erst beim Durchlaufen der `WelcomeScreen`-Scene korrekt initialisiert und mit `DontDestroyOnLoad` persistent gemacht. Startet man direkt in einer späteren Scene, fehlen diese Referenzen und es kommt zu Fehlern (z. B. `SceneLoadingManager.Instance` ist `null`).

---

## Tech Stack

- **Engine:** Unity, Version: 6000.3.14f1 (2D)
- **Sprache:** Unity-C#
- **Art-Tools:** Aseprite, Pixquare, Adobe Illustrator
- **Versionierung:** GitHub (Branch-per-Feature-Workflow)

---

## Team

**ART**:

- Vera Schöniger (Art-Lead)
- Vanessa Sadlowski
- Lucas Pietruschka
- Naomi Zellhofer

**DEV**:

- Philon Hauk (DEV-Lead)
- Naomi Zellhofer (Group-Lead)
- Joyce Goodwin
- Lucas Pietruschka

---
---

<a id="english"></a>

# Full of Bugs (English)

🇬🇧 English (this version) | 🇩🇪 [Deutsche Version weiter oben](#deutsch)

A cozy, slightly uneasy 2D top-down RPG/exploration game in pixel art style

Developed as part of an internship at **SRH Fachschule für Informatik und Medien Heidelberg**

05/04/2026 – 07/17/2026

---

## Short Description

Long ago, magic vanished from the world. To preserve it, the archmage Ganar spent years collecting it in amulets known as **Glimmers**. But the Glimmers became corrupted: magic and shadow intertwined, and dark shadow creatures fled the overflowing amulets into hiding.

One such corrupted Glimmer now resides in a mole burrow. Of all places, right where the hermit crab **Hermbert** took shelter from a thunderstorm and hopelessly lost his way.

You play as **Milbert**, a small, curious dung beetle who finds Hermbert in the mole burrow and wants to bring him back home to the beach. The burrow isn't just dark — it's full of shadow creatures lying in wait for unsuspecting beetles. Only the light Hermbert carries with him offers a little protection.

*Full of Bugs* combines exploration with a farming and crafting system, turn-based combat, and exciting quests.  

Mood: **cozy, but slightly uneasy.**

- **Genre:** RPG | Exploration | Relaxing | Story | Pixel
- **Perspective:** Top-down
- **Target audience:** Age rating 12+, fans of exploration games
- **Playtime:** Demo approx. 15 min | Full version approx. 3 hrs
- **Singleplayer:** Yes
- **Multiplayer:** No

---

## Controls

| Key | Action |
|---|---|
| `W` `A` `S` `D` | Move character, pan skill tree |
| `E` | Interact |
| `I` | Open inventory |
| `T` | Open skill tree |
| `P` | Open pause menu |
| `Spacebar` | Confirm (e.g. advance dialogue), reset skill tree (after zoom/pan) |
| `ESC` | Close all menus / open & close pause menu when no other menu is open |
| Left mouse button | Select/pick items in menu or inventory, drag & drop, acquire skill in skill tree |
| Right mouse button | Cancel selection (e.g. when selecting seeds in inventory) |
| Mouse wheel | Zoom in skill tree, pan skill tree |

---

## Objectives

**Main goal:** Reach the beach and bring Hermbert to safety.

**Optional side goals:**
- Complete all quests
- Find all crafting recipes
- Unlock all skills in the skill tree

**Core gameplay loops:**
- **Exploration:** Explore the map, find locked gates, and track down the matching keys in the "dark" part of the level
- **Combat:** Outside of Hermbert's protective light, the shadow meter fills up — the longer you stay in the dark, the higher the chance of a fight against shadow creatures

---

## Characters

| | |
|---|---|
| **Milbert / Millie** *(Geotrupes egeriei)* | Main character, a young dung beetle. Loves strawberries & honeydew melon, afraid of the dark. Armed with the "legendary cheese fork" of her great-grandmother Dungberta as well as dung balls, she is nearly unstoppable. |
| **Hermbert / Hermie** *(Pagurus bernhardus)* | Hermit crab, the "mobile base" complete with a greenhouse and lantern. Tried to escape a thunderstorm and unfortunately got lost in the mole burrow. |
| **Friedrich** *(Talpa europaea)* | The mole and final boss. Old, embittered, and carrier of a corrupted Glimmer. |
| **Shadows** *(Umbra dubia)* | Escaped, corrupted shadow creatures in three enemy types: Tank, Allrounder, Glass Cannon. |

---

## System Requirements

| | Specification |
|---|---|
| **Device** | Dell Laptop (NVMe PC SN810 NVMe WDC, 1024 GB) |
| **CPU** | Intel® Core™ Ultra 7 155H (24 MB Cache, 16 Cores) |
| **RAM** | 16 GB DDR5, 4,800 MHz |
| **GPU** | NVIDIA RTX 1000 Ada Generation Laptop GPU, 6 GB |
| **Engine** | Unity 6000.3.14f1 |

---

## Opening & Starting the Game in Unity

For anyone not using the finished build but opening the project directly in Unity:

1. Open the project in Unity (version 6000.3.14f1)
2. In the `Project` window, navigate to the **`WelcomeScreen`** scene
3. Open the **`WelcomeScreen`** scene (double-click)
4. Only press Play from this scene

> **Important:** The game must always be started from the `WelcomeScreen` scene, not from any other scene (e.g. the StartScreen or Main scene directly). Reason: the manager singletons (including `SceneLoadingManager`) are only correctly initialized and made persistent via `DontDestroyOnLoad` while passing through the `WelcomeScreen` scene. Starting directly in a later scene means these references are missing, causing errors (e.g. `SceneLoadingManager.Instance` being `null`).

---

## Tech Stack

- **Engine:** Unity, version: 6000.3.14f1 (2D)
- **Language:** Unity C#
- **Art tools:** Aseprite, Pixquare, Adobe Illustrator
- **Version control:** GitHub (branch-per-feature workflow)

---

## Team

**ART:**

- Vera Schöniger (Art Lead)
- Vanessa Sadlowski
- Lucas Pietruschka
- Naomi Zellhofer

**DEV:**

- Philon Hauk (Dev Lead)
- Naomi Zellhofer (Group Lead)
- Joyce Goodwin
- Lucas Pietruschka

---
