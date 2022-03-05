using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovement : MonoBehaviour
{
    public Camera leftCamera;
    public Transform pivot;
    public enum MovementPath {Circle};
    public MovementPath movementMode;

    public float radius;
    public float speed;

    void Update()
    {
        SetPosition();
        //SetPositionTwo();

        SetRotation();
    }

    void SetPosition ()
    {
        Vector3 mousePosRaw = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
        Vector3 mousePos = leftCamera.ScreenToWorldPoint(mousePosRaw);
        Vector3 dir = new Vector3(mousePos.x - pivot.position.x,
                                  mousePos.y - pivot.position.y, 0).normalized;
        Vector3 newPos = pivot.transform.position + dir * radius;
        transform.position = newPos;
    }

    void SetRotation ()
    {
        Vector3 dir = new Vector3(transform.position.x - pivot.position.x,
                                  transform.position.y - pivot.position.y, 0).normalized;
        float angle = - 180f * Mathf.Atan(dir.x / dir.y) / Mathf.PI;
        transform.localEulerAngles =  new Vector3(0, 0, angle);
    }

    void SetPositionTwo ()
    {
        //Calculate the new position that the space ship should go to
        Vector3 mousePosRaw = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
        Vector3 mousePos = leftCamera.ScreenToWorldPoint(mousePosRaw);
        Vector3 dir = new Vector3(mousePos.x - pivot.position.x,
                                  mousePos.y - pivot.position.y, 0).normalized;
        float newPosAngle = Mathf.Atan(dir.y / dir.x);
        if (dir.x < 0)
        {
            newPosAngle += Mathf.PI;
        }
        else if (dir.y < 0)
        {
            newPosAngle += Mathf.PI * 2;
        }

        //Calculate the space ship's current angle
        Vector3 currentDir = new Vector3(transform.position.x - pivot.position.x,
                                  transform.position.y - pivot.position.y, 0).normalized;
        float currentAngle = Mathf.Atan(currentDir.y / currentDir.x);
        if (currentDir.x < 0)
        {
            currentAngle += Mathf.PI;
        }
        else if (currentDir.y < 0)
        {
            currentAngle += Mathf.PI * 2;
        }

        //Calculate which direction the space ship should go
        float opposite = currentAngle + Mathf.PI;
        while (opposite > 2 * Mathf.PI)
        {
            opposite -= 2 * Mathf.PI;
        }
        if (opposite < newPosAngle)
        {
            currentAngle += speed * Time.deltaTime;
        }
        else
        {
            currentAngle -= speed * Time.deltaTime;
        }
        if (Mathf.Abs(currentAngle - newPosAngle) < 0.01f)
        {
            currentAngle = newPosAngle;
        }
        if (currentAngle > 2 * Mathf.PI)
        {
            currentAngle -= 2 * Mathf.PI;
        }
        else if (currentAngle < 0f)
        {
            currentAngle += 2 * Mathf.PI;
        }

        //Move the space ship
        float newX = radius * Mathf.Cos(currentAngle);
        float newY = radius * Mathf.Sin(currentAngle);
        Vector3 newPos = new Vector3(newX, newY, 0);
        transform.position = newPos;
    }

}
