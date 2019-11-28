using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cocktail_Magician_DB.Models
{
    public class BarReview
    {
        public BarReview()
        {

        }
        public BarReview(double grade, string comment, string userId, string barId, DateTime createdO)
        {
            Grade = grade;
            Comment = comment;
            UserId = userId;
            BarId = barId;
            CreatedOn = createdO;
        }
        public double Grade { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string BarId { get; set; }
        public Bar Bar { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }
    }
}
