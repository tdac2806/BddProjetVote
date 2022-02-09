namespace SpecFlowProjetVote.Specs.StepDefinitions
{
    [Binding]
    public sealed class VoteStepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef
        private readonly ScenarioContext _scenarioContext;
        private Vote _vote;
        private string _result = "";

        public VoteStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _vote = new Vote();
        }

        [Given("le nom du candidat est '(.*)'")]
        public void GivenLeNomDuCandidatEst(string nom)
        {
            _vote.tours[_vote.tourcourent].AjoutCandidat(nom);
        }

        [Given(@"les candidats sont")]
        public void GivenLesCandidatsSont(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                _vote.tours[_vote.tourcourent].AjoutCandidat(row["Nom"]);
            }
        }

        [Given("(.*) vote pour '(.*)'")]
        public void GivenVotePour(int votesNumber, string nom)
        {
            _result = _vote.tours[_vote.tourcourent].AjoutDesVotes(votesNumber, nom);
        }

        [Given("(.*) vote blanc")]
        public void GivenVoteBlanc(int votesNumber)
        {
            _result = _vote.tours[_vote.tourcourent].AjoutDesVotes(votesNumber);
        }

        [Given("deuxieme tour")]
        public void GivenDeuxiemeTour()
        {
            _result = _vote.nextRound().ToString();
        }

        [When(@"On compte les candidats")]
        public void WhenOnCompteLesCandidats()
        {
            _result = _vote.tours[_vote.tourcourent].candidats.Count.ToString();
        }

        [When("le vote est compter pour '(.*)'")]
        public void WhenLeVoteEstCompterPour(string nom)
        {
            if (String.IsNullOrWhiteSpace(_result))
                _result = _vote.tours[_vote.tourcourent].GetCandidat(nom).vote.ToString();
        }

        [When("les votes sont comptés")]
        public void WhenLesVotesSontComptes()
        {
            _result = _vote.tours[_vote.tourcourent].totalVotes.ToString();
        }
        [When("le pourcentage est calculer pour '(.*)'")]
        public void WhenLePourcentageEstCalculerPour(string nom)
        {
            if (String.IsNullOrWhiteSpace(_result))
                _result = _vote.tours[_vote.tourcourent].GetPourcentageDesVotes(nom).ToString();
        }

        [When("Fin des votes")]
        public void WhenFinDesVotes()
        {
            _result = _vote.tours[_vote.tourcourent].GetCandidatGagnant(_vote.tourcourent);
        }


        [Then("Il y a (.*) candidats")]
        public void ThenIlYAXCandidats(string result)
        {
            _result.Should().Be(result);

        }

        [Then("le candidat a (.*) vote")]
        public void ThenLeCandidatAXVote(string result)
        {
            _result.Should().Be(result);

        }

        [Then("Il y a (.*) votes")]
        public void ThenIlYAXVotes(string result)
        {
            _result.Should().Be(result);
        }

        [Then("le gagnant est '(.*)'")]
        public void ThenLeGagnantEst(string result)
        {
            _result.Should().Be(result);
        }

        [Then("le pourcentage est (.*)")]
        public void ThenLePourcentageEst(string result)
        {
            _result.Should().Be(result);
        }

        [Then("Il n'y a pas de gagnant")]
        public void ThenIlNyAPasDeGagnant()
        {
            _result.Should().Be("Pas de gagnant au deuxieme tour");
        }

    }
}