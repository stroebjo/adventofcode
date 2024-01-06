with open("test.txt", "r", encoding="utf-8") as f:
    patterns = f.read().strip().split("\n\n")


def transpose(p):
    t = []
    for i in range(len(p[0])):
        row = []
        for j in range(len(p)):
            row.append(p[j][i])
        t.append(''.join(row))
    return t


def horizontal(p):
    max_y = len(p)
    for y in range(1, max_y):
        original   = p[0:y]
        reflection = p[y:]

        if False:
            print(f"Checking mirror between {y} and {y+1}")
            print()
            for a in original:
                print(a)
            print("---------")
            for a in reflection:
                print(a)
            print()

        original.reverse()

        is_reflection = True

        for i in range(0, min(len(original), len(reflection))):

            #print(f"    {i}:\n\t\t'{original[i]}'\n\t\t'{reflection[i]}'")

            if original[i] != reflection[i]:
                is_reflection = False
                break

        if is_reflection:
            return y

    return 0

total = 0
for p in patterns:
    p = p.split("\n")

    # horizontal
    a = horizontal(p)
    #print(a)
    #exit()
    b = horizontal(transpose(p))


    #if a == b == -1:
    #    for x in p:
    #        print(x)
    #    print()

    if a > 0:
        total += a * 100
    else:
        # vertical on transposed field
        total += horizontal(transpose(p))

print(total)

total = 0
for p in patterns:
    tt = p.split("\n")

    p = []
    for t in tt:
        p.append([*t])

    print("pattern")
    for y in range(len(p)):
        for x in range(len(p[y])):
            p2 = p.copy()
            #print('wat')

            if p2[y][x] == '#':
                p2[y][x] = '.'
            else:
                p2[y][x] = '#'

            h = horizontal(p2)
            v = horizontal(transpose(p2))

            print(" xxxx", h, v)


            if h == v:
                continue

            print(" ADDD!")

            total += h * 100
            total += v
            break

print(total)
