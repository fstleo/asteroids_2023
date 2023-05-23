using System;
using Asteroids.GameMechanics.Player.PlayerLife;
using Asteroids.GameMechanics.Player.PlayerLife.UI;
using NSubstitute;
using NUnit.Framework;

namespace Tests.GameMechanics.PlayerTests
{
    
    public class LivesWidgetPresenterTest
    {
        private LivesWidgetPresenter _livesWidgetPresenter;
        private ILivesCountWidget _livesCountWidget;
        private IPlayerLifeService _livesCountService;
        private IDisposable _livesCountSubscription;
        
        [SetUp]
        public void Setup()
        {
            _livesCountSubscription = Substitute.For<IDisposable>();
            _livesCountWidget = Substitute.For<ILivesCountWidget>();
            _livesCountService = Substitute.For<IPlayerLifeService>();
            _livesCountService.LivesCount.Returns(Substitute.For<IObservable<int>>());
            _livesCountService.LivesCount.Subscribe(Arg.Any<IObserver<int>>()).Returns(_livesCountSubscription);
            _livesWidgetPresenter = new LivesWidgetPresenter(_livesCountWidget, _livesCountService);
            
        }

        [Test]
        public void Widget_subscribed()
        {
            _livesCountService.LivesCount.Received().Subscribe(Arg.Any<IObserver<int>>());
        }


        [Test]
        public void On_dispose_subscription_is_disposed()
        {
            _livesWidgetPresenter.Dispose();
            
            _livesCountSubscription.Received().Dispose();
        }
        
    }
}
