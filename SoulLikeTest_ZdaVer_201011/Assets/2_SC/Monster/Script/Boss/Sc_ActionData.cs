using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 주의 : 애니메이션의 상태머신 상태이름과 똑같아야 한다. 예외처리 불가능. 
/// </summary>
public enum EAniName_Action
{
    None,
    Act_Lose,
    Act_Die,
    Act_Hit,
    StunStart,    
    StunRoop,
    StunEnd,
    Act_Attack,
}


[CreateAssetMenu(fileName = "Action Data", 
    menuName = "Aation/Action Data", order = int.MaxValue)]
public class Sc_ActionData : ScriptableObject
{
    public EAniName_Action AniName;
}
