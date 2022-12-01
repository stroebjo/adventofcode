f = open("input.txt", "r")
lines = f.readlines()
elves = []
current = 0

for i in range(len(lines)):
    x = lines[i]
    if (x == "\n"):
        elves.append(current)
        current = 0 
        continue

    current += int(x)
elves.append(current)

print(max(elves))

elves.sort()
print(sum(elves[-3:]))