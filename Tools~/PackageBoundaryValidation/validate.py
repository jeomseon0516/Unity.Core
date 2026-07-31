#!/usr/bin/env python3
import json
import pathlib
import re
import sys


REPOSITORY_ROOT = pathlib.Path(__file__).resolve().parents[2]
PACKAGE_MANIFEST = REPOSITORY_ROOT / "package.json"
RUNTIME_ASMDEF = REPOSITORY_ROOT / "Runtime" / "Jeomseon.Unity.Core.asmdef"
RUNTIME_DIRECTORY = REPOSITORY_ROOT / "Runtime"


def load_json(path: pathlib.Path) -> dict:
    with path.open(encoding="utf-8") as stream:
        return json.load(stream)


def validate_manifest(errors: list[str]) -> None:
    manifest = load_json(PACKAGE_MANIFEST)
    if manifest.get("name") != "com.jeomseon.unity.core":
        errors.append("package.json must declare com.jeomseon.unity.core.")

    dependencies = manifest.get("dependencies", {})
    core_version = dependencies.get("com.jeomseon.core")
    if core_version is None:
        errors.append("package.json must depend on com.jeomseon.core.")
    elif not re.fullmatch(r"\d+\.\d+\.\d+(?:[-+][0-9A-Za-z.-]+)?", core_version):
        errors.append(
            "com.jeomseon.core must use a registry version instead of a file or Git URL: "
            f"{core_version}"
        )


def validate_assembly_definition(errors: list[str]) -> None:
    assembly_definition = load_json(RUNTIME_ASMDEF)
    if assembly_definition.get("rootNamespace") != "Jeomseon":
        errors.append("Runtime asmdef rootNamespace must remain Jeomseon.")
    if assembly_definition.get("overrideReferences") is not False:
        errors.append("Runtime asmdef must use normal package dependency resolution.")

    precompiled_references = assembly_definition.get("precompiledReferences", [])
    if any(pathlib.Path(reference).name == "Jeomseon.Core.dll"
           for reference in precompiled_references):
        errors.append(
            "Runtime asmdef must not bind a vendored Jeomseon.Core.dll; "
            "use the UPM dependency."
        )


def validate_no_vendored_core(errors: list[str]) -> None:
    forbidden_directories = [
        RUNTIME_DIRECTORY / "Collections",
        RUNTIME_DIRECTORY / "Reflection",
        RUNTIME_DIRECTORY / "Plugins",
        REPOSITORY_ROOT / "Source~" / "Jeomseon.Core",
    ]
    for directory in forbidden_directories:
        if directory.exists():
            errors.append(f"Pure Core implementation must not be vendored at {directory}.")

    for dll_path in REPOSITORY_ROOT.rglob("Jeomseon.Core.dll"):
        errors.append(
            f"Vendored Jeomseon.Core.dll found at {dll_path}; use com.jeomseon.core."
        )

    pure_core_namespace = re.compile(
        r"^\s*namespace\s+Jeomseon\.(?:Collections|Reflection)(?:\s|[.{])",
        re.MULTILINE,
    )
    for source_path in RUNTIME_DIRECTORY.rglob("*.cs"):
        if pure_core_namespace.search(source_path.read_text(encoding="utf-8")):
            errors.append(
                f"{source_path} declares a namespace owned by the pure Core package."
            )


def main() -> int:
    errors: list[str] = []
    validate_manifest(errors)
    validate_assembly_definition(errors)
    validate_no_vendored_core(errors)

    if errors:
        print("Package boundary validation failed:", file=sys.stderr)
        for error in errors:
            print(f"- {error}", file=sys.stderr)
        return 1

    print(
        "Package boundary validation passed: Unity.Core consumes pure Core only through UPM."
    )
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
