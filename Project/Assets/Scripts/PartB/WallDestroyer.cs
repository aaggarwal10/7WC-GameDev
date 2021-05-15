using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anima;
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wall" && anima.GetCurrentAnimatorStateInfo(0).IsName("Punch"))    
        {
            Destroy(other.gameObject);
        }
    }
}
