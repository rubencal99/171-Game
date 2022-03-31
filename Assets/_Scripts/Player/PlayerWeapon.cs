using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Extends AgentWeapon so we can use the functions from it
public class PlayerWeapon : AgentWeapon
{
    private float timeToReload = 0.0f;

    public int selectedWeapon = 0;
    public int numGrenades = 5;
    public GameObject Grenade;

    public GameObject itemPrefab;

    public RenderThrowableArc throwableArc;

    Vector3 mousePos;


    private void Start()
    {
        SelectWeapon();
        InfAmmo = false;
    }


    private void Update()
    {

        int previousSelectedWeapon = selectedWeapon;

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
        }
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
            DisplayWeapon.Instance.UpdateWeapon();
        }
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                weapon.GetComponent<Gun>().ForceReload();
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }


        AssignWeapon();
    }

    public void prepThrow()
    {
        if (numGrenades > 0)
        {
            Debug.Log("throw prepped");
            SpawnItem(transform.position, transform.rotation);
            //throwableArc.SetArcAngle(this.desiredAngle);
        }
    }
    public void ThrowItem()
    {
            Debug.Log("Item Thrown");
            itemPrefab.GetComponent<_BaseThrowable>().Thrown = true;
            itemPrefab.GetComponent<_BaseThrowable>().addForce();
            
    }


    private void SpawnItem(Vector3 position, Quaternion rotation)
    {
        Debug.Log("Before instantiate");
        itemPrefab = Instantiate(Grenade, position, rotation);
        itemPrefab.transform.parent = this.transform;
        Debug.Log("After instantiate");
    }
}
