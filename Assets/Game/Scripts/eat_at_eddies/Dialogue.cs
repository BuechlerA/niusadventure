using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Dialogue : MonoBehaviour
{
    private Text _textComponent;

    public string[] DialogueStrings;

    public float SecondsBetweenCharacters = 0.15f;
    public float CharacterRateMultiplier = 0.5f;

    private bool _isStringBeingRevealed = false;
    public bool _isDialoguePlaying = false;
    private bool _isEndOfDialogue = false;
    private bool _isEndOfCharacter = false;
    private bool _isEndOfConversation = false;
    public bool _canExitDialogue = false; // NPCInteraction script is accessing that variable to set it 'false' again (must be public)

    public GameObject ContinueIcon;
    public GameObject StopIcon;
    public GameObject dialogueBackground;
    public GameObject thePlayer;

    void Start ()
	{
        _textComponent = GetComponent<Text>();
	    _textComponent.text = "";
        
        HideIcons();
	}
	
	
	void Update () 
	{
        if(_isDialoguePlaying)
        {
            thePlayer.GetComponent<PlayerController>().enabled = false;
        }

        if (_isEndOfDialogue && _isEndOfCharacter) // conversation ends
        {
            _isEndOfConversation = true;
        }

        if (_canExitDialogue) // close the dialogue
        {
            StopAllCoroutines();
            _isStringBeingRevealed = false;
            _isDialoguePlaying = false;
            _isEndOfDialogue = false;
            _isEndOfCharacter = false;
            _isEndOfConversation = false;

            dialogueBackground.SetActive(false);
            ContinueIcon.SetActive(false);
            StopIcon.SetActive(false);
            thePlayer.GetComponent<PlayerController>().enabled = true;
            gameObject.SetActive(false);

        }

        if (!_isDialoguePlaying && !_canExitDialogue)
	    {
            _isDialoguePlaying = true;
            StartCoroutine(StartDialogue());
	    }

	}

    private IEnumerator StartDialogue()
    {
     
        int dialogueLength = DialogueStrings.Length;
        int currentDialogueIndex = 0;

        while (currentDialogueIndex < dialogueLength || !_isStringBeingRevealed)
        {

            if (!_isStringBeingRevealed)
            {
                _isStringBeingRevealed = true;

                StartCoroutine(DisplayString(DialogueStrings[currentDialogueIndex++]));
                
                if (currentDialogueIndex >= dialogueLength) //check if its the last dialogue
                {
                    _isEndOfDialogue = true;
                }

            }

            yield return 0;
        }

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Joystick1Button3))
            {
                break;
            }

            yield return 0;
        }

        HideIcons();

        _isDialoguePlaying = false;
    }

    private IEnumerator DisplayString(string stringToDisplay)
    {
        int stringLength = stringToDisplay.Length;
        int currentCharacterIndex = 0;

        dialogueBackground.SetActive(true);
        ContinueIcon.SetActive(true);
        StopIcon.SetActive(true);

        HideIcons();

        _textComponent.text = "";

        while (currentCharacterIndex < stringLength)
        {

            _textComponent.text += stringToDisplay[currentCharacterIndex];
            currentCharacterIndex++;
            _isEndOfCharacter = false;

            if (currentCharacterIndex < stringLength)
            {
                if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Joystick1Button3))
                {
                    yield return new WaitForSeconds(SecondsBetweenCharacters*CharacterRateMultiplier);
                }
                else
                {
                    yield return new WaitForSeconds(SecondsBetweenCharacters);
                }
            }
            else if(currentCharacterIndex >= stringLength)
            {
                _isEndOfCharacter = true;
            }
            else
            {
                break;
            }
        }

        ShowIcon();


        while (true) // stops the 'StartCoroutine' with 'yield return 0;' from skip to the next dialogue, after the last string is printed
        {
            if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Joystick1Button3)) && !_isEndOfConversation) // breaks the while loop and goes to the next dialogue
            {
                break;
            }

            if((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Joystick1Button3)) && _isEndOfConversation) // exit dialogue
            {
                _canExitDialogue = true;
                _isEndOfDialogue = false;
                break;
            }

            yield return 0;
        }

        HideIcons();

        _isStringBeingRevealed = false;
        _textComponent.text = "";
    }

    private void HideIcons()
    {
        ContinueIcon.SetActive(false);
        StopIcon.SetActive(false);
    }

    private void ShowIcon()
    {
        if (_isEndOfDialogue)
        {
            StopIcon.SetActive(true);
            return;
        }

        ContinueIcon.SetActive(true);
    }
}
