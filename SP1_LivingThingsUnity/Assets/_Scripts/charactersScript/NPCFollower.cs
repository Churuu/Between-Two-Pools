using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCFollower : MonoBehaviour
{

    public float minDistance;
    public float followSpeed;
    public float meanCommentTimerDelta;
    [Space]
    public Vector2 offset;
    [Space]
    public GameObject target;
    public Text commentText;
    public Image conversationImage;
    [Space]
    public string[] meanComments;

    float timer;


    void Start()
    {
        timer = meanCommentTimerDelta;
    }

    void Update()
    {
        FollowTarget();
        SayMeanComment();
    }

    void FollowTarget()
    {
        if (target == null)
            return;

        float distance = Vector2.Distance(transform.position, target.transform.position);

        Vector2 followPosition = new Vector2(target.transform.position.x + offset.x, target.transform.position.y + offset.y);

        if (distance > minDistance)
            transform.position = Vector2.Lerp(transform.position, followPosition, followSpeed * Time.deltaTime);
    }

    void SayMeanComment()
    {
        if (commentText.text == "")
            conversationImage.enabled = false;
        else
            conversationImage.enabled = true;

        if (timer < Time.time && target != null)
        {
            commentText.text = meanComments[Random.Range(0, meanComments.Length)];
            timer = meanCommentTimerDelta + Time.time;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Otter"))
        {
            target = other.gameObject;
        }
    }

}
