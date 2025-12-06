use std::fs;
use regex::Regex;

fn read_input() -> String {
    let file_path = "input.txt";
    let contents = fs::read_to_string(file_path).expect("Should have been able to read the file");
    // NO trim() here! we need the whitespaces at the last string of the last line!
    return contents.to_string();
}

fn part1() {
    let contents = read_input();
    let lines: Vec<&str> = contents.lines().collect();
    let (raw_ops, number_lines) = lines.split_last().unwrap();

    
    // parse
    let re = Regex::new(r"\s+").unwrap();
    let mut parsed_numbers: Vec<Vec<u64>> = Vec::new();
    for raw_line in number_lines {
        let line = re.replace_all(raw_line, " ").trim().to_string();
        let mut numbers: Vec<u64> = Vec::new();

        for str_number in line.split(" ") {
            let number = str_number.parse().unwrap();
            numbers.push(number);
        }

        parsed_numbers.push(numbers);
    }

    // calc
    let mut grand_total = 0u64;
    let lines = parsed_numbers.len();
    let ops = re.replace_all(raw_ops, " ").trim().to_string();
    
    let mut i = 0;
    for op in ops.split(" ") {
        if op == "+" {
            let mut result = 0u64;
            for l in 0..lines {
                result += parsed_numbers[l][i];
            }
            grand_total += result;
        } else if op == "*" {
            let mut result = 1u64;
            for l in 0..lines {
                result *= parsed_numbers[l][i];
            }
            grand_total += result;
        }

        i += 1;
    }

    println!("{grand_total}");
}

fn part2() {
    let contents = read_input();
    let lines: Vec<&str> = contents.lines().collect();
    let (raw_ops, number_lines) = lines.split_last().unwrap();
    let lines_count = number_lines.len();

    // get column widths
    let mut grand_total = 0u64;
    let mut start = raw_ops.len()-1;
    let mut end = 0;

    for i in (0..raw_ops.len()).rev() {
        let ch = raw_ops.chars().nth(i).unwrap();

        if ch == '*' || ch == '+' {
            end = i;

            let mut result = 0u64;
            if ch == '*' {
                result = 1;
            }

            for j in ((end as usize)..(start+1)as usize).rev() {

                let mut s = String::new();
                
                for l in 0..lines_count {
                    let nch = number_lines[l].chars().nth(j).unwrap();
                    if nch != ' ' {
                        s.push(nch);
                    }
                }

                let n: u64 = s.parse().unwrap();
                if ch == '*' {
                    result *= n;
                } else if ch == '+' {
                    result += n;
                }
            }
            grand_total += result;

            // check if this is the last iteration, since i is usize, subtracting would
            // lead to an negative overflow, and we break before
            if i == 0 {
                break;
            }
            start = i-2; // skip column separator
        }
    }

    println!("{grand_total}");
}

fn main() {
    part1();
    part2();
}
