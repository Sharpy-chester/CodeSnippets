using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarMovement : MonoBehaviour
{
	[Header("Game Setup")]
	[SerializeField] private Transform[] _lanes;                    // Array of lanes the car will be able to switch between
				

	[Header("Movement Settings")]
	[SerializeField] private float _movementLerpDuration;                       // The speed that the car will switch lanes

	private int _currentLane;										// Reference to the current lane the car is in
	private bool _directionSet;
	private CarInput _carInput;

	private enum Direction { left, right };							// A enum used to store the directions the player is able to move in
	private Direction _currentDirection;							// A new direction for what the player wants to go in

	[SerializeField] private Ease _movementEase;

	private void Awake()
	{
		_carInput = GetComponent<CarInput>();
		_currentLane = 1;
	}

	private void Update()
	{
		// Checking for button input and setting the direction based on which button was pressed
		GetDirection();

		// Checking if a direction has been set
		if (_directionSet)
			ChangeLane(_currentDirection);
	}

	private void GetDirection()
	{
		if (!_directionSet)
		{
			if (_carInput.HasSwipedRight)
			{
				_currentDirection = Direction.right;
				_directionSet = true;
			}

			if (_carInput.HasSwipedLeft)
			{
				//Debug.Log("Left");
				_currentDirection = Direction.left;
				_directionSet = true;
			}
		}
	}

	void ChangeLane(Direction direction)
	{
		// If the direction received was left and the player isn't in the left most lane
		if (direction == Direction.left && _currentLane != 0)
		{
			transform.DOMove(_lanes[_currentLane - 1].position, _movementLerpDuration, false).SetEase(_movementEase);
			_currentLane--;
			_directionSet = false;

		}
		// If the direction received was right and the player isn't in the right most lane
		else if (direction == Direction.right && _currentLane != _lanes.Length - 1)
		{
			transform.DOMove(_lanes[_currentLane + 1].position, _movementLerpDuration, false).SetEase(_movementEase);

			_currentLane++;
			_directionSet = false;
		}
		else
		{
			_directionSet = false;
			Debug.Log($"Player cannot move to the {direction} anymore");
		}

	}


}
