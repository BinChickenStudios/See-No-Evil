using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reation_MoveToPlayer : MonoBehaviour
{

    [SerializeField] Transform start;

    [SerializeField] Transform end;

    [SerializeField] GameObject MovingObject;

    [SerializeField] private float speed = 5;

    public bool react = false;

    private void Start()
    {
        MovingObject.transform.position = start.position;

        MovingObject.SetActive(false);

        react = false;
    }

    public void React()  //When Player reacts, an object will move
    {
        MovingObject.transform.position = start.position;

        MovingObject.SetActive(true);

        MovingObject.transform.LookAt(end);

        react = true;

    }

    void Update()
    {
        if (react == true)
        {
            // Move our position a step closer to the target.
            float step = speed * Time.deltaTime; // calculate distance to move
            MovingObject.transform.position = Vector3.MoveTowards(MovingObject.transform.position, end.position, step);

            // Check if the position of the moving object and end position are approximately equal.
            if (Vector3.Distance(MovingObject.transform.position, end.position) < 0.1f)
            {
                // make the object disappear
                MovingObject.SetActive(false);

                react = false;
            }

        }
    }


}
