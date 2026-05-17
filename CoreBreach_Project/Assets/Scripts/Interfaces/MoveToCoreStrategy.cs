using UnityEngine;

public class MoveToCoreStrategy : IMovementStrategy
{
    private Transform coreTransform;

    public void Move(Transform entityTransform, float speed)
    {
        if (coreTransform == null)
        {
            // Sahnedeki EnergyCore nesnesini buluyoruz
            EnergyCore core = Object.FindFirstObjectByType<EnergyCore>();
            if (core != null) coreTransform = core.transform;
        }

        if (coreTransform != null)
        {
            // Çekirdeğe doğru yönel ve yürü
            Vector3 direction = (coreTransform.position - entityTransform.position).normalized;
            entityTransform.Translate(direction * speed * Time.deltaTime, Space.World);

            // Düşmanın gittiği yöne doğru bakmasını sağla
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            entityTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}