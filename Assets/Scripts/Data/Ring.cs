using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

namespace Ring
{
    #region Component
    [Serializable]
    public class GameController
    {
        [ChangeColorLabel(0.2f, 1, 1)] public GameObject _player;
    }
    [Serializable]
    public class PlayerController
    {
        [ChangeColorLabel(0.2f, 1, 1)] public List<Rigidbody> _listRigidbody;
        [ChangeColorLabel(0.2f, 1, 1)] public Animator _animator;
        [ChangeColorLabel(0.2f, 1, 1)] public Transform _rotateCamPlayer;
    }

    [Serializable]
    public class MusicController
    {
        [ChangeColorLabel(0.2f, 1, 1)] public AudioSource audioSource_;
        [ChangeColorLabel(0.9f, .55f, .95f)] public AudioClip audioClip_;
    }

    [Serializable]
    public class UiController
    {
        [ChangeColorLabel(0.2f, 1, 1)] public GameObject _winGameObject;
        [ChangeColorLabel(0.2f, 1, 1)] public GameObject _settingGameObject;
    }

    [Serializable]
    public class CheckScene
    {
        [ChangeColorLabel(.7f, 1f, 1f)] public bool _isGetCurrentNameScene;
        [ChangeColorLabel(.7f, 1f, 1f)] public string _nameSceneChange;
    }

    #endregion

    #region Text Color

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(HeaderTextColorAttribute))]
    public class HeaderTextColorDecorator : DecoratorDrawer
    {
        private GUIStyle headerStyle;
        private bool initialized;

        private void InitGUIStyle()
        {
            headerStyle = new GUIStyle(GUI.skin.label);
            headerStyle.fontStyle = FontStyle.Bold;
            headerStyle.normal.textColor = ((HeaderTextColorAttribute)attribute).color;
            initialized = true;
        }

        public override float GetHeight()
        {
            /*if (!initialized)
            {
                InitGUIStyle();
            }*/

            return EditorGUIUtility.singleLineHeight * 2;
        }

        public override void OnGUI(Rect position)
        {
            if (!initialized)
            {
                InitGUIStyle();
            }

            HeaderTextColorAttribute attribute = (HeaderTextColorAttribute)this.attribute;
            string headerText = attribute.headerText;

            Rect labelRect = new Rect(position.x, position.y, EditorGUIUtility.labelWidth + 100, 50);
            EditorGUI.LabelField(labelRect, headerText, headerStyle);
        }
    }

    [CustomPropertyDrawer(typeof(ChangeColorLabelAttribute))]
    public class RedLabelDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Get the color from the attribute
            ChangeColorLabelAttribute changeColorLabelAttribute = (ChangeColorLabelAttribute)attribute;
            Color labelColor = changeColorLabelAttribute.color;

            // Set the color
            GUIStyle coloredLabelStyle = new GUIStyle(EditorStyles.label) { normal = { textColor = labelColor } };
            float originalLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = EditorStyles.label.CalcSize(label).x;

            // Draw the colored label
            EditorGUI.LabelField(position, label, coloredLabelStyle);

            // Draw the property without the label
            EditorGUIUtility.labelWidth = originalLabelWidth;
            position.x += EditorGUIUtility.labelWidth;
            position.width -= EditorGUIUtility.labelWidth;
            EditorGUI.PropertyField(position, property, GUIContent.none, true);

            EditorGUI.EndProperty();
        }
    }
#endif

    public class HeaderTextColorAttribute : PropertyAttribute
    {
        public Color color;
        public string headerText;

        public HeaderTextColorAttribute(float r, float g, float b, float a = 1.0f, string headerText = "")
        {
            color = new Color(r, g, b, a);
            this.headerText = headerText;
        }
    }

    public class ChangeColorLabelAttribute : PropertyAttribute
    {
        public Color color;

        public ChangeColorLabelAttribute(float r, float g, float b, float a = 1.0f)
        {
            color = new Color(r, g, b, a);
        }
    }

    #endregion

    #region Editor Window

#if UNITY_EDITOR

    #region Save Position Object

    public class SavingPositionObject : EditorWindow
    {
        Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();

        #region Saving Position Object

        [MenuItem("Window/Save Position/Saving Position Object")]
        public static void ShowWindow()
        {
            GetWindow<SavingPositionObject>("Saving Position Object");
        }

        void OnEnable()
        {
            EditorApplication.playModeStateChanged += HandlePlayModeChange;
        }

        void OnDisable()
        {
            EditorApplication.playModeStateChanged -= HandlePlayModeChange;
        }

        void HandlePlayModeChange(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredEditMode)
            {
                LoadPositions();
                Debug.Log(1);
            }
        }

        void OnGUI()
        {
            if (GUILayout.Button("Save Positions"))
            {
                SavePositions();
            }
        }

        void SavePositions()
        {
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                originalPositions[obj] = obj.transform.position;
            }
        }

        void LoadPositions()
        {
            foreach (KeyValuePair<GameObject, Vector3> entry in originalPositions)
            {
                if (entry.Key != null)
                {
                    entry.Key.transform.position = entry.Value;
                }
            }
        }

        #endregion
    }

    public class ObjectPositionSaverEditor : EditorWindow
    {
        Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();

        #region Object Position Saver

        [MenuItem("Window/Save Position/Object Position Saver")]
        public static void ShowWindow()
        {
            GetWindow<ObjectPositionSaverEditor>("Object Position Saver");
        }

        void OnGUI()
        {
            if (GUILayout.Button("Save Positions"))
            {
                SavePositions();
            }

            if (GUILayout.Button("Load Positions"))
            {
                LoadPositions();
            }
        }

        void SavePositions()
        {
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                originalPositions[obj] = obj.transform.position;
            }
        }

        void LoadPositions()
        {
            foreach (KeyValuePair<GameObject, Vector3> entry in originalPositions)
            {
                entry.Key.transform.position = entry.Value;
            }
        }

        #endregion
    }

    #endregion

#endif

    #endregion

    #region Base Method

    public abstract class RingSingleton<T> : MonoBehaviour where T : RingSingleton<T>
    {
        private static T _instance;

        public enum ChangeDestroy
        {
            DontDestroy,
            Destroy
        }

        public ChangeDestroy _changDestroy = ChangeDestroy.Destroy;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        Debug.LogError("An instance of " + typeof(T) +
                                       " is needed in the scene, but there is none.");
                    }
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            _instance = (T)this;
            if (_changDestroy == ChangeDestroy.DontDestroy)
            {
                DontDestroyOnLoad(gameObject);
            }
            
            
        }
    }

    #endregion
}