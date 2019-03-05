using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour {

    public float force;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            StartCoroutine(PushBack(collision.gameObject.GetComponent<Rigidbody2D>()));
    }

    private IEnumerator PushBack(Rigidbody2D rb)
    {
        float duration = 1f;
        float elapsedTime = 0f;
        float curForce = force;
        while (elapsedTime < duration)
        {
            rb.AddForce(Vector2.right * curForce);
            curForce = Mathf.Lerp(curForce, 0f, elapsedTime / duration);
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

}
