using System;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories

{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddReview(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }

        public void DeleteReview(int id)
        {
            var review = _context.Reviews.Find(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Review> GetAllReviews()
        {
            return _context.Reviews.ToList();
        }

        public IEnumerable<Review> GetReviewByField(int fieldId)
        {
            return _context.Reviews.Where(r => r.FieldId == fieldId).ToList();
        }

        public Review GetReviewById(int id)
        {
            return _context.Reviews.Find(id);
        }

        public IEnumerable<Review> GetReviewByUser(int userId)
        {
            return _context.Reviews.Where(r => r.UserId == userId).ToList();
        }

        public void UpdateReview(Review review)
        {
             _context.Reviews.Update(review);
            _context.SaveChanges();
        }
    }
}