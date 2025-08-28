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

    [Header("�Ŵ�������")]
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
        Debug.Log("�ʱ�ȭ ����");
        // �ʱ�ȭ��
        mapManager.Switch(0);

        // ���̺� �ҷ�����
        yield return new WaitForSeconds(1f);

        //���� ����
        ChangeState(GameState.Title);

        // �ڿ� ����
        yield return null;

        // ���谡 ����
        yield return null;

        Debug.Log("�ʱ�ȭ �Ϸ�");
    }


    private void Tick()
    {
        // ���� ���̺�
        autoSaveTimer += Time.deltaTime;
        if (autoSaveTimer >= autoSaveSet)
        {
            //���̺�Ŵ��� ���̺��Լ� ����
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
                Debug.Log(">Ÿ��Ʋ");
                break;
            case GameState.GamePlay:
                GameManager.Instance.onEvent = false;
                Debug.Log(">���� �÷���");
                break;
            case GameState.Battle:
                Debug.Log(">����");
                break;
        }
    }

    public void StartAdventure(string regionName)
    {

    }

    public void QuitGame()
    {
        // ���� ���̺� �Լ�

        Application.Quit();
    }
}
