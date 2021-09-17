using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiMinion : MonoBehaviour
{
    [SerializeField] float speed = 2.5f;
    [SerializeField] List<AudioClip> minionClips = new List<AudioClip>();
    [SerializeField] AudioClip deathSound;
    [SerializeField] float audioWaitDuration = 3.0f;
    [SerializeField] int minionType = 1;
    public static int minionType_Global;
    public bool isDead = false;

    private Animator anim;
    private GameObject playerTarget;
    private NavMeshAgent nav;
    /*private NavMeshObstacle navObstacle;*/
    private float attackDistance = 1.1f;
    private float targetDistance;
    private AudioSource minionAudio;
    private bool playMinionSound = true;
    private float rotationSpeed = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        minionAudio = GetComponent<AudioSource>();
        minionType_Global = minionType;
        /*navObstacle = GetComponent<NavMeshObstacle>();
        navObstacle.enabled = false;*/
    }

    // Update is called once per frame
    void Update()
    {
        if (!SaveScript.gameOver)
        {
            if (minionType == 2)
            {
                anim.SetLayerWeight(1, 0);
                anim.SetLayerWeight(1, 1);
            }
            if (minionType == 3)
            {
                anim.SetLayerWeight(1, 0);
                anim.SetLayerWeight(2, 2);
            }
            if (!isDead)
            {
                AttackTarget();
                if (!nav.isStopped)
                {
                    nav.speed = speed;
                    nav.SetDestination(playerTarget.transform.position);
                }
                PlayMinionSounds();
            }
        }
        else
        {
            nav.isStopped = true;
            Destroy(gameObject);
        }
    }

    private void AttackTarget()
    {
        targetDistance = Vector3.Distance(playerTarget.transform.position, transform.position);
        if (targetDistance < attackDistance)
        {
            anim.SetBool("Attack", true);
            nav.isStopped = true;
            Vector3 lookAt = (playerTarget.transform.position - transform.position).normalized;
            Quaternion posRotation = Quaternion.LookRotation(new Vector3(lookAt.x, 0, lookAt.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, posRotation, (Time.deltaTime * rotationSpeed));
            /*navObstacle.enabled = true;*/
        }
        else if (targetDistance > (attackDistance + 1))
        {
            anim.SetBool("Attack", false);
            nav.isStopped = false;
            /*navObstacle.enabled = false;*/
        }
    }

    private int RandomSoundClipIndex()
    {
        int randomIndex = Random.Range(0, minionClips.Count);
        return randomIndex;
    }

    IEnumerator StopAudio()
    {
        yield return new WaitForSeconds(audioWaitDuration);
        minionAudio.Stop();
        playMinionSound = true;
    }

    private void PlayMinionSounds()
    {
        if (playMinionSound == true && !minionAudio.isPlaying && isDead == false)
        {
            minionAudio.pitch = 1.0f;
            minionAudio.clip = minionClips[RandomSoundClipIndex()];
            minionAudio.Play();
            playMinionSound = false;
            StartCoroutine("StopAudio");
        }
    }

    private void NormalDeath()
    {
        if (!isDead)
        {
            anim.SetTrigger("Death");
            nav.isStopped = true;
            /*if (minionAudio.isPlaying)
                minionAudio.Stop();*/
            minionAudio.clip = deathSound;
            minionAudio.pitch = 0.8f;
            minionAudio.Play();
            SaveScript.score += 5;
            SaveScript.minionsCount -= 1;
        }
        isDead = true;
    }

    private void BurnDeath()
    {
        if (!isDead)
        {
            anim.SetTrigger("Burn");
            nav.isStopped = true;
            /*if (minionAudio.isPlaying)
                minionAudio.Stop();*/
            minionAudio.clip = deathSound;
            minionAudio.pitch = 0.8f;
            minionAudio.Play();
            SaveScript.score += 5;
            SaveScript.minionsCount -= 1;
        }
        isDead = true;
    }
}
