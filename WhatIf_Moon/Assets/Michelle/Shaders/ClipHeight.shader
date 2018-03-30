Shader "Custom/ClipHeight"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_PlanePos ("World Space Clipping Plane Position", Vector) = (0, 0, 0, 1)
	}
	SubShader {
		Pass {

			//Forward rendering!
			//Light direction in _WorldSpaceLightPos0
			//Light Color in _LightColor0
			Tags {"Lightmode"="ForwardBase"}

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			//Import a ton of helper functions
			#include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc"

			//Pull in the data from Unity's inspector
			sampler2D _MainTex;
			float4 _PlanePos;

			struct appdata {
				float4 vertex : POSITION; //Vertex points from mesh
				float3 normal: NORMAL;
				float uv : TEXCOORD0;
			};

			struct v2f {
				float4 position : SV_POSITION; //Need the SV_ to make it work on DX platforms
				fixed4 diff : COLOR0; //Lighting diffuse color
				float uv : TEXCOORD0;
				float3 worldPos : TEXCOORD1; //I learned today that it's common to use texcoords to smuggle your data
			};

			//Very useful when coupled with clip()
			float distanceToPlane(float3 planePosition, float3 pointInWorld) {
				return pointInWorld.y - planePosition.y;
			}

			//Theclip() function is a godsend
			void clipPlane(float3 worldPos) {
				clip(distanceToPlane(_PlanePos, worldPos));
			}

			//Vertex shader
			v2f vert(appdata i) {
				v2f o;

				//Screen and world position set
				o.position = UnityObjectToClipPos(i.vertex);
				o.worldPos = mul(unity_ObjectToWorld, i.vertex); //can I access this data?

				//Pass the UVs through
				o.uv = i.uv;

				//Get the world normal
				half3 worldNormal = UnityObjectToWorldNormal(i.normal); //i.normal is the normal of the vertex

				//To get standard diffuse lighting, you take the dot between normal and light direction
				//Max between 0 and the value so that the minimum is just no lighting.
				half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));

				//Get the light COLORED
				o.diff = nl * _LightColor0;

				//ShadeSH9 is a part of Unity.cginc, it evaluates ambient using the worldspace normal
				o.diff.rgb += ShadeSH9(half4(worldNormal, 1));

				return o;
			}

			//Fragment shader
			fixed4 frag (v2f i) : SV_Target{
				//If the pixel is below the clipping plane, don't draw it
				clipPlane(i.worldPos);

				fixed4 color = tex2D(_MainTex, i.uv);

				color *= i.diff;

				return color;
			}

			ENDCG
		}
	}
}