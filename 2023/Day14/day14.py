import pandas as pd

with open("input.txt", "r", encoding="utf-8") as f:
    platform = [list(l) for l in f.read().strip().split("\n")]

def pp(p):
    for r in platform:
        print(''.join(r))
    print()

def tilt_north(p):
    max_y = len(p)
    max_x = len(p[0])
    for y in range(max_y):
        for x in range(max_x):
            if p[y][x] == 'O':
                xt = x
                yt = y
                while True:
                    if (yt-1) < 0:
                        break
                    if p[yt-1][xt] != '.':
                        break
                    yt -= 1
                if yt != y:
                    p[yt][xt] = 'O'
                    p[y][x]   = '.'
    return p

def tilt_east(p):
    max_y = len(p)
    max_x = len(p[0])
    for y in range(max_y):
        for x in range(max_x-1, -1, -1):
            if p[y][x] == 'O':
                xt = x
                yt = y
                while True:
                    if (xt+1) >= max_x:
                        break
                    if p[yt][xt+1] != '.':
                        break
                    xt += 1
                if xt != x:
                    p[yt][xt] = 'O'
                    p[y][x]   = '.'
    return p

def tilt_south(p):
    max_y = len(p)
    max_x = len(p[0])
    for y in range(max_y-1, -1, -1):
        for x in range(max_x):
            if p[y][x] == 'O':
                xt = x
                yt = y
                while True:
                    if (yt+1) >= max_y:
                        break
                    if p[yt+1][xt] != '.':
                        break
                    yt += 1
                if yt != y:
                    p[yt][xt] = 'O'
                    p[y][x]   = '.'
    return p

def tilt_west(p):
    max_y = len(p)
    max_x = len(p[0])
    for y in range(max_y):
        for x in range(max_x):
            if p[y][x] == 'O':
                xt = x
                yt = y
                while True:
                    if (xt-1) < 0:
                        break
                    if p[yt][xt-1] != '.':
                        break
                    xt -= 1
                if xt != x:
                    p[yt][xt] = 'O'
                    p[y][x]   = '.'
    return p

def cycle(p):
    p = tilt_north(p)
    p = tilt_west(p)
    p = tilt_south(p)
    return tilt_east(p)

def total_load(p):
    max_y = len(p)
    max_x = len(p[0])
    tl = 0

    for y in range(max_y):
        for x in range(max_x):
            if p[y][x] == 'O':
                tl += (max_y - y)

    return tl

# -- part1 --
platform = tilt_north(platform)
print(total_load(platform))


# --part2 --
# finish cycle from pt1
platform = tilt_west(platform)
platform = tilt_south(platform)
platform = tilt_east(platform)

# save loads to CSV file for visual cycle analysis
rows = [{'cycle': 1, 'load': total_load(platform)}]
for i in range(400 - 1):
    platform = cycle(platform)
    rows.append({'cycle': i+2, 'load': total_load(platform)})
df = pd.DataFrame(rows)
df.to_csv('cycle.csv', index=False)

# test input
#transient_response = 8
#cycle_length = 7
#cycle_values = [63, 68, 69, 69, 65, 64, 65]
#x = 1000000000
#print(cycle_values[(x - transient_response) % len(cycle_values)])

# real input
transient_response = 83
cycle_length = 72
cycle_values = [99166, 99165, 99137, 99137, 99111, 99099, 99118, 99131, 99162,
       99150, 99139, 99142, 99146, 99095, 99112, 99133, 99147, 99146,
       99124, 99144, 99151, 99130, 99108, 99127, 99149, 99131, 99120,
       99129, 99153, 99135, 99143, 99123, 99143, 99133, 99105, 99125,
       99138, 99137, 99148, 99158, 99139, 99127, 99107, 99110, 99134,
       99122, 99150, 99163, 99174, 99123, 99101, 99112, 99119, 99118,
       99135, 99165, 99179, 99158, 99097, 99106, 99121, 99103, 99131,
       99150, 99181, 99163, 99132, 99102, 99115, 99105, 99116, 99146]
x = 1000000000
print(cycle_values[(x - transient_response) % len(cycle_values)])
