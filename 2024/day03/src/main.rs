use regex::Regex;

use std::fs;

fn read_input() -> String {
    let file_path = "input.txt";
    let contents = fs::read_to_string(file_path)
        .expect("Should have been able to read the file");
    return contents;
}


fn part1() {
    let contents = read_input();

    let re = Regex::new(r"mul\(([0-9]{1,3}),([0-9]{1,3})\)").unwrap();
    let mut sum = 0;

    for (_, [n1, n2]) in re.captures_iter(&contents).map(|c| c.extract()) {
        sum += n1.parse::<i32>().unwrap() * n2.parse::<i32>().unwrap();
    }

    println!("{sum}");
}

fn part2() {
    let contents = read_input();

    // the idea was to to just remove the parts between don't() ... do() and perform
    // the rest like in part1. But this misses, that there is no closing "do()" in the
    // input. So a stable solution would need to use a flag to toggle (as described in 
    // the actual task).
    let redont = Regex::new(r"(?s)(don't\(\).*?do\(\))").unwrap();
    let contents2 = redont.replace_all(&contents, "");

    let re = Regex::new(r"mul\(([0-9]{1,3}),([0-9]{1,3})\)").unwrap();
    let mut sum = 0;

    for (_, [n1, n2]) in re.captures_iter(&contents2).map(|c| c.extract()) {
        sum += n1.parse::<i32>().unwrap() * n2.parse::<i32>().unwrap();
    }

    println!("{sum}");
}


fn main() {
    part1();
    part2();
}
