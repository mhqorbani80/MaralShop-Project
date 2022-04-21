﻿using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contracts.Article
{
    public class CreateArticle
    {
        public string Title { get;  set; }
        public string ShortDescription { get;  set; }
        public string Description { get;  set; }
        public IFormFile? Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        public string Slug { get;  set; }
        public string Keywords { get;  set; }
        public string MetaDescription { get;  set; }
        public string CanonicalAddress { get;  set; }
        public long ArticleCategoryId { get;  set; }
        public List<ArticleCategoryViewModel> ArticleCategories { get; set; }
        public string CreationDate {get;  set; }
    }
}
