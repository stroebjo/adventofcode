import re

with open("input.txt", "r", encoding="utf-8") as f:
    cards = f.read().strip().split("\n")

# -- part1 --
total_points = 0
for i, card in enumerate(cards):
    a, b = card.split('|')
    winning =  re.findall(r"\d+", a)[1:]
    my = re.findall(r"\d+", b)

    points = 0.5 # use float to get 1 point for the first match

    for n in my:
        if n in winning:
            points *= 2

    # cast to int so 0.5 -> 0 in case of 0 matches
    total_points += int(points)

print(total_points)

# -- part2 --
total_cards = [1] * len(cards) # each card exists at least once
while cards:
    card = cards.pop(0)
    a, b = card.split('|')
    card_id, *winning = map(int, re.findall(r"\d+", a))
    my = map(int, re.findall(r"\d+", b))

    winning_numbers = 0

    for n in my:
        if n in winning:
            winning_numbers += 1

    for c in range(winning_numbers):
        # cards can't duplicate backwards, so the current count of copies
        # of this card is the final one
        total_cards[card_id+c] += 1 * total_cards[card_id-1]

print(sum(total_cards))
