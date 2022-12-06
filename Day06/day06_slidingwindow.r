input <- readLines("input.txt")

window_size <- 14

for(line in input) {
  is <- 1
  ie <- window_size
  
  split <- strsplit(line, "")[[1]]
  l <- length(split)
  
  while(ie <= l) {
    w <- split[is:ie]
    
    if (length(unique(w)) == window_size) {
      print(ie)
      break
    }
    
    is <- is + 1
    ie <- ie + 1
  }
}
