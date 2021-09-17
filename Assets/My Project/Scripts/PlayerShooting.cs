using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject crossHair;
    [SerializeField] GameObject stoneImpact;
    [SerializeField] GameObject metalImpact;
    [SerializeField] GameObject fleshImpact;
    [SerializeField] AudioClip singleShotSound;
    [SerializeField] float rapidWait = 0.5f;
    [SerializeField] AudioClip rapidShotsSound;
    [SerializeField] ParticleSystem grenadeTrailParticleSystem;
    [SerializeField] GameObject grenadeExplosion;
    [SerializeField] AudioClip grenadeSound;
    [SerializeField] ParticleSystem flameThrowerParticleSystem;
    [SerializeField] AudioClip flameThrowerSound;
    [SerializeField] AudioClip pickupSound;

    private AudioSource PlayerAudio;
    private RaycastHit hit;
    private Animator anim;
    private bool isAiming = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        PlayerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!SaveScript.gameOver)
        {
            PlayerAiming();
            if (isAiming)
            {
                Shooting();
            }
        }
    }

    private void MoveCrossHair() {
        crossHair.transform.position = Input.mousePosition;
    }

    private void TriggerAudio()
    {
        if (SaveScript.weaponID == 2)
        {
            PlayerAudio.clip = rapidShotsSound;
        }
        else
        {
            PlayerAudio.clip = singleShotSound;
        }
    }

    private void PlayerAiming()
    {
        MoveCrossHair();
        if (Input.GetKey(KeyCode.Mouse1))
        {
            anim.SetBool("aim", true);
            crossHair.gameObject.SetActive(true);
            isAiming = true;
        }
        else
        {
            anim.SetBool("aim", false);
            crossHair.gameObject.SetActive(false);
            isAiming = false;
        }
    }

    private void ShootingEffects()
    {
        TriggerAudio();
        Hits();
        PlayerAudio.Play();
        SaveScript.ammoAmnt -= 1;
    }

    private void RapidShooting()
    {
        if (Input.GetMouseButton(0) && Time.time >= rapidWait)
        {
            rapidWait = Time.time + 1 / 15.0f;
            ShootingEffects();
        }
    }

    private void SingleShot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootingEffects();
            SaveScript.ammoAmnt -= 1;
        }
    }

    private void GrenadeShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GrenadeHits();
            grenadeTrailParticleSystem.Play();
            PlayerAudio.clip = grenadeSound;
            PlayerAudio.Play();
            SaveScript.ammoAmnt -= 1;
        }
        else
        {
            if (grenadeTrailParticleSystem.isPlaying)
            {
                grenadeTrailParticleSystem.Stop();
            }
        }
    }

    private void FlameThrowerShoting()
    {
        if (Input.GetMouseButton(0))
        {
            flameThrowerParticleSystem.Play();
            PlayerAudio.clip = flameThrowerSound;
            PlayerAudio.pitch = 0.1f;
            PlayerAudio.loop = true;
            SaveScript.ammoAmnt -= (Mathf.CeilToInt(1 * Time.deltaTime));
            if (!PlayerAudio.isPlaying)
                PlayerAudio.Play();
        }
        else
        {
            if (flameThrowerParticleSystem.isPlaying)
            {
                flameThrowerParticleSystem.Stop();
            }
            PlayerAudio.pitch = 1.0f;
            PlayerAudio.loop = false;
            PlayerAudio.Stop();
        }
        if (SaveScript.ammoAmnt <= 0)
        {
            if (flameThrowerParticleSystem.isPlaying)
            {
                flameThrowerParticleSystem.Stop();
            }
            PlayerAudio.pitch = 1.0f;
            PlayerAudio.loop = false;
            PlayerAudio.Stop();
        }
    }

    private void Shooting()
    {
        if (SaveScript.weaponID == 1)
        {
            SingleShot();
        } 
        else if (SaveScript.weaponID == 2) {
            RapidShooting();
        } 
        else if (SaveScript.weaponID == 3)
        {
            GrenadeShooting();
        }
        else if (SaveScript.weaponID == 4)
        {
            FlameThrowerShoting();
        }
    }

    private void Hits()
    {
        Ray ray = Camera.main.ScreenPointToRay(crossHair.transform.position);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.transform.tag == "Wall")
                Instantiate(stoneImpact, hit.point, Quaternion.LookRotation(hit.normal));
            else if (hit.transform.tag == "Metal")
                Instantiate(metalImpact, hit.point, Quaternion.LookRotation(hit.normal));
            else if (hit.transform.tag == "Flesh")
            {
                Instantiate(fleshImpact, hit.point, Quaternion.LookRotation(hit.normal));
                hit.transform.SendMessage("NormalDeath");
            }
            else if (hit.transform.tag == "Boss")
            {
                Instantiate(fleshImpact, hit.point, Quaternion.LookRotation(hit.normal));
                hit.transform.SendMessage("Hit");
                if (SaveScript.weaponID == 2)
                {
                    SaveScript.score += 1.0f;
                    SaveScript.bossHealth -= 0.7f;
                }
                else
                {
                    SaveScript.score += 2.0f;
                    SaveScript.bossHealth -= 1.0f;
                }
            }
            else if (hit.transform.tag == "Explodable")
                hit.transform.SendMessage("Explode");
        }
    }

    private void GrenadeHits()
    {
        Ray ray = Camera.main.ScreenPointToRay(crossHair.transform.position);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.transform.tag == "Wall")
                Instantiate(grenadeExplosion, hit.point, Quaternion.LookRotation(hit.normal));
            else if (hit.transform.tag == "Metal")
                Instantiate(grenadeExplosion, hit.point, Quaternion.LookRotation(hit.normal));
            else if (hit.transform.tag == "Flesh")
            {
                Instantiate(fleshImpact, (hit.point + hit.normal) * 0.5f, Quaternion.LookRotation(hit.normal));
                hit.transform.SendMessage("NormalDeath");
            }
            else if (hit.transform.tag == "Boss")
            {
                Instantiate(fleshImpact, hit.point, Quaternion.LookRotation(hit.normal));
                hit.transform.SendMessage("Hit");
                SaveScript.bossHealth -= 20.0f;
                SaveScript.score += 10.0f;
            }
            else if (hit.transform.tag == "Explodable")
                hit.transform.SendMessage("Explode");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            PlayerAudio.clip = pickupSound;
            PlayerAudio.Play();
            other.gameObject.SendMessage("ChangeWeapon");
        }
    }
}
