---

title: "[유니티] 애니메이션 State 상태 체크하기"

comments: true

categories:

  - Record

tags:

  - Unity
  - Animation
  - Animator

---

유니티에서 애니메이션을 사용할 때, 특정 State가 완료되었는 지 아닌 지를 체크해야할 일이 많다. State에 직접 함수를 붙여서 체크해도 되긴 하지만, 여간 귀찮은 일이 아니다.  
그럴 때 **코루틴**을 사용하면 보다 간편하게 애니메이션 상태를 체크할 수 있다.  


```cs
...

Animator animator;
float exitTime = 0.8f;

...

IEnumerator CheckAnimationState()
{

	while (!animator.GetCurrentAnimatorStateInfo(0)
	.IsName("원하는 애니메이션 이름")) 
	{ 
		//전환 중일 때 실행되는 부분
		yield return null;
	}

	while (animator.GetCurrentAnimatorStateInfo(0)
	.normalizedTime < exitTime)
	{
		//애니메이션 재생 중 실행되는 부분
		yield return null;
	}
	
	//애니메이션 완료 후 실행되는 부분

}

...

StartCoroutine(CheckAnimationState());

```

---

위의 코루틴 함수를 애니메이션 재생과 동시에 실행해주면 된다.  

`animator.GetCurrentAnimatorStateInfo(0).IsName("애니이름")`으로 우선 현재 애니메이션 State가 원하는 지점으로 진입했는지를 체크한다.

이 경우, `animator.IsInTransition(0)`를 통해 현재 애니메이션이 전환 중인지 체크가 가능하나 AnyState등을 사용하여 전환 중인 상태에서 전환이 일어나는 경우 문제가 될 수 있다.

`animator.GetCurrentAnimatorStateInfo(0)
.normalizedTime`은 현재 애니메이션이 얼마나 진행되었나를 나타낸다. 클립의 첫 프레임이 0.0이고 마지막 프레임이 1.0이다. 즉 이 변수가 1보다 작은 동안은 애니메이션이 재생 중이라는 뜻이다. 단, 루프를 하는 애니메이션이 경우, 한 바퀴 도는 동안이 1이고, 이 이후에는 계속 더해진다.


문제는 끝나고 전환이 있는 경우이다.
`normalizedTime`는 현재 에니메이션의 진행도를 받아오되, 애니메이션중간에 전환되면, 현재 애니메이션 상태는 바뀌지만 진행도는 유지된다.

예를 들어 [idle] - [active] - [idle]로 이어지는 일련의 애니메이션 흐름이 있고, 이때 각각 애니메이션의 **Exit Time**은 0.8f, **Transition Duration**은 0.5f(Fixed Duration는 false)라 가정해보자.  
[idle]이 진행되고 0.8만큼 진행되어 [active]로 넘어가는 전환되는 경우, 현재 애니메이션은 즉시 [Active]가 되지만, 전환이 끝날 때 까지 `normalizedTime`는 진행도를 유지한다. 즉 0.8에서 현재 애니메이션은 바로 [Acitve]가 되지만, 전환이 진행되는 0.5동안, 1.3까지는 계속해서 전환 이전 이벤트의 진행도를 유지한다는 것이다.

이 때문에 전환이 시작하기 전까지를 체크하려면 'Exit Time'과 비교해야하고, 전환까지 체크하려면 'Exit Time'과 'Transition Duration'을 더한 값을 비교하거나, 'Exit Time'까지를 체크한 후, `animator.IsInTransition(0)`로 전환 중임을 체크해야한다.


모든 곳에 통용되는 코드가 아니고 애니메이션의 설정에 따라 변수가 있기 때문에 자기 상황에 맞춰 적절하게 응용하도록 하자.
