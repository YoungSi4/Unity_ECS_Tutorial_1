using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
partial struct DancerUpdateJob : IJobEntity
{
    public float Elapsed;

    void Execute(in Dancer dancer, ref LocalTransform xform)
    {
        var t = dancer.Speed * Elapsed;
        var y = math.abs(math.sin(t)) * 0.1f;
        var bank = math.cos(t) * 0.5f;

        var fwd = xform.Forward();
        var rot = quaternion.AxisAngle(fwd, bank);
        var up = math.mul(rot, math.float3(0, 1, 0));

        xform.Position.y = y;
        xform.Rotation = quaternion.LookRotation(fwd, up);
    }
}

public partial struct DancerSystem : ISystem
{
    //[BurstCompile]
    //public void OnUpdate(ref SystemState state)
    //{
    //    var elapsed = (float)SystemAPI.Time.ElapsedTime;

    //    foreach (var (dancer, xform) in
    //             SystemAPI.Query<RefRO<Dancer>,
    //                             RefRW<LocalTransform>>())
    //    {
    //        var t = dancer.ValueRO.Speed * elapsed;
    //        var y = math.abs(math.sin(t)) * 0.1f;
    //        var bank = math.cos(t) * 0.5f;

    //        var fwd = xform.ValueRO.Forward();
    //        var rot = quaternion.AxisAngle(fwd, bank);
    //        var up = math.mul(rot, math.float3(0, 1, 0));

    //        xform.ValueRW.Position.y = y;
    //        xform.ValueRW.Rotation = quaternion.LookRotation(fwd, up);
    //    }
    //}

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var job = new DancerUpdateJob()
        { Elapsed = (float)SystemAPI.Time.ElapsedTime };
        job.ScheduleParallel();
    }
}
