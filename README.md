Asteroids game

The architecture of the game is heavily inspired by ECS and Unity's old component system. 
Game entities are created using the builder pattern, and components are primarly added through extension methods. I think that this approach enhances code readabililty and maintanability.
To ensure better testability, monobehaviours are used only in view layer for presentation and collision tracking purposes. Because of that, I had to make entities lifecycle from scratch, as I couldn't rely on the Unity's objects lifecycle. 
Also I had to make an adapter to VContainer update manager to keep that dependency isolated.
Currently, asyncronous API's are only (well, almost) for Addressables loading in the component responsible for the GameObject creation, but I guess it'll be throughout the codebase after the migration of configurations and UI to Addressables system.

Used packages: 
- Addressables
- Code Coverage
- NSubstitute
- VContainer

Created packages:
- Bounds2D
- Reactive properties

Builds can be found here: 
https://drive.google.com/drive/folders/15005Zgmh6xC5QNJeZoTGFExan8kaE0z6?usp=sharing