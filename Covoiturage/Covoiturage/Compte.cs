using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covoiturage
{
    internal class Compte
    {
        public int id_compte;
        public string type_compte;
        public string nom_utilisateur;
        public string password;

        public int Id_compte { get => id_compte; set => id_compte = value; }
        public string Type_compte { get => type_compte; set => type_compte = value; }
        public string Nom_utilisateur { get => nom_utilisateur; set => nom_utilisateur = value;}
        public string Password { get => password; set => password = value; }
    }
}
