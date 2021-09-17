using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
	[SerializeField] GameObject boss;
	[SerializeField] GameObject bossHealthBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
    	if (other.gameObject.tag == "Player")
    	{
    		SaveScript.enteredBossLayer = true;
    		if (boss.activeSelf == false) {
    			boss.SetActive(true);
    			bossHealthBar.SetActive(true);
    		}
    	}
    }
}
