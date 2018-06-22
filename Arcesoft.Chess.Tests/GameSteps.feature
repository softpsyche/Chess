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

Scenario: Find moves should find all moves for white knights
	Given I start a new game in the following state
		| A  | B  | C  | D  | E | F | G  | H  |
		| BK |    |    |    |   |   |    | WN |
		| BP |    |    |    |   |   | BP |    |
		|    |    | WN |    |   |   | WP |    |
		|    |    |    |    |   |   |    |    |
		|    |    |    | BP |   |   |    |    |
		|    | WN |    |    |   |   |    |    |
		|    |    |    |    |   |   | WN |    |
		|    |    |    |    |   |   |    | WK |
	When I find moves for the current game
	Then I expect the the moves found to contain
		| Source | Destination |
		| B3     | A5          |
		| B3     | C5          |
		| B3     | C1          |
		| B3     | D4          |
		| B3     | D2          |
		| C6     | A5          |
		| C6     | A7          |
		| C6     | B8          |
		| C6     | B4          |
		| C6     | D8          |
		| C6     | D4          |
		| C6     | E7          |
		| C6     | E5          |
		| G2     | E1          |
		| G2     | E3          |
		| G2     | F4          |
		| G2     | H4          |
		| H1     | H2          |
		| H1     | G1          |
		| H8     | F7          |

Scenario: Find moves should find all moves for black knights
	Given I start a new game in the following state
		| A  | B  | C  | D  | E | F | G  | H  |
		| WK |    |    |    |   |   |    | BN |
		| WP |    |    |    |   |   |    |    |
		|    |    | BN |    |   |   | BP |    |
		|    |    |    |    |   |   | WP |    |
		|    |    |    | WP |   |   |    |    |
		|    | BN |    |    |   |   |    |    |
		|    |    |    |    |   |   | BN |    |
		|    |    |    |    |   |   |    | BK |
	Given I have the following move history
		| Source | Destination | Result |
		| A1     | A1          | None   |
	When I find moves for the current game
	Then I expect the the moves found to contain
		| Source | Destination |
		| B3     | A5          |
		| B3     | C5          |
		| B3     | C1          |
		| B3     | D4          |
		| B3     | D2          |
		| C6     | A5          |
		| C6     | A7          |
		| C6     | B8          |
		| C6     | B4          |
		| C6     | D8          |
		| C6     | D4          |
		| C6     | E7          |
		| C6     | E5          |
		| G2     | E1          |
		| G2     | E3          |
		| G2     | F4          |
		| G2     | H4          |
		| H1     | H2          |
		| H1     | G1          |
		| H8     | F7          |

Scenario: Find moves should find all moves for white bishops
	Given I start a new game in the following state
		| A  | B  | C  | D | E  | F | G | H  |
		| WB | BK |    |   |    |   |   |    |
		|    |    |    |   |    |   |   |    |
		|    |    |    |   |    |   |   |    |
		|    | BP |    |   |    |   |   |    |
		|    |    | WB |   |    |   |   |    |
		|    |    |    |   | BP |   |   | BP |
		|    |    |    |   | WP |   |   | WP |
		|    |    |    |   |    |   |   | WK |
	When I find moves for the current game
	Then I expect the the moves found to contain
		| Source | Destination |
		| A8     | B7          |
		| A8     | C6          |
		| A8     | D5          |
		| A8     | E4          |
		| A8     | F3          |
		| A8     | G2          |
		| C4     | B5          |
		| C4     | B3          |
		| C4     | A2          |
		| C4     | D3          |
		| C4     | D5          |
		| C4     | E6          |
		| C4     | F7          |
		| C4     | G8          |
		| H1     | G1          |

Scenario: Find moves should find all moves for black bishops
	Given I start a new game in the following state
		| A  | B | C | D  | E | F  | G  | H |
		| BK |   |   |    |   |    |    |   |
		| BP |   |   |    |   |    |    |   |
		| WP |   |   |    |   |    |    |   |
		|    |   |   |    |   |    |    |   |
		|    |   |   | BB |   |    |    |   |
		|    |   |   |    |   |    |    |   |
		|    |   |   |    |   | WP | WK |   |
		|    |   |   |    |   |    |    |   |
	Given I have the following move history
		| Source | Destination | Result |
		| A1     | A1          | None   |
	When I find moves for the current game
	Then I expect the the moves found to contain
		| Source | Destination |
		| A8     | B8          |
		| D4     | C5          |
		| D4     | B6          |
		| D4     | E3          |
		| D4     | F2          |
		| D4     | C3          |
		| D4     | B2          |
		| D4     | A1          |
		| D4     | E5          |
		| D4     | F6          |
		| D4     | G7          |
		| D4     | H8          |

