using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class InventorySoundManager : MonoBehaviour
{
    public FMODUnity.EventReference hoverEvent;
    public FMODUnity.EventReference clickItemEvent;
    public FMODUnity.EventReference invOpenEvent;

    public void PlayClickItemSound(ItemType _itemType)
    {
        float _typeFloat = (float)_itemType;
        AudioHelper.PlayOneShotWithParam(clickItemEvent, Vector3.zero, ("ClickReleaseType", _typeFloat));
    }
    public void PlayWepSwitchSound()
    {
        AudioHelper.PlayOneShotWithParam(clickItemEvent, Vector3.zero, ("ClickReleaseType", (float)ItemType.Weapon));
    }
    public void PlayInvOpenSound()
    {
        float _active = 0.0f;
        if (gameObject.activeSelf){ _active = 1.0f; }
        AudioHelper.PlayOneShotWithParam(invOpenEvent, Vector3.zero, ("isOpen", _active));
    }
    public void PlayHoverSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(hoverEvent);
    }


}
