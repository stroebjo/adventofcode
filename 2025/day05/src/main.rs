use std::fs;

fn read_input() -> String {
    let file_path = "input.txt";
    let contents = fs::read_to_string(file_path).expect("Should have been able to read the file");
    return contents.trim().to_string();
}

fn part1() {
    let input = read_input();
    let (fresh_ranges, ingredients) = input.split_once("\n\n").unwrap();
    let mut fresh_ingredients = 0u64;
    let mut fresh: Vec<(u64, u64)> = Vec::new();

    // parse ranges
    for range in fresh_ranges.lines() {
        let (start, end) = range.trim().split_once('-').unwrap();
        let u_start = start.parse::<u64>().unwrap();
        let u_end = end.parse::<u64>().unwrap();
        fresh.push((u_start, u_end));
    }

    // check ingredients
    for ingredient in ingredients.lines() {
        let u_ingredient = ingredient.parse::<u64>().unwrap();

        for &(start, end) in &fresh {
            if u_ingredient >= start && u_ingredient <= end {
                fresh_ingredients += 1;
                break;
            }
        }
    }
    println!("{fresh_ingredients}");
}

fn part2() {
    let input = read_input();
    let (fresh_ranges, _ingredients) = input.split_once("\n\n").unwrap();
    let mut fresh: Vec<(u64, u64)> = Vec::new();

    // parse ranges
    for range in fresh_ranges.lines() {
        let (start, end) = range.trim().split_once('-').unwrap();
        let u_start = start.parse::<u64>().unwrap();
        let u_end = end.parse::<u64>().unwrap();
        fresh.push((u_start, u_end));
    }

    // check ingredients
    // during harmonizing the ranges it can happing, that a overlap into two (or more)
    // known ranges is not detected, this is the reason we repeat this harmonization
    // loop until no change is happening.
    //
    // #1 run:
    // Known ranges:  |---r1---|  |---r2---|
    // new range:          |---r3---|
    // result:        |---r1+r3-----|
    //                            |---r2---|
    // #2 run:
    // Known ranges:  |---r1+r3-----|
    // new range:                 |---r2---|
    // result:        |---r1+r3+r2---------|
    loop {
        let mut normalized_fresh: Vec<(u64, u64)> = Vec::new();

        'new: for &(new_start, new_end) in &fresh {
            for (known_start, known_end) in &mut normalized_fresh {
                // new range is larger than known range
                // known:     |-------|
                // new:   |---------------|
                if new_start <= *known_start && *known_end <= new_end {
                    *known_start = new_start;
                    *known_end = new_end;
                    continue 'new;
                }

                // new range starts inside known range, but is longer
                // known:     |-------|
                // new:          |--------|
                if *known_start <= new_start && new_start <= *known_end && *known_end <= new_end {
                    *known_end = new_end;
                    continue 'new;
                }

                // new range starts before known range but ends inside known range
                // known:     |-------|
                // new:   |--------|
                if new_start <= *known_start && *known_start <= new_end && new_end <= *known_end {
                    let old_start = *known_start;
                    *known_start = new_start;
                    continue 'new;
                }

                // new range is inside known range
                // known:     |-------|
                // new:         |--|
                if *known_start <= new_start && new_end <= *known_end {
                    continue 'new;
                }
            }

            // no currently known overlapping range, add new
            normalized_fresh.push((new_start, new_end));
        }

        let current_ranges_len = fresh.len();
        let new_ranges_len = normalized_fresh.len();

        fresh = normalized_fresh.clone();

        // check if we could reduce the amounts of ranges, if not we are finished
        // and can continue
        if new_ranges_len == current_ranges_len {
            break;
        }
    }

    // sum ranges
    let mut fresh_ingredients = 0u64;
    for &(start, end) in &fresh {
        fresh_ingredients += end - start + 1;
    }

    println!("{fresh_ingredients}");
}

fn main() {
    part1();
    part2();
}