Scenario: Find moves should find all moves for white rooks
	Given I start a new game in the following state
		| A  | B | C | D  | E | F | G  | H  |
		| BK |   |   |    |   |   |    |    |
		|    |   |   | BP |   |   |    |    |
		|    |   |   |    |   |   |    |    |
		|    |   |   | WR |   |   |    |    |
		|    |   |   |    |   |   |    |    |
		|    |   |   |    |   |   | BP | BP |
		|    |   |   | WP |   |   |    | BP |
		|    |   |   |    |   |   |    | WK |
	When I find moves for the current game
	Then I expect the the moves found to contain
		| Source | Destination |
		| D2     | D3          |
		| D2     | D4          |
		| D5     | E5          |
		| D5     | F5          |
		| D5     | G5          |
		| D5     | H5          |
		| D5     | C5          |
		| D5     | B5          |
		| D5     | A5          |
		| D5     | D6          |
		| D5     | D7          |
		| D5     | D4          |
		| D5     | D3          |

Scenario: Find moves should find all moves for black rooks
	Given I start a new game in the following state
		| A  | B  | C | D | E  | F | G | H  |
		| BK |    |   |   |    |   |   |    |
		| WP |    |   |   | BP |   |   |    |
		| WP | WP |   |   |    |   |   |    |
		|    |    |   |   |    |   |   |    |
		|    |    |   |   | BR |   |   |    |
		|    |    |   |   |    |   |   |    |
		|    |    |   |   |    |   |   |    |
		|    |    |   |   | WR |   |   | WK |
	Given I have the following move history
		| Source | Destination | Result |
		| A1     | A1          | None   |
	When I find moves for the current game
	Then I expect the the moves found to contain
		| Source | Destination |
		| E4     | F4          |
		| E4     | G4          |
		| E4     | H4          |
		| E4     | D4          |
		| E4     | C4          |
		| E4     | B4          |
		| E4     | A4          |
		| E4     | E5          |
		| E4     | E6          |
		| E4     | E3          |
		| E4     | E2          |
		| E4     | E1          |
		| E7     | E6          |
		| E7     | E5          |

Scenario: Find moves should find all moves for white queens
	Given I start a new game in the following state
		| A | B | C | D  | E | F | G  | H  |
		|   |   |   |    |   |   |    | BK |
		|   |   |   | BP |   |   |    |    |
		|   |   |   |    |   |   |    |    |
		|   |   |   | WQ |   |   |    |    |
		|   |   |   |    |   |   |    |    |
		|   |   |   |    |   |   | BP | BP |
		|   |   |   | WP |   |   |    | BP |
		|   |   |   |    |   |   |    | WK |
	When I find moves for the current game
	Then I expect the the moves found to contain
		| Source | Destination |
		| D2     | D3          |
		| D2     | D4          |
		| D5     | E5          |
		| D5     | F5          |
		| D5     | G5          |
		| D5     | H5          |
		| D5     | C5          |
		| D5     | B5          |
		| D5     | A5          |
		| D5     | D6          |
		| D5     | D7          |
		| D5     | D4          |
		| D5     | D3          |
		| D5     | C6          |
		| D5     | B7          |
		| D5     | A8          |
		| D5     | E4          |
		| D5     | F3          |
		| D5     | G2          |
		| D5     | C4          |
		| D5     | B3          |
		| D5     | A2          |
		| D5     | E6          |
		| D5     | F7          |
		| D5     | G8          |

