using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMoney : MonoBehaviour
{   
    public GameObject obj;
    public Player p;

    public int money;
    public Text MoneyText;
    // Start is called before the first frame update
    void Start()
    {
          p = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
           //p = obj.GetComponent<Player>();
         // 
    }

    // Update is called once per frame
    void Update()
    {
         if (p != null){
           money = p.Wallet; // set health int to player health
           MoneyText.text = money.ToString();
        }

          
    }
}
