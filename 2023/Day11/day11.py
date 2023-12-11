from collections import namedtuple
import itertools

Point = namedtuple('Point', 'x y')

with open("input.txt", "r", encoding="utf-8") as f:
    universe = f.read().strip().split("\n")

max_y = len(universe)
max_x = len(universe[0])

empty_y = []
for y in range(max_y):
    if '#' not in universe[y]:
        empty_y.append(y)

empty_x = []
for x in range(max_x):
    col = ""
    for y in range(max_y):
        col += universe[y][x]
    if '#' not in col:
        empty_x.append(x)

galaxies1 = []
galaxies2 = []
for y in range(max_y):
    for x in range(max_x):
        if universe[y][x] == '#':

            # -- part1 --
            x1 = x + len([i for i in empty_x if i < x])
            y1 = y + len([i for i in empty_y if i < y])
            galaxies1.append(Point(x1, y1))

            # -- part2 --
            x2 = x + len([i for i in empty_x if i < x])*999999
            y2 = y + len([i for i in empty_y if i < y])*999999
            galaxies2.append(Point(x2, y2))

def manhatten_distance(p1, p2):
    return abs(p1.x - p2.x) + abs(p1.y - p2.y)

# -- part1 --
pairs = itertools.combinations(galaxies1, 2)
sum_shortest_paths = 0
for g1, g2 in pairs:
    d = manhatten_distance(g1, g2)
    sum_shortest_paths += d

print(sum_shortest_paths)

# -- part2 --
pairs = itertools.combinations(galaxies2, 2)
sum_shortest_paths = 0
for g1, g2 in pairs:
    d = manhatten_distance(g1, g2)
    sum_shortest_paths += d

print(sum_shortest_paths)
