# battle-kill-9000
This repo is used for a [twitch stream](https://www.twitch.tv/grofit) I'm doing on game prototyping with view separation, its generally updated as the stream progresses showing new bits and will evolve as the stream continues.

If you wonder why its called `Battle Kill 9000` thats because I couldn't come up with a name for the project so chat named it for me :D

## What happens on the stream
While this is primarily me coding stuff there is often other friends doing code on voice too so we have some lols and may go off topic, but its hopefully a fun journey. I am more than happy to answer questions on stream about tech and related approaches, so feel free to drop in and ask about why something was/is done a certain way.

## What is the game?
The game started out as a simple idea of a sort of auto battler grid card thing, which I was inspired to create after seeing someone make something similar on reddit and didn't even know this was a genre, so now im just making a really rubbish one just to see how far I can get while keeping a focus on separation from the view.

The game referenced in the reddit article can be found [here](https://krons.itch.io/towercrawl-tactics) which is far more amazing than this one is :D

## What tech are you using?
It will evolve as we continue but the way I envision things evolving would be like:

1. Write framework/game level code in class libs (c#, rx, .net 5)
2. Iteratively keep writing rubbish but functional web bits to quickly show functionality and iterate (blazor, bulma)
3. Move to use OpenRpg to cope with the complexity being added for combat/units etc
4. Move to use EcsRx to cope with the complexity being added for isolated game systems
5. End up with rubbish but functional game via web tech
6. Move to MG/Unity and basically just change view level stuff and integrate game there
...

This may not work out how I envision but at a high level thats what I am aiming for, so its partly for me to just make a simple game and have fun doing it, and partly for anyone who wants to learn about view separation and see how it is done in a semi-realistic scenario.

## You mention a lot of things I dont understand on the stream
If you don't understand something mentioned, like a `Builder` pattern, `Reactive Extensions` or even `Dependency Injection` etc don't worry as that will be half the fun learning about those things in a live coded game session.

I have written an incomplete ebook on a lot of these topics which can be found [Here: Development For Winners](https://grofit.gitbooks.io/development-for-winners/content/)

That contains most of the topics we will be covering on the stream, but feel free to just ask me on the stream about anything you want to know more about etc.
