module LibrarySystem

//Моделювання предметної області ---
type Genre = 
    | Fiction
    | NonFiction
    | Science
    | History
    | Programming
    | Other

type Status = 
    | Available
    | Issued
    | Archived

type Book = {
    Id: int
    Title: string
    Author: string
    Year: int
    Genre: Genre
    Pages: int
    Status: Status
}

//Тестовий набір даних ---
let libraryCatalog = [
    { Id = 1; Title = "Кобзар"; Author = "Тарас Шевченко"; Year = 1840; Genre = Fiction; Pages = 320; Status = Available }
    { Id = 2; Title = "Мистецтво програмування"; Author = "Дональд Кнут"; Year = 1968; Genre = Programming; Pages = 650; Status = Issued }
    { Id = 3; Title = "Чистий код"; Author = "Robert C. Martin"; Year = 2008; Genre = Programming; Pages = 450; Status = Available }
    { Id = 4; Title = "1984"; Author = "George Orwell"; Year = 1949; Genre = Fiction; Pages = 328; Status = Available }
    { Id = 5; Title = "Sapiens: Людина розумна"; Author = "Yuval Noah Harari"; Year = 2011; Genre = History; Pages = 512; Status = Issued }
    { Id = 6; Title = "Домашнє кондитерство"; Author = "Умовний автор"; Year = 2015; Genre = NonFiction; Pages = 220; Status = Available }
    { Id = 7; Title = "Історія України"; Author = "Умовний автор"; Year = 2003; Genre = History; Pages = 300; Status = Archived }
    { Id = 8; Title = "Алгоритми. Побудова та аналіз"; Author = "Cormen та ін."; Year = 1990; Genre = Programming; Pages = 1312; Status = Available }
]

//Фільтрація й пошук---
let getAvailableBooks (books: Book list) =
    books |> List.filter (fun b -> b.Status = Available)

let findBooksByGenre (genre: Genre) (books: Book list) =
    books |> List.filter (fun b -> b.Genre = genre)

let findBooksByAuthor (authorName: string) (books: Book list) =
    books |> List.filter (fun b -> b.Author.Equals(authorName, System.StringComparison.OrdinalIgnoreCase))

//Трансформації та агрегація ---
let getAllTitles (books: Book list) =
    books |> List.map (fun b -> b.Title)

let getTotalPagesByGenre (genre: Genre) (books: Book list) =
    books 
    |> List.filter (fun b -> b.Genre = genre)
    |> List.map (fun b -> b.Pages)
    |> List.sum

let getAveragePages (books: Book list) =
    match books with
    | [] -> None
    | _ -> 
        let total = books |> List.sumBy (fun b -> float b.Pages)
        Some (total / float books.Length)

// Точка входу та демонстрація ---
[<EntryPoint>]

let main argv =
    printfn "=== ДЕМОНСТРАЦІЯ РОБОТИ БІБЛІОТЕКИ ===\n"
    System.Console.OutputEncoding <- System.Text.Encoding.UTF8
    // 1. Доступні книги
    let available = getAvailableBooks libraryCatalog
    printfn "Доступні книги (%d): %A" available.Length (getAllTitles available)

    // 2. Пошук за жанром (Програмування)
    let progBooks = findBooksByGenre Programming libraryCatalog
    printfn "\nКниги з програмування: %A" (getAllTitles progBooks)

    // 3. Пошук за автором
    let orwellBooks = findBooksByAuthor "George Orwell" libraryCatalog
    printfn "\nКниги George Orwell: %A" (getAllTitles orwellBooks)

    // 4. Агрегація сторінок
    let progPages = getTotalPagesByGenre Programming libraryCatalog
    printfn "\nЗагальна кількість сторінок у розділі 'Програмування': %d" progPages

    // 5. Середня кількість сторінок
    match getAveragePages libraryCatalog with
    | Some avg -> printfn "\nСередня кількість сторінок у каталозі: %.2f" avg
    | None -> printfn "\nКаталог порожній."

    0