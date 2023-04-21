# Changelog

## [3.0.1] 2023-04-21
[Fixed]
- `EntiCSWorld.Dispose` could throw an exception due to the underlying collection being changed while disposing entities. Modified iteration to be safe against that

## [3.0.0] 2023-04-19
[Changed]
- Removes capability to have more than one component of the same type per entity. This was causing issues on some platforms, added complexity and reduced performance, while offering little value. This was also at odds with the concept of ECS, which should not allow for multiple components of the same type.

## [2.0.0] 2023-04-07
[Fixed]
- `EntiCSTicker.SetTimeScale(float)` did not actually set the internal timescale. This is fixed now.

[Changed]
- Renamed `IUpdateable.Update(float)` interface to `OnUpdate(float)` so MonoBehaviours can directly implement the interface.
- Removed `OnBeforeTick` and `OnAfterTick` actions from `EntiCSTicker`. They were only useful for profiling and caused an unnecessary null-check on each tick.
- Removed `TickerProfiler` as well, as there was no use for it anymore. The Unity Profiler is more capable for this purpose anyway.
- Some small performance tweaks. EntiCS should now be completely allocation free up until the point of the iteration in a given system.
- Removed superfluous, internal interfaces `IEntitiesRepository` and `IEntitySystemsRepository`

[Added]
- `EntiCSWorld` now destroys entities on disposal

## [1.1.0] 2023-03-29
[Added]
- Adds custom editor for MonoEntities, which allows to easily add components.

## [1.0.0] 2023-03-15
[Changed]
- Several small improvements and bugfixes that cam out of using the package in other projects

## [0.2.0] 2022-09-06
[Changed]
- Removes Zenject dependency, which necessitates a significant rewrite
- `ComponentRepository` now can handle multiple components of the same type
- `IEntity` and `IComponentRepository` now exposes all components as a list, to leverage handling multiple components of the same type
- Added a "runner" class to manage the ticker, world and systems, instead of the world doing all that work
- Several usability improvements
- Several performance improvements

## [0.1.0] 2021-11-27
[Added]
- Package creation
