
// ФАКТОРІАЛ 

let rec factorial n =
    match n with
    | 0 | 1 -> 1
    | n when n > 1 -> n * factorial (n - 1)
    | _ -> failwith "Negative number"

let factorialTests = [0; 1; 5; 7; 10]
let factorialResults = factorialTests |> List.map factorial

// ФІБОНАЧЧІ 

//рекурсія
let rec fib n =
    match n with
    | 0 -> 0
    | 1 -> 1
    | _ -> fib (n - 1) + fib (n - 2)

// Побудова списку перших n чисел Фібоначчі
let fibList n =
    let rec helper a b n =
        match n with
        | 0 -> []
        | _ -> a :: helper b (a + b) (n - 1)
    helper 0 1 n

let first10Fib = fibList 10

//МАКСИМУМ У СПИСКУ

let rec maxRecursive list =
    match list with
    | [] -> None
    | [x] -> Some x
    | x :: xs ->
        match maxRecursive xs with
        | Some m -> Some (max x m)
        | None -> Some x

// Через fold
let maxFold list =
    match list with
    | [] -> None
    | x :: xs -> Some (List.fold max x xs)

let maxTests =
    [
        [5; 2; 9; -3; 7]
        [42]
        [-5; -10; -2; -100]
    ]

let maxResults =
    maxTests
    |> List.map (fun l -> (maxRecursive l, maxFold l))

//MAP FILTER FOLD

let numbers = [1..10]

let squares = numbers |> List.map (fun x -> x * x)
let evens = numbers |> List.filter (fun x -> x % 2 = 0)
let sumAll = numbers |> List.fold (+) 0

let sumEvenSquares list =
    list
    |> List.filter (fun x -> x % 2 = 0)
    |> List.map (fun x -> x * x)
    |> List.fold (+) 0

let listTests =
    [
        [1; 2; 3; 4; 5; 6]
        [0; -1; -2; 10]
        [5; 5; 5]
    ]

let listResults =
    listTests
    |> List.map (fun l ->
        ( l,
          l |> List.map (fun x -> x * x),
          l |> List.filter (fun x -> x % 2 = 0),
          l |> List.sum,
          sumEvenSquares l ))

//НОРМАЛІЗАЦІЯ ОЦІНОК

let grades = [60; 75; 90; 100; 45; 82; 73]
let threshold = 70

let normalize grades =
    let maxGrade = List.max grades
    grades |> List.map (fun g -> float g / float maxGrade * 100.0)

let filterByThreshold threshold grades =
    grades |> List.filter (fun g -> g >= threshold)

let average grades =
    (List.sum grades) / float (List.length grades)

let normalizedGrades = normalize grades
let filteredGrades = filterByThreshold (float threshold) normalizedGrades
let avgBefore = average normalizedGrades
let avgAfter = average filteredGrades

//прінти

printfn "ФАКТОРІАЛ:"
printfn "%A -> %A\n" factorialTests factorialResults

printfn "ФІБОНАЧЧІ (перші 10):"
printfn "%A\n" first10Fib

printfn "МАКСИМУМ У СПИСКУ:"
maxTests
|> List.iter (fun l ->
    printfn "Список: %A" l
    printfn "Рекурсія: %A" (maxRecursive l)
    printfn "Fold: %A\n" (maxFold l))

printfn "MAP / FILTER / FOLD:"
listTests
|> List.iter (fun l ->
    printfn "Список: %A" l
    printfn "Квадрати: %A" (l |> List.map (fun x -> x * x))
    printfn "Парні: %A" (l |> List.filter (fun x -> x % 2 = 0))
    printfn "Сума: %d" (List.sum l)
    printfn "Сума квадратів парних: %d\n" (sumEvenSquares l))

printfn "НОРМАЛІЗАЦІЯ ОЦІНОК:"
printfn "Початкові: %A" grades
printfn "Нормалізовані: %A" normalizedGrades
printfn "Після фільтрації (>= %d): %A" threshold filteredGrades
printfn "Середнє до фільтрації: %.2f" avgBefore
printfn "Середнє після фільтрації: %.2f" avgAfter
