using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Model
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId {  get; set; }
        [ForeignKey(nameof(Movie))]
        public Guid MovieID {  get; set; }
        public  int Ratings {  get; set; }
    }
}
