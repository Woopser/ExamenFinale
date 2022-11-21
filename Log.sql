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
    ville_dep VARCHAR(20),
    ville_arr VARCHAR(20),
    voiture VARCHAR(10),
    FOREIGN KEY (id_compte) references Compte(id_compte)
);

CREATE TABLE Trajet(
    id_trajet int PRIMARY KEY AUTO_INCREMENT,
    id_chauffeur int,
    place_disp int,
    ville_dep VARCHAR(20),
    ville_arr VARCHAR(20),
    heureDep VARCHAR(5),
    heureArr VARCHAR(5),
    FOREIGN KEY (id_chauffeur) references Chauffeur(id_chauffeur)
);

CREATE TABLE Client(
    id_client int PRIMARY KEY AUTO_INCREMENT,
    id_compte int,
    id_trajet int,
    prenom VARCHAR(20),
    nom VARCHAR(20),
    adresse varchar(50),
    no_telephone VARCHAR(14),
    ville_dep VARCHAR(20),
    ville_arr VARCHAR(20),
    FOREIGN KEY (id_compte) references Compte(id_compte),
    FOREIGN KEY (id_trajet) references  Trajet(id_trajet)
);

CREATE TABLE Arret(
    id_arret int PRIMARY KEY AUTO_INCREMENT,
    id_trajet int,
    ville VARCHAR(20),
    heureArr VARCHAR(5),
    FOREIGN KEY (id_trajet) references Trajet(id_trajet)
);

CREATE TABLE Facture(
    id_facture int PRIMARY KEY AUTO_INCREMENT,
    id_trajet int,
    montant double,
    FOREIGN KEY (id_trajet) references Trajet(id_trajet)
);

CREATE TABLE Journee(
    id_journee int PRIMARY KEY AUTO_INCREMENT,
    id_facture int,
    total double,
    profit double,
    part_conducteur double,
    FOREIGN KEY (id_facture) references Facture(id_facture)
);

