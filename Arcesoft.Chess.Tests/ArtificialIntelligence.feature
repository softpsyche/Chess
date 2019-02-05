@Unit
Feature: ArtificialIntelligence
	Verify Artificial intelligence 

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
	Given I have an artificial intelligence


Scenario: Artificial intelligence should find mate in one for white
	Given I start a new game in the following state
		| A  | B | C | D | E | F | G | H  |
		| BK |   |   |   |   |   |   |    |
		| BP |   |   |   |   |   |   |    |
		| WP |   |   |   |   |   |   |    |
		|    |   |   |   |   |   |   |    |
		|    |   |   |   |   |   |   |    |
		|    |   |   |   |   |   |   |    |
		|    |   |   |   |   |   |   |    |
		| WK |   |   |   |   |   |   | WR |
	When I have the AI calculate the best move to a depth of '1'
	Then I expect the best move found to be
         | Source | Destination | 
         | H1     | H8          |  

Scenario: Artificial intelligence should find mate in three for white (rook mate)
	Given I start a new game in the following state
		| A  | B  | C  | D  | E  | F  | G  | H |
		| BK |    |    |    |    |    |    |   |
		| BP |    | BP |    | BP |    | BP |   |
		| WP |    | WP |    | WP |    | WP |   |
		|    |    |    |    |    |    |    |   |
		|    |    |    |    |    |    |    |   |
		|    |    |    |    |    |    |    |   |
		|    | WP |    | WP |    | WP |    |   |
		| WK | WR |    |    |    |    |    |   |
	When I have the AI calculate the best move to a depth of '3'
	Then I expect the best move found to be
         | Source | Destination | 
         | B1     | H1          | 

Scenario: Artificial intelligence should find mate in three for white (knight mate)
	Given I start a new game in the following state
		| A | B | C | D | E  | F | G  | H  |
		|   |   |   |   | WK |   |    | BK |
		|   |   |   |   |    |   |    | BP |
		|   |   |   |   |    |   |    | WP |
		|   |   |   |   | WN |   |    |    |
		|   |   |   |   |    |   | WN |    |
		|   |   |   |   |    |   |    |    |
		|   |   |   |   |    |   |    |    |
		|   |   |   |   |    |   |    |    |
	Given I have the following move history
         | Source | Destination |
		 | E1     | D1          |
		 | E8     | D8          |
	When I have the AI calculate the best move to a depth of '3'
	Then I expect the best move found to be
         | Source | Destination |
         | E5     | F7          |


#	Winning moves:
#	C6-D4
#	E2-D1
#	G4-E3
#	D1-C1
#	D4-E2 Mate!
@ignore
Scenario: Artificial intelligence should find mate in Five moves for black
	Given I start a new game in the following state
		| A  | B  | C  | D  | E  | F  | G  | H |
		| BR |    |    | WB | BK |    |    |   |
		| BP | BP | BP |    |    | BP | BP |   |
		|    |    | BN | BP |    |    |    |   |
		|    |    |    |    | BP |    |    |   |
		|    |    | WB |    | WP |    | BN |   |
		|    |    |    | WP |    |    |    |   |
		| WP | WP | WP | WN | WK | BP | WP |   |
		| WR | WN |    | BR |    |    |    |   |
	Given Its blacks turn
	When I have the AI calculate the best move to a depth of '4'
	Then I expect the best move found to be
         | Source | Destination |
         |        |             |


