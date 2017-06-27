using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {
    public float SPeed = 8.0f;
    public GameObject explosion;
    GameObject temp;
    AudioSource audioSource;
    Queue queue;
    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        queue = new Queue();
    }

// Update is called once per frame
void Update () {
        float moveAmt = SPeed * Time.deltaTime;
        transform.Translate(Vector3.back * moveAmt);
        if (transform.position.z < -7.0f) InitPosition();

        if (queue.Count > 1) Destroy((UnityEngine.Object)queue.Dequeue());
    }

    void InitPosition()
    {
        transform.position = new Vector3(Random.Range(-10.0f, 10.0f),Random.Range(-10.0f,10.0f),35.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet"){
            Main.Score += 100;
            audioSource.Play();
            temp = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
            queue.Enqueue(temp);
            Destroy(explosion);
            InitPosition();
        }
    }
}
