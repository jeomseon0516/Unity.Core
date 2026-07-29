# Jeomseon Unity Core

[한국어](./README.md) | English

Jeomseon Unity Core provides collections, extension methods, helpers, serialization utilities, and state-machine types shared by Jeomseon Unity packages.

## Requirements

- Unity 2022.3 or newer

## Install with OpenUPM

Register the OpenUPM scoped registry once in `Packages/manifest.json`.

```json
{
  "scopedRegistries": [
    {
      "name": "OpenUPM",
      "url": "https://package.openupm.com",
      "scopes": [
        "com.jeomseon.unity"
      ]
    }
  ],
  "dependencies": {
    "com.jeomseon.unity.core": "0.1.1"
  }
}
```

## Install from Git

Use this URL with Unity Package Manager's `Install package from git URL` command.

```text
https://github.com/jeomseon0516/Unity.Core.git#v0.1.1
```

## Local development

Reference this repository as a local package from an integration project.

```json
{
  "dependencies": {
    "com.jeomseon.unity.core": "file:../../Jeomseon.Unity.Core"
  },
  "testables": [
    "com.jeomseon.unity.core"
  ]
}
```

The package remains editable under `Packages/Jeomseon Unity Core` in the Unity Project window.

## Contents

- `Jeomseon.Collections`: Deque and PriorityQueue
- `Jeomseon.Extensions`: Unity and .NET extension methods
- `Jeomseon.Helper`: math, color, parsing, text, and texture helpers
- `Jeomseon.State`: cached state-machine implementation
- Serializable Unity value utilities
- Compiler compatibility types

## Excluded integrations

- JsonUtility, which depends on Newtonsoft JSON

These integrations remain outside Core to avoid imposing unrelated dependencies.

## Testing

Add this package to the integration project's `testables` list, then run the EditMode suite with Unity Test Runner.

## Compatibility

Source `.meta` GUIDs and C# namespaces inherited from JeomseonScriptPack are retained to preserve Unity asset references and source compatibility.

## License

[MIT License](./LICENSE.md)
