Feature: GetRoadStatus

Scenario: Valid RoadStatus
	Given a valid roadID <roadID> is specified
	When the client is run
	Then the road displayName <displayName> should be displayed

Examples:
	| roadID | displayName |
	| A1     | A1          |

Scenario: RoadStatusSeverity
	Given a valid roadID <roadID> is specified
	When the client is run
	Then the road statusSeverity <statusSeverity> should be displayed
	
Examples:
	| roadID | statusSeverity  |
	| A1     | regex not empty |

Scenario: RoadStatusSeverityDescription
	Given a valid roadID <roadID> is specified
	When the client is run
	Then the road statusSeverityDescription <statusSeverityDescription> should be displayed

Examples:
	| roadID | statusSeverityDescription |
	| A1     | regex not empty           |



Scenario: Invalid Road Status
	Given an invalid roadID <roadID> is specified
	When the client is run
	Then the application should return an informative error <error>

Examples:
	| roadID             | error                                  |
	| A233 | A233 is not a valid road |


#Scenario: TerminatesWithNonZeroExitCode
#	Given an invalid roadID <roadID> is specified
#	When the client is run
#	Then the application should exit with a non-zero System Error code <errorCode>
#
#Examples:
#	| roadID      | errorCode |
#	| RoadTraffic | 1         |