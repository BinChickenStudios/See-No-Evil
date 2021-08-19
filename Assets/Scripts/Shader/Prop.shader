//this is the shaders location to import onto models
Shader "Unlit/Prop"
{
	//this is the variables that are adjustable in the inspector
    Properties
    {
		//this is an example of a texture variable "_MainTex" is definition, "Texture" is the name in inspector, 2D is the type of data, default is white
        _MainTex ("Texture", 2D) = "white" {}

    }

	//this is the block that contains the code (there can be multiple, an example is a PC, playstation and a mobile subshader version)
    SubShader
    {
		//this defines how the renderer should render the mesh
        Tags { "RenderType"="Opaque" }
		//this is similar to the level of detail for meshes, alters the quality i guess
        LOD 100

		//this is a pass, also known as the drawcall (think of it like an update loop)
        Pass
        {

			//"CGPROGRAM" is like the initialiser, it basically defines whatever is underneath needs to be done
            CGPROGRAM

			//this is the definition for a vertex (i guess its like setting a variable up) (sorta resembles "public"/"data type"/"data name")
            #pragma vertex vert
			//this is the definition for a fragment (i guess its like setting a variable up) (sorta resembles "public"/"data type"/"data name")
            #pragma fragment frag

			//this include namespace is similar to the using namespace feature, its needed to do certain unity specific functions or access certain unity variables
            #include "UnityCG.cginc"


			//this struct is defined as an object, it contains the data related to objects (mesh)
            struct appdata
            {
				//this stores the vertex of the mesh into a float4 data bucket. the ":" suggests how the data is going to be used (in this case, its local position)
                float4 vertex : POSITION;
				//this stores the UV into a float2 data bucket. the ":" suggests how the data is going to be used (in this case, its texture Coordinates)
                float2 uv : TEXCOORD0;
            };

			//this struct is defined as an object, it contains/stores vertex data to be later converted into fragments
            struct v2f
            {
				//this stores the UV into a float2 data bucket. the ":" suggests how the data is going to be used (in this case, its texture Coordinates)
                float2 uv : TEXCOORD0;
				//this stores the vertex of the mesh into a float 4 data bucket. the ":" suggests how the data is going to be used (in this case, its Screen space position)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

			//this is a function that accesses the appdata vertexes and returns the fragments
            v2f vert (appdata v)
            {
				//a new v2f struct is created (for returning) 
                v2f o; 
				//we are setting the new v2f's vertex values using a function that converts the vertexes in normal space into clip space
                o.vertex = UnityObjectToClipPos(v.vertex);

				//
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				//return the fragments 
                return o;
            }

			//this is a function that accesses the v2f from up top and returns...
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
			//this is the end of the CGPROGRAM which means after this, there is no implemented thing (i guess its like finishing a loop or returning)
            ENDCG
        }
    }
}
