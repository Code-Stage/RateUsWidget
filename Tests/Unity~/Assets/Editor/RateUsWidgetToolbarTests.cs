namespace CodeStage.EditorCommon.Tools.Tests
{
    using System;
    using System.Collections;
    using NUnit.Framework;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Rendering;
    using UnityEngine.TestTools;

    public sealed class RateUsWidgetToolbarTests
    {
        private ToolbarTestWindow window;
        private string preference;

        [TearDown]
        public void TearDown()
        {
            if (window != null) window.Close();
            if (preference != null) EditorPrefs.DeleteKey(preference + ".HasRated");
        }

        [UnityTest]
        public IEnumerator NormalToolbarKeepsLabelAndStarHitAreas()
        {
            yield return OpenToolbar();
            Assert.GreaterOrEqual(window.Toolbar.height, 21f, "Toolbar clips its 21-point content.");
            Assert.GreaterOrEqual(window.Stars.xMin - window.Toolbar.xMin, window.LabelWidth, "Label overlaps the stars.");
            Assert.GreaterOrEqual(window.Stars.width, 78f, "Toolbar clips its five stars.");
            Assert.GreaterOrEqual(window.Toolbar.yMax, window.Stars.yMax);
            for (var star = 0; star < 5; star++)
            {
                yield return MovePointer(window.StarInputPositions[star]);
                Assert.AreEqual(star + 1, window.HoveredAfterInput,
                    $"Wrong star hit area; received {window.InputPosition}; stars {window.Stars}");
            }
            yield return MovePointer(window.StarInputPositions[5]);
            Assert.AreEqual(0, window.HoveredAfterInput);
            Debug.Log($"Toolbar graphics={SystemInfo.graphicsDeviceType}; native pixelsPerPoint={EditorGUIUtility.pixelsPerPoint}");
        }

        [UnityTest]
        public IEnumerator PersistedRatingHidesToolbarAndResetDrawsItAgain()
        {
            yield return OpenToolbar();
            Assert.IsTrue(window.Visible);
            EditorPrefs.SetBool(preference + ".HasRated", true);
            yield return MovePointer(Vector2.zero);
            Assert.IsTrue(window.Widget.HasRated);
            Assert.IsFalse(window.Visible);
            window.Widget.ResetRating();
            Assert.IsFalse(window.Widget.HasRated);
            var previousRepaints = window.Repaints;
            var deadline = EditorApplication.timeSinceStartup + 5;
            while (window.Repaints == previousRepaints && window.Error == null && EditorApplication.timeSinceStartup < deadline)
            {
                window.Repaint();
                yield return null;
            }
            Assert.IsNull(window.Error, window.Error?.ToString());
            Assert.Greater(window.Repaints, previousRepaints, "Reset toolbar did not repaint.");
            Assert.IsTrue(window.Visible);
        }

        private IEnumerator OpenToolbar()
        {
            Assert.AreNotEqual(GraphicsDeviceType.Null, SystemInfo.graphicsDeviceType, "Toolbar acceptance requires graphics.");
            Assert.IsNotNull(EditorGUIUtility.IconContent("Favorite Icon").image, "Filled star icon is unavailable.");
            Assert.IsNotNull(EditorGUIUtility.IconContent("Favorite").image, "Empty star icon is unavailable.");
            preference = "CodeStage.RateUsWidget.Tests." + Guid.NewGuid().ToString("N");
            window = ScriptableObject.CreateInstance<ToolbarTestWindow>();
            window.Widget = new RateUsWidget(preference, "", "", window.Repaint);
            window.position = new Rect(100, 100, 640, 160);
            window.wantsMouseMove = true;
            window.Show();
            var deadline = EditorApplication.timeSinceStartup + 20;
            while (window.Repaints < 2 && window.Error == null && EditorApplication.timeSinceStartup < deadline)
            {
                window.Repaint();
                yield return null;
            }
            Assert.IsNull(window.Error, window.Error?.ToString());
            Assert.GreaterOrEqual(window.Repaints, 2, "Toolbar never repainted.");
        }

        private IEnumerator MovePointer(Vector2 point)
        {
            var previousInputs = window.InputEvents;
            window.SendEvent(new Event { type = EventType.MouseMove, mousePosition = point });
            var deadline = EditorApplication.timeSinceStartup + 5;
            while (window.InputEvents == previousInputs && EditorApplication.timeSinceStartup < deadline) yield return null;
            Assert.IsNull(window.Error, window.Error?.ToString());
            Assert.Greater(window.InputEvents, previousInputs, "Window did not receive a mouse event.");
        }
    }
}
