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
            try {
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
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult<ReviewDto> AddReview([FromBody]CreateReviewDto createReviewDto)
        {
            
            try{
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
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ReviewDto> GetById([FromRoute]int id)
        {
            try {
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
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("field/{id}")]
        public ActionResult<ReviewDto> GetByField([FromRoute]int id)
        {
            try {
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
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("user/{id}")]
        public ActionResult<ReviewDto> GetByUser([FromRoute]int id)
        {
            try {
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
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult UpdateReview([FromRoute]int id,[FromBody] UpdateReviewDto updateReviewDto)
        {
           
            try
            {
                var user = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var review = _reviewService.GetReviewById(id);
                if (user == review.UserId)
                {
                _reviewService.UpdateReview(id, updateReviewDto);
                return NoContent();
                }
                else{
                    return BadRequest($"Solo puede modificar las reseñas del usuario {user} ");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteReview([FromRoute]int id)
        {
            try {
            _reviewService.DeleteReview(id);
            return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

}
