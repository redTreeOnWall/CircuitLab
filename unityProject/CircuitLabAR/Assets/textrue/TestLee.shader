Shader "Custom/TestLee" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_EmTex ("自发光 (RGB)", 2D) = "white" {}

		_bumpTex ("法线 (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
    	_xRcrollingSpeed("xRcrollingSpeed",float)=1
    	_yRcrollingSpeed("yRcrollingSpeed",float)=1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _EmTex;
		sampler2D _bumpTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
    	float _xRcrollingSpeed;
    	float _yRcrollingSpeed;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandard o) {
			/*
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
			*/
		float2 sourceUv = IN.uv_MainTex;
        float xRcrollingSpeed = _xRcrollingSpeed*_Time.y;
        float yRcrollingSpeed = _yRcrollingSpeed*_Time.y;

        sourceUv += float2(xRcrollingSpeed,yRcrollingSpeed);

        float4 c = tex2D(_MainTex,IN.uv_MainTex) * _Color;
		o.Metallic = _Metallic;
		float4 em = tex2D(_EmTex,sourceUv);
		float4 Bump = tex2D(_bumpTex,IN.uv_MainTex);
		o.Emission = em;
		o.Normal = Bump;
		o.Smoothness = _Glossiness;
        o.Albedo = c.rgb;
        o.Alpha = c.a;

		}
		ENDCG
	}
	FallBack "Diffuse"
}
