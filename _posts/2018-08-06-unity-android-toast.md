---

title: "[유니티] 유니티에서 안드로이드 토스트 메세지 띄우기"

comments: true

categories:

  - Record

tags:

  - Unity
  - Android

---


안드로이드 폰을 사용하면 하단에 작은 메세지 창이 뜨는 것을 본 적이 있을 것이다.

![토스트](http://cfile232.uf.daum.net/image/2454F53F55593CFB2ACCCA)

이러한 메세지를 **토스트 메세지**라고 한다. 안드로이드의 네이티브 기능을 이용하는 것으로, 간단한 메세지를 별도의 구현 없이 보여주기에 적합하다.

유니티에서는 에디터 상에서 직접 네이티브 기능을 건드릴 수 없지만, 스크립트를 통해 토스트 메세지를 보여주는 방법이 있다.

```cs
using UnityEngine;

public class AndroidSet : MonoBehaviour
{

#if UNITY_ANDROID

    static public AndroidToast instance;

    AndroidJavaObject currentActivity;
    AndroidJavaClass UnityPlayer;
    AndroidJavaObject context;
    AndroidJavaObject toast;


    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        UnityPlayer = 
        	new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        
        currentActivity = UnityPlayer
        	.GetStatic<AndroidJavaObject>("currentActivity");
        	
        	
        context = currentActivity
        	.Call<AndroidJavaObject>("getApplicationContext");
        	
        DontDestroyOnLoad(this.gameObject);
    }

    void ShowToast(string message)
    {
        currentActivity.Call
        (
	        "runOnUiThread", 
	        new AndroidJavaRunnable(() =>
	        {
	            AndroidJavaClass Toast 
	            = new AndroidJavaClass("android.widget.Toast");
	            
	            AndroidJavaObject javaString 
	            = new AndroidJavaObject("java.lang.String", message);
	            
	            toast = Toast.CallStatic<AndroidJavaObject>
	            (
	            	"makeText", 
	            	context, 
	            	javaString, 
	            	Toast.GetStatic<int>("LENGTH_SHORT")
	            );
	            
	            toast.Call("show");
	        })
	     );
    }

    public void CancelToast()
    {
        currentActivity.Call("runOnUiThread", 
        	new AndroidJavaRunnable(() =>
	        {
	            if (toast != null) toast.Call("cancel");
	        }));
    }


#else
    void Awake()
    {
        Destroy(gameObject);
    }
#endif

}


```

네이티브는 잘 모르기 때문에 구체적인 작동원리는 나중에 보도록 하고, 위 클래스를 적용한 후 `ShowToast()`를 사용하면 토스트를 발생시킨다. 토스트를 적용한 후 `CancleToast()`를 써주면 토스트가 꺼진다.