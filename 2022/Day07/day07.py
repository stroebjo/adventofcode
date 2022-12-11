class Node:
    def __init__(self, name, t='folder', size=0):
        self.name = name
        self.type = t
        self.children = []
        self.size = size
        self.parent = None
    
    def get_size(self):
        if (self.type == 'file'):
            return self.size
        
        if (self.size == 0):
            for c in self.children:
                self.size += c.get_size()
            
        return self.size
        
with open('input.txt') as f:
    lines = f.read().splitlines() 

tree = Node('/')
pointer = tree

for l in lines[1:]: # skip first cd /, we already have out Node(/)
    p = l.split(' ')
    
    # if command, parse it
    if p[0] == '$':
        if (p[1] == 'ls'):
            pass # nothing to do here, output is in the followinf interations
        elif (p[1] == 'cd'):            
            if (p[2] == '..'):
                pointer = pointer.parent # set cwd to parent
            else:
                # new folder and cwd to it
                n = Node(p[2])
                n.parent = pointer
                pointer.children.append(n)
                pointer = n
        continue
    
    # ls output parsing
    
    if (p[0] == 'dir'):
        continue # we don't care about dir output in ls, we cd ... into it anyway

    pointer.children.append(Node(p[1], t='file', size=int(p[0])))

below_100k = 0
total_available = 70000000
update_space = 30000000
free_space = total_available - tree.get_size()
still_needed_space = update_space - free_space

possible_folders = []

def traverse_tree(n: Node, step=0):
    global below_100k
    global possible_folder
    
    indent = '  ' * step    
    info = n.size
    
    if n.type == 'folder':
        s = n.get_size()
        info = f"dir, {s}"
        
        if s < 100000:
            below_100k += s
            
        if s >= still_needed_space:
            possible_folders.append(s)
    
    #print(f"{indent}- {n.name} ({info})")
    
    for c in n.children:
        traverse_tree(c, step+1)
        
traverse_tree(tree)

print(below_100k)
print(min(possible_folders))
