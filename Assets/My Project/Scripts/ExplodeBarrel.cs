using UnityEngine;

public class ExplodeBarrel : MonoBehaviour
{
    [SerializeField] GameObject ExplosionEffect;

    public void Explode()
    {
        Instantiate(ExplosionEffect, transform.position, ExplosionEffect.transform.rotation);
        Destroy(gameObject);
    }

}
