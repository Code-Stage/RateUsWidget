# Release test contract

The `Tests/Unity~` wrapper compiles the standalone Editor script through one direct source link. The graphics suite opens a real EditorWindow, checks label space and toolbar height, and sends mouse events through every star hit area and the outside region. Each test uses a unique EditorPrefs key and removes it during teardown.

The toolbar case uses Unity's native GUI coordinate conversions and records the graphics device and actual `pixelsPerPoint`. A separate case checks that a persisted rating hides the toolbar and that resetting it restores rendering. Fresh results from both baseline editors are required. GitHub Release publication remains disabled because this repository has no numbered product release.

Run and verify the shared matrix from this repository before a full release. Use a frozen checkout of [UAS-Common 5bdb5f4](https://github.com/Code-Stage/UAS-Common/tree/5bdb5f4b42658ddef92f8ddbb0041149de9819f6/ai-agent-guides/plugins/uas-shared/skills/ship-asset) or a later reviewed revision throughout testing, upload and finalization. The runner records its exact revision and file hashes; changing it invalidates retained evidence.

The configured minimum is 2022.3. Resolve the latest stable stream from the official archive for each release; do not substitute an installed patch or preview. A new baseline without a verified version-project mapping blocks execution.

This ship.config supplies matrix and branch settings only. Export/upload setup is not configured by this change.
