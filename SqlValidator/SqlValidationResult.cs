using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlValidator
{
    public class SqlValidationResult
    {
        public SqlValidationResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
        public SqlValidationResult(List<string> errors)
        {
            Errors = errors;
            IsSuccess = false;
        }

        public List<string> Errors { get; internal set; } = new List<string>();
        public bool IsSuccess { get; internal set; } 
    }
}
