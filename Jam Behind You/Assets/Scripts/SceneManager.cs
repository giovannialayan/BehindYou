using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> coins;
    public int level;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < coins.Count; i++)
        {

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

        GUI.Box(new Rect(10, 10, 100, 60), "Level: " + level + "\nScore: " + score);
    }
}
