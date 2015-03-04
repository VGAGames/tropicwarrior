using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour {

	public delegate void SkillHandler();
	public static event SkillHandler OnCouldownReset;

	public ETCButton skillButton;
	public SpriteRenderer spriteSkillButtonOn, spriteSkillButtonOff;

	public MainCharacterController mainCharacter;

	#region FSMSkill
	
	public FSM	FsmSkill{get;private set;}
	
	public SkillOn 			skillOn;
	
	public SkillOff 		skillOff;

	
	#endregion

	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	#region stateSkill
	[System.Serializable]
	public class SkillOn : FSM.FSMState
	{
		
		private HUDManager myOwner;
		
		public override void Init ()
		{
			base.Init ();
			
			myOwner = owner as HUDManager;
		}
		
		public override void Enter (Hashtable _parameters)
		{
			base.Enter (_parameters);

//			myOwner.skillButton.normalSprite = myOwner.spriteSkillButtonOn;
			myOwner.skillButton.enabled = false;
		}
		
		
		public override void Update ()
		{
			base.Update ();

		}
		
		public override void Exit (FSM.FSMState _nextState)
		{
			
		}
		
	}

	[System.Serializable]
	public class SkillOff : FSM.FSMState
	{
		
		private HUDManager myOwner;
		
		public override void Init ()
		{
			base.Init ();
			
			myOwner = owner as HUDManager;
		}
		
		public override void Enter (Hashtable _parameters)
		{
			base.Enter (_parameters);
			
		}
		
		
		public override void Update ()
		{
			base.Update ();
			
		}
		
		public override void Exit (FSM.FSMState _nextState)
		{
			
		}
		
	}
	#endregion
}
