# Director-AI
This is a small Unity game with a director ai that controlls the enemies.
This project is to learn how a director ai works an how to make one.

# What is a director ai and what does it do

The director ai will controll the amount of enemies that can be in the game at a given time. It will also monitor the player to change the difficulty of the game depending on how the player is doing. The purpose of the director ai is to make sure that no playthrough feels the same as the last one. The director ai also has three states he can be in. These are:
  - build up
  - peak
  - relax
  

### Recording information about the player
A director ai records information about what the player is doing in the game. He keeps track where the player is in the level, what action the player is doing and how well the player is doing that action. The director ai will adapt the game world based on this information. 

### Adapting the game world
When the player is having a hard time getting through the level, the ai will make the game a little less intense so that the player can have a little break but still feels that he accomplished something when he finishes the level.

### Enforcing the game rules
The director ai will also enforce the game rules upon the player. For example, the player can't just stand still for a couple of minutes without anything happening around him.

# The three different states of the director ai

### Build up
The build up is the state where the director ai is monitoring the player. It keeps track of the "stress" of the player. This stress will increase when the player gets hit by an enemy or when he kills an enemy that is close to him. In my implementation, every 15 seconds the ai will check what the difference is between the stress of the player at that point and the previous check. When this is below a certain treshold, the ai will increase the amount of enemies it can have alive in the level at a certain time. When the difference is above that threshold, the ai will decrease the amount of enemies. In my game there are also some health packs that can spawn. The director ai also controlles the spawn of these. When the player is doing well, the amount of health packs that spawn will be less than when the player is having a hard time.

### Peak
When the player's stress level reaches a certain treshold the director ai will switch from the build up state to the peak state. In the peak state, the intensity increases and the ai makes one last play by spawning an extra number of enemies. This amount of enemies will depend on how well the player was doing when in the build up state. During the peak, the ai won't spawn any more enemies once all the extra enemies are spawned. During this state, the health packs won't spawn either. When the player has defeated all the enemies, the director ai's state will change to the relax state.

### Relax
When the ai enters the relax state, the player can rest for a bit. In this state there won't be any enemies spawning to give the player some rest and heal up. The player's stress level gets reset. The relax state normaly lasts for 30 - 45 seconds before the ai enters the buil up state again. When the player starts sprinting or shooting and the relax time is not yet over, the ai will also change back to the build up state.

# Conclusion
  - Ai monitors player.
  - Change the game world depending on how the player does things.
  - Works realy good with survival type games.
  
 # Used references
 I based my game a lot on how they did it in the Left 4 dead games: https://left4dead.fandom.com/wiki/The_Director
 
 some useful videos
  - https://www.youtube.com/watch?v=Mnt5zxb8W0Y
  - https://www.youtube.com/watch?v=WbHMxo11HcU


