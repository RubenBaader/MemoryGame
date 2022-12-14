# Implementing highscore functionality
## Core concept
* The game records your name and \# of moves to a JSON file.
* When displaying highscores, the names and moves are sorted in ascending move \# order.

## Limitations and control
* Since the player chooses the number of cards to play with, the highscores should be for the chosen number of cards - i.e. games with 12 cards will be a different category to games with 36 cards.

## Database Structure
* The DB should load highscore data from the above mentioned JSON file, combine that data with the current game's data, sort and return up to 10 of the highest scores for given amount of cards.
* 