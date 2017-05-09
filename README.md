Crowd Simulation

Rishi Amarnani, Nick Abbs

1. 	How to use: You can run the artifact but the camera is fixed. So to be able to zoom and rotate the camera and look closely at what is going on, it would be better to actually open up the project in Unity 3D. You can then click scene, right click scene and click maximize, and use the mouse to control the camera. If using the artifact, there is no input required. Simply watch the scene to see how the car and the agents behave.

2.	Background: Our project was designed to simulate crowd behavior using Unity, with simple guidelines governing the behavior of the agents to produce more complicated looking behavior as the number of agents increases. The source article we used, cited below in the references section, talked about how to use probabilistic transitions between “states” to simulate behaviors that look realistic on a macro level in the same way that a finite automaton performs calculations. This was a helpful abstraction to provide the basis for the functionality of our project.

3.	Description: There are three major components to our project: 1) the environment itself, and 2) The code that we wrote to dictate the behavior of the agents in simulation. 3) Interaction of pedestrian agents with the car agent
	
	A: We designed the environment using the tools that Unity 3D provides, and it was meant to have enough room to give the agents the space to demonstrate different kinds of behavior while also having interesting features that would provide opportunities and challenges for the agents to interact with them. The setup is a large rectangular grassy park, with groups of trees clustered near the middle area. The central region of the park, besides being dotted with trees, is hilly, with a pool leading off into a stream adjacent to the largest hill. The park is surrounded on all sides by sidewalks, and just beyond the sidewalks, 4 streets for a car agent to travel on, and then another sidewalk on the outside end of one of the streets, which leads to a beautiful garden/flower area.
	
	B: Our code was designed much the same way as was described in the paper we referenced, with each agent tracking which state they were currently in and occasionally making transitions by random chance. In our case, our states were the general park area, the pool, the four sidewalks surrounding the park, the sidewalk corners connecting each adjacent sidewalk, the street, and the sidewalk across the street. The agents were given different “types” that led them to behave differently in the park: either wandering around in random directions, following preset destinations to specific areas in the park, or a combination of the two. 
	
	If the agents entered the sidewalk state, either by chance wandering or because it was set as a destination, they were designed to continue walking along the sidewalks in whatever direction they were initially moving in, with a small chance that they would reenter the park or change directions. 
	
	The primary way that we implemented these behaviors was by using a counter specific to each agent. Every certain number of counts, the agent would have a small chance to begin a new behavior, such as wandering in a new direction or leaving an area (depending on what state an agent was in and its previous states). This allowed us to have our agents take a specified amount of time before starting a new behavior, letting them gaze at a pool before continuing to venture around the park. 
	The main reason we specified the pool as an area was so that we could keep our agents from randomly walking inside of it.
  	
	C: The car travels on the street, in a square around the park. We made a simple script to define its behavior. Each street can only be occupied by either the pedestrian agents, or the car agent. If a street is currently occupied by at least 1 pedestrian agent, the car must wait. If the car currently occupies a street, then the pedestrian agents must wait. Of course, multiple agents can be on the same street, when a car is not occupying it. Once the pedestrians cross the street, the stop to gaze at the beautiful flowers on the other side, then they return to the main park area. Additionally, if a car is waiting for pedestrians to exit the street, the pedestrians will take not of this, and they will let the car go before walking back on. They will not make the car wait indefinitely.

4. Our Code: The 5 scripts in the base folder are the scripts we worked with throughout the project. All of the scripts are entirely our own work except for the ThirdPersonController.cs script.  
	
	A. When we attached different meshes from the Morph3D Asset to the ThirdPersonController, we encounter a problem: the character would run or walk in place, but not actually translate at all. We had to add a few lines to the ThirdPersonController.cs script to fix this.
	
	B. The majority of our code is in AICharacterController.cs. This script is provided as part of the AIThirdPersonController asset, and assumes that you are using a NavMesh to control your AI agents. We deleted the navmesh component from our agents and wiped the AIThirdPersonController.cs script clean, and wrote all of our own code. If you download the AIThirdPersonController.cs script from unity, you will see the difference. This script is defined for each agent. In the script, we detect the agent's current state depending on its location in the world, and we give it some behavior, as described in the 'Description' above.
	
	C. The CarController.cs script comes with the CarController Asset. However, after much testing, we realized the methods provided were too performance-heavy because they aimed to create a realistic vehicle with audio, smoke, and gradual acceleration. For the purposes of this project, the car was present simply to interact with the AI agents on the streets. So we rewrote the CarController script to provide the car with simplistic, unrealistic, efficient movement.
	
	D. The CarUserController script also comes with the CarController Asset. We wiped this clean so that we could treat it as a AI script, defining some basic behavior for the car, namely moving on the streets around the park.
	
	E. The NeighborController script was just made to attach the terrains in our scene together. Nothing special here.

5. Artifact: Unity project build inside Build Folder (for Windows). We were unable to upload it to github, so please access it at https://drive.google.com/drive/folders/0Bx7DKYd717EGT093OENpcE9seFE?usp=sharing

6. References:
	
	A. Article for our project: https://graphics.cs.wisc.edu/Papers/2004/SGC04/crowd.pdf
	
	B. Unity Assets:
	
		1: Standard Assets: Third Person Controller, CarController
  
 		2: Street Kit Asset from Unity Store for Streets: https://www.assetstore.unity3d.com/en/#!/content/13811
  
 		3: Morph 3D Asset to customize look of pedestrian agents: https://www.assetstore.unity3d.com/en/#!/content/45805
	
		4: Low Poly Flower Asset from Unity Store for Garden/Flower area: https://www.assetstore.unity3d.com/en/#!/content/47083
