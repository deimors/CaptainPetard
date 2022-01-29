using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableBlockPresenter : MonoBehaviour
{
    public void HandleExplosion()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
