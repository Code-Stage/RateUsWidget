# AGENTS.md

RateUsWidget is a reusable Unity Editor script. Read `README.md` before changing its integration API. Keep changes scoped to `Scripts/Editor/RateUsWidget.cs` and its documented callers.

## Branches and releases

Before creating a branch, opening a PR or preparing a release, read the [shared branch policy](https://github.com/Code-Stage/UAS-Common/blob/main/ai-agent-guides/product-branching.md) and `.github/product-release.json`.

Create feature and fix branches from `dev` and target their PRs there. Cut `release/<version>` from `dev`; merge its validated PR into `main` with a merge commit. Use merge commits for synchronization too; squash and rebase break release ancestry. After a stable merge, synchronize `main` back into `dev`.

This repository has no product version or changelog. GitHub Release publication stays disabled; the PR routing guard still applies.
