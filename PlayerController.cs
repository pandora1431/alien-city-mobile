﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

	public float MoveSpeed;
	public float RotationSpeed;
	CharacterController cc;
	private Animator anim;
	protected Vector3 gravidade = Vector3.zero;
	protected Vector3 move = Vector3.zero;
	private bool jump;
    public Joystick Joystick;
    public botaoscript botaoscript;


	
	void Start()
	{
		cc = GetComponent<CharacterController> ();
		anim = GetComponent<Animator>();
		anim.SetTrigger("Parado");
	}

	void Update()
	{

		Vector3 move = Joystick.Vertical * transform.TransformDirection (Vector3.forward) * MoveSpeed;
		transform.Rotate (new Vector3 (0, Joystick.Horizontal * RotationSpeed * Time.deltaTime, 0));
		
		if (!cc.isGrounded) {
			gravidade += Physics.gravity * Time.deltaTime;
		} 
		else 
		{
			gravidade = Vector3.zero;
			if(jump && !botaoscript.pressed)
			{
				gravidade.y = 6f;
				jump = false;
			}
		}
		move += gravidade;
		cc.Move (move* Time.deltaTime);
		Anima ();
	}
	 
	void Anima()
	{
		if (!Input.anyKey)
		{
			anim.SetTrigger("Parado");
		} 
		else 
		{
			if(!jump && botaoscript.pressed)
			{
				anim.SetTrigger("Pula");
				jump = true;
			}
			else
			{
				anim.SetTrigger("Corre");
			}
		}
	}
}
