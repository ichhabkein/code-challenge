using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace search_api
{

    public class FlickrIngest
    {
        private static string flickrURL = "https://api.flickr.com/services/feeds/photos_public.gne?format=json&nojsoncallback=1&tags=";

        // allow storage of either the limited model without the media, or the "full" with
        private Object feed = null;

        private Boolean limited;

        public FlickrIngest(Boolean limited) {
            this.limited = limited;
        }

        private string FetchTestData()
        {
            string testJson = @"{
                'title': 'Recent Uploads tagged kitten',
                'link': 'https:\/\/www.flickr.com\/photos\/tags\/kitten\/',
                'description': '',
                'modified': '2019-02-16T16:36:55Z',
                'generator': 'https:\/\/www.flickr.com',
                'items': [
                    {
                        'title': 'under cover',
                        'link': 'https:\/\/www.flickr.com\/photos\/144948606@N04\/46390237154\/',
                        'media': {'m':'https:\/\/farm8.staticflickr.com\/7871\/46390237154_d48b60ed4f_m.jpg'},
                        'date_taken': '2019-02-16T18:36:55-08:00',
                        'description': ' <p><a href=\'https:\/\/www.flickr.com\/people\/144948606@N04\/\'>barmaleyy<\/a> posted a photo:<\/p> <p><a href=\'https:\/\/www.flickr.com\/photos\/144948606@N04\/46390237154\/\' title=\'under cover\'><img src=\'https:\/\/farm8.staticflickr.com\/7871\/46390237154_d48b60ed4f_m.jpg\' width=\'181\' height=\'240\' alt=\'under cover\' \/><\/a><\/p> ',
                        'published': '2019-02-16T16:36:55Z',
                        'author': 'nobody@flickr.com (\'barmaleyy\')',
                        'author_id': '144948606@N04',
                        'tags': 'cat pet kitten feline shelter undercover animals bnw blackandwhite mono monochrome monoart eyes samsung samsunggalaxy mobile mobilephotography'
                    }     
                ]
            }";

            return testJson;

        }

        private string FetchData(string arguments)
        // because we're lazy, arguments should be comma delimited, same as flickr is expecting with tags
        {
            HttpClient client = new HttpClient();
            var result = client.GetStringAsync(flickrURL + arguments);
            return result.Result;
        }

        public void SearchFlickr(string arguments) {
            
            string flickrJSON = this.FetchData(arguments);

            try {
                if(this.limited == false) {
                    feed = JsonConvert.DeserializeObject<FlickrFeed>(flickrJSON);
                } else {
                    feed = JsonConvert.DeserializeObject<LimitedFlickrFeed>(flickrJSON);
                }

            } catch (Exception e) {
                // muffle the exception and just log to console in case of error
                Console.WriteLine(e.Message);
            }
        }

        public Object GetFeed() {
            return feed;
        }

    }


}
