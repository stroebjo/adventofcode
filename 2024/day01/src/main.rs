use std::fs;

fn read_input() -> (Vec<i32>, Vec<i32>) {
    let file_path = "input.txt";

    let contents = fs::read_to_string(file_path)
        .expect("Should have been able to read the file");

    // Vectors to store numbers
    let mut numbers1 = Vec::new();
    let mut numbers2 = Vec::new();

    for line in contents.lines() {
        let parts: Vec<&str> = line.split_whitespace().collect();

        if parts.len() == 2 {
            // Parse and store the numbers
            if let (Ok(num1), Ok(num2)) = (parts[0].parse::<i32>(), parts[1].parse::<i32>()) {
                numbers1.push(num1);
                numbers2.push(num2);
            } else {
                eprintln!("Failed to parse line: {}", line);
            }
        } 
    }

    return (numbers1, numbers2)
}

fn part1() {
    let (mut numbers1, mut numbers2) = read_input();

    numbers1.sort();
    numbers1.reverse();
    numbers2.sort();
    numbers2.reverse();

    let mut total_distance = 0;

    while let (Some(num1), Some(num2)) = (numbers1.pop(), numbers2.pop()) {
        total_distance += (num1 - num2).abs();
    }

    println!("Total distance is {total_distance}")
}

fn part2() {
    let (numbers1, numbers2) = read_input();

    let mut similarity_score = 0;

    for num1 in numbers1 {
        similarity_score += num1 * (numbers2.iter().filter(|&n| *n == num1).count() as i32)
    }

    println!("Similarity score {similarity_score}")
}

fn main() {
    part1();
    part2();
}
