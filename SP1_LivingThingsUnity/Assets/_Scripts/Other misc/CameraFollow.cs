using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform objectToFollow;
    [SerializeField] private Transform cameraObject;
    [SerializeField] private float followSpeed;

    void Start()
    {

    }

    void Update()
    {
        SmoothFollow();
    }

    public IEnumerator SwitchActiveCamera(GameObject camera)
    {
        FindObjectOfType<CharacterIndictor>().mainCamera = camera.GetComponent<Camera>();
        camera.SetActive(true);
        cameraObject.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);

        camera.SetActive(false);
        FindObjectOfType<CharacterIndictor>().mainCamera = cameraObject.GetComponent<Camera>();
        cameraObject.gameObject.SetActive(true);

    }

    void SmoothFollow()
    {
        Vector3 cameraPos = new Vector3(cameraObject.position.x, cameraObject.position.y, -10);
        Vector3 objectToFollowPos = new Vector3(objectToFollow.position.x, objectToFollow.position.y, -10);

        cameraObject.position = Vector3.Lerp(cameraPos, objectToFollowPos, followSpeed * Time.deltaTime);
    }
}
