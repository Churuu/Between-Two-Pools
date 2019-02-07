using UnityEngine;

public class TrackPlayer : MonoBehaviour
{//Påbörjad av Jonas Thunberg 2019-02-04

    [SerializeField] private GameObject player;
    private Camera camera;
    // Use this for initialization
    void Start()
    {
        camera = Camera.main;
    }
    private void Update()
    {
        if (player != null)
        {
            Vector3 v3 = GameObject.FindGameObjectWithTag("Player").transform.position;
            v3 = new Vector3(v3.x, v3.y, camera.transform.position.z);

            camera.transform.position = v3;
        }
    }

}
