using System;
using UnityEngine;

namespace Asteroids.Game.GameMenu
{
    public class GameMenuWidgetView : MonoBehaviour, IGameMenu, IDisposable
    {

        public event Action BackToTheGame;
        public event Action Exit;

        public virtual void _OnBackToTheGame()
        {
            BackToTheGame?.Invoke();
        }
        
        public virtual void _OnExit()
        {
            Exit?.Invoke();
        }
        
        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}