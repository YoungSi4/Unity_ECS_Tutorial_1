using Unity.Mathematics;

// for make short studying time, I used AI generated code in this script
// this script is made for substitute custom method of Random library
// cause in the tutorial I watch now he didin't let me know about that code... :(

public static class RandomExtensions
{
    // Unity.Mathematics.Random 구조체를 확장하여 NextOnDisk() 기능 추가
    public static float2 NextOnDisk(this ref Random random) // this ref Random random으로 연결
    {
        // 1. 랜덤한 2D 방향(정규화된 벡터)을 얻습니다.
        float2 dir = random.NextFloat2Direction();

        // 2. 랜덤한 반지름 길이를 얻습니다. 
        // (단순히 랜덤 값을 쓰면 원 중심에 점이 몰리므로, 균일한 분포를 위해 제곱근(sqrt) 처리를 해줍니다.)
        float radius = math.sqrt(random.NextFloat());

        // 3. 방향과 반지름을 곱해 원 안의 랜덤한 점 반환
        return dir * radius;
    }

    // 만약 3D 공간의 바닥(X, Z축 기준)에 무언가를 스폰하는 용도라면 아래처럼 응용할 수 있습니다.
    public static float3 NextOnDisk3D(this ref Random random)
    {
        float2 disk = random.NextOnDisk();
        return new float3(disk.x, 0f, disk.y); // Y축을 0으로 두고 평면에 배치
    }

    // Y축(위아래 축)을 기준으로 랜덤하게 회전하는 쿼터니언 반환
    public static quaternion NextYRotation(this ref Random random)
    {
        // 1. 0에서 2π 라디안(0도 ~ 360도) 사이의 랜덤한 각도를 추출합니다.
        // (Unity.Mathematics는 각도 단위로 일반적인 degree 대신 radian을 주로 사용합니다.)
        float randomAngle = random.NextFloat(math.PI * 2f);

        // 2. Y축을 기준으로 해당 각도만큼 회전하는 쿼터니언(quaternion)을 생성하여 반환합니다.
        return quaternion.RotateY(randomAngle);
    }
}