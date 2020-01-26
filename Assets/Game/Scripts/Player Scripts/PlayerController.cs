using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public CharacterController myController;
    public Collider PlayerTrigger;
    public float moveSpeed = 1.0f;
    public float jumpSpeed = 20.0f;
    public float maxJumpSpeed = 20.0f;
    public float gravityStrength = 15f;
    public float increaseJumpSpeed = 10f;
    private float jumpTimer;
    private float jumpTimerCheckRate = 1.0f;
    public Player_ComboAttackScript playercontroller;

    //ParticleSystem
    public ParticleSystem dust;
    public bool includeChildren = true;

    public float airTime = -2f;

    float VerticalVelocity;

    public bool canJump = true;

    public Transform playerCamera;
    public Transform houseCamera;
    private Transform CurrentTransform;
    
    Vector3 currentMovement;

    Animator anim;
    AudioSource sound;

    public AudioClip run;
    public AudioClip jumpsnd;
    public AudioClip doublejumpsnd;
    public AudioClip jumpland;
    public AudioClip[] coinSound;

    private bool jumplandplayed;

    public bool candouble;

    public float acceleration = 1.2f;

    public Vector3 temporaryVector;

    void Start()
    {
        PlayerTrigger = GetComponent<Collider>();
        anim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
        CurrentTransform = playerCamera;

        jumplandplayed = false;
        candouble = false;

        anim.SetBool("doublejump", false);
        dust = GetComponent<ParticleSystem>();

    }
	
	void Update ()
    {
        Vector3 myVector = new Vector3(0, 0, 0);

        if (canJump == true && playercontroller.isAttacking == false)
        {


            myVector.x += Input.GetAxis("Horizontal") * acceleration;
            temporaryVector.x = Input.GetAxis("Horizontal");
            myVector.z += Input.GetAxis("Vertical") * acceleration;
            temporaryVector.z = Input.GetAxis("Vertical");
            myVector = Vector3.ClampMagnitude(myVector, 3.0f);
            myVector = myVector * moveSpeed * Time.deltaTime;
            Quaternion inputRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(CurrentTransform.forward, Vector3.up));
            myVector = inputRotation * myVector;

        }
        else if (canJump == false && playercontroller.isAttacking == false && candouble == true)
        {
            myVector.x = temporaryVector.x * acceleration;
            myVector.x += 0.6f * Input.GetAxis("Horizontal");
            myVector.z = temporaryVector.z * acceleration; 
            myVector.z += 0.6f * Input.GetAxis("Vertical"); 
            myVector = Vector3.ClampMagnitude(myVector, 3.0f);
            myVector = myVector * moveSpeed * Time.deltaTime;
            Quaternion inputRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(CurrentTransform.forward, Vector3.up));
            myVector = inputRotation * myVector;

        }
        else if (canJump == false && playercontroller.isAttacking == false && candouble == false)
        {
            myVector.x += Input.GetAxis("Horizontal") * acceleration;          
            myVector.z += Input.GetAxis("Vertical") * acceleration;
            myVector = Vector3.ClampMagnitude(myVector, 3.0f);
            myVector = myVector * moveSpeed * Time.deltaTime;
            Quaternion inputRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(CurrentTransform.forward, Vector3.up));
            myVector = inputRotation * myVector;

        }




        if (myVector.x != 0 || myVector.z != 0)
        {
            if(canJump)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(myVector), 0.3f);
            }
            else if(!canJump)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(myVector), 0.09f);
            }
        
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("fall"))
            {
                moveSpeed = 9f;
            }
        else
            {
                moveSpeed = 15f;
            }
            anim.SetBool("running", true);       

        }

        else
        {
            anim.SetBool("running", false);
        }



        if (Input.GetButtonDown("Jump") && canJump)
        {

            VerticalVelocity += jumpSpeed;

            anim.SetBool("jump", true);
            sound.PlayOneShot(jumpsnd);
            jumplandplayed = false;
            canJump = false;
            candouble = true;
            dust.Play(includeChildren);
        }

        if (Input.GetButtonDown("Jump") && candouble &&  jumpTimer > 0.3f )
        {
            VerticalVelocity = 0;
            VerticalVelocity += jumpSpeed ;
            anim.SetBool("doublejump", true);
            sound.PlayOneShot(doublejumpsnd);
            candouble = false;
            jumpTimer = 0;
            dust.Play(includeChildren);
        }


        VerticalVelocity -= gravityStrength * Time.deltaTime;
        myVector.y = VerticalVelocity * Time.deltaTime;

        //moves the character
        CollisionFlags flags = myController.Move(myVector);

        if((flags & CollisionFlags.Below) != 0)
        {
            canJump = true;
            candouble = false;
            VerticalVelocity = -3f;
            anim.SetBool("jump", false);
            anim.SetBool("grounded", true);
            anim.SetBool("doublejump", false);
            jumpTimer = 0;

            if(!jumplandplayed)
            {
                sound.PlayOneShot(jumpland);
                jumplandplayed = true; 
            }
        }
        else if ((flags & CollisionFlags.Sides) != 0)
            {
                canJump = false;
            }
        else
        {
            canJump = false;
            jumpTimer += Time.deltaTime;
            anim.SetBool("grounded", false);
        }

        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            Debug.Log("Triggered by: " + other.gameObject.tag);
            sound.PlayOneShot(coinSound[Random.Range(0, coinSound.Length)]);
            //other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("entrance"))
        {
            Debug.Log("entered by: " + other.gameObject.tag);
            CurrentTransform = houseCamera;
            GameObject.Find("PlayerCam").GetComponent<Camera>().enabled = false;

        }

        if (other.gameObject.CompareTag("exit"))
        {
            Debug.Log("exit by: " + other.gameObject.tag);
            CurrentTransform = playerCamera;
            GameObject.Find("PlayerCam").GetComponent<Camera>().enabled = true;
        }

        if (other.gameObject.CompareTag("deathzone"))
        {
            Debug.Log("respawn");
            SceneManager.LoadScene(1);
        }

    }
    
}
