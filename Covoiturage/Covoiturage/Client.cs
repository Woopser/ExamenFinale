using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covoiturage
{
    internal class Client
    {
        int id;
        int id_compte;
        string prenom;
        string nom;
        string adresse;
        string numero;
        string villeDep;
        string villeArr;

        public int Id { get => id; set => id = value; }
        public int Id_compte { get => id_compte; set => id_compte = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public string Numero { get => numero; set => numero = value; }
        public string VilleDep { get => villeDep; set => villeDep = value; }
        public string VilleArr { get => villeArr; set => villeArr = value; }
    }
}
