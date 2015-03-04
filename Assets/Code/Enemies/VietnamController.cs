using UnityEngine;
using System.Collections;

public class VietnamController : EnemyController {

	public AudioClip fxDead;

	#region FSM
	
	public FSM	Fsm{get;private set;}
	
	public MovingState 		movingState;
	
	public FightingState 	fightingState;
	
	public DeathState 		deathState;
	
	
	
	#endregion
	
	void Start()
	{
		//Initialize FSM
		Fsm = FSM.CreateFSM(this);
		Fsm.AddFSMState (fightingState, movingState, deathState);
		
		Fsm.ChangeState(movingState);
		
		GetComponent<LifeModule>().OnKill += HandleOnKill;
	}


	
	
	void HandleOnKill (GameObject _who)
	{
		Debug.Log("Me han matado");
		Fsm.ChangeState(deathState);

	}
	
	#region states
	
	
	[System.Serializable]
	public class MovingState : FSM.FSMState
	{
		private VietnamController myOwner;
		
		public override void Init ()
		{
			base.Init ();
			
			myOwner = owner as VietnamController;
			
		}
		
		public override void Enter (Hashtable _parameters)
		{
			base.Enter (_parameters);

			myOwner.skeletonAnimation.state.SetAnimation(0, "vietcong_run", true);
		}
		
		public override void Update ()
		{
			base.Update ();
			
			myOwner.GetComponent<Rigidbody2D>().velocity =  new Vector2(myOwner.velocity, myOwner.GetComponent<Rigidbody2D>().velocity.y);
			
			float distance = Vector2.Distance(myOwner.transform.position, myOwner.mainCharacter.transform.position);
			
			if(distance < 2F)
			{
				myOwner.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				ChangeState(myOwner.fightingState);
			}
			//Debug.Log(distance);
		}
		
		public override void Exit (FSM.FSMState _nextState)
		{
			
		}
		
	}
	
	[System.Serializable]
	public class FightingState : FSM.FSMState
	{
		
		private VietnamController myOwner;
		
		public override void Init ()
		{
			base.Init ();
			
			myOwner = owner as VietnamController;
			
		}
		
		public override void Enter (Hashtable _parameters)
		{
			base.Enter (_parameters);
			
			myOwner.skeletonAnimation.state.SetAnimation(0, "vietcong_attack", true);
		}
		
		public override void Update ()
		{
			base.Update ();
			
			
		}
		
		public override void Exit (FSM.FSMState _nextState)
		{
			base.Exit (_nextState);
			
			
		}
		
		private void Attack()
		{	
			
			
			
		}
		
	}
	
	[System.Serializable]
	public class DeathState : FSM.FSMState
	{
		
		private VietnamController myOwner;
		
		public override void Init ()
		{
			base.Init ();
			
			myOwner = owner as VietnamController;
		}
		
		public override void Enter (Hashtable _parameters)
		{
			base.Enter (_parameters);
			
			SoundManager.Instance.Play2DSound(myOwner.fxDead);
		//	myOwner.skeletonAnimation.state.SetAnimation(0, "vietcong_death", false);
			myOwner.GetComponent<BoxCollider2D>().enabled = false;
			myOwner.GetComponent<CircleCollider2D>().enabled = false;

			Vector2 right = -myOwner.transform.TransformDirection(Vector2.right);
			myOwner.GetComponent<Rigidbody2D>().fixedAngle = false;
			myOwner.GetComponent<Rigidbody2D>().AddForce(new Vector2( right.x * Random.Range(6,9), right.y +Random.Range(5,8)), ForceMode2D.Impulse);
			//myOwner.rigidbody2D.AddForceAtPosition(Vector2.up * -2, -Vector2.one, ForceMode2D.Impulse);
			myOwner.skeletonAnimation.enabled = false;
			myOwner.HitParticle();
			myOwner.BloodParticle();
			Destroy(myOwner.gameObject, 2);
		}
		
	}
	
	
	
	
	#endregion
}
