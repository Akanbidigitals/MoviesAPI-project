using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.DTOs
{
    public class MovieResponseDTO
    {
        public Guid Id { get; set; }
        public  string Title { get; set; }

        public float? Rating { get; init; }
        public int? UserRating { get; init; }

        public  int YearOfRelease { get; set; }
        public  List<string> Genres { get; set; } = new();
    }
}
