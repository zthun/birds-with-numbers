# BirdsWithNumbers

This is an application that reads the Twitter stream api and reports the top 10 hashtags.

## How to Run

The following script has the commands to checkout, build and run the application.

```sh
git clone https://github.com/zthun/birds-with-numbers.git
cd birds-with-numbers
export TWITTER_BEARER_TOKEN=<Your twitter bearer token>
dotnet build
dotnet run --project BirdsWithNumbers.CLI
```

## Note on the bearer token

The bearer token is the twitter OAuth 2.0 access token. It is not the api key alone. You should have received your api key, api secret, and an accompying bearer token when you sign up for twitter's development platform.  