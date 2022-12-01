input <- readLines("input.txt")
input <- as.integer(input)

elves <- c()
calories <- 0

for(x in input) {
  if (is.na(x)) {
    elves <- append(elves, calories)
    calories <- 0
    next
  }
  calories <- (calories + x)
}
elves <- append(elves, calories) # loop will not catch final block

print(max(unlist(elves)))

elves <- sort(elves)
print(sum(tail(elves, 3)))
