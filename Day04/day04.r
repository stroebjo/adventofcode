install.packages("stringr")
library("stringr")


input <- readLines("input.txt")

fullyContains <- 0
overlap <- 0

for (line in input) {
  elves <- str_split(line, ",", 2, TRUE)
  elf1 <- as.integer(str_split(elves[1], "-", 2, TRUE))
  elf2 <- as.integer(str_split(elves[2], "-", 2, TRUE))
  
  # fully contained
  if ((elf1[1] <= elf2[1] && elf1[2] >= elf2[2]) || 
      (elf2[1] <= elf1[1] && elf2[2] >= elf1[2]) ) {
    fullyContains <- fullyContains + 1
    overlap <- overlap + 1
    next # leave the loop, both counters are incremented
  }
  
  # check for overlap
  if (elf1[1] <= elf2[1] && elf1[2] >=elf2[1]) {
    overlap <- overlap + 1
  } else if (elf2[2] <= elf1[2] && elf2[1] >=elf1[2]) {
    overlap <- overlap + 1
  } else if (elf1[1] >= elf2[2] && elf1[2] <= elf2[2]){
    overlap <- overlap + 1
  } else if (elf2[1] >= elf1[2] && elf2[2] <= elf1[2]){
    overlap <- overlap + 1
  } else if (elf2[1] <= elf1[2] && elf2[2] >= elf1[1]) {
    overlap <- overlap + 1
  }
}

print(fullyContains)
print(overlap)
