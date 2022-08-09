using (var client = new HttpClient()) //aoutomaticly closes the connection
{
    var endpoint = new Uri("https://jsonplaceholder.typicode.com/posts"); //API URI
    var newPost = new Post
    {
        Title = "Test Post",
        Body = "Hello World",
        UserId = 44
    };
    var newPostJson = JsonSerializer.Serialize(newPost);
    var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
    var response = client.PostAsync(endpoint, payload).Result;
    var responseJson = response.Content.ReadAsStringAsync().Result;
    Console.WriteLine(responseJson);
}

class Post
{
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }

}
