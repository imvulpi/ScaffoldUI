# ScaffoldUI Design Philosophy

ScaffoldUI is a UI layout system for Godot that prioritizes **clarity, performance, and explicit control**. It is split into two complementary systems: the fixed/editor-time system and the runtime/dynamic system. Both are focused on layout only; visuals, animations, and styling are intentionally excluded. This separation ensures that developers retain full control over the UI while minimizing runtime overhead and maximizing maintainability.

---

## 1. Fixed / Editor-Time System (ScaffoldUI Layout)

The fixed system acts as a **Quality-of-Service (QoS) compiler** for native Godot Control nodes. Its purpose is to translate human-readable layout intent into optimized, deterministic anchors, offsets, and margins without introducing any additional runtime cost.

### Core Principles

1. **No Custom Nodes or Resources**: The fixed system uses only vanilla Godot Controls. No proprietary node types or resources are introduced.
2. **Zero Runtime Overhead**: Once the layout is applied, it runs entirely on Godot's native systems. There is no additional performance cost beyond what the engine already spends on the vanilla nodes.
3. **Explicit and Deterministic**: Width, height, position, and margins are explicitly defined. Layout is predictable and does not rely on magic or implicit rules.
4. **Removable Without Side Effects**: The fixed/editor-time system can be removed at any point without affecting already applied UI. Once compiled, the scene is fully functional using only vanilla Godot nodes.
5. **Layout Only**: The system does not manage visuals, animations, or interactions. It provides the structure and spatial organization, leaving styling and motion to other systems or libraries.

This system is designed for **editor-time convenience**, rapid prototyping, and deterministic, optimized layouts.

---

## 2. Runtime / Dynamic System (ScaffoldUI Dynamic)

The runtime system complements the fixed system for cases where **static layout is insufficient**. ScaffoldUI Dynamic provides **lightweight, fast, reactive layout nodes** for dynamic or adaptive interfaces.

### Core Principles

1. **Lightweight Nodes**: Dynamic layout nodes are minimal and optimized to reduce overhead.
2. **Reactive**: Nodes respond to parent size changes, child addition/removal, and other layout-relevant events automatically.
3. **Fast**: Performance is a priority. Layout recalculation is limited to what is strictly necessary, avoiding unnecessary per-frame computations.
4. **Optional**: Only included when dynamic behavior is required. Static layouts remain unaffected if the dynamic system is not used.
5. **Layout Only**: Like the fixed system, dynamic nodes manage **structure and placement only**. Animations, visuals, or styles are not handled.

ScaffoldUI Dynamic exists **only where static layouts cannot suffice**, providing flexibility without compromising performance or clarity.

---

## 3. General Principles for Both Systems

- **Separation of Concerns**: ScaffoldUI focuses on layout exclusively. Visuals, animations, and interactions are outside its scope.
- **Determinism**: Both fixed and dynamic layouts produce predictable, reproducible results.
- **Minimalism**: Avoid unnecessary complexity, overabstraction, or runtime cost.
- **Extensibility**: The systems are designed to allow future modules (e.g., visuals, motion) without breaking existing workflows.
- **Trustworthy**: Users can rely on ScaffoldUI without fear of hidden behaviors, side effects, or lock-in.

---

ScaffoldUI provides **explicit, optimized layout management** for Godot: a fixed/editor-time QoS system for most UI needs, and a lightweight dynamic system where reactive behavior is necessary. Both are focused on clarity, performance, and flexibility while leaving visuals and animation to other systems.

