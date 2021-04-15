using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerMeshRoot;
    public GameObject playerMeshParent;
    public GameObject camRoot;

    private Vector3 movementVelocity = Vector3.zero;
    public float movementTime;
    private Vector3 intendedTargetPosition = Vector3.zero;

    public float tapDelay;
    private bool clickable = true;

    private float gridStep = 2f;

    
    
    //0 forward, 1 back, 2 right, 3 left
    public bool[] obstaclesCheck;
    public bool[] movingPlatformsCheck;
    public GameObject[] movingPlatforms;



    private Vector3 mousePressPos = Vector3.zero;
    private float minimumRangeBeforeDrag;

    private bool isDead = false;

    private bool isOnMovingPlatform = false;
    public GameObject currentMovingPlatform;
    private Vector3 movingPlatformPosition = Vector3.zero;

    private float movementInterpolation = 1f;
    public float movementSpeed;

    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        minimumRangeBeforeDrag = Screen.currentResolution.width / 10f;
    }

    private void FixedUpdate()
    {

        if (!isDead)
        {
            if (isOnMovingPlatform)
            {
                intendedTargetPosition = currentMovingPlatform.transform.position + Vector3.up * 0.3f;
                rb.MovePosition(intendedTargetPosition);
                //playerMeshRoot.transform.position = intendedTargetPosition;
            }
            else
            {
                rb.MovePosition(intendedTargetPosition);
                //playerMeshRoot.transform.position = Vector3.SmoothDamp(playerMeshRoot.transform.position, transform.position, ref movementVelocity, movementTime);
            }

            if (movementInterpolation < 1)
            {
                movementInterpolation += Time.fixedDeltaTime * movementSpeed;
            }

            playerMeshRoot.transform.position = Vector3.Lerp(playerMeshRoot.transform.position, intendedTargetPosition, movementInterpolation);

            camRoot.transform.position = playerMeshRoot.transform.position;
        }
    }

    private void Update()
    {

        if (!clickable || isDead)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            mousePressPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            //TAPPING
            if ((Input.mousePosition - mousePressPos).magnitude <= minimumRangeBeforeDrag)
            {
                if (!obstaclesCheck[0])
                {
                    movementInterpolation = 0;

                    if (movingPlatformsCheck[0])
                    {
                        isOnMovingPlatform = true;
                        currentMovingPlatform = movingPlatforms[0];
                    }
                    else
                    {
                        if (isOnMovingPlatform)
                        {
                            isOnMovingPlatform = false;

                            intendedTargetPosition.x = (int)intendedTargetPosition.x + (int)intendedTargetPosition.x % gridStep;
                            intendedTargetPosition.z = (int)intendedTargetPosition.z + (int)intendedTargetPosition.z % gridStep;
                        }
                        intendedTargetPosition.z += gridStep;
                    }

                    clickable = false;
                    StartCoroutine(RegainClickability());
                }
            }
            //DRAGGING
            else
            {
                Vector2 dragDifference = Input.mousePosition - mousePressPos;

                //MOVE RIGHT OR LEFT
                if (Mathf.Abs(dragDifference.x) > Mathf.Abs(dragDifference.y))
                {
                    //MOVE RIGHT
                    if (dragDifference.x > 0)
                    {
                        if (obstaclesCheck[2])
                        {
                            return;
                        }

                        movementInterpolation = 0;

                        if (movingPlatformsCheck[2])
                        {
                            isOnMovingPlatform = true;
                            currentMovingPlatform = movingPlatforms[2];
                        }
                        else
                        {
                            intendedTargetPosition.x += gridStep;
                        }

                        clickable = false;
                        StartCoroutine(RegainClickability());
                    }
                    //MOVE LEFT
                    else
                    {
                        if (obstaclesCheck[3])
                        {
                            return;
                        }

                        movementInterpolation = 0;

                        if (movingPlatformsCheck[3])
                        {
                            isOnMovingPlatform = true;
                            currentMovingPlatform = movingPlatforms[3];
                        }
                        else
                        {
                            intendedTargetPosition.x -= gridStep;
                        }

                        clickable = false;
                        StartCoroutine(RegainClickability());
                    }
                }
                //MOVE FORWARD OR BACK
                else
                {
                    //MOVE FORWARD
                    if (dragDifference.y > 0)
                    {
                        if (obstaclesCheck[0])
                        {
                            return;
                        }

                        movementInterpolation = 0;

                        if (movingPlatformsCheck[0])
                        {
                            isOnMovingPlatform = true;
                            currentMovingPlatform = movingPlatforms[0];
                        }
                        else
                        {
                            intendedTargetPosition.z += gridStep;
                        }

                        clickable = false;
                        StartCoroutine(RegainClickability());
                    }
                    //MOVE BACK
                    else
                    {
                        if (obstaclesCheck[1])
                        {
                            return;
                        }

                        movementInterpolation = 0;

                        if (movingPlatformsCheck[1])
                        {
                            isOnMovingPlatform = true;
                            currentMovingPlatform = movingPlatforms[1];
                        }
                        else
                        {
                            intendedTargetPosition.z -= gridStep;
                        }

                        clickable = false;
                        StartCoroutine(RegainClickability());
                    }
                }
            }
        }
    }

    IEnumerator RegainClickability()
    {
        yield return new WaitForSeconds(tapDelay);

        clickable = true;
    }


    public void Die()
    {
        isDead = true;
        playerMeshParent.GetComponent<Animator>().Play("FlatDown");
    }

}
