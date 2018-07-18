---

title: "[유니티] UI 클릭 시, 다른 클릭 이벤트 일어나지 않게 하기"

comments: true

categories:

  - Record

tags:

  - Unity
  - UI
  - Events

---



유니티에서 클릭(터치)이벤트를 처리할 때, **UI를 클릭할 때는 클릭 이벤트가 처리되지 않도록 해야할 때**가 있다. 예를 들어 터치한 곳으로 캐릭터가 이동하게 해야하는 경우, 화면 상에 있는 버튼 등을 눌렀을 때도 이러한 처리가 일어나서는 안된다. 이럴 때 클릭 처리 중 UI가 클릭된 경우를 제외해야하는 것이다.

---


## 일반적인 경우

일반적인 경우엔 **[IsPointerOverGameObject](https://docs.unity3d.com/ScriptReference/EventSystems.EventSystem.IsPointerOverGameObject.html)**를 사용하면 간단하게 처리가 가능하다.


유니티에서의 일반적인 클릭(터치)는 Raycast를 통해 처리하게 된다. 그렇다면 UI에도 Raycast의 타겟이 되도록 설정해준 후에 UI가 클릭되지 않았을 때만 클릭 처리를 하게 하면 되는 것이다.


![Raycast 타겟 설정](/assets/images/posts/2018-07-18-block-event-when-ui-click/01.png)


우선 원하는 UI에 위와 같이 Raycast Target 설정을 해준다.(아마 기본적으로 되어있을 것이다.) 그 후 터치 처리를 할 곳에 **`IsPointerOverGameObject()`**로 조건문 처리를 해주면 된다. UI를 클릭한 경우 true가 되고 아닌 경우 false가 되기 때문에 false인 경우에만 클릭이 작동하도록 하면 된다.

이때 매개 변수는 `Touch ID`를 받는데, 마우스라면 `-1`, 터치라면 입력된 터치의 `fingerId`를 넣어주면 된다.


```cs
using UnityEngine.EventSystems; 
```
```cs
//마우스 
if(Input.GetMouseButtonDown(0)) 
{
	if(!EventSystem.current.IsPointerOverGameObject())
	{  
	         //클릭 처리
	}
	
}
```
```cs
//터치
if(Input.touchCount > 0)
{
	if(!EventSystem.current
	.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
	{  
	         //터치 처리
	}
}

```

## 일반적이지 않은 경우

대부분의 경우 위의 방법으로 처리가 된다.

**근데 난 안되더라.**~~(대체왜...)~~

열심히 구글링한 결과 수동으로 처리하는 법을 찾아냈다.

```cs

public bool IsPointerOverUIObject(Vector2 touchPos)
{
    PointerEventData eventDataCurrentPosition 
    	= new PointerEventData(EventSystem.current);
    
    eventDataCurrentPosition.position = touchPos;
    
    List<RaycastResult> results 
    	= new List<RaycastResult>();
    	
	EventSystem.current
 		.RaycastAll(eventDataCurrentPosition, results);
 		
   return results.Count > 0;
} 

```

이렇게 만든 부울함수를 터치할 때 넣어서 체크하면 잘 작동한다.

```cs
//마우스 
if(Input.GetMouseButtonDown(0)) 
{
	if(!IsPointerOverUIObject(Input.mousePosition))
	{  
	         //클릭 처리
	}
}
```

뭐가 문제라 안된껄까....미스터리다.