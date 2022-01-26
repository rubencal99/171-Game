using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour
{
    public GameObject obj;
    private Player p1;
    public Text healthText;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        p1 = obj.GetComponent<Player>(); // Get Player class from player object
    }

    // Update is called once per frame
    void Update()
    {      
        if (p1 != null){
            health = p1.Health; // set health int to player health
            // healthText.text = "HP: " + health.ToString(); //Display player health
        }

    }
}
