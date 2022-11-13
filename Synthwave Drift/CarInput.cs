using UnityEngine;
public class CarInput : MonoBehaviour
{

	bool _canDetectSwipe;
	bool _swipedRight;
	bool _swipedLeft;
	Vector2 _swipeStartPosition;
	[SerializeField] private float _minimumSwipeDistance = 0.2f;
	public float HorizontalAxis => Input.GetAxis(InputManager.HorizontalAxis);
	public bool HasSwipedRight => _swipedRight;
	public bool HasSwipedLeft => _swipedLeft;

	//detect which direction has been swiped

	//send that to the car controller


	void Awake()
	{
		_canDetectSwipe = true;
	}

	private void Update()
	{
		DetectSwipe();
	}


	void DetectSwipe()
    {
		_swipedRight = false;
		_swipedLeft = false;
		

        if (Input.touches.Length >0)
		{
            Touch currentTouch = Input.GetTouch(0);

			switch (currentTouch.phase)
			{
				case TouchPhase.Began:

					_swipeStartPosition = new Vector2(currentTouch.position.x / Screen.width, currentTouch.position.y / Screen.width);
					break;

				case TouchPhase.Moved:

					if (_canDetectSwipe)
					{
						Vector2 endPos = new Vector2(currentTouch.position.x / Screen.width, currentTouch.position.y / Screen.width);
						Vector2 swipeDirection = endPos - _swipeStartPosition;

						if (swipeDirection.magnitude < _minimumSwipeDistance)
						{
							// Swipe was too short
							return;
						}

						if (swipeDirection.x < 0)
						{
							_swipedLeft = true;
						}
						else
						{
							_swipedRight = true;
						}

						_canDetectSwipe = false;
					}
					break;

				case TouchPhase.Ended:

					_canDetectSwipe = true;
					break;

				default:
					break;
			}


		}
    }



}
