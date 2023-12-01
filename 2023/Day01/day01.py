f = open("input.txt", "r")
codes = f.read().split("\n")

# -- part1 --
calibrations = []

for i, code in enumerate(codes):
    numbers=[]

    for c in code:
        if '0' <= c <= '9':
            numbers.append(c)

    if len(numbers) > 0:
        x = int(''.join([numbers[0], numbers[-1]]))
        calibrations.append(x)

print(sum(calibrations))

# -- part2 --
calibrations = []

for i, code in enumerate(codes):
    numbers=[]

    spelled = {
        'one':   1,
        'two':   2,
        'three': 3,
        'four':  4,
        'five':  5,
        'six':   6,
        'seven': 7,
        'eight': 8,
        'nine':  9
    }

    buf = ""
    for c in code:
        if '0' <= c <= '9':
            numbers.append(c)
            buf = ""
        else:
            buf += c

        for sp, n in spelled.items():
            # check for spelling in current buffer,
            # so we ignore random chars in front/behind
            if sp in buf:
                numbers.append(str(n))

                # for overlapping numbers, keep end of current buf
                buf = buf[-1:]

    if len(numbers) > 0:
        x = int(''.join([numbers[0], numbers[-1]]))
        calibrations.append(x)

print(sum(calibrations))
