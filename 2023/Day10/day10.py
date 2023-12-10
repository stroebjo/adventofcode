from collections import namedtuple

Point = namedtuple('Point', 'x y')
with open("input.txt", "r", encoding="utf-8") as f:
    field = f.read().strip().split("\n")

max_y = len(field)
max_x = len(field[0])
for y in range(max_y):
    for x in range(max_x):
        if field[y][x] == 'S':
            start = Point(x, y)

visited = [[0]*max_x for i in range(max_y)]
chart   = [['.']*max_x for i in range(max_y)]

REPLACE_PIPES = {
    "|": "│",
    "-": "─",
    "F": "┌",
    "L": "└",
    "7": "┐",
    "J": "┘",
    "S": "S",
}

plumber = {
    'S': {
        'N': '|7F',
        'E': '-J7',
        'S': '|LJ',
        'W':  '-LF'
    },
    '|': {
        'N': '|7FS',
        'E': '',
        'S': '|LJS',
        'W':  ''
    },
    '-': {
        'N': '',
        'E': '-J7S',
        'S': '',
        'W': '-FLS',
    },
    'L': {
        'N': '|F7S',
        'E': '-J7S',
        'S': '',
        'W':  ''
    },
    'J': {
        'N': '|F7S',
        'E': '',
        'S': '',
        'W':  '-LFS'
    },
    '7': {
        'N': '',
        'E': '',
        'S': '|LJS',
        'W':  '-LFS'
    },
    'F': {
        'N': '',
        'E': '-J7S',
        'S': '|LJS',
        'W':  ''
    }
}

steps = 0
visited[start.y][start.x] = 0
chart[start.y][start.x] = 'S'

finish = False
while not finish:
    for dx, dy, D in [(0, -1, 'N'), (0, 1, 'S'), (-1, 0, 'W'), (1, 0, 'E')]:
        p = Point(start.x + dx, start.y + dy)

        if p.x >= max_x or p.x < 0 or p.y < 0 or p.y >= max_y:
            continue

        current_pipe = field[start.y][start.x]
        next_pipe = field[p.y][p.x]

        if next_pipe == 'S' and steps < 2:
            continue

        if visited[p.y][p.x] > 0:
            continue

        if next_pipe not in plumber[current_pipe][D]:
            continue

        steps += 1
        visited[p.y][p.x] = steps
        chart[p.y][p.x] = REPLACE_PIPES[next_pipe]
        start = p

        if next_pipe == 'S':
            finish = True

        break # break here so wie don't use the second option after the start

for y in chart:
    s = ""
    for x in y:
        s += f"{x}"
    print(f"{s}")

print(int(steps/2))

# -- part2 --
