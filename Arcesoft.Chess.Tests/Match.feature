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
		| Source | Destination | Type           |
		| C2     | C4          | Move           |
		| E7     | E6          | Move           |
		| G1     | F3          | Move           |
		| D7     | D5          | Move           |
		| D2     | D4          | Move           |
		| G8     | F6          | Move           |
		| B1     | C3          | Move           |
		| F8     | E7          | Move           |
		| C1     | G5          | Move           |
		| E8     | G8          | CastleKingside |
		| E2     | E3          | Move           |
		| H7     | H6          | Move           |
		| G5     | H4          | Move           |
		| B7     | B6          | Move           |
		| C4     | D5          | CapturePawn    |
		| F6     | D5          | CapturePawn    |
		| H4     | E7          | CaptureBishop  |
		| D8     | E7          | CaptureBishop  |
		| C3     | D5          | CaptureKnight  |
		| E6     | D5          | CaptureKnight  |
		| A1     | C1          | Move           |
		| C8     | E6          | Move           |
		| D1     | A4          | Move           |
		| C7     | C5          | Move           |
		| A4     | A3          | Move           |
		| F8     | C8          | Move           |
		| F1     | B5          | Move           |
		| A7     | A6          | Move           |
		| D4     | C5          | CapturePawn    |
		| B6     | C5          | CapturePawn    |
		| E1     | G1          | CastleKingside |
		| A8     | A7          | Move           |
		| B5     | E2          | Move           |
		| B8     | D7          | Move           |
		| F3     | D4          | Move           |
		| E7     | F8          | Move           |
		| D4     | E6          | CaptureBishop  |
		| F7     | E6          | CaptureKnight  |
		| E3     | E4          | Move           |
		| D5     | D4          | Move           |
		| F2     | F4          | Move           |
		| F8     | E7          | Move           |
		| E4     | E5          | Move           |
		| C8     | B8          | Move           |
		| E2     | C4          | Move           |
		| G8     | H8          | Move           |
		| A3     | H3          | Move           |
		| D7     | F8          | Move           |
		| B2     | B3          | Move           |
		| A6     | A5          | Move           |
		| F4     | F5          | Move           |
		| E6     | F5          | CapturePawn    |
		| F1     | F5          | CapturePawn    |
		| F8     | H7          | Move           |
		| C1     | F1          | Move           |
		| E7     | D8          | Move           |
		| H3     | G3          | Move           |
		| A7     | E7          | Move           |
		| H2     | H4          | Move           |
		| B8     | B7          | Move           |
		| E5     | E6          | Move           |
		| B7     | C7          | Move           |
		| G3     | E5          | Move           |
		| D8     | E8          | Move           |
		| A2     | A4          | Move           |
		| E8     | D8          | Move           |
		| F1     | F2          | Move           |
		| D8     | E8          | Move           |
		| F2     | F3          | Move           |
		| E8     | D8          | Move           |
		| C4     | D3          | Move           |
		| D8     | E8          | Move           |
		| E5     | E4          | Move           |
		| H7     | F6          | Move           |
		| F5     | F6          | CaptureKnight  |
		| G7     | F6          | CaptureRook    |
		| F3     | F6          | CapturePawn    |
		| H8     | G8          | Move           |
		| D3     | C4          | Move           |
		| G8     | H8          | Move           |
		| E4     | F4          | Move           |
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
		| Source | Destination | Type            |
		| E2     | E4          | Move            |
		| D7     | D6          | Move            |
		| D2     | D4          | Move            |
		| G8     | F6          | Move            |
		| B1     | C3          | Move            |
		| G7     | G6          | Move            |
		| C1     | E3          | Move            |
		| F8     | G7          | Move            |
		| D1     | D2          | Move            |
		| C7     | C6          | Move            |
		| F2     | F3          | Move            |
		| B7     | B5          | Move            |
		| G1     | E2          | Move            |
		| B8     | D7          | Move            |
		| E3     | H6          | Move            |
		| G7     | H6          | CaptureBishop   |
		| D2     | H6          | CaptureBishop   |
		| C8     | B7          | Move            |
		| A2     | A3          | Move            |
		| E7     | E5          | Move            |
		| E1     | C1          | CastleQueenside |
		| D8     | E7          | Move            |
		| C1     | B1          | Move            |
		| A7     | A6          | Move            |
		| E2     | C1          | Move            |
		| E8     | C8          | CastleQueenside |
		| C1     | B3          | Move            |
		| E5     | D4          | CapturePawn     |
		| D1     | D4          | CapturePawn     |
		| C6     | C5          | Move            |
		| D4     | D1          | Move            |
		| D7     | B6          | Move            |
		| G2     | G3          | Move            |
		| C8     | B8          | Move            |
		| B3     | A5          | Move            |
		| B7     | A8          | Move            |
		| F1     | H3          | Move            |
		| D6     | D5          | Move            |
		| H6     | F4          | Move            |
		| B8     | A7          | Move            |
		| H1     | E1          | Move            |
		| D5     | D4          | Move            |
		| C3     | D5          | Move            |
		| B6     | D5          | CaptureKnight   |
		| E4     | D5          | CaptureKnight   |
		| E7     | D6          | Move            |
		| D1     | D4          | CapturePawn     |
		| C5     | D4          | CaptureRook     |
		| E1     | E7          | Move            |
		| A7     | B6          | Move            |
		| F4     | D4          | CapturePawn     |
		| B6     | A5          | CaptureKnight   |
		| B2     | B4          | Move            |
		| A5     | A4          | Move            |
		| D4     | C3          | Move            |
		| D6     | D5          | CapturePawn     |
		| E7     | A7          | Move            |
		| A8     | B7          | Move            |
		| A7     | B7          | CaptureBishop   |
		| D5     | C4          | Move            |
		| C3     | F6          | CaptureKnight   |
		| A4     | A3          | CapturePawn     |
		| F6     | A6          | CapturePawn     |
		| A3     | B4          | CapturePawn     |
		| C2     | C3          | Move            |
		| B4     | C3          | CapturePawn     |
		| A6     | A1          | Move            |
		| C3     | D2          | Move            |
		| A1     | B2          | Move            |
		| D2     | D1          | Move            |
		| H3     | F1          | Move            |
		| D8     | D2          | Move            |
		| B7     | D7          | Move            |
		| D2     | D7          | CaptureRook     |
		| F1     | C4          | CaptureQueen    |
		| B5     | C4          | CaptureBishop   |
		| B2     | H8          | CaptureRook     |
		| D7     | D3          | Move            |
		| H8     | A8          | Move            |
		| C4     | C3          | Move            |
		| A8     | A4          | Move            |
		| D1     | E1          | Move            |
		| F3     | F4          | Move            |
		| F7     | F5          | Move            |
		| B1     | C1          | Move            |
		| D3     | D2          | Move            |
		| A4     | A7          | Move            |
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
		| Source | Destination | Type           |
		| D2     | D4          | Move           |
		| G8     | F6          | Move           |
		| C2     | C4          | Move           |
		| G7     | G6          | Move           |
		| B1     | C3          | Move           |
		| F8     | G7          | Move           |
		| E2     | E4          | Move           |
		| D7     | D6          | Move           |
		| F2     | F3          | Move           |
		| E8     | G8          | CastleKingside |
		| C1     | E3          | Move           |
		| B8     | D7          | Move           |
		| D1     | D2          | Move           |
		| C7     | C5          | Move           |
		| D4     | D5          | Move           |
		| D7     | E5          | Move           |
		| H2     | H3          | Move           |
		| F6     | H5          | Move           |
		| E3     | F2          | Move           |
		| F7     | F5          | Move           |
		| E4     | F5          | CapturePawn    |
		| F8     | F5          | CapturePawn    |
		| G2     | G4          | Move           |
		| F5     | F3          | CapturePawn    |
		| G4     | H5          | CaptureKnight  |
		| D8     | F8          | Move           |
		| C3     | E4          | Move           |
		| G7     | H6          | Move           |
		| D2     | C2          | Move           |
		| F8     | F4          | Move           |
		| G1     | E2          | Move           |
		| F3     | F2          | CaptureBishop  |
		| E4     | F2          | CaptureRook    |
		| E5     | F3          | Move           |
		| E1     | D1          | Move           |
		| F4     | H4          | Move           |
		| F2     | D3          | Move           |
		| C8     | F5          | Move           |
		| E2     | C1          | Move           |
		| F3     | D2          | Move           |
		| H5     | G6          | CapturePawn    |
		| H7     | G6          | CapturePawn    |
		| F1     | G2          | Move           |
		| D2     | C4          | CapturePawn    |
		| C2     | F2          | Move           |
		| C4     | E3          | Move           |
		| D1     | E2          | Move           |
		| H4     | C4          | Move           |
		| G2     | F3          | Move           |
		| A8     | F8          | Move           |
		| H1     | G1          | Move           |
		| E3     | C2          | Move           |
		| E2     | D1          | Move           |
		| F5     | D3          | CaptureKnight  |
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
		| Source | Destination | Type           |
		| G1     | F3          | Move           |
		| G8     | F6          | Move           |
		| C2     | C4          | Move           |
		| G7     | G6          | Move           |
		| B1     | C3          | Move           |
		| F8     | G7          | Move           |
		| D2     | D4          | Move           |
		| E8     | G8          | CastleKingside |
		| C1     | F4          | Move           |
		| D7     | D5          | Move           |
		| D1     | B3          | Move           |
		| D5     | C4          | CapturePawn    |
		| B3     | C4          | CapturePawn    |
		| C7     | C6          | Move           |
		| E2     | E4          | Move           |
		| B8     | D7          | Move           |
		| A1     | D1          | Move           |
		| D7     | B6          | Move           |
		| C4     | C5          | Move           |
		| C8     | G4          | Move           |
		| F4     | G5          | Move           |
		| B6     | A4          | Move           |
		| C5     | A3          | Move           |
		| A4     | C3          | CaptureKnight  |
		| B2     | C3          | CaptureKnight  |
		| F6     | E4          | CapturePawn    |
		| G5     | E7          | CapturePawn    |
		| D8     | B6          | Move           |
		| F1     | C4          | Move           |
		| E4     | C3          | CapturePawn    |
		| E7     | C5          | Move           |
		| F8     | E8          | Move           |
		| E1     | F1          | Move           |
		| G4     | E6          | Move           |
		| C5     | B6          | CaptureQueen   |
		| E6     | C4          | CaptureBishop  |
		| F1     | G1          | Move           |
		| C3     | E2          | Move           |
		| G1     | F1          | Move           |
		| E2     | D4          | CapturePawn    |
		| F1     | G1          | Move           |
		| D4     | E2          | Move           |
		| G1     | F1          | Move           |
		| E2     | C3          | Move           |
		| F1     | G1          | Move           |
		| A7     | B6          | CaptureBishop  |
		| A3     | B4          | Move           |
		| A8     | A4          | Move           |
		| B4     | B6          | CapturePawn    |
		| C3     | D1          | CaptureRook    |
		| H2     | H3          | Move           |
		| A4     | A2          | CapturePawn    |
		| G1     | H2          | Move           |
		| D1     | F2          | CapturePawn    |
		| H1     | E1          | Move           |
		| E8     | E1          | CaptureRook    |
		| B6     | D8          | Move           |
		| G7     | F8          | Move           |
		| F3     | E1          | CaptureRook    |
		| C4     | D5          | Move           |
		| E1     | F3          | Move           |
		| F2     | E4          | Move           |
		| D8     | B8          | Move           |
		| B7     | B5          | Move           |
		| H3     | H4          | Move           |
		| H7     | H5          | Move           |
		| F3     | E5          | Move           |
		| G8     | G7          | Move           |
		| H2     | G1          | Move           |
		| F8     | C5          | Move           |
		| G1     | F1          | Move           |
		| E4     | G3          | Move           |
		| F1     | E1          | Move           |
		| C5     | B4          | Move           |
		| E1     | D1          | Move           |
		| D5     | B3          | Move           |
		| D1     | C1          | Move           |
		| G3     | E2          | Move           |
		| C1     | B1          | Move           |
		| E2     | C3          | Move           |
		| B1     | C1          | Move           |
		| A2     | C2          | Move           |
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
		| Source | Destination | Type            |
		| E2     | E4          | Move            |
		| E7     | E5          | Move            |
		| G1     | F3          | Move            |
		| D7     | D6          | Move            |
		| D2     | D4          | Move            |
		| C8     | G4          | Move            |
		| D4     | E5          | CapturePawn     |
		| G4     | F3          | CaptureKnight   |
		| D1     | F3          | CaptureBishop   |
		| D6     | E5          | CapturePawn     |
		| F1     | C4          | Move            |
		| G8     | F6          | Move            |
		| F3     | B3          | Move            |
		| D8     | E7          | Move            |
		| B1     | C3          | Move            |
		| C7     | C6          | Move            |
		| C1     | G5          | Move            |
		| B7     | B5          | Move            |
		| C3     | B5          | CapturePawn     |
		| C6     | B5          | CaptureKnight   |
		| C4     | B5          | CapturePawn     |
		| B8     | D7          | Move            |
		| E1     | C1          | CastleQueenside |
		| A8     | D8          | Move            |
		| D1     | D7          | CaptureKnight   |
		| D8     | D7          | CaptureRook     |
		| H1     | D1          | Move            |
		| E7     | E6          | Move            |
		| B5     | D7          | CaptureRook     |
		| F6     | D7          | CaptureBishop   |
		| B3     | B8          | Move            |
		| D7     | B8          | CaptureQueen    |
		| D1     | D8          | Move            |
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
		| Source | Destination | Type           |
		| E2     | E4          | Move           |
		| G8     | F6          | Move           |
		| E4     | E5          | Move           |
		| F6     | E4          | Move           |
		| D2     | D3          | Move           |
		| E4     | C5          | Move           |
		| D3     | D4          | Move           |
		| C5     | E4          | Move           |
		| D1     | D3          | Move           |
		| D7     | D5          | Move           |
		| E5     | D6          | AuPassant      |
		| E4     | D6          | CapturePawn    |
		| G1     | F3          | Move           |
		| B7     | B5          | Move           |
		| C1     | F4          | Move           |
		| E7     | E5          | Move           |
		| F4     | E5          | CapturePawn    |
		| C8     | F5          | Move           |
		| D3     | B3          | Move           |
		| B8     | C6          | Move           |
		| F1     | B5          | CapturePawn    |
		| D8     | D7          | Move           |
		| E1     | G1          | CastleKingside |
		| D6     | E4          | Move           |
		| B1     | C3          | Move           |
		| A7     | A6          | Move           |
		| B5     | A4          | Move           |
		| F5     | E6          | Move           |
		| D4     | D5          | Move           |
		| E6     | F5          | Move           |
		| A4     | C6          | CaptureKnight  |
		| D7     | C6          | CaptureBishop  |
		| D5     | C6          | CaptureQueen   |
		| F8     | C5          | Move           |
		| E5     | G7          | CapturePawn    |
		| H8     | G8          | Move           |
		| F3     | E5          | Move           |
		| G8     | G7          | CaptureBishop  |
		| C3     | E4          | CaptureKnight  |
		| F5     | E4          | CaptureKnight  |
		| G2     | G3          | Move           |
		| F7     | F5          | Move           |
		| A1     | D1          | Move           |
		| E4     | F3          | Move           |
		| D1     | D7          | Move           |
		| A8     | D8          | Move           |
		| D7     | G7          | CaptureRook    |
		| D8     | D4          | Move           |
		| B3     | F7          | Move           |
		| E8     | D8          | Move           |
		| F7     | G8          | Move           |
		| C5     | F8          | Move           |
		| G8     | F8          | CaptureBishop  |
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
