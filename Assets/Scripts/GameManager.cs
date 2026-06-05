using Cards;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int playerScore = 0;
    private int currentTurn = 1;
    private int currentRound = 1;
    private int roundScore = 0;

    public OptionsManager OptionsManager { get; private set; }
    public AudioManager AudioManager { get; private set; }
    public DeckManager DeckManager { get; private set; }
    public DiscardPileManager DiscardPileManager { get; private set; }


    [SerializeField] private RectTransform canvasRoot;

    [SerializeField] private HandManager handManager;
    [SerializeField] private GridManager gridManager;

    [SerializeField] private int roundGoalIncrease = 2;
    [SerializeField] private int turnsPerRound = 3;
    [SerializeField] private TMP_Text turnText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject retryButton;


    [SerializeField] private int scorePerTurn = 21;
    private int MinimumRoundScore => scorePerTurn * turnsPerRound + (currentRound - 1) * roundGoalIncrease;

    private int MaximumRoundScore =>
        Mathf.FloorToInt(MinimumRoundScore * 1.1f);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
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
        DiscardPileManager = CreateOrGet<DiscardPileManager>("Prefabs/DiscardPileManager");

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
    public int CurrentRound
    {
        get { return CurrentRound; }
        set { CurrentRound = value; }
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

    public void EndTurn()
    {
        Debug.Log("END TURN PRESSED");
        handManager.DiscardHand();

        int score = gridManager.CalculateBoardValue();
        roundScore = roundScore + score;

        Debug.Log("Turn Score: " + score);
        Debug.Log("Round Score: " + roundScore);

        gridManager.GridClear();

        AdvanceTurn();
    }

    private void AdvanceTurn()
    {
        currentTurn++;
        if (currentTurn > turnsPerRound)
        {
            playerScore += roundScore;
            finalScoreText.text = $"Final Score: {playerScore}";

            if (!CheckRoundResult())
            {
                return;
            }
            currentTurn = 1;
            currentRound++;


            Debug.Log("Round Score: " + roundScore);
            roundScore = 0;

            Debug.Log("Round " + currentRound);
        }
        Debug.Log("Current Turn: " + currentTurn);
        UpdateTurnUI();
        UpdateScoreUI();
        StartTurn();
    }

    private void StartTurn()
    {
        while (handManager.CardCount < DeckManager.drawStartTurn)
        {
            DeckManager.DrawCard();
        }
    }

    private void Start()
    {
        handManager = FindAnyObjectByType<HandManager>();
        gridManager = FindAnyObjectByType<GridManager>();
        losePanel.SetActive(false);
        Debug.Log($"Round {currentRound} Turn {currentTurn}");
        UpdateTurnUI();
        UpdateScoreUI();
        StartTurn();
    }

    private void UpdateTurnUI()
    {
        turnText.text = $"Round {currentRound} - Turn {currentTurn}/3";
    }

    private bool CheckRoundResult()
    {
        int minimum = scorePerTurn * turnsPerRound + (currentRound - 1) * roundGoalIncrease;
        int maximum = Mathf.FloorToInt(minimum * 1.1f);

        Debug.Log($"Round Final Score: {roundScore}");
        Debug.Log($"Required Range: {minimum} - {maximum}");

        if (roundScore >= minimum && roundScore <= maximum)
        {
            Debug.Log("ROUND PASSED");
            return true;
        }
        else
        {
            Debug.Log("ROUND FAILED");
            GameOver();
            return false;
        }
    }

    private void UpdateScoreUI()
    {
        scoreText.text = $"Score: {roundScore}/{MinimumRoundScore}-{MaximumRoundScore}";
    }

    private void GameOver()
    {
        StartCoroutine(ShowLoseScreen());
    }

    private IEnumerator ShowLoseScreen()
    {
        losePanel.SetActive(true);

        retryButton.SetActive(false);

        yield return new WaitForSeconds(1f);

        retryButton.SetActive(true);
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
