with open("input.txt", "r", encoding="utf-8") as f:
    layout = [list(l) for l in f.read().strip().split("\n")]

max_y = len(layout)
max_x = len(layout[0])

dirs = {
    'T': [0, -1],
    'R': [1, 0],
    'B': [0, 1],
    'L': [-1, 0],
}

def shoot_laser(beams):
    energized = [ [0] * max_x for _ in range(max_y)]
    traversed_splitters = []

    while beams:
        s = beams.pop(0)

        while True:
            p = [sum(x) for x in zip(s[0], dirs[s[1]])]
            d = s[1]

            # leaving the field?
            if p[0] < 0 or p[0] >= max_x or p[1] < 0 or p[1] >= max_y:
                break

            f = layout[p[1]][p[0]]
            if f == '.':
                # empty field, continue in same direction
                pass
            elif f == '|':
                if d in ['T', 'B']:
                    pass
                else:
                    new_beam = [[p[0], p[1]] , 'T']
                    if new_beam not in traversed_splitters:
                        traversed_splitters.append(new_beam)
                        beams.append(new_beam)

                    d = 'B'
                    if [p, d] not in traversed_splitters:
                        traversed_splitters.append([p, d])
                    else:
                        break

            elif f == '-':
                if d in ['T', 'B']:
                    new_beam = [[p[0], p[1]] , 'L']
                    if new_beam not in traversed_splitters:
                        traversed_splitters.append(new_beam)
                        beams.append(new_beam)

                    d = 'R'
                    if [p, d] not in traversed_splitters:
                        traversed_splitters.append([p, d])
                    else:
                        break
                else:
                    pass
            elif f == '/':
                if d == 'T':
                    d = 'R'
                elif d == 'R':
                    d = 'T'
                elif d == 'B':
                    d = 'L'
                elif d == 'L':
                    d = 'B'
            elif f == '\\':
                if d == 'T':
                    d = 'L'
                elif d == 'R':
                    d = 'B'
                elif d == 'B':
                    d = 'R'
                elif d == 'L':
                    d = 'T'

            energized[p[1] ][ p[0] ] += 1
            s = [p, d]

    energized_fields = 0
    for row in energized:
        for c in row:
            if c > 0:
                energized_fields += 1

    return energized_fields, energized

# -- part1 --
beams = [[[-1, 0], 'R']]
cnt, field = shoot_laser(beams)

for row in field:
    row_s = ""
    for c in row:
        if c > 0:
            row_s += '#'
        else:
            row_s += '.'
    print(row_s)
print(cnt)

# -- part2 --
max_cnt = 0
field = None

for x in range(max_x):
    # vvvv
    cnt, field = shoot_laser([[[x, -1], 'B']])
    if cnt > max_cnt:
        max_cnt = cnt

    # ^^^^
    cnt, field = shoot_laser([[[x, max_y], 'T']])
    if cnt > max_cnt:
        max_cnt = cnt

for y in range(max_y):
    # >
    # >
    # >
    # >
    cnt, field = shoot_laser([[[-1, y], 'R']])
    if cnt > max_cnt:
        max_cnt = cnt

    # <
    # <
    # <
    # >
    cnt, field = shoot_laser([[[max_x, y], 'L']])
    if cnt > max_cnt:
        max_cnt = cnt

print(max_cnt)
