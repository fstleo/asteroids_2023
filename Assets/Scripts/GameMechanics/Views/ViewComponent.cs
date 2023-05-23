using System;
using System.Linq;
using System.Threading.Tasks;
using Asteroids.GameMechanics.Entities;
using Asteroids.Infrastructure;
using UnityEngine;

namespace Asteroids.GameMechanics.Views
{
    public class ViewComponent : IEntityComponent, ISpawnListener, IDeathListener
    {
        private readonly GameEntity _entity;
        private readonly IProvider<GameObject> _gameObjectProvider;
        private readonly string _prefabId;

        private GameObject _currentView;
        private IDisposable _subscriptions;
        
        public ViewComponent(GameEntity entity, IProvider<GameObject> gameObjectProvider, string prefabId)
        {
            _entity = entity;
            _gameObjectProvider = gameObjectProvider;
            _prefabId = prefabId;
        }
        
        public void Spawn()
        {
            _ = SpawnAsync();
        }

        private async Task SpawnAsync()
        {
            var view = await _gameObjectProvider.GetAsync(_prefabId);
            if (view == null)
            {
                return;
            }
            view.SetActive(true);
            _currentView = view;
            _subscriptions = new CompositeDisposable(_currentView.GetComponentsInChildren<IView<GameEntity>>().Select(entityView => entityView.Init(_entity)));
        }

        public void Die()
        {
            if (_currentView == null)
            {
                return;
            }
            _currentView.SetActive(false);
            _subscriptions.Dispose();
            _gameObjectProvider.Release(_currentView.gameObject);
            _currentView = null;
        }
    }
}