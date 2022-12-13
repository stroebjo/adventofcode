from functools import cmp_to_key

PRINT_LOG = False

def log(msg):
    if (PRINT_LOG):
        print(msg)

def compare(l, r, d=0):
    """ Custom compare function for packets
    return
    - 0 same
    - -1 left is smaller
    - +1 right is smaller
    """
    log(f"{'  '*d}- Compare {l} vs {r}")

    # both ints, lower wins
    if type(l) == int and type(r) == int:
        if l == r:
            return 0

        if l < r:
            log(f"{'  '*d}  - Left side is smaller, so inputs are in the right order")
            return -1
        else:
            log(f"{'  '*d}  - Right side is smaller, so inputs are not in the right order")
            return 1

    if type(l) == int:
        log(f"{'  '*d}- Mixed types; convert left to {l} and retry comparison")
        return compare([l], r, d)
    if type(r) == int:
        log(f"{'  '*d}- Mixed types; convert right to {r} and retry comparison")
        return compare(l, [r], d)

    max_length = min(len(l), len(r))
    for i in range(max_length):
        status = compare(l[i], r[i], d+1)
        if status != 0:
            return status
        # if same, continue

    # one side is longer
    if len(l) < len(r):
        log(f"{'  '*d}- Left side ran out of items, so inputs are in the right order")
        return -1

    if len(l) > len(r):
        log(f"{'  '*d}- Right side ran out of items, so inputs are not in the right order")
        return 1

    # same length, continue with previous level
    return 0

# Part 1
with open('input.txt') as f:
    pairs = f.read().strip().split('\n\n')

pair_index = 1
indices = []
for pair in pairs:
    left, right = map(eval, pair.split("\n"))

    log(f"== Pair {pair_index} ==")

    if compare(left, right) <= 0:
        indices.append(pair_index)

    log("")

    pair_index += 1

print(sum(indices))

# Part 2
with open('input.txt') as f:
    packets = list(map(eval, f.read().strip().replace('\n\n', '\n').split('\n')))

# divider packets
packets.append([[2]])
packets.append([[6]])

packets = sorted(packets, key=cmp_to_key(compare))
print((packets.index([[2]])+1) * (packets.index([[6]])+1))
