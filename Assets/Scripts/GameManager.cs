using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int playerScore;
    private int currentTurn;

    public OptionsManager OptionsManager { get; private set; }
    public AudioManager AudioManager { get; private set; }
    public DeckManager DeckManager { get; private set; }

    [SerializeField] private RectTransform deckRoot;


    private void Awake()
    {
        if (Instance == null)
        { 
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeManagers();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeManagers()
    {
        OptionsManager = CreateOrGet<OptionsManager>("Prefabs/OptionsManager");
        AudioManager = CreateOrGet<AudioManager>("Prefabs/AudioManager");
        DeckManager = CreateOrGet<DeckManager>("Prefabs/DeckManager");

        /*
        OptionsManager = GetComponentInChildren<OptionsManager>();
        AudioManager = GetComponentInChildren<AudioManager>();
        DeckManager = GetComponentInChildren<DeckManager>();*/

        /*
        if (OptionsManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/OptionsManager");
            if (prefab == null)
            {
                Debug.Log($"OptionsManager prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                OptionsManager = GetComponentInChildren<OptionsManager>();
            }
        }
        if (AudioManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/AudioManager");
            if (prefab == null)
            {
                Debug.Log($"AudioManager prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                AudioManager = GetComponentInChildren<AudioManager>();
            }
        }
        if (DeckManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/DeckManager");
            if (prefab == null)
            {
                Debug.Log($"DeckManager prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                DeckManager = GetComponentInChildren<DeckManager>();
            }
        }*/
    }


    public int PlayerScore
    {
        get { return playerScore; }
        set { playerScore = value; }
    }
    public int CurrentTurn
    {
        get { return currentTurn; }
        set { currentTurn = value; }
    }

    private T CreateOrGet<T>(string path) where T : MonoBehaviour
    {
        T obj = GetComponentInChildren<T>();

        if (obj != null)
        {
            Debug.Log($"Found existing {typeof(T).Name}");
            return obj;
        }

        GameObject prefab = Resources.Load<GameObject>(path);

        if (prefab == null)
        {
            Debug.LogError($"{typeof(T).Name} prefab not found at {path}");
            return null;
        }

        GameObject instance = Instantiate(prefab, transform);
        Debug.Log($"Instantiated {typeof(T).Name}: {instance.name}");

        return instance.GetComponent<T>();
    }

    public void DrawCardFromDeck()
    {
        if (DeckManager != null) {
            DeckManager.DrawCard();
        }
        else {
            Debug.LogError("DeckManager is null in GameManager!");
        }
    }
}
