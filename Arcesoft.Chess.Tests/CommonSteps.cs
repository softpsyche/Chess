using SimpleInjector;
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
    internal class CommonSteps : Steps
    {
        [Given(@"I expect an exception to be thrown")]
        public void GivenIExpectAnExceptionToBeThrown()
        {
            ExpectingException = true;
        }

        [Given(@"I have a container")]
        public void GivenIHaveAContainer()
        {
            var container = new Container();
            container.Options.AllowOverridingRegistrations = true;

            new Chess.DependencyInjection.Binder().BindDependencies(container);
            new ArtificialIntelligence.DependencyInjection.Binder().BindDependencies(container);

            //this locks the container so no tx for us...
            //container.Verify();

            Container = container;
        }

        [Given(@"I have a game factory")]
        public void GivenIHaveATictactoeFactory()
        {
            GameFactory = Container.GetInstance<IGameFactory>();
        }

        [Given(@"I start a new game")]
        [When(@"I start a new game")]
        public void GivenIStartANewGame()
        {
            Invoke(() => Game = GameFactory.NewGame());
        }

        [Then(@"I expect the following Exception to be thrown")]
        public void ThenIExpectTheFollowingExceptionToBeThrown(Table table)
        {
            table.CompareToInstance(Exception);
        }

    }
}
