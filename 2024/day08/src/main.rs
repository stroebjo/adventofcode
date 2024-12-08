use std::fs;
use std::collections::HashMap;
use std::collections::HashSet;

fn read_input_matrix() -> Vec<Vec<char>> {
    let file_path = "input.txt";

    let contents = fs::read_to_string(file_path)
        .expect("Should have been able to read the file");

    let char_vector: Vec<Vec<char>> = contents
        .lines()
        .map(|line| line.chars().collect())
        .collect();

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

fn vector_between_points(p1: (i32, i32), p2: (i32, i32)) -> (i32, i32) {
    let (x1, y1) = p1;
    let (x2, y2) = p2;
    (x2 - x1, y2 - y1)
}

fn add_vector_to_point(point: (i32, i32), vector: (i32, i32)) -> (i32, i32) {
    let (px, py) = point;
    let (vx, vy) = vector;
    (px + vx, py + vy) // Add the vector to the point
}

fn negate_vector(vector: (i32, i32)) -> (i32, i32) {
    let (x, y) = vector;
    (-x, -y)
}

fn is_within_bounds(point: (i32, i32), matrix_rows: usize, matrix_cols: usize) -> bool {
    let (x, y) = point;
    x >= 0 && x < matrix_rows as i32 && y >= 0 && y < matrix_cols as i32
}


fn part1() {    
    let mut matrix = read_input_matrix();

    let max_y = matrix.len();
    let max_x = matrix[0].len();

    let mut antennas: HashMap<char, Vec<(i32, i32)>> = HashMap::new();
    let mut unique_antinodes: HashSet<(i32, i32)> = HashSet::new();


    // find pairs
    for row in 0..max_y {
        for col in 0..max_x {

            let antenna = matrix[row][col];

            if antenna == '.' {
                continue;
            }

            antennas.entry(antenna).or_insert(Vec::new()).push((col as i32, row as i32));
        }
    }

    // pairs of antennas
    for (&_key, points) in &antennas {
        for (i, &point1) in points.iter().enumerate() {
            for (j, &point2) in points.iter().enumerate() {
                if i != j { // Avoid pairing a point with itself + exclude frequencies with only one antenna
                    
                    let distance = vector_between_points(point1, point2);

                    let antinode1 = add_vector_to_point(point1, negate_vector(distance));
                    let antinode2 = add_vector_to_point(point2, distance);


                    if is_within_bounds(antinode1, max_y, max_x) {
                        unique_antinodes.insert(antinode1);
                        matrix[antinode1.1 as usize][antinode1.0 as usize] = '#';
                    }
                    
                    if is_within_bounds(antinode2, max_y, max_x) {
                        unique_antinodes.insert(antinode2);
                        matrix[antinode2.1 as usize][antinode2.0 as usize] = '#';
                    }
                }
            }
        }
    }

    println!("{}", unique_antinodes.len());
}

fn part2() {
    let mut matrix = read_input_matrix();

    let max_y = matrix.len();
    let max_x = matrix[0].len();

    let mut antennas: HashMap<char, Vec<(i32, i32)>> = HashMap::new();
    let mut unique_antinodes: HashSet<(i32, i32)> = HashSet::new();
    let mut antenna_count = 0;

    // find pairs
    for row in 0..max_y {
        for col in 0..max_x {

            let antenna = matrix[row][col];

            if antenna == '.' {
                continue;
            }
            antenna_count += 1;
            antennas.entry(antenna).or_insert(Vec::new()).push((col as i32, row as i32));
        }
    }

    // pairs of antennas
    for (&_key, points) in &antennas {
        for (i, &point1) in points.iter().enumerate() {
            for (j, &point2) in points.iter().enumerate() {
                if i != j { // Avoid pairing a point with itself + exclude frequencies with only one antenna
                    
                    let distance = vector_between_points(point1, point2);

                    let mut antinode1 = add_vector_to_point(point1, negate_vector(distance));
                    let mut antinode2 = add_vector_to_point(point2, distance);

                    while is_within_bounds(antinode1, max_y, max_x) {
                        // is this check actually required for part1 as well? 
                        if matrix[antinode1.1 as usize][antinode1.0 as usize] == '.' {
                            unique_antinodes.insert(antinode1);
                            matrix[antinode1.1 as usize][antinode1.0 as usize] = '#';
                        }

                        antinode1 = add_vector_to_point(antinode1, negate_vector(distance));
                    }
                    
                    while is_within_bounds(antinode2, max_y, max_x) {
                        // is this check actually required for part1 as well? 
                        if matrix[antinode2.1 as usize][antinode2.0 as usize] == '.' {
                            unique_antinodes.insert(antinode2);
                            matrix[antinode2.1 as usize][antinode2.0 as usize] = '#';
                        }

                        antinode2 = add_vector_to_point(antinode2, distance);
                    }
                }
            }
        }
    }

    println!("{}", unique_antinodes.len() + antenna_count);
}


fn main() {
    part1();
    part2();
}
