from pprint import pprint

with open("input.txt", "r", encoding="utf-8") as f:
    series = f.read().strip().split("\n")

# -- part1 / part2 --
sum_extrapolated_forward  = 0
sum_extrapolated_backward = 0

for s in series:
    ms = [list(map(int, s.split(' ')))]
    while True:
        all0 = True
        d = []
        for i in range(len(ms[-1])-1):
            dt = ms[-1][i+1] - ms[-1][i]
            d.append(dt)
            all0 &= (dt == 0)
        ms.append(d)

        if all0:
            break

    ms[-1].append(0)
    for i in range(len(ms)-1, 0, -1):
        ms[i-1].append(ms[i-1][-1] + ms[i][-1])
        if (i-1)==0:
            sum_extrapolated_forward += ms[i-1][-1]

    ms[-1].insert(0, 0)
    for i in range(len(ms)-1, 0, -1):
        ms[i-1].insert(0, ms[i-1][0] - ms[i][0])
        if (i-1)==0:
            sum_extrapolated_backward += ms[i-1][0]

print(sum_extrapolated_forward)
print(sum_extrapolated_backward)
