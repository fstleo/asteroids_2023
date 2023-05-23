using Asteroids.GameMechanics.Player.PlayerLife.UI;
using Asteroids.GameMechanics.Scores.UI;
using Asteroids.Infrastructure.VContainerExtensions;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids.Game.GameMenu
{

    public class GameScreenInstaller : LifetimeScope
    {
        [SerializeField] 
        private LivesCountWidgetView _lifeWidget;
        
        [SerializeField] 
        private ScoresWidgetView _scoresWidget;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab(_lifeWidget, Lifetime.Scoped).AsImplementedInterfaces();
            builder.RegisterNonLazy<LivesWidgetPresenter>();

            builder.RegisterComponentInNewPrefab(_scoresWidget, Lifetime.Scoped).AsImplementedInterfaces();
            builder.RegisterNonLazy<ScorePresenter>();
        }
    }

}
