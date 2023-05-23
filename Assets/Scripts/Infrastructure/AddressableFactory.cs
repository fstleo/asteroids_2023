using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Asteroids.Infrastructure
{

    public class AddressableFactory : IProvider<GameObject>
    {
        public Task<GameObject> GetAsync(string id)
        {
            return Addressables.InstantiateAsync(id).Task;
        }

        public void Release(GameObject item)
        {
            Addressables.ReleaseInstance(item);
        }
    }

}