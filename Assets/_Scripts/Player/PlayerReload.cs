using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReload : MonoBehaviour
{
    // Start is called before the first frame update

    private float timeToReload = 0.0f;
    private SpriteRenderer reloadBar;
    void Start()
    {
        reloadBar = this.transform.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
       if(timeToReload > 0.1f) {
            timeToReload -= Time.deltaTime;    
        }
       else {
           timeToReload = 0.0f;
            reloadBar.transform.GetChild(0).localScale = new Vector3( 2.0f, 1.0f, 1.0f);
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.transform.GetChild(1).GetComponent<Canvas>().enabled = false;
       }

         // Debug.Log("time to reload = " + timeToReload /  this.transform.parent.GetComponentInChildren<Weapon>().getReloadSpeed());
        
          //reloadBar.transform.GetChild(0).localScale = new Vector3( 2.0f * timeToReload / this.transform.parent.GetComponentInChildren<Gun>().getReloadSpeed(), 1.0f, 1.0f);

    }

    public void displayReloadProgressBar() {
        timeToReload = this.transform.parent.GetComponentInChildren<Gun>().getReloadSpeed();
        reloadBar.enabled = true;
        this.transform.GetChild(1).GetComponent<Canvas>().enabled = true;
      
    }
}
