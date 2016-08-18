using UnityEngine;
using System.Collections;

public class Locomotion : MonoBehaviour {

	public float m_acceleration = 2.0f;
	public float m_TurnSpeed = 180f;
	public float m_maxSpeed = 25f;
	public float m_maxReverse = 10f;

	private string m_MovementAxisName;     
	private string m_TurnAxisName; 
	private float m_MovementInputValue;    
	private float m_TurnInputValue;
	private float m_currentSpeed;
	private Rigidbody m_Rigidbody; 

	// Use this for initialization
	void Start () {
		m_MovementAxisName = "Vertical";
		m_TurnAxisName = "Horizontal";
		m_currentSpeed = 0.0f;
		m_Rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
		m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

		Move();

		Turn();
	}

	void Move() {

		Vector3 movement = transform.forward * m_MovementInputValue * m_currentSpeed * Time.deltaTime;
		m_Rigidbody.MovePosition(m_Rigidbody.position + movement);

		if (m_MovementInputValue > 0) //forward
		{			
			m_currentSpeed += m_acceleration;

			if (m_currentSpeed > m_maxSpeed)
				m_currentSpeed = m_maxSpeed;
		}		
		else if (m_MovementInputValue < 0) { //backward

			m_currentSpeed += m_acceleration;

			if (m_currentSpeed > m_maxReverse)
				m_currentSpeed = m_maxReverse;
		}
		else if (m_MovementInputValue == 0) { //slow down - stop

			m_currentSpeed = 0;

			if (m_currentSpeed < 0)
				m_currentSpeed = 0;
		}
	}

	void Turn() {

		float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

		Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

		m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
	}
}
