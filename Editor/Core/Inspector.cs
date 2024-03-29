using UnityEngine;
using UnityEditor;

namespace Actormachine.Editor
{
    public enum BoxStyle { Default, Error, Warning, Active }
    public enum ButtonStyle { Main, Active, Default }

    public static class Inspector
    {
        private static Color Dark = new Color(0.22f, 0.22f, 0.22f, 1f);
        private static Color GrayDark = new Color(0.5f, 0.5f, 0.5f, 1f);
        private static Color Gray = new Color(0.75f, 0.75f, 0.75f, 1f);
        private static Color Red = new Color(0.85f, 0.38f, 0.35f, 1f);
        private static Color Yellow = new Color(0.90f, 0.80f, 0.48f, 1f);
        private static Color Blue = new Color(0.45f, 0.70f, 0.85f, 1f);
        private static Color Green = new Color(0.50f, 0.80f, 0.50f, 1f);
        private static Color LightGreen = new Color(0.85f, 0.95f, 0.85f, 1f);

        ///<summary> Checking for a single instance on Scene. </summary>
        public static bool CheckSingleInstanceOnScene<T>() where T : Component
        {
            if (Instance.IsSingleInstanceOnScene<T>() == false)
            {
                DrawSubtitle("should be a single Instance on the scene", BoxStyle.Error);

                return false;
            }

            return true;
        }

        ///<summary> Checking for a single instance on gameObject and destroy duplicates. </summary>
        public static bool CheckSingleInstanceOnObject<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject.IsSingleInstanceOnObject<T>() == false)
            {
                T[] requireComponent = gameObject.GetComponents<T>();

                GameObject.DestroyImmediate(requireComponent[1]);

                return false;
            }

