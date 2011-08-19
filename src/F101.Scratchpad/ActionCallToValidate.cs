namespace F101.Scratchpad
{

    public class PostsController
    {
        public JsonResponse Create(Post post)
        {
            // do stuff
            return new JsonResponse {Success = true};
        }
    }

    public class JsonResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class Post
    {
        public string Title { get; set; }
        public string Body { get; set; }
    }
}