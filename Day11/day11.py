import copy
import re

with open("input.txt") as f:
    monkey_spec = f.read().split('\n\n')

monkeys = {}
largest = 1

# parse monkeys
for m in monkey_spec:
    spec = m.split('\n')
    
    test = spec[3].split(':')
    monkey_id = int(re.findall(r'\d+', spec[0])[0])
    monkey = {
        'items':       list(map(int, re.findall(r'\d+', spec[1]))),
        'op':          spec[2].split('=')[1].split(' '),
        'divisible':   int(re.findall(r'\d+', spec[3])[0]),
        'true':        int(re.findall(r'\d+', spec[4])[0]),
        'false':       int(re.findall(r'\d+', spec[5])[0]),
        'inspections': 0
    }
    largest *= monkey['divisible']
    monkeys[monkey_id] = monkey
tmp = copy.deepcopy(monkeys)

def worry_level(wl, op):
    p = wl if op[3] == 'old' else int(op[3])

    if (op[2] == '*'):
        return wl * p

    if (op[2] == '+'):
        return wl + p

    if (op[2] == '-'):
        return wl - p

    if (op[2] == '/'):
        return wl / p

# part1
for r in range(20):
    for i, m in monkeys.items():
        item = m['items'].pop(0) if m['items'] else False
        while item:
            wl = worry_level(item, m['op']) // 3

            nmi = m['true'] if (wl % m['divisible'] == 0) else m['false']
            monkeys[nmi]['items'].append(wl)
            m['inspections'] += 1
            item = m['items'].pop(0) if m['items'] else False

ins = []
for i, m in monkeys.items():
    ins.append(m['inspections'])
ins.sort()

print(ins[-1] * ins[-2])

# part2
monkeys = tmp

for r in range(10000):    
    for i, m in monkeys.items():
        item = m['items'].pop(0) if m['items'] else False
        while item:
            # score int would get to large "crop" so it never get's bigger
            # than the product of all monkey divisors
            wl = worry_level(item, m['op']) % largest

            nmi = m['true'] if (wl % m['divisible'] == 0) else m['false']
            monkeys[nmi]['items'].append(wl)
            m['inspections'] += 1
            item = m['items'].pop(0) if m['items'] else False

    #if (r+1) in [1, 20] or (r+1)%1000==0:
    #    print(f"== After round {r+1} ==")
    #    for j, m in monkeys.items():
    #        print(f"Monkey {j} inspected items {m['inspections']} times.")

ins = []
for i, m in monkeys.items():
    ins.append(m['inspections'])
ins.sort()

print(ins[-1] * ins[-2])
