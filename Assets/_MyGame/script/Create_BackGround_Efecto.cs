using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Create_BackGround_Efecto : MonoBehaviour
{
    Manager_Background_Effect manager_Background_Effect;

    [SerializeField]
    GameObject cloudBase;
    [SerializeField]
    GameObject blackOut;

    float spawnPoint;

    const int effectNum = 200;
    GameObject[] effectObject = new GameObject[effectNum];
    // Start is called before the first frame update
    void Start()
    {
        manager_Background_Effect = GameObject.FindWithTag("Manager").GetComponent<Manager_Background_Effect>();

        spawnPoint = UnityEngine.Random.Range(0f, 6.28f);
    }

    // Update is called once per frame
    void Update()
    {
        EfectoSpawn();


    }
    [SerializeField, Range(0, 20f)]
    float spawnDistance;
    [SerializeField, Range(0, 1f)]
    float moveSpeed;
    [SerializeField]
    float spawnTime = 5;
    float time = 0;
    eEffectType oldEffectType = eEffectType.max;
    void EfectoSpawn()
    {
        eEffectType effectType = manager_Background_Effect.GetEffectType();

        if (oldEffectType != effectType)
        {
            oldEffectType = effectType;
            for (int i = 0; i < effectNum; i++) { Destroy(effectObject[i]); }
            spawnPoint = UnityEngine.Random.Range(0f, 6.28f);
            EffectSpawnStart();
        }

        switch (effectType)
        {
            case eEffectType.none: break;
            case eEffectType.cloud:
                if (time < 0)
                {
                    time = spawnTime;
                    GameObject tmp = Instantiate(cloudBase);
                    for (int i = 0; i < effectNum; i++) { if (effectObject[i] == null) { effectObject[i] = tmp; break; } else { continue; } }

                    float x = math.cos(spawnPoint) * spawnDistance;
                    float y = math.sin(spawnPoint) * spawnDistance;
                    float spaceRandom = UnityEngine.Random.Range(-11, 11);
                    float spaceX = math.cos(spawnPoint + 1.57f) * spaceRandom;
                    float spaceY = math.sin(spawnPoint + 1.57f) * spaceRandom;
                    tmp.transform.position = new Vector2(x + spaceX, y + spaceY);
                    tmp.GetComponent<Effect_Move_Cloud>().SetMove(new Vector2(-x * moveSpeed, -y * moveSpeed));
                }
                break;
            case eEffectType.rainClouds:
                if (time < 0)
                {
                    time = spawnTime;
                    GameObject tmp = Instantiate(cloudBase);
                    for (int i = 0; i < effectNum; i++) { if (effectObject[i] == null) { effectObject[i] = tmp; break; } else { continue; } }

                    float x = math.cos(spawnPoint) * spawnDistance;
                    float y = math.sin(spawnPoint) * spawnDistance;
                    float spaceRandom = UnityEngine.Random.Range(-11, 11);
                    float spaceX = math.cos(spawnPoint + 1.57f) * spaceRandom;
                    float spaceY = math.sin(spawnPoint + 1.57f) * spaceRandom;
                    tmp.transform.position = new Vector2(x + spaceX, y + spaceY);
                    tmp.GetComponent<Effect_Move_Cloud>().SetMove(new Vector2(-x * moveSpeed, -y * moveSpeed));

                    tmp.GetComponent<SpriteRenderer>().color = Color.black;
                    tmp.GetComponent<SpriteRenderer>().sortingOrder = 10;
                }
                break;
            case eEffectType.blackOut:
                if (time == 0)
                {
                    GameObject tmp;
                    tmp = Instantiate(cloudBase);
                }
                break;
            default: Debug.Log("error : efectoType : " + effectType.HumanName()); break;
        }
        time -= Time.deltaTime;
    }
    void EffectSpawnStart()
    {
        eEffectType effectType = manager_Background_Effect.GetEffectType();

        switch (effectType)
        {
            case eEffectType.none: break;
            case eEffectType.cloud:
                for (int i = 0; i < 50; i++)
                {
                    GameObject tmp = Instantiate(cloudBase); time = spawnTime;
                    for (int j = 0; j < effectNum; j++) { if (effectObject[j] == null) { effectObject[j] = tmp; break; } else { continue; } }

                    tmp.transform.position = new Vector2(UnityEngine.Random.Range(-11, 11), UnityEngine.Random.Range(-11, 11));

                    float x = math.cos(spawnPoint) * spawnDistance;
                    float y = math.sin(spawnPoint) * spawnDistance;
                    float spaceRandom = UnityEngine.Random.Range(-11, 11);
                    float spaceX = math.cos(spawnPoint + 1.57f) * spaceRandom;
                    float spaceY = math.sin(spawnPoint + 1.57f) * spaceRandom;
                    tmp.GetComponent<Effect_Move_Cloud>().SetMove(new Vector2(-x * moveSpeed, -y * moveSpeed));
                }
                break;
            case eEffectType.rainClouds:
                for (int i = 0; i < 50; i++)
                {
                    GameObject tmp = Instantiate(cloudBase); time = spawnTime;
                    for (int j = 0; j < effectNum; j++) { if (effectObject[j] == null) { effectObject[j] = tmp; break; } else { continue; } }

                    tmp.transform.position = new Vector2(UnityEngine.Random.Range(-11, 11), UnityEngine.Random.Range(-11, 11));

                    float x = math.cos(spawnPoint) * spawnDistance;
                    float y = math.sin(spawnPoint) * spawnDistance;
                    float spaceRandom = UnityEngine.Random.Range(-11, 11);
                    float spaceX = math.cos(spawnPoint + 1.57f) * spaceRandom;
                    float spaceY = math.sin(spawnPoint + 1.57f) * spaceRandom;
                    tmp.GetComponent<Effect_Move_Cloud>().SetMove(new Vector2(-x * moveSpeed, -y * moveSpeed));

                    tmp.GetComponent<SpriteRenderer>().color = Color.black;
                    tmp.GetComponent<SpriteRenderer>().sortingOrder = 10;
                }
                break;
            default: Debug.Log("error : efectoType : " + effectType.HumanName()); break;
        }
    }
}
