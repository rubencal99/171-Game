using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Extends AgentWeapon so we can use the functions from it
public class PlayerWeapon : AgentWeapon
{
    private float timeToReload = 0.0f;

    public GameObject selectedWeapon;
    public int numGrenades = 5;
    public GameObject Grenade;

    public GameObject Primary;
    public GameObject Secondary;

    Vector3 mousePos;


    private void Start()
    {
        Primary.SetActive(true);
        Secondary.SetActive(false);
        InfAmmo = false;
    }


    /*private void Update()
    {

        GameObject previousSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
            Primary.SetActive(true);
            Secondary.SetActive(false);
            Primary.GetComponent<IWeapon>().ForceReload();
            selectedWeapon = Primary;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
            Primary.SetActive(false);
            Secondary.SetActive(true);
            Secondary.GetComponent<IWeapon>().ForceReload();
            selectedWeapon = Secondary;
        }
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
            DisplayWeapon.Instance.UpdateWeapon();
        }
    }*/

    public void TogglePrimary()
    {
        if(!Primary.activeSelf)
        {
            Primary.SetActive(true);
            Secondary.SetActive(false);
            Primary.GetComponent<IWeapon>().ForceReload();
            AssignWeapon();
        }
    }

    public void ToggleSecondary()
    {
        if(!Secondary.activeSelf)
        {
            Primary.SetActive(false);
            Secondary.SetActive(true);
            Secondary.GetComponent<IWeapon>().ForceReload();
            AssignWeapon();
        }
    }

    private void SelectWeapon()
    {
        /*int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                weapon.GetComponent<IWeapon>().ForceReload();
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }*/


        AssignWeapon();
    }

    public void ThrowItem()
    {
        if (numGrenades > 0)
        {
            Debug.Log("Item Thrown");
            SpawnItem(transform.position, transform.rotation);
        }
    }

    private void SpawnItem(Vector3 position, Quaternion rotation)
    {
        Debug.Log("Before instantiate");
        var itemPrefab = Instantiate(Grenade, position, rotation);
        Debug.Log("After instantiate");
    }
}
