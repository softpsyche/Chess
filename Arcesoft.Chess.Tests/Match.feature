@Unit
Feature: Match
	Validate match behavior

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

#########################################################################
###		Match factory												  ###
#########################################################################
Scenario: Load match should load famous game Fischer V Spassky 1972
	Given I have a match factory
	When I load the match 'fischer_spassky_1972.pgn'
	Then I expect the gamestate to be 'InPlay'
	Then I expect the following move history
		| Source | Destination | CapturedPiece | SpecialMoveType |
		| C2     | C4          |               |                 |
		| E7     | E6          |               |                 |
		| G1     | F3          |               |                 |
		| D7     | D5          |               |                 |
		| D2     | D4          |               |                 |
		| G8     | F6          |               |                 |
		| B1     | C3          |               |                 |
		| F8     | E7          |               |                 |
		| C1     | G5          |               |                 |
		| E8     | G8          |               | CastleKingside  |
		| E2     | E3          |               |                 |
		| H7     | H6          |               |                 |
		| G5     | H4          |               |                 |
		| B7     | B6          |               |                 |
		| C4     | D5          | BlackPawn     |                 |
		| F6     | D5          | WhitePawn     |                 |
		| H4     | E7          | BlackBishop   |                 |
		| D8     | E7          | WhiteBishop   |                 |
		| C3     | D5          | BlackKnight   |                 |
		| E6     | D5          | WhiteKnight   |                 |
		| A1     | C1          |               |                 |
		| C8     | E6          |               |                 |
		| D1     | A4          |               |                 |
		| C7     | C5          |               |                 |
		| A4     | A3          |               |                 |
		| F8     | C8          |               |                 |
		| F1     | B5          |               |                 |
		| A7     | A6          |               |                 |
		| D4     | C5          | BlackPawn     |                 |
		| B6     | C5          | WhitePawn     |                 |
		| E1     | G1          |               | CastleKingside  |
		| A8     | A7          |               |                 |
		| B5     | E2          |               |                 |
		| B8     | D7          |               |                 |
		| F3     | D4          |               |                 |
		| E7     | F8          |               |                 |
		| D4     | E6          | BlackBishop   |                 |
		| F7     | E6          | WhiteKnight   |                 |
		| E3     | E4          |               |                 |
		| D5     | D4          |               |                 |
		| F2     | F4          |               |                 |
		| F8     | E7          |               |                 |
		| E4     | E5          |               |                 |
		| C8     | B8          |               |                 |
		| E2     | C4          |               |                 |
		| G8     | H8          |               |                 |
		| A3     | H3          |               |                 |
		| D7     | F8          |               |                 |
		| B2     | B3          |               |                 |
		| A6     | A5          |               |                 |
		| F4     | F5          |               |                 |
		| E6     | F5          | WhitePawn     |                 |
		| F1     | F5          | BlackPawn     |                 |
		| F8     | H7          |               |                 |
		| C1     | F1          |               |                 |
		| E7     | D8          |               |                 |
		| H3     | G3          |               |                 |
		| A7     | E7          |               |                 |
		| H2     | H4          |               |                 |
		| B8     | B7          |               |                 |
		| E5     | E6          |               |                 |
		| B7     | C7          |               |                 |
		| G3     | E5          |               |                 |
		| D8     | E8          |               |                 |
		| A2     | A4          |               |                 |
		| E8     | D8          |               |                 |
		| F1     | F2          |               |                 |
		| D8     | E8          |               |                 |
		| F2     | F3          |               |                 |
		| E8     | D8          |               |                 |
		| C4     | D3          |               |                 |
		| D8     | E8          |               |                 |
		| E5     | E4          |               |                 |
		| H7     | F6          |               |                 |
		| F5     | F6          | BlackKnight   |                 |
		| G7     | F6          | WhiteRook     |                 |
		| F3     | F6          | BlackPawn     |                 |
		| H8     | G8          |               |                 |
		| D3     | C4          |               |                 |
		| G8     | H8          |               |                 |
		| E4     | F4          |               |                 |
	Then I expect the current board to contain the following
		| A  | B  | C  | D  | E  | F  | G  | H  |
		|    |    |    |    | BQ |    |    | BK |
		|    |    | BR |    | BR |    |    |    |
		|    |    |    |    | WP | WR |    | BP |
		| BP |    | BP |    |    |    |    |    |
		| WP |    | WB | BP |    | WQ |    | WP |
		|    | WP |    |    |    |    |    |    |
		|    |    |    |    |    |    | WP |    |
		|    |    |    |    |    |    | WK |    |

