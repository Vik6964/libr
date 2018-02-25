using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraAddin
{
    public class Attachment
    {
        public string id { get; set; }
        public string self { get; set; }
        public string filename { get; set; }
        public string content { get; set; }
    }
    public class Comment
    {
        public string id { get; set; }
        public string body { get; set; }
    }
    public class CommentsContainer
    {
        public int startAt { get; set; }
        public int maxResults { get; set; }
        public int total { get; set; }
        public List<Comment> comments { get; set; }
    }
    public class IssueContainer
    {
        public string expand { get; set; }

        public int maxResults { get; set; }
        public int total { get; set; }
        public int startAt { get; set; }

        public List<Issue> issues { get; set; }
    }
    public class IssueFields
    {
        public IssueFields()
        {
            timetracking = new Timetracking();

            labels = new List<String>();
            comments = new List<Comment>();
            issuelinks = new List<IssueLink>();
            attachment = new List<Attachment>();
        }

        public String summary { get; set; }
        public String description { get; set; }
        public Timetracking timetracking { get; set; }

        public List<String> labels { get; set; }
        public List<Comment> comments { get; set; }
        public List<IssueLink> issuelinks { get; set; }
        public List<Attachment> attachment { get; set; }
    }
    public class IssueLink
    {
        public IssueLink()
        {
            type = new LinkType();
            inwardIssue = new IssueRef();
            outwardIssue = new IssueRef();
        }

        public string id { get; set; }

        public LinkType type { get; set; }
        public IssueRef outwardIssue { get; set; }
        public IssueRef inwardIssue { get; set; }
    }

    public class LinkType
    {
        public string name { get; set; }
    }
    public class IssueRef
    {
        public string id { get; set; }
        public string key { get; set; }
    }
}
