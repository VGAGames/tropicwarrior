using UnityEngine;
using System.Collections;

public class VietnamShieldController : EnemyController {

	public float timeStuned = 2;

	public AudioClip damaged, dead, atack;

	#region FSM
	
	public FSM	Fsm{get;private set;}
	
	public MovingState 		movingState;
	
	public FightingState 	fightingState;
	
	public HittingState 	hittingState;
	
	public DeathState 		deathState;
	
	
	
	#endregion
	

	void Start()
	{
		//Initialize FSM
		Fsm = FSM.CreateFSM(this);
		Fsm.AddFSMState (fightingState, movingState,hittingState, deathState);
		
		Fsm.ChangeState(movingState);
		
		GetComponent<LifeModule>().OnKill += HandleOnKill;
		GetComponent<LifeModule>().OnLifeChange += HandleOnLifeChange;
	}
	
	void HandleOnLifeChange (LifeModule _who, float _currentLife, float _previous, float _percentage)
	{
		Fsm.ChangeState(hittingState);
		Invoke("ChangeState", timeStuned);
	}

	
	void HandleOnKill (GameObject _who)
	{
		Debug.Log("Me han matado");
		Fsm.ChangeState(deathState);
	}

	void ChangeState()
	{
		Fsm.ChangeState(movingState);
	}
	
	#region states
	
	
	[System.Serializable]
	public class MovingState : FSM.FSMState
	{
		private VietnamShieldController myOwner;
		
		public override void Init ()
		{
			base.Init ();
			
			myOwner = owner as VietnamShieldController;
			
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
		
		private VietnamShieldController myOwner;
		
		public override void Init ()
		{
			base.Init ();
			
			myOwner = owner as VietnamShieldController;
			
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
	public class HittingState : FSM.FSMState
	{
		
		private VietnamShieldController myOwner;
		
		public override void Init ()
		{
			base.Init ();
			
			myOwner = owner as VietnamShieldController;
		}
		
		public override void Enter (Hashtable _parameters)
		{
			base.Enter (_parameters);

			Vector2 right = -myOwner.transform.TransformDirection(Vector2.right);
			myOwner.GetComponent<Rigidbody2D>().AddForce(new Vector2( right.x * 10, 0), ForceMode2D.Impulse);
			
			myOwner.skeletonAnimation.state.SetAnimation(0, "vietcong_shieldhit", false);
			myOwner.skeletonAnimation.skeleton.SetSkin("brazo_d_empty");

		}
		
	}
	
	[System.Serializable]
	public class DeathState : FSM.FSMState
	{
		
		private VietnamShieldController myOwner;
		
		public override void Init ()
		{
			base.Init ();
			
			myOwner = owner as VietnamShieldController;
		}
		
		public override void Enter (Hashtable _parameters)
		{
			base.Enter (_parameters);
			
			
			//myOwner.skeletonAnimation.state.SetAnimation(0, "enemy_death", false);
			myOwner.GetComponent<BoxCollider2D>().enabled = false;
			myOwner.GetComponent<CircleCollider2D>().enabled = false;
			
			Vector2 right = -myOwner.transform.TransformDirection(Vector2.right);
			myOwner.GetComponent<Rigidbody2D>().fixedAngle = false;
			myOwner.GetComponent<Rigidbody2D>().AddForce(new Vector2( right.x * Random.Range(2,6), right.y +Random.Range(5,8)), ForceMode2D.Impulse);
			myOwner.skeletonAnimation.enabled = false;
			myOwner.HitParticle();
			myOwner.BloodParticle();
			Destroy(myOwner.gameObject, 2);
		}
		
	}
	
	
	
	
	#endregion
}
