using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Bogus;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Databrute
{
	class Program
	{
		static readonly HttpClient client = new HttpClient();

		static async Task Main(string[] args)
		{
			var sw = new Stopwatch();
			sw.Start();
			const int defaultCount = 100;
			int count = defaultCount;

			var argList = new List<string>(args);

			var countArg = argList.FirstOrDefault(x => x.StartsWith("--count="));
			if (countArg != null)
			{
				if (int.TryParse(countArg.Split("=")[1], out var parsedCount))
				{
					count = parsedCount;
				}
			}

			var sendRequest = false;
			var urlArg = argList.FirstOrDefault(x => x.StartsWith("--url="));
			var url = string.Empty;
			if (urlArg != null)
			{
				url = urlArg.Split("=")[1];
				sendRequest = !string.IsNullOrWhiteSpace(url);
			}

			var formatted = argList.Any(x => x.Equals("--formatted"));
			var parallel = argList.Any(x => x.Equals("--parallel"));

			var colors = new[] { "black", "red", "brown", "white" };
			var testUsers = new Faker<User>()
				.RuleFor(u => u.Gender, f => f.PickRandom<Bogus.DataSets.Name.Gender>())
				.RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName(u.Gender))
				.RuleFor(u => u.LastName, (f, u) => f.Name.LastName(u.Gender))
				.RuleFor(u => u.AvatarUrl, f => f.Internet.Avatar())
				.RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
				.RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
				.RuleFor(u => u.Age, f => f.Random.Int(18, 65))
				.RuleFor(u => u.HairColor, f => f.PickRandom(colors))
				.RuleFor(u => u.Name, (f, u) => new UserFirstLastName() { First = u.FirstName, Last = u.LastName })
				.RuleFor(u => u.FullName, (f, u) => $"{u.FirstName} {u.LastName}")
				.RuleFor(u => u.Company, f => f.Company.CompanyName())
				.RuleFor(u => u.Phone, f => f.Phone.PhoneNumber())
				.RuleFor(u => u.Address, f => f.Address.FullAddress())
				.FinishWith((f, u) =>
					{
						Console.WriteLine("User Created! Id={0}", u.UserId);
					});

			var success = 0;
			if (parallel)
			{
				var tasks = Enumerable.Range(0, count).Select(i => CreateUser(url, formatted, testUsers, sendRequest, success));
				var results = (await Task.WhenAll(tasks)).Select(x => x).ToList();
				success = results.Count(x => x);
			}
			else
			{
				for (int i = 0; i < count; i++)
				{
					var result = await CreateUser(url, formatted, testUsers, sendRequest, success);
					if (result) success++;
				}
			}
			sw.Stop();
			if (success == 0)
				Console.WriteLine($"Generated {count} users in {sw.Elapsed} time.");
			else
				Console.WriteLine($"Generated {count} users and sent {success} successfull request in {sw.Elapsed} time.");
		}

		private static async Task<bool> CreateUser(string url, bool formatted, Faker<User> testUsers, bool sendRequest, int success)
		{
			var user = testUsers.Generate();
			var body = JsonConvert.SerializeObject(user, formatted ? Formatting.Indented : Formatting.None);
			Console.WriteLine(body);
			if (!sendRequest) return true;
			var isDone = await SendRequest(url, body);
			if (isDone) return true;
			return false;
		}

		private static async Task<bool> SendRequest(string url, string body)
		{
			Console.WriteLine($"Sending request to {url}");
			try
			{
				var content = new StringContent(body, Encoding.UTF8, "application/json");
				var request = await client.PostAsync(url, content);
				Console.WriteLine($"Request is completed with {request.StatusCode}");
				return request.IsSuccessStatusCode;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Request is completed with error : {ex.Message}");
				return false;
			}
		}
	}
}
