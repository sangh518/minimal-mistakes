---

title: "[유니티] 가속도센서로 2D 중력 제어하기"

comments: true

categories:

  - Record

tags:

  - Unity
  - Physics

---
![test](/assets/images/posts/bookmark/shit.jpg)


스마트폰을 기울이면 그에 맞춰서 안에 있는 물체들이 움직이도록 자이로센서를 이용해서 구현해보려고 이런 저런 뻘짓을 하다가 실패했다. [Input.gyro](https://docs.unity3d.com/kr/current/ScriptReference/Input-gyro.html)를 이용하면 구현 가능할 것 같았는데 VR같은 경우는 3D라 쉽게 적용이 가능했지만 2D 같은 경우에는 [Quaternion](https://docs.unity3d.com/kr/current/ScriptReference/Quaternion.html)이 어쩌구 저쩌구 하면서 특정 각도만 제어하는게 상당히 어려웠다.


그래서 열심히 구글링을 한 결과, 자이로센서 대신 가속도센서를 이용해서 간단하게 구현하는 방법을 알아냈다.

```cs
void FixedUpdate()
{

	float gx = Input.acceleration.x * 9.81f;
	float gy = Input.acceleration.y * 9.81f;
	Physics2D.gravity = new Vector2(gx, gy);

}

```

[Input.acceleration](https://docs.unity3d.com/kr/530/ScriptReference/Input-acceleration.html)를 사용하면 이렇게나 간단하게 해결이 가능하다.
실제로도 매우 잘 작동하는 중....

![나는 대체 무엇을 위해](http://jjalbang.today/jjN4.jpg)

**오늘의 결론 : 역시 구글은 모든 것을 알고있다.**