using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {
	public GameObject[] objs;
	private int index = 0;

	public void reset() {
		index = 0;
	}

	virtual public GameObject getNext() {
		if (index < objs.Length) {
			return objs[index++];
		}
		return (null);
	}
}
