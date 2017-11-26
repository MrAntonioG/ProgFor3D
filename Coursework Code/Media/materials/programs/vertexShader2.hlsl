struct VS_OUTPUT {
   float4 pos: POSITION;
   float2 texCoord0: TEXCOORD0;
   float3 normal:TEXCOORD1;
   float3 wPos:TEXCOORD2;
};
float time;
float4x4 viewProj;

VS_OUTPUT main( float4 Pos: POSITION, float3 normal: NORMAL, float2 texCoord0: TEXCOORD0 )
{
   VS_OUTPUT Out;
   
   float wave = (cos(20*Pos.y+3*time)+sin(20*Pos.y-Pos.z+5*time));
   
   Pos.x += -wave *1.11;
   
   
   Out.normal = normal;
   
   Out.pos = mul(viewProj, Pos);
   Out.wPos = Out.pos;
   Out.texCoord0 = texCoord0;
   
   return Out;
}
