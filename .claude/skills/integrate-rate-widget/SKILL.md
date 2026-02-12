# Integrate Rate Us Widget

Guides integration of the Code Stage Rate Us widget into Unity IMGUI EditorWindows.

## Prerequisites

- Target window uses IMGUI (`OnGUI` + `GUILayout`)
- `RateUsWidget.cs` is available at `Scripts/Editor/RateUsWidget.cs`
- Target file has `using CodeStage.EditorCommon.Tools;`

## Required Inputs

Collect or confirm:

- Asset Store review URL (typically with `#reviews`)
- Feedback form URL for low ratings
- EditorPrefs key prefix (example: `"CodeStage.ProductName.RateUs"`)
- Target `EditorWindow` file path

## Integration Steps

1. Add a non-serialized field in the target window:
   ```csharp
   [NonSerialized]
   private RateUsWidget rateUsWidget;
   ```
2. Initialize it once in your window init flow (usually `Init()` or `OnEnable()`):
   ```csharp
   rateUsWidget = new RateUsWidget(
       "<EditorPrefs key prefix>",
       "<review URL>",
       "<feedback URL>",
       Repaint
   );
   ```
3. Ensure hover events are delivered by enabling mouse-move events in `OnEnable()`:
   ```csharp
   wantsMouseMove = true;
   ```
4. Draw the widget in toolbar row after `GUILayout.FlexibleSpace()`:
   ```csharp
   using (new GUILayout.HorizontalScope())
   {
       // existing toolbar controls
       GUILayout.FlexibleSpace();
       if (rateUsWidget.DrawToolbar())
           GUILayout.Space(5f);
   }
   ```
5. Optional debug reset:
   ```csharp
   rateUsWidget.ResetRating();
   ```

## Repaint Guidance

- Do not add a permanent `EditorApplication.update -> Repaint()` loop just for this widget.
- Current `RateUsWidget` already uses the provided repaint callback to continue repaints while hover/scale animation is in progress.
- A global update repaint loop causes unnecessary constant redraws and should be avoided by default.
- Only add update-driven repaint if your window has other independent real-time UI that truly requires it.

## Configuration

- `PositiveThreshold` default is `4`
- `Label` default is `"Rate us:"`

## Behavior

- Hover star `N` highlights stars `1..N`
- Rating `>= PositiveThreshold` opens review URL
- Rating `< PositiveThreshold` opens feedback URL
- After click, widget is hidden via EditorPrefs
- `ResetRating()` shows it again
