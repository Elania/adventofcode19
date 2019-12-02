use std::fs::File;
use std::io::Read;

pub fn part_one() -> i32{
  let filename = "../data/2.txt";
  let mut data = parse_data(filename);
  data[1] = 12;
  data[2] = 2;
  let mut i = 0;
  while i < data.len() && data[i] != 99 {
    let write_idx = (data[i + 3]) as usize;
    if data[i] == 1 {
        data[write_idx] = data[(data[i + 1]) as usize] + data[(data[i + 2]) as usize];
    }                    
    else if data[i] == 2 {
        data[write_idx] = data[(data[i + 1]) as usize] * data[(data[i + 2]) as usize];
    }            
    i += 4;
  }
  data[0]
}

pub fn part_two() -> i32{
  let filename = "../data/2.txt";   
  let mut result = 0;
  for n in 0..100 {
    for v in 0..100 {
      let mut data = parse_data(filename);
      data[1] = n;
      data[2] = v;
      let mut i = 0;
      while i < data.len() && data[i] != 99 {
        let write_idx = (data[i + 3]) as usize;
        if data[i] == 1 {
            data[write_idx] = data[(data[i + 1]) as usize] + data[(data[i + 2]) as usize];
        }                    
        else if data[i] == 2 {
            data[write_idx] = data[(data[i + 1]) as usize] * data[(data[i + 2]) as usize];
        }            
        i += 4;
      }
      if data[0] == 19690720 {
        result = 100 * n + v;
        break;
      }        
    }
  }  
  result
}

fn parse_data(filename: &str) -> Vec<i32> {
  let mut file = match File::open(filename) {
      Ok(file) => file,
      Err(_) => panic!("no such file"),
  };
  let mut file_contents = String::new();
  file.read_to_string(&mut file_contents)
      .ok()
      .expect("failed to read!");
  let lines: Vec<i32> = file_contents
      .trim()
      .split(",")
      .map(|s: &str| s.to_string())
      .filter(|s| !s.is_empty())
      .map(|s| s.parse().unwrap())
      .collect();
  lines
}