Scenario: Load match should load famous game Kasparov V Topalov 1999
	Given I have a match factory
	When I load the match 'kasparov_topalov_1999.pgn'
	Then I expect the gamestate to be 'InPlay'
	Then I expect the following move history
		| Source | Destination | CapturedPiece | SpecialMoveType |
		| E2     | E4          |               |                 |
		| D7     | D6          |               |                 |
		| D2     | D4          |               |                 |
		| G8     | F6          |               |                 |
		| B1     | C3          |               |                 |
		| G7     | G6          |               |                 |
		| C1     | E3          |               |                 |
		| F8     | G7          |               |                 |
		| D1     | D2          |               |                 |
		| C7     | C6          |               |                 |
		| F2     | F3          |               |                 |
		| B7     | B5          |               |                 |
		| G1     | E2          |               |                 |
		| B8     | D7          |               |                 |
		| E3     | H6          |               |                 |
		| G7     | H6          | WhiteBishop   |                 |
		| D2     | H6          | BlackBishop   |                 |
		| C8     | B7          |               |                 |
		| A2     | A3          |               |                 |
		| E7     | E5          |               |                 |
		| E1     | C1          |               | CastleQueenside |
		| D8     | E7          |               |                 |
		| C1     | B1          |               |                 |
		| A7     | A6          |               |                 |
		| E2     | C1          |               |                 |
		| E8     | C8          |               | CastleQueenside |
		| C1     | B3          |               |                 |
		| E5     | D4          | WhitePawn     |                 |
		| D1     | D4          | BlackPawn     |                 |
		| C6     | C5          |               |                 |
		| D4     | D1          |               |                 |
		| D7     | B6          |               |                 |
		| G2     | G3          |               |                 |
		| C8     | B8          |               |                 |
		| B3     | A5          |               |                 |
		| B7     | A8          |               |                 |
		| F1     | H3          |               |                 |
		| D6     | D5          |               |                 |
		| H6     | F4          |               |                 |
		| B8     | A7          |               |                 |
		| H1     | E1          |               |                 |
		| D5     | D4          |               |                 |
		| C3     | D5          |               |                 |
		| B6     | D5          | WhiteKnight   |                 |
		| E4     | D5          | BlackKnight   |                 |
		| E7     | D6          |               |                 |
		| D1     | D4          | BlackPawn     |                 |
		| C5     | D4          | WhiteRook     |                 |
		| E1     | E7          |               |                 |
		| A7     | B6          |               |                 |
		| F4     | D4          | BlackPawn     |                 |
		| B6     | A5          | WhiteKnight   |                 |
		| B2     | B4          |               |                 |
		| A5     | A4          |               |                 |
		| D4     | C3          |               |                 |
		| D6     | D5          | WhitePawn     |                 |
		| E7     | A7          |               |                 |
		| A8     | B7          |               |                 |
		| A7     | B7          | BlackBishop   |                 |
		| D5     | C4          |               |                 |
		| C3     | F6          | BlackKnight   |                 |
		| A4     | A3          | WhitePawn     |                 |
		| F6     | A6          | BlackPawn     |                 |
		| A3     | B4          | WhitePawn     |                 |
		| C2     | C3          |               |                 |
		| B4     | C3          | WhitePawn     |                 |
		| A6     | A1          |               |                 |
		| C3     | D2          |               |                 |
		| A1     | B2          |               |                 |
		| D2     | D1          |               |                 |
		| H3     | F1          |               |                 |
		| D8     | D2          |               |                 |
		| B7     | D7          |               |                 |
		| D2     | D7          | WhiteRook     |                 |
		| F1     | C4          | BlackQueen    |                 |
		| B5     | C4          | WhiteBishop   |                 |
		| B2     | H8          | BlackRook     |                 |
		| D7     | D3          |               |                 |
		| H8     | A8          |               |                 |
		| C4     | C3          |               |                 |
		| A8     | A4          |               |                 |
		| D1     | E1          |               |                 |
		| F3     | F4          |               |                 |
		| F7     | F5          |               |                 |
		| B1     | C1          |               |                 |
		| D3     | D2          |               |                 |
		| A4     | A7          |               |                 |
	Then I expect the current board to contain the following
		| A  | B | C  | D  | E  | F  | G  | H  |
		|    |   |    |    |    |    |    |    |
		| WQ |   |    |    |    |    |    | BP |
		|    |   |    |    |    |    | BP |    |
		|    |   |    |    |    | BP |    |    |
		|    |   |    |    |    | WP |    |    |
		|    |   | BP |    |    |    | WP |    |
		|    |   |    | BR |    |    |    | WP |
		|    |   | WK |    | BK |    |    |    |

