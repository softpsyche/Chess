Feature: GameSteps
	Validate game behavior works as expected

Background: 
	Given I have a container
	Given I have a game factory

@mytag
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
