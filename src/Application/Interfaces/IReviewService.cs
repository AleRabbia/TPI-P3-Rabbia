using System;
using Application.Models;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IReviewService
    {
         IEnumerable<Review> GetAllReviews();
        Review GetReviewById(int id);
        IEnumerable<Review> GetReviewByField(int id);
        IEnumerable<Review> GetReviewByUser(int id);
        Review AddReview(CreateReviewDto createReviewDto);
        void UpdateReview(int id, UpdateReviewDto updateReviewDto);
        void DeleteReview(int id);
    }
}


