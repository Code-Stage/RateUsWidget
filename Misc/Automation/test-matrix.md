# Release test contract

The repository contains a reusable Editor script, with no Unity test wrapper or automated toolbar/graphics assertions. Release publication remains disabled. The explicit blocker prevents an absent suite from becoming a passing result. Product minimum changes and UI fixtures require separate product work.

Run and verify the shared matrix from this repository before a full release. Use a frozen checkout of [UAS-Common 5bdb5f4](https://github.com/Code-Stage/UAS-Common/tree/5bdb5f4b42658ddef92f8ddbb0041149de9819f6/ai-agent-guides/plugins/uas-shared/skills/ship-asset) or a later reviewed revision throughout testing, upload and finalization. The runner records its exact revision and file hashes; changing it invalidates retained evidence.

The configured minimum is 2022.3. Resolve the latest stable stream from the official archive for each release; do not substitute an installed patch or preview. A new baseline without a verified version-project mapping blocks execution.

This ship.config supplies matrix and branch settings only. Export/upload setup is not configured by this change.
