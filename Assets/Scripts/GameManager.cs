using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class WaitForSecondsRealtime : CustomYieldInstruction
{
    private float waitTime;

    public override bool keepWaiting
    {
        get { return Time.realtimeSinceStartup < waitTime; }
    }

    public WaitForSecondsRealtime(float time)
    {
        waitTime = Time.realtimeSinceStartup + time;
    }
}

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField]
    private float gamePlayTime = 0f;
    public float GamePlayTime
    {
        get { return this.gamePlayTime; }
    }

    [SerializeField]
    private float bulletTimeScale = 1f;

    [SerializeField]
    private float bulletTimeLength = 0f;

    private int bulletTimeRequest = 0;

    private bool isPlaying = false;
    public bool IsPlaying
    {
        get { return this.isPlaying; }
        set { this.isPlaying = value; }
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(GameLoop());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator GameLoop()
    {
        /// 開始文字
        yield return StartCoroutine(GameStart());

        /// メインゲーム
        yield return StartCoroutine(GameMain());

        /// タイムアップ
        yield return StartCoroutine(GameTimeup());

        /// リザルト
        yield return StartCoroutine(GameResult());
    }

    private IEnumerator GameStart()
    {
        yield return 0f;
    }

    private IEnumerator GameMain()
    {
        while(true)
        {
            gamePlayTime -= Time.deltaTime;
            if(0 < gamePlayTime)
            {
                yield return null;
            }
            else
            {
                break;
            }

        }
    }

    private IEnumerator GameTimeup()
    {
        yield return 0f;
    }

    private IEnumerator GameResult()
    {
        yield return 0f;

        SceneManager.LoadScene("title");
    }

    private IEnumerator BulletTime()
    {
        Time.timeScale = bulletTimeScale;

        yield return new WaitForSecondsRealtime(bulletTimeLength);

        --bulletTimeRequest;
        if(bulletTimeRequest <= 0){
            Time.timeScale = 1f;
        }
    }

    public void StartBulletTime()
    {
        ++bulletTimeRequest;

        StartCoroutine(BulletTime());
    }

    public void ResetBulletTime()
    {
        Time.timeScale = 1f;
    }
}
