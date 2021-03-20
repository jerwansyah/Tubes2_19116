using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace BaconPancakes {
    public class friendRec {
        private string name;
        private List<string> mutualFriends;
        private int totalMutual;
        
        public friendRec(string _name) {
            name = _name;
            mutualFriends = new List<string>();
            totalMutual = 0;
        }
        
        public string getName() {
            return name;
        }

        public int getTotalMutual() {
            return totalMutual;
        }

        public List<string> getMutualFriends {
            get {return mutualFriends;}
        }

        public void addMutualFriend(string name) {
            if (!mutualFriends.Contains(name)) {
                mutualFriends.Add(name);
            }
            totalMutual++;
        }
    }
}