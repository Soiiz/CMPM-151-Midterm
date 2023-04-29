using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePlayer : MonoBehaviour
{
	public float JumpStrength;
	public float speed;
	public Text countText;

	private Rigidbody rb;
	private int count;

	private float WaitTime = 0;
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;
		setCountText();
	}


	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		bool jump = Input.GetKeyDown("space");

		//countText.text = "(x,y,z): (" + rb.position.x.ToString() + ", " + rb.position.y.ToString() + ", " + rb.position.z.ToString() + ")";
		WaitTime -= Time.deltaTime;
		if ((jump == true) && (WaitTime < 0))
		{
			WaitTime = 1;
			rb.AddForce(Vector3.up * JumpStrength);
		}

		Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

		rb.AddForce(movement * speed);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Pick Up"))
		{
			other.gameObject.SetActive(false);
			count = count + 1;
			setCountText();
		}
	}

	void setCountText()
	{
		countText.text = "Count: " + count.ToString();
	}

}
