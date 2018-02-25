using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JiraAddin
{
    public interface IJiraClient
    {
        /// <summary>
        /// Return single issue for the given project and issue key
        /// </summary>
        IEnumerable<Issue> GetIssue(String projectKey, String issueKey);
        /// <summary>Returns all issues for the given project</summary>
        IEnumerable<Issue> GetIssues(String projectKey);
        /// <summary>Returns all issues of the specified type for the given project</summary>
        IEnumerable<Issue> GetIssues(String projectKey, String issueType);
        /// <summary>
        /// Return all subtasks of the issue
        /// </summary>
        IEnumerable<Issue> GetSubTasksOfIssue(String projectKey, String issueKey, String issueType);
        /// <summary>Returns the issue identified by the given ref</summary>
        Issue LoadIssue(String issueRef);
        /// <summary>Returns the issue identified by the given ref</summary>
        Issue LoadIssue(IssueRef issueRef);
        /// <summary>
        /// Creates Subtasks against issue
        /// </summary>
        Issue CreateSubTask(String projectKey, String parentKey, String issueType, String summary, String description);
        /// <summary>Creates an issue of the specified type for the given project</summary>
        Issue CreateIssue(String projectKey, String issueType, String summary);
        /// <summary>Creates an issue of the specified type for the given project</summary>
        Issue CreateIssue(String projectKey, String issueType, String summary, String description);
        /// <summary>Updates the given issue on the remote system</summary>
        Issue UpdateIssue(Issue issue);
        /// <summary>Deletes the given issue from the remote system</summary>
        void DeleteIssue(IssueRef issue);

        /// <summary>Returns all comments for the given issue</summary>
        IEnumerable<Comment> GetComments(IssueRef issue);
        /// <summary>Adds a comment to the given issue</summary>
        Comment CreateComment(IssueRef issue, String comment);
        /// <summary>Deletes the given comment</summary>
        void DeleteComment(IssueRef issue, Comment comment);

        /// <summary>Return all attachments for the given issue</summary>
        IEnumerable<Attachment> GetAttachments(IssueRef issue);
        /// <summary>Creates an attachment to the given issue</summary>
        Attachment CreateAttachment(IssueRef issue, Stream stream, String fileName);
        /// <summary>Deletes the given attachment</summary>
        void DeleteAttachment(Attachment attachment);

        /// <summary>Returns all links for the given issue</summary>
        IEnumerable<IssueLink> GetIssueLinks(IssueRef issue);
        /// <summary>Returns the link between two issues of the given relation</summary>
        IssueLink LoadIssueLink(IssueRef parent, IssueRef child, String relationship);
        /// <summary>Creates a link between two issues with the given relation</summary>
        IssueLink CreateIssueLink(IssueRef parent, IssueRef child, String relationship);
        /// <summary>Removes the given link of two issues</summary>
        void DeleteIssueLink(IssueLink link);
    }
}
