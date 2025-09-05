using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cArea : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            other.GetComponent<cFood>().setVaccum(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            other.GetComponent<cFood>().setVaccum(false);
        }
    }
}
