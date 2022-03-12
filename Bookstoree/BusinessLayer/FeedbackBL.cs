using CommonLayer;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class FeedbackBL:IFeedbackBL
    {
        IFeedbackRL feedbackRl;
        public FeedbackBL(IFeedbackRL feedbackRl)
        {
            this.feedbackRl = feedbackRl;
        }
        public FeedbackModel AddFeedback(FeedbackModel addfeedback)
        {
            try
            {
                return this.feedbackRl.AddFeedback(addfeedback);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<FeedbackModel> RetrieveOrderDetails(int bookId)
        {
            try
            {
                return this.feedbackRl.RetrieveOrderDetails(bookId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
