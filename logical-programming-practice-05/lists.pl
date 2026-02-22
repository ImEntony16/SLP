% 1. Довжина списку (my_length)
my_length([], 0). % Базовий випадок: порожній список має довжину 0
my_length([_|Tail], N) :- 
    my_length(Tail, N1), 
    N is N1 + 1.

% 2. Сума елементів списку (sum_list)
sum_list([], 0). % Базовий випадок: сума порожнього списку - 0
sum_list([Head|Tail], Sum) :- 
    sum_list(Tail, TailSum), 
    Sum is Head + TailSum.

% 3. Максимальний елемент (max_list)
% Предикат працює тільки для непорожніх списків.
max_list([X], X). % Базовий випадок: один елемент і є максимальним
max_list([Head|Tail], Max) :- 
    max_list(Tail, TailMax), 
    (Head > TailMax -> Max = Head ; Max = TailMax).

% 4. Квадрати чисел (map_square)
map_square([], []). % Базовий випадок
map_square([H|T], [ResH|ResT]) :- 
    ResH is H * H, 
    map_square(T, ResT).