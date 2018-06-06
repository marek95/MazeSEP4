using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseUI : MonoBehaviour {

	Animator anim;

	void Start () {
		anim = GetComponent<Animator>();
	}

	public void Show() {
		anim.Play("slideDown");
	}

	public void hide() {
		anim.Play("slideUp");
	}
}
