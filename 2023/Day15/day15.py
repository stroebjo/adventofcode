

def HASH(str):
    v = 0
    for c in list(str):
        ascii_dec = ord(c)
        v += ascii_dec
        v *= 17
        v = v % 256
    return v

with open("input.txt", "r", encoding="utf-8") as f:
    steps = f.read().strip().split(",")

# -- part1 --
the_sum = 0
for s in steps:
    the_sum += HASH(s)
print(the_sum)

# -- part2 --
boxes = {}

for s in steps:
    if s[-1] != '-':
        label, focal_length = s.split('=')
    else:
        label = s[:-1]
        focal_length = None

    box_id = HASH(label)
    if box_id not in boxes:
        boxes[box_id] = []

    if focal_length:
        # insert/update lense
        found = False
        for i, entry  in enumerate(boxes[box_id]):
            if entry[0] == label:
                boxes[box_id][i] = [label, focal_length]
                found = True
                break

        if not found:
            boxes[box_id].append([label, focal_length])
    else:
        # remove
        for i, entry  in enumerate(boxes[box_id]):
            if entry[0] == label:
                del boxes[box_id][i]
                break

    if False:
        print(f"After \"{s}\":")
        for i in range(256):
            if i in boxes and len(boxes[i]) > 0:
                contents = ""
                for e in boxes[i]:
                    contents += f"[{e[0]} {e[1]}] "
                print(f"Box {i}: {contents}")
        print()

focus_power = 0
for box_i in range(256):
    if box_i in boxes and len(boxes[box_i])  > 0:
        for slot, e in enumerate(boxes[box_i]):
            focus_power += (box_i+1) * (slot+1) * int(e[1])
print(focus_power)
