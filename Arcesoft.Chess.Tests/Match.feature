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
		| Source | Destination | Result  |
		| C2     | C4          | None    |
		| E7     | E6          | None    |
		| G1     | F3          | None    |
		| D7     | D5          | None    |
		| D2     | D4          | None    |
		| G8     | F6          | None    |
		| B1     | C3          | None    |
		| F8     | E7          | None    |
		| C1     | G5          | None    |
		| E8     | G8          | Castle  |
		| E2     | E3          | None    |
		| H7     | H6          | None    |
		| G5     | H4          | None    |
		| B7     | B6          | None    |
		| C4     | D5          | Capture |
		| F6     | D5          | Capture |
		| H4     | E7          | Capture |
		| D8     | E7          | Capture |
		| C3     | D5          | Capture |
		| E6     | D5          | Capture |
		| A1     | C1          | None    |
		| C8     | E6          | None    |
		| D1     | A4          | None    |
		| C7     | C5          | None    |
		| A4     | A3          | None    |
		| F8     | C8          | None    |
		| F1     | B5          | None    |
		| A7     | A6          | None    |
		| D4     | C5          | Capture |
		| B6     | C5          | Capture |
		| E1     | G1          | Castle  |
		| A8     | A7          | None    |
		| B5     | E2          | None    |
		| B8     | D7          | None    |
		| F3     | D4          | None    |
		| E7     | F8          | None    |
		| D4     | E6          | Capture |
		| F7     | E6          | Capture |
		| E3     | E4          | None    |
		| D5     | D4          | None    |
		| F2     | F4          | None    |
		| F8     | E7          | None    |
		| E4     | E5          | None    |
		| C8     | B8          | None    |
		| E2     | C4          | None    |
		| G8     | H8          | None    |
		| A3     | H3          | None    |
		| D7     | F8          | None    |
		| B2     | B3          | None    |
		| A6     | A5          | None    |
		| F4     | F5          | None    |
		| E6     | F5          | Capture |
		| F1     | F5          | Capture |
		| F8     | H7          | None    |
		| C1     | F1          | None    |
		| E7     | D8          | None    |
		| H3     | G3          | None    |
		| A7     | E7          | None    |
		| H2     | H4          | None    |
		| B8     | B7          | None    |
		| E5     | E6          | None    |
		| B7     | C7          | None    |
		| G3     | E5          | None    |
		| D8     | E8          | None    |
		| A2     | A4          | None    |
		| E8     | D8          | None    |
		| F1     | F2          | None    |
		| D8     | E8          | None    |
		| F2     | F3          | None    |
		| E8     | D8          | None    |
		| C4     | D3          | None    |
		| D8     | E8          | None    |
		| E5     | E4          | None    |
		| H7     | F6          | None    |
		| F5     | F6          | Capture |
		| G7     | F6          | Capture |
		| F3     | F6          | Capture |
		| H8     | G8          | None    |
		| D3     | C4          | None    |
		| G8     | H8          | None    |
		| E4     | F4          | None    |
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
		| Source | Destination | Result  |
		| E2     | E4          | None    |
		| D7     | D6          | None    |
		| D2     | D4          | None    |
		| G8     | F6          | None    |
		| B1     | C3          | None    |
		| G7     | G6          | None    |
		| C1     | E3          | None    |
		| F8     | G7          | None    |
		| D1     | D2          | None    |
		| C7     | C6          | None    |
		| F2     | F3          | None    |
		| B7     | B5          | None    |
		| G1     | E2          | None    |
		| B8     | D7          | None    |
		| E3     | H6          | None    |
		| G7     | H6          | Capture |
		| D2     | H6          | Capture |
		| C8     | B7          | None    |
		| A2     | A3          | None    |
		| E7     | E5          | None    |
		| E1     | C1          | Castle  |
		| D8     | E7          | None    |
		| C1     | B1          | None    |
		| A7     | A6          | None    |
		| E2     | C1          | None    |
		| E8     | C8          | Castle  |
		| C1     | B3          | None    |
		| E5     | D4          | Capture |
		| D1     | D4          | Capture |
		| C6     | C5          | None    |
		| D4     | D1          | None    |
		| D7     | B6          | None    |
		| G2     | G3          | None    |
		| C8     | B8          | None    |
		| B3     | A5          | None    |
		| B7     | A8          | None    |
		| F1     | H3          | None    |
		| D6     | D5          | None    |
		| H6     | F4          | None    |
		| B8     | A7          | None    |
		| H1     | E1          | None    |
		| D5     | D4          | None    |
		| C3     | D5          | None    |
		| B6     | D5          | Capture |
		| E4     | D5          | Capture |
		| E7     | D6          | None    |
		| D1     | D4          | Capture |
		| C5     | D4          | Capture |
		| E1     | E7          | None    |
		| A7     | B6          | None    |
		| F4     | D4          | Capture |
		| B6     | A5          | Capture |
		| B2     | B4          | None    |
		| A5     | A4          | None    |
		| D4     | C3          | None    |
		| D6     | D5          | Capture |
		| E7     | A7          | None    |
		| A8     | B7          | None    |
		| A7     | B7          | Capture |
		| D5     | C4          | None    |
		| C3     | F6          | Capture |
		| A4     | A3          | Capture |
		| F6     | A6          | Capture |
		| A3     | B4          | Capture |
		| C2     | C3          | None    |
		| B4     | C3          | Capture |
		| A6     | A1          | None    |
		| C3     | D2          | None    |
		| A1     | B2          | None    |
		| D2     | D1          | None    |
		| H3     | F1          | None    |
		| D8     | D2          | None    |
		| B7     | D7          | None    |
		| D2     | D7          | Capture |
		| F1     | C4          | Capture |
		| B5     | C4          | Capture |
		| B2     | H8          | Capture |
		| D7     | D3          | None    |
		| H8     | A8          | None    |
		| C4     | C3          | None    |
		| A8     | A4          | None    |
		| D1     | E1          | None    |
		| F3     | F4          | None    |
		| F7     | F5          | None    |
		| B1     | C1          | None    |
		| D3     | D2          | None    |
		| A4     | A7          | None    |
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
		| Source | Destination | Result  |
		| D2     | D4          | None    |
		| G8     | F6          | None    |
		| C2     | C4          | None    |
		| G7     | G6          | None    |
		| B1     | C3          | None    |
		| F8     | G7          | None    |
		| E2     | E4          | None    |
		| D7     | D6          | None    |
		| F2     | F3          | None    |
		| E8     | G8          | Castle  |
		| C1     | E3          | None    |
		| B8     | D7          | None    |
		| D1     | D2          | None    |
		| C7     | C5          | None    |
		| D4     | D5          | None    |
		| D7     | E5          | None    |
		| H2     | H3          | None    |
		| F6     | H5          | None    |
		| E3     | F2          | None    |
		| F7     | F5          | None    |
		| E4     | F5          | Capture |
		| F8     | F5          | Capture |
		| G2     | G4          | None    |
		| F5     | F3          | Capture |
		| G4     | H5          | Capture |
		| D8     | F8          | None    |
		| C3     | E4          | None    |
		| G7     | H6          | None    |
		| D2     | C2          | None    |
		| F8     | F4          | None    |
		| G1     | E2          | None    |
		| F3     | F2          | Capture |
		| E4     | F2          | Capture |
		| E5     | F3          | None    |
		| E1     | D1          | None    |
		| F4     | H4          | None    |
		| F2     | D3          | None    |
		| C8     | F5          | None    |
		| E2     | C1          | None    |
		| F3     | D2          | None    |
		| H5     | G6          | Capture |
		| H7     | G6          | Capture |
		| F1     | G2          | None    |
		| D2     | C4          | Capture |
		| C2     | F2          | None    |
		| C4     | E3          | None    |
		| D1     | E2          | None    |
		| H4     | C4          | None    |
		| G2     | F3          | None    |
		| A8     | F8          | None    |
		| H1     | G1          | None    |
		| E3     | C2          | None    |
		| E2     | D1          | None    |
		| F5     | D3          | Capture |
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
		| Source | Destination | Result  |
		| G1     | F3          | None    |
		| G8     | F6          | None    |
		| C2     | C4          | None    |
		| G7     | G6          | None    |
		| B1     | C3          | None    |
		| F8     | G7          | None    |
		| D2     | D4          | None    |
		| E8     | G8          | Castle  |
		| C1     | F4          | None    |
		| D7     | D5          | None    |
		| D1     | B3          | None    |
		| D5     | C4          | Capture |
		| B3     | C4          | Capture |
		| C7     | C6          | None    |
		| E2     | E4          | None    |
		| B8     | D7          | None    |
		| A1     | D1          | None    |
		| D7     | B6          | None    |
		| C4     | C5          | None    |
		| C8     | G4          | None    |
		| F4     | G5          | None    |
		| B6     | A4          | None    |
		| C5     | A3          | None    |
		| A4     | C3          | Capture |
		| B2     | C3          | Capture |
		| F6     | E4          | Capture |
		| G5     | E7          | Capture |
		| D8     | B6          | None    |
		| F1     | C4          | None    |
		| E4     | C3          | Capture |
		| E7     | C5          | None    |
		| F8     | E8          | None    |
		| E1     | F1          | None    |
		| G4     | E6          | None    |
		| C5     | B6          | Capture |
		| E6     | C4          | Capture |
		| F1     | G1          | None    |
		| C3     | E2          | None    |
		| G1     | F1          | None    |
		| E2     | D4          | Capture |
		| F1     | G1          | None    |
		| D4     | E2          | None    |
		| G1     | F1          | None    |
		| E2     | C3          | None    |
		| F1     | G1          | None    |
		| A7     | B6          | Capture |
		| A3     | B4          | None    |
		| A8     | A4          | None    |
		| B4     | B6          | Capture |
		| C3     | D1          | Capture |
		| H2     | H3          | None    |
		| A4     | A2          | Capture |
		| G1     | H2          | None    |
		| D1     | F2          | Capture |
		| H1     | E1          | None    |
		| E8     | E1          | Capture |
		| B6     | D8          | None    |
		| G7     | F8          | None    |
		| F3     | E1          | Capture |
		| C4     | D5          | None    |
		| E1     | F3          | None    |
		| F2     | E4          | None    |
		| D8     | B8          | None    |
		| B7     | B5          | None    |
		| H3     | H4          | None    |
		| H7     | H5          | None    |
		| F3     | E5          | None    |
		| G8     | G7          | None    |
		| H2     | G1          | None    |
		| F8     | C5          | None    |
		| G1     | F1          | None    |
		| E4     | G3          | None    |
		| F1     | E1          | None    |
		| C5     | B4          | None    |
		| E1     | D1          | None    |
		| D5     | B3          | None    |
		| D1     | C1          | None    |
		| G3     | E2          | None    |
		| C1     | B1          | None    |
		| E2     | C3          | None    |
		| B1     | C1          | None    |
		| A2     | C2          | None    |
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
		| Source | Destination | Result  |
		| E2     | E4          | None    |
		| E7     | E5          | None    |
		| G1     | F3          | None    |
		| D7     | D6          | None    |
		| D2     | D4          | None    |
		| C8     | G4          | None    |
		| D4     | E5          | Capture |
		| G4     | F3          | Capture |
		| D1     | F3          | Capture |
		| D6     | E5          | Capture |
		| F1     | C4          | None    |
		| G8     | F6          | None    |
		| F3     | B3          | None    |
		| D8     | E7          | None    |
		| B1     | C3          | None    |
		| C7     | C6          | None    |
		| C1     | G5          | None    |
		| B7     | B5          | None    |
		| C3     | B5          | Capture |
		| C6     | B5          | Capture |
		| C4     | B5          | Capture |
		| B8     | D7          | None    |
		| E1     | C1          | Castle  |
		| A8     | D8          | None    |
		| D1     | D7          | Capture |
		| D8     | D7          | Capture |
		| H1     | D1          | None    |
		| E7     | E6          | None    |
		| B5     | D7          | Capture |
		| F6     | D7          | Capture |
		| B3     | B8          | None    |
		| D7     | B8          | Capture |
		| D1     | D8          | None    |
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
		| Source | Destination | Result           |
		| E2     | E4          | None             |
		| G8     | F6          | None             |
		| E4     | E5          | None             |
		| F6     | E4          | None             |
		| D2     | D3          | None             |
		| E4     | C5          | None             |
		| D3     | D4          | None             |
		| C5     | E4          | None             |
		| D1     | D3          | None             |
		| D7     | D5          | None             |
		| E5     | D6          | CaptureAuPassant |
		| E4     | D6          | Capture          |
		| G1     | F3          | None             |
		| B7     | B5          | None             |
		| C1     | F4          | None             |
		| E7     | E5          | None             |
		| F4     | E5          | Capture          |
		| C8     | F5          | None             |
		| D3     | B3          | None             |
		| B8     | C6          | None             |
		| F1     | B5          | Capture          |
		| D8     | D7          | None             |
		| E1     | G1          | Castle           |
		| D6     | E4          | None             |
		| B1     | C3          | None             |
		| A7     | A6          | None             |
		| B5     | A4          | None             |
		| F5     | E6          | None             |
		| D4     | D5          | None             |
		| E6     | F5          | None             |
		| A4     | C6          | Capture          |
		| D7     | C6          | Capture          |
		| D5     | C6          | Capture          |
		| F8     | C5          | None             |
		| E5     | G7          | Capture          |
		| H8     | G8          | None             |
		| F3     | E5          | None             |
		| G8     | G7          | Capture          |
		| C3     | E4          | Capture          |
		| F5     | E4          | Capture          |
		| G2     | G3          | None             |
		| F7     | F5          | None             |
		| A1     | D1          | None             |
		| E4     | F3          | None             |
		| D1     | D7          | None             |
		| A8     | D8          | None             |
		| D7     | G7          | Capture          |
		| D8     | D4          | None             |
		| B3     | F7          | None             |
		| E8     | D8          | None             |
		| F7     | G8          | None             |
		| C5     | F8          | None             |
		| G8     | F8          | Capture          |
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
