# Hackathon Game: Shape Matching for Autistic Children

A game built at a hackathon for autistic children. The player drags shapes onto matching target spots, and the game checks the placement, gives sound feedback and shows a win screen when the level is complete. It uses both 2D and 3D objects.

<!-- TODO: add the hackathon name and date, e.g. "Built at <hackathon name>, <month year>." -->
<!-- TODO: add one or two screenshots in a /docs folder, then: ![Gameplay](docs/screenshot.png) -->

## How it works

- **Drag and snap.** Shapes are dragged with the mouse. On release, a shape snaps to the nearest target within a set distance. The first target counts as the correct one.
- **Flat and 3D shapes.** Cubes can show a "fake 3D" child object that switches off when the cube lands on the flat target.
- **Procedural 3D meshes.** The pyramid and triangular prism are generated in code from vertices and triangles, not imported as models.
- **Level manager.** A singleton counts the correctly placed shapes and shows the win screen when all are correct.
- **Characters.** A 2D character patrols between two points with sprite flipping and walk animation. A 3D character plays an intro: it walks in, turns to face the player and waves.

## Scripts

| Script | Purpose |
|---|---|
| `CubeSnapper.cs` | Drag, snap and sound for cube pieces, with the flat/3D toggle |
| `spodimo.cs` | Same for pyramid pieces, which are generated as a mesh |
| `ummmm.cs` | Level manager and win condition |
| `ozot.cs`, `prizma.cs` | Procedural pyramid and prism meshes |
| `ecja.cs` | 2D character patrol |
| `walk.cs` | 3D character intro sequence |

## Tech

- Unity 6 (6000.4.1f1), 2D URP template, C#
- Unity Input System
- A phone version of the game was also built in Python.

## Run it

1. Install Unity Hub and Unity 6 (6000.4.1f1).
2. Clone this repo and add the folder in Unity Hub.
3. Open the `lvl1` scene in `Assets/Scenes` and press Play.

<!-- TODO: add the team and your own role, e.g. "Team: ...". Only list what you built. -->
