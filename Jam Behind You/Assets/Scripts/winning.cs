using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class winning : MonoBehaviour
{
    public Text winning_text;
    public Image winscreen;
    public LevelManager levelmanager;
 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        winning_text.color = new Color(.2f, .2f, .2f, 1f);
        winscreen.color = new Color(.8f, .8f, .8f, 1f);
        Destroy(other.gameObject);
        winning_text.text += "\nScore:" + levelmanager.score; 
    }

}
