using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace SpecFlowProjetVote
{
    public class Candidats
    {
        public string nom { get; set; }
        public int vote { get; set; }

        public Candidats(string nom, int vote = 0)
        {
            this.nom = nom;
            this.vote = vote;
        }
    }

    public class Vote
    {
        public List<Tour> tours { get; set; }
        public int tourcourent { get; set; }

        public Vote()
        {
            tours = new List<Tour>();
            tours.Add(new Tour());
            tourcourent = 0;
        }

        public bool nextRound()
        {
            bool result = false;
            if(tourcourent + 1 <= 1)
            {
                tours.Add(new Tour());
                tourcourent++;
                result = true;
            }
            return result;
        }
    }


    public class Tour
    {
        public List<Candidats> candidats;
        public int totalVotes { get; set; }
        public bool votefini { get; set; }

        public Tour()
        {
            candidats = new List<Candidats>();
            totalVotes = 0;
            votefini = false;
        }

        public Candidats GetCandidat(string nom)
        {
            for (int i = 0; i < candidats.Count; i++)
            {
                if (candidats[i].nom == nom)
                {
                    return candidats[i];
                }
            }
            return null;
        }

        public void AjoutCandidat(string nom)
        {
            if (null == GetCandidat(nom) && !votefini)
                candidats.Add(new Candidats(nom));
        }

        public int GetCandidatIndex(string nom)
        {
            int result = 0;
            for (int i = 0; i < candidats.Count; i++)
            {
                if (candidats[i].nom == nom)
                {
                    break;
                }
                result += 1;
            }
            return result;
        }

        public string AjoutDesVotes(int value, string nom = "")
        {
            string result = "";

            if(null != GetCandidat(nom))
            {
                if (!votefini)
                {
                    candidats[GetCandidatIndex(nom)].vote += value;
                    totalVotes += value;
                }
                else
                {
                    result = "Le vote est fini";
                }
            }
            else
            {
                totalVotes += value;
            }

            return result;
        }

        public float GetPourcentageDesVotes(string nom)
        {
            float result = (float)GetCandidat(nom).vote / totalVotes * 100;
            return result;
        }

        public string GetCandidatsGagnantPremierTour(List<Candidats> candidats)
        {
            Candidats candidat = new Candidats("");
            for (int i = 0; i < candidats.Count; i++)
            {
                if (GetPourcentageDesVotes(candidats[i].nom) > 50)
                {
                    candidat = candidats[i];
                }
            }
            return candidat.nom;
        }

        public string GetCandidatPourSecondTour(List<Candidats> candidats)
        {
            Candidats candidat1 = new Candidats("");
            Candidats candidat2 = new Candidats("");

            for (int i = 0; i < candidats.Count; i++)
            {
                if (candidats[i].vote >= candidat1.vote)
                    candidat1 = candidats[i];
            }

            candidats.Remove(candidat1);

            for (int i = 0; i < candidats.Count; i++)
            {
                if (candidats[i].vote >= candidat1.vote)
                    candidat2 = candidats[i];
            }
            return candidat1.nom + " et " + candidat2.nom; ;
        }

        public string GetCandidatGagnant(int roundNumber)
        {
            votefini = true;

            string gagnant = "";

            if (roundNumber == 0)
            {
                gagnant =  GetCandidatsGagnantPremierTour(candidats);

                if ("" == gagnant)
                {
                    gagnant = GetCandidatPourSecondTour(candidats);
                }
            }
            else
            {
                if (candidats[0].vote > candidats[1].vote)
                {
                    gagnant = candidats[0].nom;
                }
                else if (candidats[0].vote < candidats[1].vote)
                {
                    gagnant = candidats[1].nom;
                }
                else
                {
                    gagnant =  "Pas de gagnant au deuxieme tour";
                }
            }
            return gagnant;
        }


    }
}

