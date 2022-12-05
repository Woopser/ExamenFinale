using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covoiturage
{
    internal class Client
    {
        int id_client;
        int id_compte;
        string prenom;
        string nom;
        string adresse;
        string email;
        string numero;
        string villeDep;
        string villeArr;

        public int Id_client { get => id_client; set => id_client = value; }
        public int Id_compte { get => id_compte; set => id_compte = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Email { get => email; set => email = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public string Numero { get => numero; set => numero = value; }
        public string VilleDep { get => villeDep; set => villeDep = value; }
        public string VilleArr { get => villeArr; set => villeArr = value; }
    }
}
