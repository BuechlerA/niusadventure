using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueController : MonoBehaviour
{
    public GameObject selectTheDialogue;

    public AudioSource grandpaSound;
    public AudioClip huh;

    bool huhplayed = false;

    void Update()
    {
        if(selectTheDialogue.GetComponent<Dialogue>()._isDialoguePlaying && !huhplayed)
        {
            grandpaSound.PlayOneShot(huh);
            huhplayed = true;
        }

        if (selectTheDialogue.GetComponent<Dialogue>()._canExitDialogue && huhplayed)
        {
            huhplayed = false;
        }
    }


}
