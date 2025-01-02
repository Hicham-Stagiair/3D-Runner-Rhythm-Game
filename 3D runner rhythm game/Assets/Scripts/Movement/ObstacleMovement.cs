using System;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speedModifier = 0f;
    public float BeatsShownInAdvance;
    public float beatOfThisNote;
    public float songPosInBeats;
    
    public Vector3 startPos;
    public Vector3 endPos;

    private void Start()
    {
        startPos = transform.position;
        endPos = new Vector3(transform.position.x, transform.position.y, -5f);
    }

    // Update is called once per frame
    void Update()
    {
        float t = (BeatsShownInAdvance - (beatOfThisNote - songPosInBeats) / BeatsShownInAdvance) * speedModifier;
        Debug.Log(t);
        //t = Mathf.Clamp01(t); // Ensure t is between 0 and 1
        transform.position = Vector3.Lerp(startPos, endPos, (float)t);
        
        if(transform.position == endPos)
        {
            //Debug.Log("reached end");
            Destroy(gameObject);
        }
    }
}
