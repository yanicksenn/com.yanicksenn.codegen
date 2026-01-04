# Codegen

The code generation package for Unity projects.

## Installation

1. Open "Package Manager"
2. Choose "Add package from git URL..."
3. Use the HTTPS URL of this repository:
4. Enter `https://github.com/yanicksenn/com.yanicksenn.codegen.git`
5. Click "Add"

## Dependencies

- [com.yanicksenn.utils](https://github.com/yanicksenn/com.yanicksenn.utils)
- [VContainer](https://vcontainer.hadashikick.jp/)

## Features

### Injection Registries

Automatically generates VContainer installers (`ScriptableObjectInstaller`) for ScriptableObject assets. This allows you to easily inject specific instances of ScriptableObjects into your dependency graph.

**Usage:**

1. Add the `[GenerateInjectionRegistry]` attribute to your ScriptableObject class.
2. Create assets of this type in your project.
3. The generator will create a `<Type>Registry` class in `Assets/Generated/Registries`.
4. This registry acts as a `ScriptableObjectInstaller` and registers all found assets of that type, keyed by an enum generated from their file names.

```csharp
[GenerateInjectionRegistry]
[CreateAssetMenu(fileName = "MyConfig", menuName = "Configs/MyConfig")]
public class MyConfig : ScriptableObject { ... }
```

### Variables

Automatically generates typed ScriptableObject Variables and References, facilitating the "ScriptableObject Variable" architecture for shared state and event-driven data.

**Usage:**

1. Add the `[GenerateVariable]` attribute to any serializable type (class or struct).
2. The generator will create:
    - `<Type>Variable`: A `ScriptableObject` wrapper for the type.
    - `<Type>Reference`: A serializable reference that can point to either a constant value or a `<Type>Variable`.
3. Generated files are placed in `Assets/Generated/Variables`.

```csharp
[GenerateVariable]
[Serializable]
public struct PlayerStats {
    public int Health;
    public int Mana;
}
```

### Configuration

You can configure the behavior of the code generation system via `Assets/Settings/CodeGenConfiguration.asset`.

- **Disable Auto Code Generation**: Toggles whether code is automatically regenerated when assets change.

## Usage

### Triggering Generation

Code generation is typically automatic (unless disabled). You can manually trigger or clear generated code via the menu:

- **Tools/Code Generation/Generate All**: Forces regeneration of all artifacts.
- **Tools/Code Generation/Clear All**: Deletes all generated artifacts.

### Generated Output

By default, all generated code is placed in `Assets/Generated/`.
