using Asteroids.GameMechanics.Scores.UI;
using Asteroids.Infrastructure.VContainerExtensions;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids.Game.GameMenu
{
    public class GameOverScreenInstaller : LifetimeScope
    {
        
        [SerializeField] 
        private ScoresWidgetView _scoresWidget;
        
        [SerializeField] 
        private GameMenuWidgetView _menuWidgetView;

        [SerializeField] private Canvas _text;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab(_scoresWidget, Lifetime.Scoped).AsImplementedInterfaces();
            builder.RegisterNonLazy<ScorePresenter>();

            builder.RegisterComponentInNewPrefab(_menuWidgetView, Lifetime.Scoped).AsImplementedInterfaces();
            builder.RegisterNonLazy<GameMenuPresenter>();

            builder.RegisterComponentInNewPrefab(_text, Lifetime.Scoped).AsImplementedInterfaces();
        }
    }
}