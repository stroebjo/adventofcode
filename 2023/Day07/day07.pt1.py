from enum import IntEnum
from functools import cmp_to_key

class Type(IntEnum):
    FIVE_OF_A_KIND = 7
    FOUR_OF_A_KIND = 6
    FULL_HOUSE = 5
    THREE_OF_A_KIND = 4
    TWO_PAIR = 3
    ONE_PAIR = 2
    HIGH_CARD = 1

ranks = list(reversed(['A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2']))

def compare(l, r):
    """ Custom compare function for camel card hands
    return
    - 0 same
    - -1 left is smaller
    - +1 right is smaller
    """

    # Type
    tl = _get_type(l['cards'])
    tr = _get_type(r['cards'])

    if tl != tr:
        return tl - tr

    # Strength
    for i in range(len(l['cards'])):
        rl = ranks.index(l['cards'][i])
        rr = ranks.index(r['cards'][i])

        if rl != rr:
            return rl - rr

    # same cards
    return 0

def _get_type(cards):
    card_count = [0] * len(ranks)

    for c in cards:
        card_count[ranks.index(c)] += 1

    available_cards = [i for i in card_count if i != 0]
    if 5 in card_count:
        return Type.FIVE_OF_A_KIND
    elif 4 in card_count:
        return Type.FOUR_OF_A_KIND
    elif len(available_cards) == 2 and (3 in available_cards) and (2 in available_cards):
        return Type.FULL_HOUSE
    elif 3 in available_cards:
        return Type.THREE_OF_A_KIND
    elif available_cards.count(2) == 2:
        return Type.TWO_PAIR
    elif 2 in available_cards:
        return Type.ONE_PAIR

    return Type.HIGH_CARD

with open("input.txt", "r", encoding="utf-8") as f:
    hands_txt = f.read().strip().split("\n")

hands = []
for hand in hands_txt:
    cards, bid = hand.split(' ')

    hands.append({
        'cards': cards,
        'bid': int(bid)
    })
hands.sort(key=cmp_to_key(compare), reverse=False)

total_winnings = 0
for i, h in enumerate(hands):
    total_winnings += h['bid']*(i+1)
    print(f"{i+1:>4}. {h['cards']}: {h['bid']:>3} * {i+1:<3} = {h['bid']*(i+1)}")

print(total_winnings)
