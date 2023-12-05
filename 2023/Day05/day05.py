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
# brute force solution, don't try at home.
# starts with the 0 as location ID and traverses the mappings backwards
# until the first seed in a start range is found, since we start at the
# bottom this also has to be the lowest.
ranges = []
for i in range(0, len(seeds), 2):
    ranges.append([seeds[i], seeds[i+1]])

location_id = 0
do = True

while do:
    x = location_id

    if x % 100000 == 0:
        print(x)

    for m in reversed(seed_map):
        mapping_label, lines = m.split(':')
        old_x = x

        labels = mapping_label.split(' ')[0].split('-')

        for l in lines.strip().split("\n"):
            dest, source, length  = map(int, l.strip().split(' '))

            source_s = source
            source_e = source + length -1

            dest_s = dest
            dest_e = dest + length -1

            if dest_s <= x <= dest_e:
                x = source + (x - dest)
                break

        if mapping_label == 'seed-to-soil map':
            for r in ranges:
                if r[0] <= x <= (r[0]+r[1]):
                    do = False
                    break

    if do:
        location_id += 1

print(location_id)
