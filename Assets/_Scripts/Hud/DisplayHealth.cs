using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour
{
    public GameObject obj;
    private Player p1;
     private Image HealthBar;
    public Text healthText;
    private int health;
    private int lastHealth;

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
             healthText.text = "HP: " + health.ToString(); //Display player health
           // if(health != lastHealth)
              //  StartCoroutine(UpdateHealthBar());
            
          //  lastHealth = health;
        }

    }

    public void CallUpdateHealthBar() {
        StartCoroutine(UpdateHealthBar());
    }

     public IEnumerator UpdateHealthBar() {
    	HealthBar = this.transform.GetChild(0).GetChild(0).GetComponent<Image>();
    	var fillAmount = Mathf.Clamp((float)p1.Health / (float)p1.MaxHealth, 0.0f, 1.0f);
    	Debug.Log("in UpdateHealthBar");
    	

    	for(float t = 0.0f; t < 1.0f; t += Time.deltaTime){
   			HealthBar.fillAmount =  Mathf.Lerp(HealthBar.fillAmount, fillAmount, t*t);
   			yield return null;
    	}
    	
    }
}
