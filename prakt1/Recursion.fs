module Recursion


// Рекурсивна функція факторіалу
let rec factorial n =
    if n = 0 then 1
    else n * factorial (n - 1)

// Рекурсивна функція суми списку
let rec sumList list =
    match list with
    | [] -> 0
    | x :: xs -> x + sumList xs
