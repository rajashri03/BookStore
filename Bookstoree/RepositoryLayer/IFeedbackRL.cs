using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer
{
    public interface IFeedbackRL
    {
        public FeedbackModel AddFeedback(FeedbackModel addfeedback);
        public List<FeedbackModel> RetrieveOrderDetails(int bookId);
    }
}
