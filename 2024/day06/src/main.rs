use std::fs;
use std::collections::HashSet;


fn read_input_matrix() -> Vec<Vec<char>> {
    let file_path = "input.txt";

    let contents = fs::read_to_string(file_path)
        .expect("Should have been able to read the file");

    let char_vector: Vec<Vec<char>> = contents
        .lines() // Split the string by newlines
        .map(|line| line.chars().collect()) // Convert each line into Vec<char>
        .collect(); // Collect the rows into a Vec<Vec<char>>

    return char_vector;
}

fn find_start(matrix: Vec<Vec<char>>) -> (usize, usize) {
    let max_y = matrix.len();
    let max_x = matrix[0].len();

    for row in 0..max_y {
        for col in 0..max_x {
            if matrix[row][col] == '^' {
                return (col, row);
            }
        }
    }

    panic!("No start point found."); 
}

#[allow(dead_code)]
fn print_matrix<T: std::fmt::Display>(matrix: &[Vec<T>]) {
    for row in matrix {
        for item in row {
            print!("{}", item);
        }
        println!(); // Newline after each row
    }
}

const UP: (isize, isize) = (0, -1);
const DOWN: (isize, isize) = (0, 1);
const LEFT: (isize, isize) = (-1, 0);
const RIGHT: (isize, isize) = (1, 0);

fn part1() {
    let mut matrix = read_input_matrix();
    let max_y = matrix.len() as isize;
    let max_x = matrix[0].len() as isize;

    // find start
    let mut pos = find_start(matrix.clone());
    let mut field_count = 0;

    let directions = vec![UP, RIGHT, DOWN, LEFT];
    let mut current_dir = 0;


    loop {

        // we need to count unique fields we visited, so don't recount.
        if matrix[pos.1][pos.0] != 'X' {
            field_count += 1;
        }

        matrix[pos.1][pos.0] = 'X';
        


        let mut dir = directions[current_dir];
        let mut ipos = (pos.0 as isize + dir.0, pos.1 as isize + dir.1);

        // break the loop if we would leave the field
        if ipos.1 < 0 || ipos.1 >= max_y || ipos.0 < 0 || ipos.0 >= max_x {
            break;
        }

        let mut next_pos = (ipos.0 as usize, ipos.1 as usize);

        // if we would hit an obstacle, turn right 90deg.
        if matrix[next_pos.1][next_pos.0] == '#' {
            current_dir = (current_dir + 1) % directions.len();

            // we just expect the new direction cant be an obstacle again, RIGHT?
            dir = directions[current_dir];
            ipos = (pos.0 as isize + dir.0, pos.1 as isize + dir.1);
            next_pos = (ipos.0 as usize, ipos.1 as usize);
        }
        
        pos = next_pos;

    }

    println!("{field_count}");
}



fn part2() {
    let matrix = read_input_matrix();
    let max_y = matrix.len() as isize;
    let max_x = matrix[0].len() as isize;

    let umax_y = matrix.len();
    let umax_x = matrix[0].len();

    let start = find_start(matrix.clone());

    let mut loop_options = 0;

    for row in 0..umax_y {
        for col in 0..umax_x {
            if matrix[row][col] == '^' {
                continue;
            }

            if matrix[row][col] == '#' {
                continue;
            }

            let mut new_matrix = matrix.clone();
            new_matrix[row][col] = '#';

            // find start
            let mut pos = start;

            let directions = vec![UP, RIGHT, DOWN, LEFT];
            let mut current_dir = 0;

            // combination of obstacle + direction hit twice is a loop!
            let mut loop_detection: HashSet<((usize, usize), (isize, isize))> = HashSet::new();

            loop {
                let mut dir = directions[current_dir];
                let mut ipos = (pos.0 as isize + dir.0, pos.1 as isize + dir.1);

                // break the loop if we would leave the field
                if ipos.1 < 0 || ipos.1 >= max_y || ipos.0 < 0 || ipos.0 >= max_x {
                    break;
                }

                let mut next_pos = (ipos.0 as usize, ipos.1 as usize);

                // if we would hit an obstacle, turn right 90deg.
                if new_matrix[next_pos.1][next_pos.0] == '#' {

                    let new_combination = (next_pos, dir);
                    if loop_detection.contains(&new_combination) {
                        loop_options += 1;
                        break;
                    }
                    loop_detection.insert(new_combination);


                    current_dir = (current_dir + 1) % directions.len();

                    // we just expect the new direction cant be an obstacle again, RIGHT?
                    dir = directions[current_dir];
                    ipos = (pos.0 as isize + dir.0, pos.1 as isize + dir.1);
                    next_pos = (ipos.0 as usize, ipos.1 as usize);
                }
                
                pos = next_pos;

            }

        }
    }

    println!("{loop_options}");

}

fn main() {
    part1();
    part2();
}
