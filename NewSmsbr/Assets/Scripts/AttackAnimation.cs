using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : MonoBehaviour
{
    public void attack()
    {
        GetComponent<Animation>().Play();

    }
}
