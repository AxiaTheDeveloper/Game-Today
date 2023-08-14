using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent_ObjectInTheWay : MonoBehaviour
{
    private List<Object_InTheWay> object_InTheWay_List, object_Transparent_List;
    [SerializeField]private Transform player;
    private Transform cam;
    private void Awake()
    {
        object_InTheWay_List = new List<Object_InTheWay>();
        object_Transparent_List = new List<Object_InTheWay>();
        
        cam = gameObject.transform;
    }
    private void Start() {
        DebugError();
    }
    private void DebugError()
    {
        if(object_InTheWay_List == null) Debug.LogError("List object_InTheWay_List di Transparent_ObjectInTheWay belum di new List nama" + gameObject.name);
        if(object_Transparent_List == null) Debug.LogError("List object_Transparent_List di Transparent_ObjectInTheWay belum di new List nama" + gameObject.name);
        if(!cam) Debug.LogError("Transform cam masih kosong di Transparent_ObjectInTheWay nama" + gameObject.name);
        if(!player) Debug.LogError("Transform player masih kosong di Transparent_ObjectInTheWay nama" + gameObject.name);
    }
    
    private void Update()
    {
        GetAllObjectInTheWay();
        Transparent();
        Solidify();
    }
    private void GetAllObjectInTheWay()
    {
        object_InTheWay_List.Clear();
        // Debug.Log(player.gameObject);
        float playerTocamDistance = Vector3.Magnitude(cam.position - player.position);
        Ray forward = new Ray(cam.position, player.position - cam.position);
        Ray backward = new Ray(player.position, cam.position - player.position);
        RaycastHit[] hit_Forward =  Physics.RaycastAll(forward, playerTocamDistance);
        RaycastHit[] hit_Backward =  Physics.RaycastAll(backward, playerTocamDistance);
        
        foreach(RaycastHit rayHit in hit_Forward)
        {
            if(rayHit.collider.gameObject.TryGetComponent(out Object_InTheWay hit))
            {
                // Debug.Log(hit);
                if(!object_InTheWay_List.Contains(hit)) object_InTheWay_List.Add(hit);
                
            }
        }
        foreach(RaycastHit rayHit in hit_Backward)
        {
            if(rayHit.collider.gameObject.TryGetComponent(out Object_InTheWay hit))
            {
                // Debug.Log(hit);
                if(!object_InTheWay_List.Contains(hit)) object_InTheWay_List.Add(hit);
            }
        }

    }
    private void Transparent()
    {
        foreach(Object_InTheWay inTheWay in object_InTheWay_List)
        {
            if(!object_Transparent_List.Contains(inTheWay))
            {
                inTheWay.InTheWay();
                object_Transparent_List.Add(inTheWay);
            }
        }
    }
    private void Solidify()
    {
        List<Object_InTheWay> transparentCopy = new List<Object_InTheWay>(object_Transparent_List);
        foreach(Object_InTheWay wasInTheWay in transparentCopy)
        {
            if(!object_InTheWay_List.Contains(wasInTheWay))
            {
                wasInTheWay.NotInTheWay();
                object_Transparent_List.Remove(wasInTheWay);
            }
        }
        
    }
}
