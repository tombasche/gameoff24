using System;
using TMPro;
using UnityEngine;

public class CrouchingIndicator : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "not crouching";
    }

    //private void OnEnable()
    //{
    //    PlayerController.OnPlayerHide += Handle_OnPlayerCrouched;
    //}

    //private void OnDisable()
    //{
    //    PlayerController.OnPlayerHide -= Handle_OnPlayerCrouched;
    //}

    //void Handle_OnPlayerCrouched(bool didCrouch, Action<bool> _)
    //{
    //    if (didCrouch)
    //    {
    //        text.text = "crouching";
    //    }
    //    else
    //    {
    //        text.text = "not crouching";
    //    }
    //}
}
