---

title: "[유니티] 게임 비활성화, 종료 시 처리"

comments: true

categories:

  - Record

tags:

  - Unity

---


유니티로 게임을 만들면 홈버튼을 눌러서 비활성화할 때와 게임을 종료할 때의 처리를 진행해야할 필요가 있다. 이러한 기능들은 유니티에서 제공하고 있기 때문에 [MonoBehaviour](https://docs.unity3d.com/kr/current/ScriptReference/MonoBehaviour.html)가 지원하는 기본 함수를 사용하여 간단하게 적용할 수 있다.

### OnApplicationPause

[OnApplicationPause](https://docs.unity3d.com/kr/current/ScriptReference/MonoBehaviour.OnApplicationPause.html)는 홈버튼을 눌러 어플을 비활성화하거나 다시 활성화했을 때 실행되는 함수로, bool값을 매개변수로 받는다.

```cs

//앱의 활성화 상태를 저장하는 변수
bool isPaused = false; 

void OnApplicationPause(bool pause)
{
	if (pause)
	{
    	isPaused = true;
    	/* 앱이 비활성화 되었을 때 처리 */    
    }

	else
	{
		if (isPause)
		{
	    	isPaused = false;
	    	/* 앱이 활성화 되었을 때 처리 */    
	    }
  	}
}
  
```


### OnApplicationQuit

[OnApplicationQuit](https://docs.unity3d.com/kr/current/ScriptReference/MonoBehaviour.OnApplicationQuit.html)은 앱이 꺼질 때 실행되는 함수다. 별다른거 없고 그냥 쓰면 된다.

```cs
void OnApplicationQuit()
{

  /* 앱이 종료 될 때 처리 */   

}
```