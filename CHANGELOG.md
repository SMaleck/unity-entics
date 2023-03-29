# Changelog

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
