using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTest : MonoBehaviour {

    public Swipe swipeControls;
    public RectTransform card;
    private Vector3 desiredPosition = new Vector3(0, 46, 0);

    private void Update()
    {
        if (swipeControls.SwipeLeft)
            desiredPosition += Vector3.left;
        if (swipeControls.SwipeRight)
            desiredPosition += Vector3.right;
        if (swipeControls.SwipeUp)
            desiredPosition += Vector3.forward;
        if (swipeControls.SwipeDown)
            desiredPosition += Vector3.back;

        Debug.Log(desiredPosition);
        Debug.Log(card.localPosition);
        card.transform.position = Vector3.MoveTowards(card.position, desiredPosition, 20f * Time.deltaTime);

        if (swipeControls.Tap)
            Debug.Log("Tap!");
    }
}
