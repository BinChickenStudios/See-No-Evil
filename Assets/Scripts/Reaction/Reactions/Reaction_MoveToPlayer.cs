using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaction_MoveToPlayer : MonoBehaviour
{

    [SerializeField] Transform start;

    [SerializeField] Transform end;

    [SerializeField] GameObject MovingObject;

    [SerializeField] private float speed;

    [SerializeField] private Vector3 OgSize;

    [SerializeField] private Vector3 Growth;

    [SerializeField] private float TimerLength;

    [SerializeField] private float ObjectPlayerDistance;

    [SerializeField] private Animator masterAnim;
    [SerializeField] private string animParameterID;   //Once the object is at the player, it will begin to fade away
    [SerializeField] private int animIntID;
    [SerializeField] private int animIntIDEnd;

    private void Start() //making sure the moving object is turned off and at start
    {
        MovingObject.transform.position = start.position;

        MovingObject.SetActive(false);


    }

    public void React()  //When Player reacts, an object will move
    {
        MovingObject.transform.position = start.position;

        MovingObject.SetActive(true);

        MovingObject.transform.localScale = OgSize;

        StartCoroutine("MoveObject");

    }

    IEnumerator MoveObject()
    {
        float t = 0;


        //REDTACTED while (Vector3.Distance(MovingObject.transform.position, end.position) > ObjectPlayerDistance) //Once the moving object is approximately equal to its destination

        while (t < TimerLength)
        {
            // Move our position a step closer to the target.
            float step = speed * Time.deltaTime; // calculate distance to move

            if ((Vector3.Distance(MovingObject.transform.position, end.position) > ObjectPlayerDistance))

            { MovingObject.transform.position = Vector3.MoveTowards(MovingObject.transform.position, end.position, step); }
                

            MovingObject.transform.localScale += Growth; //The object will grow as it gets closer to player

            MovingObject.transform.LookAt(end); //constantly looking at player

            t += Time.deltaTime;

            yield return null;

        }

        masterAnim.SetInteger(animParameterID, animIntID);

        yield return new WaitForSeconds(1);

        

        MovingObject.SetActive(false); //the object will disappear

        masterAnim.SetInteger(animParameterID, animIntIDEnd);

    }


}
