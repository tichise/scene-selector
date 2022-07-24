using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class SceneSelectManager : MonoBehaviour
{
    [SerializeField]
    private Vector2 scrollPos;

    private int scenes;

    private float

            entryHeight,
            entryWidth,
            screenHeight,
            screenWidth;

    private GUIStyle

            fontStyle,
            vertStyle,
            horizStyle;

    private Rect

            scrollviewPos,
            viewport;

    void Start()
    {
#if UNITY_EDITOR
        scenes = EditorBuildSettings.scenes.Length;
#else
    scenes = SceneManager.sceneCountInBuildSettings;
#endif


        screenHeight = Screen.height;
        screenWidth = Screen.width;
        entryWidth = Screen.width - screenWidth * 0.1f;
        entryHeight = Screen.height * 0.1f;
    }

    private void OnGUI()
    {
        if (fontStyle == null)
        {
            fontStyle = new GUIStyle(GUI.skin.button);
            fontStyle.fontSize = 50;
            scrollviewPos = new Rect(0, 0, screenWidth, screenHeight);
            viewport = new Rect(0, 0, entryWidth, entryHeight * scenes);
            vertStyle = new GUIStyle(GUI.skin.verticalScrollbar);
            vertStyle.fixedWidth = 50f;
            GUI.skin.verticalScrollbarThumb.fixedWidth = 50f;
            horizStyle = new GUIStyle(GUI.skin.horizontalScrollbar);
        }

        var dy = 0.0f;
        scrollPos =
            GUI
                .BeginScrollView(scrollviewPos,
                scrollPos,
                viewport,
                horizStyle,
                vertStyle);

        for (var i = 0; i < scenes; i++)
        {
#if UNITY_EDITOR
            var scene = EditorBuildSettings.scenes[i].path;
#else
      var scene = SceneUtility.GetScenePathByBuildIndex(i);
#endif

            var text = scene.Substring(scene.LastIndexOf('/') + 1);

            if (
                GUI
                    .Button(new Rect(0, dy, screenWidth, entryHeight),
                    text,
                    fontStyle)
            )
            {
                Debug.LogFormat("Loading scene {0}", text);


#if UNITY_EDITOR
                SceneManager.LoadScene (scene);
#else
        SceneManager.LoadScene(i);
#endif


                break;
            }

            dy += entryHeight;
        }

        GUI.EndScrollView();
    }
}
