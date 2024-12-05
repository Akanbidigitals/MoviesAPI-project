using System.Globalization;

namespace Movies.Api
{
    public class ApiEndpoints
    {
        private const string ApiBase = "api";

        public static class Movies
        {
            private const string Base = $"{ApiBase}/movies";

            public const string Create = Base;
            public const string Get = $"{Base}/{{idOrSlug}}";
            public const string Update = $"{Base}/{{id:guid}}";
            public const string Delete = $"{Base}/{{id:guid}}";
            public const string GetAll = Base ;

            public const string Rate = $"{Base}/{{id:guid}}/ratings";
            public const string DeleteRating = $"{Base}/{{id:guid}}/ratings";
        }

        public static class Ratings
        {
            private const string Base = $"{ApiBase}/ratings";
            public const string GetUserRatings = $"{Base}/me";
        }


        public static class User
        {
            private const string UserBase = $"{ApiBase}/users";

            public const string Create = UserBase;
            public const string Getall = UserBase;
            public const string Get = $"{UserBase}/{{id:guid}}";

        }
    }
}
