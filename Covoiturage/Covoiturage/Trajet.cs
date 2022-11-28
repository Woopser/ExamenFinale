using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covoiturage
{
    internal class Trajet
    {
        int id;
        int id_chauffeur;
        int placeDisp;
        string villeDep;
        string villeArr;
        int heureDep;
        int heureArr;

        public int Id { get => id; set => id = value; }
        public int Id_chauffeur { get => id_chauffeur; set => id_chauffeur = value; }

        public int PlaceDisp { get => placeDisp; set => placeDisp = value; }
        public string VilleDep { get => villeDep; set => villeDep = value; }
        public string VilleArr { get => villeArr; set => villeArr = value; }
        public int HeureDep { get => heureDep; set => heureDep = value; }
        public int HeureArr { get => heureArr; set => heureArr = value; }



    }
}
