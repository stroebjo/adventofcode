import re


with open("input.txt", "r", encoding="utf-8") as f:
    txt = f.read().strip().split("\n")

# -- part1 --
options = 1
races = list(zip(
    list(map(int, re.findall(r"\d+", txt[0]))),
    list(map(int, re.findall(r"\d+", txt[1])))
))

for time, distance in races:
    option_count = 0
    a = 0

    while time > 0:
        if a * time > distance:
            option_count += 1

        time -= 1
        a += 1

    options *= option_count

print(options)

# -- part2 --
options = 1
races = list(zip(
    list(map(int, re.findall(r"\d+", txt[0].replace(' ', '')))),
    list(map(int, re.findall(r"\d+", txt[1].replace(' ', ''))))
))

for time, distance in races:
    option_count = 0
    a = 0

    while time > 0:
        if a * time > distance:
            option_count += 1

        time -= 1
        a += 1

    options *= option_count

print(options)
