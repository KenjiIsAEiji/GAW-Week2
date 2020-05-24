using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] bool DebugLoadMode = false;
    [SerializeField] List<SpawnData> spawnDatas;
    [SerializeField] int dataIndex = 0;

    [Header("UI")]
    [SerializeField] CanvasGroup LoadingGroup;
    [SerializeField] Image LoadProgress;
    [SerializeField] CanvasGroup titleGroup;

    [SerializeField] CanvasGroup mainGroup;
    [SerializeField] Image HpImage;
    [SerializeField] Gradient HpColor;

    [SerializeField] CanvasGroup ClearGroup;

    [SerializeField] CanvasGroup GameOverGroup;

    [Header("EnemySpawn")]
    public List<EnemyController> enemies;

    public PlayerController player;
    public SpawnManager spawnManager;

    [SerializeField] bool clearFlag = false;

    enum GameState
    {
        Ready,
        Play,
        End
    }

    [SerializeField] GameState gameState;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.Ready:

                if (Keyboard.current.spaceKey.isPressed)
                {
                    StartCoroutine(CustomLoadScene("main"));
                }

                if (player && spawnManager)
                {
                    gameState = GameState.Play;
                }
                break;

            case GameState.Play:
                titleGroup.alpha = 0;
                mainGroup.alpha = 1;

                if(enemies.Count <= 0)
                {
                    if (dataIndex >= spawnDatas.Count)
                    {
                        gameState = GameState.End;
                        clearFlag = true;
                        ClearAnimation();
                    }
                    else
                    {
                        spawnManager.NextSpawn(spawnDatas[dataIndex]);
                        Debug.Log("Spaen");
                        dataIndex++;
                    }
                }

                HpImage.fillAmount = player.Hp / player.MaxHp;
                HpImage.color = HpColor.Evaluate(player.Hp / player.MaxHp);

                if (player.isDead)
                {
                    GameOverAnimation();
                    gameState = GameState.End;
                }
                
                break;

            case GameState.End:

                if (Keyboard.current.spaceKey.isPressed)
                {
                    StartCoroutine(CustomLoadScene("Title"));
                }

                break;
        }
    }


    IEnumerator CustomLoadScene(string loadSceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(loadSceneName);

        LoadProgress.gameObject.SetActive(true);
        LoadProgress.fillAmount = 0f;
        LoadingGroup.DOFade(1f, 0.1f);

        while (true)
        {
            yield return null;

            LoadProgress.fillAmount = async.progress;
            if(async.progress >= 0.9f)
            {
                LoadProgress.fillAmount = 1f;
                async.allowSceneActivation = true;

                break;
            }
        }

        LoadingGroup.DOFade(0f, 0.1f);
    }


    void ClearAnimation()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(ClearGroup.DOFade(1f, 0.5f));
        sequence.Play();
    }
    
    void GameOverAnimation()
    {
        if (!clearFlag)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(GameOverGroup.DOFade(1f, 3f));
            sequence.Play();
        }
    }
}
