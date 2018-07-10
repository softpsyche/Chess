using Arcesoft.Chess.Implementation;
using Arcesoft.Chess.Models;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Arcesoft.Chess.Tests
{
    internal abstract class Steps
    {
        protected ScenarioContext CurrentContext => ScenarioContext.Current;

        protected Boolean ExpectingException
        {
            get
            {
                return GetScenarioContextItemOrDefault<Boolean>(nameof(ExpectingException));
            }
            set
            {
                CurrentContext.Set(value, nameof(ExpectingException));
            }
        }

        protected Exception Exception
        {
            get
            {
                return GetScenarioContextItemOrDefault<Exception>();
            }
            set
            {
                CurrentContext.Set(value);
            }
        }

        protected Container Container
        {
            get
            {
                return GetScenarioContextItemOrDefault<Container>();
            }
            set
            {
                CurrentContext.Set(value);
            }
        }

        protected IGameFactory GameFactory
        {
            get
            {
                return GetScenarioContextItemOrDefault<IGameFactory>();
            }
            set
            {
                CurrentContext.Set(value);
            }
        }

        protected IMatchFactory MatchFactory
        {
            get
            {
                return GetScenarioContextItemOrDefault<IMatchFactory>();
            }
            set
            {
                CurrentContext.Set(value);
            }
        }

        protected Match Match
        {
            get
            {
                return GetScenarioContextItemOrDefault<Match>();
            }
            set
            {
                CurrentContext.Set(value);
            }
        }

        protected IGame Game
        {
            get
            {
                return GetScenarioContextItemOrDefault<IGame>();
            }
            set
            {
                CurrentContext.Set(value);
            }
        }

        protected List<IMove> Moves
        {
            get
            {
                return GetScenarioContextItemOrDefault<List<IMove>>();
            }
            set
            {
                CurrentContext.Set(value);
            }
        }

        private T GetScenarioContextItemOrDefault<T>(string key = null)
        {
            var keyName = key ?? typeof(T).FullName;

            if (CurrentContext.ContainsKey(keyName))
            {
                return CurrentContext.Get<T>(keyName);
            }
            else
            {
                return default(T);
            }
        }

        protected void Invoke(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                if (ExpectingException)
                {
                    Exception = ex;
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
