import re

with open("input.txt", "r", encoding="utf-8") as f:
    field = f.read().strip().split("\n")

# -- part1 --
symbols = ['*', '#', '$', '+', '&', '@', '/', '=', '-', '%']

def is_part(f, x1, y1, x2, y2):
    max_x = len(f[0])
    max_y = len(f)
    l = x2 - x1

    for i in range(l):
        for dx in [-1, 0, 1]:
            p_start = x1 + i
            for dy in [-1, 0, 1]:

                x = p_start + dx
                y = y1 + dy

                if 0 > x or x >= max_x:
                    continue
                if 0 > y or y >= max_y:
                    continue

                if field[y][x] in symbols:
                    return True

    return False

sum_parts = 0
for y, line in enumerate(field):
    parts = re.finditer(r"\d+", line)

    for m in parts:
        x_start = m.start()
        p = m.group()

        if is_part(field, x_start, y, x_start+len(p), y):
            sum_parts += int(p)

print(sum_parts)


# -- part2 --
def get_gears(f, x1, y1):
    max_x = len(f[0])
    max_y = len(f)

    gear_parts = []

    for dx in [-1, 0, 1]:
        for dy in [-1, 0, 1]:

            x = x1 + dx
            y = y1 + dy

            if 0 > x or x >= max_x:
                continue
            if 0 > y or y >= max_y:
                continue

            if '0' <= field[y][x] <= '9':
                gear_parts.append({
                    'y': y,
                    'x': x
                })
    # use a dict with (y, x_1, x_2) of number.
    # -> same number could be used like:
    # 123.123
    # ...*...
    # .......
    #
    # y is also needed since some numbers could start at the same x:
    # ..3.123
    # ...*...
    # ..4....
    nrs = {}

    for gear in gear_parts:
        parts = re.finditer(r"\d+", f[gear['y']])
        for m in parts:
            if m.start() <= gear['x'] <= m.end():
                nrs[(gear['y'], m.start(), m.end())] = int(m.group())

    if len(nrs) != 2:
        return []

    return list(nrs.values())

sum_ratios = 0
for y, line in enumerate(field):
    parts = re.finditer(r"\*", line)

    for m in parts:
        g = get_gears(field, m.start(), y)

        if len(g):
            sum_ratios += g[0] * g[1]
print(sum_ratios)
