using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
public static class GeneralSceneHelper
{
    public static void QuitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public static void ShowMessage(string message, GameObject dialog, Text dialogText)
    {
        dialogText.text = message;
        dialog.SetActive(true);
    }


    /// <summary>
    /// Get the screen rect bounds of this object. corners is working storage for 4 points, it is set to the screen space coordinates
    /// or use the returned Rect which finds the min/max
    /// </summary>
    public static Rect GetScreenRect(Vector3[] corners, RectTransform rectTransform)
    {
        rectTransform.GetWorldCorners(corners);

        float xMin = float.PositiveInfinity, xMax = float.NegativeInfinity, yMin = float.PositiveInfinity, yMax = float.NegativeInfinity;
        for (int i = 0; i < 4; ++i)
        {
            // For Canvas mode Screen Space - Overlay there is no Camera; best solution I've found
            // is to use RectTransformUtility.WorldToScreenPoint) with a null camera.
            Vector3 screenCoord = RectTransformUtility.WorldToScreenPoint(null, corners[i]);
            if (screenCoord.x < xMin) xMin = screenCoord.x;
            if (screenCoord.x > xMax) xMax = screenCoord.x;
            if (screenCoord.y < yMin) yMin = screenCoord.y;
            if (screenCoord.y > yMax) yMax = screenCoord.y;
            corners[i] = screenCoord;
        }
        Rect result = new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
        return result;
    }

}