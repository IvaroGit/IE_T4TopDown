using System;
using UnityEngine;

public class AH_HandleDoor : MonoBehaviour
{
    [SerializeField]
    private GameObject nextRoom;
    [SerializeField] 
    private float roomHeight;

    private GameObject _player;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        Instantiate(nextRoom, new Vector2(0, roomHeight), nextRoom.transform.rotation);
        /*
         Instantiate new room at (x, y + 8) with transform.rotation + offset to mirror
         
         */
    }
}
