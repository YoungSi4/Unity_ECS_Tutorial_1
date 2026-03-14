using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

public struct Dancer : IComponentData
{
    public float Speed;

    public static Dancer Random(uint seed)
        => new Dancer() { Speed = new Unity.Mathematics.Random(seed).NextFloat(1, 8) };
}

public class DancerAuthoring : MonoBehaviour
{
    public float _speed = 1;

    class Baker : Baker<DancerAuthoring>
    {
        public override void Bake(DancerAuthoring src)
        {
            var data = new Dancer(){ Speed = src._speed };
            AddComponent(GetEntity(TransformUsageFlags.Dynamic), data);
        }
    }
}
