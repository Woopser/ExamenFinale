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
        int heureDep;
        int heureArr;
        DateTime journee;
        int estFini;

        public int Id_trajet { get => id_trajet; set => id_trajet = value; }
        public int Id_chauffeur { get => id_chauffeur; set => id_chauffeur = value; }
        public int PlaceDisp { get => placeDisp; set => placeDisp = value; }
        public string VilleDep { get => villeDep; set => villeDep = value; }
        public string VilleArr { get => villeArr; set => villeArr = value; }
        public string Arret { get=> arret; set=> arret = value;}
        public int HeureDep { get => heureDep; set => heureDep = value; }
        public int HeureArr { get => heureArr; set => heureArr = value; }
        public int EstFini { get => estFini; set => estFini = value; }
        public DateTime Journee { get => journee; set => journee = value; }

    }
}
