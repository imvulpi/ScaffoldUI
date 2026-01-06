# Contributing to ScaffoldUI

Thank you for your interest in contributing to **ScaffoldUI**.

ScaffoldUI is designed around explicitness, determinism, and performance. Contributions should align with these principles, especially the strict separation between editor-time (Fixed) and runtime (Dynamic) systems.

---

## Design Principles (Quick Summary)

Before contributing, please familiarize yourself with the core philosophy:

* **Layout-only**: ScaffoldUI does not handle visuals, themes, or animations.
* **Fixed system is editor-time only**: It must be removable with zero runtime impact.
* **Dynamic system is runtime-focused**: Nodes must be lightweight, reactive, and fast.
* **No unnecessary abstractions**: Prefer explicit data and simple flows.

For the full rationale, see `PHILOSOPHY.md`.

---

## Branch Naming Convention

All work should be done in feature branches using the following format:

```
<username>/<short-description>
```

### Examples

```
alex/layout-parser
jane/fixed-compiler
maria/dynamic-vbox
```

Guidelines:

* Use lowercase
* Use hyphens for separation
* Keep the description short and focused

---

## Commit Message Convention

ScaffoldUI uses a lightweight, conventional commit format:

```
<type>: <short summary>
```

### Commit Types

* **feat** – New functionality or capability
* **refactor** – Code changes without behavior changes
* **fix** – Bug fixes
* **docs** – Documentation-only changes
* **perf** – Performance improvements
* **test** – Adding or updating tests
* **chore** – Maintenance or tooling changes

### Examples

```
feat: add parsed layout data structures
refactor: rename layout schema classes
fix: correct percent anchor calculation
docs: add ScaffoldUI philosophy
perf: reduce allocations in dynamic layout pass
```

### Rules

* Use present tense ("add", not "added")
* Keep the summary under 72 characters
* One logical change per commit
* Avoid implementation details in the title

---

## Fixed vs Dynamic Contribution Rules

### Fixed (Editor-Time) System

* Must not add runtime dependencies
* Must not introduce custom resources
* Must compile entirely into native Godot `Control` properties
* Must remain deterministic and explicit

### Dynamic (Runtime) System

* Nodes must be lightweight
* Layout updates must be efficient
* Avoid unnecessary allocations during layout passes
* Use Fixed layouts whenever possible; Dynamic is for cases Fixed cannot cover

---

## Code Style Guidelines

* Match existing naming and structure
* File name must match class name
* Avoid magic values; prefer explicit enums and data structures
* Keep data models separate from logic (e.g., raw vs parsed data)

---

## Pull Requests

Before opening a pull request:

* Ensure your branch follows the naming convention
* Ensure commits follow the commit message convention
* Verify that Fixed changes have no runtime impact
* Keep pull requests focused and scoped

---

## Final Notes

ScaffoldUI values clarity over cleverness and performance over abstraction. When in doubt, choose the simpler and more explicit solution.

Happy scaffolding.
