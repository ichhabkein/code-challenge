using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace search_api
{

    // lightweight UI for testing the API
    public class Diagnostics
    {

        public string arguments;

        private static string searchForm = "<form method='post' action=''>" +
                                "[Flickr Feed] Search For: " + 
                                "<input type='text' name='q' value = ''>" +
                                "<input type='submit' value='Go!'>" +
                                "</form><hr>";

        public Diagnostics (string arguments) {

            this.arguments = arguments;

        }

        public ContentResult GenerateDiagnosticPage()
        {

            FlickrIngest ingest = new FlickrIngest(false); // non-limited data

            // get data from flickr
            ingest.SearchFlickr(this.arguments);

            return new ContentResult {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = GenerateDiagnosticPageContent((FlickrFeed)ingest.GetFeed())
            };

        }

        public ContentResult GenerateEmptyDiagnosticPage()
        {

            string pageContent = "<html><head></head><body>" + searchForm;
            pageContent = pageContent + 
                    "</body></html>";

            return new ContentResult {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = pageContent
            };

        }

        // should be converted to a view if we had the time
        private string GenerateDiagnosticPageContent(FlickrFeed feed)
        {
            
            string searchForm = "<form method='get' action=''>" +
                                "Search For: " + 
                                "<input type='text' name='q' value = ''>" +
                                "<input type='submit' value='Go!'>" +
                                "</form><hr>";

            string pageContent = "<html><head></head><body>" + searchForm;

            if (feed != null) {
                foreach (FlickrFeedItem item in feed.items) {
                    pageContent = pageContent + 
                    item.date_taken + "<br>" +
                    "<a href = '" + item.link + "'>" +
                    "<img style='max-width:120px' src = '" + item.media.m + "'><br>" +
                    item.title +
                    "</a>" +"<hr>";
                }
            }

            pageContent = pageContent + 
                    "</body></html>";

            return pageContent;
        }
    }
}
