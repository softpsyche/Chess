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
	Then I expect the moves found should contain
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
	Then I expect the moves found should contain
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
	Then I expect the moves found should contain
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
	Then I expect the moves found should contain
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
	Then I expect the moves found should contain
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
	Then I expect the moves found should contain
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
	Then I expect the moves found should contain
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
	Then I expect the moves found should contain
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
	Then I expect the moves found should contain
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
	Then I expect the moves found should contain
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
	Then I expect the moves found should contain
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
	Then I expect the moves found should contain
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
		| A | B | C  | D  | E | F | G | H  |
		|   |   |    |    |   |   |   |    |
		|   |   |    |    |   |   |   |    |
		|   |   | BB |    |   |   |   |    |
		|   |   |    | WK |   |   |   |    |
		|   |   | BQ |    |   |   |   |    |
		|   |   |    |    |   |   |   |    |
		|   |   |    |    |   |   |   |    |
		|   |   |    |    |   |   |   | BK |
	When I find moves for the current game
	Then I expect the moves found should contain
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
	Then I expect the moves found should contain
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
	Then I expect the moves found should contain
		| Source | Destination |
		| D5     | D6          |
		| D5     | E5          |
		| D5     | C4          |

Scenario: Find moves should find castle moves for white king
	Given I start a new game in the following state
		| A  | B | C | D | E  | F | G | H  |
		|    |   |   |   | BK |   |   |    |
		|    |   |   |   |    |   |   |    |
		|    |   |   |   |    |   |   |    |
		|    |   |   |   |    |   |   |    |
		|    |   |   |   |    |   |   |    |
		|    |   |   |   |    |   |   |    |
		| WP |   |   |   |    |   |   | WP |
		| WR |   |   |   | WK |   |   | WR |
	When I find moves for the current game
	Then I expect the moves found should contain
		| Source | Destination |
		#king
		| E1     | F1          |
		| E1     | F2          |
		| E1     | E2          |
		| E1     | D2          |
		| E1     | D1          |
		#king castle
		| E1     | G1          |
		| E1     | C1          |
		#pawn and rook moves
		| A1     | B1          |
		| A1     | C1          |
		| A1     | D1          |
		| A2     | A3          |
		| A2     | A4          |
		| H1     | G1          |
		| H1     | F1          |
		| H2     | H3          |
		| H2     | H4          |

Scenario: Find moves should find castle moves for black king
	Given I start a new game in the following state
		| A  | B | C | D | E  | F | G | H  |
		| BR |   |   |   | BK |   |   | BR |
		| BP |   |   |   |    |   |   | BP |
		|    |   |   |   |    |   |   |    |
		|    |   |   |   |    |   |   |    |
		|    |   |   |   |    |   |   |    |
		|    |   |   |   |    |   |   |    |
		|    |   |   |   |    |   |   |    |
		|    |   |   |   | WK |   |   |    |
	Given I have the following move history
		| Source | Destination | Result |
		| A1     | A1          | None   |
	When I find moves for the current game
	Then I expect the moves found should contain
		| Source | Destination |
		#king
		| E8     | F8          |
		| E8     | F7          |
		| E8     | E7          |
		| E8     | D7          |
		| E8     | D8          |
		#king castle
		| E8     | G8          |
		| E8     | C8          |
		#pawn and rook moves
		| A7     | A6          |
		| A7     | A5          |
		| A8     | B8          |
		| A8     | C8          |
		| A8     | D8          |
		| H7     | H6          |
		| H7     | H5          |
		| H8     | G8          |
		| H8     | F8          |

