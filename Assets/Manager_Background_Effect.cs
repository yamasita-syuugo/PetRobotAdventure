using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEffectType
{
    none,

    cloud,
    rainClouds,

    blackOut,

    [InspectorName("")] max,
}

public class Manager_Background_Effect : MonoBehaviour
{
    [SerializeField]
    eEffectType effectType;
    public eEffectType EffectType { get {  return effectType; } set {  effectType = value; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
