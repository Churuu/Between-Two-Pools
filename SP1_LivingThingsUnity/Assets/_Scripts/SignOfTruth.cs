using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SignOfTruth : MonoBehaviour
{

    public Text signOfTruthText;
    public string otterTruth, sealTruth, frogTruth;


    void OnTriggerEnter2D(Collider2D collider)
    {
        var col = collider.gameObject;

        if (col.CompareTag("Otter"))
        {
            signOfTruthText.text = otterTruth;
        }
        else if (col.CompareTag("Seal"))
        {
            signOfTruthText.text = sealTruth;
        }
        else if (col.CompareTag("Frog"))
        {
            signOfTruthText.text = frogTruth;
        }

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        signOfTruthText.text = "";
    }
}
