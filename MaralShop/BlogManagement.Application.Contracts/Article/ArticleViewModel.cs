﻿namespace BlogManagement.Application.Contracts.Article
{
    public class ArticleViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Picture { get; set; }
        public string ShortDescription { get; set; }
        public long ArticleCategoryId { get; set; }
        public string ArticleCategory { get; set; }
        public string CreationDate { get; set; }

    }
}
