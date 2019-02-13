using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutomaticSlotSelection : MonoBehaviour {

	void Start () {

        Vector2 artworkOriginalSize = transform.Find("artwork").GetComponent<RectTransform>().sizeDelta;

        int random = Random.Range(0, SmashCSS.instance.characters.Count - 1);

        Character randomChar = SmashCSS.instance.characters[random];

        SmashCSS.instance.ShowCharacterInSlot(transform.GetSiblingIndex(), randomChar);

        transform.Find("artwork").GetComponent<RectTransform>().sizeDelta = artworkOriginalSize;
        transform.Find("artwork").GetComponent<RectTransform>().sizeDelta *= randomChar.zoom;
    }
	
}
