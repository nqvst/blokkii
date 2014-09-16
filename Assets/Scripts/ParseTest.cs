using UnityEngine;
using System.Collections;
using Parse;

public class ParseTest : MonoBehaviour {

	public string testString = "test";
	public bool testSaveNow = false;


	void Update () {
		if(testSaveNow){
			testSaveNow = false;
			saveTestData();
		}
	}

	void saveTestData () {
		ParseObject testObject = new ParseObject("TestObject");
		testObject["foo"] = testString;
		testObject.SaveAsync();
	}
}
