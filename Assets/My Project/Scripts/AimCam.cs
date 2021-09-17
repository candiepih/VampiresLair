using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCam : MonoBehaviour
{
    private Animator CamAnim;
    // Start is called before the first frame update
    void Start()
    {
        CamAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        ZoomCam();
    }

    private void ZoomCam()
    {
        if (Input.GetKey(KeyCode.Mouse1))
            CamAnim.SetBool("CamAim", true);
        else if (Input.GetKeyUp(KeyCode.Mouse1))
            CamAnim.SetBool("CamAim", false);
    }
}
