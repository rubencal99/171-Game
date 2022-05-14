using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class InventorySoundManager : MonoBehaviour
{
    public FMODUnity.EventReference hoverEvent;
    public FMODUnity.EventReference clickItemSound;

    public void PlayClickItemSound(ItemType _itemType)
    {
        float _typeFloat = (float)(int)_itemType;
        AudioHelper.PlayOneShotWithParam(clickItemSound, Vector3.zero, ("ClickReleaseType", _typeFloat));
    }
    public void PlayHoverSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(hoverEvent);
    }


}
