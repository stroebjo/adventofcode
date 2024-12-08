use std::fs;

fn read_input() -> Vec<Vec<i32>> {
    let file_path = "input.txt";

    let contents = fs::read_to_string(file_path)
        .expect("Should have been able to read the file");

    let matrix: Vec<Vec<i32>> = contents
        .lines()
        .map(|line| {
            line.split_whitespace()
                .map(|num| num.parse::<i32>().unwrap())
                .collect()
        })
        .collect();

    return matrix;
}

fn part1() {
    let matrix = read_input();

    let mut safe_reports = 0;

    for report in matrix  {

        let mut safe = true;
        let mut last_level = report[0];
        let decreasing = report[0] > report[1];

        for &level in &report[1..] {

            let delta = (last_level - level).abs();
            
            if !(delta > 0 && delta < 4) {
                safe = false;
                break;
            }
            
            if decreasing {
                if !(last_level > level) {
                    safe = false;
                    break;
                }
            } else { // increasing
                if  !(last_level < level) {
                    safe = false;
                    break;
                }
            }

            last_level = level;
        }

        if safe {
            safe_reports += 1;
        }
    }

    println!("Safe reports {safe_reports}");
}

fn part2() {
    
}

fn main() {
    part1();
    part2();
}
