module Functions

// ЧИСТІ ФУНКЦІЇ

// Повертає квадрат числа
let square x = x * x

// Повертає модуль числа
let absValue x =
    if x < 0 then -x else x

// Повертає більше з двох чисел
let maxOfTwo a b =
    if a > b then a else b
