using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    enum ReflectionReference
    {
        XAxis,
        YAxis,
        Origin
    }


    [SerializeField]
    private Camera mainCam;
    public Vector2 accleration;
    public float rotationSpeed;
    private Vector2 deacc;
    private Vector3 shootingPos;
    private GameObject bullets;
    private Vector2 velocity;
    private Vector2 position;
    public GameObject testAstroid;


    private void Update()
    {
        position = transform.position;

        ReflectionReference reflection;
        if (IsOutOfBounds(out reflection))
        {
            SpawnOppositeSite(reflection);
            return;
        }

        shootingPos = transform.forward * new Vector2(0, 2);
        var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (direction.x != 0) Rotate(direction);

        position += velocity * Time.deltaTime;



        if (velocity.magnitude >= 0 && direction.y != 1)
        {
            Deceleration();
            transform.position = position;
            return;
        }

        if (direction.y != 0) Thrust();
        transform.position = position;

    }

    private void Shoot()
    {
        print("Pew!");
    }

    private void Rotate(Vector3 direction)
    {
        transform.Rotate(new Vector3(0, direction.x));
    }

    private void Deceleration()
    {
        deacc = -(Vector2.zero - velocity) / Time.deltaTime;
    }

    private void Thrust()
    {
        velocity += accleration * transform.forward * Time.deltaTime;
    }

    private void SpawnOppositeSite(ReflectionReference reflection)
    {
        var buffer = transform.position * 0.01f;

        Vector3 newPos = Vector3.zero;

        var xPos = transform.position.x;
        var yPos = transform.position.y;

        if (transform.position.x > transform.position.y) newPos = new Vector3(-xPos, yPos);
        else if (transform.position.x < transform.position.y) newPos = new Vector3(xPos, -yPos);
        //else newPos = new Vector3(-xPos, -yPos);

        transform.position = newPos + buffer;
    }

    private Vector3 GetReflection(ReflectionReference reflection)
    {
        switch (reflection)
        {
            case ReflectionReference.XAxis:
                return new Vector2(1, 0);
            case ReflectionReference.YAxis:
                return new Vector2(0, 1);
            case ReflectionReference.Origin:
                return Vector2.zero;
            default:
                throw new System.Exception("WTF is this reflection?!");
        }
    }

    private bool IsOutOfBounds(out ReflectionReference reflection)
    {
        var screenPos = position;

        float size = mainCam.orthographicSize;

        var screenHeight = size;
        var screenWidth = size + (size * mainCam.rect.x);

        var xBounds = screenPos.x < -screenWidth || screenPos.x > screenWidth;
        var yBounds = screenPos.y < -screenHeight || screenPos.y > screenHeight;

        if (xBounds && yBounds) reflection = ReflectionReference.Origin;
        else if (xBounds) reflection = ReflectionReference.XAxis;
        else reflection = ReflectionReference.YAxis;


        return (
            screenPos.x < -screenWidth || screenPos.x > screenWidth ||
            screenPos.y < -screenHeight || screenPos.y > screenHeight
        );
    }

    private bool HasCollided(Transform astroid)
    {
        // Get the distance from the center of the spaceship to the astroids center
        var distance = Vector3.Distance(astroid.position, transform.position);

        Vector2 astroidRadius = (Vector3.up + Vector3.right * astroid.GetComponent<Renderer>().bounds.extents.sqrMagnitude);
        Vector2 spaceshipRadius = (Vector3.up + Vector3.right * transform.GetComponentInChildren<Renderer>().bounds.extents.sqrMagnitude);

        if (distance <= (spaceshipRadius + astroidRadius).x) return true;

        return false;
    }
}
