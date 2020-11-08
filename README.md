2d generated lavels similar to Spelunky.

Basic concept:
	- Level consists of a grid of n x m rooms.
	- Level generation is based on seed. A given seed should always result in the exact same 	generated level.
	- The top tow contains a spawning room for the player.
	- The bottom row contains a goal room that has to be reached to finish the level.
 	- There is always a guaranteed path that is traversable from the start room to the goal goom.
 	- On each row the guaranteed path has at least one horizontal step (to the left or right) before descending to the next lower row.

Generation:
 	- Initialize random number generation with seed
	- Place Start room at a random position on the top floor
	- Place goal room on the bottom floor
 	- Generate a random path from start to goal and plan for these room to require the openings to make the path possible
 	- Fill the grid with random rooms satisfiying the requirements. This puts random rooms 	outside of the required path.
	- The settings for the generation are combined in a scriptable object used as a config file.

Rooms:
 	- Rooms are designercreated as prefabs and added to a pool of rooms.
	- Room variations can contain set tiles for floors, traps etc. And spawnpoints to have certain tiles or groups of tiles be random in the rooms itself to create more variation.
	- Room variations are grouped by openings for the algorithm.
	- There are special rooms for spawn and goal containing a spawn point and a goal area.

Spelunky level generation as a reference:
	http://tinysubversions.com/spelunkyGen/
