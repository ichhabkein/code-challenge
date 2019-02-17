using System.Runtime.Serialization;
using System.Collections.Generic;
using System;

public class Media
{
    public string m { get; set; }
}

// 3 required as part of code challenge
public class LimitedFlickrFeedItem
{
    public string title { get; set; }
    public string link { get; set; }
    public DateTime date_taken { get; set; }
}

// we add media so pretty pictures can be shown in the UI
public class FlickrFeedItem
{
    public string title { get; set; }
    public string link { get; set; }
    public Media media { get; set; }
    public DateTime date_taken { get; set; }

    // ignore, unused, listed here for reference

    // public string description { get; set; }
    // public DateTime published { get; set; }
    // public string author { get; set; }
    // public string author_id { get; set; }
    // public string tags { get; set; }
}

public class FlickrFeed
{

    // ignore, unused, listed here for reference

    // public string title { get; set; }
    // public string link { get; set; }
    // public string description { get; set; }
    // public DateTime modified { get; set; }
    // public string generator { get; set; }
    public List<FlickrFeedItem> items { get; set; }
}

public class LimitedFlickrFeed
{
    public List<LimitedFlickrFeedItem> items { get; set; }
}
