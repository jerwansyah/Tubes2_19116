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
        
        public string GetName() {
            return name;
        }

        public int GetTotalMutual() {
            return totalMutual;
        }

        public List<string> GetMutualFriends {
            get {return mutualFriends;}
        }

        public void AddMutualFriend(string name) {
            if (!mutualFriends.Contains(name)) {
                mutualFriends.Add(name);
            }
            totalMutual++;
        }

    }
}