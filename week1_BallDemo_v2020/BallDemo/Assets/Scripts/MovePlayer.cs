using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePlayer : MonoBehaviour
{
	public float JumpStrength;
	public float speed;
	public Text countText;

	public Rigidbody rb;
	private int count;

	//bool jump;

	private float WaitTime = 0;
	void Start()
	{
		//************* Instantiate the OSC Handler...
		OSCHandler.Instance.Init();
		OSCHandler.Instance.SendMessageToClient("pd", "/unity/trigger", "ready");
		//*************

		rb = GetComponent<Rigidbody>();
		count = 0;
		setCountText();
	}


	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			//WaitTime = 1;
			rb.AddForce(Vector3.up * JumpStrength);
			OSCHandler.Instance.SendMessageToClient("pd", "/unity/jump", 1);
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		//jump = Input.GetKeyDown("space");

		//countText.text = "(x,y,z): (" + rb.position.x.ToString() + ", " + rb.position.y.ToString() + ", " + rb.position.z.ToString() + ")";
		//WaitTime -= Time.deltaTime;
		//if ((jump == true) && (WaitTime < 0))

		Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
		rb.AddForce(movement * speed);
		//Debug.Log(rb.velocity.magnitude);
		/* if(rb.velocity.magnitude > 0) {
		 *		speed input = 30 * velocity.magnitude
		 *		play roll sounds
		 * 30 * velocity.magnitude
		 * }
		 */
		if (rb.velocity.magnitude > 0)
		{
			OSCHandler.Instance.SendMessageToClient("pd", "/unity/walk", rb.velocity.magnitude);
		}

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Wall"))
		{
			Debug.Log("hit");
			OSCHandler.Instance.SendMessageToClient("pd", "/unity/colwall", 1);
		}
	}

	void setCountText()
	{
		countText.text = "Count: " + count.ToString();
	}

}
