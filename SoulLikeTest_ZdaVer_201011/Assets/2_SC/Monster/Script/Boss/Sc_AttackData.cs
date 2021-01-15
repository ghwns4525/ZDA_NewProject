using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 애니메이션 이름을 캐릭터에 맞게 제작해야하는데
// 애니메이션 이름들이 제각각이라 보스를 만들때마다 만들어줘야 한다.
// 이때 이름을 Enum으로 만들어서 다른 클래스에서도 사용가능하게 하고 있다.

// 이때 Enum을 강제로 만들게 하고 싶다. => 이 생각 자체가 지금 하고 있는 것과 맞지 않음.
// 스킬 하나를 커스텀 할 수 있게끔 하려고 이걸 만드는 건데
// 무엇을 커스텀 할껀데? 능력치랑 스킬 이름
// 근데 스킬이름 철자 틀리면 오류나서 그 꼬라지 보기 싫음..
// 그럼 스크립트마다 



[CreateAssetMenu(fileName = "Data_Atk_", menuName = "Aation /Attack Data", order = int.MaxValue)]
public class Sc_AttackData : ScriptableObject
{
    [Header("AniName 값은 꼭! 반드시! 애니메이션 상태 이름과 같아야함.")]
    /// <summary>
    /// 데미지
    /// </summary>
    [SerializeField]
    public string aniName;

    [Header("Status")]
    /// <summary>
    /// 데미지
    /// </summary>
    [SerializeField]
    public float damage;

    /// <summary>
    /// 경직치
    /// </summary>
    [SerializeField]
    public float stunAmount;

    /// <summary>
    /// 공격 사정거리
    /// </summary>
    [SerializeField]
    public float attackRange;

    /// <summary>
    /// 공격시 데미지 범위
    /// </summary>
    [SerializeField]
    public float attackDamageRange;

    
}



