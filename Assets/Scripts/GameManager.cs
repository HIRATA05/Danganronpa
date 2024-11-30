using UnityEngine;
using UnityEngine.SceneManagement;

namespace TECHC.Kamiyashiki
{
    public class GameManager2 : MonoBehaviour
    {
        public static GameManager2 Instance { get; private set; }

        [Header("GameManager")]
        [SerializeField] private bool isGameStart;
        [SerializeField] private int level;
        [SerializeField] private bool isClear = false;
        [SerializeField] private int currentLife = 0;
        [SerializeField] private int maxLife = 3;

        [Header("Debug")]
        public bool isDebug;

        public bool IsGameStart
        {
            get { return isGameStart; }
            set { isGameStart = value; }
        }
        public bool IsClear
        {
            get { return isClear; }
            set { isClear = value; }
        }
        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        public int CurrentLife
        {
            get { return currentLife; }
            set { currentLife = value; }
        }
        public int MaxLife
        {
            get { return maxLife; }
            set { maxLife = value; }
        }

        //ÉVÉìÉOÉãÉgÉì
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            Init();
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
#endif
        }

        /// <summary>
        /// èâä˙âª
        /// </summary>
        private void Init()
        {
            isGameStart = false;
            isClear = false;
            level = 1;
            currentLife = maxLife;
        }
    }
}