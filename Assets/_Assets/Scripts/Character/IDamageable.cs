using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco
{
    public interface IDamageable
    {
        void TakeDamage(float damage, GameObject source = null);
    }
}