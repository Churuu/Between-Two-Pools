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
    public GameObject target;
    public Text commentText;
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
        float distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance > minDistance)
            transform.position = Vector2.Lerp(transform.position, target.transform.position, followSpeed * Time.deltaTime);
    }

    void SayMeanComment()
    {
        if (timer < Time.time)
        {
            commentText.text = meanComments[Random.Range(0, meanComments.Length)];
            timer = meanCommentTimerDelta + Time.time;
        }
    }

}
