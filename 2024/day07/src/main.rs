use std::fs;
use std::collections::VecDeque;


fn read_input() -> Vec<(u64, Vec<u64>)> {
    let file_path = "input.txt";

    let input = fs::read_to_string(file_path)
        .expect("Should have been able to read the file");

    input
        .lines()
        .filter_map(|line| {
            let mut parts = line.split(':');
            let key = parts.next()?.trim().parse::<u64>().ok()?;
            let values = parts.next()?
                .split_whitespace()
                .filter_map(|v| v.parse::<u64>().ok())
                .collect::<Vec<u64>>();
            Some((key, values))
        })
        .collect()
}

fn part1() {
    let equations = read_input();
    let mut possible_equations = 0;

    for (result, operands) in equations {
        let mut deque = VecDeque::from(operands);
        let mut results: Vec<u64> = Vec::new();
        results.push(deque.pop_front().unwrap());

        while let Some(next) = deque.pop_front() {
            let temp = results.clone();
            results.clear();

            for x in temp {
                results.push(x + next);
                results.push(x * next);
            }
        }

        for x in results {
            if x == result {
                possible_equations += result;
                break;
            }
        }
    }

    println!("{possible_equations}");
}

fn concatenate_u64(a: u64, b: u64) -> u64 {
    let b_digits = (b as f64).log(10.0).floor() as u64 + 1; // Number of digits in `b`
    a * 10u64.pow(b_digits as u32) + b // Concatenate by scaling and adding
}

fn part2() {
    let equations = read_input();
    let mut possible_equations = 0;

    for (result, operands) in equations {
        let mut deque = VecDeque::from(operands);
        let mut results: Vec<u64> = Vec::new();
        results.push(deque.pop_front().unwrap());

        while let Some(next) = deque.pop_front() {
            let temp = results.clone();
            results.clear();

            for x in temp {
                results.push(x + next);
                results.push(x * next);
                results.push(concatenate_u64(x, next));
            }
        }

        for x in results {
            if x == result {
                possible_equations += result;
                break;
            }
        }
    }

    println!("{possible_equations}");
}

fn main() {
    part1();
    part2();
}
