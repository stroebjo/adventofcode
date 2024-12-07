use std::fs;

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

#[allow(dead_code)]
fn print_matrix<T: std::fmt::Display>(matrix: &[Vec<T>]) {
    for row in matrix {
        for item in row {
            print!("{}", item);
        }
        println!(); // Newline after each row
    }
}

fn is_xmas(x: usize, y: usize, dir: (isize, isize), matrix: &Vec<Vec<char>>) -> bool {

    let max_y = matrix.len();
    let max_x = matrix[0].len();

    let my_string = String::from("XMAS");
    let mut chars = my_string.chars();
    let mut pos = (x, y);

    while let Some(c) = chars.next() {
        // bound check is done at the beginning, if it's done at the end of the while-body
        // it would fire if the last char is at an edge. 
        // bound check, pos is signed so it can't be smaller than 0 and we don't need to check
        if pos.1 >= max_y || pos.0 >= max_x {
            return false;
        }

        if c != matrix[pos.1][pos.0] {
            return false;
        }

        let ipos = (pos.0 as isize + dir.0, pos.1 as isize + dir.1);
        pos = (ipos.0 as usize, ipos.1 as usize);
    }

    return true;
}

fn part1() {
    let matrix = read_input_matrix();

    let max_y = matrix.len();
    let max_x = matrix[0].len();

    // These are the relative changes in x (row) and y (col) for all 8 possible directions
    let directions = [(-1, 0), (1, 0), (0, -1), (0, 1), (-1, -1), (-1, 1), (1, -1), (1, 1)];

    let mut xmas_count = 0;
    for row in 0..max_y {
        for col in 0..max_x {
            if matrix[row][col] == 'X' {
                for (dx, dy) in directions {
                    if is_xmas(col, row, (dx, dy), &matrix) {
                        xmas_count += 1;
                    }
                }
            }
        }
    }

    println!("{xmas_count}");
}

fn part2() {
    let matrix = read_input_matrix();

    let max_y = matrix.len();
    let max_x = matrix[0].len();

    let mut xmas_count = 0;

    // the center A can never be on the outer index, so we loop from 1..-1
    for row in 1..(max_y-1) {
        for col in 1..(max_x-1) {

            if matrix[row][col] == 'A' {

                // since we exclude the outer most index, this can't be a out of bound
                let pos_ul = (col - 1, row - 1);
                let pos_ur = (col + 1, row - 1);
                let pos_ll = (col - 1, row + 1);
                let pos_lr = (col + 1, row + 1);

                // test for [MS]A[SM] on each diagonal
                if ((matrix[pos_ul.1][pos_ul.0] == 'M' && matrix[pos_lr.1][pos_lr.0] == 'S') || 
                  (matrix[pos_ul.1][pos_ul.0] == 'S' && matrix[pos_lr.1][pos_lr.0] == 'M')) && 
                  ((matrix[pos_ur.1][pos_ur.0] == 'M' && matrix[pos_ll.1][pos_ll.0] == 'S') || 
                  (matrix[pos_ur.1][pos_ur.0] == 'S' && matrix[pos_ll.1][pos_ll.0] == 'M')) {
                    xmas_count += 1;
                }
            }
        }
    }

    println!("{xmas_count}");
}

fn main() {
    part1();
    part2();
}
