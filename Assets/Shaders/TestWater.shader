Shader "Test/TestWater"
{
	Properties
	{
		_MainColor("MainColor",Color) = "white"
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Geometry+1"
			"RenderType" = "Opaque"
		}

		Pass
		{
			Tags
			{
				"LightMode" = "Fowardbase"
			}
			
			
			
		}


	}

	Fallback "diffuse"
}