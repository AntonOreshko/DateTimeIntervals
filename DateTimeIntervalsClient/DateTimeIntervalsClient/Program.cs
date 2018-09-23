using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using DateTimeIntervals.Dtos.Dtos;
using Newtonsoft.Json;

namespace DateTimeIntervalsClient
{
    class Program
    {
        private static readonly HttpClient Client = new HttpClient();

        private static UserForReturnDto _currentUser;

        private static string _baseUrl = "http://localhost:5800/api/";

        private static string _loggerUrl = "http://localhost:5900/api/";

        static void Main(string[] args)
        {
            Console.WriteLine(" ==================== Intervals Processor ====================");

            CommandsProcessor(CommandType.Help);

            while (true)
            {
                Console.Write("============================ Type command:   ");
                var input = Console.ReadLine();
                var result = Enum.TryParse(input, out CommandType command);
                if (result) CommandsProcessor(command);
                else
                {
                    Error();
                }
            }
        }

        public static void CommandsProcessor(CommandType command)
        {
            switch (command)
            {
                case CommandType.Register:
                    RegisterCommand();
                    break;
                case CommandType.Login:
                    LoginCommand();
                    break;
                case CommandType.Logout:
                    LogoutCommand();
                    break;
                case CommandType.AddInterval:
                    AddIntervalCommand();
                    break;
                case CommandType.GetIntervals:
                    GetIntervalsCommand();
                    break;
                case CommandType.GetIntersection:
                    GetIntersectionCommand();
                    break;
                case CommandType.Help:
                    HelpCommand();
                    break;
                case CommandType.Clear:
                    ClearCommand();
                    break;
            }
        }

        #region Commands

        private static void RegisterCommand()
        {
            if (!CheckLogoutStatus()) return;

            Console.WriteLine();
            Console.Write("Type login:  ");
            var login = Console.ReadLine();
            Console.Write("Type password:  ");
            var password = Console.ReadLine();

            Console.WriteLine();

            var requestDto = new RequestDto
            {
                RequestTime = DateTime.Now,
                RequestProtocol = "HTTP/1.1",
                RequestMethod = "POST",
                RequestBody = JsonConvert.SerializeObject(new UserForRegisterDto {Login = login, Password = password}),
                RequestPath = _baseUrl + "auth/register"
            };


            var registerTask = Client.PostAsync(_baseUrl + "auth/register", new JsonContent(new UserForRegisterDto { Login = login, Password = password }));
            while(!registerTask.IsCompletedSuccessfully) { }

            requestDto.ResponseTime = DateTime.Now;
            requestDto.ResponseCode = (int)registerTask.Result.StatusCode;

            var resultTask = registerTask.Result.Content.ReadAsStringAsync();

            while (!resultTask.IsCompletedSuccessfully) { }

            if (registerTask.Result.StatusCode == HttpStatusCode.Created)
            {
                var registerResult = JsonConvert.DeserializeObject<UserForReturnDto>(resultTask.Result);
                Console.WriteLine("User with login <" + login + "> succesfully created!");
            }
            else
            {
                Console.WriteLine("Unable to create user!");
            }

            requestDto.ResponseBody = resultTask.Result;

            Client.PostAsync(_loggerUrl + "requests", new JsonContent(requestDto));

            Console.WriteLine();
        }

