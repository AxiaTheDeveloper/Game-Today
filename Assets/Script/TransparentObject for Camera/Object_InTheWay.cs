using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_InTheWay : MonoBehaviour
{
    [SerializeField]private GameObject solid, transparent;

    private void Awake() 
    {
        if(!solid) solid = gameObject.transform.GetChild(0).gameObject;
        if(!transparent) transparent = gameObject.transform.GetChild(1).gameObject;
    }
    private void Start() 
    {
        DebugError();
    }
    private void DebugError()
    {
        if(!solid) Debug.LogError("GameObject solid di Object_InTheWay nama " + gameObject.name + " masih kosong");
        if(!transparent) Debug.LogError("GameObject transparent di Object_InTheWay nama " + gameObject.name + " masih kosong");
    }
    public void NotInTheWay()
    {
        solid.SetActive(true);
        transparent.SetActive(false);
    }
    public void InTheWay()
    {
        solid.SetActive(false);
        transparent.SetActive(true);
    }
}
