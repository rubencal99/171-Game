using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    [SerializeField]
    public string name {get; set;}

    public void ForceReload();
}
