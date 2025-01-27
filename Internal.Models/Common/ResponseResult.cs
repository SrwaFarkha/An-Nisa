using Microsoft.AspNetCore.Mvc;
using SharedModels.Enums;
using System.Net;

namespace Internal.Models.Common
{
    public interface IResponseResult : IActionResult
	{
		bool IsSuccessful { get; }

		ICollection<ApiError> Errors { get; }
	}

	public interface IResponseResult<T> : IResponseResult
	{
		T ResponseObject { get; set; }
	}

	public class ResponseResult<T> : ResponseResult, IResponseResult<T>
	{
		public T ResponseObject { get; set; }

		public ResponseResult()
		{
		}

		public ResponseResult(Enums.ApiErrorCode apiErrorCode) : base(apiErrorCode)
		{
		}

		public ResponseResult(params ApiError[] apiErrors) : base(apiErrors)
		{
		}

		public ResponseResult(T responseObject) => ResponseObject = responseObject;
		protected override int StatusCode => !IsSuccessful ? (int)HttpStatusCode.BadRequest : ResponseObject != null ? (int)HttpStatusCode.OK : (int)HttpStatusCode.NoContent;

		public override Task ExecuteResultAsync(ActionContext context)
		{
			var result = new ObjectResult((object)ResponseObject ?? Errors)
			{
				StatusCode = StatusCode
			};
			return result.ExecuteResultAsync(context);
		}
	}

	public class ResponseResult : IResponseResult
	{
		public bool IsSuccessful => !Errors.Any();

		public ResponseResult(params ApiError[] apiErrors) => Errors = apiErrors.ToList();

        public ResponseResult(Enums.ApiErrorCode apiErrorCode)
        {
            ApiErrorCode = apiErrorCode;
        }

        public virtual Task ExecuteResultAsync(ActionContext context)
		{
			ActionResult result = IsSuccessful ? new NoContentResult() : new ObjectResult(Errors)
			{
				StatusCode = StatusCode
			};
			return result.ExecuteResultAsync(context);
		}

		public ICollection<ApiError> Errors { get; } = new List<ApiError>();
		protected virtual int StatusCode => !IsSuccessful ? (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.NoContent;

        public Enums.ApiErrorCode ApiErrorCode { get; }
    }

	public class GenericActionResponse<T> : ActionResponse
	{
		public T ResponseObject { get; set; }

		public GenericActionResponse(T value)
		{
			ResponseObject = value;
		}

		public GenericActionResponse(IEnumerable<ApiError> errors)
		{
			foreach (var item in errors)
			{
				Errors.Add(item);
			}
		}

		public GenericActionResponse()
		{
		}
	}
}