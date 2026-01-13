namespace Cash.Models
{

    public class ApiResponse<T> : ApiResponse
    {
        public T? Value { get; set; } = default;

        #region FailureConstructors
        public static new ApiResponse<T> Factory(Exception ex) => Factory(ProblemDetails.Factory(ex));

        public static new ApiResponse<T> Factory(ProblemDetails problemDetails)
        {
            return new ApiResponse<T>()
            { ProblemDetails = problemDetails };
        }
        public static new ApiResponse<T> Factory(string errorReason)
        {
            return new ApiResponse<T>()
            { ProblemDetails = new ProblemDetails { Title = errorReason } };
        }

        #endregion

    }

    public class ApiResponse
    {
        public ProblemDetails? ProblemDetails { get; set; }

        #region FailureConstructors
        public static ApiResponse Factory(Exception ex) => Factory(ProblemDetails.Factory(ex));

        public static ApiResponse Factory(ProblemDetails problemDetails)
        {
            return new ApiResponse()
            { ProblemDetails = problemDetails };
        }
        public static ApiResponse Factory(string errorReason)
        {
            return new ApiResponse()
            { ProblemDetails = new ProblemDetails { Title = errorReason } };
        }

        #endregion

        public bool Fail() => !Success();
        public bool Success() => ProblemDetails == null;
    }

    public class ProblemDetails
    {
        public string? Title { get; set; }
        public string? Detail { get; set; }
        public System.Exception? Exception { get; set; }

        public Dictionary<string, string[]> Errors { get; set; } = new();


        public static ProblemDetails Factory(Exception ex) => new ProblemDetails
        {
            Exception = ex,
            Title = ex.Message,
            Detail = ex.InnerException?.Message
        };

        public bool MoreInfoAvailable() => Exception != null | !string.IsNullOrEmpty(Detail);

        public string Details()
        {
            var sb = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(Detail)) sb.AppendLine(Detail);
            foreach (var err in Errors)
            {
                sb.AppendLine(err.Key.ToString());
                foreach (var val in err.Value)
                {
                    sb.AppendLine(val.ToString());
                }
            }
            return sb.ToString();
        }

        public Exception ToException(string[]? segments = null)
        {
            if (segments != null)
                Detail = string.Join("/", segments) + "<br/>" + Detail;
            if (string.IsNullOrEmpty(Detail))
                return new Exception(Title);
            else if (Errors?.Count > 0)
            {
                foreach (var error in Errors)
                {
                    Detail += "<br/>" + error.Key + ": " + string.Join("-", error.Value);
                }
                return new Exception(Title, new Exception(Detail));
            }
            else
                return new Exception(Title, new Exception(Detail));
        }
    }

    public class ProblemDetailsArgs : EventArgs
    {
        public ProblemDetails? Value { get; set; }

        public ProblemDetailsArgs(ProblemDetails? value)
        {
            Value = value;
        }
    }

}
