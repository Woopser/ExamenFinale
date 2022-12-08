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
    journee date,
    estFini bool,
    FOREIGN KEY (id_chauffeur) references Chauffeur(id_chauffeur)
);

CREATE TABLE Client(
    id_client int PRIMARY KEY AUTO_INCREMENT,
    id_compte int,
    prenom VARCHAR(20),
    nom VARCHAR(20),
    adresse varchar(50),
    email varchar(50),
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
        UPDATE session Set total = total + NEW.montant;
        UPDATE Session SET profit = total - (NEW.montant * 0.3);
    end //
    
    INSERT INTO compte(type_compte, nom_utilisateur, password) VALUES ('Admin', 'root', 'root');

INSERT INTO compte(type_compte, nom_utilisateur, password) VALUES ('Chauffeur', 'Thomas', 'Thomas');
INSERT INTO compte(type_compte, nom_utilisateur, password) VALUES ('Chauffeur', 'Alex', 'Alex');

INSERT INTO chauffeur(id_compte, prenom, nom, adresse, no_telephone, email, voiture) VALUES (5, 'Thomas', 'Desruisseaux', '123 rue street', '819-459-6528', 'thom.des@hotmail.com', 'VUS');
INSERT INTO chauffeur(id_compte, prenom, nom, adresse, no_telephone, email, voiture) VALUES (6, 'Alex', 'Carle', '456 rue street', '819-184-6926', 'alex.car@hotmail.com', 'Berline');

DELIMITER //
    CREATE TRIGGER trigger_place_dispo
        AFTER INSERT ON arret
        FOR EACH ROW
        BEGIN
            UPDATE trajet SET place_disp = place_disp + 1 WHERE NEW.id_trajet LIKE trajet.id_trajet;
        END
// DELIMITER ;

DELIMITER  //
    CREATE TRIGGER trigger_session
        AFTER INSERT ON facture
        FOR EACH ROW
        BEGIN
           UPDATE session SET total = total + new.montant;
           UPDATE session SET profit = profit + 0.25 * new.montant;
           UPDATE session SET part_conducteur = part_conducteur + 0.25 * new.montant;
        END
// DELIMITER ;

INSERT INTO session (total, profit, part_conducteur) VALUES (0, 0, 0);
INSERT INTO facture (id_trajet, montant) VALUES (1, 200) ;
INSERT INTO facture (id_trajet, montant) VALUES (1, 400) ;

CREATE VIEW session_view AS
    SELECT * FROM session;

CREATE VIEW facture_view AS
    SELECT * FROM facture;

CREATE VIEW clients_view AS
    SELECT * FROM client;

CREATE VIEW chauffeurs_view AS
    SELECT * FROM chauffeur;
    
DELIMITER //
CREATE PROCEDURE ajout_client(IN id_com INT, IN prenom1 VARCHAR(20), IN nom1 VARCHAR(20), IN adresse1 VARCHAR(50), IN notel VARCHAR(14), IN villeDep VARCHAR(20), IN villeArr VARCHAR(20), IN email1 VARCHAR(20))
BEGIN
    INSERT INTO Client ( id_compte, prenom, nom,email, adresse, no_telephone, ville_dep, ville_arr) VALUES (id_com,prenom1,nom1,email1,adresse1,notel,villeDep,villeArr);
end //

DELIMITER //
CREATE PROCEDURE ajout_chauffeur(IN id_com INT, IN prenom1 VARCHAR(20), IN nom1 VARCHAR(20), IN adresse1 VARCHAR(50), IN notel VARCHAR(14), IN email1 VARCHAR(20), IN voit VARCHAR(20))
BEGIN
    INSERT INTO Chauffeur ( id_compte, prenom, nom, adresse, no_telephone, email, voiture) VALUES (id_com,prenom1,nom1,adresse1,notel, email1,voit);
end //

DELIMITER //
CREATE PROCEDURE ajout_arret(IN idtraj INT, IN idcli INT, IN vil VARCHAR(20), IN heure INT)
BEGIN
    INSERT INTO Arret ( id_trajet, id_client, ville, heureArr) VALUES (idtraj,idcli,vil,heure);
end //

DELIMITER //
CREATE PROCEDURE ajout_trajet(IN idchauf INT, IN place INT, IN villeD VARCHAR(20),IN villeA VARCHAR(20), IN heureD INT, IN heureA INT, IN heur date, IN bully bool)
BEGIN
    INSERT INTO Trajet (id_chauffeur, place_disp, ville_dep, ville_arr, heureDep, heureArr,journee,estFini) VALUES (idchauf,place,villeD,villeA,heureD,heureA,heur,bully);
end //


/*Fonction stockés
  1 Retourne le nombre de compte d'un type donner --FAIT
  2 Retourne le nombre de trajet qui passe par une ville donner --FAIT
  3 Retourne --FAIT
  4 Retourne compter les factures pour un date donner A FAIRE
 */

DELIMITER //
CREATE FUNCTION count_account(type VARCHAR(20)) RETURNS INT
BEGIN
    DECLARE nb INT;
    SELECT count(*) INTO nb FROM Compte WHERE type_compte LIKE type;
    RETURN nb;
end //

DELIMITER //
CREATE FUNCTION villeDep_traj(ville VARCHAR(20)) RETURNS INT
BEGIN
    DECLARE nb INT;
    SELECT count(*) INTO nb FROM Trajet WHERE ville_arr LIKE ville;
    RETURN nb;
end //

DELIMITER //
CREATE FUNCTION count_conduc(id int) returns INT
BEGIN
    DECLARE nb INT;
    SELECT count(*) INTO nb FROM Trajet WHERE id_chauffeur LIKE id;
    return nb;
end //

DELIMITER //
CREATE FUNCTION voiture_chauffeur (type int) RETURNS VARCHAR(10)
BEGIN
    DECLARE voiture VARCHAR(10);
    SELECT voiture INTO voiture FROM chauffeur WHERE id_chauffeur like type;
    RETURN voiture;
END //

/*Test de choses plus insert dans trajet */
SELECT * FRom compte WHERE nom_utilisateur LIKE 'Alex' AND password LIKE 'Thomas';

insert into trajet (id_chauffeur, place_disp, ville_dep, ville_arr, heureDep, heureArr, journee, estFini) VALUES (2, 3, 'Maskinonge', 'Louiseville', 15, 16, 20221205 , false);

SELECT * FROM trajet WHERE journee > 20221204 AND journee < 20221206;
