using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace ProjetVote
{
    public class Vote
    {
        public List<Round> rounds { get; set; }
        public int currentRound { get; set; }
        public Boolean decide { get; set; }

        public Vote()
        {
            rounds = new List<Round>();
            rounds.Add(new Round());
            currentRound = 1;
            decide = false;
        }

        public string nextRound()
        {
            string res = "";
            if (decide == false && currentRound + 1 > 2)
            {
                res = "There can only be a maximum of two rounds of voting";
            }
            else if (decide == true && currentRound + 1 > 3)
            {
                res = "There can only be a maximum of two rounds of voting";
            }
            else
            {
                rounds.Add(new Round());
                currentRound++;
            }
            return res;
        }
    }

    public class Candidat
    {
        public string name { get; set; }
        public int vote { get; set; }

        public Candidat(string name, int vote)
        {
            this.name = name;
            this.vote = vote;
        }

        public Candidat(string name)
        {
            this.name = name;
            this.vote = 0;
        }
    }

    public class Round
    {
        public const int PercentToWin = 50;

        public List<Candidat> candidates;
        public int totalVotes { get; set; }
        public int totalBlankVotes { get; set; }
        public bool closed { get; set; }

        public Round()
        {
            candidates = new List<Candidat>();
            totalVotes = 0;
            totalBlankVotes = 0;
            closed = false;
        }

        public bool CheckCandidateExists(string name)
        {
            bool res = false;
            foreach (Candidat c in candidates)
            {
                if (c.name == name)
                {
                    res = true;
                    break;
                }
            }
            return res;
        }

        public Candidat GetCandidate(string name)
        {
            foreach (Candidat c in candidates)
            {
                if (c.name == name)
                    return c;
            }
            return null;
        }

        public int GetCandidateIndex(string name)
        {
            int res = 0;
            foreach (Candidat c in candidates)
            {
                if (c.name == name)
                {
                    break;
                }
                res += 1;
            }
            return res;
        }

        public void AddCandidate(string name)
        {
            if (!CheckCandidateExists(name) && !closed)
                candidates.Add(new Candidat(name));
        }

        public void AddCandidates(List<string> names)
        {
            if (!closed)
            {
                foreach (string name in names)
                {
                    AddCandidate(name);
                }
            }
        }

        public string AddVotesToCandidate(string name, int value)
        {
            if (CheckCandidateExists(name) && !closed)
            {
                candidates[GetCandidateIndex(name)].vote += value;
                totalVotes += value;
                return null;
            }
            else
            {
                return "Cannot add votes to this candidate (unknow candidate)";
            }
        }

        public string AddVotes(int value)
        {
            totalBlankVotes += value;
            return null;
        }

        public string GetWinners(int roundNumber, Boolean decide)
        {
            closed = true;
            if (totalVotes == 0)
            {
                return "There is no votes";
            }

            if (roundNumber == 1)
            {
                foreach (Candidat c in candidates)
                {
                    if (HasVotesMajority(c))
                    {
                        return c.name;
                    }
                }

                Candidat w1 = GetCandidateWithMaxVotes(candidates);

                List<Candidat> listWithoutFirstWinner = candidates;
                listWithoutFirstWinner.Remove(w1);

                Candidat w2 = GetCandidateWithMaxVotes(listWithoutFirstWinner);

                return w1.name + " and " + w2.name;
            }
            else if (roundNumber == 2 && decide == true)
            {
                return GetCandidateWithMaxVotes(candidates).name;
            }
            else
            {
                if (candidates[0].vote > candidates[1].vote)
                {
                    return candidates[0].name;
                }
                else if (candidates[0].vote < candidates[1].vote)
                {
                    return candidates[1].name;
                }
                else
                {
                    return "No winners in 2nd round";
                }
            }
        }

        public bool HasVotesMajority(Candidat c)
        {
            if (GetPercentOfCandidate(c) > PercentToWin)
            {
                return true;
            }
            return false;
        }

        public Candidat GetCandidateWithMaxVotes(List<Candidat> list)
        {
            Candidat winner = new Candidat("");
            foreach (Candidat c in list)
            {
                if (c.vote >= winner.vote)
                    winner = c;
            }
            return winner;
        }

        public float GetPercentOfCandidate(Candidat c)
        {
            float res = (float)c.vote / totalVotes * 100;
            return res;
        }

        public string createSecondRound(Vote voting)
        {
            string res = voting.nextRound();
            if (voting.decide == true && res == "")
            {
                Round round1 = voting.rounds[0];
                Round roundDecide = voting.rounds[1];
                Round round2 = voting.rounds[2];

                Candidat w1 = round1.GetCandidateWithMaxVotes(round1.candidates);
                Candidat w2 = roundDecide.GetCandidateWithMaxVotes(roundDecide.candidates);

                w1.vote = 0;
                w2.vote = 0;
                round2.candidates.Add(w1);
                round2.candidates.Add(w2);
                return "";
            }
            else if (res == "")
            {
                Round round1 = voting.rounds[0];
                Candidat w1 = round1.GetCandidateWithMaxVotes(candidates);

                List<Candidat> listWithoutFirstWinner = candidates;
                listWithoutFirstWinner.Remove(w1);

                Candidat w2 = round1.GetCandidateWithMaxVotes(listWithoutFirstWinner);

                Round round2 = voting.rounds[1];
                w1.vote = 0;
                w2.vote = 0;
                round2.candidates.Add(w1);
                round2.candidates.Add(w2);

                return "";
            }
            else
            {
                return res;
            }
        }

        public string createDecideRound(Vote voting)
        {
            voting.decide = true;
            string test = voting.nextRound();
            if (test == "")
            {
                Round round1 = voting.rounds[0];
                Candidat w1 = round1.GetCandidateWithMaxVotes(candidates);

                List<Candidat> listWithoutFirstWinner = candidates;
                listWithoutFirstWinner.Remove(w1);

                Candidat w2 = round1.GetCandidateWithMaxVotes(listWithoutFirstWinner);
                listWithoutFirstWinner.Remove(w2);

                Candidat w3 = round1.GetCandidateWithMaxVotes(listWithoutFirstWinner);

                Round roundDecide = voting.rounds[1];
                w2.vote = 0;
                w3.vote = 0;
                roundDecide.candidates.Add(w2);
                roundDecide.candidates.Add(w3);
                candidates.Add(w1);

                return "";
            }
            else
            {
                return test;
            }
        }

        public string AllVote()
        {
            string allVote = (totalVotes + totalBlankVotes).ToString();
            return allVote;
        }
    }
}

