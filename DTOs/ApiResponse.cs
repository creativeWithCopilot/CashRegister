namespace CashRegister.DTOs
{
    /// <summary>
    /// Represents a standardized API response structure for both success and error cases.
    /// </summary>
    /// <typeparam name="T">The type of the response data payload.</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Gets or sets the HTTP status code of the response (e.g., 200, 400, 500).
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the request was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the response data when the request is successful.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Gets or sets the list of error messages when the request fails.
        /// </summary>
        public List<string> Errors { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class
        /// with default values. Marks the response as successful and initializes
        /// an empty error list.
        /// </summary>
        public ApiResponse()
        {
            Success = true;
            Errors = new List<string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class
        /// for successful responses with data.
        /// </summary>
        /// <param name="statusCode">The HTTP status code of the response.</param>
        /// <param name="data">The response data payload.</param>
        public ApiResponse(int statusCode, T data)
        {
            StatusCode = statusCode;
            Success = true;
            Data = data;
            Errors = new List<string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class
        /// for error responses with multiple error messages.
        /// </summary>
        /// <param name="statusCode">The HTTP status code of the response.</param>
        /// <param name="errors">The list of error messages.</param>
        public ApiResponse(int statusCode, List<string> errors)
        {
            StatusCode = statusCode;
            Success = false;
            Errors = errors;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class
        /// for error responses with a single error message.
        /// </summary>
        /// <param name="statusCode">The HTTP status code of the response.</param>
        /// <param name="error">The error message.</param>
        public ApiResponse(int statusCode, string error)
        {
            StatusCode = statusCode;
            Success = false;
            Errors = new List<string> { error };
        }
    }
}
