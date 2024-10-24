using System;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IReviewRepository
    {
        IEnumerable<Review> GetAllReviews();
        Review GetReviewById(int id);
        IEnumerable<Review> GetReviewByField(int id);
        IEnumerable<Review> GetReviewByUser(int id);
        void AddReview(Review review);
        void UpdateReview(Review review);
        void DeleteReview(int id);
    }
}