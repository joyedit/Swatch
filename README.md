# Swatch

Try a different material on a build before you commit to it.

Mark two corners of a structure, pick a material out of it, and swap it for
another one across the whole area — andesite to chalk, oak to maple,
cobblestone to bricks. If you don't like it, undo.

## Using it

1. Aim at one corner of the building and run `/sw mark`.
2. Aim at the opposite corner and run `/sw mark` again.

   Both marks at the same height read as a *footprint*: the top of the area is
   taken from the rain map, so a whole building is covered without flying up to
   its roof. Marks at different heights are taken literally as a box.
3. Look over what the outline actually covers, then press **Ctrl+Shift+M**. The
   left list holds the materials found in the area, the right list what each can
   become, with how many blocks the swap would touch. **Apply**, and **Undo** if
   you don't like it.

The marked area is outlined while it lasts; each corner shows as a coloured
pillar.

### From chat, if you prefer

| Command | What it does |
| --- | --- |
| `/sw mark` | Mark a corner (aim at a block) |
| `/sw unmark` | Clear the marked area |
| `/sw list` | List the blocks inside the area |
| `/sw options` | Materials in the area that can be swapped |
| `/sw options andesite` | What `andesite` could become, and how many blocks would change |
| `/sw swap andesite chalk` | Do it |
| `/sw undo` | Step back through recent swaps |

Requires the `controlserver` privilege.

## How a material is recognised

Nothing is hardcoded to vanilla stone. A block code is read as a list of
hyphen-separated tokens, and Swatch asks the block registry which single-token
changes land on a block that actually exists:

```
cobblestone-andesite              ->  cobblestone-chalk
stonebricks-andesite              ->  stonebricks-chalk
polishedrock-andesite             ->  polishedrock-chalk
stonebrickstairs-andesite-north   ->  stonebrickstairs-chalk-north
```

So every shape of a stone moves together in one action — cobble, bricks,
polished, stairs and slabs — and stair facings survive the swap.
Because the choices come from the registry rather than a list, modded stone and
wood show up on their own, and swaps that don't exist are never offered.

The same reading works sideways: `cobblestone` → `stonebricks` keeps the rock
type and changes the finish.

The first token of a code names the *kind* of block and the ones after it name
what it is made of, so the material list puts materials (`chalk`, `claystone`,
`oak`) above kinds of block (`cobblestone`, `slantedroofing`) — restyling a
build usually means the former. Both are offered; each group is ordered by how
much of the selection it covers.

Tokens that describe orientation or state (`north`, `opened`, `free`, …) are
kept out of the lists — they are not materials, and they would crowd out the
real choices.

## What it leaves alone

- **The landscape.** Only blocks somebody placed are listed or swapped — grass,
  soil, gravel, sand, ore, raw stone, water and plant life are skipped, so a
  build's own materials aren't buried under the ground it stands on, and
  restyling a wall never repaints the hillside behind it. A block counts as
  natural if the game files it under the *terrain* or *flora* creative tab
  without also offering it under *construction* or *decorative*, which is how
  modded blocks are judged too; blocks in no tab at all (every stair facing but
  the first) go by their material instead.
- **Blocks holding contents or state** — chests, doors, and anything else with a
  block entity are skipped and reported, rather than swapped and emptied.
- **Chiselled blocks** are re-materialed in place instead of being replaced, so
  the carving is kept. This is the same field the game's own material exchange
  rewrites.

## Undo

The last 25 swaps per player are undoable, newest first. Undo restores what was
there before the swap even if the area has been built on since — getting the
material back matters more than protecting edits made inside the footprint
afterwards.

The undo history lives in memory, so it does not survive a server restart. The
marked area doesn't either.

## Limits

- 512,000 blocks per selection, and 256 blocks of auto-detected height.
- Decor (mouldings, carpets attached to a block face) is not swapped.
- A chiselled block's material only changes if the material itself is swappable.
  Chiselled blocks are the one place raw stone still counts — somebody carved
  it, so it moves with the rest of the build.

## Building

```sh
./deploy.sh
```

Builds and drops `Swatch_<version>.zip` into `~/.config/VintagestoryData/Mods`,
removing older copies first. Needs the Vintage Story install at
`~/Apps/vintagestory` or `$VINTAGE_STORY` pointing at it.
