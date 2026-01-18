module Lists

open Functions

// ÑÏÈÑÊÈ ÒÀ ÂÈÙ² ÔÓÍÊÖ²¯

// Ñïèñîê ö³ëèõ ÷èñåë â³ä 1 äî 10
let numbers = [1..10]

// map: êâàäğàòè ÷èñåë
let squares = List.map square numbers

// filter: ïàğí³ ÷èñëà
let evens = List.filter (fun x -> x % 2 = 0) numbers

// fold: ñóìà åëåìåíò³â
let sumNumbers = List.fold (+) 0 numbers

// Ñóìà êâàäğàò³â ïàğíèõ ÷èñåë
let sumOfEvenSquares list =
    list
    |> List.filter (fun x -> x % 2 = 0)
    |> List.map square
    |> List.sum
