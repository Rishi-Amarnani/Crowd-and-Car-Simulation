Crowd Simulation

Rishi Amarnani, Nick Abbs

1. How to use: You can run the artifact but the camera is fixed.So to be able to zoom and rotate the camera and look closely at what is going on, it would be better to actually open up the project in Unity 3D. You can then click scene, right click scene and click maximize, and use the mouse to control the camera.

2.	Background: Our project was designed to simulate crowd behavior using Unity, with simple guidelines governing the behavior of the agents to produce more complicated looking behavior as the number of agents increases. The source article we used, cited below in the references section, talked about how to use probabilistic transitions between “states” to simulate behaviors that look realistic on a macro level in the same way that a finite automaton performs calculations. This was a helpful abstraction to provide the basis for the functionality of our project.

3.	Description: There are three major components to our project: 1) the environment itself, and 2) The code that we wrote to dictate the behavior of the agents in simulation. 3) Interaction of pedestrian agents with the car agent
	
	1: We designed the environment using the tools that Unity 3D provides, and it was meant to have enough room to give the agents the space to demonstrate different kinds of behavior while also having interesting features that would provide opportunities and challenges for the agents to interact with them. The setup is a large rectangular grassy park, with groups of trees clustered near the middle area. The central region of the park, besides being dotted with trees, is hilly, with a pool leading off into a stream adjacent to the largest hill. The park is surrounded on all sides by sidewalks, and just beyond the sidewalks, 4 streets for a car agent to travel on, and then another sidewalk on the outside end of one of the streets.
	
	2: Our code was designed much the same way as was described in the paper we referenced, with each agent tracking which state they were currently in and occasionally making transitions by random chance. In our case, our states were the general park area, the pool, the four sidewalks surrounding the park, the sidewalk corners connecting each adjacent sidewalk, the street, and the sidewalk across the street. The agents were given different “types” that led them to behave differently in the park: either wandering around in random directions, following preset destinations to specific areas in the park, or a combination of the two. 
	
	If the agents entered the sidewalk state, either by chance wandering or because it was set as a destination, they were designed to continue walking along the sidewalks in whatever direction they were initially moving in, with a small chance that they would reenter the park or change directions. 
	
	The primary way that we implemented these behaviors was by using a counter specific to each agent. Every certain number of counts, the agent would have a small chance to begin a new behavior, such as wandering in a new direction or leaving an area (depending on what state an agent was in and its previous states). This allowed us to have our agents take a specified amount of time before starting a new behavior, letting them gaze at a pool before continuing to venture around the park. 
	The main reason we specified the pool as an area was so that we could keep our agents from randomly walking inside of it.
  	
	3: The car travels on the street, in a square around the park. We made a simple script to define its behavior. Each street can only be occupied by either the pedestrian agents, or the car agent. If a street is currently occupied by at least 1 pedestrian agent, the car must wait. If the car currently occupies a street, then the pedestrian agents must wait. Of course, multiple agents can be on the same street, when a car is not occupying it. Once the pedestrians cross the street, the stop to gaze at the beautiful flowers on the other side, then they return to the main park area.


4.	Artifact: (Unity project build)

5.	Article: https://graphics.cs.wisc.edu/Papers/2004/SGC04/crowd.pdf for our project.

6.  Unity Assets:
  	
	1: Standard Assets: Third Person Controller, CarController
  
  	2: Street Kit Asset from Unity Store for Streets: https://www.assetstore.unity3d.com/en/#!/content/13811
  
  	3: Morph 3D Asset to customize look of pedestrian agents: https://www.assetstore.unity3d.com/en/#!/content/45805
	
	4: Low Poly Flower Asset from Unity Store for Garden/Flower area: https://www.assetstore.unity3d.com/en/#!/content/47083
