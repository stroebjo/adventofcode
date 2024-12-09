use std::fs;


fn part1() {
    let file_path = "input.txt";

    let input1 = fs::read_to_string(file_path)
        .expect("Should have been able to read the file");
    let input = input1.trim();

    let mut size = 0;

    // get total disk size
    for ch in input.chars() {
        let num = ch as u8 - b'0';
        size += num as i32;
    }
   // println!("{}", size);

    let mut data: Vec<Option<u32>> = vec![None; size as usize];

    // fill in files
    let mut file = true;
    let mut pos: usize = 0;
    let mut file_id = 0;
    for ch in input.chars() {
        let num = ch as u8 - b'0';
        if file {
            for i in 0..num {
                data[pos] = Some(file_id);
                pos += 1;
            }
            file_id += 1;
        } else {
            pos += num as usize;
        }
        file = !file;
    }

    // fill in empty spaces from the end
    loop {
        // get first not none value from the right <--
        if let Some(last_some_index) = data.iter().rposition(|v| v.is_some()) {
            // get first none value from the left -->
            if let Some(first_none_index) = data.iter().position(|v| v.is_none()) {

                // if none index is AFTER the value index, there are no more empty slots
                if last_some_index < first_none_index {
                    break;
                }

                // insert the value in the none slot in the front
                if let Some(value) = data[last_some_index].take() { 
                    data[first_none_index] = Some(value);
                }
            }
        }
    }

    let mut checksum = 0;
    let mut position: u64 = 0;
    loop {

        if let Some(file_id) = data[position as usize] {
            checksum += position * file_id as u64;
            position += 1;
        } else {
            break;
        }
    }

    println!("{checksum}");
}

fn main() {
    part1();
}
