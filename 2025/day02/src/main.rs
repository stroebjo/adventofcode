use std::fs;

fn read_input() -> String {
    let file_path = "input.txt";
    let contents = fs::read_to_string(file_path).expect("Should have been able to read the file");
    return contents.trim().to_string();
}

// Works for test input, but not for real input.
// It misses the range 3-23. It loops from 3[3] to 9[9] and then aborts
// There is missing a way to continue from 9->10 to then find 11 and 22 in the range.
fn part1() {
    let contents = read_input();
    let mut invalid_sum = 0u64;

    for range in contents.split(",") {
        let (start, end) = range.trim().split_once('-').unwrap();
        let u_start = start.parse::<u64>().unwrap();
        let u_end = end.parse::<u64>().unwrap();

        let start_id_length = start.len();
        let stat_sequence_length = (start_id_length / 2).max(1);

        let end_id_length = end.len();
        let end_sequence_length = end_id_length / 2;

        let mut sequence: String = start.chars().take(stat_sequence_length).collect();
        let mut u_sequence = sequence.parse::<u64>().unwrap();

        loop {
            let full_sequence = sequence.repeat(2);
            let u_full_sequence = full_sequence.parse::<u64>().unwrap();

            if u_start <= u_full_sequence && u_full_sequence <= u_end {
                invalid_sum += u_full_sequence;
            }

            // increment sequence
            u_sequence += 1;
            sequence = u_sequence.to_string();

            // abort if the next sequence test would have mor digits
            // than the possible length
            if sequence.len() > end_sequence_length {
                break;
            }

            // abort if the last tested full sequence was larger than our end range
            // -> this is faulty, the current tested sequence can be larger than the end
            // but still more sequences can be possible, i.e.
            // range 3-23 -> first run would test 33 adn then abort, and we would test further,
            // but in this range 11 and 22 need to be found.
            // Removing it still doesn't fix the underlying issue for not continuing with 10.
            //if u_full_sequence > u_end {
            //    break;
            //}
        }
    }

    println!("{invalid_sum}");
}

fn part1brute() {
    let contents = read_input();
    let mut invalid_sum = 0u64;

    for range in contents.split(",") {
        let (start, end) = range.trim().split_once('-').unwrap();
        let u_start = start.parse::<u64>().unwrap();
        let u_end = end.parse::<u64>().unwrap();

        for i in u_start..=u_end {
            let sequence = i.to_string();
            let mut sequence_length = sequence.len();

            // only even length can have a pattern occurring twice
            if sequence_length % 2 == 1 {
                continue;
            }
            sequence_length /= 2;

            let sequence_pt1: String = sequence.chars().take(sequence_length).collect();
            let sequence_pt2: String = sequence
                .chars()
                .skip(sequence_length)
                .take(sequence_length)
                .collect();

            if sequence_pt1 == sequence_pt2 {
                invalid_sum += i;
            }
        }
    }

    println!("{invalid_sum}");
}

fn main() {
    part1();
    println!("Bruteforce:");
    part1brute();
}
