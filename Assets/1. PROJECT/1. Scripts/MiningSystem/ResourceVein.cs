using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourceVein : MonoBehaviour
{
    public LayerMask suitableSurfaceLayer;
    public float searchRadius = 5f;

    private void Start()
    {
        PlaceVein();
    }

    private void FixedUpdate()
    {
        //PlaceVein();
    }

    private void PlaceVein()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, searchRadius, suitableSurfaceLayer);
        if (hitColliders.Length > 0)
        {
            // Выбор случайной поверхности
            Collider chosenSurface = hitColliders[Random.Range(0, hitColliders.Length)];

            // Повернуть жилу, чтобы её верхняя часть соответствовала нормали поверхности
            transform.up = chosenSurface.transform.up;

            // Пускать луч вниз от жилы
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit, Mathf.Infinity, suitableSurfaceLayer))
            {
                // Перемещение жилы в точку соприкосновения луча с поверхностью
                // Учитывать размер жилы, чтобы нижняя её часть касалась поверхности
                transform.position = hit.point;
            }
            else
            {
                Debug.Log("Поверхность для соприкосновения не найдена");
            }
        }
        else
        {
            Debug.Log("Подходящая поверхность не найдена");
        }
    }
}



