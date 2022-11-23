/*Query pour exam*/

CREATE TABLE Compte(
    id_compte int PRIMARY KEY auto_increment,
    type_compte VARCHAR(20),
    nom_utilisateur VARCHAR(20),
    password VARCHAR(20)
);

CREATE TABLE Chauffeur(
    id_chauffeur int PRIMARY KEY auto_increment,
    id_compte int,
    prenom VARCHAR(20),
    nom VARCHAR(20),
    adresse VARCHAR(50),
    no_telephone varchar(14),
    email VARCHAR(20),
    voiture VARCHAR(10),
    FOREIGN KEY (id_compte) references Compte(id_compte)
);

CREATE TABLE Trajet(
    id_trajet int PRIMARY KEY AUTO_INCREMENT,
    id_chauffeur int,
    place_disp int,
    ville_dep VARCHAR(20),
    ville_arr VARCHAR(20),
    heureDep int,
    heureArr int,
    FOREIGN KEY (id_chauffeur) references Chauffeur(id_chauffeur)
);

CREATE TABLE Client(
    id_client int PRIMARY KEY AUTO_INCREMENT,
    id_compte int,
    prenom VARCHAR(20),
    nom VARCHAR(20),
    adresse varchar(50),
    no_telephone VARCHAR(14),
    ville_dep VARCHAR(20),
    ville_arr VARCHAR(20),
    FOREIGN KEY (id_compte) references Compte(id_compte)
);

CREATE TABLE Arret(
    id_arret int PRIMARY KEY AUTO_INCREMENT,
    id_trajet int,
    id_client int,
    ville VARCHAR(20),
    heureArr int,
    FOREIGN KEY (id_trajet) references Trajet(id_trajet),
    FOREIGN KEY (id_client) references Client(id_client)
);

CREATE TABLE Facture(
    id_facture int PRIMARY KEY AUTO_INCREMENT,
    id_trajet int,
    montant double,
    FOREIGN KEY (id_trajet) references Trajet(id_trajet)
);

CREATE TABLE Session(
    id_journee int PRIMARY KEY AUTO_INCREMENT,
    id_facture int,
    total double,
    profit double,
    part_conducteur double,
    FOREIGN KEY (id_facture) references Facture(id_facture)
);

INSERT INTO Compte ( type_compte, nom_utilisateur, password) VALUES ('Client','Jean','Jean');

INSERT INTO Compte ( type_compte, nom_utilisateur, password) VALUES ('Client', 'Paul', 'Paul' );

INSERT INTO Compte ( type_compte, nom_utilisateur, password) VALUES ('Client', 'Joe', 'Joe');

INSERT INTO Client ( id_compte, prenom, nom, adresse, no_telephone, ville_dep, ville_arr) VALUES (1, 'Jean', 'LaJeance' ,'123 Fausse Rue', '819-420-8008', 'Maskinonge', 'Louiseville');
INSERT INTO Client ( id_compte, prenom, nom, adresse, no_telephone, ville_dep, ville_arr) VALUES (2, 'Paul', 'Paulisson', '456 Fond de Rivieres', '819-999-8888','Trois-Rivieres', 'La Tuque');
INSERT INTO Client ( id_compte, prenom, nom, adresse, no_telephone, ville_dep, ville_arr) VALUES (3, 'Joe', 'Jonisson', '789 LaForet' , '819-123-4567', 'Maskinonge', 'Mekinac');


/*Triggers
  1 creation de facture 
  2 update la session 
  3 Update trajet pour place disponible quand ajout d'arret
  4 update Nom mais on verra si on a besoin de autre chose
  */
  DELIMITER //
CREATE TRIGGER facturation
    AFTER INSERT ON Trajet
    FOR EACH ROW
    BEGIN
        INSERT INTO Facture ( id_trajet, montant) VALUES (NEW.id_trajet, (SELECT place_disp FROM Trajet WHERE id_trajet = NEW.id_trajet) * 30);
    end //

DELIMITER //
CREATE TRIGGER Sesh
    AFTER INSERT ON Facture
    FOR EACH ROW
    BEGIN
        UPDATE Session SET part_conducteur = (NEW.montant * 0.3) + Session.part_conducteur;
        /*UPDATE session Set total = */
    end //
