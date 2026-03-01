% =================================================================
% ЕКСПЕРТНА СИСТЕМА ПІДБОРУ ІНТЕРНЕТ-ТАРИФІВ "NET-ADVISOR"
% =================================================================
% ПРЕДИКАТИ:
% client(Name, Category) - клієнт та його категорія (new, regular, vip).
% usage_profile(Name, GbPerMonth, DevicesCount, NeedsSpeed) - профіль споживання.
% tariff(Id, MaxGb, Speed, Price, Type) - характеристики тарифу.
% recommended_tariff(ClientName, TariffId) - основне правило рекомендації.
% =================================================================



% Клієнти
client(alex, new).
client(olena, regular).
client(dmytro, vip).
client(ivan, regular).
client(maria, new).
client(petro, regular).

% Профілі використання: Ім'я, ГБ/міс, К-сть пристроїв, Чи потрібна висока швидкість (yes/no)
usage_profile(alex, 20, 1, no).      % Легкий користувач
usage_profile(olena, 150, 3, no).    % Середній користувач
usage_profile(dmytro, 800, 10, yes). % Геймер/Робота
usage_profile(ivan, 50, 2, no).      % Соцмережі
usage_profile(maria, 400, 4, yes).   % Стрімінг/Сім'я
usage_profile(petro, 200, 2, yes).   % Дистанційне навчання

% Тарифи: ID, Обсяг ГБ (9999 - безлім), Швидкість Мбіт/с, Ціна, Тип
tariff(starter, 100, 50, 150, mobile).
tariff(home_standard, 500, 100, 250, cable).
tariff(ultra_speed, 9999, 1000, 500, fiber).
tariff(night_owl, 300, 100, 200, cable).
tariff(economy, 50, 20, 100, mobile).

% -----------------------------------------------------------------
% ЗАВДАННЯ 3. ПРАВИЛА ЕКСПЕРТНОЇ СИСТЕМИ
% -----------------------------------------------------------------

% 1. Базове правило: Тариф "Економ" для тих, хто споживає дуже мало
is_economy_user(Name) :-
    usage_profile(Name, Gb, Devices, no),
    Gb =< 50, Devices =< 2.

% 2. Правило для геймерів та важкого контенту (Потрібна швидкість + багато ГБ)
is_heavy_user(Name) :-
    usage_profile(Name, Gb, _, yes), Gb > 300.
is_heavy_user(Name) :-
    usage_profile(Name, _, Devices, _), Devices > 5.

% 3. Правило лояльності: VIP клієнтам пропонуємо тільки найкраще незалежно від трафіку
is_priority_client(Name) :-
    client(Name, vip).

% 4. Рекурсивне правило (Ланцюжок вигоди): 
% Якщо тариф дорогий, але клієнт regular/vip -> перевіряємо чи є він у списку "пріоритетних бонусів"
has_bonus_access(Name, TariffId) :-
    client(Name, Category),
    Category \= new,
    tariff(TariffId, _, _, Price, _),
    Price > 300.

% 5. ОСНОВНЕ ПРАВИЛО: Рекомендація тарифу (Комбіноване)
% Випадок А: Для важких користувачів або VIP - Ultra Speed
recommended_tariff(Name, ultra_speed) :-
    is_heavy_user(Name); is_priority_client(Name).

% Випадок Б: Для економних
recommended_tariff(Name, economy) :-
    is_economy_user(Name),
    \+ is_priority_client(Name).

% Випадок В: Середній сегмент
recommended_tariff(Name, home_standard) :-
    usage_profile(Name, Gb, _, _),
    Gb > 50, Gb =< 500,
    \+ is_heavy_user(Name).

% 6. Спеціальна пропозиція для нових клієнтів (Starter)
recommended_tariff(Name, starter) :-
    client(Name, new),
    usage_profile(Name, Gb, _, _),
    Gb < 100.

% -----------------------------------------------------------------
% ЗАВДАННЯ 4. ВИКОРИСТАННЯ findall/3
% -----------------------------------------------------------------

% Знайти всі імена клієнтів, яким підходить конкретний тариф
clients_for_tariff(TariffId, ClientList) :-
    findall(Name, recommended_tariff(Name, TariffId), ClientList).

% Знайти всі доступні тарифи для клієнта (якщо їх декілька)
all_possible_tariffs(Name, Tariffs) :-
    findall(T, recommended_tariff(Name, T), Tariffs).

% -----------------------------------------------------------------
% ЗАВДАННЯ 5. ІНТЕРФЕЙС СИСТЕМИ (ЗАПИТИ)
% -----------------------------------------------------------------

% Головний предикат для консультації
advise_me(Name) :-
    all_possible_tariffs(Name, List),
    (   List \= [] 
    ->  write('Для клієнта '), write(Name), write(' рекомендовано: '), write(List), nl
    ;   write('На жаль, підходящого тарифу не знайдено.'), nl
    ).