using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float Speed = 1.0f;
    public GameObject Bullet;
    AudioSource audioSource;
    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveAmt = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float moveAmt2 = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
        Vector3 moveVector = Vector3.right * moveAmt + Vector3.up * moveAmt2;

        transform.Translate(moveVector);
        if (Input.GetKeyDown(KeyCode.Space)) { audioSource.Play(); Instantiate(Bullet, transform.position, transform.rotation); }
    }
}
