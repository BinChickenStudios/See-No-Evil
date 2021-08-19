public class Root_Vision : VR_Root
{
    //at the beginning of the game
    private void Start()
    {
        //subscribe to the onVisionEnabled event (by calling the function "Reaction")
        VR_Vision.instance.onVisionEnabled += VisionReact;
        //subscribe to the onVisionDisabled event (by calling the function "UnReact")
        VR_Vision.instance.onVisionDisabled += VisionReact;
    }

    //a function which calls a reaction (via the vision)
    private void VisionReact()
    {
        //set the allow react to the vr vision value
        rootRequirement = VR_Vision.instance.Vision;
        //apply a reaction (checks whether to enable or disable said reaction)
        RequestReaction();
    }
}
