namespace Panner.AspNetCore.Samples.AspNetCore2_2.EFModel
{
    using System;

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
