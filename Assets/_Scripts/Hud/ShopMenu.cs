using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ShopMenuUI;
    public bool isShoping;
    private void start()
    {
        isShoping = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))        //change to interact with shop keeper
        {
            //Debug.Log("Enter the shop");
            if (isShoping == true)
            {
                LeaveShop();
            }else
            {
                EnterShop();
            }
            
        }
    }

    public void EnterShop()
    {
        var Menu = ShopMenuUI.GetComponent<Canvas>();
        Menu.enabled = !Menu.enabled;
        isShoping = true;
    }

    public void LeaveShop()
    {
        var Menu = ShopMenuUI.GetComponent<Canvas>();
        Menu.enabled = !Menu.enabled;
        isShoping = false;
    }
}
