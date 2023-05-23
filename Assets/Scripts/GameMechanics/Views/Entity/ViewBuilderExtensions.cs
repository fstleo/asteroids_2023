using Asteroids.GameMechanics.Entities;
using Asteroids.Infrastructure;
using UnityEngine;

namespace Asteroids.GameMechanics.Views.Entity
{
    
    public static class ViewBuilderExtensions
    {
        public static EntityBuilder WithView(this EntityBuilder builder, IProvider<GameObject> viewProvider, string prefabId)
        {
            return builder.WithComponent(entity => new ViewComponent(entity, viewProvider, prefabId));
        }
    }

}
