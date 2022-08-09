using (var client = new HttpClient()) //aoutomaticly closes the connection
{
    var endpoint = new Uri("https://jsonplaceholder.typicode.com/posts"); //API URI
    var newPost = new Post
    {
        Title = "Test Post",
        Body = "Hello World",
        UserId = 44
    };//C# model for json object
    var newPostJson = JsonSerializer.Serialize(newPost);//model to json
    var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");//json to http content for posting transactions
    var response = client.PostAsync(endpoint, payload).Result;//posting and response 
    var responseJson = response.Content.ReadAsStringAsync().Result;//json to string
    Console.WriteLine(responseJson);
}

class Post
{
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }

}
