������� ��������������:

1. ������������� Connection Strings � ����� appsettings.json ������� DateTimeIntervalsServer:
	-  "DefaultConnection": "Data Source=TERMINATOR\\SQLEXPRESS;Initial Catalog=DateTimeIntervals;Integrated Security=True",
	-  "LoggerConnection":  "Data Source=TERMINATOR\\SQLEXPRESS;Initial Catalog=DateTimeIntervalsServerLogs;Integrated Security=True"

	DefaultConnection - ������� ������ ��� ���� ������ ��������� ����������
	LoggerConnection - ������� ������ ��� ���� ������ ����� �������

2. ������������� applicationUrl � ����� launchSettings.json ��� ��������:
	
	DateTimeIntervalsServer - "http://localhost:5800"
	DateTimeIntervalsLogger - "http://localhost:5900"

3. � ������� DateTimeIntervalsServer ��������� Package Manager Console � ������ ������� ��� �������� ��� ������ ��������� ���������� � ��������� �����:
	
	 update-database -context DateTimeIntervalContext
	 update-database -context LoggerContext

4. � Microsoft SQL Server Management Studio ��������� � ��������� �� ���������� ������ createDatabase.sql �� ��������� �������� �����������.
   �� ������ ���� ������ � ����� ��� ���������� ���������� �����.

5. ����� � ��������� launchSettings.json ����� ���������� ���� "launchBrowser": false

6. ��������� ��� 3 ���������� � ������ ����� ���������� ������ �������.
