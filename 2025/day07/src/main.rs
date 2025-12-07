use std::collections::HashMap;
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

#[derive(Copy, Clone, Debug, Eq, Hash, PartialEq)]
struct Coord {
    x: usize,
    y: usize,
}

impl Coord {
    /// Check whether the coordinate is inside a matrix
    fn in_bounds(&self, matrix: &[Vec<char>]) -> bool {
        if self.y >= matrix.len() {
            return false;
        }
        if self.x >= matrix[self.y].len() {
            return false;
        }
        true
    }
}

#[allow(dead_code)]
fn print_grid(grid: &[Vec<char>]) {
    for row in grid {
        for &c in row {
            print!("{}", c);
        }
        println!(); // new line at the end of each row
    }
}

fn tachyon_beam(start: Coord, matrix: &mut Vec<Vec<char>>) -> u32 {
    let mut splits = 0u32;
    let mut next = Coord {
        x: start.x,
        y: start.y + 1,
    };

    matrix[start.y][start.x] = '|';

    loop {
        // leaving field
        if !next.in_bounds(matrix) {
            break;
        }

        // split
        let field = matrix[next.y][next.x];

        // laser beam was already here
        if field == '|' {
            break;
        }

        if field == '^' {
            splits += 1;

            // left
            let left = Coord {
                x: next.x - 1,
                y: next.y,
            };
            if left.in_bounds(matrix) {
                splits += tachyon_beam(left, matrix);
            }

            // right
            let right = Coord {
                x: next.x + 1,
                y: next.y,
            };
            if right.in_bounds(matrix) {
                splits += tachyon_beam(right, matrix);
            }

            break;
        }
        matrix[next.y][next.x] = '|';

        // continue down
        next.y += 1;
    }

    return splits;
}

fn part1() {
    let mut matrix = read_input_matrix();

    let start_y = 0;
    let start_x = matrix[0]
        .iter()
        .position(|&c| c == 'S')
        .expect("No Start found");
    let start = Coord {
        x: start_x,
        y: start_y,
    };

    let total_splits = tachyon_beam(start, &mut matrix);
    println!("{total_splits}");
}

fn tachyon_beam_quantum(
    start: Coord,
    matrix: &[Vec<char>],
    cache: &mut HashMap<Coord, u64>,
) -> u64 {
    let mut splits = 0u64;
    let mut next = Coord {
        x: start.x,
        y: start.y + 1,
    };

    if let Some(&result) = cache.get(&start) {
        return result;
    }

    loop {
        // leaving field
        if !next.in_bounds(matrix) {
            splits += 1;
            break;
        }

        // split
        let field = matrix[next.y][next.x];

        if field == '^' {
            // left
            let left = Coord {
                x: next.x - 1,
                y: next.y,
            };
            if left.in_bounds(matrix) {
                splits += tachyon_beam_quantum(left, matrix, cache);
            }

            // right
            let right = Coord {
                x: next.x + 1,
                y: next.y,
            };
            if right.in_bounds(matrix) {
                splits += tachyon_beam_quantum(right, matrix, cache);
            }

            break;
        }

        // continue down
        next.y += 1;
    }

    cache.insert(start, splits);
    return splits;
}

fn part2() {
    let matrix = read_input_matrix();
    let mut cache = HashMap::new();

    let start_y = 0;
    let start_x = matrix[0]
        .iter()
        .position(|&c| c == 'S')
        .expect("No Start found");
    let start = Coord {
        x: start_x,
        y: start_y,
    };

    let total_splits = tachyon_beam_quantum(start, &matrix, &mut cache);
    println!("{total_splits}");
}

fn main() {
    part1();
    part2();
}
