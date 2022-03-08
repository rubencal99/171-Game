using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArgumentationMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ArgumentingMenu;
    public bool LookingArgument;
    private void start()
    {
        LookingArgument = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //Debug.Log("Tap press");
            if (LookingArgument == true)
            {
                LeaveArgument();
            }else
            {
                Argumenting();
            }
            
        }
    }

    public void Argumenting()
    {
        var Menu = ArgumentingMenu.GetComponent<Canvas>();
        Menu.enabled = !Menu.enabled;
        LookingArgument = true;
    }

    public void LeaveArgument()
    {
        var Menu = ArgumentingMenu.GetComponent<Canvas>();
        Menu.enabled = !Menu.enabled;
        LookingArgument = false;
    }

}
