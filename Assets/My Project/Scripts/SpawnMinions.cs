using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMinions : MonoBehaviour
{
    [SerializeField] List<GameObject> minions = new List<GameObject>();
    private bool canSpawn = true;
    private float spawnWait = 1f;
    private float minionSpawnInterval;

    // Update is called once per frame
    void Update()
    {
        if (SaveScript.gameOver)
            Destroy(gameObject);
        if (canSpawn == true && SaveScript.minionsCount <= 20)
        {
            canSpawn = false;
            StartCoroutine("SpawnMinion");
        }
    }

    IEnumerator SpawnMinion()
    {
        yield return new WaitForSeconds(spawnWait);
        int minionIndex = (int)(RandomIndex(0, minions.Count));
        for (int i = 0; i < 3; i++)
        {
            minionSpawnInterval = RandomIndex(2, (minions.Count + 1));
            Instantiate(minions[minionIndex], transform.position, transform.rotation);
            SaveScript.minionsCount += 1;
            yield return new WaitForSeconds(minionSpawnInterval);
        }
        yield return new WaitForSeconds(spawnWait);
        canSpawn = true;
    }

    private float RandomIndex(int from, int to)
    {
        float random = Random.Range(from, to);
        return random;
    }

    private void DestroySpawnLocation()
    {
        Destroy(gameObject);
    }
}
