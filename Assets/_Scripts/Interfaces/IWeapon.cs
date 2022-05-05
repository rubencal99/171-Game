using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    [SerializeField]
    public string name {get; set;}

    [field: SerializeField]
    public Vector3 startOffset {get; set;}

    public void ForceReload();

    public bool CheckSwap();


}
