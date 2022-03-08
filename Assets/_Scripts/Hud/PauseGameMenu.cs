using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameMenu : MonoBehaviour
{
    public GameObject PauseMenu;
    public bool isPause;
    public Shop Shop;
    private void start()
    {
        isPause = false;
        // Debug.Log("size: \n" + UnityEngine.Screen.width + "\n" + UnityEngine.Sreen.height);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log("Esc press");
            if (Shop.inShop == false){              //Shop can also use "esc" to exit,therefore need to be not in shop for press esc to pause
                // Debug.Log("not in shop");
                if (isPause == true)
                {
                    OnResume();
                }else
                {
                    OnPause();
                }
            }
        }
    }

    public void OnPause()
    {
        var Menu = PauseMenu.GetComponent<Canvas>();        
        Menu.enabled = !Menu.enabled;
        isPause = true;
        Time.timeScale = 0;
    }

    public void OnResume()
    {
        var Menu = PauseMenu.GetComponent<Canvas>();
        Menu.enabled = !Menu.enabled;
        isPause = false;
        Time.timeScale = 1.0f;
    }

    public void button02()
    {

    }

    public void button03()
    {
        
    }


}
