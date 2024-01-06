import re

with open("test.txt", "r", encoding="utf-8") as f:
    records = f.read().strip().split("\n")


def is_valid_arrangement(inp, dist):
    m = re.findall(r'#+', inp)
    return list(map(len, m)) == dist

valid_count = 0
i = 0
for r in records:
    i+=1
    print(i)
    pos, dist = r.split(' ')
    dist = list(map(int, dist.strip().split(',')))

    permutations = set()
    options = [""]
    for s in pos:
        next_options = []
        if s == '?':
            for o in options:
                o += '#'
                next_options.append(o)
            for o in options:
                o += '.'
                next_options.append(o)
        else:
            for o in options:
                o += s
                next_options.append(o)
        options = next_options


    for o in options:
        if is_valid_arrangement(o, dist):
            valid_count += 1

print("")
print(valid_count)

# -- part2 --
valid_count = 0
i = 0
for r in records:
    i+=1
    print(i)
    pos, dist = r.split(' ')
    dist = list(map(int, dist.strip().split(','))) * 5

    pos2 = [pos] * 5
    print(pos2)
    pos = "?".join(pos2)
    print(pos, dist)

    permutations = set()
    options = [""]
    for s in pos:
        next_options = []
        if s == '?':
            for o in options:
                o += '#'
                next_options.append(o)
            for o in options:
                o += '.'
                next_options.append(o)
        else:
            for o in options:
                o += s
                next_options.append(o)
        options = next_options


    for o in options:
        if is_valid_arrangement(o, dist):
            valid_count += 1

print("")
print(valid_count)
