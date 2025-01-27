using System.Collections.ObjectModel;
using static SharedModels.Enums.Enums;

namespace Internal.Models.Common
{
    public partial class ActionResponse
    {
        public bool IsSuccessful
        {
            get
            {
                return Errors.Count == 0;
            }
        }

        public ICollection<ApiError> Errors { get; set; } = new Collection<ApiError>();

        public ActionResponse()
        {
        }

        public ActionResponse(ApiErrorCode code, string msg)
        {
            Errors.Add(new ApiError(code, msg));
        }

        public ActionResponse(IEnumerable<ApiError> errors)
        {
            foreach (var item in errors)
            {
                Errors.Add(item);
            }
        }
    }
}
