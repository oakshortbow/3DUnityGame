Shader "Custom/OpaqueGlowTwoRanges"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _OffsetColor("OffsetColor", Color) = (1,1,1,1) 
        _Offset("Offset", Range(0, 100)) = 0 
        _Light("Lighting Power", Range(0, 10)) = 1.4
        _Ambient ("Ambient Power", Range (0,6)) = 0.8	
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows
        //#pragma surface surf NegativeLambert fullforwardshadows alpha:opaque
        

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Ambient;
        half _Light;
        half _Offset;
        fixed4 _Color;
	    fixed4 _OffsetColor;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)
 

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
	     fixed4 c = _Color * abs(((_Offset - 100) /100)) + _OffsetColor * (_Offset / 100) ;
             o.Albedo = c.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
