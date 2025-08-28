using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    Intro,
    Title,
    GamePlay,
    Battle
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("매니저모음")]
    public GuildManager guildManager;
    public AdventurerManager adventurerManager;
    public BattleManager battleManager;
    public InventoryManager inventoryManager;
    public ResourceManager resourceManager;
    public SaveManager saveManager;
    public UIManager uiManager;
    public MapManager mapManager;

    public GameState CurrentState { get; private set; }

    private float autoSaveTimer = 0f;
    private float autoSaveSet = 30f;

    public GameObject onObject;
    public Adventurer pickAdventurer;
    public int mapIndex = -1;
    public bool onEvent = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(InitGame());
    }

    private void Update()
    {
        Tick();
    }

    IEnumerator InitGame()
    {
        Debug.Log("초기화 시작");
        // 초기화면
        mapManager.Switch(0);

        // 세이브 불러오기
        yield return new WaitForSeconds(1f);

        //상태 변경
        ChangeState(GameState.Title);

        // 자원 세팅
        yield return null;

        // 모험가 생성
        yield return null;

        Debug.Log("초기화 완료");
    }


    private void Tick()
    {
        // 오토 세이브
        autoSaveTimer += Time.deltaTime;
        if (autoSaveTimer >= autoSaveSet)
        {
            //세이브매니저 세이브함수 실행
            autoSaveTimer = 0f;
        }
    }

    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;

        switch (CurrentState)
        {
            case GameState.Title:
                Debug.Log(">타이틀");
                break;
            case GameState.GamePlay:
                GameManager.Instance.onEvent = false;
                Debug.Log(">게임 플레이");
                break;
            case GameState.Battle:
                Debug.Log(">전투");
                break;
        }
    }

    public void StartAdventure(string regionName)
    {

    }

    public void QuitGame()
    {
        // 게임 세이브 함수

        Application.Quit();
    }
}
