using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids.Infrastructure
{
    public class AsteroidsGameApp : IApplication
    {
        public void Quit()
        {
            Application.Quit();
        }

        public void Reload()
        {
            SceneManager.LoadScene("Game");
        }
    }
}
