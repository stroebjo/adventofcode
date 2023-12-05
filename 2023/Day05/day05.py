import sys

with open("input.txt", "r", encoding="utf-8") as f:
    seed_map = f.read().strip().split("\n\n")

# -- part1 --
seeds = list(map(int, seed_map.pop(0).split(':')[1].strip().split(' ')))
min_location = sys.maxsize

for s in seeds:
    x = s

    for m in seed_map:
        mapping_label, lines = m.split(':')

        for l in lines.strip().split("\n"):
            dest, source, length  = map(int, l.strip().split(' '))

            if source <= x <= (source + length):
                x = dest + (x - source)
                break

        if mapping_label.split('-')[2] == 'location map':
            if x < min_location:
                min_location = x

print(min_location)

# -- part2 --
