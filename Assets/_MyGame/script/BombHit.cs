using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombHit : MonoBehaviour
{
    public GameObject explosion;
    public AudioSource explosionSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ScoreManager.EnemyBomPointAdd();

            Explosion();
        }
        else if (collision.tag == "Attack")
        {
            ScoreManager.DestroyPointAdd();

            Destroy(collision.GameObject());

            GameObject.FindAnyObjectByType<FlagCreate>().FlagSpaun();

            Explosion();
        }
        else if (collision.tag == "Enemy")
        {
            ScoreManager.EnemyBomPointAdd();

            Explosion();
        }
    }

    public void Explosion()
    {
        GameObject tmp = Instantiate<GameObject>(explosion);
        tmp.transform.position = this.transform.position;
        explosionSource.Play();

        Destroy(this.GameObject());
    }
}