Scenario: Load match should load famous game Beliavsky V Nunn 1985
	Given I have a match factory
	When I load the match 'beliavsky_nunn_1985.pgn'
	Then I expect the gamestate to be 'InPlay'
	Then I expect the following move history
		| Source | Destination | CapturedPiece | SpecialMoveType |
		| D2     | D4          |               |                 |
		| G8     | F6          |               |                 |
		| C2     | C4          |               |                 |
		| G7     | G6          |               |                 |
		| B1     | C3          |               |                 |
		| F8     | G7          |               |                 |
		| E2     | E4          |               |                 |
		| D7     | D6          |               |                 |
		| F2     | F3          |               |                 |
		| E8     | G8          |               | CastleKingside  |
		| C1     | E3          |               |                 |
		| B8     | D7          |               |                 |
		| D1     | D2          |               |                 |
		| C7     | C5          |               |                 |
		| D4     | D5          |               |                 |
		| D7     | E5          |               |                 |
		| H2     | H3          |               |                 |
		| F6     | H5          |               |                 |
		| E3     | F2          |               |                 |
		| F7     | F5          |               |                 |
		| E4     | F5          | BlackPawn     |                 |
		| F8     | F5          | WhitePawn     |                 |
		| G2     | G4          |               |                 |
		| F5     | F3          | WhitePawn     |                 |
		| G4     | H5          | BlackKnight   |                 |
		| D8     | F8          |               |                 |
		| C3     | E4          |               |                 |
		| G7     | H6          |               |                 |
		| D2     | C2          |               |                 |
		| F8     | F4          |               |                 |
		| G1     | E2          |               |                 |
		| F3     | F2          | WhiteBishop   |                 |
		| E4     | F2          | BlackRook     |                 |
		| E5     | F3          |               |                 |
		| E1     | D1          |               |                 |
		| F4     | H4          |               |                 |
		| F2     | D3          |               |                 |
		| C8     | F5          |               |                 |
		| E2     | C1          |               |                 |
		| F3     | D2          |               |                 |
		| H5     | G6          | BlackPawn     |                 |
		| H7     | G6          | WhitePawn     |                 |
		| F1     | G2          |               |                 |
		| D2     | C4          | WhitePawn     |                 |
		| C2     | F2          |               |                 |
		| C4     | E3          |               |                 |
		| D1     | E2          |               |                 |
		| H4     | C4          |               |                 |
		| G2     | F3          |               |                 |
		| A8     | F8          |               |                 |
		| H1     | G1          |               |                 |
		| E3     | C2          |               |                 |
		| E2     | D1          |               |                 |
		| F5     | D3          | WhiteKnight   |                 |
	Then I expect the current board to contain the following
		| A  | B  | C  | D  | E  | F  | G  | H  |
		|    |    |    |    |    | BR | BK |    |
		| BP | BP |    |    | BP |    |    |    |
		|    |    |    | BP |    |    | BP | BB |
		|    |    | BP | WP |    |    |    |    |
		|    |    | BQ |    |    |    |    |    |
		|    |    |    | BB |    | WB |    | WP |
		| WP | WP | BN |    |    | WQ |    |    |
		| WR |    | WN | WK |    |    | WR |    |

