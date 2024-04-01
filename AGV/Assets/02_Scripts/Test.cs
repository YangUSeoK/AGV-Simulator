using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Test : MonoBehaviour
{
	private readonly float origFloat = 5f;

	[ContextMenu("test")]
	public void JsonTest()
	{
		// 맨 처음 float 값
		Debug.Log(origFloat);

		// Float 자체를 그대로 json으로 변경 시도. 
		// JsonUtility.ToJson 은 변경이 안되고, 값이 null 로 들어감
		// JsonConvert.SerializeObject 는 "5.0"으로 변환해준다.
		string jsonFloat = JsonConvert.SerializeObject(origFloat);
		Debug.Log($"jsonFloat : {jsonFloat}");

		// "5.0" 을 float 으로 파싱
		float parsedFloat = JsonConvert.DeserializeObject<float>(jsonFloat);
		Debug.Log($"parsedFloat : {parsedFloat}");

		// 5.0f 를 json으로 변환
		string jsonFloat2 = JsonConvert.SerializeObject(5.0f);
		Debug.Log($"jsonFloat2 : {jsonFloat2}");

		// 위와 내용 같음. "5.0" 을 float 으로 파싱
		float parsedFloat2 = JsonConvert.DeserializeObject<float>(jsonFloat);
		Debug.Log($"parsedFloat2 : {parsedFloat2}");

		// 익명 형식의 객체 생성
		var objFloat = new { floatt = origFloat };
		Debug.Log($"objFloat : {objFloat}");

		// 익명 형식의 객체를 json 으로 변환
		string jsonObj = JsonConvert.SerializeObject(objFloat);
		Debug.Log($"jsonObj : {jsonObj}");

		// ParsingObject 라는 파싱용 클래스로 파싱해서 값을 받아온다.
		// 이 때, ParsingObject 의 멤버변수 floatt와
		// 익명형식 객체 objFloat 의 멤버 floatt 의 이름이 같아야 한다.
		ParsingObject jsonObject = JsonConvert.DeserializeObject<ParsingObject>(jsonObj);
		Debug.Log($"jsonObject.floatt : {jsonObject.floatt}");

		// 익명 형식의 객체를 json으로 변환한 후,
		// 그걸 JObject로 파싱 하고 JObject 에서 각각의 키/밸류를 뽑아오는 방법 
		JObject jObj = JObject.Parse(jsonObj);
		float jsonFloat3 = jObj.Value<float>("origFloat");
		Debug.Log($"jsonFloat3 : {jsonFloat3}");



	}
}
public class ParsingObject
{
	public float floatt;
}