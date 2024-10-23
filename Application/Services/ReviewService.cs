using System;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFieldRepository _fieldRepository;

        public ReviewService(IReviewRepository reviewRepository, IUserRepository userRepository, IFieldRepository fieldRepository)
        {
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
            _fieldRepository = fieldRepository;
        }

        public Review AddReview(CreateReviewDto createReviewDto)
        {
            var user = _userRepository.GetById(createReviewDto.UserId);
            if (user == null)
                throw new Exception("Usuario no encontrado");

            var field = _fieldRepository.GetFieldById(createReviewDto.FieldId);
            if (field == null)
                throw new Exception("Campo no encontrado");

            

            var review = new Review
            {
                UserId = createReviewDto.UserId,
                FieldId = createReviewDto.FieldId,
                Rating = createReviewDto.Rating,
                Comment = createReviewDto.Comment
            };

            _reviewRepository.AddReview(review);
            return review;
        }

        public void DeleteReview(int id)
        {
             _reviewRepository.DeleteReview(id);
        }

        public IEnumerable<Review> GetAllReviews()
        {
            return _reviewRepository.GetAllReviews();
        }

        public IEnumerable<Review> GetReviewByField(int id)
        {
            return _reviewRepository.GetReviewByField(id);
        }

        public Review GetReviewById(int id)
        {
            return _reviewRepository.GetReviewById(id);
        }

        public IEnumerable<Review> GetReviewByUser(int id)
        {
             return _reviewRepository.GetReviewByUser(id);
        }

        public void UpdateReview(int id, UpdateReviewDto updateReviewDto)
        {
            var review = _reviewRepository.GetReviewById(id);
            if (review == null)
                throw new Exception("No existe la reseña.");

            var field = _fieldRepository.GetFieldById(updateReviewDto.FieldId);
            if (field == null)
                throw new Exception("No existe el campo.");

            

            review.FieldId = updateReviewDto.FieldId;
            review.Rating = updateReviewDto.Rating;
            review.Comment = updateReviewDto.Comment;

            _reviewRepository.UpdateReview(review);
        }
    }

   
}