using Asteroids.Infrastructure.UI;

namespace Asteroids.GameMechanics.Scores.UI
{
    public class ScorePresenter : Presenter
    {
        public ScorePresenter(IScoreService scoreService, IScoresWidget scoresWidget)
        {
            ToDispose(scoreService.Score.Subscribe(scoresWidget.SetScore));
            ToDispose(scoreService.TopScore.Subscribe(scoresWidget.SetTopScore));
        }

    }
}
