using UnityEngine;

public class Reaction_GiveProgression : MonoBehaviour
{
    #region Variables

    //progress information
    [SerializeField] private Root_Progress progress = null;

    //a boolean that checks if progression was already given
    private bool progressionGiven;
    #endregion

    #region Core
    //override react
    public void React()
    {
        //give progression
        GiveProgression();
    }
    #endregion

    #region Details
    //this adds progression to the players progression list
    private void GiveProgression()
    {
        //if there was no progression given already
        if (!progressionGiven)
        {
            //add the progressionID to the players progression list
            VR_Progression.instance.AddProgression(progress.progressionID);
            //set progression given to true
            progressionGiven = true;
            //end here
            return;
        }
        //log a warning to state that a limitReaction should be used here
        Debug.LogWarning("Progression was already given, consider placing a Root_LimitReaction script to the object");
        
    }
    #endregion
}
