using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField]
    private Vector2 accleration;
    [SerializeField]
    private float rotationSpeed;
    private Vector3 shootingPos = new Vector3(0, 0, 2);
    private GameObject bullets;
    private Vector2 velocity;
    private float position;
    Vector2 posFinal;


    private void Update()
    {
        if (hasCollided)
            var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;



        velocity += accleration * Time.deltaTime;
        posFinal += velocity * Time.deltaTime;
        transform.position = posFinal;
    }

    private void Shoot()
    {
        print("Pew!");
    }

    private void Rotate(Vector3 direction)
    {
        transform.Rotate(new Vector3(direction.x, direction.y));
    }

    private void Thrust()
    {

    }

    private bool HasCollided(Transform astroid)
    {
        // CHeck the distance from the center of the spaceship to the 
        var distance = Vector3.Distance(astroid.position, transform.position);
        var astroidRadius = astroid.position + astroid.transform.lossyScale;
        var spaceshipRadius = transform.position + transform.lossyScale;
        return distance <= ;
    }

}
