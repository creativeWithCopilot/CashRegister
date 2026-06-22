namespace CashRegister.DTOs
{
    /// <summary>
    /// Represents a simple confirmation response returned by the API.
    /// Typically used to acknowledge successful operations or provide
    /// user-friendly messages without additional data payloads.
    /// </summary>
    public class ConfirmationResponseDTO
    {
        /// <summary>
        /// Gets or sets the confirmation message describing the outcome
        /// of the operation (e.g., "Transaction completed successfully").
        /// </summary>
        public string Message { get; set; }
    }
}
