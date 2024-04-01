using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Test : MonoBehaviour
{
	private readonly float origFloat = 5f;

	[ContextMenu("test")]
	public void JsonTest()
	{
		// �� ó�� float ��
		Debug.Log(origFloat);

		// Float ��ü�� �״�� json���� ���� �õ�. 
		// JsonUtility.ToJson �� ������ �ȵǰ�, ���� null �� ��
		// JsonConvert.SerializeObject �� "5.0"���� ��ȯ���ش�.
		string jsonFloat = JsonConvert.SerializeObject(origFloat);
		Debug.Log($"jsonFloat : {jsonFloat}");

		// "5.0" �� float ���� �Ľ�
		float parsedFloat = JsonConvert.DeserializeObject<float>(jsonFloat);
		Debug.Log($"parsedFloat : {parsedFloat}");

		// 5.0f �� json���� ��ȯ
		string jsonFloat2 = JsonConvert.SerializeObject(5.0f);
		Debug.Log($"jsonFloat2 : {jsonFloat2}");

		// ���� ���� ����. "5.0" �� float ���� �Ľ�
		float parsedFloat2 = JsonConvert.DeserializeObject<float>(jsonFloat);
		Debug.Log($"parsedFloat2 : {parsedFloat2}");

		// �͸� ������ ��ü ����
		var objFloat = new { floatt = origFloat };
		Debug.Log($"objFloat : {objFloat}");

		// �͸� ������ ��ü�� json ���� ��ȯ
		string jsonObj = JsonConvert.SerializeObject(objFloat);
		Debug.Log($"jsonObj : {jsonObj}");

		// ParsingObject ��� �Ľ̿� Ŭ������ �Ľ��ؼ� ���� �޾ƿ´�.
		// �� ��, ParsingObject �� ������� floatt��
		// �͸����� ��ü objFloat �� ��� floatt �� �̸��� ���ƾ� �Ѵ�.
		ParsingObject jsonObject = JsonConvert.DeserializeObject<ParsingObject>(jsonObj);
		Debug.Log($"jsonObject.floatt : {jsonObject.floatt}");

		// �͸� ������ ��ü�� json���� ��ȯ�� ��,
		// �װ� JObject�� �Ľ� �ϰ� JObject ���� ������ Ű/����� �̾ƿ��� ��� 
		JObject jObj = JObject.Parse(jsonObj);
		float jsonFloat3 = jObj.Value<float>("origFloat");
		Debug.Log($"jsonFloat3 : {jsonFloat3}");



	}
}
public class ParsingObject
{
	public float floatt;
}