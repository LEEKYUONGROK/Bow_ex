Shader "Custom/TripleColorMask" {
	Properties {
		_MainTex ("Base Color Texture", 2D) = "white" {}
		_MaskTex ("Triple Mask Texture", 2D) = "black" {}
		[MaterialToggle] _UseRedChannel ("Use Red Channel", Float) = 1
		_RColor ("Red Channel Color", Color) = (1,1,1,1)
		[MaterialToggle] _UseGreenChannel ("Use Green Channel", Float) = 1
		_GColor ("Green Channel Color", Color) = (1,1,1,1)
		[MaterialToggle] _UseBlueChannel ("Use Blue Channel", Float) = 1
		_BColor ("Blue Channel Color", Color) = (1,1,1,1)
		_Smoothness ("Smoothness Intensity", Range(0,5)) = 1
		_MetalSmooth ("Metallic/Smoothness Texture", 2D) = "white" {}
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
		sampler2D _MaskTex;
		sampler2D _MetalSmooth;

		struct Input {
			float2 uv_MainTex;
		};

		half _Smoothness;
		fixed4 _Color;
		fixed4 _RColor;
		fixed4 _GColor;
		fixed4 _BColor;

		float _UseRedChannel;
		float _UseGreenChannel;
		float _UseBlueChannel;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {

			// Albedo comes from a texture and mask
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			fixed4 cm = tex2D (_MaskTex, IN.uv_MainTex);
			fixed4 sm = tex2D (_MetalSmooth, IN.uv_MainTex);
			o.Albedo = lerp(lerp(lerp(c.rgb, _RColor, cm.r * _UseRedChannel), _GColor, cm.g * _UseGreenChannel), _BColor, cm.b * _UseBlueChannel);
			// Metallic and smoothness
			o.Metallic = sm.r;
			o.Smoothness = _Smoothness * sm.a;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
