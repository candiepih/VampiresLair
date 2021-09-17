using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5.0f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.forward * Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SaveScript.health -= 10;
            other.gameObject.SendMessage("Impact");
        }
    }
}
