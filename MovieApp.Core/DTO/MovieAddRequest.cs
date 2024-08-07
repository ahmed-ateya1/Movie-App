﻿using Microsoft.AspNetCore.Http;
using MovieApp.Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.DTO
{
    public class MovieAddRequest
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Release Date is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public DateTime? ReleaseDate { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        [Range(0, 10, ErrorMessage = "Rating must be between 0 and 10")]
        public double? Rating { get; set; }
        public string? ImageURL { get; set; }
        [Required]
        public IFormFile? Poster {  get; set; }

        [Required(ErrorMessage = "Genre is required")]
        public Guid? GenreID { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is MovieAddRequest request &&
                   Title == request.Title &&
                   Description == request.Description &&
                   ReleaseDate == request.ReleaseDate &&
                   Rating == request.Rating &&
                   ImageURL == request.ImageURL &&
                   EqualityComparer<Guid?>.Default.Equals(GenreID, request.GenreID);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Description, ReleaseDate, Rating, ImageURL, GenreID);
        }

        public Movie ToMovie()
        {
            return new Movie()
            {
                Description = Description,
                Title = Title,
                ReleaseDate = ReleaseDate,
                Rating = Rating,
                GenreID = GenreID,
                ImageURL = ImageURL,
            };
        }
    }
}
