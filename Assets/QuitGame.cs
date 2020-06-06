using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void LeaveGame()
    {
        //TODO: maybe confirm?
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
