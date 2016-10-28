using UnityEngine;
using System.Collections;

public class ButtonDetector : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision");
        if (other.tag == "Player")
        {
            this.gameObject.GetComponentInParent<ButtonParent>().Press();
        }
    }
}
