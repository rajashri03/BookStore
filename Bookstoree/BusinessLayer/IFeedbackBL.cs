using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface IFeedbackBL
    {
        public FeedbackModel AddFeedback(FeedbackModel addfeedback);
        public List<FeedbackModel> RetrieveOrderDetails(int bookId);
    }
}
