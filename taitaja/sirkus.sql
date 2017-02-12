CREATE DATABASE Sirkus;

CREATE TABLE Esitys(
esitysID INT PRIMARY KEY NOT NULL,
teema VARCHAR(20) NOT NULL,
esityspaikka VARCHAR(30),
kaupunki VARCHAR(20),
pvm DATE NOT NULL,
paikat INT NOT NULL,
vapaatpaikat INT NOT NULL
);

CREATE TABLE Tilaaja(
tilaajaID INT PRIMARY KEY NOT NULL,
sposti VARCHAR(40) NOT NULL,
puhelin VARCHAR(30),
paikkojenlkm INT NOT NULL,
esitysID INT NOT NULL,
FOREIGN KEY (esitysID) REFERENCES Esitys(esitysID)
);

INSERT INTO Esitys VALUES (1,'Jooga','Kulttuuritalo', 'Helsinki', 2016-05-30, 1000, 250 ),
(2,'Kesäinen niitty', 'Eerikinkatu 1a8', 'Helsinki',2016-06-15, 20, 5),
(3,'Kaamosaika', 'Kulttuuritalo Valve', 'Oulu',2016-01-30, 500,0 );

INSERT INTO Tilaaja VALUES (1,'suski@gmail.com','050343424', 2, 1 ),
(2,'sakarinieminen@hotmail.com', '040586733',  2, 2);