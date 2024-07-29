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
}
