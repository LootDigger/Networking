using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLight : MonoBehaviour
{


    #region Private fields

    private bool isLighterActive = false;
    private Light light;

    #endregion



    #region UnityLifecycle

    void Awake()
    {
        light = gameObject.GetComponent<Light>();
        EventController.turnOnTheLight += TurnTheLight;

        light.enabled = false;

    }

    #endregion

    #region Private methods

    private void TurnTheLight()
    {
        isLighterActive = !isLighterActive;

        if (isLighterActive)
            light.enabled = true;
        else
           if (!isLighterActive)
            light.enabled = false;

    }

    #endregion

}

