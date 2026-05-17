using UnityEngine;

public class ZigZagStrategy : IMovementStrategy
{
    private Transform coreTransform;
    private float frequency = 5f; // Zikzak sıklığı
    private float magnitude = 2f; // Zikzak genişliği

    public void Move(Transform entityTransform, float speed)
    {
        if (coreTransform == null)
        {
            EnergyCore core = Object.FindFirstObjectByType<EnergyCore>();
            if (core != null) coreTransform = core.transform;
        }

        if (coreTransform != null)
        {
            // Çekirdeğe doğru ana yön
            Vector3 direction = (coreTransform.position - entityTransform.position).normalized;

            // Bu yöne dik bir zikzak vektörü hesapla (2D için yukarı/aşağı dalgalanma)
            Vector3 perpendicular = new Vector3(-direction.y, direction.x, 0);
            Vector3 wave = perpendicular * Mathf.Sin(Time.time * frequency) * magnitude;

            // Ana hareket ile zikzak dalgasını birleştir
            entityTransform.Translate((direction * speed + wave) * Time.deltaTime, Space.World);
        }
    }
}