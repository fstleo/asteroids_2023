using System;
using UnityEngine;

namespace Asteroids.GameMechanics.Player.PlayerLife.UI
{
    public class LivesCountWidgetView : MonoBehaviour, ILivesCountWidget, IDisposable
    {
        [SerializeField] 
        private GameObject _iconPrefab;
        
        [SerializeField] 
        private Transform _container;

        public void SetLivesCount(int lives)
        {
            for (int i = _container.childCount; i < lives; i++)
            {
                Instantiate(_iconPrefab, _container);
            }

            for (int i = 0; i < _container.childCount; i++)
            {
                _container.GetChild(i).gameObject.SetActive(i < lives);
            }
        }
        
        public void Dispose()
        {
            if (this != null)
            {
                Destroy(gameObject);
            }
        }
    }
}
