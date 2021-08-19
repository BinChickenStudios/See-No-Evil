using UnityEngine;

[System.Serializable]
public class VR_Light 
{
    public Color color = Color.white;
    [Range(1,30)]
    public float range = 10;
    [Range(1f,10f)]
    public float intensity = 1;
    public LayerMask layers;

}
