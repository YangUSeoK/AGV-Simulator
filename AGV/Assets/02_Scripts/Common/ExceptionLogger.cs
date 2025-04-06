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

		// 로그 파일이 설정한 최대 크기를 초과하는지 확인
		if (File.Exists(logFilePath) && new FileInfo(logFilePath).Length > maxFileSize)
		{
			// 현재 파일 이름에 타임스탬프를 추가하여 새 파일을 생성
			string timestamp = System.DateTime.Now.ToString("yyyyMMddHHmmss");
			string newLogFileName = $"log_{timestamp}.txt";
			logFilePath = $"{Application.persistentDataPath}/{newLogFileName}";
		}

		streamWriter = new StreamWriter(logFilePath, true);
		streamWriter.AutoFlush = true; // 스트림 버퍼를 자동으로 비움
	}

	private void handleLog(string _logString, string _stackTrace, LogType _type)
	{
		if (!(_type == LogType.Exception || _type == LogType.Error)) return;

		streamWriter.WriteLine($"Logged at : {System.DateTime.Now} / Log Desc : {_logString} / Trace : {_stackTrace} / Type : {_type}\n");
	}
}