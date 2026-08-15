# Changelog

## 1.0.0

First release.

- `/sw mark` twice to bracket a structure. Two marks at the same height read as
  a footprint and the top is taken from the rain map, so a building is covered
  without flying to its roof.
- Material dialog (Ctrl+Shift+M): the materials found in the area on the left,
  what each can become on the right, with the number of blocks each swap would
  touch. Materials are listed above kinds of block, since restyling a build
  usually means the material.
- Swaps are found by reading block codes as hyphen-separated tokens and asking
  the block registry which single-token changes exist, so every finish of a
  material converts in one action, stair facings survive, and modded blocks are
  picked up without a list to maintain.
- Only blocks somebody placed are listed or swapped. Grass, soil, gravel, sand,
  ore, raw stone, water and plant life stay out of it, so the material list
  holds the build rather than the ground under it, and a wall can be restyled
  without repainting the hillside behind it.
- Chiselled blocks are re-materialed in place, keeping their carving — and are
  the one place raw stone still counts, since somebody carved it.
- Blocks holding contents or state (chests, doors) are skipped and reported
  rather than swapped and emptied.
- Undo of the last 25 swaps per player, from the dialog or `/sw undo`.
- Chat equivalents: `/sw list`, `/sw options [material]`, `/sw swap <from> <to>`,
  `/sw unmark`.
