﻿namespace Panner.AspNetCore.Samples.AspNetCore3_0.EFModel
{
    using System;

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsVisible { get; set; }
        public int AmtLikes { get; set; }
        public int AmtComments { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
