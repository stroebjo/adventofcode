use std::fs;

fn read_input_matrix() -> Vec<Vec<char>> {
    let file_path = "input.txt";

    let contents = fs::read_to_string(file_path).expect("Should have been able to read the file");

    let char_vector: Vec<Vec<char>> = contents
        .lines()
        .map(|line| line.chars().collect())
        .collect();

    return char_vector;
}

fn part1() {
    let matrix = read_input_matrix();

    // for accessing the matrix we need usize, but for calculating our directions
    // we also need -1m, so we use isize and cast to usize for the actual access.
    let max_y = matrix.len() as isize;
    let max_x = matrix[0].len() as isize;

    let mut roll_count = 0;

    for y in 0..max_y {
        for x in 0..max_x {
            if matrix[y as usize][x as usize] != '@' {
                continue;
            }

            let mut adjacent_rolls = 0;

            // count adjacent
            for dy in [-1, 0, 1] {
                for dx in [-1, 0, 1] {
                    if dy == 0 && dx == 0 {
                        continue;
                    }

                    let test_y = y + dy;
                    let test_x = x + dx;

                    if test_y < 0 || test_y >= max_y || test_x < 0 || test_x >= max_x {
                        continue;
                    }

                    if matrix[test_y as usize][test_x as usize] == '@' {
                        adjacent_rolls += 1;
                    }
                }
            }

            if adjacent_rolls < 4 {
                roll_count += 1;
            }
        }
    }

    println!("{roll_count}");
}

fn part2() {
    let mut matrix = read_input_matrix();
    let max_y = matrix.len() as isize;
    let max_x = matrix[0].len() as isize;

    let mut roll_count = 0;

    loop {
        let mut run_roll_cunt = 0;

        for y in 0..max_y {
            for x in 0..max_x {
                if matrix[y as usize][x as usize] != '@' {
                    continue;
                }

                let mut adjacent_rolls = 0;

                // count adjacent
                for dy in [-1, 0, 1] {
                    for dx in [-1, 0, 1] {
                        if dy == 0 && dx == 0 {
                            continue;
                        }

                        let test_y = y + dy;
                        let test_x = x + dx;

                        if test_y < 0 || test_y >= max_y || test_x < 0 || test_x >= max_x {
                            continue;
                        }

                        if matrix[test_y as usize][test_x as usize] == '@' {
                            adjacent_rolls += 1;
                        }
                    }
                }

                if adjacent_rolls < 4 {
                    matrix[y as usize][x as usize] = '.';
                    roll_count += 1;
                    run_roll_cunt += 1;
                }
            }
        }

        if run_roll_cunt == 0 {
            break;
        }
    }

    println!("{roll_count}");
}

fn main() {
    part1();
    part2();
}