        private static void LoginCommand()
        {
            if (!CheckLogoutStatus()) return;

            Console.WriteLine();
            Console.Write("Type login:  ");
            var login = Console.ReadLine();
            Console.Write("Type password:  ");
            var password = Console.ReadLine();

            Console.WriteLine();

            var requestDto = new RequestDto
            {
                RequestTime = DateTime.Now,
                RequestProtocol = "HTTP/1.1",
                RequestMethod = "POST",
                RequestBody = JsonConvert.SerializeObject(new UserForLoginDto() { Login = login, Password = password }),
                RequestPath = _baseUrl + "auth/login"
            };

            var loginTask = Client.PostAsync(_baseUrl + "auth/login", new JsonContent(new UserForLoginDto { Login = login, Password = password }));
            while (!loginTask.IsCompletedSuccessfully) { }

            requestDto.ResponseTime = DateTime.Now;
            requestDto.ResponseCode = (int)loginTask.Result.StatusCode;

            var resultTask = loginTask.Result.Content.ReadAsStringAsync();

            while (!resultTask.IsCompletedSuccessfully) { }

            if (loginTask.Result.StatusCode == HttpStatusCode.OK)
            {

                var loginResult = JsonConvert.DeserializeObject<UserForReturnDto>(resultTask.Result);
                _currentUser = loginResult;
                Console.WriteLine("Logged in <" + login + "> succesfully!");
            }
            else
            {
                Console.WriteLine("Unable to login!");
            }

            requestDto.ResponseBody = resultTask.Result;

            Client.PostAsync(_loggerUrl + "requests", new JsonContent(requestDto));

            Console.WriteLine();
        }

        private static void LogoutCommand()
        {
            _currentUser = null;
            Client.DefaultRequestHeaders.Authorization = null;

            Console.WriteLine();
            Console.WriteLine("Successfully logged out!");
            Console.WriteLine();
        }

        private static void AddIntervalCommand()
        {
            if (!CheckLoginStatus()) return;

            Console.WriteLine();
            var begin = InputDate("Type Begin Date:  ");
            var end = InputDate("Type End Date:  ");
            Console.WriteLine();

            var requestDto = new RequestDto
            {
                RequestTime = DateTime.Now,
                RequestProtocol = "HTTP/1.1",
                RequestMethod = "POST",
                RequestBody = JsonConvert.SerializeObject(new JsonContent(new DateTimeIntervalForCreationDto { Begin = begin, End = end })),
                RequestPath = _baseUrl + "intervals"
            };

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _currentUser.Token);
            var addIntervalTask = Client.PostAsync(_baseUrl + "intervals/" + _currentUser.Id, new JsonContent(new DateTimeIntervalForCreationDto { Begin = begin, End = end }));
            while (!addIntervalTask.IsCompletedSuccessfully) { }

            requestDto.ResponseTime = DateTime.Now;
            requestDto.ResponseCode = (int)addIntervalTask.Result.StatusCode;

            var resultTask = addIntervalTask.Result.Content.ReadAsStringAsync();

            while (!resultTask.IsCompletedSuccessfully) { }

            if (addIntervalTask.Result.StatusCode == HttpStatusCode.Created)
            {
                Console.WriteLine("Interval succesfully added!");
            }
            else
            {
                Console.WriteLine("Unable to create interval!");
            }

            requestDto.ResponseBody = resultTask.Result;

            Client.PostAsync(_loggerUrl + "requests", new JsonContent(requestDto));

            Console.WriteLine();
        }

        private static void GetIntervalsCommand()
        {
            if (!CheckLoginStatus()) return;

            var requestDto = new RequestDto
            {
                RequestTime = DateTime.Now,
                RequestProtocol = "HTTP/1.1",
                RequestMethod = "GET",
                RequestBody = null,
                RequestPath = _baseUrl + "intervals/" + _currentUser.Id + "/all"
            };

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _currentUser.Token);
            var getIntervalsTask = Client.GetAsync(_baseUrl + "intervals/" + _currentUser.Id + "/all");
            while (!getIntervalsTask.IsCompletedSuccessfully) { }

            requestDto.ResponseTime = DateTime.Now;
            requestDto.ResponseCode = (int)getIntervalsTask.Result.StatusCode;

            var resultTask = getIntervalsTask.Result.Content.ReadAsStringAsync();
            while (!resultTask.IsCompletedSuccessfully) { }

            if (getIntervalsTask.Result.StatusCode == HttpStatusCode.OK)
            {

                var result = JsonConvert.DeserializeObject<List<DateTimeIntervalForReturnDto>>(resultTask.Result);

                Console.WriteLine();
                Console.WriteLine("Result:");
                foreach (var interval in result)
                {
                    Console.WriteLine(interval.ToString());
                }
                Console.WriteLine();

            }
            else
            {
                Console.WriteLine("Unable to get intervals!");
            }

