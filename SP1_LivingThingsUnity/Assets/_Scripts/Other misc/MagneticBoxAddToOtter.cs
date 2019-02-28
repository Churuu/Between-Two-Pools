using UnityEngine;

public class MagneticBoxAddToOtter : MonoBehaviour
{
    bool One = true;


    // Update is called once per frame
    void Update()
    {
        if (One)
        {
            if (EventManager.instance.OnStartAddMagneticBox != null)
            {
                Debug.Log("Im In Guys");
                EventManager.instance.OnStartAddMagneticBox(gameObject.GetComponent<Rigidbody2D>());
            }
            One = false;
        }
    }
}
