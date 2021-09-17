using UnityEngine;

public class FlameThrowerHandling : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Explodable")
            other.gameObject.SendMessage("Explode");
        else if (other.transform.tag == "Flesh")
            other.gameObject.SendMessage("BurnDeath");
        else if (other.transform.tag == "SpawnPoint")
            other.gameObject.SendMessage("DestroySpawnLocation");
        else if (other.gameObject.tag == "Boss")
        {
        	SaveScript.bossHealth -= 0.5f;
        	SaveScript.score += 1.0f;
        	other.gameObject.SendMessage("Hit");
        }
    }
}
