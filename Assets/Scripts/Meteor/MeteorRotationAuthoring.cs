using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class MeteorRotationAuthoring : MonoBehaviour
{
    public float3 rotateSpeed;

    private class Baker : Baker<MeteorRotationAuthoring>
    {
        public override void Bake(MeteorRotationAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new MeteorRotation
            {
                rotateSpeed = authoring.rotateSpeed
            });
        }
    }
}

public struct MeteorRotation : IComponentData
{
    public float3 rotateSpeed;
}

public struct MeteorRandomSeed : IComponentData
{
    public uint Value;
}
