input <- readLines("input.txt")

# Part1 
total <- 0

for(line in input) {
  parts <- as.list(strsplit(line, "")[[1]])
  l <- length(parts)
  
  compartment1 <- parts[0:(l/2)]
  compartment2 <- parts[(l/2+1):l]
  same <- intersect(compartment1, compartment2)[1]
 
  wat <- charToRaw(paste(same[1], ""))
  x <- as.integer(wat[1])
  
  if (x >= 97) {
    x <- x - 96
  } else {
    x <- x - 64 + 26
  }
  
  total <- total + x
}

print(total)

# Part2
total <- 0
steps <- seq(0, length(input)-3, by=3)

for(i in steps) {
  elf1 <- as.list(strsplit(input[i+1], "")[[1]])
  elf2 <- as.list(strsplit(input[i+2], "")[[1]])
  elf3 <- as.list(strsplit(input[i+3], "")[[1]])
  
  a <- intersect(elf1, elf2)
  b <- intersect(elf2, elf3)
  c <- intersect(a, b)

  wat <- charToRaw(paste(c[1], ""))
  x <- as.integer(wat[1])
  
  if (x >= 97) {
    x <- x - 96
  } else {
    x <- x - 64 + 26
  }
  
  total <- total + x
}


print(total)