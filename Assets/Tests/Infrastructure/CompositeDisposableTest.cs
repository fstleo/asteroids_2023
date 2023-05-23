using System;
using NSubstitute;
using NUnit.Framework;

namespace Tests.GameMechanics.Moving
{
    public class CompositeDisposableTest
    {
        private CompositeDisposable _compositeDisposable;
        
        
        [Test]
        public void All_disposables_are_disposed()
        {
            var firstDisposable = Substitute.For<IDisposable>();
            var secondDisposable = Substitute.For<IDisposable>();
            
            _compositeDisposable = new CompositeDisposable(new[] { firstDisposable, secondDisposable });
            _compositeDisposable.Dispose();
            
            firstDisposable.Received().Dispose();
            secondDisposable.Received().Dispose();
        }
        
        [Test]
        public void Disposables_are_disposed_only_once()
        {
            var firstDisposable = Substitute.For<IDisposable>();
            var secondDisposable = Substitute.For<IDisposable>();
            
            _compositeDisposable = new CompositeDisposable(new[] { firstDisposable, secondDisposable });
            _compositeDisposable.Dispose();
            _compositeDisposable.Dispose();
            
            firstDisposable.Received(1).Dispose();
            secondDisposable.Received(1).Dispose();
        }
    }
}
