using UnityEngine;
using System.IO;

public class ExceptionLogger : MonoBehaviour
{
	private StreamWriter streamWriter = null;
	private readonly string logFileName = "log.txt";
	private readonly long maxFileSize = 1048576; // 1MB

	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);

		openLogFile();
	}

	private void OnEnable()
	{
		Application.logMessageReceived += handleLog;
	}

	private void OnDisable()
	{
		Application.logMessageReceived -= handleLog;
	}

	private void OnDestroy()
	{
		streamWriter?.Close();
		streamWriter = null;
	}

	private void openLogFile()
	{
		string logFilePath = $"{Application.persistentDataPath}/{logFileName}";

		// �α� ������ ������ �ִ� ũ�⸦ �ʰ��ϴ��� Ȯ��
		if (File.Exists(logFilePath) && new FileInfo(logFilePath).Length > maxFileSize)
		{
			// ���� ���� �̸��� Ÿ�ӽ������� �߰��Ͽ� �� ������ ����
			string timestamp = System.DateTime.Now.ToString("yyyyMMddHHmmss");
			string newLogFileName = $"log_{timestamp}.txt";
			logFilePath = $"{Application.persistentDataPath}/{newLogFileName}";
		}

		streamWriter = new StreamWriter(logFilePath, true);
		streamWriter.AutoFlush = true; // ��Ʈ�� ���۸� �ڵ����� ���
	}

	private void handleLog(string _logString, string _stackTrace, LogType _type)
	{
		if (!(_type == LogType.Exception || _type == LogType.Error)) return;

		streamWriter.WriteLine($"Logged at : {System.DateTime.Now} / Log Desc : {_logString} / Trace : {_stackTrace} / Type : {_type}\n");
	}
}