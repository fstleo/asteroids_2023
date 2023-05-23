using Asteroids.Infrastructure.UI;

namespace Asteroids.GameMechanics.Player.PlayerLife.UI
{
    public class LivesWidgetPresenter : Presenter 
    {
        public LivesWidgetPresenter(ILivesCountWidget livesCountWidget, IPlayerLifeService lifeService)
        {
            ToDispose(lifeService.LivesCount.Subscribe(livesCountWidget.SetLivesCount));
        }
    }

}
