using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covoiturage
{
    internal class Comptes
    {
        int id_compte;
        string type_compte;
        string nom_utilisateur;
        string password;


        public int Id_compte { get => id_compte; set => id_compte = value; }
        public string Type_compte { get => type_compte; set => type_compte = value; }
        public string Nom_utilisateur { get => nom_utilisateur; set => nom_utilisateur = value; }
        public string Password { get => password; set => password = value; }

        public override string ToString()
        {
            return nom_utilisateur + password;
        }
    }
}
