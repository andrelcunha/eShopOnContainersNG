using System.Collections.Generic;

namespace ALC.WebApp.MVC.Models
{
    public class ErrorViewModel
    {
        public int ErroCode { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

    public class ResponseResult
    {
        public string Title { get; set; } = string.Empty;
        public int Status { get; set; }
        public ResponseErrorMessages? Errors { get; set; }
    }

    public class ResponseErrorMessages
    {
        public List<string>? Messages { get; set; } 
    }
}
