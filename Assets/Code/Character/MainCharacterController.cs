using UnityEngine;
using System.Collections;

public class MainCharacterController : MonoBehaviour {

	public delegate void SkillHandler();
	public static event SkillHandler OnCouldownReset;

	public enum Weapon
	{
		colt,
		M4
	}



	public Transform Skeleton;
	public LayerMask ignoreLayer;
	public Weapon currentWeapon;
	public float distanceAim;
	public float couldownSkill;
	public Transform Skill;

	public AudioClip coltFire, coltClipin, coltClipOut;

	public float timeBlock;

	private SkeletonAnimation skeletonAnimation;

	private float currentCouldown;

	#region FSM
	
	public FSM	Fsm{get;private set;}
	
	public IdleState 		idleState;
	
	public ShootingState 	shootingState;

	public MissingState 	missingState;

	public SkillState 		skillState;
	
	public DeathState 		deathState;
	
	
	
	#endregion

	void Start()
	{

		currentCouldown = 0;

		skeletonAnimation = Skeleton.GetComponent<SkeletonAnimation>();

		//Initialize FSM
		Fsm = FSM.CreateFSM(this);
		Fsm.AddFSMState (idleState, shootingState, skillState, missingState, deathState);
		
		Fsm.ChangeState(idleState);

		//Esto lo hago para ignorar las capas que no quiero que golpeen con el rayo...el simbolo ~ hace que coja lo inverso
		ignoreLayer = ~ignoreLayer;
	}

	//BORRAR CUANDO SE TERMINE EL JUEGO; SOLO ES PARA VER EL RAYO 
	void FixedUpdate()
	{
		Vector2 right = transform.TransformDirection(Vector2.right);
		Debug.DrawRay(transform.position, right * distanceAim, Color.green);
	}

	public void MissingShoot()
	{
		Debug.Log("Blocked");
		Invoke("ManualChangeState",timeBlock);
	}

	//This is only used for invoke method.
	void ManualChangeState()
	{
		SoundManager.Instance.Play2DSound(coltClipOut);
		Fsm.ChangeState(idleState);
	}

	public void LeftButtonPush()
	{
		if(Fsm.GetCurrentState() == idleState)
		{
			Fsm.ChangeState(shootingState ,new Hashtable(){{"dir", "left"}});
		}
	}

	public void RightButtonPush()
	{
		if(Fsm.GetCurrentState() == idleState)
		{
			Fsm.ChangeState(shootingState ,new Hashtable(){{"dir", "right"}});
		}
	}

	public void SkillButtonPush()
	{
		if(Fsm.GetCurrentState() != deathState)
		{
			Debug.Log("SFKNQ");
			Fsm.ChangeState(skillState);
		}
	}
	#region states
	
	
	[System.Serializable]
	public class IdleState : FSM.FSMState
	{

		private MainCharacterController myOwner;

		public override void Init ()
		{
			base.Init ();

			myOwner = owner as MainCharacterController;
		}
		
		public override void Enter (Hashtable _parameters)
		{
			base.Enter (_parameters);

			myOwner.skeletonAnimation.state.SetAnimation(0, "rambo_idle_"+myOwner.currentWeapon, true);
		}

		
		public override void Update ()
		{
			base.Update ();

		//if i push left, change state to shoot and parameter left
			if(Input.GetKeyDown(KeyCode.LeftArrow))
			{
				ChangeState(myOwner.shootingState ,new Hashtable(){{"dir", "left"}});
			}
			
			if(Input.GetKeyDown(KeyCode.RightArrow))
			{
				ChangeState(myOwner.shootingState ,new Hashtable(){{"dir", "right"}});
			}
		}
		
		public override void Exit (FSM.FSMState _nextState)
		{
			
		}
		
	}
	
	[System.Serializable]
	public class ShootingState : FSM.FSMState
	{
		
		private MainCharacterController myOwner;
		
		public override void Init ()
		{
			base.Init ();
			
			myOwner = owner as MainCharacterController;
		}
		
		public override void Enter (Hashtable _parameters)
		{
			base.Enter (_parameters);

			Shoot(_parameters["dir"] == "left");


		}
		
		public override void Update ()
		{
			base.Update ();
			
			
		}
		
		public override void Exit (FSM.FSMState _nextState)
		{
			base.Exit (_nextState);
			
			
		}
		
		public void Shoot(bool _left)
		{

			Quaternion rot = myOwner.transform.rotation;
			//Roto mi personaje hacia el lado que le de
			if(_left)
				rot.y = 180;
			else
				rot.y = 0;
			
			myOwner.transform.rotation = rot;

			//Compruebo si cuando roto, hay algun enemigo en mi rango
			Vector2 right = myOwner.transform.TransformDirection(Vector2.right);
			RaycastHit2D hit = Physics2D.Raycast(myOwner.transform.position, right,myOwner.distanceAim, myOwner.ignoreLayer);

			//Si no golpeo a nadie, encasquillo el arma MISS
			if(hit.collider == null)
			{
				ChangeState(myOwner.missingState);
			}
			else if(hit != null && hit.collider.tag == "Enemy")
			{
				ChangeState(myOwner.idleState);
				SoundManager.Instance.Play2DSound(myOwner.coltFire);
				myOwner.skeletonAnimation.state.SetAnimation(1, "rambo_attack_" + myOwner.currentWeapon.ToString(), false);
				hit.transform.GetComponent<LifeModule>().DoDamage(1);
			}
		}
		
	}

	[System.Serializable]
	public class MissingState : FSM.FSMState
	{
		
		private MainCharacterController myOwner;
		
		public override void Init ()
		{
			base.Init ();
			
			myOwner = owner as MainCharacterController;
		
		}
		
		public override void Enter (Hashtable _parameters)
		{
			base.Enter (_parameters);
			myOwner.skeletonAnimation.state.SetAnimation(0, "rambo_FAIL_" + myOwner.currentWeapon.ToString(), false);
			myOwner.MissingShoot();
			SoundManager.Instance.Play2DSound(myOwner.coltClipin);
		}
		
		public override void Update ()
		{
			base.Update ();
			
			
		}
		
		public override void Exit (FSM.FSMState _nextState)
		{
			base.Exit (_nextState);
			
			
		}
	}

	[System.Serializable]
	public class SkillState : FSM.FSMState
	{
		
		private MainCharacterController myOwner;
		public override void Init ()
		{
			base.Init ();
			myOwner = owner as MainCharacterController;
			
			
		}
		
		public override void Enter (Hashtable _parameters)
		{
			base.Enter (_parameters);
			Instantiate (myOwner.Skill);
			myOwner.skeletonAnimation.state.SetAnimation(1, "magia", false);
			SkillController.OnDestroy += delegate() {
				ChangeState(myOwner.idleState);
			};
			//Compruebo el tiempo de la animacion del helicoptero y despues cambio a idle
			
		}

		public override void Exit (FSM.FSMState _nextState)
		{
			base.Exit (_nextState);
			
			
		}

	}

	[System.Serializable]
	public class DeathState : FSM.FSMState
	{
		
		
		public override void Init ()
		{
			base.Init ();
			
			
			
		}
		
		public override void Enter (Hashtable _parameters)
		{
			base.Enter (_parameters);
			
			
			
		}
		
	}
	
	
	
	
	#endregion
}