Scenario: Load match should load famous game Byrne V Fischer 1956
	Given I have a match factory
	When I load the match 'byrne_fischer_1956.pgn'
	Then I expect the gamestate to be 'BlackWin'
	Then I expect the following move history
		| Source | Destination | CapturedPiece | SpecialMoveType |
		| G1     | F3          |               |                 |
		| G8     | F6          |               |                 |
		| C2     | C4          |               |                 |
		| G7     | G6          |               |                 |
		| B1     | C3          |               |                 |
		| F8     | G7          |               |                 |
		| D2     | D4          |               |                 |
		| E8     | G8          |               | CastleKingside  |
		| C1     | F4          |               |                 |
		| D7     | D5          |               |                 |
		| D1     | B3          |               |                 |
		| D5     | C4          | WhitePawn     |                 |
		| B3     | C4          | BlackPawn     |                 |
		| C7     | C6          |               |                 |
		| E2     | E4          |               |                 |
		| B8     | D7          |               |                 |
		| A1     | D1          |               |                 |
		| D7     | B6          |               |                 |
		| C4     | C5          |               |                 |
		| C8     | G4          |               |                 |
		| F4     | G5          |               |                 |
		| B6     | A4          |               |                 |
		| C5     | A3          |               |                 |
		| A4     | C3          | WhiteKnight   |                 |
		| B2     | C3          | BlackKnight   |                 |
		| F6     | E4          | WhitePawn     |                 |
		| G5     | E7          | BlackPawn     |                 |
		| D8     | B6          |               |                 |
		| F1     | C4          |               |                 |
		| E4     | C3          | WhitePawn     |                 |
		| E7     | C5          |               |                 |
		| F8     | E8          |               |                 |
		| E1     | F1          |               |                 |
		| G4     | E6          |               |                 |
		| C5     | B6          | BlackQueen    |                 |
		| E6     | C4          | WhiteBishop   |                 |
		| F1     | G1          |               |                 |
		| C3     | E2          |               |                 |
		| G1     | F1          |               |                 |
		| E2     | D4          | WhitePawn     |                 |
		| F1     | G1          |               |                 |
		| D4     | E2          |               |                 |
		| G1     | F1          |               |                 |
		| E2     | C3          |               |                 |
		| F1     | G1          |               |                 |
		| A7     | B6          | WhiteBishop   |                 |
		| A3     | B4          |               |                 |
		| A8     | A4          |               |                 |
		| B4     | B6          | BlackPawn     |                 |
		| C3     | D1          | WhiteRook     |                 |
		| H2     | H3          |               |                 |
		| A4     | A2          | WhitePawn     |                 |
		| G1     | H2          |               |                 |
		| D1     | F2          | WhitePawn     |                 |
		| H1     | E1          |               |                 |
		| E8     | E1          | WhiteRook     |                 |
		| B6     | D8          |               |                 |
		| G7     | F8          |               |                 |
		| F3     | E1          | BlackRook     |                 |
		| C4     | D5          |               |                 |
		| E1     | F3          |               |                 |
		| F2     | E4          |               |                 |
		| D8     | B8          |               |                 |
		| B7     | B5          |               |                 |
		| H3     | H4          |               |                 |
		| H7     | H5          |               |                 |
		| F3     | E5          |               |                 |
		| G8     | G7          |               |                 |
		| H2     | G1          |               |                 |
		| F8     | C5          |               |                 |
		| G1     | F1          |               |                 |
		| E4     | G3          |               |                 |
		| F1     | E1          |               |                 |
		| C5     | B4          |               |                 |
		| E1     | D1          |               |                 |
		| D5     | B3          |               |                 |
		| D1     | C1          |               |                 |
		| G3     | E2          |               |                 |
		| C1     | B1          |               |                 |
		| E2     | C3          |               |                 |
		| B1     | C1          |               |                 |
		| A2     | C2          |               |                 |
	Then I expect the current board to contain the following
		| A | B  | C  | D | E  | F  | G  | H  |
		|   | WQ |    |   |    |    |    |    |
		|   |    |    |   |    | BP | BK |    |
		|   |    | BP |   |    |    | BP |    |
		|   | BP |    |   | WN |    |    | BP |
		|   | BB |    |   |    |    |    | WP |
		|   | BB | BN |   |    |    |    |    |
		|   |    | BR |   |    |    | WP |    |
		|   |    | WK |   |    |    |    |    |

