CREATE TABLE Member(
	OIB INT CONSTRAINT member_OIB_pk PRIMARY KEY,
	FirstName VARCHAR(20) NOT NULL,
	LastName VARCHAR(20) NOT NULL
	
); 

CREATE TABLE Membership(
    Id INT CONSTRAINT membership_id_pk PRIMARY KEY,
    MemberOIB INT CONSTRAINT member_OIB_fk REFERENCES member(OIB),
    Cijena INT NOT NULL
);


INSERT INTO Member VALUES(121,'Jimmy', 'Jim');
INSERT INTO Member VALUES(122,'Annie', 'Ann');
INSERT INTO Member VALUES(123,'Timmie', 'Tim');
INSERT INTO Member VALUES(124,'Kimmy', 'Kim');


INSERT INTO Membership VALUES(1, 121, 250);
INSERT INTO Membership VALUES(2, 122, 200);
INSERT INTO Membership VALUES(3, 123, 250);
INSERT INTO Membership VALUES(4, 124, 150);

SELECT * FROM Member;
SELECT * FROM Membership;