# **E**nti**CS**
A simple ECS solution for Unity.
> You can see a minimal reference implementation in [Kid Game](https://github.com/SMaleck/kid-game).

This is not a perfect solution and still being improved. Below are **several major caveats** that you should consider, before using this package.
## Components cannot be added/removed during runtime
Systems request entities to work on via a "Filter". The filter result is cached in the `EntitiesRepository` instance of the world. Therefore adding or removing components on entities that are currently in a world will not have any effect. As a workaround you can 
1. Remove the entity from the world
2. Change components as needed
3. Add it back to the world
This will re-filter the entity.

## Entites are not destroyed, when the world is disposed
`EntiCSWorld` implements `IDisposable`, but currently entities that have been added to the world will not be disposed or destroyed, when you dispose the world. You will have to take care of this yourself.

# Quick Start
Add the dependency as a git-link to your project. Please double check, that the version from the snippet is the one you actually want.
```json
{
  "dependencies": {
    "com.smaleck.entics": "git://github.com/SMaleck/unity-entics.git#v0.2.0"
  }
}
```
----
# Integration

## Worlds
EntiCS has a concept of worlds. An EntiCS world contains entities and systems. 
Each update cycle it will iterate over all systems, passing them the requested entities to operate on.
Each world can tick independently, although you could also pass the same ticker to every world.

The simplest use case is to have a world per scene, with its related entities. Since systems are supposed to be stateless, it would make sense to create each system just once and add the same instance all worlds. However, each world has its own list of systems, so you can pass a different set, if you have a special use case.

## Entities
`IEntity` is the main entity interface. It is implemented by two classes out-of-the-box:

- `Entity` is meant to be used in C# directly, and allows you to have a an entity without GameObjects being involved
- `MonoEntity` is meant to be added to a GameObject or Prefab, and is the easiest way of having an entity directly in the scene

Entities must be added to an `IEntiCSWorld` in order to be updated by Systems.

## Components
Create Components by inheriting from `IEntityComponent`. Any class inheriting this interface can be added as a component to `IEntity`.
This interface is implemented by two classes out-of-the-box:

- `EntityComponent`, similar to `Entity`, this is the pure-C# version of components. You will have to attach it manually to an entity.
- `MonoEntityComponent`, similar to `MonoEntity`, this is meant to be added to a GameObject with a `MonoEntity` and will be automatically attached.

## Systems
Create Systems by inheriting from `IEntitySystem`. Each System needs to define a type-filter array, so the world knows which entities to send to it. Systems also need to be added to a  World, in order to be run.
You can control order of execution by setting the `Priority` property.