            return true;
        }

        ///<summary> Checking for a single instance in children and destroy duplicates. </summary>
        public static bool CheckSingleInstanceInChildren<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject.IsSingleInstanceInChildren<T>() == false)
            {
                T[] requireComponent = gameObject.GetComponentsInChildren<T>();

                GameObject.DestroyImmediate(requireComponent[1]);

                return false;
            }

            return true;
        }

        public static void ShowLink(MonoBehaviour monoScript)
        {
            // Show script Link
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour(monoScript), typeof(MonoScript), false);
            EditorGUI.EndDisabledGroup();

            Rect scriptRect = GUILayoutUtility.GetLastRect();
            EditorGUIUtility.AddCursorRect(scriptRect, MouseCursor.Arrow);

            if (GUI.Button(scriptRect, "", GUIStyle.none))
            {
                UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(AssetDatabase.GetAssetPath(MonoScript.FromMonoBehaviour(monoScript)), 0);
            }
        }

        public static void DrawHeader(string info, int fontSize = 16)
        {
            // Font Style
            GUIStyle fontStyle = new GUIStyle(EditorStyles.label);
            fontStyle.fontSize = fontSize;
            fontStyle.fontStyle = FontStyle.Normal;
            fontStyle.alignment = TextAnchor.MiddleCenter;

            EditorGUILayout.Space(6);
            EditorGUILayout.LabelField(info.ToUpper(), fontStyle, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        }
        
        public static void DrawSubtitle(string info, BoxStyle boxStyle = BoxStyle.Default)
        {
            // Font Style
            Color textColor = GrayDark;

            if (boxStyle == BoxStyle.Error)
            {
                textColor = Red;
            }
            else if (boxStyle == BoxStyle.Warning)
            {
                textColor = Yellow;
            }
            else if (boxStyle == BoxStyle.Active)
            {
                textColor = Green;
            }

            // GUI Style
            GUIStyle fontStyle = new GUIStyle(EditorStyles.label);
            fontStyle.normal.textColor = textColor;
            fontStyle.fontSize = 12;
            fontStyle.fontStyle = FontStyle.Normal;
            fontStyle.alignment = TextAnchor.MiddleCenter;

            // Draw Inspector
            EditorGUILayout.Space(2);
            EditorGUILayout.LabelField(info.ToUpper(), fontStyle, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        }

        public static void DrawText(string info, int fontSize = 12, TextAnchor textAnchor = TextAnchor.MiddleLeft)
        {
            // Font Style
            GUIStyle fontStyle = new GUIStyle(EditorStyles.label);
            fontStyle.normal.textColor = GrayDark;
            fontStyle.fontSize = fontSize;
            fontStyle.fontStyle = FontStyle.Normal;
            fontStyle.alignment = textAnchor;
            EditorGUILayout.Space(2);

            EditorGUILayout.LabelField(info, fontStyle, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        }

        public static void DrawLinkButton(string name, GameObject gameObject, ButtonStyle buttonStyle = ButtonStyle.Default)
        {
            Color backgroundColor = Gray;
            Color textColor = Dark;
            float height = 22;
            int fontSize = 12;

            if (buttonStyle == ButtonStyle.Main)
            {
                backgroundColor = Blue;
                textColor = Dark;
                height = 25;
                fontSize = 16;
            }
            else if (buttonStyle == ButtonStyle.Active)
            {
                backgroundColor = Green;
                textColor = LightGreen;
            }
            else if (buttonStyle == ButtonStyle.Default)
            {
                backgroundColor = Gray;
                textColor = Dark;
            }

            GUIStyle guiStyle = new GUIStyle(GUI.skin.button);
            guiStyle.normal.background = buttonTexture(1, 1, backgroundColor);
            guiStyle.border = new RectOffset(1, 1, 1, 1);
            guiStyle.fontSize = fontSize;
            guiStyle.fontStyle = FontStyle.Bold;
            guiStyle.alignment = TextAnchor.MiddleCenter;
            guiStyle.normal.textColor = textColor;

            EditorGUILayout.Space();

            if (GUILayout.Button(name, guiStyle, GUILayout.ExpandWidth(true), GUILayout.Height(height)))
            {
                EditorGUIUtility.PingObject(gameObject);
            }
        }

        public static void DrawInfoBox(string info, BoxStyle boxStyle = BoxStyle.Default)
        {
            Color backgroundColor = Gray;
            Color textColor = Dark;

            if (boxStyle == BoxStyle.Error)
            {
                backgroundColor = Red;
                textColor = Dark;
            }
            else if (boxStyle == BoxStyle.Warning)
            {
                backgroundColor = Yellow;
                textColor = Dark;
            }
            else if (boxStyle == BoxStyle.Active)
            {
                backgroundColor = Green;
                textColor = LightGreen;
            }

            drawBox(info, backgroundColor, textColor);
        }

        private static void drawBox(string info, Color backgroundColor, Color textColor, int height = 12)
        {
            EditorGUILayout.Space(2);

            // Font Style
            GUIStyle fontStyle = new GUIStyle(EditorStyles.label);
            fontStyle.fontSize = height;
            fontStyle.fontStyle = FontStyle.Normal;
            fontStyle.alignment = TextAnchor.MiddleCenter;
            fontStyle.normal.textColor = textColor;

            // Create a Texture2D object
            Texture2D backgroundTexture = new Texture2D(1, 1);
            backgroundTexture.SetPixel(0, 0, backgroundColor);
            backgroundTexture.Apply();

            // Create a Box
            GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.border = new RectOffset(8, 8, 8, 8);
            boxStyle.normal.background = backgroundTexture;

            EditorGUILayout.BeginVertical(boxStyle);
            EditorGUILayout.LabelField(info.ToUpper(), fontStyle, GUILayout.Height(height), GUILayout.Width(80), GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            EditorGUILayout.EndVertical();
        }

        private static Texture2D buttonTexture(int width, int height, Color color)
        {
            Color[] pixels = new Color[width * height];

            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = color;
            }

            Texture2D texture = new Texture2D(width, height);
            texture.SetPixels(pixels);
            texture.Apply();

            return texture;
        }
    }
}