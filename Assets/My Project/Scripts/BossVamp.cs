using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVamp : MonoBehaviour
{
	[SerializeField] GameObject player;
	[SerializeField] float rotationSpeed = 2.5f;
    [SerializeField] List<Transform> spawnLocations = new List<Transform>();
    [SerializeField] GameObject bossPrefab;
    [SerializeField] Transform orbSpawnLocation;
    [SerializeField] GameObject bossOrb;
    [SerializeField] AudioClip spawnSound;
    [SerializeField] AudioClip attackSound;
    [SerializeField] AudioClip hitSound;

    private bool isHit = false;
    private float attackInterval = 6.0f;
    private bool isAttacking = false;
    private float spawnWait = 1.7f;
    private float initialYPos;
    private Animator anim;
    private Vector3 orbLook;
    private AudioSource bossAudio;
    // Start is called before the first frame update
    void Start()
    {
        initialYPos = transform.position.y;
        anim = GetComponent<Animator>();
        bossAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
    	if (!SaveScript.gameOver)
    	{
    		RotateBoss();
        	if (!isAttacking && !isHit)
        	{
            	isAttacking = true;
            	Attack();
        	}
    	}
    	CheckBossHealth();
    }

    IEnumerator SpawnAfterDuration()
    {
        yield return new WaitForSeconds(spawnWait);
        SpawnToRandomLocation();
    }

    private void CheckBossHealth()
    {
    	if (SaveScript.bossHealth <= 0)
    	{
    		Destroy(gameObject);
    		SaveScript.won = true;
    		SaveScript.gameOver = true;
    	}
    }

    private void RotateBoss()
    {
        Vector3 lookAt = (player.transform.position - transform.position).normalized;
        Vector3 lookDirection = new Vector3(lookAt.x, 0, lookAt.z);
        Quaternion posRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, posRotation, (Time.deltaTime * rotationSpeed));
    }

    IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(attackInterval);
        isAttacking = false;
    }

    public void SpawnOrb()
    {
        Instantiate(bossOrb, orbSpawnLocation.position, orbSpawnLocation.rotation);
    }

    private void Attack()
    {
        anim.SetTrigger("Bite");
        StartCoroutine("AttackTimer");
        bossAudio.clip = attackSound;
        bossAudio.Play();
    }

    private void SpawnToRandomLocation()
    {
        int randomIndex = Random.Range(0, spawnLocations.Count);
        Transform spawnPos = spawnLocations[randomIndex];
        Vector3 newSpawnPos = new Vector3(spawnPos.position.x, initialYPos, spawnPos.position.z);
        Instantiate(bossPrefab, newSpawnPos, spawnPos.rotation);
        Destroy(gameObject);
        bossAudio.clip = spawnSound;
        bossAudio.Play();
    }

    private void Hit()
    {
        if (!isHit)
        {
            isHit = true;
            anim.SetTrigger("Spin");
            StartCoroutine("SpawnAfterDuration");
            bossAudio.clip = hitSound;
            bossAudio.Play();
        }
        // SpawnToRandomLocation();
    }
}
