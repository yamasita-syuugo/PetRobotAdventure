using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombHit : MonoBehaviour
{
    public GameObject Explosion;
    public AudioSource ExplosionSource;
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
        if(collision.tag == "Player")
        {
            GameObject tmp = Instantiate<GameObject>(Explosion);
            tmp.transform.position = this.transform.position;

            ScoreManager.EnemyBomPointAdd();

            ExplosionSource.Play();
            Destroy(this.GameObject());
        }
        else if(collision.tag == "Bullet")
        {
            GameObject tmp = Instantiate<GameObject>(Explosion);
            tmp.transform.position = this.transform.position;

            ExplosionSource.Play();
            ScoreManager.DestroyPointAdd();
            GameObject.FindAnyObjectByType<FlagCreate>().FlagSpaun();

            Destroy(collision.GameObject());
            Destroy(this.GameObject());
        }
        else if(collision.tag == "Enemy")
        {
            GameObject tmp = Instantiate<GameObject>(Explosion);
            tmp.transform.position = this.transform.position;

            ExplosionSource.Play();
            ScoreManager.EnemyBomPointAdd();

            Destroy(this.GameObject());
        }
    }
}