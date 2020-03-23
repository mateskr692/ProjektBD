using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;

namespace Buisness.Contracts
{
    public class WResult
    {
        public ResultStatus Status { get; set; }
        public IEnumerable<string> Errors { get; set; }


        public WResult()
        {
        }
        public WResult(string error)
        {
            this.Status = ResultStatus.Failed;
            this.Errors = new string[] { error };
        }
        public WResult( ResultStatus status, string error = null )
        {
            this.Status = status;
            this.Errors = new string[] { error };
        }
    }



    public class WResult<TData> : WResult where TData : class
    {
        public TData Data { get; set; }


        public WResult() : base()
        {
        }
        public WResult( string error ) : base( error )
        {
        }
        public WResult( TData data, string error = null ) : base( ResultStatus.Succeded, error )
        {
            this.Data = data;
        }
        public WResult( TData data, ResultStatus status, string error = null ) : base( status, error )
        {
            this.Data = data;
        }
    }
}
