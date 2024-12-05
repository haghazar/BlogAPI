﻿using BlogApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApi.Services.DTO
{
    public sealed class TagDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        //public IEnumerable<PostDTO> PostTags { get; set; } = [];
    }
}
