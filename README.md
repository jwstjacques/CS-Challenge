# README

An app that gets random Chuck Norris jokes and can substitute other peoples names into the joke instead of Chuck Norris'.

### Changes from initial release

1. All calls to external APIs are async/await calls, bubbling up each calling function to also be async/await.
2. Removed the collections API call, the "how many jokes do you want" action as they are not part of the spec, and are scope creep. Re-implementation is trivial but best to stick to the spec.
3. A much saner and better user interface.
4. The ability to exit.
5. A very conservative rate limiter on hitting the APIs, to not get my IP locked out.
6. Name replace on Chuck Norris, when the "fake name" option is chosen
7. Replacement of male gender pronouns with female pronouns, when a female "fake name" is generated.
8. Clear screen when iterating to new screen or joke.

### Not implemented, but should/could be

1. A threaded feedback loop while the jokes are loading. Thread.Abort does not work on Mac, so I gave up.
2. Unit tests on API calls
3. Ability to add a custom name (trivial, but wasn't clear in spec)

### Built on

2015 Macbook Pro
Running OSX Mojave 10.14.6
In VS Studio Code
Run using dotnet run from the mac terminal, after installing dotnet on os

### Build status

[![Build status](https://ci.appveyor.com/api/projects/status/lsvryi8ea0n6b4xo?svg=true)](https://ci.appveyor.com/project/fleetcarma/cs-challenge)
