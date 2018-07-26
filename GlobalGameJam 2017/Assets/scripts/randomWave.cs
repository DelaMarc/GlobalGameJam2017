using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomWave : Wave {

	public int count = -1;

	override public GameObject getNext() {
		if (count != 0) {
			if (count > 0)
				--count;
			return objs [(int)Random.Range (0, objs.Length)];
		}
		return null;
	}
}
