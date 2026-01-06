# ScaffoldUI

**ScaffoldUI** is a Godot UI layout system designed for clarity, performance, and explicit control. It provides a deterministic and optimized way to organize UI elements, focusing solely on layout. Visuals, animations, and interactions are intentionally excluded, allowing developers to pair it with their preferred styling or animation systems.

ScaffoldUI is split into two complementary systems:

* **ScaffoldUI Layout (Fixed / Editor-Time)**: A QoS compiler for native Godot Control nodes that translates human-readable layout definitions into optimized anchors, offsets, and margins. Zero runtime overhead; scenes remain fully functional without the plugin once layouts are applied.
* **ScaffoldUI Dynamic (Runtime / Reactive)**: Lightweight, fast, reactive layout nodes for dynamic interfaces where fixed layouts are insufficient. Optimized to minimize performance cost while adapting to parent or child changes.

---

## Features

### ScaffoldUI Layout (Editor-Time)

* Explicit width, height, position, and margins
* Converts human-readable layout definitions to native Godot anchors and offsets
* No custom nodes or resources
* Zero runtime performance cost
* Can be removed without affecting applied UI
* Layout-only; compatible with any visual or animation system

### ScaffoldUI Dynamic (Runtime)

* Lightweight reactive layout nodes
* Automatic adaptation to parent size changes and dynamic content
* Fast layout recalculation, optimized for performance
* Optional, where dynamic behavior is desired
* Layout-only; compatible with external visuals and animation libraries

---

## Philosophy

ScaffoldUI is built around **explicit, deterministic, and optimized layout management**:

* Layout-only; visuals, animation, and styles are separate
* Fixed/editor-time layouts provide QoS compilation to native nodes
* Dynamic runtime nodes are lightweight, fast, and reactive
* Systems are modular and optional; scenes remain functional without them

For a detailed discussion, see [PHILOSOPHY.md](PHILOSOPHY.md).

---

## Usage

1. Define width, height, and position for Control nodes in percentage or pixel offsets.
2. Apply fixed layouts using ScaffoldUI Layout for deterministic results.
3. Introduce ScaffoldUI Dynamic nodes where reactive or adaptive behavior is needed.

---

## License

MIT License
