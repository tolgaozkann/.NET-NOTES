using (var client = new HttpClient())//aoutomaticly closes the connection
{
    var endpoint = new Uri("https://jsonplaceholder.typicode.com/posts");//API URI
    var result = client.GetAsync(endpoint).Result;//HttpResponseMessage
    var json = result.Content.ReadAsStringAsync().Result;//json as a string
    Console.WriteLine(json);
}
