input <- readLines("input.txt")

for(line in input) {
  split <- strsplit(line, "")[[1]]
  lowest_i <- length(split)
    
  for (j in seq(1:length(split))) {
    v <- c()
    i <- j - 1

    for (c in split[j:length(split)]) {
      if(c %in% v) {
        v <- c(c)
      } else {
        v <- append(v, c)
      }
      
      i <- i + 1
      
      if (length(v) == 14) {
        
        if (i <lowest_i ){
          lowest_i <- i
        }
      }
    }
  }
}

print(lowest_i)