#When are you not allowed to castle?
#There are a number of cases when castling is not permitted.
#-Your king has been moved earlier in the game.
#-The rook that castles has been moved earlier in the game.
#-There are pieces standing between your king and rook.
#-The king is in check.
#-The king moves through a square that is attacked by a piece of the opponent.
#-The king would be in check after castling.
Scenario Outline: Find moves should NOT find castle moves for white king when
	Given I start a new game in the following state
		| A  | B    | C    | D    | E  | F    | G    | H  |
		|    |      |      |      | BK | <F8> |      |    |
		|    |      |      |      |    |      |      |    |
		|    |      |      |      |    |      |      |    |
		|    |      |      |      |    |      |      |    |
		|    |      |      |      |    |      |      |    |
		|    | <B3> | <C3> |      |    |      |      |    |
		| WP |      |      |      |    |      |      | WP |
		| WR |      |      | <D1> | WK |      | <G1> | WR |
	Given I have the following move history
		| Source              | Destination              | Result |
		| D1                  | D1                       | None   |
		| <MoveHistorySource> | <MoveHistoryDestination> | None   |
	When I find moves for the current game
	Then I expect the moves found should NOT contain '<NonExpectedMoves>'
	Then I expect the moves found should contain '<ExpectedMoves>'
	Examples: 
		| TestName                   | MoveHistorySource | MoveHistoryDestination | B3 | C3 | D1 | F8 | G1 | ExpectedMoves | NonExpectedMoves |
		| king has moved             | E1                | F1                     |    |    |    |    |    |               | E1-G1,E1-C1      |
		| king is in check           | D4                | D4                     |    | BB |    |    |    |               | E1-G1,E1-C1      |
		| eastern rook has moved     | H1                | H3                     |    |    |    |    |    | E1-C1         | E1-G1            |
		| eastern path blocked       | D4                | D4                     |    |    |    |    | WN | E1-C1         | E1-G1            |
		| eastern path is threatened | D4                | D4                     |    |    |    | BR |    | E1-C1         | E1-G1            |
		| western rook has moved     | A1                | D1                     |    |    |    |    |    | E1-G1         | E1-C1            |
		| western path blocked       | D4                | D4                     |    |    | WQ |    |    | E1-G1         | E1-C1            |
		| western path is threatened | D4                | D4                     | BB |    |    |    |    | E1-G1         | E1-C1            |

Scenario Outline: Find moves should NOT find castle moves for black king when
	Given I start a new game in the following state
		| A  | B    | C    | D    | E  | F    | G    | H  |
		| BR |      |      | <D8> | BK |      | <G8> | BR |
		| BP |      |      |      |    |      |      | BP |
		|    | <B6> | <C6> |      |    |      |      |    |
		|    |      |      |      |    |      |      |    |
		|    |      |      |      |    |      |      |    |
		|    |      |      |      |    |      |      |    |
		|    |      |      |      |    |      |      |    |
		|    |      |      |      | WK | <F1> |      |    |
	Given I have the following move history
		| Source              | Destination              | Result |
		| <MoveHistorySource> | <MoveHistoryDestination> | None   |
	When I find moves for the current game
	Then I expect the moves found should NOT contain '<NonExpectedMoves>'
	Then I expect the moves found should contain '<ExpectedMoves>'
	Examples: 
		| TestName                   | MoveHistorySource | MoveHistoryDestination | B6 | C6 | D8 | F1 | G8 | ExpectedMoves | NonExpectedMoves |
		| king has moved             | E8                | F8                     |    |    |    |    |    |               | E8-G8,E8-C8      |
		| king is in check           | D4                | D4                     |    | WB |    |    |    |               | E8-G8,E8-C8      |
		| eastern rook has moved     | H8                | H3                     |    |    |    |    |    | E8-C8         | E8-G8            |
		| eastern path blocked       | D4                | D4                     |    |    |    |    | BN | E8-C8         | E8-G8            |
		| eastern path is threatened | D4                | D4                     |    |    |    | WR |    | E8-C8         | E8-G8            |
		| western rook has moved     | A8                | D8                     |    |    |    |    |    | E8-G8         | E8-C8            |
		| western path blocked       | D4                | D4                     |    |    | BQ |    |    | E8-G8         | E8-C8            |
		| western path is threatened | D4                | D4                     | WB |    |    |    |    | E8-G8         | E8-C8            |


#########################################################################
###		Pinned Moves												  ###
#########################################################################
#NOTE: we never have to worry about au-passant
Scenario: Find moves should find all moves for pinned white pawns
	Given I start a new game in the following state
		| A  | B  | C  | D  | E  | F  | G  | H  |
		| BB |    |    | BR |    |    | BB |    |
		|    | WP |    |    |    |    |    |    |
		|    |    |    | WP | WP |    |    |    |
		| BN | BR | WP | WK |    | WP |    | BR |
		|    |    |    |    |    |    |    |    |
		|    | WP | BR |    |    | WP |    |    |
		| BB |    |    | WP |    |    | BB |    |
		|    |    |    | BR |    |    |    | BK |
	When I find moves for the current game
	Then I expect the moves found should contain
		| Source | Destination |
		#kings moves
		| D5     | E5          |
		| D5     | E4          |
		| D5     | D4          |
		#pawn moves
		| B7     | A8          |
		| D6     | D7          |
		| D2     | D3          |
		| D2     | D4          |
