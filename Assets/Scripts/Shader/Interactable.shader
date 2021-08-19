//defines the shaders location
Shader "GhostVision/Interactable"
{
	//the container in which code is performed
	SubShader
	{
		//the drawcall which will do the code snippet
		Pass
		{
			//the program that will run the code snippet (i think?)
			CGPROGRAM
			
			//the variable for a vertex (similar to regular variables)
			#pragma vertex vert
			//the variable for a fragment (similar to regular variables)
			#pragma fragment frag

			//a namespace that gives us access to unity helper functions
			#include "UnityCG.cginc"

			//defines what information/data we are accessing from the mesh
			struct appdata 
			{
				//the vertex information (taking in the normal position)
				float4 vertex : POSITION;
			};

			//defines what information/data will be passing the fragment function
			struct v2f 
			{
				//the vertex information (defined as the screen space/vector position)
				float4 vertex : SV_POSITION;
			};

			//takes the appdata information and returns a v2f 
			v2f vert(appdata v) 
			{
				//sets up a new v2f variable
				v2f o;
				//set the new v2fs vertex as the appdata vertex (converted into a clip position)
				o.vertex = UnityObjectToClipPos(v.vertex);
				//return the v2f out
				return o;
			}

			//takes the v2f data and returns a color (in a float4 variable)
			float4 frag(v2f i) : SV_Target
			{
				//return a plain white float4 variable
				return float4(1,1,1,1);
			}

			//the program code snippet ends here
			ENDCG
		}
	}
}
