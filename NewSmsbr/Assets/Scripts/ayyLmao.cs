using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ayyLmao : MonoBehaviour
{
    public Animation anim;

    void Start()
    {
        anim = GetComponent<Animation>();
        anim.Play();
    }

    // Update is called once per frame
    void Update()
    {
        anim.PlayQueued("stand_attack_0");
    }
}
