Порядок разворачивания:

1. Устанавливаем Connection Strings в файле appsettings.json проекта DateTimeIntervalsServer:
	-  "DefaultConnection": "Data Source=TERMINATOR\\SQLEXPRESS;Initial Catalog=DateTimeIntervals;Integrated Security=True",
	-  "LoggerConnection":  "Data Source=TERMINATOR\\SQLEXPRESS;Initial Catalog=DateTimeIntervalsServerLogs;Integrated Security=True"

	DefaultConnection - конекшн стринг для базы данных основного приложения
	LoggerConnection - конекшн стринг для базы данных логов сервера

2. Устанавливаем applicationUrl в файле launchSettings.json для проектов:
	
	DateTimeIntervalsServer - "http://localhost:5800"
	DateTimeIntervalsLogger - "http://localhost:5900"

3. В проекте DateTimeIntervalsServer открываем Package Manager Console и вводим команды для создания баз данных основного приложения и серверных логов:
	
	 update-database -context DateTimeIntervalContext
	 update-database -context LoggerContext

4. В Microsoft SQL Server Management Studio открываем и запускаем на выполнение скрипт createDatabase.sql из корневого каталога репозитория.
   Он создаёт базу данных и схему для вебсервиса клиентских логов.

5. Также в настройка launchSettings.json можно установить флаг "launchBrowser": false

6. Запускаем все 3 приложения и вводим через консольный клиент команды.
