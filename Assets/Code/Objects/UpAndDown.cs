using UnityEngine;
using System.Collections;
using DG.Tweening;

public class UpAndDown : MonoBehaviour {

	public float endValue;
	public float timeToStart;
	public float timeToGoUp;
	public float timeToGoDown;
	public float timeToRestart;
	public enum Type_Movement
	{
		goUp,
		goDowm,
		goUpAndDown,
		goDownAndUp
	};
	public Ease easeType;


	public Type_Movement typeMovement;
	private float startValue;
	
	void Start () 
	{
		startValue = transform.localPosition.y;
		if(typeMovement == Type_Movement.goUp || typeMovement == Type_Movement.goUpAndDown)
		{
			Invoke("GoUp", timeToStart);
		}
		else
		{
			Invoke("GoDown", timeToStart);
		}
	}
	
	void GoUp()
	{
		if(typeMovement == Type_Movement.goUp)
		{
			transform.DOLocalMoveY(endValue, timeToGoUp).SetEase(easeType);
		}
		else
		{
			if(typeMovement == Type_Movement.goUpAndDown)
			{
				transform.DOLocalMoveY(endValue, timeToGoUp).OnComplete(GoDown).SetEase(easeType);
			}
			else
			{
				transform.DOLocalMoveY(startValue, timeToGoUp).OnComplete(Restart);
			}
		}
	}

	void GoDown()
	{
		if(typeMovement == Type_Movement.goDowm)
		{
			transform.DOLocalMoveY(endValue, timeToGoDown).SetEase(easeType);
		}
		else
		{
			if(typeMovement == Type_Movement.goDownAndUp)
			{
				transform.DOLocalMoveY(endValue, timeToGoDown).OnComplete(GoUp).SetEase(easeType);
			}
			else
			{
				transform.DOLocalMoveY(startValue, timeToGoDown).OnComplete(Restart);
			}
		}
	}

	void Restart()
	{
		if(typeMovement == Type_Movement.goDownAndUp)
		{
			Invoke("GoDown",timeToRestart);
		}
		else
		{
			Invoke("GoUp",timeToRestart);
		}

	}
}