#NOTE: we never have to worry about au-passant
Scenario: Find moves should find all moves for pinned black pawns
	Given I start a new game in the following state
		| A  | B  | C  | D  | E  | F  | G  | H  |
		| WB |    |    | WR |    |    | WB |    |
		|    | BP |    | BP |    |    |    |    |
		|    |    |    |    | BP |    |    |    |
		| WN | WR | BP | BK |    | BP |    | WR |
		|    |    |    |    | BP |    |    |    |
		|    | BP |    | BP |    |    |    |    |
		| WB |    |    |    | WP |    | WB |    |
		|    |    |    | WR |    |    |    | WK |
	Given I have the following move history
		| Source | Destination | Result |
		| A1     | A1          | None   |
	When I find moves for the current game
	Then I expect the moves found should contain
		| Source | Destination |
		#kings moves
		| D5     | D6          |
		| D5     | E5          |
		| D5     | D4          |
		#pawn moves
		| D7     | D6          |
		| D3     | D2          |
		| B3     | A2          |

Scenario: Find moves should find all moves for pinned white knights
	Given I start a new game in the following state
		| A  | B | C  | D  | E  | F | G  | H  |
		| BB |   |    | BR |    |   | BB |    |
		|    |   |    |    |    |   |    |    |
		|    |   | WN | WN | WN |   |    |    |
		| BR |   | WN | WK | WN |   | BR |    |
		|    |   | WN | WN | WN |   |    |    |
		|    |   |    |    |    |   |    |    |
		| BB |   |    | BR |    |   | BB |    |
		|    |   |    |    |    |   |    | BK |
	When I find moves for the current game
	Then I expect no moves were found

Scenario: Find moves should find all moves for pinned black knights
	Given I start a new game in the following state
		| A  | B | C  | D  | E  | F | G  | H  |
		| WB |   |    | WR |    |   | WB |    |
		|    |   |    |    |    |   |    |    |
		|    |   | BN | BN | BN |   |    |    |
		| WR |   | BN | BK | BN |   | WR |    |
		|    |   | BN | BN | BN |   |    |    |
		|    |   |    |    |    |   |    |    |
		| WB |   |    | WR |    |   | WB |    |
		|    |   |    |    |    |   |    | WK |
	Given I have the following move history
		| Source | Destination | Result |
		| A1     | A1          | None   |
	When I find moves for the current game
	Then I expect no moves were found

Scenario: Find moves should find all moves for pinned white bishops
	Given I start a new game in the following state
		| A  | B  | C  | D  | E  | F | G  | H  |
		| BB |    |    | BR |    |   | BB |    |
		|    | WB |    |    |    |   |    |    |
		|    |    |    | WB | WB |   |    |    |
		| BR |    | WB | WK | WB |   | BR |    |
		|    |    | WB | WB | WB |   |    |    |
		|    |    |    |    |    |   |    |    |
		| BB |    |    | BR |    |   | BB |    |
		|    |    |    |    |    |   |    | BK |
	When I find moves for the current game
	Then I expect the moves found should contain
		| Source | Destination |
		| B7     | A8          |
		| B7     | C6          |
		| C4     | B3          |
		| C4     | A2          |
		| D5     | C6          |
		| E4     | F3          |
		| E4     | G2          |
		| E6     | F7          |
		| E6     | G8          |
		
Scenario: Find moves should find all moves for pinned black bishops
	Given I start a new game in the following state
		| A  | B  | C  | D  | E  | F | G  | H  |
		| WB |    |    | WR |    |   | WB |    |
		|    | BB |    |    |    |   |    |    |
		|    |    |    | BB | BB |   |    |    |
		| WR |    | BB | BK | BB |   | WR |    |
		|    |    | BB | BB | BB |   |    |    |
		|    |    |    |    |    |   |    |    |
		| WB |    |    | WR |    |   | WB |    |
		|    |    |    |    |    |   |    | WK |
	Given I have the following move history
		| Source | Destination | Result |
		| A1     | A1          | None   |
	When I find moves for the current game
	Then I expect the moves found should contain
		| Source | Destination |
		| B7     | A8          |
		| B7     | C6          |
		| C4     | B3          |
		| C4     | A2          |
		| D5     | C6          |
		| E4     | F3          |
		| E4     | G2          |
		| E6     | F7          |
		| E6     | G8          |

