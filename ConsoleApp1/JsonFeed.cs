using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1 {
	class JsonFeed {
		/// <summary>Retreive a random joke from the supplied URL</summary>
		/// <exception cref="System.HttpRequestException">Thrown when the response status code is not 200.</exception>
		/// <returns>A string containing a chuck norris joke</returns>
		public static async Task<string> GetRandomJoke () {
			string url = "https://api.chucknorris.io/jokes/random";
			using (HttpClient client = new HttpClient ()) {
				client.BaseAddress = new Uri (url);
				client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));

				try {
					HttpResponseMessage response = await client.GetAsync (client.BaseAddress);
					response.EnsureSuccessStatusCode ();
					string responseBody = await response.Content.ReadAsStringAsync ();
					var joke = JsonConvert.DeserializeObject<dynamic> (responseBody);
					return joke["value"];
				} catch (HttpRequestException e) {
					Console.WriteLine ("\nException Caught!");
					Console.WriteLine ("Message :{0} ", e.Message);
					return "Failed to find a joke! :(";
				}
			}
		}

		/// <summary>
		/// returns an object that contains name and surname
		/// </summary>
		/// <param name="client2"></param>
		/// <exception cref="System.HttpRequestException">Thrown when the response status code is not 200.</exception>
		/// <returns>An object containing name, surname, gender and region properties</returns>
		public static async Task<dynamic> Getname () {
			string url = "http://uinames.com/api/";
			using (HttpClient client = new HttpClient ()) {
				client.BaseAddress = new Uri (url);
				client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));

				try {
					HttpResponseMessage response = await client.GetAsync (client.BaseAddress);
					response.EnsureSuccessStatusCode ();
					string responseBody = await response.Content.ReadAsStringAsync ();
					return JsonConvert.DeserializeObject<dynamic> (responseBody);
				} catch (HttpRequestException e) {
					Console.WriteLine ("\nException Caught!");
					Console.WriteLine (e);
					Console.WriteLine ("Message :{0} ", e.Message);
					return new string[0];
				}
			}
		}
	}
}