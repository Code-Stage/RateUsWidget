namespace CodeStage.EditorCommon.Tools.Tests
{
    using System;
    using System.Reflection;
    using UnityEditor;
    using UnityEngine;

    internal sealed class ToolbarTestWindow : EditorWindow
    {
        public RateUsWidget Widget;
        public Rect Stars;
        public Rect Toolbar;
        public float LabelWidth;
        public int Repaints;
        public int InputEvents;
        public int HoveredAfterInput;
        public Vector2 InputPosition;
        public readonly Vector2[] StarInputPositions = new Vector2[6];
        public bool Visible;
        public Exception Error;

        private void OnGUI()
        {
            if (Widget == null) return;
            try
            {
                using (new GUILayout.AreaScope(new Rect(0, 0, position.width, position.height)))
                using (var row = new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
                {
                    Visible = Widget.DrawToolbar();
                    if (Event.current.type == EventType.MouseMove)
                    {
                        InputEvents++;
                        InputPosition = Event.current.mousePosition;
                        HoveredAfterInput = (int)typeof(RateUsWidget).GetField("hoveredStar", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Widget);
                    }
                    if (Event.current.type == EventType.Repaint && Visible)
                    {
                        Stars = GUILayoutUtility.GetLastRect();
                        Toolbar = row.rect;
                        var style = (GUIStyle)typeof(RateUsWidget).GetField("labelStyle", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Widget);
                        LabelWidth = style.CalcSize(new GUIContent(Widget.Label)).x;
                        for (var star = 0; star < StarInputPositions.Length; star++)
                        {
                            var point = new Vector2(Stars.x + star * 16 + 7, Stars.center.y);
                            StarInputPositions[star] = GUIUtility.GUIToScreenPoint(point) - position.position;
                        }
                        Repaints++;
                    }
                }
            }
            catch (Exception exception) { Error = exception; }
        }
    }
}
