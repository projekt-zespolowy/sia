-- SET SCHEMA 'buk';

DROP VIEW IF EXISTS rynek;
DROP VIEW IF EXISTS kuponag;

DROP TABLE IF EXISTS uzytkownik;
DROP TABLE IF EXISTS wydarzenie;
DROP TABLE IF EXISTS wynik;
DROP TABLE IF EXISTS kupon;

CREATE TABLE uzytkownik
(
	koduz INTEGER NOT NULL,
	konto INTEGER NOT NULL,
	imie VARCHAR NOT NULL
);
CREATE TABLE wydarzenie
(
	kodwyd INTEGER NOT NULL,
	nazwawyd VARCHAR NOT NULL,
	ryzyko INTEGER NOT NULL
);
CREATE TABLE wynik
(
	kodwyd INTEGER NOT NULL,
	kodwyn INTEGER NOT NULL,
	nazwawyn VARCHAR,
	PRIMARY KEY (kodwyd, kodwyn)
);
CREATE TABLE kupon
(
	koduz INTEGER NOT NULL,
	kodwyd INTEGER NOT NULL,
	kodwyn INTEGER NOT NULL,
	stawka INTEGER NOT NULL
);


CREATE VIEW kuponag AS 
	SELECT 
		kupon.koduz, kupon.kodwyd, kupon.kodwyn,
		nazwawyd, nazwawyn,
		sum(stawka) AS stawka
	FROM kupon NATURAL JOIN wynik NATURAL JOIN wydarzenie
	GROUP BY kupon.koduz, kupon.kodwyd, kupon.kodwyn, nazwawyd, nazwawyn
	ORDER BY kupon.koduz, kupon.kodwyd, kupon.kodwyn;

CREATE VIEW rynek AS
	SELECT
		kupon.kodwyd, kupon.kodwyn,
		nazwawyd, nazwawyn,
		sum(stawka) AS stawka
	FROM wynik LEFT OUTER JOIN kupon ON (kupon.kodwyd = wynik.kodwyd AND kupon.kodwyn = wynik.kodwyn) NATURAL JOIN wydarzenie 
	GROUP BY kupon.kodwyd, kupon.kodwyn, nazwawyd, nazwawyn
	ORDER BY kupon.kodwyd, kupon.kodwyn;

INSERT INTO uzytkownik VALUES (0, 0, 'SYSTEM');
INSERT INTO wydarzenie VALUES (0, 'NULLEVENT', 1);

-- Test

--INSERT INTO uzytkownik VALUES (1, 0, 'wojtek');
--INSERT INTO uzytkownik VALUES (2, 0, 'klient');

--INSERT INTO wydarzenie VALUES (0, 'meczyk', 30000);
--INSERT INTO wydarzenie VALUES (1, 'wybory', 100000);

---- INSERT INTO wynik VALUES (0, 0, 'barca');
-- INSERT INTO wynik VALUES (0, 1, 'remis');
-- INSERT INTO wynik VALUES (0, 2, 'bayern');
-- INSERT INTO wynik VALUES (0, 3, 'sendzia kalosz!');

-- INSERT INTO wynik VALUES (1, 0, 'tusk');
-- INSERT INTO wynik VALUES (1, 1, 'kaczynski');

-- Systemowe zerowanie
-- INSERT INTO kupon VALUES (0, 0, 0, 0);
-- INSERT INTO kupon VALUES (0, 0, 1, 0);
-- INSERT INTO kupon VALUES (0, 0, 2, 0);
-- INSERT INTO kupon VALUES (0, 0, 3, 0);
-- INSERT INTO kupon VALUES (0, 1, 0, 0);
-- INSERT INTO kupon VALUES (0, 1, 1, 0);

-- INSERT INTO kupon VALUES (1, 0, 0, 5000);
-- INSERT INTO kupon VALUES (1, 0, 0, 2500);
-- INSERT INTO kupon VALUES (2, 0, 3, 1000);
-- INSERT INTO kupon VALUES (2, 1, 0, 300);
-- INSERT INTO kupon VALUES (2, 1, 1, 10042010);

SELECT * FROM rynek;


