# RateUsWidget

[![Unity Version](https://img.shields.io/badge/Unity-2021.3%2B-blue?logo=unity)](https://unity.com/)
[![License](https://img.shields.io/badge/License-MPL--2.0-orange)](LICENSE)
[![Discord](https://img.shields.io/discord/847940058476052491?color=7289da&label=Discord&logo=discord&logoColor=white)](https://discord.gg/Ppsb89naWf)

Animated star-rating widget for Unity Editor windows (IMGUI). Lets users rate your tool directly from the editor toolbar - positive ratings open your Asset Store review page, lower ratings redirect to a feedback form.

## Features

- Animated hover effects with smooth star scaling
- Configurable positive rating threshold
- Separate URLs for positive reviews and constructive feedback
- Remembers rating state via EditorPrefs
- Integrates into any IMGUI toolbar row

## Demo

https://github.com/user-attachments/assets/bfd04f56-5c80-4b57-b7fa-c15c3dd6f7ab

## Installation

### Option 1: Copy the Script

Copy `Scripts/Editor/RateUsWidget.cs` into your Unity project's `Editor` folder.

### Option 2: Git Submodule

```bash
git submodule add https://github.com/Code-Stage/RateUsWidget.git Assets/RateUsWidget
```

## Quick Start

1. Add a field in your `EditorWindow`:
   ```csharp
   [NonSerialized]
   private RateUsWidget rateUsWidget;
   ```

2. Initialize in `OnEnable()`:
   ```csharp
   rateUsWidget = new RateUsWidget(
       "MyCompany.MyTool.RateUs",
       "https://assetstore.unity.com/packages/your-asset#reviews",
       "https://your-feedback-url.com",
       Repaint
   );
   wantsMouseMove = true;
   ```

3. Draw in your toolbar:
   ```csharp
   using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
   {
       // your toolbar controls ...
       GUILayout.FlexibleSpace();
       if (rateUsWidget.DrawToolbar())
           GUILayout.Space(5f);
   }
   ```

## Configuration

| Property | Default | Description |
|---|---|---|
| `PositiveThreshold` | `4` | Minimum star count to open the review URL |
| `Label` | `"Rate us:"` | Label displayed before the stars |

## Claude Code Skill

This repo includes a [Claude Code](https://docs.anthropic.com/en/docs/claude-code) skill at `.claude/skills/integrate-rate-widget/SKILL.md` that guides AI-assisted integration of the widget into your EditorWindow.

To use it, add this repo as a skill source in your project's `.claude/settings.json`:

```json
{
  "skills": [
    "path/to/RateUsWidget/.claude/skills/integrate-rate-widget/SKILL.md"
  ]
}
```

Then Claude Code can walk through the full integration automatically when you ask it to add a rate-us widget to your editor window.

## Support

[Discord](https://discord.gg/Ppsb89naWf)

## License

This project is licensed under [Mozilla Public License Version 2.0](LICENSE).

## Contributing

Please report any bugs and suggestions via [GitHub Issues](https://github.com/Code-Stage/RateUsWidget/issues).
Pull requests are welcome!
