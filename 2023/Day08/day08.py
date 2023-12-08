import re
from math import lcm

with open("input.txt", "r", encoding="utf-8") as f:
    instructions, network = f.read().strip().split("\n\n")

nodes = {}
nn = []
for line in network.split("\n"):
    n, l, r = re.findall(r"[A-Z0-9]{3}", line)
    nodes[n] = {'L': l, 'R': r}

    if n[-1] == 'A':
        nn.append(n)

# -- part1 --
n = 'AAA'
c = 0
i = 0
while True:
    go = instructions[i]
    i = i+1 if i < len(instructions)-1 else 0
    c += 1

    n = nodes[n][go]

    if n == 'ZZZ':
        break

print(c)

# -- part2 --
ctoz = []
for n in nn:
    i = 0
    c = 0

    while True:
        go = instructions[i]
        i = i+1 if i < len(instructions)-1 else 0
        c += 1

        n = nodes[n][go]

        if n[-1] == 'Z':
            break

    ctoz.append(c)

print(lcm(*ctoz))
