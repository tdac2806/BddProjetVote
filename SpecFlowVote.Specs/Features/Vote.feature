Feature: Vote
@mytag
Scenario: Ajout de candidat
	Given le nom du candidat est 'Tristan Da Costa'
	And le nom du candidat est 'Eliott Delannay'
	And le nom du candidat est 'Benoit Pegaz'
	When On compte les candidats
	Then Il y a 3 candidats

Scenario: Ajout de plusieurs candidats
	Given les candidats sont
	| Nom			    |
	| Tristan Da Costa	|
	| Eliott Delannay	|
	| Benoit Pegaz      |
	When On compte les candidats
	Then Il y a 3 candidats

Scenario: Ajout d'un vote
	Given le nom du candidat est 'Tristan Da Costa'
	Given 1 vote pour 'Tristan Da Costa'
	When le vote est compter pour 'Tristan Da Costa'
	Then le candidat a 1 vote

Scenario Outline: Ajout de vote multiple
	Given le nom du candidat est '<candidat>'
	Given <vote1> vote pour '<candidat>'
	Given <vote2> vote pour '<candidat>'
	When le vote est compter pour '<candidat>'
	Then le candidat a <total> vote
	Examples: 
	| candidat         | vote1  | vote2  | total |
	| Tristan Da Costa | 2      | 2      | 4     |
	| Eliott Delannay  | 4      | 5      | 9     |
	| Benoit Pegaz     | 1      | 0      | 1     |

Scenario: comptage des votes
	Given les candidats sont
	| Nom			    |
	| Tristan Da Costa	|
	| Eliott Delannay   |
	| Benoit Pegaz 		|
	Given 2 vote pour 'Tristan Da Costa'
	Given 4 vote pour 'Eliott Delannay'
	Given 1 vote pour 'Benoit Pegaz'
	When les votes sont comptés
	Then Il y a 7 votes

	
Scenario: comptage des votes avec votes blancs
	Given les candidats sont
	| Nom			    |
	| Tristan Da Costa	|
	| Eliott Delannay   |
	| Benoit Pegaz 		|
	Given 2 vote pour 'Tristan Da Costa'
	Given 4 vote pour 'Eliott Delannay'
	Given 1 vote pour 'Benoit Pegaz'
	Given 3 vote blanc
	When les votes sont comptés
	Then Il y a 10 votes
		
Scenario: Premier tour
	Given le nom du candidat est 'Tristan Da Costa'
	And le nom du candidat est 'Eliott Delannay'
	And le nom du candidat est 'Benoit Pegaz'
	Given 2 vote pour 'Tristan Da Costa'
	Given 4 vote pour 'Eliott Delannay'
	Given 1 vote pour 'Benoit Pegaz'
	When Fin des votes
	Then le gagnant est 'Eliott Delannay'

Scenario: Gagnant du premier tour avec égalitée
	Given le nom du candidat est 'Tristan Da Costa'
	And le nom du candidat est 'Eliott Delannay'
	And le nom du candidat est 'Benoit Pegaz'
	Given 3 vote pour 'Tristan Da Costa'
	Given 3 vote pour 'Eliott Delannay'
	Given 1 vote pour 'Benoit Pegaz'
	When Fin des votes
	Then le gagnant est 'Eliott Delannay et Tristan Da Costa'

Scenario: Gagnant du deuxieme tour
	Given deuxieme tour
	Given le nom du candidat est 'Tristan Da Costa'
	And le nom du candidat est 'Eliott Delannay'
	Given 3 vote pour 'Tristan Da Costa'
	And 4 vote pour 'Eliott Delannay'
	When Fin des votes
	Then le gagnant est 'Eliott Delannay'
	
Scenario: Deuxieme tour sans gagnant
	Given deuxieme tour
	Given le nom du candidat est 'Tristan Da Costa'
	And le nom du candidat est 'Eliott Delannay'
	Given 4 vote pour 'Tristan Da Costa'
	And 4 vote pour 'Eliott Delannay'
	When Fin des votes
	Then Il n'y a pas de gagnant

Scenario: Calcul du pourcentage de vote
	Given le nom du candidat est 'Tristan Da Costa'
	And le nom du candidat est 'Eliott Delannay'
	Given 4 vote pour 'Tristan Da Costa'
	And 6 vote pour 'Eliott Delannay'
	When le pourcentage est calculer pour 'Tristan Da Costa'
	Then le pourcentage est 40


