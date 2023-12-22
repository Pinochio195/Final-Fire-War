using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Ring
{
    #region Component

    [Serializable]
    public class GameController
    {
        [ChangeColorLabel(0.2f, 1, 1)] public GameObject _playerPrefabs;
        [ChangeColorLabel(0.2f, 1, 1)] public GameObject _zombiePrefabs;
        [ChangeColorLabel(0.2f, 1, 1)] public Transform _playerPositionCreate;
        [ChangeColorLabel(0.2f, 1, 1)] public List<BotController> _listZombie;
        //[ChangeColorLabel(0.2f, 1, 1)][Tooltip("xác định số lượng zombies để tính gold")] public int _startCountZombie;
        [ChangeColorLabel(0.2f, 1, 1)] public List<Transform> _listPositionCreateZombie;
    }

    [Serializable]
    public class PlayerController
    {
        [ChangeColorLabel(0.2f, 1, 1)] public List<Rigidbody> _listRigidbody;
        [ChangeColorLabel(0.2f, 1, 1)] public Animator _animator;
        [ChangeColorLabel(0.2f, 1, 1)] public Transform _rotatePlayer;

        [HeaderTextColor(0.5f, .5f, 1f, headerText = "Fire For Player")]
        [ChangeColorLabel(0.2f, 1, 1)] public Transform _firePosition;

        [ChangeColorLabel(0.2f, 1, 1)] public GameObject _prefabBullet;
        [ChangeColorLabel(0.2f, 1, 1)] public Vector3 _directionBullet;
        [ChangeColorLabel(0.2f, 1, 1)] public ParticleSystem _effectFire;
        [ChangeColorLabel(0.2f, 1, 1)] public ParticleSystem _effectBullet;
        [HeaderTextColor(0.5f, .5f, 1f, headerText = "List Bot in deadzone")]
        [ChangeColorLabel(0.2f, 1, 1)] public List<BotController> _listBot;
        [ChangeColorLabel(0.2f, 1, 1)][Tooltip("Dùng để check khi có zombie vào trong vòng deadzone")] public bool isCheckTarget;
        [ChangeColorLabel(0.2f, 1, 1)] public GameObject _targetFollow;
        [ChangeColorLabel(0.2f, 1, 1)][Tooltip("xác định đối tưởng target")] public BotController currentZombieTarget;
        [ChangeColorLabel(0.2f, 1, 1)][Tooltip("khi bằng true sẽ next sang con thứ tiếp theo")] public bool isNextZombie;
    }
    [Serializable]
    public class BotManager
    {
        [HeaderTextColor(0.5f, .5f, 1f, headerText = "State Machine")]
        [ChangeColorLabel(0.2f, 1, 1)] public List<Rigidbody> _listRigidbody;
        [ChangeColorLabel(0.2f, 1, 1)] public Animator _animator;
        [ChangeColorLabel(0.2f, 1, 1)] public Transform _rotateZombie;
        [ChangeColorLabel(0.2f, 1, 1)] public Collider _collider;
        [ChangeColorLabel(0.2f, 1, 1)] public NavMeshAgent _navMeshAgent;
        public enum CheckPlayer
        {
            InSide,
            OutSide
        }
        [HeaderTextColor(0.5f, .5f, 1f, headerText = "Check Player")]
        [ChangeColorLabel(0.2f, 1, 1)] public CheckPlayer _checkZonePlayer = CheckPlayer.OutSide;
        [Tooltip("Tìm player để nhận destination (Không sử dụng để reference)")][ChangeColorLabel(0.2f, 1, 1)] public Vector3 _positionDestination;
        [HeaderTextColor(0.5f, .5f, 1f, headerText = "Touch Bullet")]
        [ChangeColorLabel(0.2f, 1, 1)] public TouchBullet _touchBullet;
        [ChangeColorLabel(0.2f, 1, 1)] public GameObject _rotateTarget;

        


    }

    [Serializable]
    public class MusicController
    {
        [ChangeColorLabel(0.2f, 1, 1)] public AudioSource audioSource_Element;
        [ChangeColorLabel(0.9f, .55f, .95f)] public List<AudioClip> listAudioClip;

        public enum TypeMusic
        {
            Fire,
            FireLoop,
            TouchBorder, 
            TouchZombie,
            Bomb,
            BackGround
        }
        [ChangeColorLabel(0.9f, .55f, .95f)] public TypeMusic _audioType;
    }

    [Serializable]
    public class UiController
    {
        [ChangeColorLabel(0.2f, 1, 1)] public Text _textAlive;
        [ChangeColorLabel(0.2f, 1, 1)] public VariableJoystick _variableJoystick;
        [ChangeColorLabel(0.2f, 1, 1)] public LevelManager _levelManager;
    }
    [Serializable]
    public class LevelController
    {
        [ChangeColorLabel(0.2f, 1, 1)] public List<GameObject> _listLevel;
        [ChangeColorLabel(0.2f, 1, 1)] public List<D_Weapons> _listDataWeapon;
    }
    [Serializable]
    public class CheckScene
    {
        [ChangeColorLabel(.7f, 1f, 1f)] public bool _isGetCurrentNameScene;
        [ChangeColorLabel(.7f, 1f, 1f)] public string _nameSceneChange;
    }

    #endregion Component

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

    #endregion Text Color

    #region Editor Window

#if UNITY_EDITOR

    #region Save Position Object

    public class SavingPositionObject : EditorWindow
    {
        private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();

        #region Saving Position Object

        [MenuItem("Window/Save Position/Saving Position Object")]
        public static void ShowWindow()
        {
            GetWindow<SavingPositionObject>("Saving Position Object");
        }

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged += HandlePlayModeChange;
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= HandlePlayModeChange;
        }

        private void HandlePlayModeChange(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredEditMode)
            {
                LoadPositions();
                Debug.Log(1);
            }
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Save Positions"))
            {
                SavePositions();
            }
        }

        private void SavePositions()
        {
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                originalPositions[obj] = obj.transform.position;
            }
        }

        private void LoadPositions()
        {
            foreach (KeyValuePair<GameObject, Vector3> entry in originalPositions)
            {
                if (entry.Key != null)
                {
                    entry.Key.transform.position = entry.Value;
                }
            }
        }

        #endregion Saving Position Object
    }

    public class ObjectPositionSaverEditor : EditorWindow
    {
        private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();

        #region Object Position Saver

        [MenuItem("Window/Save Position/Object Position Saver")]
        public static void ShowWindow()
        {
            GetWindow<ObjectPositionSaverEditor>("Object Position Saver");
        }

        private void OnGUI()
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

        private void SavePositions()
        {
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                originalPositions[obj] = obj.transform.position;
            }
        }

        private void LoadPositions()
        {
            foreach (KeyValuePair<GameObject, Vector3> entry in originalPositions)
            {
                entry.Key.transform.position = entry.Value;
            }
        }

        #endregion Object Position Saver
    }

    #endregion Save Position Object

#endif

    #endregion Editor Window

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

    #endregion Base Method
}