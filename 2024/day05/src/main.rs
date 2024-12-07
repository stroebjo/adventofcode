use std::fs;



fn part1() {
    let file_path = "input.txt";
    let input = fs::read_to_string(file_path)
        .expect("Should have been able to read the file");

    let mut parts = input.splitn(2, "\n\n");

    let input_rules = parts.next().unwrap_or("");
    let input_updates = parts.next().unwrap_or("");
    
    let rules: Vec<(i32, i32)> = input_rules
        .lines()
        .filter_map(|line| {
            let mut parts = line.split('|');
            let a = parts.next()?.parse::<i32>().ok()?;
            let b = parts.next()?.parse::<i32>().ok()?;
            Some((a, b))
        })
        .collect();

    let updates: Vec<Vec<i32>> = input_updates
        .lines()
        .map(|line| { 
            line.split(',')
                .filter_map(|num| num.parse::<i32>().ok())
                .collect::<Vec<i32>>()
        })
        .collect();

    let mut middle_sum = 0;

    for update in updates {
        let mut is_valid = true;

        for rule in &rules {
            let index1 = update.iter().position(|&x| x == rule.0);
            let index2 = update.iter().position(|&x| x == rule.1);
            
            if let (Some(i1), Some(i2)) = (index1, index2) {
                if i1 > i2 {
                    is_valid = false;
                    break;
                }
            }
        }

        if is_valid {
            let mid_index = update.len() / 2;
            middle_sum += update[mid_index];
        }
    }

    println!("{middle_sum}");
}


fn part2() {
    let file_path = "input.txt";
    let input = fs::read_to_string(file_path)
        .expect("Should have been able to read the file");

    let mut parts = input.splitn(2, "\n\n");

    let input_rules = parts.next().unwrap_or("");
    let input_updates = parts.next().unwrap_or("");
    
    let rules: Vec<(i32, i32)> = input_rules
        .lines()
        .filter_map(|line| {
            let mut parts = line.split('|');
            let a = parts.next()?.parse::<i32>().ok()?;
            let b = parts.next()?.parse::<i32>().ok()?;
            Some((a, b))
        })
        .collect();

    let mut updates: Vec<Vec<i32>> = input_updates
        .lines()
        .map(|line| { 
            line.split(',')
                .filter_map(|num| num.parse::<i32>().ok())
                .collect::<Vec<i32>>()
        })
        .collect();

    let mut middle_sum = 0;

    for mut update in updates {
        let mut is_valid = true;
        let mut was_changed = true;

        while was_changed {
            was_changed = false;
            for rule in &rules {
                let index1 = update.iter().position(|&x| x == rule.0);
                let index2 = update.iter().position(|&x| x == rule.1);
                
                if let (Some(i1), Some(i2)) = (index1, index2) {
                    if i1 > i2 {
                        was_changed = true;
                        is_valid = false;

                        let element = update.remove(i2);
                        update.insert(i1, element);

                        //println!("{update:?}");

                        break;
                    }
                }
            }
        }


        if !is_valid {
            let mid_index = update.len() / 2;
            middle_sum += update[mid_index];
        }
    }

    println!("{middle_sum}");
}

fn main() {
    part1();
    part2();
}