Scenario: Find moves should find all moves for pinned white rooks
	Given I start a new game in the following state
		| A  | B | C  | D  | E  | F | G  | H  |
		| BB |   |    | BR |    |   | BB |    |
		|    |   |    |    |    |   |    |    |
		|    |   | WR | WR | WR |   |    |    |
		| BR |   | WR | WK | WR |   | BR |    |
		|    |   | WR |    | WR |   |    |    |
		|    |   |    | WR |    |   |    |    |
		| BB |   |    | BR |    |   | BB |    |
		|    |   |    |    |    |   |    | BK |
	When I find moves for the current game
	Then I expect the moves found should contain
		| Source | Destination |
		| D5     | D4          |
		| D6     | D7          |
		| D6     | D8          |
		| E5     | F5          |
		| E5     | G5          |
		| D3     | D4          |
		| D3     | D2          |
		| C5     | B5          |
		| C5     | A5          |
		
Scenario: Find moves should find all moves for pinned black rooks
	Given I start a new game in the following state
		| A  | B | C  | D  | E  | F | G  | H  |
		| WB |   |    | WR |    |   | WB |    |
		|    |   |    |    |    |   |    |    |
		|    |   | BR | BR | BR |   |    |    |
		| WR |   | BR | BK | BR |   | WR |    |
		|    |   | BR |    | BR |   |    |    |
		|    |   |    | BR |    |   |    |    |
		| WB |   |    | WR |    |   | WB |    |
		|    |   |    |    |    |   |    | WK |
	Given I have the following move history
		| Source | Destination | Result |
		| A1     | A1          | None   |
	When I find moves for the current game
	Then I expect the moves found should contain
		| Source | Destination |
		| D5     | D4          |
		| D6     | D7          |
		| D6     | D8          |
		| E5     | F5          |
		| E5     | G5          |
		| D3     | D4          |
		| D3     | D2          |
		| C5     | B5          |
		| C5     | A5          |

Scenario: Find moves should find all moves for pinned white queens
	Given I start a new game in the following state
		| A  | B  | C  | D  | E  | F | G  | H  |
		| BB |    |    | BR |    |   | BB |    |
		|    | WQ |    |    |    |   |    |    |
		|    |    |    | WQ | WQ |   |    |    |
		| BR |    | WQ | WK | WQ |   | BR |    |
		|    |    | WQ |    | WQ |   |    |    |
		|    |    |    | WQ |    |   |    |    |
		| BB |    |    | BR |    |   | BB |    |
		|    |    |    |    |    |   |    | BK |
	When I find moves for the current game
	Then I expect the moves found should contain
		| Source | Destination |
		| D5     | D4          |
		| D5     | C6          |
		| D6     | D7          |
		| D6     | D8          |
		| E5     | F5          |
		| E5     | G5          |
		| D3     | D4          |
		| D3     | D2          |
		| C5     | B5          |
		| C5     | A5          |
		| B7     | A8          |
		| B7     | C6          |
		| C4     | B3          |
		| C4     | A2          |
		| E4     | F3          |
		| E4     | G2          |
		| E6     | F7          |
		| E6     | G8          |
		
Scenario: Find moves should find all moves for pinned black queens
	Given I start a new game in the following state
		| A  | B  | C  | D  | E  | F | G  | H  |
		| WB |    |    | WR |    |   | WB |    |
		|    | BQ |    |    |    |   |    |    |
		|    |    |    | BQ | BQ |   |    |    |
		| WR |    | BQ | BK | BQ |   | WR |    |
		|    |    | BQ |    | BQ |   |    |    |
		|    |    |    | BQ |    |   |    |    |
		| WB |    |    | WR |    |   | WB |    |
		|    |    |    |    |    |   |    | WK |
	Given I have the following move history
		| Source | Destination | Result |
		| A1     | A1          | None   |
	When I find moves for the current game
	Then I expect the moves found should contain
		| Source | Destination |
		| D5     | D4          |
		| D5     | C6          |
		| D6     | D7          |
		| D6     | D8          |
		| E5     | F5          |
		| E5     | G5          |
		| D3     | D4          |
		| D3     | D2          |
		| C5     | B5          |
		| C5     | A5          |
		| B7     | A8          |
		| B7     | C6          |
		| C4     | B3          |
		| C4     | A2          |
		| E4     | F3          |
		| E4     | G2          |
		| E6     | F7          |
		| E6     | G8          |