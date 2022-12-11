input <- readLines("input.txt")

mapping <- c(
  'A' = 'Rock',
  'B' = 'Paper',
  'C' = 'Scissors',
  
  'X' = 'Rock',
  'Y' = 'Paper',
  'Z' = 'Scissors'
)

win <- c(
  "Rock" = "Scissors",
  "Scissors" = "Paper",
  "Paper" = "Rock"
)

lose <- c(
  "Scissors" = "Rock",
  "Paper" = "Scissors",
  "Rock" = "Paper"
)

match <- function(they, you) {
  points <- 0
  
  # points for selection
  if (you == "Rock") {
    points <- points + 1
  } else if (you == "Paper") {
    points <- points + 2
  } else if (you == "Scissors") {
    points <- points + 3
  }
  
  # match points
  if (win[you] == they) {
    points <- points + 6
  } else if (you == they) {
    points <- points + 3
  }
  
  return(points)
}

total_a <- 0
total_b <- 0

for(x in input) {
  they <- mapping[substr(x, 0, 1)]
  y <- substr(x, 3, 4)
  you  <- mapping[y]
  
  total_a <- total_a + match(they, you)
  
  if (y == "X") {
    you <- win[they]
  } else if (y == "Y") {
    you <- they
  } else {
    you <- lose[they]
  }
  

  total_b <- total_b + match(they, you)
}

print(total_a)
print(total_b)