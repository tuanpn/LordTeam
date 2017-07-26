using UnityEngine;
using System.Collections;

public class AchievementLayer : MonoBehaviour {

    private Vector2 scrollPosition;
    private Rect bounds;

    private void Start()
    {
        scrollPosition = Vector2.zero;
        bounds = new Rect(128, 68, 538, 322);
    }

    private void OnGUI()
    {
        
        /*
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width / 800f, Screen.height / 480f, 1f));
        GUI.BeginGroup(bounds);

       // GUILayout.BeginHorizontal(GUILayout.Width(538), GUILayout.Height(322));

        GUILayout.BeginScrollView(scrollPosition, GUIStyle.none);

        for (int i = 0; i < 100; i++)
        {
         //   GUI.Button(new Rect(0, 100 * i, 100, 30), "asdasdadsasd");
            GUILayout.Button("aaaaaa");
        }
        //GUILayout.EndHorizontal();

        GUILayout.EndScrollView();

        GUI.EndGroup();
        */

        /*
        scrollPosition = GUI.BeginScrollView(new Rect(10, 0, 280, 100), scrollPosition, new Rect(0, 0, 220, 200));
        GUI.Button(new Rect(0, 0, 100, 20), "Top-left");
        GUI.Button(new Rect(120, 0, 100, 20), "Top-right");
        GUI.Button(new Rect(0, 180, 100, 20), "Bottom-left");
        GUI.Button(new Rect(120, 180, 100, 20), "Bottom-right");
        GUI.EndScrollView();
         * */
    }
}
