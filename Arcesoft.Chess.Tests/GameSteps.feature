@Unit
Feature: GameSteps
	Validate game behavior works as expected

#PGN format for the board
#    A  B  C  D  E  F  G  H
#8:| 7|15|23|31|39|47|55|63|
#7:| 6|14|22|30|38|46|54|62|
#6:| 5|13|21|29|37|45|53|61|
#5:| 4|12|20|28|36|44|52|60|
#4:| 3|11|19|27|35|43|51|59|
#3:| 2|10|18|26|34|42|50|58|
#2:| 1| 9|17|25|33|41|49|57|
#1:| 0| 8|16|24|32|40|48|56|

Background: 
	Given I have a container
	Given I have a game factory

Scenario: New game should start in correct state
	When I start a new game
	Then I expect the game to not be over
	Then I expect the gamestate to be 'InPlay'
	Then I expect no moves to have been made
	Then I expect the current player is 'White'
	Then I expect the current board to contain the following
		| A  | B  | C  | D  | E  | F  | G  | H  |
		| BR | BN | BB | BQ | BK | BB | BN | BR |
		| BP | BP | BP | BP | BP | BP | BP | BP |
		|    |    |    |    |    |    |    |    |
		|    |    |    |    |    |    |    |    |
		|    |    |    |    |    |    |    |    |
		|    |    |    |    |    |    |    |    |
		| WP | WP | WP | WP | WP | WP | WP | WP |
		| WR | WN | WB | WQ | WK | WB | WN | WR |

Scenario: New game should find moves for starting board
	Given I start a new game
	When I find moves for the current game
	Then I expect the the moves found to contain
		| Source | Destination |
		| A2     | A3          |
		| A2     | A4          |
		| B1     | A3          |
		| B1     | C3          |
		| B2     | B3          |
		| B2     | B4          |
		| C2     | C3          |
		| C2     | C4          |
		| D2     | D3          |
		| D2     | D4          |
		| E2     | E3          |
		| E2     | E4          |
		| F2     | F3          |
		| F2     | F4          |
		| G1     | F3          |
		| G1     | H3          |
		| G2     | G3          |
		| G2     | G4          |
		| H2     | H3          |
		| H2     | H4          |

#This includes pawn movement tests which cover all the following:
#moving up one space
#moving up two spaces
#diagonal captures
#au passant
#also negative tests like not being able to move into occupied spaces or capture friendly pieces
Scenario: Find moves should find all moves for white pawns
	Given I start a new game in the following state
		| A  | B  | C  | D  | E  | F  | G  | H  |
		| BK |    |    |    |    |    |    |    |
		|    |    |    |    |    |    |    |    |
		|    |    |    |    |    |    | BP |    |
		|    |    |    |    |    | BP | WP | BP |
		|    |    |    | BP |    |    |    |    |
		|    | WP | WP |    | BP |    |    | WK |
		| WP |    |    | WP | WP |    |    | WP |
		|    |    |    |    |    |    |    |    |
	#moves made must be even so its whites turn
	#last move is to test au passant
	Given I have the following move history
		| Source | Destination | Result |
		| A1     | A2          | None   |
		| F7     | F5          | None   |
	When I find moves for the current game
	Then I expect the the moves found to contain
		| Source | Destination |
		| A2     | A3          |
		| A2     | A4          |
		| B3     | B4          |
		| C3     | C4          |
		| C3     | D4          |
		| D2     | D3          |
		| D2     | E3          |
		| G5     | F6          |
		| H3     | H4          |
		| H3     | G3          |
		| H3     | G2          |

#This includes pawn movement tests which cover all the following:
#moving up one space
#moving up two spaces
#diagonal captures
#au passant
#also negative tests like not being able to move into occupied spaces or capture friendly pieces
Scenario: Find moves should find all moves for black pawns
	Given I start a new game in the following state
		| A  | B  | C  | D  | E | F | G  | H  |
		| BK |    |    |    |   |   |    |    |
		| BP | BP |    |    |   |   |    |    |
		|    | WP | BP | BP |   |   |    | BP |
		|    | WP |    | WP |   |   |    |    |
		|    |    |    |    |   |   | BP | WP |
		|    |    |    |    |   |   | WP |    |
		|    |    |    |    |   |   |    |    |
		|    |    |    |    |   |   |    | WK |
	#moves made must be odd so its blacks turn
	#last move is to test au passant
	Given I have the following move history
		| Source | Destination | Result |
		| H2     | H4          | None   |
	When I find moves for the current game
	Then I expect the the moves found to contain
		| Source | Destination |
		| A8     | B8          |
		| A7     | A6          |
		| A7     | A5          |
		| A7     | B6          |
		| C6     | B5          |
		| C6     | C5          |
		| C6     | D5          |
		| G4     | H3          |
		| H6     | H5          |