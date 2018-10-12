using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{


    #region Serializeable fields

    [SerializeField]
    GameObject ConnectUI;

    [SerializeField]
    GameObject CommunicationtUI;

    #endregion


    #region UnityLifecycle


    void Start()
    {
        EventController.clientJoinServer += HideConnectUI;
        EventController.clientJoinServer += ShowCommunicationUI;
    }


    #endregion




    #region private methods

    void HideConnectUI()
    {
        ConnectUI.SetActive(false);
    }

    void ShowCommunicationUI()
    {
        CommunicationtUI.SetActive(true);
    }
    
    #endregion

}
