using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco
{
    public class MiscEvents
    {
        public event Action<DonBosco.API.Account> onLogout;
        public void Logout(DonBosco.API.Account account)
        {
            if (onLogout != null)
            {
                onLogout(account);
            }
        }
    }
}
