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

    void Awake()
    {
        explosion = gameObject.GetComponent<ParticleSystem>();
        EventController.makeExplosion += MakeExplosion;


    }



    #region Private methods

    private void MakeExplosion()
    {
        explosion.Play();
    }

    #endregion
}
