use std::fs;

fn read_input() -> String {
    let file_path = "input.txt";
    let contents = fs::read_to_string(file_path).expect("Should have been able to read the file");
    return contents;
}

fn part1() {
    let contents = read_input();

    let mut dial = 50;
    let mut dial0counter = 0;

    for line in contents.lines() {
        if line.trim().is_empty() {
            continue;
        }

        let letter = line.chars().next().unwrap();
        let number_str = &line[1..];
        let number: i32 = number_str.parse().unwrap();

        if letter == 'R' {
            dial += number
        } else {
            dial -= number
        }

        if dial < 0 {
            dial = 100 + dial;
        }

        dial = dial % 100;

        if dial == 0 {
            dial0counter += 1;
        }
    }

    println!("{dial0counter}");
}

fn part2() {
    let contents = read_input();

    let mut dial = 50;
    let mut dial0counter = 0;

    for line in contents.lines() {
        if line.trim().is_empty() {
            continue;
        }

        let letter = line.chars().next().unwrap();
        let number_str = &line[1..];
        let mut number: i32 = number_str.parse().unwrap();

        // count and remove total revolutions before
        let mut moved_along0: i32 = number / 100;
        number = number % 100;

        let dial_old = dial;

        if letter == 'R' {
            dial += number
        } else {
            dial -= number
        }

        if dial < 0 {
            dial = 100 + dial;
        }

        dial = dial % 100;

        // because we removed total revolutions already only partial revolutions are
        // left and we can check if they moved over 0
        if letter == 'L' && dial_old != 0 && dial_old <= dial {
            // check if rotated Left and moved over 0
            moved_along0 += 1;
        } else if letter == 'R' && dial_old != 0 && dial_old >= dial {
            // check if rotated Right and moved over 0
            moved_along0 += 1;
        } else if dial == 0 {
            dial0counter += 1;
        }

        dial0counter += moved_along0;
    }

    println!("{dial0counter}");
}

fn main() {
    part1();
    part2();
}
