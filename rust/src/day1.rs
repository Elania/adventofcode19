use std::fs::File;
use std::io::Read;

fn main() { 
}

pub fn part_one() -> i32{
  let filename = "../data/1.txt";
  let masses = parse_data(filename);
  let mut result = 0;
  for mass in masses {
    result += get_fuel_for_mass(mass);
  }
  result
}

pub fn part_two() -> i32{
  let filename = "../data/1.txt";
  let masses = parse_data(filename);
  let mut result = 0;
  for mass in masses {
    let mut current_fuel = get_fuel_for_mass(mass);
    let mut total_fuel = 0;
    while current_fuel > 0 {
      total_fuel += current_fuel;
      current_fuel = get_fuel_for_mass(current_fuel);
    }
    result += total_fuel;
  }
  result
}

fn get_fuel_for_mass(mass: i32) -> i32 {
  return (mass / 3) - 2;
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
  let lines: Vec<i32> = file_contents.split("\n")
      .map(|s: &str| s.to_string())
      .filter(|s| !s.is_empty())
      .map(|s| s.parse().unwrap())
      .collect();
  lines
}