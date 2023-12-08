from pprint import pprint

def compare(l, r, d=0):
    """ Custom compare function for camel card hands
    return
    - 0 same
    - -1 left is smaller
    - +1 right is smaller
    """

    # type

    # same hands
    return 0


ranks = list(reversed(['A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2']))

def hand_to_bin(hand):
    #print(hand)

    value = 0

    mask_count = [0] * len(hand)
    card_count = [0] * len(ranks)

    pos = 0
    exp = len(hand)
    for c in hand:
        exp -= 1

        value += ranks.index(c) + 10**exp

        #print(f"{c} -- {ranks.index(c) + 10**exp}")
        card_count[ranks.index(c)] += 1
        mask_count[pos] += 1


        pos += 1
    #print(f"  -> {value}")

    available_cards = [i for i in card_count if i != 0]

    if 5 in card_count:
        value += 60000000
    elif 4 in card_count:
        value += 50000000
    elif len(available_cards) == 2 and (3 in available_cards) and (2 in available_cards):
        value += 40000000
    elif (3 in available_cards):
        value += 30000000
    elif available_cards.count(2) == 2:
        value += 20000000
    elif (2 in available_cards):
        value += 10000000

    return value

with open("input.txt", "r", encoding="utf-8") as f:
    hands_txt = f.read().strip().split("\n")

hands = []
for hand in hands_txt:
    cards, bid = hand.split(' ')

    hands.append({
        'cards': cards,
        'value': hand_to_bin(cards),
        'bid': int(bid)
    })

#pprint(hands)

hands.sort(key=lambda x: x['value'], reverse=False)
total_winnings = 0
for i, h in enumerate(hands):
    total_winnings += h['bid']*(i+1)
    print(f"{i+1:>4}. {h['cards']}: {h['bid']:>3} * {i+1:<3} = {h['bid']*(i+1)}")

print(total_winnings)
