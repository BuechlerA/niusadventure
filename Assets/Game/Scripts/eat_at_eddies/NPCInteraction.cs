using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject NPCDialogueIcon;
    private GameObject NPCToInteractWith;
    private NPCDialogueController _NPCDialogueController;

    private bool _isInTrigger;

    private float timer;
    
    void Start()
    {
        NPCDialogueIcon.SetActive(false);
    }

    void Update ()
    {
		if((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Joystick1Button3)) && (NPCToInteractWith != null) && _isInTrigger)
        {
            _NPCDialogueController.selectTheDialogue.SetActive(true);
        }

        if((NPCToInteractWith != null) && _isInTrigger)
        {

            if (_NPCDialogueController.selectTheDialogue.GetComponent<Dialogue>()._canExitDialogue)
            {
                timer += Time.deltaTime;

                if (timer >= 0.3f)
                {
                    _NPCDialogueController.selectTheDialogue.GetComponent<Dialogue>()._canExitDialogue = false;
                    timer = 0;
                }

            }
        }
	}

    void OnTriggerStay(Collider target)
    {
        if (target.tag == "NPC")
        {
            NPCToInteractWith = target.transform.gameObject;
            _NPCDialogueController = NPCToInteractWith.GetComponent<NPCDialogueController>();
            _isInTrigger = true;
            NPCDialogueIcon.SetActive(true);
        }
    }

    void OnTriggerExit(Collider target)
    {
        if (target.tag == "NPC")
        {
            NPCDialogueIcon.SetActive(false);
            _isInTrigger = false;
            
            NPCToInteractWith = null;
        }
    }
}
