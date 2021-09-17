using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    [SerializeField] float DestroyTime = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, DestroyTime);
    }
}
