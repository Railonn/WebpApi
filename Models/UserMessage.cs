using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class UserMessage
    {
        [Key]
        public int MessageId { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
    }
}
