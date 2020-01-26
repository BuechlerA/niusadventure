using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCoins : MonoBehaviour
{
    public GameObject panda;
    public AudioSource sound;
    public AudioClip[] coinSound;
    public Collider player;
    [SerializeField]
    private Stats coinCounter;

    private void Awake()
    {
        coinCounter.initialize();
        GetComponentInChildren<AudioSource>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            Console.WriteLine("Hit the coin");
            coinCounter.CurrentVal +=1;
            other.gameObject.SetActive(false);
            sound.PlayOneShot(coinSound[UnityEngine.Random.Range(0, coinSound.Length)]);
        }
    }
    
}

