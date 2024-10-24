using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Presentation.Controllers;
[ApiController]
[Route("api/review")]

public class ReviewController : ControllerBase
{
     private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

        [HttpGet]
        public ActionResult<IEnumerable<ReviewDto>> GetAll()
        {
            var reviews = _reviewService.GetAllReviews();
            var reviewsDTOs = new List<ReviewDto>();
            foreach (var review in reviews)
            {
                reviewsDTOs.Add(new ReviewDto
                {
                    
                Id = review.Id,
                UserId = review.UserId,
                FieldId = review.FieldId,
                Rating = review.Rating,
                Comment = review.Comment
                });
            }
            return Ok(reviewsDTOs);
        }

        [HttpPost]
        public ActionResult<ReviewDto> AddReview(CreateReviewDto createReviewDto)
        {
            

            var review = _reviewService.AddReview(createReviewDto);

            var reviewDto = new ReviewDto
            {
                Id = review.Id,
                UserId = review.UserId,
                FieldId = review.FieldId,
                Rating = review.Rating,
                Comment = review.Comment
            };

            return CreatedAtAction(nameof(GetById), new { id = reviewDto.Id }, reviewDto);
        }
        [HttpGet("{id}")]
        public ActionResult<ReviewDto> GetById(int id)
        {
            var review = _reviewService.GetReviewById(id);
            if (review == null)
            {
                return NotFound();
            }
            var reviewDto = new ReviewDto
            {
                Id = review.Id,
                UserId = review.UserId,
                FieldId = review.FieldId,
                Rating = review.Rating,
                Comment = review.Comment
            };

            return reviewDto;
        }
        [HttpGet("field/{id}")]
        public ActionResult<ReviewDto> GetByField(int id)
        {
            var reviews = _reviewService.GetReviewByField(id);
            var reviewsDTOs = new List<ReviewDto>();
            foreach (var review in reviews)
            {
                reviewsDTOs.Add(new ReviewDto
                {
                    
                Id = review.Id,
                UserId = review.UserId,
                FieldId = review.FieldId,
                Rating = review.Rating,
                Comment = review.Comment
                });
            }
            return Ok(reviewsDTOs);
        }
        [HttpGet("user/{id}")]
        public ActionResult<ReviewDto> GetByUser(int id)
        {
            var reviews = _reviewService.GetReviewByUser(id);
            var reviewsDTOs = new List<ReviewDto>();
            foreach (var review in reviews)
            {
                reviewsDTOs.Add(new ReviewDto
                {
                    
                Id = review.Id,
                UserId = review.UserId,
                FieldId = review.FieldId,
                Rating = review.Rating,
                Comment = review.Comment
                });
            }
            return Ok(reviewsDTOs);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateReview(int id, UpdateReviewDto updateReviewDto)
        {
           
            try
            {
                _reviewService.UpdateReview(id, updateReviewDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteReview(int id)
        {

            _reviewService.DeleteReview(id);
            return NoContent();
        }

}