Scenario: Find moves should find all moves for black queens
	Given I start a new game in the following state
		| A | B | C | D  | E | F | G  | H  |
		|   |   |   |    |   |   |    | BK |
		|   |   |   | BP |   |   |    | WP |
		|   |   |   |    |   |   | WP | WP |
		|   |   |   |    |   |   |    |    |
		|   |   |   | BQ |   |   |    |    |
		|   |   |   |    |   |   |    |    |
		|   |   |   | WP |   |   |    |    |
		|   |   |   |    |   |   |    | WK |
	Given I have the following move history
		| Source | Destination | Result |
		| A1     | A1          | None   |
	When I find moves for the current game
	Then I expect the the moves found to contain
		| Source | Destination |
		| D4     | E4          |
		| D4     | F4          |
		| D4     | G4          |
		| D4     | H4          |
		| D4     | C4          |
		| D4     | B4          |
		| D4     | A4          |
		| D4     | D5          |
		| D4     | D6          |
		| D4     | D3          |
		| D4     | D2          |
		| D4     | C5          |
		| D4     | B6          |
		| D4     | A7          |
		| D4     | E3          |
		| D4     | F2          |
		| D4     | G1          |
		| D4     | C3          |
		| D4     | B2          |
		| D4     | A1          |
		| D4     | E5          |
		| D4     | F6          |
		| D4     | G7          |
		| D7     | D6          |
		| D7     | D5          |

Scenario: Find moves should find all moves for white King (no threats)
	Given I start a new game in the following state
		| A | B | C | D  | E | F | G | H  |
		|   |   |   |    |   |   |   |    |
		|   |   |   |    |   |   |   |    |
		|   |   |   |    |   |   |   |    |
		|   |   |   | WK |   |   |   |    |
		|   |   |   |    |   |   |   |    |
		|   |   |   |    |   |   |   |    |
		|   |   |   |    |   |   |   |    |
		|   |   |   |    |   |   |   | BK |
	When I find moves for the current game
	Then I expect the the moves found to contain
		| Source | Destination |
		| D5     | D6          |
		| D5     | E5          |
		| D5     | C4          |
		| D5     | D4          |
		| D5     | C5          |
		| D5     | E6          |
		| D5     | C6          |
		| D5     | E4          |

Scenario: Find moves should find all moves for white King 
	Given I start a new game in the following state
		| A | B | C  | D  | E | F  | G | H  |
		|   |   |    |    |   |    |   |    |
		|   |   |    |    |   |    |   |    |
		|   |   |    |    |   |    |   |    |
		|   |   | BR | WK |   | WP |   |    |
		|   |   |    |    |   |    |   |    |
		|   |   |    |    |   |    |   |    |
		|   |   |    |    |   |    |   |    |
		|   |   |    |    |   |    |   | BK |
	When I find moves for the current game
	Then I expect the the moves found to contain
		| Source | Destination |
		| D5     | D6          |
		| D5     | E5          |
		| D5     | C4          |

Scenario: Find moves should find all moves for black King (no threats)
	Given I start a new game in the following state
		| A | B | C | D  | E | F | G | H  |
		|   |   |   |    |   |   |   |    |
		|   |   |   |    |   |   |   |    |
		|   |   |   |    |   |   |   |    |
		|   |   |   | BK |   |   |   |    |
		|   |   |   |    |   |   |   |    |
		|   |   |   |    |   |   |   |    |
		|   |   |   |    |   |   |   |    |
		|   |   |   |    |   |   |   | WK |
	Given I have the following move history
		| Source | Destination | Result |
		| A1     | A1          | None   |
	When I find moves for the current game
	Then I expect the the moves found to contain
		| Source | Destination |
		| D5     | D6          |
		| D5     | E5          |
		| D5     | C4          |
		| D5     | D4          |
		| D5     | C5          |
		| D5     | E6          |
		| D5     | C6          |
		| D5     | E4          |

Scenario: Find moves should find all moves for black King 
	Given I start a new game in the following state
		| A | B | C  | D  | E | F | G | H  |
		|   |   |    |    |   |   |   |    |
		|   |   |    |    |   |   |   |    |
		|   |   | WB |    |   |   |   |    |
		|   |   |    | BK |   |   |   |    |
		|   |   | WQ |    |   |   |   |    |
		|   |   |    |    |   |   |   |    |
		|   |   |    |    |   |   |   |    |
		|   |   |    |    |   |   |   | WK |
	Given I have the following move history
		| Source | Destination | Result |
		| A1     | A1          | None   |
	When I find moves for the current game
	Then I expect the the moves found to contain
		| Source | Destination |
		| D5     | D6          |
		| D5     | E5          |
		| D5     | C4          |