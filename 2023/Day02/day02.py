with open("input.txt", "r", encoding="utf-8") as f:
    games = f.read().strip().split("\n")

# -- part1 --
possible_sum = 0
gms = []

for game in games:
    game_id, sets = game.split(':')
    game_id = int(game_id.split(' ')[1])

    gmo = {
        'id': game_id,
        'rolls': []
    }

    possible = True
    colors = sets.split(';')
    for roll in colors:
        colors = roll.split(',')

        r = {
            'red': 0,
            'green': 0,
            'blue': 0,
        }

        for c in colors:
            count, color = c.strip().split(' ')
            r[color] = int(count)

        gmo['rolls'].append(r)

        if r['red'] > 12 or r['green'] > 13 or r['blue'] > 14:
            possible = False


    gms.append(gmo)
    if possible:
        possible_sum += game_id

print(possible_sum)

# -- part2 --
power_sum = 0

for game in games:
    game_id, sets = game.split(':')
    game_id = int(game_id.split(' ')[1])

    gmo = {
        'id': game_id,
        'min': {
            'red': 0,
            'green': 0,
            'blue': 0,
        }
    }

    colors = sets.split(';')
    for roll in colors:
        colors = roll.split(',')

        for c in colors:
            count, color = c.strip().split(' ')
            if gmo['min'][color] < int(count):
                gmo['min'][color] = int(count)

    power_sum += (gmo['min']['red'] * gmo['min']['green'] * gmo['min']['blue'])

print(power_sum)
