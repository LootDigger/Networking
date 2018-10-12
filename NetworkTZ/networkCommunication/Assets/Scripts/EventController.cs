using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    #region Events

    public static event Action turnOnTheLight;
    public static event Action makeExplosion;


    public static event Action clientJoinServer;



    #endregion


    #region Enums

    public enum Events
    {
       turnOnTheLigh,
       makeExplosion,
       joinServer


    }

    #endregion
    


    #region Public Methods

    public static void InvokeEvent(Events myEvent)
    {

        if(myEvent == Events.turnOnTheLigh)
        {
            if (turnOnTheLight != null)
                turnOnTheLight();
        }

        if (myEvent == Events.makeExplosion)
        {
            if (makeExplosion != null)
                makeExplosion();
        }

        if (myEvent == Events.joinServer)
        {
            if (clientJoinServer != null)
                clientJoinServer();
        }

    }

    #endregion




}
