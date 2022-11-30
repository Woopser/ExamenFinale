using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Covoiturage
{
    internal class Chauffeur
    {
        public int id_chauffeur;
        public int id_compte;
        public string prenom;
        public string nom;
        public string adresse;
        public string numero;
        public string email;
        public string voiture;

        public int Id_chauffeur { get => id_chauffeur; set=> id_chauffeur = value; }
        public int Id_compte { get => id_compte; set => id_compte = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public string Numero { get => numero; set => numero = value; }
        public string Email { get => email; set => email = value; }
        public string Voiture { get => voiture; set => voiture = value; }
    }
}
