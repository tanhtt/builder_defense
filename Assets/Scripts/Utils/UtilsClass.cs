using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsClass
{
    [SerializeField] private static Camera mainCamera;

    public static Vector3 MouseWorldPosition()
    {
        if(mainCamera == null) mainCamera = Camera.main;

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;

        return mouseWorldPosition;
    }

    public static Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    public static float GetAngleFromVector(Vector3 position)
    {
        return (Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg);
    }
}
