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

}