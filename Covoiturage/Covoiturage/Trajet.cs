using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covoiturage
{
    internal class Trajet
    {
        int id_trajet;
        int id_chauffeur;
        int placeDisp;
        string villeDep;
        string villeArr;
        string arret;
        DateTime dateDepart;
        int estFini;

        public int Id_trajet { get => id_trajet; set => id_trajet = value; }
        public int Id_chauffeur { get => id_chauffeur; set => id_chauffeur = value; }
        public int PlaceDisp { get => placeDisp; set => placeDisp = value; }
        public string VilleDep { get => villeDep; set => villeDep = value; }
        public string VilleArr { get => villeArr; set => villeArr = value; }
        public string Arret { get=> arret; set=> arret = value;}
        public DateTime DateDepart { get => dateDepart; set => dateDepart = value; }
        public int EstFini { get => estFini; set => estFini = value; }
    }
}
