using Arcesoft.Chess.ArtificialIntelligence;
using Arcesoft.Chess.ArtificialIntelligence.Implementation;
using Arcesoft.Chess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Arcesoft.Chess.Tests
{
    [Binding]
    internal sealed class ArtificialIntelligenceSteps: Steps
    {
        [Given(@"I have an artificial intelligence")]
        public void GivenIHaveAnArtificialIntelligence()
        {
            ArtificialIntelligence = Container.GetInstance<MiniMaxArtificialIntelligence>();
        }

        [When(@"I have the AI calculate the best move to a depth of '(.*)'")]
        public void WhenIHaveTheAICalculateTheBestMoveToADepthOf(int depth)
        {
            MiniMaxGraph = new MiniMaxGraph();
            Invoke(() => BestMove = ArtificialIntelligence.TryFindBestMove(Game, depth, MiniMaxGraph));
        }

        [Then(@"I expect the best move found to be")]
        public void ThenIExpectTheBestMovesFoundToContain(Table table)
        {
            table.CompareToInstance(BestMove);
        }

        #region Helpers
        private IMove BestMove
        {
            get
            {
                return GetScenarioContextItemOrDefault<IMove>(nameof(BestMove));
            }
            set
            {
                CurrentContext.Set(value, nameof(BestMove));
            }
        }

        private MiniMaxArtificialIntelligence ArtificialIntelligence
        {
            get
            {
                return GetScenarioContextItemOrDefault<MiniMaxArtificialIntelligence>();
            }
            set
            {
                CurrentContext.Set(value);
            }
        }
        private MiniMaxGraph MiniMaxGraph
        {
            get
            {
                return GetScenarioContextItemOrDefault<MiniMaxGraph>();
            }
            set
            {
                CurrentContext.Set(value);
            }
        }
        #endregion
    }
}
