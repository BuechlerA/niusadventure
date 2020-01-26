using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimator : MonoBehaviour
{

    Animator anim;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();

        anim.SetBool("idle2", false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        anim.SetBool("idle2", true);

	}
}
