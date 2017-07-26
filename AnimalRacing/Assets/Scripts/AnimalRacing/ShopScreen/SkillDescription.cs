using UnityEngine;
using System.Collections;

public class SkillDescription {
    private string[]	names;
	private string[,]	descriptions;
	
	public SkillDescription(){
		names = new string[]{"Fire", "Ice", "Shock", "Thunder", "Bomb", "Swap", "Shield", "Speed Up", "Health"};
		descriptions = new string[,]{
			{"Attack enemy by fire ball",""},
			{"Freeze enemy by ice",""},
			{"Stun enemy by flash",""},
			{"Attack and stun enemies ", "whom got hit by skill"},
			{"Attack enemy in character's ", "behind by bomb"},
			{"Swap position with enemy ", "who got hit by skill"},
			{"Make a shield to block ", "one attack"},
			{"Increase character's move ", "speed by 50%"},
			{"Increase character's", "health by 50%"}
		};
	}
	public string getName(int skillIndex){
		return this.names[skillIndex];
	}

	public string getDesciptions1(int skillIndex){
		return this.descriptions[skillIndex, 0];
	}
    public string getDesciptions2(int skillIndex)
    {
        return this.descriptions[skillIndex, 1];
    }
}