Scenario: Load match should load famous game Morphy Duke Karl Count Isouard 1858
	Given I have a match factory
	When I load the match 'morphy_duke_karl_count_isouard_1858.pgn'
	Then I expect the gamestate to be 'WhiteWin'
	Then I expect the following move history
		| Source | Destination | CapturedPiece | SpecialMoveType |
		| E2     | E4          |               |                 |
		| E7     | E5          |               |                 |
		| G1     | F3          |               |                 |
		| D7     | D6          |               |                 |
		| D2     | D4          |               |                 |
		| C8     | G4          |               |                 |
		| D4     | E5          | BlackPawn     |                 |
		| G4     | F3          | WhiteKnight   |                 |
		| D1     | F3          | BlackBishop   |                 |
		| D6     | E5          | WhitePawn     |                 |
		| F1     | C4          |               |                 |
		| G8     | F6          |               |                 |
		| F3     | B3          |               |                 |
		| D8     | E7          |               |                 |
		| B1     | C3          |               |                 |
		| C7     | C6          |               |                 |
		| C1     | G5          |               |                 |
		| B7     | B5          |               |                 |
		| C3     | B5          | BlackPawn     |                 |
		| C6     | B5          | WhiteKnight   |                 |
		| C4     | B5          | BlackPawn     |                 |
		| B8     | D7          |               |                 |
		| E1     | C1          |               | CastleQueenside |
		| A8     | D8          |               |                 |
		| D1     | D7          | BlackKnight   |                 |
		| D8     | D7          | WhiteRook     |                 |
		| H1     | D1          |               |                 |
		| E7     | E6          |               |                 |
		| B5     | D7          | BlackRook     |                 |
		| F6     | D7          | WhiteBishop   |                 |
		| B3     | B8          |               |                 |
		| D7     | B8          | WhiteQueen    |                 |
		| D1     | D8          |               |                 |
	Then I expect the current board to contain the following
		| A  | B  | C  | D  | E  | F  | G  | H  |
		|    | BN |    | WR | BK | BB |    | BR |
		| BP |    |    |    |    | BP | BP | BP |
		|    |    |    |    | BQ |    |    |    |
		|    |    |    |    | BP |    | WB |    |
		|    |    |    |    | WP |    |    |    |
		|    |    |    |    |    |    |    |    |
		| WP | WP | WP |    |    | WP | WP | WP |
		|    |    | WK |    |    |    |    |    |

Scenario: Load match should load game with au passant
	Given I have a match factory
	When I load the match 'nobody_has_aupassant.pgn'
	Then I expect the gamestate to be 'WhiteWin'
	Then I expect the following move history
		| Source | Destination | CapturedPiece | SpecialMoveType |
		| E2     | E4          |               |                 |
		| G8     | F6          |               |                 |
		| E4     | E5          |               |                 |
		| F6     | E4          |               |                 |
		| D2     | D3          |               |                 |
		| E4     | C5          |               |                 |
		| D3     | D4          |               |                 |
		| C5     | E4          |               |                 |
		| D1     | D3          |               |                 |
		| D7     | D5          |               |                 |
		| E5     | D6          | BlackPawn     | AuPassant       |
		| E4     | D6          | WhitePawn     |                 |
		| G1     | F3          |               |                 |
		| B7     | B5          |               |                 |
		| C1     | F4          |               |                 |
		| E7     | E5          |               |                 |
		| F4     | E5          | BlackPawn     |                 |
		| C8     | F5          |               |                 |
		| D3     | B3          |               |                 |
		| B8     | C6          |               |                 |
		| F1     | B5          | BlackPawn     |                 |
		| D8     | D7          |               |                 |
		| E1     | G1          |               | CastleKingside  |
		| D6     | E4          |               |                 |
		| B1     | C3          |               |                 |
		| A7     | A6          |               |                 |
		| B5     | A4          |               |                 |
		| F5     | E6          |               |                 |
		| D4     | D5          |               |                 |
		| E6     | F5          |               |                 |
		| A4     | C6          | BlackKnight   |                 |
		| D7     | C6          | WhiteBishop   |                 |
		| D5     | C6          | BlackQueen    |                 |
		| F8     | C5          |               |                 |
		| E5     | G7          | BlackPawn     |                 |
		| H8     | G8          |               |                 |
		| F3     | E5          |               |                 |
		| G8     | G7          | WhiteBishop   |                 |
		| C3     | E4          | BlackKnight   |                 |
		| F5     | E4          | WhiteKnight   |                 |
		| G2     | G3          |               |                 |
		| F7     | F5          |               |                 |
		| A1     | D1          |               |                 |
		| E4     | F3          |               |                 |
		| D1     | D7          |               |                 |
		| A8     | D8          |               |                 |
		| D7     | G7          | BlackRook     |                 |
		| D8     | D4          |               |                 |
		| B3     | F7          |               |                 |
		| E8     | D8          |               |                 |
		| F7     | G8          |               |                 |
		| C5     | F8          |               |                 |
		| G8     | F8          | BlackBishop   |                 |
	Then I expect the current board to contain the following
		| A  | B  | C  | D  | E  | F  | G  | H  |
		|    |    |    | BK |    | WQ |    |    |
		|    |    | BP |    |    |    | WR | BP |
		| BP |    | WP |    |    |    |    |    |
		|    |    |    |    | WN | BP |    |    |
		|    |    |    | BR |    |    |    |    |
		|    |    |    |    |    | BB | WP |    |
		| WP | WP | WP |    |    | WP |    | WP |
		|    |    |    |    |    | WR | WK |    |
