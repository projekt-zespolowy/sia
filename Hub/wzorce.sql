-- Wyszukuje i sumuje kupony graczy

SELECT 
	u.kod_uz, 
	sum(k.stawka), 
	e.nazwa as "wydarzenie", 
	w.nazwa as "wynik" 
	FROM 
	uzytkownik u 
	join kupon k on (k.kod_uz = u.kod_uz)
	join wynik w on (k.kod_wyn = w.kod_wyn AND k.kod_wyd = w.kod_wyd)
	join wydarzenie e on(k.kod_wyd = e.kod_wyd)
	GROUP BY 
	u.kod_uz, e.nazwa, e.kod_wyd, w.nazwa, w.kod_wyd, w.kod_wyn;

-- Sumuje wysokości stawek na dane wydarzenie
SELECT 
	sum(k.stawka), 
	e.nazwa as "wydarzenie", 
	w.nazwa as "wynik" 
	FROM 
	uzytkownik u 
	join kupon k on (k.kod_uz = u.kod_uz)
	join wynik w on (k.kod_wyn = w.kod_wyn AND k.kod_wyd = w.kod_wyd)
	join wydarzenie e on(k.kod_wyd = e.kod_wyd)
	GROUP BY 
	e.nazwa, e.kod_wyd, w.nazwa, w.kod_wyd, w.kod_wyn;

