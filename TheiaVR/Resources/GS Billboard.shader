//This is a modified version of a shader published by smb02dunnal on the Unity forums:
//https://forum.unity3d.com/threads/billboard-geometry-shader.169415/

Shader "Custom/GS Billboard"
{
	Properties
	{
		_PointSize("PointSize", Range(0, 0.2)) = 0.1
	}

	SubShader
	{
		Pass
		{
			Tags{ "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM

			#pragma vertex VS_Main
			#pragma fragment FS_Main
			#include "UnityCG.cginc" 

			// **************************************************************
			// Data structures												*
			// **************************************************************
			struct GS_INPUT
			{
				float4	pos		: POSITION;
				float4	col		: COLOR;
			};

			struct FS_INPUT
			{
				float4	pos		: POSITION;
				float4  col		: COLOR;
			};


			// **************************************************************
			// Vars															*
			// **************************************************************

			float _PointSize;

			// **************************************************************
			// Shader Programs												*
			// **************************************************************

			// Vertex Shader ------------------------------------------------
			GS_INPUT VS_Main(appdata_full v)
			{
				GS_INPUT output = (GS_INPUT)0;

				output.pos = mul(unity_ObjectToWorld, v.vertex);
				output.col = v.color;

				return output;
			}

			// Fragment Shader -----------------------------------------------
			float4 FS_Main(FS_INPUT input) : COLOR
			{
				return input.col;
			}

			ENDCG
		}
	}
}
