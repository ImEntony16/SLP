open System
open System.Text

open Functions
open Recursion
open Lists

[<EntryPoint>]
let main argv =
    // UTF-8 для української мови
    Console.OutputEncoding <- Encoding.UTF8

    // 1. Hello, world
    printfn "Привіт! Це моя перша програма на функціональній мові."

    printfn "\n=== Чисті функції ==="
    printfn "square 5 = %d" (square 5)
    printfn "absValue -10 = %d" (absValue -10)
    printfn "maxOfTwo 7 3 = %d" (maxOfTwo 7 3)

    printfn "\n=== Рекурсія ==="
    printfn "factorial 5 = %d" (factorial 5)
    printfn "sum [1;2;3] = %d" (sumList [1;2;3])
    printfn "sum [10;-5;7] = %d" (sumList [10;-5;7])

    printfn "\n=== Списки та вищі функції ==="
    printfn "Початковий список: %A" numbers
    printfn "Квадрати: %A" squares
    printfn "Парні числа: %A" evens
    printfn "Сума елементів: %d" sumNumbers

    printfn "\n=== Сума квадратів парних чисел ==="
    printfn "sumOfEvenSquares [1;2;3;4] = %d"
        (sumOfEvenSquares [1;2;3;4])

    0
