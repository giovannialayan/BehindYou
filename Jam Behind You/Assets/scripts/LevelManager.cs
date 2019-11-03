using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public GameObject player;
    public List<GameObject> coins;
    public BoxCollider toLevelTwo;
    public BoxCollider toLevelThree;
    public int level;
    private int score;
    private bool guiTwo = false;
    private bool guiThree = false;
    public Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        player = LevelManager.Instance.player;
        coins = LevelManager.Instance.coins;
        score = LevelManager.Instance.score;
        level = LevelManager.Instance.level;

        if (level == 2)
        { guiThree = true;}
    }

    // Update is called once per frame
    void Update()
    {
        //coin collection check
        for (int i = coins.Count - 1; i >= 0; i--)
        {
            if (player.GetComponent<CapsuleCollider>().bounds.Contains(coins[i].transform.position))
            {
                Destroy(coins[i]);
                score += 100;
            }
        }
        LevelManager.Instance.player = player;
        LevelManager.Instance.coins = coins;
        LevelManager.Instance.score = score;

        //scene loading things
        if (toLevelTwo.bounds.Contains(player.transform.position+new Vector3(0,3,0))||guiTwo==true)
        {
            guiTwo = true;
            if (Input.GetKeyDown(KeyCode.T))
            {
                level = 2;
                LevelManager.Instance.level = level;
                StartCoroutine(LoadAsyncScene("LevelTwo"));}
        }
        else if (toLevelThree.bounds.Contains(player.transform.position+new Vector3(0, 3, 0)))
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                level = 3;
                LevelManager.Instance.level = level;
                StartCoroutine(LoadAsyncScene("LevelThree"));
            }
        }
        LevelManager.Instance.level = level;

        //"death" check
        if (player.transform.position.y < -15)
        {
            switch (level)
            {
                case 1: StartCoroutine(LoadAsyncScene("LevelOne"));
                    break;
                case 2: StartCoroutine(LoadAsyncScene("LevelTwo"));
                    break;
                case 3: StartCoroutine(LoadAsyncScene("LevelThree"));
                    break;
                default:
                    LevelManager.Instance.player = player;
                    LevelManager.Instance.coins = coins;//we need a default value for this
                    LevelManager.Instance.score = 0;
                    LevelManager.Instance.level = 1;
                    StartCoroutine(LoadAsyncScene("Title"));///NOTE:TITLE SCENE IS NOT A THING(yet)
                    break;
            }
        }

        float minutes = Mathf.Floor(Time.time / 60);
        float seconds = Time.time % 60;
        float centiseconds = Mathf.FloorToInt(Time.time * 100 % 100);
        LevelManager.Instance.timeText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, centiseconds);
    }

    IEnumerator LoadAsyncScene(string scene)
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    /// <summary>
    /// Displays stats to the player
    /// </summary>
    private void OnGUI()
    {
        GUI.color = Color.white;

        GUI.skin.box.fontSize = 20;

        GUI.skin.box.wordWrap = true;

        GUI.Box(new Rect(10, 10, 160, 60), "Level: " + level + "\nScore: " + score);
        if (guiTwo == true)
        {
            GUI.Box(new Rect(new Vector2(100, 0), new Vector2(700, 100)),
            "I finally understand! The answer is to look behind you...Behind U!!" +
            " Now I can finally have a free-roaming camera!!!\nPress 't' to continue");
        }
        if (guiThree == true)
        {
            GUI.Box(new Rect(new Vector2(100, 0), new Vector2(700, 100)), "Aw, nuts");
        }
    }
}
    
