using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAttack : MonoBehaviour
{
    [SerializeField] int damage = 1;
    private AudioSource minionAudio;
    // Start is called before the first frame update
    void Start()
    {
        minionAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SaveScript.health -= damage;
            other.transform.gameObject.SendMessage("HitAttack");
            if (minionAudio.isPlaying)
                minionAudio.Stop();
            minionAudio.Play();
        }
    }
}
