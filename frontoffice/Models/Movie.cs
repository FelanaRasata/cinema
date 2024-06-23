﻿using System.ComponentModel.DataAnnotations;

namespace backoffice.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Duration { get; set; }

        public string Category { get; set; }
    }
}