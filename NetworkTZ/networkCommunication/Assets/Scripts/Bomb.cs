using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    #region Private fields

   // private bool isLighterActive = false;

    #endregion


    #region Serializable fields

   
    private ParticleSystem explosion;

    #endregion


    #region Unity LifeCycle

    void Awake()
    {
        explosion = gameObject.GetComponent<ParticleSystem>();
        EventController.makeExplosion += MakeExplosion;


    }

    #endregion

    
    #region Private methods

    private void MakeExplosion()
    {
        explosion.Play();
    }

    #endregion
}
