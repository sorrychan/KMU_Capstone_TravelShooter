using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	public Easing.Type easing;

	void Start () {
		Camera.main.FadeIn(5f, easing);         //페이드 인
        //Camera.main.FadeOut(5f, easing);      //페이드 아웃
    }  
}
