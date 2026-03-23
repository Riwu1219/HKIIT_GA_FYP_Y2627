using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct MeteorISystemScript : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        foreach (var seed in SystemAPI.Query<RefRW<MeteorRandomSeed>>())
        {
            seed.ValueRW.Value = (uint)UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        }
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float dt = SystemAPI.Time.DeltaTime;

        foreach ((RefRW<LocalTransform> lt,
                  RefRO<MeteorRotation> rot,
                  RefRW<MeteorRandomSeed> seed)
                 in SystemAPI.Query<RefRW<LocalTransform>, RefRO<MeteorRotation>, RefRW<MeteorRandomSeed>>())
        {
            var rng = new Random(seed.ValueRO.Value);

            //Hardcoded speed range for less work for Burst
            float minSpeed = 1f;
            float maxSpeed = 3f;

            float rx = rng.NextFloat(minSpeed, maxSpeed);
            float ry = rng.NextFloat(minSpeed, maxSpeed);
            float rz = rng.NextFloat(minSpeed, maxSpeed);

            seed.ValueRW.Value = rng.NextUInt();

            quaternion q = lt.ValueRO.Rotation;
            q = math.mul(q, quaternion.Euler(math.radians(new float3(rx * rot.ValueRO.rotateSpeed.x * dt, 0, 0))));
            q = math.mul(q, quaternion.Euler(math.radians(new float3(0, ry * rot.ValueRO.rotateSpeed.y * dt, 0))));
            q = math.mul(q, quaternion.Euler(math.radians(new float3(0, 0, rz * rot.ValueRO.rotateSpeed.z * dt))));

            lt.ValueRW = new LocalTransform
            {
                Position = lt.ValueRO.Position,
                Rotation = q,
                Scale = lt.ValueRO.Scale
            };
        }
    }
}