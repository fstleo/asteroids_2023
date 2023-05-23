using System;
using Asteroids.GameMechanics;
using Asteroids.GameMechanics.AsteroidField;
using Asteroids.GameMechanics.Components.Armor;
using Asteroids.GameMechanics.Components.Moving;
using Asteroids.GameMechanics.Entities;
using Asteroids.Infrastructure;
using Asteroids.Infrastructure.Update;
using GameMechanics.Components.WrapPosition;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Tests.GameMechanics.EnemiesSpawn
{
    public class DefaultBuilderTest
    {

        private IAsteroidField _asteroidField;
        private ITickSource _tickSource;
        private IProvider<GameObject> _viewFactory;
        private GameEntity _defaultEntity;

        [SetUp]
        public void Setup()
        {
            _asteroidField = Substitute.For<IAsteroidField>();
            _tickSource = Substitute.For<ITickSource>();
            _viewFactory = Substitute.For<IProvider<GameObject>>();
            _defaultEntity = new DefaultEntityBuilder(_asteroidField, _tickSource, _viewFactory).Create("Id").Create();
        }

        [Test]
        public void Spawns_effect_on_death()
        {
            _defaultEntity.Die();

            _viewFactory.Received().GetAsync("Id_Death");
        }
        
        [Test]
        public void Has_armor()
        {
            Assert.NotNull(_defaultEntity.GetComponent<IArmor>());
        }

        [Test]
        public void Has_engine()
        {
            Assert.NotNull(_defaultEntity.GetComponent<IEngine>());
        }
        
        [Test]
        public void Kept_in_field_bounds()
        {
            Assert.NotNull(_defaultEntity.GetComponent<WrapPosition>());
        }
        
        [Test]
        public void View_created()
        {
            _viewFactory.Received().GetAsync("Id");
        }
 
        [Test]
        public void Entity_subscribed_to_update()
        {
            _tickSource.Received().AddListener(_defaultEntity);
        }
    }
    
    public class ReactivePropertyTest
    {
        private ReactiveProperty<int> _reactiveProperty;
        
        [SetUp]
        public void Setup()
        {
            _reactiveProperty = new ReactiveProperty<int>();
        }

        [Test]
        public void Setting_value_changes_it()
        {
            _reactiveProperty.Value = 5;
            
            Assert.AreEqual(5, _reactiveProperty.Value);
        }
        
        [Test]
        public void Setting_value_calls_observers()
        {
            var observer = Substitute.For<IObserver<int>>();
            _reactiveProperty.Subscribe(observer);
            
            _reactiveProperty.Value = 5;

            observer.Received().OnNext(5);
        }
        
        [Test]
        public void Setting_value_doesnt_call_observers_if_it_equals()
        {
            var observer = Substitute.For<IObserver<int>>();
            
            _reactiveProperty.Value = 5;
            
            _reactiveProperty.Subscribe(observer);
            
            _reactiveProperty.Value = 5;

            observer.Received(1).OnNext(5);
        }
        
        [Test]
        public void Subscription_dispose_stops_update()
        {
            var observer = Substitute.For<IObserver<int>>();
            
            var subscription = _reactiveProperty.Subscribe(observer);
            
            _reactiveProperty.Value = 5;
            
            subscription.Dispose();
            
            _reactiveProperty.Value = 2;

            observer.DidNotReceive().OnNext(2);
        }
    }
}
