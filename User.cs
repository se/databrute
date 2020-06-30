using System;
using Newtonsoft.Json;
using static Bogus.DataSets.Name;

namespace Databrute
{
	public class User
	{
		[JsonProperty("userId")]
		public Guid UserId { get; set; }
		[JsonProperty("isActive")]
		public bool IsActive { get; set; }
		[JsonProperty("firstName")]
		public string FirstName { get; set; }
		[JsonProperty("lastName")]
		public string LastName { get; set; }
		[JsonProperty("avatarUrl")]
		public string AvatarUrl { get; set; }
		[JsonProperty("age")]
		public int Age { get; set; }
		[JsonProperty("hairColor")]
		public string HairColor { get; set; }
		[JsonProperty("name")]
		public UserFirstLastName Name { get; set; }
		[JsonProperty("userName")]
		public string UserName { get; set; }
		[JsonProperty("company")]
		public string Company { get; set; }
		[JsonProperty("email")]
		public string Email { get; set; }
		[JsonProperty("phone")]
		public string Phone { get; set; }
		[JsonProperty("address")]
		public string Address { get; set; }
		[JsonProperty("gender")]
		public Gender Gender { get; set; }
		[JsonProperty("fullName")]
		public string FullName { get; set; }

		public User()
		{
			UserId = Guid.NewGuid();
		}
	}

	public class UserFirstLastName
	{
		[JsonProperty("first")]
		public string First { get; set; }
		[JsonProperty("last")]
		public string Last { get; set; }
	}
}