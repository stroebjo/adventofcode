install.packages("stringr")
install.packages("tictoc")

library("stringr")
library("tictoc")

input <- readLines("input.txt")
parse_stacks <- FALSE
crates <- list()

reverse_stack <- function(stack) {
  rstack <- Stack()
  l <- size(stack)
  while (l > 0) {
    push(rstack, pop(stack))
    l <- l - 1
  }
  return(rstack)
}

for(line in input) {
  
  # parse stacks
  if (!parse_stacks) {
    line_length <- str_length(line)

    # separator line for initials setup and instructions
    if (line_length == 0) {
      parse_stacks <- TRUE
      
      # we parse the stacks top to bottom, so we need to reverse the order
      for (i in names(crates)) {
        crates[[i]] <- reverse_stack(crates[[i]])
      }
      
      next
    }
    
    # skip lines with stack index
    if (!grepl("[", line, fixed = TRUE)) {
      next
    }
    
    stacks <- seq(2, line_length, by=4)
    stack_index <- 1
    for (i in stacks) {
      crate <- substr(line, i, i)
      if (crate != " ") {
        # as.character() since R can't handle numeric name (?!)
        if (is.null(crates[[as.character(stack_index)]]) ) {
          crates[[as.character(stack_index)]] <- Stack()
        }
        push(crates[[as.character(stack_index)]], crate)
      }
      stack_index <- stack_index + 1
    }
    next
  }
  
  # parse instructions
  print(line)
  instructions <- str_split(line, " ",simplify=TRUE)
  
  
  # part1
  #for (i in seq(1, as.numeric(instructions[2]))) {
  #  push(crates[[instructions[6]]], pop(crates[[instructions[4]]]))
  #}
  
  
  # part2
  craneStack <- Stack()
  for (i in seq(1, as.numeric(instructions[2]))) {
    push(craneStack, pop(crates[[instructions[4]]]))
  }
  
  l <- size(craneStack)
  while (l > 0) {
    push(crates[[instructions[6]]], pop(craneStack))
    l <- l - 1
  }
}

# print solution
for (i in str_sort(names(crates))) {
  print(pop(crates[[i]]))
}
