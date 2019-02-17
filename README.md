Flickr Search

[redacted]+ichhabkein@gmail.com

LuckyDay coding challenge - wraps searchable Flickr feed from:
https://api.flickr.com/services/feeds/photos_public.gne
and returns a modified version of their json data served directly from their API

Takes post and get requests with parameter 'q', passes the parameter directly to the above flickr api in order to search tags with format=json and nojsoncallback=1, takes the feed result and strips the relevant data (title, image, published date) before returning.

There is a (not fancy) human-readable diagnostics client page that allows a user to submit queries and then takes the json passed from the api and presents the results in human-readable format.  For fun, we throw up the media image as part of the data.

A better test client would sample directly from the modified JSON to be delivered through our API, but we needed to cheat a little to show a thumbnail sized image.

Built on top of the vanilla API .NET core project.

Assumptions:

1.  No caching.  Caching would be left up to the client if necessary, we assume that liveness of the data takes priority over caching the data, and if caching is necessary, flickr will handle that.  We are just a wrapper layer - if flickr is down, we're down, caching would just muddy the waters here.

The only reason to revisit the caching is if *we* were bandwidth/session constrained but had plenty of local storage, and we didn't care if we served stale data... or if we wanted to dynamically build an index of what was in Flickr based on organic search requests.  Out of scope for this, but an interesting thing to look at if needed.

How to use:

https://127.0.0.1:5001/?q=ponies

Gives you ponies

https://127.0.0.1:5001/api/?q=ponies

Gives you ponies as json

https://127.0.0.1:5001/

Just gives you a blank page.  Try searching for kittens.


Design:

We cheat and serialize the media paramter when using the ui to view the data in addition to the title, link, and date.  When you use the API, we restrict to just the 3 listed required values (title, link, date).  No limiting was implemented because Flickr seems to be doing that already, and no pagination was implemented because there aren't enough items returned to be worth the ffort.


Tests:

None at the moment.


Running:

In the root of the project directory, "dotnet run".  Then go to the URLs as above for ponies and kittens.

