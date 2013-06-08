-- SET SCHEMA 'buk';

DROP VIEW rynek;
DROP VIEW kuponag;

DROP TABLE uzytkownik;
DROP TABLE wydarzenie;
DROP TABLE wynik;
DROP TABLE kupon;

CREATE TABLE uzytkownik
(
	koduz INTEGER PRIMARY KEY,
	konto INTEGER NOT NULL,
	imie VARCHAR NOT NULL
);
CREATE TABLE wydarzenie
(
	kodwyd INTEGER PRIMARY KEY,
	nazwawyd VARCHAR NOT NULL,
	ryzyko INTEGER NOT NULL
);
CREATE TABLE wynik
(
	kodwyd INTEGER NOT NULL,
	kodwyn INTEGER NOT NULL,
	nazwawyn VARCHAR
);
CREATE TABLE kupon
(
	koduz INTEGER NOT NULL,
	kodwyd INTEGER NOT NULL,
	kodwyn INTEGER NOT NULL,
	stawka INTEGER NOT NULL
);

-- Sumowanie stawek dla pojedynczego gracza, na dany wynik
CREATE VIEW kuponag AS 
	SELECT 
		koduz, kodwyd, kodwyn,
		nazwawyd, nazwawyn,
		sum(stawka) AS stawka
	FROM kupon NATURAL JOIN wynik NATURAL JOIN wydarzenie
	GROUP BY koduz, kodwyd, kodwyn, nazwawyd, nazwawyn
	ORDER BY koduz, kodwyd, kodwyn;

-- Sumowanie stawek na dane wyniki
CREATE VIEW rynek AS
	SELECT
		kodwyd, kodwyn,
		nazwawyd, nazwawyn,
		sum(stawka) AS stawka
	FROM kupon RIGHT OUTER JOIN wynik USING (kodwyd, kodwyn) NATURAL JOIN wydarzenie 
	GROUP BY kodwyd, kodwyn, nazwawyd, nazwawyn
	ORDER BY kodwyd, kodwyn;

INSERT INTO uzytkownik VALUES (0, 0, 'SYSTEM');

-- Test

INSERT INTO uzytkownik VALUES (1, 123, 'wojtek');
INSERT INTO uzytkownik VALUES (2, 0, 'klient');

INSERT INTO wydarzenie VALUES (0, 'meczyk', 100);
INSERT INTO wydarzenie VALUES (1, 'wybory', 1000);

INSERT INTO wynik VALUES (0, 0, 'barca');
INSERT INTO wynik VALUES (0, 1, 'remis');
INSERT INTO wynik VALUES (0, 2, 'bayern');

INSERT INTO wynik VALUES (1, 0, 'tusk');
INSERT INTO wynik VALUES (1, 1, 'kaczynski');

INSERT INTO kupon VALUES (1, 0, 1, 39);
INSERT INTO kupon VALUES (1, 0, 1, 3);
INSERT INTO kupon VALUES (2, 0, 1, 5);
INSERT INTO kupon VALUES (2, 0, 0, 10);
INSERT INTO kupon VALUES (2, 1, 1, 10042010);

SELECT * FROM rynek;


