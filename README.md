# Smokeball_WebScraper
Smokeball take home coding exercise as part of application process. "The CEO wants to know where his company sits in google rankings, and is sick of doing this manually"

## What is it
* Simple WPF .Net 5 application that downloads from https://www.google.com.au/search?num=100&q=conveyancing+software and outputs where smokeball.com.au sits in the rankings
* Can run directly in Visual Studio 2019 provided you have appropriate WPF tools/extensions installed.
* Clone the repo and try it out!


## Unfinished/meh code
* Unit tests are sorely lacking due to time constraints
* Implementing a my own HtmlParser is tough work, it is making do with a less than ideal DivAndAnchorFlattenedHtmlParser that ignores everything in html exept anchors and divs. The code is written in a way where HtmlParser could be swapped out for something a bit more comprehensive/robust.
* HtmlElement parsing is primitive, all attributes are saved as a single combined string, and basic string.contains() to see if it has an attribute value you are looking for. This could easily run into trouble if the value you are looking for is on another attribute name!
* Need to add appsetting.json file to make it easier to configure different google search terms, what links to look for, and how many results to search through
* Web scrapers can be pretty meh in general. This identifies search results based on css class I saw by manually looking at google.com.au's html, this will certainly change with time and break this application!

### Tests I do have
* Some basic "integration" tests, testing how the GoogleRanker code works _with_ the html parser, given test html with known expected results.
* Some (but not comprehensive) unit tests for GoogleRanker.cs, didn't get round to writing any for any other classes.

## Where this could be taken in future
* User could input what search terms they want in google, what url they are looking for rankings for, and how far into google search results they want to go.
* A simple console app could be created that uses the WebScraper.Logic library. It could for example be a cron job/lambda function that runs once a day and persists the result to some data store. This could give a look at how well google rankings are doing over time, and be used to judge effective different SEO strategies have been...
* ...
