# Jeomseon Unity Core

Shared collections, extension methods, helpers, serialization utilities, and state-machine types used by Jeomseon Unity packages.

## Installation

Install the package through a Unity Package Manager scoped registry:

```json
{
  "dependencies": {
    "com.jeomseon.unity.core": "0.1.0"
  }
}
```

For local development, add this repository as a local package from the Unity Package Manager.

## Contents

- Collections
- Compiler services compatibility types
- Unity and .NET extension methods
- General-purpose helpers
- Serializable Unity value wrappers
- State machine interfaces and implementation

The attribute-dependent reflection helper and Newtonsoft JSON utilities remain outside this package to keep the core dependency-free.

## Compatibility

- Unity 2022.3 or newer

Existing source `.meta` files are retained from JeomseonScriptPack to preserve Unity asset references.