            requestDto.ResponseBody = resultTask.Result;

            Client.PostAsync(_loggerUrl + "requests", new JsonContent(requestDto));
        }

        private static void GetIntersectionCommand()
        {
            if (!CheckLoginStatus()) return;

            Console.WriteLine();
            var begin = InputDate("Type Begin Date:  ");
            var end = InputDate("Type End Date:  ");
            Console.WriteLine();

            var requestDto = new RequestDto
            {
                RequestTime = DateTime.Now,
                RequestProtocol = "HTTP/1.1",
                RequestMethod = "POST",
                RequestBody = JsonConvert.SerializeObject(new JsonContent(new DateTimeIntervalForIntersectionDto { Begin = begin, End = end })),
                RequestPath = _baseUrl + "intervals/" + _currentUser.Id + "/intersection"
            };

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _currentUser.Token);
            var getIntersectionTask = Client.PostAsync(_baseUrl + "intervals/" + _currentUser.Id + "/intersection", new JsonContent(new DateTimeIntervalForIntersectionDto { Begin = begin, End = end }));
            while (!getIntersectionTask.IsCompletedSuccessfully) { }

            requestDto.ResponseTime = DateTime.Now;
            requestDto.ResponseCode = (int)getIntersectionTask.Result.StatusCode;

            var resultTask = getIntersectionTask.Result.Content.ReadAsStringAsync();
            while (!resultTask.IsCompletedSuccessfully) { }

            if (getIntersectionTask.Result.StatusCode == HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<List<DateTimeIntervalForReturnDto>>(resultTask.Result);
                Console.WriteLine("Result:");
                foreach (var interval in result)
                {
                    Console.WriteLine(interval.ToString());
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Unable to get intersection!");
            }

            requestDto.ResponseBody = resultTask.Result;

            Client.PostAsync(_loggerUrl + "requests", new JsonContent(requestDto));

            Console.WriteLine();
        }

        private static void HelpCommand()
        {
            Console.WriteLine();
            Console.WriteLine("List of commands:");
            Console.WriteLine();

            Console.WriteLine("- Register           : New user registration");
            Console.WriteLine("- Login              : Exsisted user authorization");
            Console.WriteLine("- Logout             : Remove strored credentials for current user");
            Console.WriteLine("- AddInterval        : Add new interval for current user");
            Console.WriteLine("- GetIntervals       : Get all intervals for current user");
            Console.WriteLine("- GetIntersection    : Get intersection of intervals for current user");
            Console.WriteLine("- Help               : List of all commands");
            Console.WriteLine("- Clear              : Console clearing");

            Console.WriteLine();
        }

        private static void ClearCommand()
        {
            Console.Clear();
        }

        private static void Error()
        {
            Console.WriteLine();
            Console.WriteLine("Wrong input! Use 'Help' command to see all avaliable commands!");
            Console.WriteLine();
        }

        private static DateTime InputDate(string message)
        {
            bool result;
            DateTime dateTime;
            do
            {
                Console.Write(message);
                var date = Console.ReadLine();
                result = DateTime.TryParse(date, out dateTime);
                if (!result)
                {
                    Console.WriteLine();
                    Console.WriteLine("Wrong date format!");
                    Console.WriteLine();
                }
            }
            while (!result);

            return dateTime;
        }

        private static bool CheckLoginStatus()
        {
            if (_currentUser == null)
            {
                Console.WriteLine();
                Console.WriteLine("Please, log in!");
                Console.WriteLine();

                return false;
            }

            return true;
        }

        private static bool CheckLogoutStatus()
        {
            if (_currentUser != null)
            {
                Console.WriteLine();
                Console.WriteLine("Please, log out!");
                Console.WriteLine();

                return false;
            }

            return true;
        }

        #endregion
    }

    public class JsonContent : StringContent
    {
        public JsonContent(object obj) :
            base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
        { }
    }

    public enum CommandType
    {
        Register,
        Login,
        Logout,
        AddInterval,
        GetIntervals,
        GetIntersection,
        Help,
        Clear
    }
}
