# **E**nti**CS**
A simple ECS solution for Unity using Zenject.

## Quick Start
> This package uses the [OpenUPM](https://openupm.com/) scoped registry.

To use this package in your Unity project, you have to manually add it to you `manifest.json`.

1. Go to `YourProject/Packages/`
2. Open `manifest.json`
3. Add the OpenUPM scoped registry
3. Add this packages git repository to the dependencies object in the JSON:

### Example:
```json
{
  "dependencies": {
    "com.smaleck.actor-system-rx": "git://github.com/SMaleck/unity-actor-system-rx.git#v0.3.0"
  }
}
```

### With Scoped Registry Dependencies
```json
{
  "dependencies": {
    "com.smaleck.actor-system-rx": "git://github.com/SMaleck/unity-entics.git#v1.0.0"
  },
  "scopedRegistries": [
      {
        "name": "package.openupm.com",
        "url": "https://package.openupm.com",
        "scopes": [
          "com.openupm",
          "com.svermeulen.extenject",
        ]
      }
    ]
  }
```

## How To
See the [documentation](./Documentation/EntiCS.md) for details.