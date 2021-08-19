using UnityEngine;


public class Root_GetProgression : VR_Root
{
    //checks the progression to request a reaction
    public void CheckToReact()
    {
        //send message is being sent to this function (testing)
        print("Checking Progression");

        //set the root requirement via the GetProgression function
        rootRequirement = GetProgression();
    }

    //progression data (requirements, progressionID)
    [SerializeField] private Root_Progress progress = null;

    //this checks whether the progression was met
    private bool GetProgression()
    {
        //for every key string requirement
        for (int i = 0; i < progress.Requirements.Length; i++)
        {
            //if the vr_progression (player progresion) does not contain the looped requirement
            if (!VR_Progression.instance.progression.Contains(progress.Requirements[i]))
            {
                //return false (this discontinues)
                return false;
            }
        }

        //if all requirements were met, it will return true
        return true;
    }
}
