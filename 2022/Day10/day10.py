with open("input.txt") as f:
    instructions = [x.strip('\n') for x in f.readlines()]

# cycles of interest
marks = [20, 60, 100, 140, 180, 220]
signal_strength = 0

# CPU registers
X = 1
cycle = 1

row = 0
col = 0
width = 40
height = 6
screen = []
for y in range(height):
    screen.append([' '] * width)

for op in instructions:
    p = op.split(' ')    
    opx = 0
    opcycles = 1
    
    if p[0] == 'addx':
        opcycles = 2
        opx = int(p[1])
        
    for i in range(opcycles):
        # check signal strenght
        if (cycle in marks):
            print(f"[{cycle}] X={X}, signal_strength={X*cycle}")
            signal_strength += X * cycle
           
        # check CRT
        # bound check on sprite is necessary so a for X=-1 the sprite pos
        # at the end get's not changed. Sprite "moves out" at the edges.
        sprite = list("░" * 40)
        if 0 <= (X-1) < 40: sprite[X-1] = '█'
        if 0 <= (X)   < 40: sprite[X]   = '█'
        if 0 <= (X+1) < 40: sprite[X+1] = '█'
            
        #print('Sprite: ', ''.join(sprite), f'col={col}')
        screen[row][col] = sprite[col]

        # more clever solution...
        #screen[row][col] = '░'
        #if col - X in [-1,0,1]:
        #    screen[row][col] = '█'
        
        col += 1
        if (col == width):
            col = 0
            row += 1
        
        cycle += 1
    
    if (opx != 0):
        X += opx

    
# Part 1
print(signal_strength)
print()

# Part 2
for y in range(height):
    print(''.join(screen[y]))